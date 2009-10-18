using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core.Native;

namespace WatiN.Core.WatchableObjects
{
    public class ConfirmDialog : AlertDialog
    {
        public const string ClickCancelAction = "ClickCancel";

        internal ConfirmDialog(INativeDialog nativeDialog)
            : base(nativeDialog)
        {
        }

        public void ClickCancelButton()
        {
            NativeDialog.PerformAction(ClickCancelAction, null);
        }
    }
}
