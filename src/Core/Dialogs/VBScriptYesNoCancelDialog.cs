using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core.Native;
using WatiN.Core.Logging;

namespace WatiN.Core.Dialogs
{
    [Handleable(NativeDialogConstants.VBScriptYesNoCancelDialog)]
    public class VBScriptYesNoCancelDialog : VBScriptYesNoDialog
    {
        internal VBScriptYesNoCancelDialog(INativeDialog nativeDialog)
            : base(nativeDialog)
        {
        }

        public void ClickCancelButton()
        {
            ClickButton(NativeDialogConstants.ClickCancelAction, "Cancel");
        }
    }
}
