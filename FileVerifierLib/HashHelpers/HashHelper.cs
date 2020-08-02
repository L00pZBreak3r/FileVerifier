using System;
using System.IO;
using System.Security.Cryptography;
using FileVerifierLib.ByteStringConversion;

namespace FileVerifierLib.HashHelpers
{
  public enum HashAlgorithmType
  {
    Unknown,
    MD5,
    SHA1,
    SHA256,
    SHA384,
    SHA512
  };

  public class HashHelper : IDisposable
  {
    private static readonly int[] m_hsize = { 0, 16, 20, 32, 48, 64 };
    private ByteStringAuto m_HashValue;
    private HashAlgorithmType m_AlgorithmType;
    private HashAlgorithm m_algorithm;
    public HashHelper(ByteStringAuto src)
    {
      HashValue = src;
    }
    public HashHelper(byte[] src, ByteStringFormat format)
    {
      HashValue = new ByteStringAuto(src, format);
    }
    public HashHelper(byte[] src)
    {
      HashValue = new ByteStringAuto(src);
    }
    public HashHelper(string src)
    {
      HashValue = new ByteStringAuto(src);
    }
    public HashHelper(HashAlgorithmType algtype)
    {
      AlgorithmType = algtype;
    }
    public HashHelper()
    {
    }
    ~HashHelper()
    {
      Dispose(false);
    }
    public HashAlgorithm Algorithm
    {
      get
      {
        return m_algorithm;
      }
      set
      {
        m_AlgorithmType = HashAlgorithmType.Unknown;
        if (m_algorithm != null)
          m_algorithm.Clear();
        m_algorithm = value;
        if (m_algorithm != null)
          if (m_algorithm is MD5)
            m_AlgorithmType = HashAlgorithmType.MD5;
          else
            if (m_algorithm is SHA1)
              m_AlgorithmType = HashAlgorithmType.SHA1;
            else
              if (m_algorithm is SHA256)
                m_AlgorithmType = HashAlgorithmType.SHA256;
              else
                if (m_algorithm is SHA384)
                  m_AlgorithmType = HashAlgorithmType.SHA384;
                else
                  if (m_algorithm is SHA512)
                    m_AlgorithmType = HashAlgorithmType.SHA512;
      }
    }
    public HashAlgorithmType AlgorithmType
    {
      get
      {
        return m_AlgorithmType;
      }
      set
      {
        if (m_algorithm != null)
        {
          m_algorithm.Clear();
          m_algorithm = null;
        }
        m_AlgorithmType = value;
        switch (m_AlgorithmType)
        {
          case HashAlgorithmType.MD5:
            m_algorithm = new MD5CryptoServiceProvider();
            break;
          case HashAlgorithmType.SHA1:
            m_algorithm = new SHA1CryptoServiceProvider();
            break;
          case HashAlgorithmType.SHA256:
            m_algorithm = new SHA256CryptoServiceProvider();
            break;
          case HashAlgorithmType.SHA384:
            m_algorithm = new SHA384CryptoServiceProvider();
            break;
          case HashAlgorithmType.SHA512:
            m_algorithm = new SHA512CryptoServiceProvider();
            break;
        }
      }
    }
    public ByteStringAuto HashValue
    {
      get
      {
        return m_HashValue;
      }
      set
      {
        m_HashValue = value;
        AlgorithmType = HashAlgorithmType.Unknown;
        if (m_HashValue != null)
        {
          int c = m_HashValue.Length;
          int cc = m_hsize.Length;
          for (int i = 0; i < cc; ++i)
            if (c == m_hsize[i])
            {
              AlgorithmType = (HashAlgorithmType)i;
              break;
            }
        }
      }
    }
    private bool SetHashValue(byte[] bs, ByteStringFormat format)
    {
      if (m_HashValue != null)
      {
        m_HashValue.Bytes = bs;
        m_HashValue.StringFormat = format;
      }
      else
        m_HashValue = new ByteStringAuto(bs, format);
      return bs != null;
    }
    public bool ComputeHash(byte[] buffer, ByteStringFormat format)
    {
      bool res = (m_algorithm != null);
      if (res)
        res = SetHashValue(m_algorithm.ComputeHash(buffer), format);
      return res;
    }
    public bool ComputeHash(byte[] buffer)
    {
      return ComputeHash(buffer, ByteStringFormat.Bytes);
    }
    public bool ComputeHash(byte[] buffer, int offset, int count, ByteStringFormat format)
    {
      bool res = (m_algorithm != null);
      if (res)
        res = SetHashValue(m_algorithm.ComputeHash(buffer, offset, count), format);
      return res;
    }
    public bool ComputeHash(byte[] buffer, int offset, int count)
    {
      return ComputeHash(buffer, offset, count, ByteStringFormat.Bytes);
    }
    public bool ComputeHash(Stream inputStream, ByteStringFormat format)
    {
      bool res = (m_algorithm != null);
      if (res)
        res = SetHashValue(m_algorithm.ComputeHash(inputStream), format);
      return res;
    }
    public bool ComputeHash(Stream inputStream)
    {
      return ComputeHash(inputStream, ByteStringFormat.Bytes);
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (disposing && (m_algorithm != null))
      {
        m_algorithm.Clear();
        m_algorithm = null;
        m_AlgorithmType = HashAlgorithmType.Unknown;
      }
    }
  }
}
