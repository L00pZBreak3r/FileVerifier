using System;
using System.Windows.Forms;

namespace FileVerifier
{
  public static class DialogHelper
  {
    public static string DoOpenFileDialog(string filter, bool checkFileExists, bool restoreDir, string filename)
    {
      OpenFileDialog fd = new OpenFileDialog();
      fd.CheckFileExists = checkFileExists;
      fd.RestoreDirectory = restoreDir;
      fd.Filter = filter;
      if (!string.IsNullOrEmpty(filename))
        fd.FileName = filename;
      return (fd.ShowDialog() == DialogResult.OK) ? fd.FileName : string.Empty;
    }
    public static string DoOpenFileDialog(string filter, bool checkFileExists, string filename)
    {
      return DoOpenFileDialog(filter, checkFileExists, false, filename);
    }
    public static string DoOpenFileDialog(string filter, bool checkFileExists)
    {
      return DoOpenFileDialog(filter, checkFileExists, false, null);
    }
    public static string DoOpenFileDialog(string filter, string filename)
    {
      return DoOpenFileDialog(filter, true, false, filename);
    }
    public static string DoOpenFileDialog(string filter)
    {
      return DoOpenFileDialog(filter, true, false, null);
    }
    public static string DoSaveFileDialog(string filter, bool restoreDir, string filename)
    {
      SaveFileDialog fd = new SaveFileDialog();
      fd.RestoreDirectory = restoreDir;
      fd.Filter = filter;
      if (!string.IsNullOrEmpty(filename))
        fd.FileName = filename;
      return (fd.ShowDialog() == DialogResult.OK) ? fd.FileName : string.Empty;
    }
    public static string DoSaveFileDialog(string filter, string filename)
    {
      return DoSaveFileDialog(filter, false, filename);
    }
    public static string DoSaveFileDialog(string filter, bool restoreDir)
    {
      return DoSaveFileDialog(filter, restoreDir, null);
    }
    public static string DoSaveFileDialog(string filter)
    {
      return DoSaveFileDialog(filter, false, null);
    }
    public static string DoFolderBrowserDialog(string description, string selpath)
    {
      FolderBrowserDialog fd = new FolderBrowserDialog();
      if (!string.IsNullOrEmpty(description))
        fd.Description = description;
      if (!string.IsNullOrEmpty(selpath))
        fd.SelectedPath = selpath;
      return (fd.ShowDialog() == DialogResult.OK) ? fd.SelectedPath : string.Empty;
    }
    public static string DoFolderBrowserDialog(string description)
    {
      return DoFolderBrowserDialog(description, null);
    }
    public static string DoFolderBrowserDialog()
    {
      return DoFolderBrowserDialog(null, null);
    }
  }
}
