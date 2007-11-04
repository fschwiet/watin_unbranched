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
        private readonly FireFoxClientPort clientPort;        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="XULBrowser"/> class.
        /// </summary>
        /// <param name="clientPort">The client port.</param>
        public XULBrowser(FireFoxClientPort clientPort)
        {
            this.clientPort = clientPort;
        }

        public FireFoxClientPort ClientPort
        {
            get { return clientPort; }
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

        public void Reload()
        {
            throw new NotImplementedException();
        }
    }
}
