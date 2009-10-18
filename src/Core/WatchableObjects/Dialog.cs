using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core.Interfaces;
using WatiN.Core.Native;

namespace WatiN.Core.WatchableObjects
{
    public abstract class Dialog : IWatchable
    {
        INativeDialog nativeDialogImpl = null;

        internal Dialog(INativeDialog nativeDialog)
        {
            nativeDialogImpl = nativeDialog;
        }

        internal INativeDialog NativeDialog
        {
            get { return nativeDialogImpl; }
        }

        #region IWatchable Members
        /// <inheritdoc />
        public bool Exists
        {
            get { return nativeDialogImpl != null && nativeDialogImpl.DialogWindow != null && nativeDialogImpl.DialogWindow.Exists; }
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (nativeDialogImpl != null)
                nativeDialogImpl.Dispose();
        }

        #endregion
    }
}
