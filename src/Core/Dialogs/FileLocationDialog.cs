using System;
using System.Collections.Generic;
using System.Text;
using WatiN.Core.Native;

namespace WatiN.Core.Dialogs
{
    public class FileLocationDialog : Dialog
    {
        internal FileLocationDialog(INativeDialog nativeDialog)
            : base(nativeDialog)
        {
        }

        public string FileName
        {
            get { return NativeDialog.GetProperty(NativeDialogConstants.FileNameProperty).ToString(); }
            set { NativeDialog.PerformAction(NativeDialogConstants.SetFileNameAction, new object[] { value }); }
        }

        public void ClickOpen()
        {
            NativeDialog.PerformAction(NativeDialogConstants.ClickOpenAction, null);
        }

        public void ClickSave()
        {
            NativeDialog.PerformAction(NativeDialogConstants.ClickSaveAction, null);
        }

        public void SetFileName(string fileName)
        {
            NativeDialog.PerformAction(NativeDialogConstants.SetFileNameAction, new object[] { fileName });
        }
    }
}
