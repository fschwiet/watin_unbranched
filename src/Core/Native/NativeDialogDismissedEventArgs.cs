using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatiN.Core.Native
{
    public class NativeDialogDismissedEventArgs : EventArgs
    {
        INativeDialog _nativeDialog = null;

        internal NativeDialogDismissedEventArgs(INativeDialog nativeDialog)
        {
            _nativeDialog = nativeDialog;
        }

        internal INativeDialog NativeDialog
        {
            get { return _nativeDialog; }
        }
    }
}
