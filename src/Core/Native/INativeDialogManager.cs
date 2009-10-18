using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core.Native;

namespace WatiN.Core
{
    public interface INativeDialogManager : IDisposable
    {
        /// <summary>
        /// Event raised when a dialog is found matching one of the types of dialogs registered with the dialog manager.
        /// </summary>
        event EventHandler<NativeDialogFoundEventArgs> DialogFound;
    }
}
