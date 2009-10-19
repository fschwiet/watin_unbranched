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

namespace WatiN.Core
{
	/// <summary>
	/// This class hosts functionality for classes which are an entry point
	/// to a document and its elements and/or frames.
	/// </summary>
	public abstract class DomContainer : Document
	{
		private INativeDocument _nativeDocument;

        /// <summary>
        /// <c>true</c> if the <see cref="Dispose"/> method has been called to release resources.
        /// </summary>
        protected bool IsDisposed { get; set; }

	    protected DomContainer()
		{
			DomContainer = this;
		}

        public abstract Window HostWindow { get; }
		//public abstract IntPtr hWnd { get; }

        //public abstract int ProcessID { get; }

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
        public abstract void SetHandler<TWatchable>(Action<TWatchable> action) where TWatchable : IWatchable;

        /// <summary>
        /// Clears the handler for the given watchable object type (e.g., dialog, infobar, etc.).
        /// </summary>
        /// <typeparam name="TWatchable">An object implementing the <see cref="IWatchable"/> interface.</typeparam>
        public abstract void ClearHandler<TWatchable>() where TWatchable : IWatchable;

        /// <summary>
        /// Resumes handling of a given watchable type.
        /// </summary>
        /// <typeparam name="TWatchable">An object implementing the <see cref="IWatchable"/> interface.</typeparam>
        public abstract void ResetHandler<TWatchable>() where TWatchable : IWatchable;

        /// <summary>
        /// Sets an expectation for a watchable object (e.g., dialog, infobar, etc.) to appear.
        /// </summary>
        /// <typeparam name="TWatchable">An object implementing the <see cref="IWatchable"/> interface.</typeparam>
        /// <returns>An <see cref="Expectation&lt;TWatchable&gt;"/> object the user can use to manipulate the object.</returns>
        public abstract Expectation<TWatchable> Expect<TWatchable>() where TWatchable : IWatchable;

        /// <summary>
        /// Sets an expectation for a watchable object (e.g., dialog, infobar, etc.) to appear.
        /// </summary>
        /// <typeparam name="TWatchable">An object implementing the <see cref="IWatchable"/> interface.</typeparam>
        /// <param name="timeout">The timeout in seconds within which the expectation should be filled.</param>
        /// <returns>An <see cref="Expectation&lt;TWatchable&gt;"/> object the user can use to manipulate the object.</returns>
        public abstract Expectation<TWatchable> Expect<TWatchable>(int timeout) where TWatchable : IWatchable;
    }
}
