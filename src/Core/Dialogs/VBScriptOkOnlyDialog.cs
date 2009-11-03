using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core.Native;
using WatiN.Core.Logging;

namespace WatiN.Core.Dialogs
{
    [Handleable(NativeDialogConstants.VBScriptOKOnlyDialog)]
    public class VBScriptOkOnlyDialog : VBScriptMsgBoxDialog
    {
        internal VBScriptOkOnlyDialog(INativeDialog nativeDialog)
            : base(nativeDialog)
        {
        }

        public void ClickOkButton()
        {
            ClickButton(NativeDialogConstants.ClickOkAction, "OK");
        }
    }
}
