using System;
using System.Collections.Generic;
using System.Text;

namespace WatiN.Core.Native
{
    public static class NativeDialogConstants
    {
        internal const string JavaScriptAlertDialog = "AlertDialog";
        internal const string JavaScriptConfirmDialog = "ConfirmDialog";
        internal const string FireFoxRestoreSessionDialog = "FireFoxRestoreSessionDialog";
        internal const string VBScriptOKOnlyDialog = "VBScriptOKOnlyDialog";
        internal const string VBScriptOKCancelDialog = "VBScriptOKCancelDialog";
        internal const string VBScriptAbortRetryIgnoreDialog = "VBScriptAbortRetryIgnoreDialog";
        internal const string VBScriptYesNoCancelDialog = "VBScriptYesNoCancelDialog";
        internal const string VBScriptYesNoDialog = "VBScriptYesNoDialog";
        internal const string VBScriptRetryCancelDialog = "VBScriptRetryCancelDialog";

        public static readonly string TitleProperty = "TitleProperty";
        public static readonly string MessageProperty = "MessageProperty";
        public static readonly string FileNameProperty = "FileNameProperty";
        public static readonly string UserNameProperty = "UserName";
        public static readonly string PasswordProperty = "Password";

        public static readonly string ClickOkAction = "ClickOk";
        public static readonly string ClickCancelAction = "ClickCancel";
        public static readonly string ClickYesAction = "ClickYes";
        public static readonly string ClickNoAction = "ClickNo";
        public static readonly string ClickAbortAction = "ClickAbort";
        public static readonly string ClickRetryAction = "ClickRetry";
        public static readonly string ClickIgnoreAction = "ClickIgnore";
        public static readonly string ClickOpenAction = "ClickOpen";
        public static readonly string ClickSaveAction = "ClickSave";
        public static readonly string ClickRestoreSessionAction = "ClickRestoreSession";
        public static readonly string ClickStartNewSessionAction = "ClickStartNewSession";
        public static readonly string SetFileNameAction = "SetFileName";
        public static readonly string SetUserNameAction = "SetUserName";
        public static readonly string SetPasswordAction = "SetPassword";
    }
}
