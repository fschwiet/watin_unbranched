using System;
using System.Collections.Generic;
using System.Text;
using WatiN.Core.Interfaces;

namespace WatiN.Core.WatchableObjects
{
    public class WatchableObjectHandler<TWatchable> where TWatchable : IWatchable
    {
        private Action<TWatchable> _handlerAction;
        private bool handleObjectOnce = false;
        private bool handlerEnabled = true;
        private int timesHandled = 0;

        public WatchableObjectHandler(Action<TWatchable> handlerAction)
        {
            _handlerAction = handlerAction;
        }

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

        public void HandleObject(TWatchable objectToHandle)
        {
            _handlerAction(objectToHandle);
        }
    }
}
