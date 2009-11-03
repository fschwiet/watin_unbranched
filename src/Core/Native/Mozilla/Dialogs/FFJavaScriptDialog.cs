using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core.Native.Windows;
using WatiN.Core.UtilityClasses;

namespace WatiN.Core.Native.Mozilla.Dialogs
{
    internal class FFJavaScriptDialog : NativeDialog
    {
        #region Private members
        private readonly int okButtonId = 0;
        private readonly int cancelButtonId = 0;
        private readonly string messageLabelClass = string.Empty;
        #endregion

        public FFJavaScriptDialog()
        {
            Kind = NativeDialogConstants.JavaScriptAlertDialog; 
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

        #region INativeDialog Members

        public override object GetProperty(string propertyId)
        {
            object propertyValue = null;
            if (propertyId == NativeDialogConstants.TitleProperty)
            {
                    propertyValue = DialogWindow.Text;
            }
            else if (propertyId == NativeDialogConstants.MessageProperty)
            {
                string className = WindowFactory.GetWindowClassForRole(AccessibleRole.Text, false);
                if (Environment.OSVersion.Platform == PlatformID.Unix)
                    className = WindowFactory.GetWindowClassForRole(AccessibleRole.Label, false);
                IList<Window> staticLabel = DialogWindow.GetChildWindows(w => w.ClassName == className);
                propertyValue = staticLabel[0].Text;
                WindowFactory.DisposeWindows(staticLabel);
            }
            else
            {
                throw new ArgumentException(string.Format("Invalid property name '{0}'", propertyId), "propertyId");
            }
            return propertyValue;
        }

        public override void PerformAction(string actionId, object[] args)
        {
            if (actionId == NativeDialogConstants.ClickCancelAction || actionId == NativeDialogConstants.ClickOkAction)
            {
                int buttonId = okButtonId;
                if (actionId == NativeDialogConstants.ClickCancelAction)
                    buttonId = cancelButtonId;
                IList<Window> buttons = DialogWindow.GetChildWindows(b => b.ClassName == WindowFactory.GetWindowClassForRole(AccessibleRole.PushButton, false) && b.ItemId == buttonId);
                buttons[0].Press();
                WindowFactory.DisposeWindows(buttons);
                WaitForWindowToDisappear();
            }
            else
            {
                throw new ArgumentException(string.Format("Invalid action name '{0}'", actionId), "propertyId");
            }
        }

        public override bool WindowIsDialogInstance(Window candidateWindow)
        {
            bool windowIsDialog = false;
            IList<Window> buttons = candidateWindow.GetChildWindows(w => w.ClassName == WindowFactory.GetWindowClassForRole(AccessibleRole.PushButton, false));
            IList<Window> staticLabel = candidateWindow.GetChildWindows(w => w.ClassName == messageLabelClass);
            if (buttons.Count == 1 && buttons[0].ItemId == okButtonId && staticLabel.Count == 1)
            {
                Kind = NativeDialogConstants.JavaScriptAlertDialog;
                windowIsDialog = true;
            }
            else if (buttons.Count == 2 && buttons[0].ItemId == okButtonId && buttons[1].ItemId == cancelButtonId && staticLabel.Count == 1)
            {
                Kind = NativeDialogConstants.JavaScriptConfirmDialog;
                windowIsDialog = true;
            }
            WindowFactory.DisposeWindows(buttons);
            WindowFactory.DisposeWindows(staticLabel);
            return windowIsDialog;
        }
        #endregion
    }
}
