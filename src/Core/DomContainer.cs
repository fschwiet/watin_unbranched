#region WatiN Copyright (C) 2006-2008 Jeroen van Menen

//Copyright 2006-2008 Jeroen van Menen
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
using WatiN.Core.DialogHandlers;
using WatiN.Core.Interfaces;

namespace WatiN.Core
{
	/// <summary>
	/// This class hosts functionality for classes which are an entry point
	/// to a document and its elements and/or frames. Currently implemented
	/// by IE and HTMLDialog.
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

		public abstract IntPtr hWnd { get; }

		/// <summary>
		/// Gets the process ID the Internet Explorer or HTMLDialog is running in.
		/// </summary>
		/// <value>The process ID.</value>
		public int ProcessID
		{
			get
			{
				int iePid;

				NativeMethods.GetWindowThreadProcessId(hWnd, out iePid);

				return iePid;
			}
		}

		/// <summary>
		/// This method must be overriden by all sub classes
		/// </summary>
		public abstract INativeDocument OnGetNativeDocument();

		/// <summary>
		/// Returns the 'raw' html document for the internet explorer DOM.
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
		/// Call this function (from a subclass) as soon as the process is started.
		/// </summary>
		protected void StartDialogWatcher()
		{
		    if (!Settings.AutoStartDialogWatcher || DialogWatcher != null) return;
		    
            DialogWatcher = DialogWatcher.GetDialogWatcherForProcess(ProcessID);
		    DialogWatcher.IncreaseReferenceCount();
		}

	    /// <summary>
	    /// Gets the dialog watcher.
	    /// </summary>
	    /// <value>The dialog watcher.</value>
	    public DialogWatcher DialogWatcher { get; private set; }

	    public abstract INativeBrowser NativeBrowser
		{
			get;
		}

		/// <summary>
		/// Adds the dialog handler.
		/// </summary>
		/// <param name="handler">The dialog handler.</param>
		public void AddDialogHandler(IDialogHandler handler)
		{
			DialogWatcher.Add(handler);
		}

		/// <summary>
		/// Removes the dialog handler.
		/// </summary>
		/// <param name="handler">The dialog handler.</param>
		public void RemoveDialogHandler(IDialogHandler handler)
		{
			DialogWatcher.Remove(handler);
		}

		/// <summary>
		/// This method must be called by its inheritor to dispose references
		/// to internal resources.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
		    if (IsDisposed) return;
		    
            _nativeDocument = null;
		    if (DialogWatcher != null)
		    {
		        DialogWatcher.DecreaseReferenceCount();
		        DialogWatcher = null;
		    }
		    IsDisposed = true;

		    base.Dispose(true);
		}

		/// <summary>
		/// Waits for the page to be completely loaded using the Settings.WaitForCompleteTimeOut setting
		/// </summary>
		public virtual void WaitForComplete()
		{
			WaitForComplete(new WaitForComplete(this));
		}

		/// <summary>
        /// Waits for the page to be completely loaded.
		/// </summary>
		/// <param name="waitForCompleteTimeOut">The number of seconds to wait before timing out</param>
		public virtual void WaitForComplete(int waitForCompleteTimeOut)
		{
			WaitForComplete(new WaitForComplete(this, waitForCompleteTimeOut));
		}

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
		/// If you want more controle over the output, use <seealso cref="CaptureWebPage.CaptureWebPageToFile(string, bool, bool, int, int)"/>
		/// </summary>
		/// <param name="filename">The filename.</param>
        public void CaptureWebPageToFile(string filename)
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
	}
}