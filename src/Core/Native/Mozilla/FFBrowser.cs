using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using WatiN.Core.Native.Windows;
using WatiN.Core.Native.Mozilla.Dialogs;
using WatiN.Core.WatchableObjects;

namespace WatiN.Core.Native.Mozilla
{
    public class FFBrowser : JSBrowserBase
    {
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
                    Process[] firefoxProcesses = Process.GetProcessesByName("firefox");
                    int processId = firefoxProcesses[0].Id;
                    mainWindows = WindowFactory.GetWindows(candidateWindow => candidateWindow.ProcessId == processId);
                    _hostWindow = mainWindows[0];
                    mainWindows.Remove(_hostWindow);
                    _dialogManager = new FFDialogManager(_hostWindow, WindowEnumerationMethod.AssistiveTechnologyApi);
                    mainWindowFound = true;
                }
                else
                {
                    mainWindows = WindowFactory.GetWindows(candidateWindow => candidateWindow.ClassName == "MozillaUIWindowClass" || candidateWindow.ClassName == "MozillaDialogClass", false);
                    if (mainWindows.Count >= 1)
                    {
                        if (mainWindows[0].ClassName == "MozillaUIWindowClass")
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
                            using (FFRestoreSessionDialog nativeDialog = new FFRestoreSessionDialog())
                            {
                                nativeDialog.DialogWindow = sessionTerminatedDialog;
                                using (RestoreSessionDialog dialog = new RestoreSessionDialog(nativeDialog))
                                {
                                    dialog.ClickStartNewSessionButton();
                                }
                            }
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
    }
}
