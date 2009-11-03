using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core.Native;
using WatiN.Core.Logging;

namespace WatiN.Core.Dialogs
{
    [Handleable(NativeDialogConstants.VBScriptAbortRetryIgnoreDialog)]
    public class VBScriptAbortRetryIgnoreDialog : VBScriptMsgBoxDialog
    {
        internal VBScriptAbortRetryIgnoreDialog(INativeDialog nativeDialog)
            : base(nativeDialog)
        {
        }

        public void ClickAbortButton()
        {
            ClickButton(NativeDialogConstants.ClickAbortAction, "Abort");
        }

        public void ClickRetryButton()
        {
            ClickButton(NativeDialogConstants.ClickRetryAction, "Retry");
        }

        public void ClickIgnoreButton()
        {
            ClickButton(NativeDialogConstants.ClickIgnoreAction, "Ignore");
        }
    }
}
