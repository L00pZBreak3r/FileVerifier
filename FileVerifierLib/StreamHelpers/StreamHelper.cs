using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FileVerifierLib.ByteStringConversion;

namespace FileVerifierLib.StreamHelpers
{
  public enum PriorityItem
  {
    Biggest,
    First,
    Last
  };

  public abstract class StreamHelper
  {
    protected TextReader reader;
    protected TextWriter writer;
    public Dictionary<string, ByteStringAuto> hashes;
    public PriorityItem priority;
    public bool CaseSensitive;
    public object AdditionalInfo;
    protected abstract bool SetReader();
    protected abstract bool SetWriter();
    protected abstract bool ReadItem(out KeyValuePair<string, ByteStringAuto> item);
    protected abstract bool WriteItem(KeyValuePair<string, ByteStringAuto> item);
    protected abstract bool ReadAdditionalInfo();
    protected abstract bool WriteAdditionalInfo();
    public int Read()
    {
      AdditionalInfo = null;
      if (SetReader())
      {
        if (hashes == null)
          hashes = new Dictionary<string, ByteStringAuto>();
        int c = hashes.Count;
        using (reader)
        {
          ReadAdditionalInfo();
          KeyValuePair<string, ByteStringAuto> item;
          while (ReadItem(out item))
            if (!string.IsNullOrEmpty(item.Key))
            {
              string k = item.Key;
              if (!CaseSensitive)
                k = k.ToLower();
              if (priority == PriorityItem.Last)
                hashes[k] = item.Value;
              else
              {
                try
                {
                  hashes.Add(k, item.Value);
                }
                catch (ArgumentException)
                {
                  ByteStringAuto bs = hashes[k];
                  if (bs == null)
                    hashes[k] = item.Value;
                  else
                    if ((priority == PriorityItem.Biggest) && (item.Value != null) && (bs.Length < item.Value.Length))
                      hashes[k] = item.Value;
                }
              }
            }
          reader.Close();
        }
        return hashes.Count - c;
      }
      return 0;
    }
    public int Write(int index, int count)
    {
      int res = 0;
      if (hashes != null)
      {
        if (index < 0)
          index = 0;
        int c = hashes.Count - index;
        if ((count < 0) || (count > c))
          count = c;
        if ((count > 0) && SetWriter())
        {
          count += index;
          c = 0;
          using (writer)
          {
            WriteAdditionalInfo();
            foreach (KeyValuePair<string, ByteStringAuto> item in hashes)
            {
              if ((c >= index) && WriteItem(item))
                ++res;
              if (++c >= count)
                break;
            }
            writer.Close();
          }
        }
      }
      return res;
    }
    public int Write(int index)
    {
      return Write(index, -1);
    }
    public int Write()
    {
      int res = 0;
      if ((hashes != null) && SetWriter())
        using (writer)
        {
          WriteAdditionalInfo();
          foreach (KeyValuePair<string, ByteStringAuto> item in hashes)
            if (WriteItem(item))
              ++res;
          writer.Close();
        }
      return res;
    }
    public void SetByteStringFormat(ByteStringFormat format)
    {
      foreach (KeyValuePair<string, ByteStringAuto> item in hashes)
        item.Value.StringFormat = format;
    }
  }
}
