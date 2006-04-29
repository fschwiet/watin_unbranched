using System;
using System.Text;
using System.Threading;

using WatiN.Exceptions;

namespace WatiN
{
  public class PopupWatcher
  {
    public delegate int Callback(IntPtr hwnd, int lParam);

    private int iePid;
    private bool keepRunning;

    private System.Collections.Queue alertQueue;

    public PopupWatcher(int iePid)
    {
      this.iePid = iePid;
      this.keepRunning = true;
      this.alertQueue = new System.Collections.Queue();
    }

    public int alertCount()
    {
      return alertQueue.Count;
    }

    public string popAlert()
    {
      if (alertQueue.Count == 0)
      {
        throw new MissingAlertException();
      }

      return (string) alertQueue.Dequeue();
    }

    public string[] alerts
    {
      get
      {
        string[] result = new string[alertQueue.Count];
        Array.Copy(alertQueue.ToArray(), result, alertQueue.Count);
        return result;
      }
    }

    public void flushAlerts()
    {
      alertQueue.Clear();
    }

    public void run()
    {
      while (keepRunning)
      {
        Thread.Sleep(1000);

        System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(iePid);

        foreach (System.Diagnostics.ProcessThread t in p.Threads)
        {
          int threadId = t.Id;

          Win32.EnumThreadProc callbackProc = new Win32.EnumThreadProc(MyEnumThreadWindowsProc);
          Win32.EnumThreadWindows(threadId, callbackProc, IntPtr.Zero);
        }
      }
    }

    public void Stop()
    {
      keepRunning = false;
    }

    private bool MyEnumThreadWindowsProc(IntPtr hwnd, IntPtr lParam)
    {
      if (IsDialog(hwnd))
      {        
        IntPtr handleToDialogText = Win32.GetDlgItem(hwnd, 0xFFFF);
        string alertMessage = GetText(handleToDialogText);
        alertQueue.Enqueue(alertMessage);

        Win32.SendMessage(hwnd, Win32.WM_CLOSE, 0, 0);
      }

      return true;
    }

    private bool IsDialog( IntPtr wParam )
    {
      StringBuilder className = new StringBuilder(255);
      Win32.GetClassName(wParam, className, className.Capacity);

      return (className.ToString() == "#32770");
    }

    private static string GetText( IntPtr handle )
    {
      int length = Win32.GetWindowTextLength(handle) + 1;
      StringBuilder buffer = new StringBuilder(length);
      Win32.GetWindowText(handle, buffer, length);
			
      return buffer.ToString();
    }
  }
}