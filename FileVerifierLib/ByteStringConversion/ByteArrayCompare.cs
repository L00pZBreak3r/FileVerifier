using System;
using System.Text;

namespace FileVerifierLib.ByteStringConversion
{
  public static class ByteArrayCompare
  {
    public static bool Compare(byte[] arr1, int index1, byte[] arr2, int index2, int count)
    {
      bool res = (arr1 != null) && (arr2 != null) && (count != 0);
      if (res)
      {
        if (index1 < 0)
          index1 = 0;
        if (index2 < 0)
          index2 = 0;
        int c1 = arr1.Length - index1;
        int c2 = arr2.Length - index2;
        res = (c1 > 0) && (c2 > 0);
        if (res)
        {
          int cmin = (c2 < c1) ? c2 : c1;
          if (count < 0)
          {
            count = cmin;
            res = c1 == c2;
          }
          else
            res = count <= cmin;
          for (int i = 0; (i < count) && res; ++i)
            res &= arr1[index1 + i] == arr2[index2 + i];
          return res;
        }
        return (count < 0) && (c1 == 0) && (c2 == 0);
      }
      return (count == 0) || (arr1 == null) && (arr2 == null);
    }
    public static bool Compare(byte[] arr1, int index1, byte[] arr2, int index2)
    {
      return Compare(arr1, index1, arr2, index2, -1);
    }
    public static bool Compare(byte[] arr1, byte[] arr2, int count)
    {
      return Compare(arr1, 0, arr2, 0, count);
    }
    public static bool Compare(byte[] arr1, byte[] arr2)
    {
      return Compare(arr1, 0, arr2, 0, -1);
    }
  }
}
