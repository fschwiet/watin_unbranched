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
using MIL.Html;

namespace WatiN.Core.Mozilla
{
    public class Element
    {
        private readonly FireFoxClientPort clientPort;
        private readonly MIL.Html.HtmlElement parsedElement;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Element"/> class.
        /// </summary>
        /// <param name="outerHtml">The outer HTML that defines the element.</param>
        /// <param name="clientPort">The client port.</param>
        public Element(string outerHtml, FireFoxClientPort clientPort)
        {
            this.clientPort = clientPort;
            HtmlDocument htmlDoc = HtmlDocument.Create(outerHtml);            
            if (htmlDoc == null || htmlDoc.Nodes == null || htmlDoc.Nodes.Count == 0)
            {
                throw new FireFoxException(string.Format("Unable to create html element using outerHtml: {0}", outerHtml));
            }

            parsedElement = htmlDoc.Nodes[0] as HtmlElement;
            if (parsedElement == null)
            {
                throw new FireFoxException(string.Format("Unable to create html element using outerHtml: {0}", outerHtml));
            }
        }

        #endregion

        #region Public instance properties

        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <value>The id.</value>
        public string Id
        {
            get
            {
                return GetAttribute("id");
            }
        }

        /// <summary>
        /// Gets the name of the element's css class.
        /// </summary>
        /// <value>The name of the element's class.</value>
        public string ClassName
        {
            get
            {
                return GetAttribute("class");
            }
        }               

        #endregion

        #region Protected instance methods

        /// <summary>
        /// Gets the client port used to communicate with the jssh server..
        /// </summary>
        /// <value>The client port.</value>
        protected FireFoxClientPort ClientPort
        {
            get { return clientPort; }
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns></returns>
        protected string GetAttribute(string attributeName)
        {
            string attibuteValue = null;
            HtmlAttribute attribute = parsedElement.Attributes.FindByName(attributeName);

            if (attribute != null)
            {
                attibuteValue = attribute.Value;
            }

            return attibuteValue;
        }

        /// <summary>
        /// Sets the attribute.
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="value">The value.</param>
        protected void SetAttribute(string attributeName, string value)
        {
            this.ClientPort.Write(string.Format("{0}.getElementById(\"{1}\").{2} = \"{3}\";", FireFoxClientPort.DocumentVariableName, this.Id, attributeName, value));
            
            HtmlAttribute attribute = parsedElement.Attributes.FindByName(attributeName);
            if (attribute != null)
            {
                attribute.Value = value;
            }
            else
            {
                parsedElement.Attributes.Add(new HtmlAttribute(attributeName, value));
            }
        }
        #endregion
    }
}
