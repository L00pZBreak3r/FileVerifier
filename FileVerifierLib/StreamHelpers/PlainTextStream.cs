using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FileVerifierLib.ByteStringConversion;

namespace FileVerifierLib.StreamHelpers
{
  public class PlainTextStream : StreamHelper
  {
    private static readonly string[] STRING_COMMENT = { "#", ";" };
    private const string STRING_HASH_DELIMITER = " *";
    private readonly Stream stream;
    private readonly string path;
    public Encoding encoding = Encoding.Default;
    public bool detectEncodingFromByteOrderMarks;
    public bool append;
    private int m_ReadCount;
    public PlainTextStream(Stream s, Encoding enc, Dictionary<string, ByteStringAuto> h, PriorityItem p, bool caseSensitive)
    {
      stream = s;
      encoding = enc;
      hashes = h;
      priority = p;
      CaseSensitive = caseSensitive;
    }
    public PlainTextStream(Stream s, Dictionary<string, ByteStringAuto> h, PriorityItem p, bool caseSensitive)
    {
      stream = s;
      hashes = h;
      priority = p;
      CaseSensitive = caseSensitive;
    }
    public PlainTextStream(Stream s, Encoding enc, Dictionary<string, ByteStringAuto> h, PriorityItem p)
    {
      stream = s;
      encoding = enc;
      hashes = h;
      priority = p;
    }
    public PlainTextStream(Stream s, Dictionary<string, ByteStringAuto> h, PriorityItem p)
    {
      stream = s;
      hashes = h;
      priority = p;
    }
    public PlainTextStream(Stream s, Encoding enc, Dictionary<string, ByteStringAuto> h, bool caseSensitive)
    {
      stream = s;
      encoding = enc;
      hashes = h;
      CaseSensitive = caseSensitive;
    }
    public PlainTextStream(Stream s, Dictionary<string, ByteStringAuto> h, bool caseSensitive)
    {
      stream = s;
      hashes = h;
      CaseSensitive = caseSensitive;
    }
    public PlainTextStream(Stream s, Encoding enc, PriorityItem p, bool caseSensitive)
    {
      stream = s;
      encoding = enc;
      priority = p;
      CaseSensitive = caseSensitive;
    }
    public PlainTextStream(Stream s, PriorityItem p, bool caseSensitive)
    {
      stream = s;
      priority = p;
      CaseSensitive = caseSensitive;
    }
    public PlainTextStream(Stream s, Encoding enc, Dictionary<string, ByteStringAuto> h)
    {
      stream = s;
      encoding = enc;
      hashes = h;
    }
    public PlainTextStream(Stream s, Dictionary<string, ByteStringAuto> h)
    {
      stream = s;
      hashes = h;
    }
    public PlainTextStream(Stream s, Encoding enc, bool caseSensitive)
    {
      stream = s;
      encoding = enc;
      CaseSensitive = caseSensitive;
    }
    public PlainTextStream(Stream s, bool caseSensitive)
    {
      stream = s;
      CaseSensitive = caseSensitive;
    }
    public PlainTextStream(Stream s, Encoding enc, PriorityItem p)
    {
      stream = s;
      encoding = enc;
      priority = p;
    }
    public PlainTextStream(Stream s, PriorityItem p)
    {
      stream = s;
      priority = p;
    }
    public PlainTextStream(Stream s, Encoding enc)
    {
      stream = s;
      encoding = enc;
    }
    public PlainTextStream(Stream s)
    {
      stream = s;
    }
    public PlainTextStream(string fp, Encoding enc, Dictionary<string, ByteStringAuto> h, PriorityItem p, bool caseSensitive)
    {
      path = fp;
      encoding = enc;
      hashes = h;
      priority = p;
      CaseSensitive = caseSensitive;
    }
    public PlainTextStream(string fp, Dictionary<string, ByteStringAuto> h, PriorityItem p, bool caseSensitive)
    {
      path = fp;
      hashes = h;
      priority = p;
      CaseSensitive = caseSensitive;
    }
    public PlainTextStream(string fp, Encoding enc, Dictionary<string, ByteStringAuto> h, PriorityItem p)
    {
      path = fp;
      encoding = enc;
      hashes = h;
      priority = p;
    }
    public PlainTextStream(string fp, Dictionary<string, ByteStringAuto> h, PriorityItem p)
    {
      path = fp;
      hashes = h;
      priority = p;
    }
    public PlainTextStream(string fp, Encoding enc, Dictionary<string, ByteStringAuto> h, bool caseSensitive)
    {
      path = fp;
      encoding = enc;
      hashes = h;
      CaseSensitive = caseSensitive;
    }
    public PlainTextStream(string fp, Dictionary<string, ByteStringAuto> h, bool caseSensitive)
    {
      path = fp;
      hashes = h;
      CaseSensitive = caseSensitive;
    }
    public PlainTextStream(string fp, Encoding enc, PriorityItem p, bool caseSensitive)
    {
      path = fp;
      encoding = enc;
      priority = p;
      CaseSensitive = caseSensitive;
    }
    public PlainTextStream(string fp, PriorityItem p, bool caseSensitive)
    {
      path = fp;
      priority = p;
      CaseSensitive = caseSensitive;
    }
    public PlainTextStream(string fp, Encoding enc, Dictionary<string, ByteStringAuto> h)
    {
      path = fp;
      encoding = enc;
      hashes = h;
    }
    public PlainTextStream(string fp, Dictionary<string, ByteStringAuto> h)
    {
      path = fp;
      hashes = h;
    }
    public PlainTextStream(string fp, Encoding enc, bool caseSensitive)
    {
      path = fp;
      encoding = enc;
      CaseSensitive = caseSensitive;
    }
    public PlainTextStream(string fp, bool caseSensitive)
    {
      path = fp;
      CaseSensitive = caseSensitive;
    }
    public PlainTextStream(string fp, Encoding enc, PriorityItem p)
    {
      path = fp;
      encoding = enc;
      priority = p;
    }
    public PlainTextStream(string fp, PriorityItem p)
    {
      path = fp;
      priority = p;
    }
    public PlainTextStream(string fp, Encoding enc)
    {
      path = fp;
      encoding = enc;
    }
    public PlainTextStream(string fp)
    {
      path = fp;
    }
    protected override bool SetReader()
    {
      m_ReadCount = 0;
      bool bstrm = stream != null;
      bool fext = File.Exists(path);
      reader = (bstrm && stream.CanRead || fext) ? ((bstrm) ? ((encoding != null) ? new StreamReader(stream, encoding, detectEncodingFromByteOrderMarks) : new StreamReader(stream, detectEncodingFromByteOrderMarks)) : ((encoding != null) ? new StreamReader(path, encoding, detectEncodingFromByteOrderMarks) : new StreamReader(path, detectEncodingFromByteOrderMarks))) : null;
      return reader != null;
    }
    protected override bool SetWriter()
    {
      bool bstrm = stream != null;
      bool fext = !string.IsNullOrEmpty(path);
      writer = (bstrm && stream.CanWrite || fext) ? ((bstrm) ? ((encoding != null) ? new StreamWriter(stream, encoding) : new StreamWriter(stream)) : ((encoding != null) ? new StreamWriter(path, append, encoding) : new StreamWriter(path, append))) : null;
      return writer != null;
    }
    protected override bool ReadItem(out KeyValuePair<string, ByteStringAuto> item)
    {
      item = new KeyValuePair<string, ByteStringAuto>();
      string s;
      do
      {
        s = reader.ReadLine();
        if (s == null)
          return false;
        s = s.TrimStart(null);
        for (int j = 0; j < STRING_COMMENT.Length; ++j)
          if (s.StartsWith(STRING_COMMENT[j]))
          {
            s = string.Empty;
            break;
          }
      }
      while (s == string.Empty);
      int i = s.IndexOf(STRING_HASH_DELIMITER);
      bool res = i >= 0;
      if (res)
      {
        string h = s.Substring(0, i).TrimEnd(null);
        s = s.Substring(i + STRING_HASH_DELIMITER.Length).TrimStart(new char[] { Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar });
        res = !string.IsNullOrEmpty(s);
        if (res)
        {
          ByteStringAuto bs;
          try
          {
            bs = new ByteStringAuto(h);
          }
          catch (FormatException)
          {
            bs = null;
          }
          res = (bs != null) && (bs.Length > 0);
          if (res)
            item = new KeyValuePair<string, ByteStringAuto>(s.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar), bs);
        }
      }
      else
        if ((m_ReadCount == 0) && !string.IsNullOrEmpty(path) && (reader.Peek() == -1))
        {
          s = s.TrimEnd(null);
          ByteStringAuto bs = new ByteStringAuto(s);
          res = bs.Length > 0;
          if (res)
          {
            s = Path.GetFileNameWithoutExtension(path);
            res = !string.IsNullOrEmpty(s);
            if (res)
              item = new KeyValuePair<string, ByteStringAuto>(s, bs);
          }
        }
      if (res)
        ++m_ReadCount;
      return res;
    }
    protected override bool WriteItem(KeyValuePair<string, ByteStringAuto> item)
    {
      bool res = !string.IsNullOrEmpty(item.Key) && (item.Value != null) && (item.Value.Length > 0);
      if (res)
        writer.WriteLine(string.Concat(item.Value.StringAuto, STRING_HASH_DELIMITER, item.Key.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)));
      return res;
    }
    protected override bool ReadAdditionalInfo()
    {
      return false;
    }
    protected override bool WriteAdditionalInfo()
    {
      string s = AdditionalInfo as string;
      bool res = !string.IsNullOrEmpty(s);
      if (res)
        writer.WriteLine(string.Concat(STRING_COMMENT[0], " ", s.Replace("\n", string.Concat("\n", STRING_COMMENT[0], " ")), "\n"));
      return res;
    }
  }
}
