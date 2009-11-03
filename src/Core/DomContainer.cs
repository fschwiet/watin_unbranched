#region WatiN Copyright (C) 2006-2009 Jeroen van Menen

//Copyright 2006-2009 Jeroen van Menen
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

#endregion Copyright

using System;
using WatiN.Core.Interfaces;
using WatiN.Core.Native;
using WatiN.Core.Native.Windows;
using WatiN.Core.UtilityClasses;
using System.Collections.Generic;
using WatiN.Core.Dialogs;

namespace WatiN.Core
{
	/// <summary>
	/// This class hosts functionality for classes which are an entry point
	/// to a document and its elements and/or frames.
	/// </summary>
	public abstract class DomContainer : Document
	{
		private INativeDocument _nativeDocument;
        private Dictionary<Type, IWatcher> _watchers;

        /// <summary>
        /// <c>true</c> if the <see cref="Dispose"/> method has been called to release resources.
        /// </summary>
        protected bool IsDisposed { get; set; }

	    protected DomContainer()
		{
			DomContainer = this;
		}

        /// <summary>
        /// Gets the OS window hosting the container.
        /// </summary>
        public abstract Window HostWindow { get; }

        /// <summary>
        /// Gets the native document.
        /// </summary>
        /// <returns>The native document.</returns>
        public abstract INativeDocument OnGetNativeDocument();

		/// <summary>
		/// Returns a browser specific <see cref="INativeDocument"/> instance.
		/// </summary>
		public override INativeDocument NativeDocument
		{
			get
			{
                if (_nativeDocument == null)
                {
                    _nativeDocument = OnGetNativeDocument();
                }

				return _nativeDocument;
			}
		}

		/// <summary>
		/// This method must be called by its inheritor to dispose references
		/// to internal resources.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
		    if (IsDisposed) return;
		    
            _nativeDocument = null;
		    IsDisposed = true;

		    base.Dispose(true);
		}

		/// <summary>
		/// Waits for the page to be completely loaded using the Settings.WaitForCompleteTimeOut setting
		/// </summary>
		public void WaitForComplete()
		{
            WaitForComplete(Settings.WaitForCompleteTimeOut);
		}

		/// <summary>
        /// Waits for the page to be completely loaded.
		/// </summary>
		/// <param name="waitForCompleteTimeOut">The number of seconds to wait before timing out</param>
		public abstract void WaitForComplete(int waitForCompleteTimeOut);

	    /// <summary>
		/// Waits for the page to be completely loaded
		/// </summary>
		/// <param name="waitForComplete">The wait for complete.</param>
		public void WaitForComplete(IWait waitForComplete)
		{
			waitForComplete.DoWait();
		}

        /// <summary>
		/// Captures the web page to file. The file extension is used to 
		/// determine the image format. The following image formats are
		/// supported (if the encoder is available on the machine):
		/// jpg, tif, gif, png, bmp.
		/// If you want more controle over the output, use &lt;seealso cref="CaptureWebPage.CaptureWebPageToFile(string, bool, bool, int, int)"/&gt;
		/// </summary>
		/// <param name="filename">The filename.</param>
        public virtual void CaptureWebPageToFile(string filename)
		{
            var captureWebPage = new CaptureWebPage(this);
            captureWebPage.CaptureWebPageToFile(filename, false, false, 100, 100);
		}

		/// <summary>
		/// Recycles the DomContainer to its initially created state so that it can be reused.
		/// </summary>
		protected virtual void Recycle()
		{
			Dispose(true);
			DomContainer = this;
			IsDisposed = false;
		}

        /// <summary>
        /// Sets a handler for a type of watchable object (e.g., dialog, infobar, etc.).
        /// </summary>
        /// <typeparam name="TWatchable">An object implementing the <see cref="IWatchable"/> interface.</typeparam>
        /// <param name="action">An <see cref="System.Action&lt;T&gt;"/> delegate to handle the object.</param>
        /// <remarks>Will overwrite any existing handler for the watchable type. To replace the existing
        /// handler, use ReplaceHandler.</remarks>
        public virtual void SetHandler<TWatchable>(Action<TWatchable> action) where TWatchable : IWatchable
        {
            IWatcher watcher = GetWatcher(typeof(TWatchable), true);
            watcher.SetHandler(action);
        }


        /// <summary>
        /// Clears the handler for the given watchable object type (e.g., dialog, infobar, etc.).
        /// </summary>
        /// <typeparam name="TWatchable">An object implementing the <see cref="IWatchable"/> interface.</typeparam>
        public virtual void ClearHandler<TWatchable>() where TWatchable : IWatchable
        {
            IWatcher watcher = GetWatcher(typeof(TWatchable), false);
            if (watcher != null)
            {
                watcher.ClearHandler<TWatchable>();
            }
        }

        /// <summary>
        /// Resumes handling of a given watchable type.
        /// </summary>
        /// <typeparam name="TWatchable">An object implementing the <see cref="IWatchable"/> interface.</typeparam>
        public virtual void ResetHandler<TWatchable>() where TWatchable : IWatchable
        {
            IWatcher watcher = GetWatcher(typeof(TWatchable), false);
            if (watcher != null)
                watcher.ResetHandler<TWatchable>();
        }

