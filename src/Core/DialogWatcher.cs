using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core.Dialogs;
using WatiN.Core.Interfaces;
using WatiN.Core.Native;
using WatiN.Core.Logging;

namespace WatiN.Core
{
    public class DialogWatcher : IWatcher
    {
        #region Private members
        private Dictionary<Type, WatchableObjectHandler> handlers;
        private Dictionary<Type, Expectation> pendingExpectations;
        #endregion

        internal DialogWatcher(INativeDialogManager manager, Type watchableType)
        {
            manager.DialogFound += new EventHandler<NativeDialogFoundEventArgs>(manager_DialogFound);
        }

        #region IWatcher Members
        /// <inheritdoc/>
        public void SetHandler<TWatchable>(Action<TWatchable> action) where TWatchable : IWatchable
        {
            if (handlers == null)
                handlers = new Dictionary<Type, WatchableObjectHandler>();

            WatchableObjectHandler handler = GetExistingHandler(typeof(TWatchable));
            if (handler == null)
            {
                handlers.Add(typeof(TWatchable), new WatchableObjectHandler<TWatchable>(action));
            }
        }

        /// <inheritdoc/>
        public void ClearHandler<TWatchable>() where TWatchable : IWatchable
        {
            WatchableObjectHandler existingHandler = GetExistingHandler(typeof(TWatchable));
            if (existingHandler != null)
            {
                handlers.Remove(typeof(TWatchable));
            }
        }

        /// <inheritdoc/>
        public void ResetHandler<TWatchable>() where TWatchable : IWatchable
        {
            WatchableObjectHandler existingHandler = GetExistingHandler(typeof(TWatchable));
            if (existingHandler != null)
            {
                existingHandler.Reset();
            }
        }

        /// <inheritdoc/>
        public Expectation<TWatchable> Expect<TWatchable>() where TWatchable : IWatchable
        {
            return Expect<TWatchable>(Expectation<TWatchable>.DefaultTimeout, null);
        }

        /// <inheritdoc/>
        public Expectation<TWatchable> Expect<TWatchable>(TimeSpan timeout) where TWatchable : IWatchable
        {
            return Expect<TWatchable>(timeout, null);
        }

        /// <inheritdoc/>
        public Expectation<TWatchable> Expect<TWatchable>(Predicate<TWatchable> predicate) where TWatchable : IWatchable
        {
            return Expect<TWatchable>(Expectation<TWatchable>.DefaultTimeout, predicate);
        }

        /// <inheritdoc/>
        public Expectation<TWatchable> Expect<TWatchable>(TimeSpan timeout, Predicate<TWatchable> predicate) where TWatchable : IWatchable
        {
            if (pendingExpectations == null)
                pendingExpectations = new Dictionary<Type, Expectation>();
            
            // Expectations, by definition, take precedence over handlers. If we set an
            // expectation for a given dialog type, we must disable the handler for the
            // dialog type, if any has been defined. At present, it is up to the user to
            // re-enable the dialog handler by calling ResetHandler().
            Expectation<TWatchable> expectation = GetExistingExpectation(typeof(TWatchable)) as Expectation<TWatchable>;
            if (expectation == null)
            {
                expectation = new Expectation<TWatchable>(timeout, predicate);
                pendingExpectations.Add(typeof(TWatchable), expectation);
                WatchableObjectHandler existingHandler = GetExistingHandler(typeof(TWatchable));
                if (existingHandler != null)
                {
                    Logger.LogInfo("Handler for watchable type {0} disabled and Expectation set", typeof(TWatchable).Name);
                    existingHandler.Enabled = false;
                }
            }
            return expectation;
        }
        #endregion

        private void manager_DialogFound(object sender, NativeDialogFoundEventArgs e)
        {
            Dialog dialogInstance = DialogFactory.CreateDialog(e.NativeDialog);
            HandleDialogOrFulfillExpectation(dialogInstance);
        }

        private void HandleDialogOrFulfillExpectation(Dialog watchableObject)
        {
            Type dialogType = watchableObject.GetType();
            Expectation existingExpectation = GetExistingExpectation(dialogType);
            WatchableObjectHandler existingHandler = GetExistingHandler(dialogType);
            if (existingExpectation != null)
            {
                // Once the expectation is met, we can remove it from the list of
                // pending expectations.
                Logger.LogAction("{0} dialog found meeting expectation", watchableObject.NativeDialog.Kind);
                existingExpectation.SetObject(watchableObject);
                pendingExpectations.Remove(dialogType);
            }
            else if (existingHandler != null && existingHandler.Enabled)
            {
                // Handle the dialog with the handler. N.B. WatchableObjectHandler will
                // dispose of the IWatchable object by default. It also will catch any
                // exceptions from poorly written handler code.
                Logger.LogAction("Handling {0} dialog with handler", watchableObject.NativeDialog.Kind);
                existingHandler.HandleObject(watchableObject);
                if (existingHandler.HandleOnce)
                {
                    handlers.Remove(dialogType);
                }
            }
        }

        private WatchableObjectHandler GetExistingHandler(Type dialogType)
        {
            WatchableObjectHandler existingHandler = null;
            if (handlers != null && handlers.ContainsKey(dialogType))
            {
                existingHandler = handlers[dialogType];
            }
            return existingHandler;
        }

        private Expectation GetExistingExpectation(Type dialogType)
        {
            Expectation existingExpectation = null;
            if (pendingExpectations != null && pendingExpectations.ContainsKey(dialogType))
            {
                existingExpectation = pendingExpectations[dialogType];
            }
            return existingExpectation;
        }
    }
}
