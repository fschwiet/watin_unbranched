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
using WatiN.Core.Exceptions;

namespace WatiN.Core.Interfaces
{
    public interface IDocument : IElementsContainerTemp
    {
        /// <summary>
        /// Gets the active element in this webpage.
        /// </summary>
        /// <value>The active element or <c>null</c> if no element has the focus.</value>
        IElement ActiveElement { get;}

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
        /// Gets the HTML of the Body part of the webpage.
        /// </summary>
        /// <value>The HTML of the Body part of the webpage.</value>
        string Html { get; }

        /// <summary>
        /// Gets the inner text of the Body part of the webpage.
        /// </summary>
        /// <value>The inner text.</value>
        string Text { get; }

        /// <summary>
        /// Gets the title of the webpage.
        /// </summary>
        /// <value>The title.</value>
        string Title { get; }

        /// <summary>
		/// Returns a System.Uri instance of the url displayed in the address bar of the browser, 
		/// of the currently displayed web page.
		/// </summary>
		/// <example>
		/// The following example creates a new Internet Explorer instances, navigates to
		/// the WatiN Project website on SourceForge and writes the Uri of the
		/// currently displayed webpage to the debug window.
		/// <code>
		/// using WatiN.Core;
		/// using System.Diagnostics;
		///
		/// namespace NewIEExample
		/// {
		///    public class WatiNWebsite
		///    {
		///      public WatiNWebsite()
		///      {
		///        IE ie = new IE("http://watin.sourceforge.net");
		///        Debug.WriteLine(ie.Uri.ToString());
		///      }
		///    }
		///  }
		/// </code>
		/// </example>
        Uri Uri { get; }

        /// <summary>
        /// Returns the url, as displayed in the address bar of the browser, of the currently
        /// displayed web page.
        /// </summary>
        /// <example>
        /// The following example creates a new Internet Explorer instances, navigates to
        /// the WatiN Project website on SourceForge and writes the Url of the
        /// currently displayed webpage to the debug window.
        /// <code>
        /// using WatiN.Core;
        /// using System.Diagnostics;
        ///
        /// namespace NewIEExample
        /// {
        ///    public class WatiNWebsite
        ///    {
        ///      public WatiNWebsite()
        ///      {
        ///        IE ie = new IE("http://watin.sourceforge.net");
        ///        Debug.WriteLine(ie.Url);
        ///      }
        ///    }
        ///  }
        /// </code>
        /// </example>
        string Url { get;}
    }
}