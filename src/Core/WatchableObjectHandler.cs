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
            set { timesHandled = value; }
        }

        public abstract void HandleObject(object objectToHandle);
    }

    public class WatchableObjectHandler<TWatchable> : WatchableObjectHandler where TWatchable : IWatchable
    {
        private Action<TWatchable> _handlerAction;

        public WatchableObjectHandler(Action<TWatchable> handlerAction)
        {
            _handlerAction = handlerAction;
        }

        public override void HandleObject(object objectToHandle)
        {
            IWatchable watchableObject = objectToHandle as IWatchable;
            if (objectToHandle != null)
            {
                HandleObjectInternal((TWatchable)objectToHandle);
            }
        }

        private void HandleObjectInternal(TWatchable objectToHandle)
        {
            _handlerAction(objectToHandle);
        }
    }
}
