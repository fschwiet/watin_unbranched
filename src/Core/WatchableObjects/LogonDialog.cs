using System;
using System.Collections.Generic;
using System.Text;
using WatiN.Core.Native;

namespace WatiN.Core.WatchableObjects
{
    public class LogonDialog : Dialog
    {
        public static readonly string UserNameProperty = "UserName";
        public static readonly string PasswordProperty = "Password";
        public static readonly string ClickOkAction = "ClickOk";
        public static readonly string SetUserNameAction = "SetUserName";
        public static readonly string SetPasswordAction = "SetPassword";

        internal LogonDialog(INativeDialog nativeDialog)
            : base(nativeDialog)
        {
        }

        public string UserName
        {
            get { return NativeDialog.GetProperty(UserNameProperty).ToString(); }
        }

        public string Password
        {
            get { return NativeDialog.GetProperty(PasswordProperty).ToString(); }
        }

        public void ClickOkButton()
        {
            NativeDialog.PerformAction(ClickOkAction, null);
        }

        public void SetUserName(string userName)
        {
            NativeDialog.PerformAction(UserNameProperty, new object[] { userName });
        }

        public void SetPassword(string password)
        {
            NativeDialog.PerformAction(SetPasswordAction, new object[] { password });
        }
    }
}
