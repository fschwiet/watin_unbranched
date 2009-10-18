using System;
using System.Collections.Generic;
using System.Text;
using WatiN.Core.Native.Windows;

namespace WatiN.Core.Native.Mozilla.Dialogs
{
    public class FFRestoreSessionDialog : INativeDialog
    {
        #region Private members
        readonly int restoreSessionButtonId = 0;
        readonly int newSessionButtonId = 1;
        Window _dialogWindow;
        #endregion

        public FFRestoreSessionDialog()
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX)
            {
                restoreSessionButtonId = 1;
                newSessionButtonId = 2;
            }
            else
            {
                restoreSessionButtonId = 10;
                newSessionButtonId = 11;
            }
        }

        #region INativeDialog Members

        public WatiN.Core.Native.Windows.Window DialogWindow
        {
            get { return _dialogWindow; }
            set { _dialogWindow = value; }
        }

        public string Kind
        {
            get { return "FireFoxRestoreSessionDialog"; }
        }

        public object GetProperty(string propertyId)
        {
            throw new NotImplementedException();
        }

        public void PerformAction(string actionId, object[] args)
        {
            switch (actionId)
            {
                case "ClickRestoreSession":
                case "ClickStartNewSession":
                    int buttonId = restoreSessionButtonId;
                    if (actionId == "ClickStartNewSession")
                        buttonId = newSessionButtonId;
                    IList<Window> buttons = _dialogWindow.GetChildWindows(b => b.ClassName == WindowFactory.GetWindowClassForRole(AccessibleRole.PushButton, false) && b.ItemId == buttonId);
                    buttons[0].Press();
                    WindowFactory.DisposeWindows(buttons);
                    while (_dialogWindow.Exists)
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                    break;
            }

        }

        public void Dismiss()
        {
            _dialogWindow.ForceClose();
        }

        public bool WindowIsDialogInstance(Window candidateWindow)
        {
            bool windowIsDialog = false;
            IList<Window> buttons = candidateWindow.GetChildWindows(w => w.ClassName == WindowFactory.GetWindowClassForRole(AccessibleRole.PushButton, false));
            if (buttons.Count == 2 && candidateWindow.Text.StartsWith("Firefox - "))
            {
                windowIsDialog = true;
            }
            WindowFactory.DisposeWindows(buttons);
            return windowIsDialog;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (_dialogWindow != null)
                _dialogWindow.Dispose();
        }

        #endregion
    }
}
