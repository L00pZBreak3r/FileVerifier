using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Windows.Forms;
using FileVerifierLib.ByteStringConversion;
using FileVerifierLib.HashHelpers;

namespace FileVerifierLib.UserControls
{
  public struct HashCheckResult
  {
    public bool IsOk;
    public string ErrorMessage;
    public HashCheckResult(bool isok, string errmsg)
    {
      IsOk = isok;
      ErrorMessage = errmsg;
    }
  }

  public partial class FileHashListView : ListView
  {
    private enum ItemStateValue
    {
      Unprocessed,
      Processing,
      Ok,
      Error
    };
    private static readonly string ERROR_TEXT_FILE_NOT_PROCESSED = Properties.FileHashListViewResources.ERROR_TEXT_FILE_NOT_PROCESSED;
    private static readonly string ERROR_TEXT_FILE_NOT_FOUND = Properties.FileHashListViewResources.ERROR_TEXT_FILE_NOT_FOUND;
    private static readonly string ERROR_TEXT_FILE_NOT_ACCESSIBLE = Properties.FileHashListViewResources.ERROR_TEXT_FILE_NOT_ACCESSIBLE;
    private static readonly string ERROR_TEXT_HASH_NOT_SPECIFIED = Properties.FileHashListViewResources.ERROR_TEXT_HASH_NOT_SPECIFIED;
    private static readonly string ERROR_TEXT_HASH_NOT_MATCH = Properties.FileHashListViewResources.ERROR_TEXT_HASH_NOT_MATCHED;
    private static readonly string ERROR_TEXT_SUCCESS = Properties.FileHashListViewResources.ERROR_TEXT_SUCCESS;
    private const int IMAGE_INDEX_UNPROCESSED = (int)ItemStateValue.Unprocessed;
    private const int IMAGE_INDEX_PROCESSING = (int)ItemStateValue.Processing;
    private const int IMAGE_INDEX_OK = (int)ItemStateValue.Ok;
    private const int IMAGE_INDEX_ERROR = (int)ItemStateValue.Error;
    private Dictionary<string, ByteStringAuto> m_hashes;
    private Dictionary<string, HashCheckResult> m_result;
    private string m_Root = string.Empty;
    private int m_ErrorCount = -1;
    private HashAlgorithmType m_ProcessingMode;
    private bool m_RefreshInProcess;
    public delegate void ItemStateChangedEventHandler(object sender, ItemStateChangedEventArgs e);
    public event ItemStateChangedEventHandler ItemStateChanged;
    public FileHashListView()
    {
      InitializeComponent();
    }
    [Browsable(false)]
    public Dictionary<string, ByteStringAuto> Hashes
    {
      get
      {
        return m_hashes;
      }
      set
      {
        m_result = null;
        m_ErrorCount = -1;
        Items.Clear();
        m_hashes = value;
        if ((m_hashes != null) && (m_hashes.Count > 0))
        {
          foreach (KeyValuePair<string, ByteStringAuto> item in m_hashes)
          {
            string h = string.Empty;
            string tt = string.Empty;
            object obj = null;
            if (m_ProcessingMode <= HashAlgorithmType.Unknown)
            {
              if (item.Value != null)
              {
                h = item.Value.StringBytes;
                if (item.Value.Bytes != null)
                {
                  HashHelper hh = new HashHelper(item.Value);
                  if (hh.AlgorithmType > HashAlgorithmType.Unknown)
                  {
                    obj = hh;
                    tt = hh.AlgorithmType.ToString();
                  }
                }
              }
            }
            else
            {
              obj = new HashHelper(m_ProcessingMode);
              tt = m_ProcessingMode.ToString();
            }
            ListViewItem lvi = new ListViewItem(new string[] { string.Empty, item.Key, h, string.Empty }, IMAGE_INDEX_UNPROCESSED);
            lvi.ToolTipText = tt;
            lvi.SubItems[2].Tag = obj;
            if (DoItemStateChanged(ItemEventType.Add, lvi, Items.Count))
              Items.Add(lvi);
          }
        }
        //Refresh();
      }
    }
    [Description("Defines path to root folder.")]
    public string RootPath
    {
      get
      {
        return m_Root;
      }
      set
      {
        m_Root = (value != null) ? value : string.Empty;
        if (!string.IsNullOrEmpty(m_Root))
          m_Root = Path.GetFullPath(m_Root.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar).TrimEnd(new char[] { Path.DirectorySeparatorChar }));
      }
    }
    public int Execute()
    {
      m_result = null;
      m_ErrorCount = -1;
      int c = Items.Count;
      if (!string.IsNullOrEmpty(m_Root) && (c > 0))
      {
        ++m_ErrorCount;
        for (int i = 0; i < c; ++i)
        {
          string s = Path.Combine(m_Root, Items[i].SubItems[1].Text);
          if (File.Exists(s))
          {
            HashHelper hh0 = Items[i].SubItems[2].Tag as HashHelper;
            if ((hh0 != null) && (hh0.AlgorithmType > HashAlgorithmType.Unknown) && ((m_ProcessingMode > HashAlgorithmType.Unknown) || (hh0.HashValue != null) && (hh0.HashValue.Length > 0)))
            {
              if (DoItemStateChanged(ItemEventType.StartProcessing, Items[i], i))
              {
                Items[i].ImageIndex = IMAGE_INDEX_PROCESSING;
                if (m_RefreshInProcess)
                {
                  EnsureVisible(i);
                  Refresh();
                }
                byte[] h0 = (hh0.HashValue != null) ? hh0.HashValue.Bytes : null;
                bool bCR;
                using (FileStream rfs = new FileStream(s, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                  bCR = hh0.ComputeHash(rfs);
                  //rfs.Close();
                }
                if (bCR)
                {
                  if (m_ProcessingMode <= HashAlgorithmType.Unknown)
                  {
                    if (hh0.HashValue.Equals(h0))
                    {
                      if (DoItemStateChanged(ItemEventType.SuccessMatch, Items[i], i))
                      {
                        Items[i].ImageIndex = IMAGE_INDEX_OK;
                        Items[i].SubItems[3].Text = ERROR_TEXT_SUCCESS;
                      }
                    }
                    else
                    {
                      hh0.HashValue.Bytes = h0;
                      if (DoItemStateChanged(ItemEventType.ErrorHashNotMatch, Items[i], i))
                      {
                        Items[i].SubItems[3].Text = ERROR_TEXT_HASH_NOT_MATCH;
                        Items[i].ImageIndex = IMAGE_INDEX_ERROR;
                        ++m_ErrorCount;
                      }
                    }
                  }
                  else
                  {
                    m_hashes[Items[i].SubItems[1].Text] = hh0.HashValue;
                    Items[i].SubItems[2].Text = hh0.HashValue.StringBytes;
                    Items[i].SubItems[3].Text = ERROR_TEXT_SUCCESS;
                    Items[i].ToolTipText = hh0.AlgorithmType.ToString();
                    if (DoItemStateChanged(ItemEventType.SuccessCompute, Items[i], i))
                      Items[i].ImageIndex = IMAGE_INDEX_OK;
                  }
                }
                else
                {
                  if (DoItemStateChanged(ItemEventType.ErrorAccessDenied, Items[i], i))
                  {
                    Items[i].SubItems[3].Text = ERROR_TEXT_FILE_NOT_ACCESSIBLE;
                    Items[i].ImageIndex = IMAGE_INDEX_ERROR;
                    ++m_ErrorCount;
                  }
                }
              }
            }
            else
            {
              if (DoItemStateChanged(ItemEventType.ErrorHashNotSpecified, Items[i], i))
              {
                Items[i].SubItems[3].Text = ERROR_TEXT_HASH_NOT_SPECIFIED;
                Items[i].ImageIndex = IMAGE_INDEX_ERROR;
                ++m_ErrorCount;
              }
            }
          }
          else
          {
            if (DoItemStateChanged(ItemEventType.ErrorFileNotFound, Items[i], i))
            {
              Items[i].SubItems[3].Text = ERROR_TEXT_FILE_NOT_FOUND;
              Items[i].ImageIndex = IMAGE_INDEX_ERROR;
              ++m_ErrorCount;
            }
          }
          if (m_RefreshInProcess)
          {
            EnsureVisible(i);
            Refresh();
          }
        }
      }
      return m_ErrorCount;
    }
    [Browsable(false), Description("Error count.")]
    public int ErrorCount
    {
      get
      {
        return m_ErrorCount;
      }
    }
    public HashAlgorithmType ProcessingMode
    {
      get
      {
        return m_ProcessingMode;
      }
      set
      {
        HashAlgorithmType old = m_ProcessingMode;
        m_ProcessingMode = value;
        if ((m_ProcessingMode != old) && (m_ProcessingMode > HashAlgorithmType.Unknown))
        {
          m_result = null;
          m_ErrorCount = -1;
          int c = Items.Count;
          for (int i = 0; i < c; ++i)
          {
            HashHelper hh0 = Items[i].SubItems[2].Tag as HashHelper;
            if (hh0 != null)
              hh0.AlgorithmType = m_ProcessingMode;
            else
              Items[i].SubItems[2].Tag = new HashHelper(m_ProcessingMode);
            Items[i].ImageIndex = IMAGE_INDEX_UNPROCESSED;
            Items[i].ToolTipText = m_ProcessingMode.ToString();
            Items[i].SubItems[2].Text = string.Empty;
            Items[i].SubItems[3].Text = string.Empty;
          }
        }
      }
    }
    public bool RefreshInProcess
    {
      get
      {
        return m_RefreshInProcess;
      }
      set
      {
        m_RefreshInProcess = value;
      }
    }
    [Browsable(false)]
    public Dictionary<string, HashCheckResult> Result
    {
      get
      {
        int c = Items.Count;
        if ((m_result == null) && (c > 0))
        {
          m_result = new Dictionary<string, HashCheckResult>(c);
          for (int i = 0; i < c; ++i)
          {
            int r = Items[i].ImageIndex;
            m_result.Add(Items[i].SubItems[1].Text, new HashCheckResult(r == IMAGE_INDEX_OK, (r == IMAGE_INDEX_UNPROCESSED) ? ERROR_TEXT_FILE_NOT_PROCESSED : Items[i].SubItems[3].Text));
          }
        }
        return m_result;
      }
    }
    private bool DoItemStateChanged(ItemEventType ev, ListViewItem item, int index)
    {
      ItemStateChangedEventArgs e = new ItemStateChangedEventArgs(ev, item, index);
      OnItemStateChanged(e);
      return e.Proceed;
    }
    protected virtual void OnItemStateChanged(ItemStateChangedEventArgs e)
    {
      if (ItemStateChanged != null)
        ItemStateChanged(this, e);
    }
  }
}
