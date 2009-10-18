using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WatiN.Core.Native.Windows;
using System.Reflection;

namespace WatiN.Core.Native.Chrome
{
    public class ChromeDialogManager : DialogManager
    {
        public ChromeDialogManager(Window chromeMainWindow, WindowEnumerationMethod childEnumerationMethod)
            : base(chromeMainWindow, childEnumerationMethod)
        {
        }

        protected override void RegisterDialogs()
        {
            //RegisteredDialogTypes.Add(typeof(FFJavaScriptDialog));
        }
    }
}
