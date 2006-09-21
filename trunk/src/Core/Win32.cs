#region WatiN Copyright (C) 2006 Jeroen van Menen

// WatiN (Web Application Testing In dotNet)
// Copyright (C) 2006 Jeroen van Menen
//
// This library is free software; you can redistribute it and/or modify it under the terms of the GNU 
// Lesser General Public License as published by the Free Software Foundation; either version 2.1 of 
// the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without 
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU 
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along with this library; 
// if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 
// 02111-1307 USA 

#endregion Copyright

using System;
using System.Runtime.InteropServices;
using System.Text;

using mshtml;
using SHDocVw;
using WatiN.Core.Interfaces;

namespace WatiN.Core
{
  /// <summary>
  /// Class that contains native win32 API support.
  /// </summary>
  public sealed class NativeMethods
  {
    #region Constants

    internal const int WM_SYSCOMMAND = 0x0112;
    internal const int WM_CLOSE = 0x0010;
    internal const int SC_CLOSE = 0xF060;

    internal const int KEYEVENTF_EXTENDEDKEY = 0x1;
    internal const int KEYEVENTF_KEYUP = 0x2;
    internal const int KEYEVENTF_TAB = 0x09;

    internal const Int32 SMTO_ABORTIFHUNG = 2;

    #endregion Constants

    #region Structs

    [StructLayout(LayoutKind.Sequential)]
    internal struct WINDOWPLACEMENT
    {
      public int length;
      public int flags;
      public int showCmd;
      public POINT ptMinPosition;
      public POINT ptMaxPosition;
      public RECT rcNormalPosition;
    }

    [StructLayout( LayoutKind.Sequential )]
    internal struct POINT 
    {
      public int X;
      public int Y;
    }

    [StructLayout( LayoutKind.Sequential )]
    internal struct RECT 
    {
      public int Left;
      public int Top;
      public int Right;
      public int Bottom;
    }

    #endregion Structs

    internal delegate bool EnumThreadProc(IntPtr hwnd, IntPtr lParam);
    internal delegate bool EnumChildProc(IntPtr hWnd, ref IntPtr lParam);

    #region Enums

    /// <summary>
    /// Enumeration of the different ways of showing a window using 
    /// ShowWindow
    /// </summary>
    public enum WindowShowStyle : int
    {
      /// <summary>Hides the window and activates another window.</summary>
      /// <remarks>See SW_HIDE</remarks>
      Hide = 0,
      /// <summary>Activates and displays a window. If the window is minimized 
      /// or maximized, the system restores it to its original size and 
      /// position. An application should specify this flag when displaying 
      /// the window for the first time.</summary>
      /// <remarks>See SW_SHOWNORMAL</remarks>
      ShowNormal = 1,
      /// <summary>Activates the window and displays it as a minimized window.</summary>
      /// <remarks>See SW_SHOWMINIMIZED</remarks>
      ShowMinimized = 2,
      /// <summary>Activates the window and displays it as a maximized window.</summary>
      /// <remarks>See SW_SHOWMAXIMIZED</remarks>
      ShowMaximized = 3,
      /// <summary>Maximizes the specified window.</summary>
      /// <remarks>See SW_MAXIMIZE</remarks>
      Maximize = 3,
      /// <summary>Displays a window in its most recent size and position. 
      /// This value is similar to "ShowNormal", except the window is not 
      /// actived.</summary>
      /// <remarks>See SW_SHOWNOACTIVATE</remarks>
      ShowNormalNoActivate = 4,
      /// <summary>Activates the window and displays it in its current size 
      /// and position.</summary>
      /// <remarks>See SW_SHOW</remarks>
      Show = 5,
      /// <summary>Minimizes the specified window and activates the next 
      /// top-level window in the Z order.</summary>
      /// <remarks>See SW_MINIMIZE</remarks>
      Minimize = 6,
      /// <summary>Displays the window as a minimized window. This value is 
      /// similar to "ShowMinimized", except the window is not activated.</summary>
      /// <remarks>See SW_SHOWMINNOACTIVE</remarks>
      ShowMinNoActivate = 7,
      /// <summary>Displays the window in its current size and position. This 
      /// value is similar to "Show", except the window is not activated.</summary>
      /// <remarks>See SW_SHOWNA</remarks>
      ShowNoActivate = 8,
      /// <summary>Activates and displays the window. If the window is 
      /// minimized or maximized, the system restores it to its original size 
      /// and position. An application should specify this flag when restoring 
      /// a minimized window.</summary>
      /// <remarks>See SW_RESTORE</remarks>
      Restore = 9,
      /// <summary>Sets the show state based on the SW_ value specified in the 
      /// STARTUPINFO structure passed to the CreateProcess function by the 
      /// program that started the application.</summary>
      /// <remarks>See SW_SHOWDEFAULT</remarks>
      ShowDefault = 10,
      /// <summary>Windows 2000/XP: Minimizes a window, even if the thread 
      /// that owns the window is hung. This flag should only be used when 
      /// minimizing windows from a different thread.</summary>
      /// <remarks>See SW_FORCEMINIMIZE</remarks>
      ForceMinimized = 11
    }

