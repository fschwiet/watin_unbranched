using System;
using System.Collections.Generic;
using System.Text;
using WatiN.Core.Native.Windows;
using WatiN.Core.UtilityClasses;

namespace WatiN.Core.Native
{
    public abstract class NativeDialog : INativeDialog
    {
        #region Private members
        private string _kind = string.Empty;
        private Window _dialogWindow = null;
        #endregion

        #region INativeDialog Members
        /// <inheritdoc />
        public Window DialogWindow
        {
            get { return _dialogWindow; }
            set { _dialogWindow = value; }
        }

        /// <inheritdoc />
        public string Kind
        {
            get { return _kind; }
            protected set { _kind = value; }
        }

        /// <inheritdoc />
        public abstract object GetProperty(string propertyId);

        /// <inheritdoc />
        public abstract void PerformAction(string actionId, object[] args);

        /// <inheritdoc />
        public abstract bool WindowIsDialogInstance(Window candidateWindow);

        /// <inheritdoc />
        public virtual void Dismiss()
        {
            _dialogWindow.ForceClose();
            WaitForWindowToDisappear();
        }

        public event EventHandler DialogDismissed;

        #endregion

        #region IDisposable Members
        /// <inheritdoc />
        public virtual void Dispose()
        {
            if (_dialogWindow != null)
                _dialogWindow.Dispose();
        }

        #endregion

        protected void OnDialogDismissed(EventArgs e)
        {
            if (DialogDismissed != null)
            {
                DialogDismissed(this, e);
            }
        }

        protected void WaitForWindowToDisappear()
        {
            if (TryFuncUntilTimeOut.Try<bool>(TimeSpan.FromSeconds(10), () => { return !_dialogWindow.Exists; }))
            {
                OnDialogDismissed(new EventArgs());
            }
        }
    }
}
