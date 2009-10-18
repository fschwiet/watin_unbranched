using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core.Native.Windows;

namespace WatiN.Core.Native.Mozilla.Dialogs
{
    public class FFJavaScriptDialog : INativeDialog
    {
        #region Private members
        string _kind = "AlertDialog";
        Window _dialogWindow = null;

        readonly int okButtonId = 0;
        readonly int cancelButtonId = 0;
        readonly string messageLabelClass = string.Empty;
        #endregion

        public FFJavaScriptDialog()
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX)
            {
                okButtonId = 1;
                cancelButtonId = 2;
                messageLabelClass = WindowFactory.GetWindowClassForRole(AccessibleRole.Label, false);
            }
            else
            {
                okButtonId = 10;
                cancelButtonId = 11;
                messageLabelClass = WindowFactory.GetWindowClassForRole(AccessibleRole.Text, false);
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (_dialogWindow != null)
                _dialogWindow.Dispose();
        }

        #endregion

        #region INativeDialog Members

        public Window DialogWindow
        {
            get { return _dialogWindow; }
            set { _dialogWindow = value; }
        }

        public string Kind
        {
            get { return _kind; }
        }

        public object GetProperty(string propertyId)
        {
            object propertyValue = null;
            switch (propertyId)
            {
                case "Title":
                    propertyValue = _dialogWindow.Text;
                    break;

                case "Message":
                    string className = WindowFactory.GetWindowClassForRole(AccessibleRole.Text, false);
                    if (Environment.OSVersion.Platform == PlatformID.Unix)
                        className = WindowFactory.GetWindowClassForRole(AccessibleRole.Label, false);
                    IList<Window> staticLabel = _dialogWindow.GetChildWindows(w => w.ClassName == className);
                    propertyValue = staticLabel[0].Text;
                    WindowFactory.DisposeWindows(staticLabel);
                    break;
            }
            return propertyValue;
        }

        public void PerformAction(string actionId, object[] args)
        {
            switch (actionId)
            {
                case "ClickCancel":
                case "ClickOk":
                    int buttonId = okButtonId;
                    if (actionId == "ClickCancel")
                        buttonId = cancelButtonId;
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
            IList<Window> staticLabel = candidateWindow.GetChildWindows(w => w.ClassName == messageLabelClass);
            if (buttons.Count == 1 && buttons[0].ItemId == okButtonId && staticLabel.Count == 1)
            {
                _kind = "AlertDialog";
                windowIsDialog = true;
            }
            else if (buttons.Count == 2 && buttons[0].ItemId == okButtonId && buttons[1].ItemId == cancelButtonId && staticLabel.Count == 1)
            {
                _kind = "ConfirmDialog";
                windowIsDialog = true;
            }
            WindowFactory.DisposeWindows(buttons);
            WindowFactory.DisposeWindows(staticLabel);
            return windowIsDialog;
        }
        #endregion
    }
}
