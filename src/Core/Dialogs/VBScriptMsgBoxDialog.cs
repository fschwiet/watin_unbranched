using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core.Native;
using WatiN.Core.Logging;

namespace WatiN.Core.Dialogs
{
    public class VBScriptMsgBoxDialog : Dialog
    {
        internal VBScriptMsgBoxDialog(INativeDialog nativeDialog)
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

        protected void ClickButton(string buttonActionId, string buttonDescription)
        {
            Logger.LogAction("Clicking {0} button on {1} dialog", buttonDescription, NativeDialog.Kind);
            NativeDialog.PerformAction(buttonActionId, null);
        }
    }
}
