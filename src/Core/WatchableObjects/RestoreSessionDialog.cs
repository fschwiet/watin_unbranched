using System;
using System.Collections.Generic;
using System.Text;
using WatiN.Core.Native;

namespace WatiN.Core.WatchableObjects
{
    public class RestoreSessionDialog : Dialog
    {
        public static readonly string ClickRestoreSessionAction = "ClickRestoreSession";
        public static readonly string ClickStartNewSessionAction = "ClickStartNewSession";

        internal RestoreSessionDialog(INativeDialog nativeDialog)
            : base(nativeDialog)
        {
        }

        public void ClickRestoreSessionButton()
        {
            NativeDialog.PerformAction(ClickRestoreSessionAction, null);
        }

        public void ClickStartNewSessionButton()
        {
            NativeDialog.PerformAction(ClickStartNewSessionAction, null);
        }

    }
}
