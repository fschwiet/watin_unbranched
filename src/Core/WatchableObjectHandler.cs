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

        public bool HandleOnce
        {
            get { return handleObjectOnce; }
            set { handleObjectOnce = value; }
        }

        public bool Enabled
        {
            get { return handlerEnabled; }
            set { handlerEnabled = value; }
        }

        public int HandleCount
        {
            get { return timesHandled; }
            protected set { timesHandled = value; }
        }

        public abstract void HandleObject(object objectToHandle);

        public void Reset()
        {
            handlerEnabled = true;
            timesHandled = 0;
        }
    }

    public class WatchableObjectHandler<TWatchable> : WatchableObjectHandler where TWatchable : IWatchable
    {
        private Action<TWatchable> _handlerAction;
        private bool autoDisposeObject = true;

        public WatchableObjectHandler(Action<TWatchable> handlerAction)
            : this(handlerAction, true)
        {
        }

        public WatchableObjectHandler(Action<TWatchable> handlerAction, bool automaticallyDisposeWatchedObject)
        {
            _handlerAction = handlerAction;
            autoDisposeObject = automaticallyDisposeWatchedObject;
        }

        public override void HandleObject(object objectToHandle)
        {
            // Verify the object is an IWatchable before setting.
            IWatchable watchableObject = objectToHandle as IWatchable;
            if (watchableObject != null)
            {
                // Be sure to catch any exceptions in the user-defined handler code.
                TWatchable castObject = (TWatchable)objectToHandle;
                try
                {
                    HandleObjectInternal((TWatchable)castObject);
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
