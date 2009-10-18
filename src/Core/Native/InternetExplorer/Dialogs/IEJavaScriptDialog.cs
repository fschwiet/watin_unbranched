using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core.Native.Windows;
using WatiN.Core.WatchableObjects;

namespace WatiN.Core.Native.InternetExplorer.Dialogs
{
    public class IEJavaScriptDialog : INativeDialog
    {
        #region Private members
        private string _kind = "AlertDialog";
        private Window _dialogWindow = null;
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (_dialogWindow != null)
                _dialogWindow.Dispose();
        }

        #endregion

        #region INativeDialog Members
        /// <inheritdoc />
        public Window DialogWindow
        {
            get { return _dialogWindow; }
            set { _dialogWindow = value; }
        }

        /// <inheritdoc />
        public string Kind
        {
            get { return _kind; }
        }

        /// <inheritdoc />
        public object GetProperty(string propertyId)
        {
            object propertyValue = null;
            switch (propertyId)
            {
                case AlertDialog.TitleProperty:
                    propertyValue = _dialogWindow.Text;
                    break;

                case AlertDialog.MessageProperty:
                    IList<Window> staticLabel = _dialogWindow.GetChildWindows(w => w.ClassName == WindowFactory.GetWindowClassForRole(AccessibleRole.Label, true) && w.ItemId == 0xFFFF);
                    propertyValue = staticLabel[0].Text;
                    WindowFactory.DisposeWindows(staticLabel);
                    break;
            }
            return propertyValue;
        }

        /// <inheritdoc />
        public void PerformAction(string actionId, object[] args)
        {
            switch (actionId)
            {
                case ConfirmDialog.ClickCancelAction:
                case AlertDialog.ClickOkAction:
                    int buttonId = 2;
                    if (_kind != "AlertDialog" && actionId == AlertDialog.ClickOkAction)
                        buttonId = 1;
                    IList<Window> buttons = _dialogWindow.GetChildWindows(b => b.ClassName == WindowFactory.GetWindowClassForRole(AccessibleRole.PushButton, true) && b.ItemId == buttonId);
                    buttons[0].Press();
                    WindowFactory.DisposeWindows(buttons);
                    break;
            }
        }

        /// <inheritdoc />
        public void Dismiss()
        {
            _dialogWindow.ForceClose();
        }

        /// <inheritdoc />
        public bool WindowIsDialogInstance(Window candidateWindow)
        {
            bool windowIsDialog = false;
            IList<Window> buttons = candidateWindow.GetChildWindows(w => w.ClassName == WindowFactory.GetWindowClassForRole(AccessibleRole.PushButton, true));
            IList<Window> staticLabel = candidateWindow.GetChildWindows(w => w.ClassName == WindowFactory.GetWindowClassForRole(AccessibleRole.Label, true) && w.ItemId == 0xFFFF);
            if (buttons.Count == 1 && buttons[0].ItemId == 2 && staticLabel.Count == 1)
            {
                _kind = "AlertDialog";
                windowIsDialog = true;
            }
            else if (buttons.Count == 2 && buttons[0].ItemId == 1 && buttons[1].ItemId == 2 && staticLabel.Count == 1)
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
