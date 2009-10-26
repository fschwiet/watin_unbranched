using System;
using System.Collections.Generic;
using System.Text;
using WatiN.Core.Native.Windows;
using WatiN.Core.UtilityClasses;

namespace WatiN.Core.Native.Mozilla.Dialogs
{
    public class FFRestoreSessionDialog : NativeDialog
    {
        #region Private members
        readonly int restoreSessionButtonId = 0;
        readonly int newSessionButtonId = 1;
        #endregion

        public FFRestoreSessionDialog()
        {
            Kind = NativeDialogConstants.FireFoxRestoreSessionDialog;
            if (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX)
            {
                restoreSessionButtonId = 2;
                newSessionButtonId = 1;
            }
            else
            {
                restoreSessionButtonId = 10;
                newSessionButtonId = 11;
            }
        }

        #region INativeDialog Members

        public override object GetProperty(string propertyId)
        {
            throw new NotImplementedException();
        }

        public override void PerformAction(string actionId, object[] args)
        {
            if (actionId == NativeDialogConstants.ClickRestoreSessionAction || actionId == NativeDialogConstants.ClickStartNewSessionAction)
            {
                int buttonId = restoreSessionButtonId;
                if (actionId == NativeDialogConstants.ClickStartNewSessionAction)
                    buttonId = newSessionButtonId;
                IList<Window> buttons = DialogWindow.GetChildWindows(b => b.ClassName == WindowFactory.GetWindowClassForRole(AccessibleRole.PushButton, false) && b.ItemId == buttonId);
                buttons[0].Press();
                WindowFactory.DisposeWindows(buttons);
                WaitForWindowToDisappear();
            }
        }

        public override bool WindowIsDialogInstance(Window candidateWindow)
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
    }
}
