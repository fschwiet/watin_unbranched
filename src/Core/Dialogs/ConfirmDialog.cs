using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core.Native;
using WatiN.Core.Logging;

namespace WatiN.Core.Dialogs
{
    [Handleable(NativeDialogConstants.JavaScriptConfirmDialog)]
    public class ConfirmDialog : AlertDialog
    {
        internal ConfirmDialog(INativeDialog nativeDialog)
            : base(nativeDialog)
        {
        }

        public void ClickCancelButton()
        {
            Logger.LogAction("Clicking Cancel button on {0} dialog", NativeDialog.Kind);
            NativeDialog.PerformAction(NativeDialogConstants.ClickCancelAction, null);
        }
    }
}
