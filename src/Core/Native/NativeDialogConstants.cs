using System;
using System.Collections.Generic;
using System.Text;

namespace WatiN.Core.Native
{
    public static class NativeDialogConstants
    {
        public static readonly string JavaScriptAlertDialog = "AlertDialog";
        public static readonly string JavaScriptConfirmDialog = "ConfirmDialog";
        public static readonly string FireFoxRestoreSessionDialog = "FireFoxRestoreSessionDialog";

        public static readonly string TitleProperty = "TitleProperty";
        public static readonly string MessageProperty = "MessageProperty";
        public static readonly string FileNameProperty = "FileNameProperty";
        public static readonly string UserNameProperty = "UserName";
        public static readonly string PasswordProperty = "Password";

        public static readonly string ClickOkAction = "ClickOk";
        public static readonly string ClickCancelAction = "ClickCancel";
        public static readonly string ClickOpenAction = "ClickOpen";
        public static readonly string ClickSaveAction = "ClickSave";
        public static readonly string ClickRestoreSessionAction = "ClickRestoreSession";
        public static readonly string ClickStartNewSessionAction = "ClickStartNewSession";
        public static readonly string SetFileNameAction = "SetFileName";
        public static readonly string SetUserNameAction = "SetUserName";
        public static readonly string SetPasswordAction = "SetPassword";
    }
}
