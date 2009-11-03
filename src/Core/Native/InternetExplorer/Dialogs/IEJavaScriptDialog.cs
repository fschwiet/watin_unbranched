using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core.Native.Windows;
using WatiN.Core.Dialogs;
using WatiN.Core.UtilityClasses;

namespace WatiN.Core.Native.InternetExplorer.Dialogs
{
    internal class IEJavaScriptDialog : NativeDialog
    {
        public IEJavaScriptDialog()
        {
            Kind = NativeDialogConstants.JavaScriptAlertDialog;
        }

        #region INativeDialog Members
        /// <inheritdoc />
        public override object GetProperty(string propertyId)
        {
            object propertyValue = null;
            if (propertyId == NativeDialogConstants.TitleProperty)
            {
                propertyValue = DialogWindow.Text;
            }
            else if (propertyId == NativeDialogConstants.MessageProperty)
            {
                IList<Window> staticLabel = DialogWindow.GetChildWindows(w => w.ClassName == WindowFactory.GetWindowClassForRole(AccessibleRole.Label, true) && w.ItemId == 0xFFFF);
                propertyValue = staticLabel[0].Text;
                WindowFactory.DisposeWindows(staticLabel);
            }
            else
            {
                throw new ArgumentException(string.Format("Invalid property name '{0}'", propertyId), "actionId");
            }
            return propertyValue;
        }

        /// <inheritdoc />
        public override void PerformAction(string actionId, object[] args)
        {
            if (actionId == NativeDialogConstants.ClickCancelAction || actionId == NativeDialogConstants.ClickOkAction)
            {
                int buttonId = 2;
                if (Kind != NativeDialogConstants.JavaScriptAlertDialog && actionId == NativeDialogConstants.ClickOkAction)
                    buttonId = 1;
                IList<Window> buttons = DialogWindow.GetChildWindows(b => b.ClassName == WindowFactory.GetWindowClassForRole(AccessibleRole.PushButton, true) && b.ItemId == buttonId);
                buttons[0].Press();
                WindowFactory.DisposeWindows(buttons);
                WaitForWindowToDisappear();
            }
            else
            {
                throw new ArgumentException(string.Format("Invalid action name '{0}'", actionId), "propertyId");
            }
        }

        /// <inheritdoc />
        public override bool WindowIsDialogInstance(Window candidateWindow)
        {
            bool windowIsDialog = false;
            if (!candidateWindow.Text.ToLower().Contains("vbscript"))
            {
                IList<Window> buttons = candidateWindow.GetChildWindows(w => w.ClassName == WindowFactory.GetWindowClassForRole(AccessibleRole.PushButton, true));
                IList<Window> staticLabel = candidateWindow.GetChildWindows(w => w.ClassName == WindowFactory.GetWindowClassForRole(AccessibleRole.Label, true) && w.ItemId == 0xFFFF);
                if (buttons.Count == 1 && buttons[0].ItemId == 2 && staticLabel.Count == 1)
                {
                    Kind = NativeDialogConstants.JavaScriptAlertDialog;
                    windowIsDialog = true;
                }
                else if (buttons.Count == 2 && buttons[0].ItemId == 1 && buttons[1].ItemId == 2 && staticLabel.Count == 1)
                {
                    Kind = NativeDialogConstants.JavaScriptConfirmDialog;
                    windowIsDialog = true;
                }
                WindowFactory.DisposeWindows(buttons);
                WindowFactory.DisposeWindows(staticLabel);
            }
            return windowIsDialog;
        }
        #endregion
    }
}
