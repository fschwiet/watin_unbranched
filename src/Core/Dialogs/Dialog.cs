using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core.Interfaces;
using WatiN.Core.Native;

namespace WatiN.Core.Dialogs
{
    public abstract class Dialog : IWatchable
    {
        private INativeDialog nativeDialogImpl = null;
        private bool isDisposed = false;

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
        public virtual bool Exists
        {
            get { return nativeDialogImpl != null && nativeDialogImpl.DialogWindow != null && nativeDialogImpl.DialogWindow.Exists; }
        }

        /// <inheritdoc />
        public virtual void DoDefaultAction()
        {
            nativeDialogImpl.Dismiss();
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

        protected void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    if (nativeDialogImpl != null)
                        nativeDialogImpl.Dispose();
                }
                nativeDialogImpl = null;
                isDisposed = true;
            }
        }
    }
}
