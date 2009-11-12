using System;
using System.Collections.Generic;
using System.Text;
using WatiN.Core.Native.Windows;
using WatiN.Core.UtilityClasses;

namespace WatiN.Core.Native.Mozilla.Dialogs
{
    internal class FFLogonDialog : NativeDialog
    {
        #region Private members
        private readonly int okButtonId = 0;
        private readonly int cancelButtonId = 0;
        #endregion

        public FFLogonDialog()
        {
            Kind = NativeDialogConstants.LogonDialog;
            if (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX)
            {
                okButtonId = 1;
                cancelButtonId = 2;
            }
            else
            {
                okButtonId = 14;
                cancelButtonId = 15;
            }
        }

        #region INativeDialog Members
        /// <inheritdoc />
        public override object GetProperty(string propertyId)
        {
            object propertyValue = null;
            if (propertyId == NativeDialogConstants.UserNameProperty || propertyId == NativeDialogConstants.PasswordProperty)
            {
                string targetClassName = WindowFactory.GetWindowClassForRole(AccessibleRole.Text, false);
                if (propertyId == NativeDialogConstants.PasswordProperty)
                    targetClassName = AccessibleRole.PasswordText.ToString();

                IList<Window> windowList = DialogWindow.GetChildWindows(w => w.ClassName == targetClassName);
                if (windowList.Count > 0)
                {
                    propertyValue = windowList[0].Text;
                }
                WindowFactory.DisposeWindows(windowList);
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
                int buttonId = okButtonId;
                if (actionId == NativeDialogConstants.ClickCancelAction)
                    buttonId = cancelButtonId;
                IList<Window> buttons = DialogWindow.GetChildWindows(b => b.ClassName == WindowFactory.GetWindowClassForRole(AccessibleRole.PushButton, false) && b.ItemId == buttonId);
                if (buttons.Count > 0)
                {
                    buttons[0].Press();
                }
                WindowFactory.DisposeWindows(buttons);
                WaitForWindowToDisappear();
            }
            else if (actionId == NativeDialogConstants.SetUserNameAction || actionId == NativeDialogConstants.SetPasswordAction)
            {
                string textValue = UtilityClass.EscapeSendKeysCharacters(args[0].ToString());
                string targetClassName = WindowFactory.GetWindowClassForRole(AccessibleRole.Text, false);
                if (actionId == NativeDialogConstants.SetPasswordAction)
                    targetClassName = AccessibleRole.PasswordText.ToString();

                IList<Window> windowList = DialogWindow.GetChildWindows(w => w.ClassName == targetClassName && !w.AccessibleObject.StateSet.Contains(AccessibleState.SelectableText));
                if (windowList.Count > 0)
                {
                    windowList[0].SendKeystrokes(textValue);
                }
                WindowFactory.DisposeWindows(windowList);
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
            IList<Window> childWindowList = candidateWindow.GetChildWindows(w => w.ClassName == AccessibleRole.PasswordText.ToString());
            if (childWindowList.Count > 0)
            {
                windowIsDialog = true;
            }
            WindowFactory.DisposeWindows(childWindowList);
            return windowIsDialog;
        }
        #endregion
    }
}
