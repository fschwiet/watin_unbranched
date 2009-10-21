using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core.Native;
using WatiN.Core.Logging;

namespace WatiN.Core.Dialogs
{
    public class AlertDialog : Dialog
    {
        internal AlertDialog(INativeDialog nativeDialog)
            : base(nativeDialog)
        {
        }

        public string Title
        {
            get { return NativeDialog.GetProperty(NativeDialogConstants.TitleProperty).ToString(); }
        }

        public string Message
        {
            get { return NativeDialog.GetProperty(NativeDialogConstants.MessageProperty).ToString(); }
        }

        public void ClickOkButton()
        {
            Logger.LogAction("Clicking OK button on {0} dialog", NativeDialog.Kind);
            NativeDialog.PerformAction(NativeDialogConstants.ClickOkAction, null);
        }
    }
}
