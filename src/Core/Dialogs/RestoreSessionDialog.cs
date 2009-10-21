using System;
using System.Collections.Generic;
using System.Text;
using WatiN.Core.Native;
using WatiN.Core.Logging;

namespace WatiN.Core.Dialogs
{
    public class RestoreSessionDialog : Dialog
    {
        internal RestoreSessionDialog(INativeDialog nativeDialog)
            : base(nativeDialog)
        {
        }

        public void ClickRestoreSessionButton()
        {
            Logger.LogAction("Clicking Restore New Session button on {0} dialog", NativeDialog.Kind);
            NativeDialog.PerformAction(NativeDialogConstants.ClickRestoreSessionAction, null);
        }

        public void ClickStartNewSessionButton()
        {
            Logger.LogAction("Clicking Start New Session button on {0} dialog", NativeDialog.Kind);
            NativeDialog.PerformAction(NativeDialogConstants.ClickStartNewSessionAction, null);
        }

    }
}
