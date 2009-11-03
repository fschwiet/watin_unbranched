using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace WatiN.Core.Interfaces
{
    public interface IWatcher
    {
        /// <summary>
        /// Gets a value indicating the total number of handlers the watcher is tracking.
        /// </summary>
        int TotalHandlerCount { get; }

        /// <summary>
        /// Gets a list of types handled by the current watcher. This only returns handled types where the handler is enabled.
        /// </summary>
        ReadOnlyCollection<Type> ActivelyHandledTypes { get; }

        /// <summary>
        /// Sets a handler for a type of watchable object (e.g., dialog, infobar, etc.).
        /// </summary>
        /// <typeparam name="TWatchable">An object implementing the <see cref="WatiN.Core.Interfaces.IWatchable"/> interface.</typeparam>
        /// <param name="action">An <see cref="System.Action&lt;T&gt;"/> delegate to handle the object.</param>
        void SetHandler<TWatchable>(Action<TWatchable> action) where TWatchable : IWatchable;

        /// <summary>
        /// Clears the handler for the given watchable object type (e.g., dialog, infobar, etc.).
        /// </summary>
        /// <typeparam name="TWatchable">An object implementing the <see cref="WatiN.Core.Interfaces.IWatchable"/> interface.</typeparam>
        void ClearHandler<TWatchable>() where TWatchable : IWatchable;

        /// <summary>
        /// Resets the handler for the given watchable object type (e.g., dialog, infobar, etc.).
        /// </summary>
        /// <typeparam name="TWatchable">An object implementing the <see cref="WatiN.Core.Interfaces.IWatchable"/> interface.</typeparam>
        /// <remarks>ResetHandler will also remove any unmet Expectations for the given dialog type.</remarks>
        void ResetHandler<TWatchable>() where TWatchable : IWatchable;

        /// <summary>
        /// Gets the handler for a type of watchable object (e.g., dialog, infobar, etc.).
        /// </summary>
        /// <typeparam name="TWatchable">An object implementing the <see cref="WatiN.Core.Interfaces.IWatchable"/> interface.</typeparam>
        /// <returns>The <see cref="WatchableObjectHandler&lt;TWatchable&gt;"/> object that handles the object type.</returns>
        WatchableObjectHandler<TWatchable> GetHandler<TWatchable>() where TWatchable : IWatchable;

        /// <summary>
        /// Sets an expectation for a watchable object (e.g., dialog, infobar, etc.) to appear.
        /// </summary>
        /// <typeparam name="TWatchable">An object implementing the <see cref="WatiN.Core.Interfaces.IWatchable"/> interface.</typeparam>
        /// <returns>An <see cref="WatiN.Core.Expectation&lt;TWatchable&gt;"/> object the user can use to manipulate the object.</returns>
        /// <remarks>Expecting a watchable object to appear will suspend processing of any registered handlers for
        /// that object type. To resume automatic handling, you must call ResetHandler for the object type.</remarks>
        Expectation<TWatchable> Expect<TWatchable>() where TWatchable : IWatchable;

        /// <summary>
        /// Sets an expectation for a watchable object (e.g., dialog, infobar, etc.) to appear.
        /// </summary>
        /// <typeparam name="TWatchable">An object implementing the <see cref="WatiN.Core.Interfaces.IWatchable"/> interface.</typeparam>
        /// <param name="timeout">A <see cref="System.TimeSpan"/> structure representing the time within which the expectation should be filled.</param>
        /// <returns>An <see cref="WatiN.Core.Expectation&lt;TWatchable&gt;"/> object the user can use to manipulate the object.</returns>
        /// <remarks>Expecting a watchable object to appear will suspend processing of any registered handlers for
        /// that object type. To resume automatic handling, you must call ResetHandler for the object type.</remarks>
        Expectation<TWatchable> Expect<TWatchable>(TimeSpan timeout) where TWatchable : IWatchable;

        /// <summary>
        /// Sets an expectation for a watchable object (e.g., dialog, infobar, etc.) to appear.
        /// </summary>
        /// <typeparam name="TWatchable">An object implementing the <see cref="WatiN.Core.Interfaces.IWatchable"/> interface.</typeparam>
        /// <param name="predicate">A <see cref="System.Predicate&lt;T&gt;"/> defining the criteria for the expectation.</param>
        /// <returns>An <see cref="WatiN.Core.Expectation&lt;TWatchable&gt;"/> object the user can use to manipulate the object.</returns>
        /// <remarks>Expecting a watchable object to appear will suspend processing of any registered handlers for
        /// that object type. To resume automatic handling, you must call ResetHandler for the object type.</remarks>
        Expectation<TWatchable> Expect<TWatchable>(Predicate<TWatchable> predicate) where TWatchable : IWatchable;

        /// <summary>
        /// Sets an expectation for a watchable object (e.g., dialog, infobar, etc.) to appear.
        /// </summary>
        /// <typeparam name="TWatchable">An object implementing the <see cref="WatiN.Core.Interfaces.IWatchable"/> interface.</typeparam>
        /// <param name="timeout">A <see cref="System.TimeSpan"/> structure representing the time within which the expectation should be filled.</param>
        /// <param name="predicate">A <see cref="System.Predicate&lt;T&gt;"/> defining the criteria for the expectation.</param>
        /// <returns>An <see cref="WatiN.Core.Expectation&lt;TWatchable&gt;"/> object the user can use to manipulate the object.</returns>
        /// <remarks>Expecting a watchable object to appear will suspend processing of any registered handlers for
        /// that object type. To resume automatic handling, you must call ResetHandler for the object type.</remarks>
        Expectation<TWatchable> Expect<TWatchable>(TimeSpan timeout, Predicate<TWatchable> predicate) where TWatchable : IWatchable;
    }
}
