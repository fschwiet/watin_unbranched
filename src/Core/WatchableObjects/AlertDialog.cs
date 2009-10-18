using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core.Native;

namespace WatiN.Core.WatchableObjects
{
    public class AlertDialog : Dialog
    {
        public const string TitleProperty = "Title";
        public const string MessageProperty = "Message";
        public const string ClickOkAction = "ClickOk";

        internal AlertDialog(INativeDialog nativeDialog)
            : base(nativeDialog)
        {
        }

        public string Title
        {
            get { return NativeDialog.GetProperty(TitleProperty).ToString(); }
        }

        public string Message
        {
            get { return NativeDialog.GetProperty(MessageProperty).ToString(); }
        }

        public void ClickOkButton()
        {
            NativeDialog.PerformAction(ClickOkAction, null);
        }
    }
}
