#region WatiN Copyright (C) 2006-2007 Jeroen van Menen

//Copyright 2006-2007 Jeroen van Menen
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
using System.Text.RegularExpressions;
using WatiN.Core.Exceptions;
using WatiN.Core.Interfaces;
using WatiN.Core.Logging;

namespace WatiN.Core.Mozilla
{
    public class FireFox : Document, IDisposable, IBrowser
    {
        /// <summary>
        /// <c>true</c> if the <see cref="Dispose()"/> method has been called to release resources.
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Wrapper for the XUL:Browser object see: http://developer.mozilla.org/en/docs/XUL:browser
        /// </summary>
        private XULBrowser xulBrowser;

        #region Public constructors / destructor

        /// <summary>
        /// Initializes a new instance of the <see cref="FireFox"/> class.
        /// </summary>
        public FireFox() : base(string.Empty, new FireFoxClientPort())
        {
        	CreateFireFoxInstance();
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="FireFox"/> class.
        /// </summary>
        /// <param name="url">The url to go to</param>
        public FireFox(string url) : base(string.Empty, new FireFoxClientPort())
        {
        	CreateFireFoxInstance();            
            GoTo(url);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FireFox"/> class.
        /// </summary>
        /// <param name="uri">The url to go to</param>
        public FireFox(Uri uri) : base(string.Empty, new FireFoxClientPort())
        {
        	CreateFireFoxInstance();            
            GoTo(uri);
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="FireFox"/> is reclaimed by garbage collection.
        /// </summary>
        ~FireFox()
        {
            Dispose(false);
        }

        #endregion

        #region Public instance properties

        public BrowserType BrowserType
        {
            get { return Core.BrowserType.FireFox; }
        }

        /// <summary>
        /// Determines whether the text inside the HTML Body element contains the given <paramref name="text" />.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        ///     <c>true</c> if the specified text is contained in <see cref="IDocument.Html"/>; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsText(string text)
        {
            string innertext = Text;

            if (innertext == null) return false;

            return (innertext.IndexOf(text) >= 0);
        }

        /// <summary>
        /// Determines whether the text inside the HTML Body element contains the given <paramref name="regex" />.
        /// </summary>
        /// <param name="regex">The regular expression to match with.</param>
        /// <returns>
        ///     <c>true</c> if the specified text is contained in <see cref="IDocument.Html"/>; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsText(Regex regex)
        {
            string innertext = Text;

            if (innertext == null) return false;

            return (regex.Match(innertext).Success);
        }

        /// <summary>
        /// Evaluates the specified JavaScript code within the scope of this
        /// document. Returns the value computed by the last expression in the
        /// code block after implicit conversion to a string.
        /// </summary>
        /// <example>
        /// The following example shows an alert dialog then returns the string "4".
        /// <code>
        /// Eval("window.alert('Hello world!'); 2 + 2");
        /// </code>
        /// </example>
        /// <param name="javaScriptCode">The JavaScript code</param>
        /// <returns>The result converted to a string</returns>
        /// <exception cref="JavaScriptException">Thrown when the JavaScript code cannot be evaluated
        /// or throws an exception during evaluation</exception>
        public string Eval(string javaScriptCode)
        {
            this.ClientPort.Write(javaScriptCode);
            return this.ClientPort.LastResponse;
        }

        #endregion Public instance properties

        #region Public instance methods

        /// <summary>
        /// Navigates the browser back to the previously displayed Url (like the back
        /// button in Internet Explorer).
        /// </summary>
        public void Back()
        {
            this.xulBrowser.Back();
        }

        /// <summary>
        /// Brings the current browser to the front (makes it the top window)
        /// </summary>
        public void BringToFront()
        {
            this.xulBrowser.BringToFront();
        }

        /// <summary>
        /// Navigates the browser forward to the next displayed Url (like the forward
        /// button in Internet Explorer).
        /// </summary>
        public void Forward()
        {
            this.xulBrowser.Forward();    
        }

        /// <summary>
        /// Navigates to the given <paramref name="url" />.
        /// </summary>
        /// <param name="url">The URL to GoTo.</param>
        /// <example>
        /// The following example creates a new Internet Explorer instance and navigates to
        /// the WatiN Project website on SourceForge.
        /// <code>
        /// using WatiN.Core;
        /// 
        /// namespace NewIEExample
        /// {
        ///    public class WatiNWebsite
        ///    {
        ///      public WatiNWebsite()
        ///      {
        ///        FireFox ff = new FireFox();
        ///        ff.GoTo("http://watin.sourceforge.net");
        ///      }
        ///    }
        ///  }
        /// </code>
        /// </example>
        public void GoTo(string url)
        {
            this.GoTo(CreateUri(url));
        }

        /// <summary>
        /// Navigates Internet Explorer to the given <paramref name="url" />.
        /// </summary>
        /// <param name="url">The URL specified as a wel formed Uri.</param>
        /// <example>
        /// The following example creates an Uri and Internet Explorer instance and navigates to
        /// the WatiN Project website on SourceForge.
        /// <code>
        /// using WatiN.Core;
        /// using System;
        /// 
        /// namespace NewIEExample
        /// {
        ///    public class WatiNWebsite
        ///    {
        ///      public WatiNWebsite()
        ///      {
        ///        Uri URL = new Uri("http://watin.sourceforge.net");
        ///        IE ie = new IE();
        ///        ie.GoTo(URL);
        ///      }
        ///    }
        ///  }
        /// </code>
        /// </example>
        public void GoTo(Uri url)
        {
            this.xulBrowser.LoadUri(url);
        }

        public IntPtr hWnd
        {
            get { return this.xulBrowser.Handle; }
        }

        /// <summary>
        /// Reloads the currently displayed webpage.
        /// </summary>
        public void Refresh()
        {
            this.xulBrowser.Reload();
        }

        /// <summary>
        /// Closes then reopens the browser navigating to a blank page.
        /// </summary>
        /// <remarks>
        /// Useful when clearing the cookie cache and continuing execution to a test.
        /// </remarks>
        public void Reopen()
        {
            this.ClientPort.Dispose();
            this.ClientPort.Connect();
        }

        /// <summary>
        /// Runs the javascript code in the current browser.
        /// </summary>
        /// <param name="javaScriptCode">The javascript code.</param>
        public void RunScript(string javaScriptCode)
        {
            this.ClientPort.Write(javaScriptCode);
        }

        /// <summary>
        /// Waits until the page is completely loaded
        /// </summary>
        public void WaitForComplete()
        {
        	this.xulBrowser.WaitForComplete();
        }
        
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue 
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }
        
        #endregion Public instance methods

        #region Protected instance methods      

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    this.ClientPort.Dispose();
                }

                // Call the appropriate methods to clean up 
                // unmanaged resources here.
                
                
            }

            this.disposed = true;
        }

        #endregion Protected instance methods

        #region Private instance methods

        

        #endregion Private instance methods
        
        #region Private static methods

        private static Uri CreateUri(string url)
        {
            Uri uri;
            try
            {
                uri = new Uri(url);
            }
            catch (UriFormatException)
            {
                uri = new Uri("http://" + url);
            }
            return uri;
        }

        private void CreateFireFoxInstance()
        {
            Logger.LogAction("Creating new FireFox instance");
            this.ClientPort.Connect();

            this.xulBrowser = new XULBrowser(this.ClientPort);
        }

        #endregion
    }
}
