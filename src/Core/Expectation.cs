using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using WatiN.Core.Interfaces;
using WatiN.Core.UtilityClasses;
using WatiN.Core.Logging;

namespace WatiN.Core
{
    public abstract class Expectation : IDisposable
    {
        public abstract bool IsSatisfied { get; }
        public abstract bool TimeoutReached { get; }
        internal abstract void SetObject(object objectToSet);

        #region IDisposable Members
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

        protected virtual void Dispose(bool disposing)
        {
        }
    }

    public class Expectation<TWatchable> : Expectation where TWatchable : IWatchable
    {
        public static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(30);

        TWatchable expectedObject = default(TWatchable);
        TimeSpan expectedTimeout;
        bool timedOut = false;
        bool expectationSatisfied = false;
        bool isDisposed = false;
        Predicate<TWatchable> criteriaMatched = new Predicate<TWatchable>(expectedObject => expectedObject.Exists);

        public Expectation()
            : this(DefaultTimeout)
        {
        }

        public Expectation(TimeSpan timeout)
            : this(timeout, null)
        {
        }

        public Expectation(Predicate<TWatchable> criteria)
            : this(DefaultTimeout, criteria)
        {
        }

        public Expectation(TimeSpan timeout, Predicate<TWatchable> predicate)
        {
            if (timeout.TotalSeconds == 0)
            {
                timeout = DefaultTimeout;
            }

            expectedTimeout = timeout;
            if (predicate != null)
            {
                criteriaMatched = predicate;
            }
        }

        public override bool IsSatisfied
        {
            get { return expectationSatisfied; }
        }

        public override bool TimeoutReached
        {
            get { return timedOut; }
        }

        public TWatchable Object
        {
            get
            {
                WaitUntilSatisfied();
                return expectedObject; 
            }
        }

        public void WaitUntilSatisfied()
        {
            TryFuncUntilTimeOut functionExecutor = new TryFuncUntilTimeOut(expectedTimeout);
            expectationSatisfied = functionExecutor.Try<bool>(IsExpectationSatisfied);
            timedOut = functionExecutor.DidTimeOut;
        }

        public void Reset()
        {
            expectedObject = default(TWatchable);
            expectationSatisfied = false;
            timedOut = false;
        }

        internal override void SetObject(object objectToSet)
        {
            // Verify the object is an IWatchable before setting.
            IWatchable watchableObject = objectToSet as IWatchable;
            if (watchableObject != null)
            {
                SetObjectInternal((TWatchable)objectToSet);
            }
        }

        private void SetObjectInternal(TWatchable objectToSet)
        {
            expectedObject = objectToSet;
        }

        private bool IsExpectationSatisfied()
        {
            return expectedObject != null && !expectedObject.Equals(default(TWatchable)) && criteriaMatched(expectedObject);
        }

        protected override void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    if (expectedObject != null && !expectedObject.Equals(default(TWatchable)))
                    {
                        expectedObject.Dispose();
                    }
                    expectedObject = default(TWatchable);
                    isDisposed = true;
                }
            }
            base.Dispose(disposing);
        }
    }
}
