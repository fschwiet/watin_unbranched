using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using WatiN.Core.Native.Windows;
using WatiN.Core.Native.Mozilla.Dialogs;
using WatiN.Core.Dialogs;
using WatiN.Core.UtilityClasses;

namespace WatiN.Core.Native.Mozilla
{
    public class FFBrowser : JSBrowserBase
    {
        private const string MozillaMainWindowClass = "MozillaUIWindowClass";
        private const string MozillaDialogWindowClass = "MozillaDialogClass";
        #region Private members
        private FFDialogManager _dialogManager = null;
        private Window _hostWindow = null;
        #endregion

        #region Constructor
        public FFBrowser(ClientPortBase clientPort) : base(clientPort)
        {
            bool mainWindowFound = false;
            IList<Window> mainWindows = new List<Window>();
            while (!mainWindowFound)
            {
                if (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX)
                {
                    Process ffProcess = FireFox.CurrentProcess;
                    if (ffProcess != null)
                    {
                        int processId = ffProcess.Id;
                        mainWindows = WindowFactory.GetWindows(candidateWindow => candidateWindow.ProcessId == processId);
                        // Note that the normal workflow here is to wait for the application 
                        // window to appear before we ever get here. Still, we could have an
                        // instance where we have a time lag before window creation, so let's
                        // check first.
                        if (mainWindows.Count > 0)
                        {
                            // On Linux, Firefox displays only the session terminated dialog
                            // without a window, so let's drop the window, then wait to see if
                            // we get the main browser window to appear.
                            if (mainWindows[0].IsDialog)
                            {
                                Window sessionTerminatedDialog = mainWindows[0];
                                mainWindows.Remove(sessionTerminatedDialog);
                                DismissSesssionTerminatedDialog(sessionTerminatedDialog, true);
                                WindowFactory.DisposeWindows(mainWindows);

                                if (MainBrowserWindowIsVisible(processId))
                                {
                                    mainWindows = WindowFactory.GetWindows(candidateWindow => candidateWindow.ProcessId == processId);
                                }
                            }
                            _hostWindow = mainWindows[0];
                            mainWindows.Remove(_hostWindow);
                            _dialogManager = new FFDialogManager(_hostWindow, WindowEnumerationMethod.AssistiveTechnologyApi);
                            mainWindowFound = true;
                        }
                    }
                }
                else
                {
                    mainWindows = WindowFactory.GetWindows(candidateWindow => candidateWindow.ClassName == MozillaMainWindowClass || candidateWindow.ClassName == MozillaDialogWindowClass, false);
                    if (mainWindows.Count >= 1)
                    {
                        // MozillaUIWindowClass is the class for the main window. If it does not exist,
                        // but MozillaDialogClass does, we probably have the Restore Previous Session
                        // dialog, which we need to knock down, and look again for the main window.
                        if (mainWindows[0].ClassName == MozillaMainWindowClass)
                        {
                            _hostWindow = mainWindows[0];
                            mainWindows.Remove(_hostWindow);
                            _dialogManager = new FFDialogManager(_hostWindow, WindowEnumerationMethod.AssistiveTechnologyApi);
                            mainWindowFound = true;
                        }
                        else
                        {
                            Window sessionTerminatedDialog = mainWindows[0];
                            mainWindows.Remove(sessionTerminatedDialog);
                            DismissSesssionTerminatedDialog(sessionTerminatedDialog, true);
                        }
                    }
                }
                WindowFactory.DisposeWindows(mainWindows);
            }
        }
        #endregion

        /// <summary>
        /// Load a URL into the document. see: http://developer.mozilla.org/en/docs/XUL:browser#m-loadURI
        /// </summary>
        /// <param name="url">The URL to laod.</param>
        /// <param name="waitForComplete">If false, makes to execution of LoadUri asynchronous.</param>
        protected override void LoadUri(Uri url, bool waitForComplete)
        {
            var command = string.Format("{0}.loadURI(\"{1}\");", BrowserVariableName, url.AbsoluteUri);
            if (!waitForComplete)
            {
                command = JSUtils.WrapCommandInTimer(command);
            }

            ClientPort.Write(command);
        }

        public override INativeDocument NativeDocument
        {
            get { return new FFDocument(ClientPort); }
        }

        public override Window HostWindow
        {
            get { return _hostWindow; }
        }

        public override INativeDialogManager NativeDialogManager
        {
            get { return _dialogManager; }
        }

        public override void Dispose()
        {
            if (_dialogManager != null)
                _dialogManager.Dispose();
        }

        private void DismissSesssionTerminatedDialog(Window sessionTerminatedDialog, bool startNewSession)
        {
            using (FFRestoreSessionDialog nativeDialog = new FFRestoreSessionDialog())
            {
                nativeDialog.DialogWindow = sessionTerminatedDialog;
                using (RestoreSessionDialog dialog = new RestoreSessionDialog(nativeDialog))
                {
                    if (startNewSession)
                    {
                        dialog.ClickStartNewSessionButton();
                    }
                    else
                    {
                        dialog.ClickRestoreSessionButton();
                    }
                }
            }
        }

        private bool MainBrowserWindowIsVisible(int ffProcessId)
        {
            return TryFuncUntilTimeOut.Try<bool>(TimeSpan.FromMilliseconds(10000), () => 
                {
                    IList<Window> windowList = WindowFactory.GetWindows(w => w.ProcessId == ffProcessId);
                    bool windowFound = windowList.Count > 0;
                    WindowFactory.DisposeWindows(windowList);
                    if (windowFound)
                    {
                        return true;
                    }
                    return false;
                });
        }
    }
}
