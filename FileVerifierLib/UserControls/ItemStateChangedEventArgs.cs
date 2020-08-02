using System;
using System.Windows.Forms;

namespace FileVerifierLib.UserControls
{
  public enum ItemEventType
  {
    Add,
    StartProcessing,
    SuccessMatch,
    SuccessCompute,
    ErrorFileNotFound,
    ErrorAccessDenied,
    ErrorHashNotSpecified,
    ErrorHashNotMatch
  };
  public class ItemStateChangedEventArgs : EventArgs
  {
    public readonly ItemEventType EventType; //m_ev;
    public readonly ListViewItem Item; //m_item;
    public readonly int ItemIndex; //m_index;
    public bool Proceed = true; //m_result;
    public ItemStateChangedEventArgs(ItemEventType ev, ListViewItem item, int index)
    {
      EventType = ev;
      Item = item;
      ItemIndex = index;
    }
    /*public ItemEventType EventType
    {
      get
      {
        return m_ev;
      }
    }
    public ListViewItem Item
    {
      get
      {
        return m_item;
      }
    }
    public int ItemIndex
    {
      get
      {
        return m_index;
      }
    }*/
  }
}
