using System;
using System.Collections.Generic;
using System.Text;
using WatiN.Core.Native;

namespace WatiN.Core.WatchableObjects
{
    public class FileLocationDialog : Dialog
    {
        public static readonly string FileNameProperty = "FileName";
        public static readonly string ClickOpenAction = "ClickOpen";
        public static readonly string ClickSaveAction = "ClickSave";
        public static readonly string SetFileNameAction = "SetFileName";

        internal FileLocationDialog(INativeDialog nativeDialog)
            : base(nativeDialog)
        {
        }

        public string FileName
        {
            get { return NativeDialog.GetProperty(FileNameProperty).ToString(); }
        }

        public void ClickOpen()
        {
            NativeDialog.PerformAction(ClickOpenAction, null);
        }

        public void ClickSave()
        {
            NativeDialog.PerformAction(ClickSaveAction, null);
        }

        public void SetFileName(string fileName)
        {
            NativeDialog.PerformAction(SetFileNameAction, new object[] { fileName });
        }
    }
}
