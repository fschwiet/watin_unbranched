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
        private Dictionary<Type, Action<IWatchable>> handlers;
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
                handlers = new Dictionary<Type, Action<IWatchable>>();

            Action<IWatchable> existingHandler;
            if (!handlers.TryGetValue(typeof(TWatchable), out existingHandler))
                handlers.Add(typeof(TWatchable), o => action((TWatchable)o));
        }

        /// <inheritdoc/>
        public void ClearHandler<TWatchable>()
        {
            if (handlers.ContainsKey(typeof(TWatchable)))
                handlers.Remove(typeof(TWatchable));
        }

        /// <inheritdoc/>
        public void ResetHandler<TWatchable>()
        {
            throw new NotImplementedException();
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

            Expectation<TWatchable> expectation = new Expectation<TWatchable>(timeout, predicate);
            object pendingExpectation;
            if (!pendingExpectations.TryGetValue(typeof(TWatchable), out pendingExpectation))
                pendingExpectations.Add(typeof(TWatchable), expectation);
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
            object expectation;
            Action<IWatchable> handler;
            if (pendingExpectations != null && pendingExpectations.TryGetValue(typeof(TWatchable), out expectation))
            {
                //If we have a pending expectation for the type of watchable object, set the expectation's
                //object, and remove the expectation from the pending list so it does not get triggered again.
                Expectation<TWatchable> pendingExpectation = expectation as Expectation<TWatchable>;
                if (pendingExpectation != null)
                {
                    pendingExpectation.SetObject(watchableObject);
                    pendingExpectations.Remove(typeof(TWatchable));
                }
            }
            else if (handlers != null && handlers.TryGetValue(typeof(TWatchable), out handler))
            {
                handler(watchableObject);
            }
        }
    }
}
