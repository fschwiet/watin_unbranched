using System;
using System.Collections.Generic;
using System.Text;
using WatiN.Core.Interfaces;
using WatiN.Core.Logging;

namespace WatiN.Core
{
    public abstract class WatchableObjectHandler
    {
        private bool handleObjectOnce = false;
        private bool handlerEnabled = true;
        private int timesHandled = 0;

        protected WatchableObjectHandler()
        {
        }

        internal bool HandleOnce
        {
            get { return handleObjectOnce; }
            set { handleObjectOnce = value; }
        }

        internal bool Enabled
        {
            get { return handlerEnabled; }
            set { handlerEnabled = value; }
        }

        internal int HandleCount
        {
            get { return timesHandled; }
            set { timesHandled = value; }
        }

        internal abstract void HandleObject(object objectToHandle);

        internal void Reset()
        {
            handlerEnabled = true;
            timesHandled = 0;
        }
    }

    public sealed class WatchableObjectHandler<TWatchable> : WatchableObjectHandler where TWatchable : IWatchable
    {
        private Action<TWatchable> _handlerAction;
        private bool autoDisposeObject = true;

        internal WatchableObjectHandler(Action<TWatchable> handlerAction)
            : this(handlerAction, true)
        {
        }

        internal WatchableObjectHandler(Action<TWatchable> handlerAction, bool automaticallyDisposeWatchedObject)
        {
            _handlerAction = handlerAction;
            autoDisposeObject = automaticallyDisposeWatchedObject;
        }

        internal Action<TWatchable> HandlerAction
        {
            get { return _handlerAction; }
        }

        internal override void HandleObject(object objectToHandle)
        {
            // Verify the object is an IWatchable before setting.
            IWatchable watchableObject = objectToHandle as IWatchable;
            if (watchableObject != null)
            {
                // Be sure to catch any exceptions in the user-defined handler code.
                TWatchable castObject = (TWatchable)objectToHandle;
                try
                {
                    HandleObjectInternal(castObject);
                    HandleCount++;
                }
                catch (Exception ex)
                {
                    Logger.LogInfo("Unexpected exception handling object: {0}", ex.Message);
                }
                finally
                {
                    if (autoDisposeObject)
                    {
                        castObject.Dispose();
                    }
                }
            }
        }

        private void HandleObjectInternal(TWatchable objectToHandle)
        {
            _handlerAction(objectToHandle);
        }
    }
}
