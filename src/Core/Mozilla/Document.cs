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
using WatiN.Core.Interfaces;

namespace WatiN.Core.Mozilla
{
    public abstract class Document : ElementsContainer, IDocument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Document"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="clientPort">The client port.</param>
        protected Document(string id, FireFoxClientPort clientPort) : base(id, clientPort)
        {
        }

        #region Public instance properties

        /// <summary>
        /// Gets the active element in this webpage.
        /// </summary>
        /// <value>The active element or <c>null</c> if no element has the focus.</value>
        public IElement ActiveElement
        {
            get 
            {
                return GetElementByProperty("activeElement");
            }
        }

        /// <summary>
        /// Gets the HTML of the Body part of the webpage.
        /// </summary>
        /// <value>The HTML of the Body part of the webpage.</value>
        public string Html
        {
            get
            {
                this.ClientPort.Write(string.Format("domDumpFull({0}.body);", FireFoxClientPort.DocumentVariableName));
                return this.ClientPort.LastResponse;
            }
        }

        /// <summary>
        /// Gets the inner text of the Body part of the webpage.
        /// </summary>
        /// <value>The inner text.</value>
        public new string Text
        {
            get
            {
                this.ClientPort.Write(string.Format("{0}.body.textContent;", FireFoxClientPort.DocumentVariableName));
                return this.ClientPort.LastResponse;
            }    
        }

        /// <summary>
        /// Gets the title of the webpage.
        /// </summary>
        /// <value>The title.</value>
        public new string Title
        {
            get
            {
                this.ClientPort.Write(string.Format("{0}.title", FireFoxClientPort.DocumentVariableName));
                return this.ClientPort.LastResponse;
            }
        }

        /// <summary>
        /// Returns a System.Uri instance of the url displayed in the address bar of the browser,
        /// of the currently displayed web page.
        /// </summary>
        /// <value></value>
        /// <example>
        /// The following example creates a new Internet Explorer instances, navigates to
        /// the WatiN Project website on SourceForge and writes the Uri of the
        /// currently displayed webpage to the debug window.
        /// <code>
        /// using WatiN.Core;
        /// using System.Diagnostics;
        /// namespace NewIEExample
        /// {
        /// public class WatiNWebsite
        /// {
        /// public WatiNWebsite()
        /// {
        /// IE ie = new IE("http://watin.sourceforge.net");
        /// Debug.WriteLine(ie.Uri.ToString());
        /// }
        /// }
        /// }
        /// </code>
        /// </example>
        public Uri Uri
        {
            get
            {
                return new Uri(this.Url);
            }
        }

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
        public string Url
        {
            get
            {
                this.ClientPort.Write("{0}.location", FireFoxClientPort.WindowVariableName);
                return this.ClientPort.LastResponse;
            }
        }

        #endregion

        #region Internal instance methods        

        /// <summary>
        /// Gets the element by property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Returns the element that is returned by the specified property</returns>
        internal new IElement GetElementByProperty(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }

            string elementvar = FireFoxClientPort.CreateVariableName();
            string command = string.Format("{0}={1}.{2};{0}==null", elementvar, FireFoxClientPort.DocumentVariableName, propertyName);
            this.ClientPort.Write(command);

            if (!this.ClientPort.LastResponseAsBool)
            {
                return new Element(elementvar, this.ClientPort);
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}