    [Flags()]
    public enum tagOLECONTF
    {
      OLECONTF_EMBEDDINGS = 1,
      OLECONTF_LINKS = 2,
      OLECONTF_OTHERS = 4,
      OLECONTF_ONLYUSER = 8,
      OLECONTF_ONLYIFRUNNING = 16,
    }

    #endregion Enums
    
    /// <summary>
    /// Prevent creating an instance of this class (contains only static members)
    /// </summary>
    private NativeMethods(){}

    #region DllImport User32

    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    internal static extern bool EnumThreadWindows(int threadId, EnumThreadProc pfnEnum, IntPtr lParam);

    [DllImport("user32", EntryPoint = "GetClassNameA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    internal static extern int GetClassName( IntPtr handleToWindow, StringBuilder className, int maxClassNameLength );
    
    [DllImport("user32.dll", SetLastError=true)]
    internal static extern IntPtr SetActiveWindow(IntPtr hWnd);
    
    [DllImport("user32.dll", SetLastError=true, CharSet=CharSet.Auto)]
    internal static extern IntPtr GetDlgItem( IntPtr handleToWindow, int ControlId );

    /// <summary>
    /// The GetForegroundWindow function returns a handle to the foreground window.
    /// </summary>
    [DllImport("user32.dll", SetLastError=true)]
    internal static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll", SetLastError=true, CharSet=CharSet.Auto)]
    internal static extern int GetWindowText( IntPtr handleToWindow, StringBuilder windowText, int maxTextLength );

    [DllImport("user32.dll", SetLastError=true, CharSet=CharSet.Auto)]
    internal static extern int GetWindowTextLength(IntPtr hWnd);

    [DllImport("user32.dll", SetLastError=true, CharSet=CharSet.Auto)]
    internal static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

    [DllImport("user32.dll", CharSet=CharSet.Auto)]
    internal static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

    [DllImport("user32.dll", SetLastError=true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    internal static extern Int32 EnumChildWindows(IntPtr hWndParent, EnumChildProc lpEnumFunc, ref IntPtr lParam);
    
    [DllImport("user32", EntryPoint = "RegisterWindowMessageA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    internal static extern Int32 RegisterWindowMessage(string lpString);
    
    [DllImport("user32", EntryPoint = "SendMessageTimeoutA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    internal static extern Int32 SendMessageTimeout(IntPtr hWnd, Int32 msg, Int32 wParam, Int32 lParam, Int32 fuFlags, Int32 uTimeout, ref Int32 lpdwResult);
    
    [DllImport("user32.dll")]
    internal static extern bool AttachThreadInput(int idAttach, int idAttachTo,bool fAttach);

    [DllImport("user32.dll")]
    internal static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

    /// <summary>Shows a Window</summary>
    /// <remarks>
    /// <para>To perform certain special effects when showing or hiding a 
    /// window, use AnimateWindow.</para>
    ///<para>The first time an application calls ShowWindow, it should use 
    ///the WinMain function's nCmdShow parameter as its nCmdShow parameter. 
    ///Subsequent calls to ShowWindow must use one of the values in the 
    ///given list, instead of the one specified by the WinMain function's 
    ///nCmdShow parameter.</para>
    ///<para>As noted in the discussion of the nCmdShow parameter, the 
    ///nCmdShow value is ignored in the first call to ShowWindow if the 
    ///program that launched the application specifies startup information 
    ///in the structure. In this case, ShowWindow uses the information 
    ///specified in the STARTUPINFO structure to show the window. On 
    ///subsequent calls, the application must call ShowWindow with nCmdShow 
    ///set to SW_SHOWDEFAULT to use the startup information provided by the 
    ///program that launched the application. This behavior is designed for 
    ///the following situations: </para>
    ///<list type="">
    ///    <item>Applications create their main window by calling CreateWindow 
    ///    with the WS_VISIBLE flag set. </item>
    ///    <item>Applications create their main window by calling CreateWindow 
    ///    with the WS_VISIBLE flag cleared, and later call ShowWindow with the 
    ///    SW_SHOW flag set to make it visible.</item>
    ///</list></remarks>
    /// <param name="hWnd">Handle to the window.</param>
    /// <param name="nCmdShow">Specifies how the window is to be shown. 
    /// This parameter is ignored the first time an application calls 
    /// ShowWindow, if the program that launched the application provides a 
    /// STARTUPINFO structure. Otherwise, the first time ShowWindow is called, 
    /// the value should be the value obtained by the WinMain function in its 
    /// nCmdShow parameter. In subsequent calls, this parameter can be one of 
    /// the WindowShowStyle members.</param>
    /// <returns>
    /// If the window was previously visible, the return value is nonzero. 
    /// If the window was previously hidden, the return value is zero.
    /// </returns>
    [DllImport("user32.dll")]
    internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    #endregion DllImport User32

    [DllImport("oleacc", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
    internal static extern Int32 ObjectFromLresult(Int32 lResult, ref Guid riid, Int32 wParam, ref IHTMLDocument2 ppvObject);
    
    [DllImport("kernel32")]
    internal static extern int GetCurrentThreadId();
    
    #region ComImport Interfaces

    [ComImport, Guid("00000100-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumUnknown
    {
      [PreserveSig]
      int Next(
        [In, MarshalAs(UnmanagedType.U4)] int celt,
        [Out, MarshalAs(UnmanagedType.IUnknown)] out object rgelt,
        [Out, MarshalAs(UnmanagedType.U4)] out int pceltFetched
        );

      [PreserveSig]
      int Skip([In, MarshalAs(UnmanagedType.U4)] int celt);

      void Reset();

      void Clone(out IEnumUnknown ppenum);
    }

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("0000011B-0000-0000-C000-000000000046")]
    public interface IOleContainer
    {
      [PreserveSig]
      int ParseDisplayName(
        [In, MarshalAs(UnmanagedType.Interface)] object pbc,
        [In, MarshalAs(UnmanagedType.BStr)] string pszDisplayName,
        [Out, MarshalAs(UnmanagedType.LPArray)] int[] pchEaten,
        [Out, MarshalAs(UnmanagedType.LPArray)] object[] ppmkOut
        );

      [PreserveSig]
      int EnumObjects(
        [In, MarshalAs(UnmanagedType.U4)] tagOLECONTF grfFlags,
        out IEnumUnknown ppenum
        );

      [PreserveSig]
      int LockContainer(bool fLock);
    }

    #endregion ComImport Interfaces

    #region Methods

    internal static void EnumIWebBrowser2Interfaces(IProcessIWebBrowser2 processer)
    {
      IEnumUnknown eu;
      IOleContainer oc = processer.HTMLDocument() as IOleContainer;
      int hr = oc.EnumObjects(tagOLECONTF.OLECONTF_EMBEDDINGS, out eu);
      Marshal.ThrowExceptionForHR(hr);

      try
      {
        object pUnk;
        int fetched;
        const int MAX_FETCH_COUNT = 1;

        // get the first embedded object
        // pUnk alloc
        hr = eu.Next(MAX_FETCH_COUNT, out pUnk, out fetched);
        Marshal.ThrowExceptionForHR(hr);

        while (hr == 0)
        {
          // Query Interface pUnk for the IWebBrowser2 interface
          IWebBrowser2 brow = pUnk as IWebBrowser2;

          if (brow != null)
          {
            processer.Process(brow);
                   
            if (!processer.Continue())
            {
              break;
            }
          }

          // pUnk free
          Marshal.ReleaseComObject(brow);

          // get the next embedded object
          // pUnk alloc
          hr = eu.Next(MAX_FETCH_COUNT, out pUnk, out fetched);
          Marshal.ThrowExceptionForHR(hr);
        }
      }
      finally
      {
        // eu free
        Marshal.ReleaseComObject(eu);
      }
    }

    /// <summary>
    /// This method incapsulates all the details of getting
    /// the full length text in a StringBuffer and returns
    /// the StringBuffer contents as a string.
    /// </summary>
    /// <param name="hwnd">The handle to the window</param>
    /// <returns>Text of the window</returns>
    public static string GetWindowText(IntPtr hwnd)
    {
      int length = GetWindowTextLength(hwnd) + 1;
      StringBuilder buffer = new StringBuilder(length);
      GetWindowText(hwnd, buffer, length);
			
      return buffer.ToString();
    }

    /// <summary>
    /// This method incapsulates all the details of getting
    /// the full length classname in a StringBuffer and returns
    /// the StringBuffer contents as a string.
    /// </summary>
    /// <param name="hwnd">The handle to the window</param>
    /// <returns>Text of the window</returns>
    public static string GetClassName(IntPtr hwnd)
    {
      StringBuilder className = new StringBuilder(255);
      GetClassName(hwnd, className, className.Capacity);
      
      return className.ToString();
    }
    
    #endregion Methods
  }
}

namespace WatiN.Core.Interfaces
{
  internal interface IProcessIWebBrowser2
  {
    HTMLDocument HTMLDocument();
    void Process(IWebBrowser2 webBrowser2);
    bool Continue();
  }
}