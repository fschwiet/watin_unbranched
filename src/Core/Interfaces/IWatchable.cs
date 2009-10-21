using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatiN.Core.Interfaces
{
    public interface IWatchable : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether the watchable object exists.
        /// </summary>
        bool Exists { get; }

        /// <summary>
        /// Performs the default action for a watchable object, such as canceling a dialog.
        /// </summary>
        void DoDefaultAction();
    }
}