        /// <summary>
        /// Replaces the handler for the given watchable type (e.g., dialog, infobar, etc.).
        /// </summary>
        /// <typeparam name="TWatchable">An object implementing the <see cref="IWatchable"/> interface.</typeparam>
        /// <param name="action">An <see cref="System.Action&lt;T&gt;"/> delegate to handle the object.</param>
        /// <returns>The previously used <see cref="System.Action&lt;T&gt;"/> delegate for the object. Returns null 
        /// (Nothing in Visual Basic) if a handler has not been set for the type of object.</returns>
        /// <remarks>This method allows the user to cache the existing handler, then reapply it later. This might
        /// be useful if the user wanted to temporarily replace a default handler, then restore it to operation later.</remarks>
        public virtual Action<TWatchable> ReplaceHandler<TWatchable>(Action<TWatchable> action) where TWatchable : IWatchable
        {
            Action<TWatchable> existingAction = null;
            IWatcher watcher = GetWatcher(typeof(TWatchable), true);
            WatchableObjectHandler<TWatchable> handler = watcher.GetHandler<TWatchable>();
            if (handler != null)
            {
                existingAction = handler.HandlerAction;
                watcher.ClearHandler<TWatchable>();
            }
            watcher.SetHandler<TWatchable>(action);
            return existingAction;
        }

        /// <summary>
        /// Gets a value indicating whether or not a handler is enabled for the given watchable type.
        /// </summary>
        /// <typeparam name="TWatchable">An object implementing the <see cref="IWatchable"/> interface.</typeparam>
        /// <returns>true if a handler for the type exists and is enabled, false otherwise</returns>
        public virtual bool HandlerIsEnabled<TWatchable>() where TWatchable : IWatchable
        {
            bool enabled = false;
            IWatcher watcher = GetWatcher(typeof(TWatchable), false);
            if (watcher != null)
            {
                WatchableObjectHandler<TWatchable> handler = watcher.GetHandler<TWatchable>();
                if (handler != null)
                {
                    enabled = handler.Enabled;
                }
            }
            return enabled;
        }

        /// <summary>
        /// Gets a value indicating the number of times a handler has been executed for the given watchable type.
        /// </summary>
        /// <typeparam name="TWatchable">An object implementing the <see cref="IWatchable"/> interface.</typeparam>
        /// <returns>The number of times the handler for this object type has been executed. Zero if the handler does not exist.</returns>
        public virtual int HandlerExecutedCount<TWatchable>() where TWatchable : IWatchable
        {
            int numberOfTimesExecuted = 0;
            IWatcher watcher = GetWatcher(typeof(TWatchable), false);
            if (watcher != null)
            {
                WatchableObjectHandler<TWatchable> handler = watcher.GetHandler<TWatchable>();
                if (handler != null)
                {
                    numberOfTimesExecuted = handler.HandleCount;
                }
            }
            return numberOfTimesExecuted;
        }

        /// <summary>
        /// Gets the count of handlers for the given base watchable type.
        /// </summary>
        /// <param name="baseWatchableType">A <see cref="System.Type"/> object representing the base type of object (e.g., Dialog, InfoBar, Page, etc.).</param>
        /// <param name="enabledHandlersOnly">true to only count enabled handlers, false to count all handlers.</param>
        /// <returns>The count of handlers.</returns>
        public virtual int GetHandlerCount(Type baseWatchableType, bool enabledHandlersOnly)
        {
            int handlerCount = 0;
            IWatcher watcher = GetWatcher(baseWatchableType, false);
            if (watcher != null)
            {
                if (enabledHandlersOnly)
                {
                    handlerCount = watcher.ActivelyHandledTypes.Count;
                }
                else
                {
                    handlerCount = watcher.TotalHandlerCount;
                }
            }
            return handlerCount;
        }

        /// <summary>
        /// Sets an expectation for a watchable object (e.g., dialog, infobar, etc.) to appear.
        /// </summary>
        /// <typeparam name="TWatchable">An object implementing the <see cref="IWatchable"/> interface.</typeparam>
        /// <returns>An <see cref="Expectation&lt;TWatchable&gt;"/> object the user can use to manipulate the object.</returns>
        public virtual Expectation<TWatchable> Expect<TWatchable>() where TWatchable : IWatchable
        {
            return Expect<TWatchable>(Expectation<TWatchable>.DefaultTimeout);
        }

        /// <summary>
        /// Sets an expectation for a watchable object (e.g., dialog, infobar, etc.) to appear.
        /// </summary>
        /// <typeparam name="TWatchable">An object implementing the <see cref="IWatchable"/> interface.</typeparam>
        /// <param name="timeout">A <see cref="System.TimeSpan"/> structure representing the time within which the expectation should be filled.</param>
        /// <returns>An <see cref="Expectation&lt;TWatchable&gt;"/> object the user can use to manipulate the object.</returns>
        public virtual Expectation<TWatchable> Expect<TWatchable>(TimeSpan timeout) where TWatchable : IWatchable
        {
            IWatcher watcher = GetWatcher(typeof(TWatchable), true);
            return watcher.Expect<TWatchable>(timeout);
        }

        private IWatcher GetWatcher(Type watchableType, bool createIfAbsent)
        {
            if (_watchers == null)
                _watchers = new Dictionary<Type, IWatcher>();

            Type baseWatchableType = GetBaseWatchableType(watchableType);

            IWatcher watcher;
            if (!_watchers.TryGetValue(baseWatchableType, out watcher) && createIfAbsent)
            {
                watcher = CreateWatcher(baseWatchableType);
                _watchers.Add(baseWatchableType, watcher);
            }

            return watcher;
        }

        private Type GetBaseWatchableType(Type watchableType)
        {
            Type baseWatchableType = typeof(object);

            if (typeof(Dialog).IsAssignableFrom(watchableType))
                baseWatchableType = typeof(Dialog);

            //else if (typeof(InfoBar).IsAssignableFrom(watchableType))
            //    baseWatchableType = typeof(InfoBar);

            //else if (typeof(Page).IsAssignableFrom(watchableType))
            //    baseWatchableType = typeof(Page);

            else
                throw new NotSupportedException("Unsupported watcher type.");

            return baseWatchableType;
        }

        protected abstract IWatcher CreateWatcher(Type watchableType);
    }
}
