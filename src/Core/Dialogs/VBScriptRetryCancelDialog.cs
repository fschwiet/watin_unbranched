using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core.Native;
using WatiN.Core.Logging;

namespace WatiN.Core.Dialogs
{
    [Handleable(NativeDialogConstants.VBScriptRetryCancelDialog)]
    public class VBScriptRetryCancelDialog : VBScriptMsgBoxDialog
    {
        internal VBScriptRetryCancelDialog(INativeDialog nativeDialog)
            : base(nativeDialog)
        {
        }

        public void ClickRetryButton()
        {
            ClickButton(NativeDialogConstants.ClickRetryAction, "Retry");
        }

        public void ClickCancelButton()
        {
            ClickButton(NativeDialogConstants.ClickCancelAction, "Cancel");
        }
    }
}
