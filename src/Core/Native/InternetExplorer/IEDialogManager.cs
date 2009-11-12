using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WatiN.Core.Native.Windows;
using System.Reflection;
using WatiN.Core.Native.InternetExplorer.Dialogs;

namespace WatiN.Core.Native.InternetExplorer
{
    public class IEDialogManager : DialogManager
    {
        public IEDialogManager(Window mainIeWindow, WindowEnumerationMethod childEnumerationMethod)
            : base(mainIeWindow, childEnumerationMethod)
        {
        }

        protected override void RegisterDialogs()
        {
            RegisteredDialogTypes.Add(typeof(IEJavaScriptDialog));
            RegisteredDialogTypes.Add(typeof(IEVBScriptDialog));
            RegisteredDialogTypes.Add(typeof(IELogonDialog));
        }
    }
}
