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
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using WatiN.Core.Exceptions;

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
        /// Brings the current browser to the front (makes it the top window)
        /// </summary>
        void BringToFront();

        /// <summary>
        /// Gets the type of the browser.
        /// </summary>
        /// <value>The type of the browser.</value>
        BrowserType BrowserType { get; }

        /// <summary>
        /// Determines whether the text inside the HTML Body element contains the given <paramref name="text" />.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        ///     <c>true</c> if the specified text is contained in <see cref="IDocument.Html"/>; otherwise, <c>false</c>.
        /// </returns>
        bool ContainsText(string text);

        /// <summary>
        /// Determines whether the text inside the HTML Body element contains the given <paramref name="regex" />.
        /// </summary>
        /// <param name="regex">The regular expression to match with.</param>
        /// <returns>
        ///     <c>true</c> if the specified text is contained in <see cref="IDocument.Html"/>; otherwise, <c>false</c>.
        /// </returns>
        bool ContainsText(Regex regex);

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
        string Eval(string javaScriptCode);

        /// <summary>
        /// Gets the text inside the HTML Body element that matches the regular expression.
        /// </summary>
        /// <param name="regex">The regular expression to match with.</param>
        /// <returns>The matching text, or null if none.</returns>
        string FindText(Regex regex);

        /// <summary>
        /// Navigates the browser forward to the next displayed Url (like the forward
        /// button in Internet Explorer). 
        /// </summary>
        void Forward();

        /// <summary>
        /// Gets the window style.
        /// </summary>
        /// <returns>The style currently applied to the ie window.</returns>
        NativeMethods.WindowShowStyle GetWindowStyle();

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

        IntPtr hWnd { get; }

        /// <summary>
        /// Sends a Tab key to the current browser window to simulate tabbing through
        /// the elements (and address bar).
        /// </summary>
        void PressTab();

        /// <summary>
		/// Gets the process ID the current browser is running in.
		/// </summary>
		/// <value>The process ID the current browser is running in.</value>
        int ProcessID { get; }

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

        /// <summary>
        /// Runs the javascript code in the current browser.
        /// </summary>
        /// <param name="javaScriptCode">The javascript code.</param>
        void RunScript(string javaScriptCode);

        /// <summary>
        /// Make the current browser window full screen, minimized, maximized and more.
        /// </summary>
        /// <param name="showStyle">The style to apply.</param>
        void ShowWindow(NativeMethods.WindowShowStyle showStyle);

        /// <summary>
        /// Waits until the document is fully loaded
        /// </summary>
        void WaitForComplete();
    }
}
