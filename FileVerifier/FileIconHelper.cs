using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace FileVerifier
{
  #region Public SHGetFileInfo structures
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  internal struct SHFILEINFO
  {
    private const int MAX_PATH = 260;
    public IntPtr hIcon;
    public int iIcon;
    public int dwAttributes;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
    public string szDisplayName;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
    public string szTypeName;
  }

  [Flags]
  internal enum FileInfoFlags : uint
  {
    SHGFI_ICON = 0x000000100,     // get icon
    SHGFI_DISPLAYNAME = 0x000000200,     // get display name
    SHGFI_TYPENAME = 0x000000400,     // get type name
    SHGFI_ATTRIBUTES = 0x000000800,     // get attributes
    SHGFI_ICONLOCATION = 0x000001000,     // get icon location
    SHGFI_EXETYPE = 0x000002000,     // return exe type
    SHGFI_SYSICONINDEX = 0x000004000,     // get system icon index
    SHGFI_LINKOVERLAY = 0x000008000,     // put a link overlay on icon
    SHGFI_SELECTED = 0x000010000,     // show icon in selected state
    SHGFI_ATTR_SPECIFIED = 0x000020000,     // get only specified attributes
    SHGFI_LARGEICON = 0x000000000,     // get large icon
    SHGFI_SMALLICON = 0x000000001,     // get small icon
    SHGFI_OPENICON = 0x000000002,     // get open icon
    SHGFI_SHELLICONSIZE = 0x000000004,     // get shell size icon
    SHGFI_PIDL = 0x000000008,     // pszPath is a pidl
    SHGFI_USEFILEATTRIBUTES = 0x000000010,     // use passed dwFileAttribute
    SHGFI_ADDOVERLAYS = 0x000000020,     // apply the appropriate overlays
    SHGFI_OVERLAYINDEX = 0x000000040, // Get the index of the overlay in the upper 8 bits of the iIcon
  };
  internal enum ImageListSize : int
  {
    SHIL_LARGE,
    SHIL_SMALL,
    SHIL_EXTRALARGE,
    SHIL_SYSSMALL,
    SHIL_JUMBO,
    SHIL_LAST = SHIL_JUMBO
  }
  #endregion
  internal static class NativeMethods
  {
    [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
    internal static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, FileInfoFlags uFlags);
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool DestroyIcon(IntPtr handle);
    [DllImport("shell32.dll")]
    [return: MarshalAs(UnmanagedType.I4)]
    internal static extern int SHGetImageList(ImageListSize iImageList, ref Guid riid, ref IntPtr handle);
    [DllImport("user32")]
    internal static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
  }

  static class FileIconHelper
  {
    private static class SHGetFileInfoWrapper
    {
      private const string IIMAGELIST_GUID_STRING = "46EB5926-582E-4017-9FDF-E8998DAA0950";
      /*
      #region Private ImageList structures
      [StructLayout(LayoutKind.Sequential)]
      private struct POINT
      {
        int x;
        int y;
      }
      [StructLayout(LayoutKind.Sequential)]
      private struct RECT
      {
        int left;
        int top;
        int right;
        int bottom;
      }
      [StructLayout(LayoutKind.Sequential)]
      private struct IMAGELISTDRAWPARAMS
      {
        public int cbSize;
        public IntPtr himl;
        public int i;
        public IntPtr hdcDst;
        public int x;
        public int y;
        public int cx;
        public int cy;
        public int xBitmap;        // x offest from the upperleft of bitmap
        public int yBitmap;        // y offset from the upperleft of bitmap
        public int rgbBk;
        public int rgbFg;
        public int fStyle;
        public int dwRop;
        public int fState;
        public int Frame;
        public int crEffect;
      }

      [StructLayout(LayoutKind.Sequential)]
      private struct IMAGEINFO
      {
        public IntPtr hbmImage;
        public IntPtr hbmMask;
        public int Unused1;
        public int Unused2;
        public RECT rcImage;
      }
      #endregion
      #region Private ImageList COM Interop (XP)
      [ComImportAttribute(), GuidAttribute(IIMAGELIST_GUID_STRING), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
      private interface IImageList
      {
        [PreserveSig]
        int Add(
              IntPtr hbmImage,
              IntPtr hbmMask,
              ref int pi);

        [PreserveSig]
        int ReplaceIcon(
              int i,
              IntPtr hicon,
              ref int pi);

        [PreserveSig]
        int SetOverlayImage(
              int iImage,
              int iOverlay);

        [PreserveSig]
        int Replace(
              int i,
              IntPtr hbmImage,
              IntPtr hbmMask);

        [PreserveSig]
        int AddMasked(
              IntPtr hbmImage,
              int crMask,
              ref int pi);

        [PreserveSig]
        int Draw(
              ref IMAGELISTDRAWPARAMS pimldp);

        [PreserveSig]
        int Remove(
          int i);

        [PreserveSig]
        int GetIcon(
              int i,
              int flags,
              ref IntPtr picon);

        [PreserveSig]
        int GetImageInfo(
              int i,
              ref IMAGEINFO pImageInfo);

        [PreserveSig]
        int Copy(
              int iDst,
              IImageList punkSrc,
              int iSrc,
              int uFlags);

        [PreserveSig]
        int Merge(
              int i1,
              IImageList punk2,
              int i2,
              int dx,
              int dy,
              ref Guid riid,
              ref IntPtr ppv);

        [PreserveSig]
        int Clone(
              ref Guid riid,
              ref IntPtr ppv);

        [PreserveSig]
        int GetImageRect(
              int i,
              ref RECT prc);

        [PreserveSig]
        int GetIconSize(
              ref int cx,
              ref int cy);

        [PreserveSig]
        int SetIconSize(
              int cx,
              int cy);

        [PreserveSig]
        int GetImageCount(
          ref int pi);

        [PreserveSig]
        int SetImageCount(
              int uNewCount);

        [PreserveSig]
        int SetBkColor(
              int clrBk,
              ref int pclr);

        [PreserveSig]
        int GetBkColor(
              ref int pclr);

        [PreserveSig]
        int BeginDrag(
              int iTrack,
              int dxHotspot,
              int dyHotspot);

        [PreserveSig]
        int EndDrag();

        [PreserveSig]
        int DragEnter(
              IntPtr hwndLock,
              int x,
              int y);

        [PreserveSig]
        int DragLeave(
              IntPtr hwndLock);

        [PreserveSig]
        int DragMove(
              int x,
              int y);

        [PreserveSig]
        int SetDragCursorImage(
              ref IImageList punk,
              int iDrag,
              int dxHotspot,
              int dyHotspot);

        [PreserveSig]
        int DragShowNolock(
              int fShow);

        [PreserveSig]
        int GetDragImage(
              ref POINT ppt,
              ref POINT pptHotspot,
              ref Guid riid,
              ref IntPtr ppv);

        [PreserveSig]
        int GetItemFlags(
              int i,
              ref int dwFlags);

        [PreserveSig]
        int GetOverlayImage(
              int iOverlay,
              ref int piIndex);
      };
      #endregion
      */

      private const int LVM_FIRST = 0x1000;
      private const int LVM_SETIMAGELIST = (LVM_FIRST + 3);

      private const int LVSIL_NORMAL = 0;
      private const int LVSIL_SMALL = 1;
      private const int LVSIL_STATE = 2;

      /*private const int TV_FIRST = 0x1100;
      private const int TVM_SETIMAGELIST = (TV_FIRST + 9);

      private const int TVSIL_NORMAL = 0;
      private const int TVSIL_STATE = 2;*/
      
      /*public static int ReleaseImageList(object imageList)
      {
        if (imageList != null)
          return Marshal.ReleaseComObject(imageList);
        return -1;
      }*/
      /// <summary>
      /// Associates a SysImageList with a ListView control
      /// </summary>
      /// <param name="listView">ListView control to associate ImageList with</param>
      /// <param name="sysImageList">System Image List to associate</param>
      /// <param name="forStateImages">Whether to add ImageList as StateImageList</param>
      public static void SetListViewSystemImageList(ListView listView, ImageListSize imageListSize, bool forStateImages)
      {
        //object res = null;
        if (listView != null)
        {
          // Get the System IImageList object from the Shell:
          Guid iidImageList = new Guid(IIMAGELIST_GUID_STRING);
          //IImageList iImageList = null;
          IntPtr hIml = IntPtr.Zero;
          //int ret = SHGetImageList(imageListSize, ref iidImageList, ref iImageList);
          int ret = NativeMethods.SHGetImageList(imageListSize, ref iidImageList, ref hIml);
          if (ret == 0)
          {
            //IntPtr hIml = IntPtr.Zero;
            // the image list handle is the IUnknown pointer, but 
            // using Marshal.GetIUnknownForObject doesn't return
            // the right value.  It really doesn't hurt to make
            // a second call to get the handle:
            //ret = SHGetImageListHandle(imageListSize, ref iidImageList, ref hIml);
            //if (ret == 0)
            //{
            int wParam = LVSIL_NORMAL;
            if ((imageListSize == ImageListSize.SHIL_SMALL) || (imageListSize == ImageListSize.SHIL_SYSSMALL))
              wParam = LVSIL_SMALL;
            if (forStateImages)
              wParam = LVSIL_STATE;
            NativeMethods.SendMessage(
              listView.Handle,
              LVM_SETIMAGELIST,
              (IntPtr)wParam,
              hIml);
            //res = hIml;
            //}
            //else
            //  ReleaseImageList(iImageList);
          }
        }
        //return res;
      }
      /*public static void SetListViewSystemImageList(ListView listView, ImageListSize imageListSize)
      {
        SetListViewSystemImageList(listView, imageListSize, false);
      }
      public static void SetListViewSystemImageList(ListView listView)
      {
        SetListViewSystemImageList(listView, ImageListSize.SHIL_SMALL, false);
      }*/
    }
    public static bool AddFileIcon(ImageList imageList, string filename, bool useLargeIcon)
    {
      bool res = false;
      if ((imageList != null) && (File.Exists(filename) || Directory.Exists(filename)))
      {
        SHFILEINFO shinfo = new SHFILEINFO();
        IntPtr hImg = NativeMethods.SHGetFileInfo(filename, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), FileInfoFlags.SHGFI_ICON | ((useLargeIcon) ? FileInfoFlags.SHGFI_LARGEICON : FileInfoFlags.SHGFI_SMALLICON));
        //if (hImg != null)
        //{
          //The icon is returned in the hIcon member of the shinfo struct
          Icon myIcon = Icon.FromHandle(shinfo.hIcon);
          imageList.Images.Add(myIcon);
          NativeMethods.DestroyIcon(shinfo.hIcon);
          res = true;
        //}
      }
      return res;
    }
    public static bool AddFileIcon(ImageList imageList, string filename)
    {
      return AddFileIcon(imageList, filename, false);
    }
    public static int GetFileIconSystemIndex(string filename, bool useLargeIcon)
    {
      int res = -1;
      if (File.Exists(filename) || Directory.Exists(filename))
      {
        SHFILEINFO shinfo = new SHFILEINFO();
        IntPtr hImg = NativeMethods.SHGetFileInfo(filename, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), FileInfoFlags.SHGFI_SYSICONINDEX | FileInfoFlags.SHGFI_ICON | ((useLargeIcon) ? FileInfoFlags.SHGFI_LARGEICON : FileInfoFlags.SHGFI_SMALLICON));
        //if (hImg != null)
        //{
          NativeMethods.DestroyIcon(shinfo.hIcon);
          res = shinfo.iIcon;
        //}
      }
      return res;
    }
    public static int GetFileIconSystemIndex(string filename)
    {
      return GetFileIconSystemIndex(filename, false);
    }
    public static void SetListViewSystemImageList(ListView listView, bool largeImageSize, bool forStateImages)
    {
      SHGetFileInfoWrapper.SetListViewSystemImageList(listView, (largeImageSize) ? ImageListSize.SHIL_LARGE : ImageListSize.SHIL_SMALL, forStateImages);
    }
    public static void SetListViewSystemImageList(ListView listView, bool largeImageSize)
    {
      SetListViewSystemImageList(listView, largeImageSize, false);
    }
    public static void SetListViewSystemImageList(ListView listView)
    {
      SetListViewSystemImageList(listView, false, false);
    }
  }
}
