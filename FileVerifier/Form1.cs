using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using FileVerifierLib.ByteStringConversion;
using FileVerifierLib.StreamHelpers;
using FileVerifierLib.UserControls;

namespace FileVerifier
{
  public partial class Form1 : Form
  {
    private const string FILTER_STRING = "Checksum files (*.md5)|*.md5|Text files (*.txt)|*.txt|All checksum files (*.md5;*.txt)|*.md5;*.txt|All files (*.*)|*.*";
    private const string FOLDER_DESCRIPTION = "Select the root folder:";
    private bool m_init;
    //private PlainTextStream m_pts;
    private Dictionary<string, ByteStringAuto> dic;
    private string addinfo;
    private int m_mode;
    private int m_exres;
    //private string m_flnm;
    public Form1()
    {
      InitializeComponent();
    }

    private PlainTextStream getTextFile()
    {
      PlainTextStream res = null;
      string s = textBox2.Text;
      if (m_mode == 0)
        while (!string.IsNullOrEmpty(s) && !File.Exists(s))
        {
          s = DialogHelper.DoOpenFileDialog(FILTER_STRING);
          if (!string.IsNullOrEmpty(s))
            textBox2.Text = s;
        }
      if (!string.IsNullOrEmpty(s))
        res = new PlainTextStream(s);
      return res;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      m_exres = -1;
      if (m_init)
      {
        Cursor = Cursors.WaitCursor;
        fileHashListView1.RefreshInProcess = checkBox1.Checked;
        fileHashListView1.ProcessingMode = (FileVerifierLib.HashHelpers.HashAlgorithmType)m_mode;
        progressBar1.Value = progressBar1.Minimum;
        progressBar1.Show();
        DateTime dt1 = DateTime.Now;
        m_exres = fileHashListView1.Execute();
        TimeSpan tsp1 = DateTime.Now - dt1;
        progressBar1.Hide();
        Cursor = Cursors.Default;
        MessageBox.Show(m_exres.ToString() + " errors.\nElapsed time: " + tsp1.ToString(), comboBox1.Text, MessageBoxButtons.OK, (m_exres > 0) ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

      }
      else
      {
        string rp = textBox1.Text;
        while (!string.IsNullOrEmpty(rp) && !Directory.Exists(rp))
        {
          rp = DialogHelper.DoFolderBrowserDialog(FOLDER_DESCRIPTION);
          if (!string.IsNullOrEmpty(rp))
            textBox1.Text = rp;
        }
        if (!string.IsNullOrEmpty(rp))
        {
          m_mode = comboBox1.SelectedIndex;
          fileHashListView1.RefreshInProcess = checkBox1.Checked;
          fileHashListView1.ProcessingMode = (FileVerifierLib.HashHelpers.HashAlgorithmType)m_mode;
          fileHashListView1.Hashes = null;
          fileHashListView1.RootPath = rp;
          if (m_mode == 0)
          {
            PlainTextStream pts = getTextFile();
            if ((pts != null) && (pts.Read() > 0))
            {
              Cursor = Cursors.WaitCursor;
              fileHashListView1.Hashes = pts.hashes;
              progressBar1.Value = progressBar1.Minimum;
              progressBar1.Maximum = pts.hashes.Count << 1;
              progressBar1.Show();
              DateTime dt1 = DateTime.Now;
              m_exres = fileHashListView1.Execute();
              TimeSpan tsp1 = DateTime.Now - dt1;
              progressBar1.Hide();
              Cursor = Cursors.Default;
              MessageBox.Show(m_exres.ToString() + " errors.\nElapsed time: " + tsp1.ToString(), comboBox1.Text, MessageBoxButtons.OK, (m_exres > 0) ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
              m_init = true;
            }
          }
          else
          {
            dic = null;
            using (Form2 fm2 = new Form2())
            {
              fm2.RootFolder = fileHashListView1.RootPath;
              if (fm2.ShowDialog(this) == DialogResult.OK)
              {
                int cc = fm2.listBox2.Items.Count;
                if (cc > 0)
                {
                  Cursor = Cursors.WaitCursor;
                  int rl = fm2.RootFolder.Length + 1;
                  dic = new Dictionary<string, ByteStringAuto>(cc);
                  foreach (object fl in fm2.listBox2.Items)
                    dic.Add((fl as string).Substring(rl), null);
                  Cursor = Cursors.Default;
                }
              }
            }
            if (dic != null)
            {
              Cursor = Cursors.WaitCursor;
              fileHashListView1.Hashes = dic;
              progressBar1.Value = progressBar1.Minimum;
              progressBar1.Maximum = dic.Count << 1;
              progressBar1.Show();
              DateTime dt1 = DateTime.Now;
              m_exres = fileHashListView1.Execute();
              TimeSpan tsp1 = DateTime.Now - dt1;
              progressBar1.Hide();
              //if (m_exres == 0)
              //{
              addinfo = string.Concat(fileHashListView1.ProcessingMode.ToString(), " checksums generated by ", ProductName, string.Concat("\nGenerated ", DateTime.Now.ToString()));
              m_init = true;
              //}
              Cursor = Cursors.Default;
              MessageBox.Show(m_exres.ToString() + " errors.\nElapsed time: " + tsp1.ToString(), comboBox1.Text, MessageBoxButtons.OK, (m_exres > 0) ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
            }
          }
        }
        button2.Enabled = m_init;
      }
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      string s = DialogHelper.DoFolderBrowserDialog(FOLDER_DESCRIPTION, textBox1.Text);
      if (!string.IsNullOrEmpty(s))
        textBox1.Text = s;
    }

    private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      string m_flnm = textBox1.Text.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
      if (!string.IsNullOrEmpty(m_flnm))
      {
        if (m_flnm.EndsWith(new string(Path.DirectorySeparatorChar, 1)))
          m_flnm = m_flnm.Substring(0, m_flnm.Length - 1);
        m_flnm = Path.GetFileName(m_flnm) + ".md5";
      }
      string s = (comboBox1.SelectedIndex <= 0) ? DialogHelper.DoOpenFileDialog(FILTER_STRING, m_flnm) : DialogHelper.DoSaveFileDialog(FILTER_STRING, m_flnm);
      if (!string.IsNullOrEmpty(s))
        textBox2.Text = s;
    }

    private void button2_Click(object sender, EventArgs e)
    {
      if (m_init)
        if (m_mode == 0)
        {
          string m_flnm = textBox2.Text;
          string s = DialogHelper.DoSaveFileDialog("Text files (*.txt)|*.txt", (string.IsNullOrEmpty(m_flnm)) ? null : (Path.GetFileNameWithoutExtension(m_flnm) + ".txt"));
          if (!string.IsNullOrEmpty(s))
          {
            Dictionary<string, HashCheckResult> res = fileHashListView1.Result;
            if ((res != null) && (res.Count > 0))
            {
              Cursor = Cursors.WaitCursor;
              using (StreamWriter sr = new StreamWriter(s))
              {
                foreach (KeyValuePair<string, HashCheckResult> ritem in res)
                  sr.WriteLine(string.Concat(ritem.Key, " ", (ritem.Value.IsOk) ? "OK" : string.Concat("ERROR: ", ritem.Value.ErrorMessage)));
                //sr.Close();
              }
              Cursor = Cursors.Default;
            }
          }
        }
        else
          if ((m_mode > 0) && (dic != null))
          {
            bool b = m_exres > 0;
            if (b)
              b = MessageBox.Show(m_exres.ToString() + " errors.\nDo you want to save the output file anyway?", comboBox1.Text, MessageBoxButtons.YesNo) == DialogResult.No;
            if (!b)
            {
              Cursor = Cursors.WaitCursor;
              PlainTextStream pts = getTextFile();
              int i = -1;
              if (pts != null)
              {
                pts.hashes = dic;
                pts.AdditionalInfo = addinfo;
                pts.SetByteStringFormat(((saveMenu.Items[0] as ToolStripMenuItem).Checked) ? ByteStringFormat.Bytes : ByteStringFormat.Base64);
                i = pts.Write();
              }
              Cursor = Cursors.Default;
              if (i >= 0)
                MessageBox.Show(i.ToString() + " checksums written to output file.", comboBox1.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
              else
                MessageBox.Show("Wrong output file specified.", comboBox1.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          }
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
      m_mode = comboBox1.SelectedIndex;
      m_exres = -1;
      m_init = false;
      //m_flnm = string.Empty;
      button2.Enabled = false;
    }

    private void fileHashListView1_ItemStateChanged(object sender, ItemStateChangedEventArgs e)
    {
      if (e.EventType != ItemEventType.Add)
        progressBar1.PerformStep();
    }

    private void toolStripMenuItem1_CheckedChanged(object sender, EventArgs e)
    {
      (sender as ToolStripMenuItem).Checked = true;
      (saveMenu.Items[(saveMenu.Items[0].Equals(sender)) ? 1 : 0] as ToolStripMenuItem).Checked = false;
    }

    private void textBox2_TextChanged(object sender, EventArgs e)
    {
      if (m_mode == 0)
      {
        m_exres = -1;
        m_init = false;
        button2.Enabled = false;
      }
    }
  }
}