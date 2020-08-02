using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FileVerifier
{
  public partial class Form2 : Form
  {
    private const string UPLEVEL_FOLDER_NAME = "..";
    private string m_RootFolder;
    //private int m_iconIndex;
    public Form2()
    {
      InitializeComponent();
      FileIconHelper.SetListViewSystemImageList(listView1);
    }

    private static string GetNormalizedName(string s)
    {
      if (!string.IsNullOrEmpty(s))
      {
        s = s.ToLower().Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
        if (s.EndsWith(new string(Path.DirectorySeparatorChar, 1)))
          s = s.Substring(0, s.Length - 1);
      }
      return s;
    }

    private void LoadFileItem(string filename)
    {
      //int res = -1;
      if (!string.IsNullOrEmpty(filename))
      {
        bool bFl = File.Exists(filename);
        if (bFl || Directory.Exists(filename))
        {
          string nm = Path.GetFileName(filename);
          filename = GetNormalizedName(filename);
          if (nm == UPLEVEL_FOLDER_NAME)
          {
            filename = filename.Substring(0, filename.Length - 3);
            int d = filename.LastIndexOf(Path.DirectorySeparatorChar);
            if (d >= 0)
              filename = filename.Substring(0, d);
          }
          /*if (FileIconHelper.AddFileIcon(imageList1, filename))
            listView1.Items.Add(nm, m_iconIndex++);
          else
            listView1.Items.Add(nm);*/
          int i = FileIconHelper.GetFileIconSystemIndex(filename);
          ListViewItem lvi = (i >= 0) ? new ListViewItem(nm, i) : new ListViewItem(nm);
          FileListViewItemInfo flvii = new FileListViewItemInfo(filename, nm, (bFl) ? FileListItemType.File : FileListItemType.Directory, i);
          lvi.Tag = flvii;
          listView1.Items.Add(lvi);
        }
      }
      //return res;
    }

    private void LoadFolderToListView(string path)
    {
      listView1.Items.Clear();
      if (!string.IsNullOrEmpty(path))
      {
        DirectoryInfo di = new DirectoryInfo(path);
        if (di.Exists)
        {
          Cursor = Cursors.WaitCursor;
          if ((path.Length > m_RootFolder.Length) && path.StartsWith(m_RootFolder))
            LoadFileItem(Path.Combine(path, UPLEVEL_FOLDER_NAME));
          DirectoryInfo[] dirs = di.GetDirectories();
          foreach (DirectoryInfo dr in dirs)
            LoadFileItem(dr.FullName);
          FileInfo[] fls = di.GetFiles();
          foreach (FileInfo fl in fls)
            LoadFileItem(fl.FullName);
          Cursor = Cursors.Default;
        }
      }
    }

    public string RootFolder
    {
      get
      {
        return m_RootFolder;
      }
      set
      {
        if (m_RootFolder != value)
        {
          m_RootFolder = GetNormalizedName(value);
          LoadFolderToListView(m_RootFolder);
        }
      }
    }

    private void AddFileNameToList(string filename)
    {
      if (listBox2.Items.IndexOf(filename) < 0)
        listBox2.Items.Add(filename);
    }

    private void AddFolderToList(string path, bool rec)
    {
      DirectoryInfo di = new DirectoryInfo(path);
      if (di.Exists)
      {
        if (rec)
        {
          DirectoryInfo[] dirs = di.GetDirectories();
          foreach (DirectoryInfo dr in dirs)
            AddFolderToList(dr.FullName, rec);
        }
        FileInfo[] fls = di.GetFiles();
        foreach (FileInfo fl in fls)
          AddFileNameToList(GetNormalizedName(fl.FullName));
      }
    }

    private void AddListViewItemsToList(bool rec)
    {
      Cursor = Cursors.WaitCursor;
      foreach (ListViewItem lvi in listView1.SelectedItems)
      {
        FileListViewItemInfo ii = lvi.Tag as FileListViewItemInfo;
        if (ii != null)
        {
          if (ii.ItemType == FileListItemType.Directory)
          {
            if (rec)
              AddFolderToList(ii.FullName, rec);
          }
          else
            AddFileNameToList(ii.FullName);
        }
      }
      label3.Text = listBox2.Items.Count.ToString();
      Cursor = Cursors.Default;
    }

    private void listView1_DoubleClick(object sender, EventArgs e)
    {
      if (listView1.SelectedItems.Count > 0)
      {
        ListViewItem lvi = listView1.SelectedItems[0];
        FileListViewItemInfo ii = lvi.Tag as FileListViewItemInfo;
        if (ii != null)
        {
          if (ii.ItemType == FileListItemType.Directory)
            LoadFolderToListView(ii.FullName);
          else
          {
            AddFileNameToList(ii.FullName);
            label3.Text = listBox2.Items.Count.ToString();
          }
        }
      }
    }

    private void button2_Click(object sender, EventArgs e)
    {
      listBox2.Items.Clear();
      label3.Text = "0";
      button7.Enabled = false;
    }

    private void button7_Click(object sender, EventArgs e)
    {
      while (listBox2.SelectedItems.Count > 0)
        listBox2.Items.Remove(listBox2.SelectedItems[0]);
      label3.Text = listBox2.Items.Count.ToString();
    }

    private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
      button7.Enabled = listBox2.SelectedItems.Count > 0;
    }

    private void label3_TextChanged(object sender, EventArgs e)
    {
      button5.Enabled = button2.Enabled = listBox2.Items.Count > 0;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      /*listView1.Select();
      SendKeys.Send("{HOME}");
      SendKeys.Send("+{END}");*/
      int c = listView1.Items.Count;
      for (int i = 0; i < c; ++i)
        listView1.Items[i].Selected = true;
      listView1.Select();
    }

    private void button3_Click(object sender, EventArgs e)
    {
      AddListViewItemsToList(false);
    }

    private void listView1_SelectedIndexChanged(object sender, EventArgs e)
    {
      button4.Enabled = button3.Enabled = listView1.SelectedItems.Count > 0;
    }

    private void button4_Click(object sender, EventArgs e)
    {
      AddListViewItemsToList(true);
    }
  }

  enum FileListItemType
  {
    File,
    Directory
  };

  class FileListViewItemInfo
  {
    public string FullName;
    public string DisplayName;
    public FileListItemType ItemType;
    public int IconIndex;
    public FileListViewItemInfo(string fullName, string displayName, FileListItemType itemType, int iconIndex)
    {
      FullName = fullName;
      DisplayName = displayName;
      ItemType = itemType;
      IconIndex = iconIndex;
    }
    public FileListViewItemInfo()
    {
    }
  }
}