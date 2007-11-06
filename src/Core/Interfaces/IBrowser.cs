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
using System.Collections.Generic;
using System.Text;

namespace WatiN.Core.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBrowser : IDocument, IDisposable
    {
        /// <summary>
        /// Navigates the browser back to the previously displayed Url (like the back
        /// button in Internet Explorer). 
        /// </summary>
        void Back();

        /// <summary>
        /// Gets the type of the browser.
        /// </summary>
        /// <value>The type of the browser.</value>
        BrowserType BrowserType { get; }

        /// <summary>
        /// Navigates the browser forward to the next displayed Url (like the forward
        /// button in Internet Explorer). 
        /// </summary>
        void Forward();

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
        ///        IE ie = new IE();
        ///        ie.GoTo("http://watin.sourceforge.net");
        ///      }
        ///    }
        ///  }
        /// </code>
        /// </example>
        void GoTo(string url);

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
        void GoTo(Uri url);

        /// <summary>
        /// Reloads the currently displayed webpage.
        /// </summary>
        void Refresh();

        /// <summary>
        /// Closes then reopens the browser navigating to a blank page.
        /// </summary>
        /// <remarks>
        /// Useful when clearing the cookie cache and continuing execution to a test.
        /// </remarks>
        void Reopen();
    }
}