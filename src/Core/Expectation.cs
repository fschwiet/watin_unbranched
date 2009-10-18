using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using WatiN.Core.Interfaces;

namespace WatiN.Core
{
    public class Expectation<TWatchable> where TWatchable : IWatchable
    {
        public static readonly int DefaultTimeout = 30;

        TWatchable expectedObject = default(TWatchable);
        int expectedTimeout;
        Stopwatch timer = new Stopwatch();
        Predicate<TWatchable> criteriaMatched = new Predicate<TWatchable>(expectedObject => expectedObject.Exists);

        public Expectation()
            : this(DefaultTimeout)
        {
        }

        public Expectation(int timeout)
            : this(timeout, null)
        {
        }

        public Expectation(Predicate<TWatchable> criteria)
            : this(DefaultTimeout, criteria)
        {
        }

        public Expectation(int timeout, Predicate<TWatchable> predicate)
        {
            expectedTimeout = timeout;
            if (predicate != null)
            {
                criteriaMatched = predicate;
            }
            timer.Start();
        }

        public bool IsSatisfied
        {
            get
            {
                return expectedObject != null && !expectedObject.Equals(default(TWatchable)) && criteriaMatched(expectedObject);
            }
        }

        public bool TimeoutReached
        {
            get
            {
                return timer.Elapsed.TotalSeconds > expectedTimeout;
            }
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
            while (!IsSatisfied && !TimeoutReached)
            {
                System.Threading.Thread.Sleep(200);
            }
            timer.Stop();
        }

        public void Reset()
        {
            expectedObject = default(TWatchable);
            timer.Reset();
        }

        internal void SetObject(TWatchable objectToSet)
        {
            expectedObject = objectToSet;
        }
    }
}
