using System;
using System.Text;
using System.IO;

namespace FileVerifierLib.ByteStringConversion
{
  public class ByteString
  {
    private byte[] m_bytes;
    private string m_strbase64 = string.Empty;
    private string m_strbytes = string.Empty;
    private static string BytesToString(byte[] src)
    {
      if (src != null)
      {
        int c = src.Length;
        if (c > 0)
        {
          StringBuilder sb = new StringBuilder(c << 1);
          for (int i = 0; i < c; ++i)
            sb.Append(src[i].ToString("x2"));
          return sb.ToString();
        }
      }
      return string.Empty;
    }
    private static byte[] StringToBytes(string src)
    {
      if (src != null)
      {
        int c = src.Length;
        if ((c > 0) && (c % 2 == 0))
        {
          c >>= 1;
          byte[] res = new byte[c];
          for (int i = 0; i < c; ++i)
            res[i] = byte.Parse(src.Substring(i << 1, 2), System.Globalization.NumberStyles.HexNumber);
          return res;
        }
      }
      return null;
    }
    public byte[] Bytes
    {
      get
      {
        return m_bytes;
      }
      set
      {
        m_strbytes = string.Empty;
        m_strbase64 = string.Empty;
        m_bytes = value;
        if (m_bytes != null)
        {
          int c = m_bytes.Length;
          if (c > 0)
          {
            m_strbase64 = Convert.ToBase64String(m_bytes);
            m_strbytes = BytesToString(m_bytes);
          }
        }
      }
    }
    public byte[] BytesCopy
    {
      get
      {
        return m_bytes;
      }
      set
      {
        m_strbytes = string.Empty;
        m_strbase64 = string.Empty;
        m_bytes = null;
        if (value != null)
        {
          int c = value.Length;
          if (c > 0)
          {
            m_bytes = new byte[c];
            Buffer.BlockCopy(value, 0, m_bytes, 0, c);
            m_strbase64 = Convert.ToBase64String(m_bytes);
            m_strbytes = BytesToString(m_bytes);
          }
        }
      }
    }
    public string StringBytes
    {
      get
      {
        return m_strbytes;
      }
      set
      {
        m_bytes = null;
        m_strbase64 = string.Empty;
        m_strbytes = (value != null) ? value : string.Empty;
        if (m_strbytes.Length > 0)
        {
          m_bytes = StringToBytes(m_strbytes);
          m_strbase64 = Convert.ToBase64String(m_bytes);
        }
      }
    }
    public string StringBase64
    {
      get
      {
        return m_strbase64;
      }
      set
      {
        m_bytes = null;
        m_strbytes = string.Empty;
        m_strbase64 = (value != null) ? value : string.Empty;
        if (m_strbase64.Length > 0)
        {
          m_bytes = Convert.FromBase64String(m_strbase64);
          m_strbytes = BytesToString(m_bytes);
        }
      }
    }
    public int Length
    {
      get
      {
        return (m_bytes != null) ? m_bytes.Length : 0;
      }
    }
    public override string ToString()
    {
      return (m_strbytes != null) ? m_strbytes : string.Empty;
    }
    public bool Equals(ByteString obj)
    {
      if (obj != null)
      {
        byte[] bs = obj.Bytes;
        if (bs == null)
        {
          if (m_bytes == null)
            return true;
        }
        else
          return (m_bytes != null) ? ByteArrayCompare.Compare(m_bytes, bs) : false;
      }
      return false;
    }
    public override bool Equals(object obj)
    {
      if ((obj != null) && (obj != this))
      {
        ByteString bstr = obj as ByteString;
        byte[] bs;
        if (bstr != null)
        {
          bs = bstr.Bytes;
          if ((bs == null) && (m_bytes == null))
            return true;
        }
        else
          bs = obj as byte[];
        if (bs != null)
          return (m_bytes != null) ? ByteArrayCompare.Compare(m_bytes, bs) : false;
      }
      return base.Equals(obj);
    }
    public override int GetHashCode()
    {
      int res = 0;
      if (m_bytes != null)
      {
        int c = m_bytes.Length;
        for (int i = 0; i < 4; ++i)
          if (c > i)
            res |= m_bytes[i] << (i << 3);
      }
      return res;
    }
    public ByteString(byte[] src)
    {
      Bytes = src;
    }
    public ByteString()
    {
    }
  }
}
