using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using WatiN.Core.Logging;

namespace WatiN.Core.Mozilla
{
    /// <summary>
    /// Wrapper around the XUL:browser class, see: http://developer.mozilla.org/en/docs/XUL:browser
    /// </summary>
    internal class XULBrowser
    {
        #region Private fields

        private readonly FireFoxClientPort clientPort;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="XULBrowser"/> class.
        /// </summary>
        /// <param name="clientPort">The client port.</param>
        public XULBrowser(FireFoxClientPort clientPort)
        {
            this.clientPort = clientPort;
        }

        #endregion

        #region Public instance properties

        /// <summary>
        /// Gets the client port used to communicate with the instance of FireFox.
        /// </summary>
        /// <value>The client port.</value>
        public FireFoxClientPort ClientPort
        {
            get { return clientPort; }
        }

        public IntPtr Handle
        {
            get { return this.ClientPort.Process.MainWindowHandle; }
        }

        public int ProcessId
        {
            get { return this.ClientPort.Process.Id; }
        }

        #endregion

        #region Public instance methods

        /// <summary>
        /// Navigates to the previous page in the browser history.
        /// </summary>
        public void Back()
        {
            this.ClientPort.Write(string.Format("{0}.goBack();", FireFoxClientPort.BrowserVariableName));
            WaitForComplete();
        }

        public void BringToFront()
        {
            bool result = NativeMethods.SetForegroundWindow(this.Handle);
            
            if (!result)
            {
                Logger.LogAction("Failed to set Firefox as the foreground window.");
            }
        }

        /// <summary>
        /// Navigates to the next back in the browser history.
        /// </summary>
        public void Forward()
        {
            this.ClientPort.Write(string.Format("{0}.goForward();", FireFoxClientPort.BrowserVariableName));
            WaitForComplete();
        }

        /// <summary>
        /// Load a URL into the document. see: http://developer.mozilla.org/en/docs/XUL:browser#m-loadURI
        /// </summary>
        /// <param name="url">The URL to laod.</param>
        public void LoadUri(Uri url)
        {
        	this.ClientPort.Write(string.Format("{0}.loadURI(\"{1}\");", FireFoxClientPort.BrowserVariableName, url));
            WaitForComplete();
       }

        /// <summary>
        /// Reloads this instance.
        /// </summary>
        public void Reload()
        {
            this.ClientPort.Write(string.Format("{0}.reload();", FireFoxClientPort.BrowserVariableName));
            WaitForComplete();
        }

        /// <summary>
        /// Waits until the document is loaded
        /// </summary>
        public void WaitForComplete()
        {
            WaitForComplete(this.ClientPort);
        }
        #endregion

        #region Internal static methods

        /// <summary>
        /// Waits until the document, associated with the <param name="clientPort" /> is loaded.
        /// </summary>
        internal static void WaitForComplete(FireFoxClientPort clientPort)
        {
            string command = string.Format("{0}.webProgress.isLoadingDocument;", FireFoxClientPort.BrowserVariableName);
            clientPort.Write(command);

            while (clientPort.LastResponse == "true")
            {
                Thread.Sleep(200);
                command = string.Format("{0}.webProgress.isLoadingDocument;", FireFoxClientPort.BrowserVariableName);
                clientPort.Write(command);
            }

            clientPort.InitializeDocument();
        }

        #endregion
    }
}
