using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core.Native;
using WatiN.Core.Interfaces;
using WatiN.Core.WatchableObjects;

namespace WatiN.Core
{
    public class DialogWatcher : IWatcher
    {
        #region Private members
        private Dictionary<Type, object> handlers;
        private Dictionary<Type, object> pendingExpectations;
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
                handlers = new Dictionary<Type, object>();

            WatchableObjectHandler<TWatchable> handler = GetExistingHandler<TWatchable>();
            if (handler == null)
            {
                handlers.Add(typeof(TWatchable), new WatchableObjectHandler<TWatchable>(action));
            }
        }

        /// <inheritdoc/>
        public void ClearHandler<TWatchable>() where TWatchable : IWatchable
        {
            WatchableObjectHandler<TWatchable> existingHandler = GetExistingHandler<TWatchable>();
            if (existingHandler != null)
            {
                handlers.Remove(typeof(TWatchable));
            }
        }

        /// <inheritdoc/>
        public void ResetHandler<TWatchable>() where TWatchable : IWatchable
        {
            WatchableObjectHandler<TWatchable> existingHandler = GetExistingHandler<TWatchable>();
            if (existingHandler != null)
            {
                existingHandler.HandleCount = 0;
                existingHandler.Enabled = true;
            }
        }

        /// <inheritdoc/>
        public Expectation<TWatchable> Expect<TWatchable>() where TWatchable : IWatchable
        {
            return Expect<TWatchable>(Expectation<TWatchable>.DefaultTimeout, null);
        }

        /// <inheritdoc/>
        public Expectation<TWatchable> Expect<TWatchable>(int timeout) where TWatchable : IWatchable
        {
            return Expect<TWatchable>(timeout, null);
        }

        /// <inheritdoc/>
        public Expectation<TWatchable> Expect<TWatchable>(Predicate<TWatchable> predicate) where TWatchable : IWatchable
        {
            return Expect<TWatchable>(Expectation<TWatchable>.DefaultTimeout, predicate);
        }

        /// <inheritdoc/>
        public Expectation<TWatchable> Expect<TWatchable>(int timeout, Predicate<TWatchable> predicate) where TWatchable : IWatchable
        {
            if (pendingExpectations == null)
                pendingExpectations = new Dictionary<Type, object>();

            // Expectations, by definition, take precedence over handlers. If we set an
            // expectation for a given dialog type, we must disable the handler for the
            // dialog type, if any has been defined. At present, it is up to the user to
            // re-enable the dialog handler by calling ResetHandler().
            Expectation<TWatchable> expectation = GetExistingExpectation<TWatchable>();
            if (expectation == null)
            {
                expectation = new Expectation<TWatchable>(timeout);
                pendingExpectations.Add(typeof(TWatchable), expectation);
                WatchableObjectHandler<TWatchable> existingHandler = GetExistingHandler<TWatchable>();
                if (existingHandler != null)
                {
                    existingHandler.Enabled = false;
                }
            }
            return expectation;
        }
        #endregion

        private void manager_DialogFound(object sender, NativeDialogFoundEventArgs e)
        {
            switch (e.NativeDialog.Kind)
            {
                case "AlertDialog":
                    HandleDialogOrFulfillExpectation<AlertDialog>(new AlertDialog(e.NativeDialog));
                    break;
                case "ConfirmDialog":
                    HandleDialogOrFulfillExpectation<ConfirmDialog>(new ConfirmDialog(e.NativeDialog));
                    break;
                default:
                    break;
            }
        }

        private void HandleDialogOrFulfillExpectation<TWatchable>(TWatchable watchableObject) where TWatchable : IWatchable
        {
            Expectation<TWatchable> existingExpectation = GetExistingExpectation<TWatchable>();
            WatchableObjectHandler<TWatchable> existingHandler = GetExistingHandler<TWatchable>();
            if (existingExpectation != null)
            {
                // Once the expectation is met, we can remove it from the list of
                // pending expectations.
                existingExpectation.SetObject(watchableObject);
                pendingExpectations.Remove(typeof(TWatchable));
            }
            else if (existingHandler != null && existingHandler.Enabled)
            {
                existingHandler.HandleObject(watchableObject);
                existingHandler.HandleCount++;
                if (existingHandler.HandleOnce)
                {
                    handlers.Remove(typeof(TWatchable));
                }
            }
        }

        private WatchableObjectHandler<TWatchable> GetExistingHandler<TWatchable>() where TWatchable : IWatchable
        {
            WatchableObjectHandler<TWatchable> existingHandler = null;
            if (handlers != null && handlers.ContainsKey(typeof(TWatchable)))
            {
                existingHandler = handlers[typeof(TWatchable)] as WatchableObjectHandler<TWatchable>;
            }
            return existingHandler;
        }

        private Expectation<TWatchable> GetExistingExpectation<TWatchable>() where TWatchable : IWatchable
        {
            Expectation<TWatchable> existingExpectation = null;
            if (pendingExpectations != null && pendingExpectations.ContainsKey(typeof(TWatchable)))
            {
                existingExpectation = pendingExpectations[typeof(TWatchable)] as Expectation<TWatchable>;
            }
            return existingExpectation;
        }
    }
}
