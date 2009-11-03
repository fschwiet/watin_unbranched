using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core.Native;
using WatiN.Core.Logging;

namespace WatiN.Core.Dialogs
{
    [Handleable(NativeDialogConstants.VBScriptYesNoDialog)]
    public class VBScriptYesNoDialog : VBScriptMsgBoxDialog
    {
        internal VBScriptYesNoDialog(INativeDialog nativeDialog)
            : base(nativeDialog)
        {
        }

        public void ClickYesButton()
        {
            ClickButton(NativeDialogConstants.ClickYesAction, "Yes");
        }

        public void ClickNoButton()
        {
            ClickButton(NativeDialogConstants.ClickNoAction, "No");
        }
    }
}
