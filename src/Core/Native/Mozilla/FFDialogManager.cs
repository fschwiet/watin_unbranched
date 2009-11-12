using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WatiN.Core.Native.Windows;
using System.Reflection;
using WatiN.Core.Native.Mozilla.Dialogs;

namespace WatiN.Core.Native.Mozilla
{
    public class FFDialogManager : DialogManager
    {
        public FFDialogManager(Window ffMainWindow, WindowEnumerationMethod childEnumerationMethod)
            : base(ffMainWindow, childEnumerationMethod)
        {
        }

        protected override void RegisterDialogs()
        {
            RegisteredDialogTypes.Add(typeof(FFJavaScriptDialog));
            RegisteredDialogTypes.Add(typeof(FFLogonDialog));
        }
    }
}
