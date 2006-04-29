using System;
using System.Runtime.InteropServices;
using System.Text;

using mshtml;

namespace WatiN
{
	/// <summary>
	/// Summary description for HTMLDialog.
	/// </summary>
	public class HTMLDialog : DomContainer
	{
    private IntPtr hWnd = IntPtr.Zero;

    public static bool IsIETridenDlgFrame(IntPtr hWnd)
    {
      return CompareClassNames(hWnd, "Internet Explorer_TridentDlgFrame");
    }

		public HTMLDialog(IntPtr hWnd)
		{
      this.hWnd = hWnd;
    }

    public void Close()
    {
      base.Dispose();
      Win32.SendMessage(this.hWnd, Win32.WM_CLOSE, 0, 0);
    }

    public bool HasFocus()
    {
      // TODO: Find Win32 API to determine if window has the focus
      return true;
    }

    public override IHTMLDocument2 OnGetHTMLDocument()
    {
      return IEDOMFromhWnd(this.hWnd);
    }

    private IHTMLDocument2 IEDOMFromhWnd(IntPtr hWnd)
    {
      System.Guid IID_IHTMLDocument2 = new System.Guid("626FC520-A41E-11CF-A731-00A0C9082637");

      Int32 lRes = 0;
      Int32 lMsg = 0;
      Int32 hr = 0;

      if (IsIETridenDlgFrame(hWnd))
      {
        if (!IsIEServerWindow(hWnd))
        {
          // Get 1st child IE server window
          Win32.EnumChildProc childProc = new Win32.EnumChildProc(EnumChildForServerWindow);
          Win32.EnumChildWindows(hWnd, childProc, ref hWnd);
        }

        if (IsIEServerWindow(hWnd))
        {
          // Register the message
          lMsg = Win32.RegisterWindowMessage("WM_HTML_GETOBJECT");
          // Get the object
          Win32.SendMessageTimeout(hWnd, lMsg, 0, 0, Win32.SMTO_ABORTIFHUNG, 1000, ref lRes);
          if (lRes != 0)
          {
            // Get the object from lRes
            IHTMLDocument2 ieDOMFromhWnd = null;
            hr = Win32.ObjectFromLresult(lRes, ref IID_IHTMLDocument2, 0, ref ieDOMFromhWnd);
            if (hr != 0)
            {
              throw new COMException("ObjectFromLresult has thown an exception", hr);
            }
            return ieDOMFromhWnd;
          }
        }
      }
      return null;
    }

    private bool EnumChildForServerWindow(IntPtr hWnd, ref IntPtr lParam)
    {
      if (IsIEServerWindow(hWnd))
      {
        lParam = hWnd;
        return false;
      }
      else
      {
        return true;
      }
    }

    private bool IsIEServerWindow(IntPtr hWnd)
    {
      return CompareClassNames(hWnd, "Internet Explorer_Server");
    }

    private static bool CompareClassNames(IntPtr hWnd, string expectedClassName)
    {
      StringBuilder className = new StringBuilder(100);

      // Get the window class name
      Int32 lRes = Win32.GetClassName(hWnd, className, className.MaxCapacity);
      if (lRes == 0)
      {
        return false;
      }

      return className.ToString().Equals(expectedClassName);
    }
	}
}
