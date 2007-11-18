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
using WatiN.Core.Interfaces;

namespace WatiN.Core.Mozilla
{
    public class Element : IElement
    {
        private readonly FireFoxClientPort clientPort;
        private readonly string elementVariable;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Element"/> class.
        /// </summary>
        /// <param name="elementVariable">The javascript variable name referencing this element.</param>
        /// <param name="clientPort">The client port.</param>
        public Element(string elementVariable, FireFoxClientPort clientPort)
        {
            this.elementVariable = elementVariable;
            this.clientPort = clientPort;
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
            	return GetAttributeValue("id");
            }
        }

        /// <summary>
        /// Gets the type of the node, Mozilla DOM only?
        /// </summary>
        /// <value>The type of the node.</value>
        public NodeType NodeType
        {
            get
            {
                string nodeTypeValue = GetProperty("nodeType");
                return (NodeType)Convert.ToInt32(nodeTypeValue);
            }
        }

        /// <summary>
        /// Gets the inner HTML of this element.
        /// </summary>
        /// <value>The inner HTML.</value>
        public string InnerHtml
        {
            get
            {
                return this.GetProperty("innerHTML");
            }
        }

        public IElement Parent
        {
            get
            {
                return GetElementByProperty("parentNode");
            }
        }

        public IElement NextSibling
        {
            get
            {
                Element element = (Element)GetElementByProperty("nextSibling");

                while (true)
                {
                    if(element == null || element.NodeType != NodeType.Text)
                    {
                        return element;                        
                    }

                    element = (Element) element.NextSibling;
                }
            }
        }

        public IElement PreviousSibling
        {
            get
            {
                Element element = (Element)GetElementByProperty("previousSibling");

                while (true)
                {
                    if (element == null || element.NodeType != NodeType.Text)
                    {
                        return element;
                    }

                    element = (Element)element.PreviousSibling;
                }
            }
        }

        /// <summary>
        /// Gets the innertext of this element (and the innertext of all the elements contained
        /// in this element).
        /// </summary>
        /// <value>The inner text of this element</value>
        public virtual string Text
        {
            get
            {
                return this.GetProperty("textContent");
            }
        }

        /// <summary>
        /// Gets the title of the element.
        /// </summary>
        /// <value>The title of this element.</value>
        public string Title
        {
            get
            {
                return this.GetAttributeValue("title");
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
                return GetAttributeValue("class");
            }
        }

        #endregion

        #region Public instance methods

        /// <summary>
        /// Clicks this element and waits till the event is completely finished (page is loaded
        /// and ready) .
        /// </summary>
        public void Click()
        {
            this.ExecuteMethod("click");
            this.ClientPort.InitializeDocument();
        }

        public bool StoredElementReferenceExists()
        {
            string getAttributeWrite = string.Format("{0} != null; ", elementVariable);
            this.ClientPort.Write(getAttributeWrite);
            return this.clientPort.LastResponse == "true";
        }
        #endregion

        #region Protected instance methods

        /// <summary>
        /// Gets the client port used to communicate with the jssh server..
        /// </summary>
        /// <value>The client port.</value>
        public FireFoxClientPort ClientPort
        {
            get { return clientPort; }
        }

        /// <summary>
        /// Executes the method.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        protected void ExecuteMethod(string methodName)
        {
            this.ClientPort.Write(
                string.Format(
                    "var event = {0}.createEvent(\"MouseEvents\");\n" +
                    "event.initEvent(\"{1}\",true,true);\n" +
                    "{2}.dispatchEvent(event)", FireFoxClientPort.DocumentVariableName,
                    methodName, this.elementVariable));
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns></returns>
        public string GetAttributeValue(string attributeName)
        {
            if (UtilityClass.IsNullOrEmpty(attributeName))
            {
                throw new ArgumentNullException("attributeName", "Null or Empty not allowed.");
            }

            string getAttributeWrite = string.Format("{0}.getAttribute(\"{1}\");", this.elementVariable, attributeName);
            this.ClientPort.Write(getAttributeWrite);

            if (this.ClientPort.LastResponseIsNull)
            {
                return null;
            }

            return this.ClientPort.LastResponse;
        }

        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        protected string GetProperty(string propertyName)
        {
            if (UtilityClass.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName", "Null or Empty not allowed.");
            }

            string command = string.Format("{0}.{1};", this.elementVariable, propertyName);
            this.ClientPort.Write(command);

            return this.ClientPort.LastResponse;
        }

        /// <summary>
        /// Sets the attribute.
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="value">The value.</param>
        protected void SetAttributeValue(string attributeName, string value)
        {
            string command = string.Format("{0}.setAttribute(\"{1}\", \"{2}\");", this.elementVariable, attributeName, value);
            this.ClientPort.Write(command);
        }

        /// <summary>
        /// Gets the element by property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Returns the element that is returned by the specified property</returns>
        private IElement GetElementByProperty(string propertyName)
        {
        	string elementvar = FireFoxClientPort.CreateVariableName();
        	string command = string.Format("{0}={1}.{2};", elementvar, this.elementVariable, propertyName);
            this.ClientPort.Write(command);

            if (!this.ClientPort.LastResponseIsNull)
            {
                return new Element(elementvar, this.ClientPort);    
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region private instance methods


        #endregion
    }
}
