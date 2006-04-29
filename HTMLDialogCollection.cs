using System;
using System.Collections;
using System.Diagnostics;

namespace WatiN
{
  public class HTMLDialogCollection : IEnumerable
  {
    private ArrayList htmlDialogs;

    public HTMLDialogCollection(Process ieProcess) 
    {
      this.htmlDialogs = new ArrayList();

      IntPtr hWnd = IntPtr.Zero;

      foreach (System.Diagnostics.ProcessThread t in ieProcess.Threads)
      {
        int threadId = t.Id;

        Win32.EnumThreadProc callbackProc = new Win32.EnumThreadProc(EnumChildForTridentDialogFrame);
        Win32.EnumThreadWindows(threadId, callbackProc, hWnd);
      }
    }

    private bool EnumChildForTridentDialogFrame(IntPtr hWnd, IntPtr lParam)
    {
      if (HTMLDialog.IsIETridenDlgFrame(hWnd))
      {
        HTMLDialog htmlDialog = new HTMLDialog(hWnd);
        this.htmlDialogs.Add(htmlDialog);
      }

      return true;
    }


    public int length { get { return htmlDialogs.Count; } }

    public HTMLDialog this[int index] { get { return (HTMLDialog)htmlDialogs[index]; } }

    public Enumerator GetEnumerator() 
    {
      return new Enumerator(htmlDialogs);
    }

    IEnumerator IEnumerable.GetEnumerator() 
    {
      return GetEnumerator();
    }

    public class Enumerator: IEnumerator 
    {
      ArrayList children;
      int index;
      public Enumerator(ArrayList children) 
      {
        this.children = children;
        Reset();
      }

      public void Reset() 
      {
        index = -1;
      }

      public bool MoveNext() 
      {
        ++index;
        return index < children.Count;
      }

      public HTMLDialog Current 
      {
        get 
        {
          return (HTMLDialog)children[index];
        }
      }

      object IEnumerator.Current { get { return Current; } }
    }
  }
}