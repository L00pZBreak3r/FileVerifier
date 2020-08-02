using System;

namespace FileVerifierLib.ByteStringConversion
{
  public enum ByteStringFormat
  {
    Bytes,
    Base64
  };

  public class ByteStringAuto : ByteString
  {
    public ByteStringFormat StringFormat;
    public ByteStringAuto(byte[] src, ByteStringFormat format)
      : base(src)
    {
      StringFormat = format;
    }
    public ByteStringAuto(byte[] src)
      : base(src)
    {
    }
    public ByteStringAuto(ByteStringFormat format)
    {
      StringFormat = format;
    }
    public ByteStringAuto(string src)
    {
      StringAuto = src;
    }
    public ByteStringAuto()
    {
    }
    public string StringAuto
    {
      get
      {
        return (StringFormat == ByteStringFormat.Bytes) ? StringBytes : StringBase64;
      }
      set
      {
        try
        {
          StringBytes = value;
          StringFormat = ByteStringFormat.Bytes;
        }
        catch
        {
          StringBase64 = value;
          StringFormat = ByteStringFormat.Base64;
        }
      }
    }
    public override string ToString()
    {
      return StringAuto;
    }
  }
}
