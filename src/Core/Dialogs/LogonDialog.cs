using System;
using System.Collections.Generic;
using System.Text;
using WatiN.Core.Native;

namespace WatiN.Core.Dialogs
{
    public class LogonDialog : Dialog
    {
        internal LogonDialog(INativeDialog nativeDialog)
            : base(nativeDialog)
        {
        }

        public string UserName
        {
            get { return NativeDialog.GetProperty(NativeDialogConstants.UserNameProperty).ToString(); }
        }

        public string Password
        {
            get { return NativeDialog.GetProperty(NativeDialogConstants.PasswordProperty).ToString(); }
        }

        public void ClickOkButton()
        {
            NativeDialog.PerformAction(NativeDialogConstants.ClickOkAction, null);
        }

        public void SetUserName(string userName)
        {
            NativeDialog.PerformAction(NativeDialogConstants.UserNameProperty, new object[] { userName });
        }

        public void SetPassword(string password)
        {
            NativeDialog.PerformAction(NativeDialogConstants.SetPasswordAction, new object[] { password });
        }
    }
}
