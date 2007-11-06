using System;
using System.Collections.Generic;
using System.Text;

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

        #endregion

        #region Public instance methods

        /// <summary>
        /// Navigates to the previous page in the browser history.
        /// </summary>
        public void Back()
        {
            this.ClientPort.Write(string.Format("{0}.goBack();", FireFoxClientPort.BrowserVariableName));
            this.ClientPort.InitializeDocument();
        }

        /// <summary>
        /// Navigates to the next back in the browser history.
        /// </summary>
        public void Forward()
        {
            this.ClientPort.Write(string.Format("{0}.goForward();", FireFoxClientPort.BrowserVariableName));
            this.ClientPort.InitializeDocument();
        }

        /// <summary>
        /// Load a URL into the document. see: http://developer.mozilla.org/en/docs/XUL:browser#m-loadURI
        /// </summary>
        /// <param name="url">The URL to laod.</param>
        public void LoadUri(Uri url)
        {
            this.ClientPort.Write(string.Format("{0}.loadURI(\"{1}\");", FireFoxClientPort.BrowserVariableName, url));
            this.ClientPort.InitializeDocument();
        }

        /// <summary>
        /// Reloads this instance.
        /// </summary>
        public void Reload()
        {
            this.ClientPort.Write(string.Format("{0}.reload();", FireFoxClientPort.BrowserVariableName));
            this.ClientPort.InitializeDocument();
        }

        #endregion        
    }
}
