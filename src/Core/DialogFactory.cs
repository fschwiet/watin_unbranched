using System;
using System.Collections.Generic;
using System.Text;
using WatiN.Core.Native;
using System.Reflection;
using WatiN.Core.Dialogs;
using WatiN.Core.Interfaces;

namespace WatiN.Core
{
    internal static class DialogFactory
    {
        internal static Dialog CreateDialog(INativeDialog nativeDialog)
        {
            // TODO: Refactor this into a true factory, including facility for
            // user registration of new dialog types.
            Dialog dialogInstance = null;
            if (nativeDialog.Kind == NativeDialogConstants.JavaScriptAlertDialog)
            {
                dialogInstance = new AlertDialog(nativeDialog);
            }
            else if (nativeDialog.Kind == NativeDialogConstants.JavaScriptConfirmDialog)
            {
                dialogInstance = new ConfirmDialog(nativeDialog);
            }
            return dialogInstance;
        }
    }
}
