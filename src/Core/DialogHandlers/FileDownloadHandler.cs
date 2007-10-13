namespace WatiN.Core.DialogHandlers
{
  using System;
  using System.Threading;
  using WatiN.Core.Exceptions;
  using WatiN.Core.Logging;

  public class FileDownloadHandler : BaseDialogHandler
  {
    private Window downloadProgressDialog;
    private bool hasHandledFileDownloadDialog = false;
    private FileDownloadOptionEnum _optionEnum;
    private string saveAsFilename = String.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileDownloadHandler"/> class.
    /// Use this constructor if you want to Run, Open or Cancel the download.
    /// </summary>
    /// <param name="option">The option to choose on the File Download dialog.</param>
    public FileDownloadHandler(FileDownloadOptionEnum option)
    {
      if(option == FileDownloadOptionEnum.Save)
      {
        throw new ArgumentException("When using FileDownloadOptionEnum.Save call the constructor which accepts a filename as an argument");
      }

      this._optionEnum = option;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FileDownloadHandler"/> class.
    /// Use this contructor if you want to download and save a file.
    /// </summary>
    /// <param name="filename">The filename.</param>
    public FileDownloadHandler(string filename)
    {
      if(UtilityClass.IsNullOrEmpty(filename))
      {
        throw new ArgumentNullException("filename", "Not a valid value");
      }

      _optionEnum = FileDownloadOptionEnum.Save;
      saveAsFilename = filename;
    }

    /// <summary>
    /// Gets a value indicating whether this instance has handled a file download dialog.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance has handled a file download dialog; otherwise, <c>false</c>.
    /// </value>
    public bool HasHandledFileDownloadDialog
    {
      get { return hasHandledFileDownloadDialog; }
    }

    /// <summary>
    /// Gets the full save as filename used when the downloaded file will be saved to disk.
    /// </summary>
    /// <value>The save as filename.</value>
    public string SaveAsFilename
    {
      get { return saveAsFilename; }
    }

    /// <summary>
    /// Handles the dialogs to download (and save) a file
    /// Mainly used internally by WatiN.
    /// </summary>
    /// <param name="window">The window.</param>
    /// <returns></returns>
    public override bool HandleDialog(Window window)
    {
//      Logger.LogAction(">> HandleDialog");
//      Logger.LogAction("hwnd = " + window.Hwnd.ToString());
//      Logger.LogAction("style = " + window.Style.ToString());
//      Logger.LogAction("stylehex = " + window.StyleInHex);
//      Logger.LogAction("<< HandleDialog");

      // This if handles the File download dialog
      if (!HasHandledFileDownloadDialog && IsFileDownloadDialog(window))
      {

        window.ToFront();
        window.SetActivate();

        DownloadProgressDialog = new Window(window.ParentHwnd);
			  
        int buttonid = 0;

        switch (_optionEnum)
        {
          case FileDownloadOptionEnum.Run: buttonid = 4426; break;
          case FileDownloadOptionEnum.Open: buttonid = 4426; break;
          case FileDownloadOptionEnum.Save: buttonid = 4427; break;
          case FileDownloadOptionEnum.Cancel: buttonid = 2; break;
        }

        WinButton btn = new WinButton(buttonid, window.Hwnd);
        btn.Click();

        hasHandledFileDownloadDialog = !Exists(window);

        if (HasHandledFileDownloadDialog)
        {
          Logger.LogAction("Download started at " + DateTime.Now.ToLongTimeString());
          Logger.LogAction("Clicked " + _optionEnum.ToString());
        }

        return true;
      }
		  
      // This if handles the download progress dialog
      if (IsDownloadProgressDialog(window))
      {
        DownloadProgressDialog = window;

        return true;
      }
		  
      // This if handles the File save as dialog
      if (IsFileSaveDialog(window))
      {
        Logger.LogAction("Saving Download file as " + saveAsFilename);
			  
        DownloadProgressDialog = new Window(window.ParentHwnd);

        HandleFileSaveDialog(window);
        
        return true;
      }

      // Window is not a dialog this handler can handle.
      return false;
    }

    /// <summary>
    /// Determines whether the specified window is a file download dialog by
    /// checking the style property of the window. It should match
    /// <c>window.StyleInHex == "94C80AC4"</c>
    /// </summary>
    /// <param name="window">The window.</param>
    /// <returns>
    /// 	<c>true</c> if the specified window is a file download dialog; otherwise, <c>false</c>.
    /// </returns>
    public bool IsFileDownloadDialog(Window window)
    {
      return (window.StyleInHex == "94C80AC4");
    }

    /// <summary>
    /// Determines whether the specified window is a download progress dialog by
    /// checking the style property of the window. It should match
    /// <c>(window.StyleInHex == "9CCA0BC4") || (window.StyleInHex == "94CA0BC4")</c>
    /// </summary>
    /// <param name="window">The window.</param>
    /// <returns>
    /// 	<c>true</c> if the specified window is a download progress dialog; otherwise, <c>false</c>.
    /// </returns>
    public bool IsDownloadProgressDialog(Window window)
    {
      // "9CCA0BC4" is valid before downloading the file has started
      // "94CA0BC4" is valid during and after the download
      return (window.StyleInHex == "9CCA0BC4") || (window.StyleInHex == "94CA0BC4");
    }

    /// <summary>
    /// Determines whether the specified window is a file save as dialog by
    /// checking the style property of the window. It should match
    /// <c>(window.StyleInHex == "96CC20C4") || (window.StyleInHex == "96CC02C4")</c>
    /// </summary>
    /// <param name="window">The window.</param>
    /// <returns>
    /// 	<c>true</c> if the specified window is a file save as dialog; otherwise, <c>false</c>.
    /// </returns>
    public bool IsFileSaveDialog(Window window)
    {
      // "96CC20C4" is valid for Windows XP, Win 2000 and Win 2003
      // "96CC02C4" is valid for Windows Vista
      return (window.StyleInHex == "96CC20C4") || (window.StyleInHex == "96CC02C4");
    }

    /// <summary>
    /// Determines if a dialog still exists by checking the the existance of the 
    /// window.Hwnd and checking if the window is visible.
    /// </summary>
    /// <param name="dialog">The dialog.</param>
    /// <returns><c>true</c> if exists and visible, otherwise <c>false</c></returns>
    public bool Exists(Window dialog)
    {
      return dialog.Exists() && dialog.Visible;

//	    bool exists = dialog.Exists();
//      Logger.LogAction("Exists: exists == " + exists.ToString());
//
//	    bool visible = dialog.Visible;
//      Logger.LogAction("Exists: visible == " + visible.ToString());
//      
//      return exists && visible;
    }

    /// <summary>
    /// Checks if window is null or <see cref="Exists"/>.
    /// </summary>
    /// <param name="dialog">The dialog.</param>
    /// <returns><c>true</c> if null or exists, otherwise <c>false</c></returns>
    public bool ExistsOrNull(Window dialog)
    {
      if (dialog == null)
      {
//	      Logger.LogAction("ExistsOrNull: dialog == null");
        return true;
      }

      return Exists(dialog);
    }

    /// <summary>
    /// Wait until the save/open/run dialog opens.
    /// This exists because some web servers are slower to start a file than others.
    /// </summary>
    /// <param name="waitDurationInSeconds">duration in seconds to wait</param>
    public void WaitUntilFileDownloadDialogIsHandled(int waitDurationInSeconds)
    {
      SimpleTimer timeoutTimer = new SimpleTimer(waitDurationInSeconds);

      while (!HasHandledFileDownloadDialog && !timeoutTimer.Elapsed)
      {
//        Logger.LogAction("WaitUntilFileDownloadDialogIsHandled");
        Thread.Sleep(200);
      }
      
      if (!HasHandledFileDownloadDialog)
      {
        throw new WatiNException(string.Format("Has not shown dialog after {0} seconds.", waitDurationInSeconds.ToString()));
      }
    }

    /// <summary>
    /// Wait until the download progress window does not exist any more
    /// </summary>
    /// <param name="waitDurationInSeconds">duration in seconds to wait</param>
    public void WaitUntilDownloadCompleted(int waitDurationInSeconds)
    {
      SimpleTimer timeoutTimer = new SimpleTimer(waitDurationInSeconds);

      while (ExistsOrNull(DownloadProgressDialog) && !timeoutTimer.Elapsed)
      {
//        Logger.LogAction("WaitUntilDownloadCompleted");
        Thread.Sleep(200);
      }
      
      if (ExistsOrNull(DownloadProgressDialog))
      {
        throw new WatiNException(string.Format("Still downloading after {0} seconds.", waitDurationInSeconds.ToString()));
      }

      Logger.LogAction("Download complete at " + DateTime.Now.ToLongTimeString());
    }

    private Window DownloadProgressDialog
    {
      get { return downloadProgressDialog; }
      set
      {
//        Logger.LogAction("Entering DownloadProgressDialog.");
        if (downloadProgressDialog == null)
        {
//          Logger.LogAction("downloadProgressDialog == null");
          
          Window dialog = value;

          if (dialog != null)
          {
//            Logger.LogAction("!dialog.Exists()");

            if (!dialog.Exists())
            {
//              Logger.LogAction("download progress dialog should exist.");
              dialog = null;
            }

//            Logger.LogAction("!IsDownloadProgressDialog(dialog)");
            if (!IsDownloadProgressDialog(dialog))
            {
//              Logger.LogAction("Should be a download progress dialog.");
              dialog = null;
            }
          }

//          Logger.LogAction("dialog != null");
          if (dialog != null)
          {
//            Logger.LogAction("Set downloadProgressDialog field.");
            downloadProgressDialog = dialog;
          }
          else
          {
//            Logger.LogAction("downloadProgressDialog not set.");
          }
        }
      }
    }

    private void HandleFileSaveDialog(Window window)
    {      
      IntPtr usernameControlHandle = NativeMethods.GetChildWindowHwnd(window.Hwnd, "Edit");

      NativeMethods.SetForegroundWindow(usernameControlHandle);
      NativeMethods.SetActiveWindow(usernameControlHandle);

      System.Windows.Forms.SendKeys.SendWait(saveAsFilename + "{ENTER}");
    }
  }
}