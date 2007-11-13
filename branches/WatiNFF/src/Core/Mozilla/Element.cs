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
        private readonly string elementId;

        /// <summary>
        /// If an element can not be identified by id, because known has been specified,
        /// the knownElementId is used in conjuction with <see cref="findByPath"/> to
        /// locate the element. For an example of it's usage <see cref="Element.ctor(string, string, FireFoxClientPort)"/>
        /// </summary>
        private string knownElementId;

        private string findByPath;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Element"/> class.
        /// </summary>
        /// <param name="elementId">The element id.</param>
        /// <param name="clientPort">The client port.</param>
        public Element(string elementId, FireFoxClientPort clientPort)
            : this(clientPort)
        {
            this.elementId = elementId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Element"/> class using the path
        /// to identify how the element can be retrieved. Useful for elements that do not
        /// have an id attribute specified
        /// </summary>
        /// <param name="findByPath">The path by which the element can be found.</param>
        /// <param name="knownElementId">The known element id.</param>
        /// <param name="clientPort">The client port.</param>
        /// <example>Element noIdElement = new Element("NextSibling.NextSibling", "form1")
        /// Where form1 is the element that can be identified by id.
        /// </example>
        internal Element(string findByPath, string knownElementId, FireFoxClientPort clientPort)
            : this(clientPort)
        {
            this.findByPath = findByPath;
            this.knownElementId = knownElementId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Element"/> class.
        /// </summary>
        /// <param name="clientPort">The client port.</param>
        private Element(FireFoxClientPort clientPort)
        {
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
                return elementId;
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

                //Element element = (Element)GetElementByProperty("nextSibling");

                //while (element != null && element.NodeType == NodeType.Text)
                //{
                //    element = (Element) element.NextSibling;
                //}

                //return element;
            }
        }

        /// <summary>
        /// Gets the innertext of this element (and the innertext of all the elements contained
        /// in this element).
        /// </summary>
        /// <value>The inner text of this element</value>
        public string Text
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
                    methodName, this.LocationToElementSuffix));
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

            string getAttributeWrite = string.Format("{0}.getAttribute(\"{1}\");", this.LocationToElementSuffix, attributeName);
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

            string getAttributeWrite = string.Format("{0}.{1};", this.LocationToElementSuffix, propertyName);
            this.ClientPort.Write(getAttributeWrite);

            return this.ClientPort.LastResponse;
        }

        /// <summary>
        /// Sets the attribute.
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="value">The value.</param>
        protected void SetAttributeValue(string attributeName, string value)
        {
            string command = string.Format("{0}.setAttribute(\"{1}\", \"{2}\");", this.LocationToElementSuffix, attributeName, value);
            this.ClientPort.Write(command);
        }

        /// <summary>
        /// Gets the element by property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Returns the element that is returned by the specified property</returns>
        private IElement GetElementByProperty(string propertyName)
        {
            this.ClientPort.Write(
                string.Format("{0}.{1}.id;", this.LocationToElementSuffix, propertyName));

            if (!this.ClientPort.LastResponseIsNull)
            {
                string id = this.ClientPort.LastResponse;

                if (!string.IsNullOrEmpty(id))
                {
                    return new Element(id, this.ClientPort);
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.findByPath))
                    {
                        return new Element(string.Format("{0}.{1}", this.findByPath, propertyName), this.knownElementId, this.ClientPort);
                    }
                    else
                    {
                        return new Element(propertyName, this.Id, this.ClientPort);
                    }
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the location to element suffix.
        /// </summary>
        /// <returns></returns>
        private string LocationToElementSuffix
        {
            get
            {
                if (string.IsNullOrEmpty(this.Id) && string.IsNullOrEmpty(this.findByPath))
                {
                    throw new FireFoxException("Unable to locate an element without an id or a specified find path");
                }

                string locationSuffix = "";

                if (!string.IsNullOrEmpty(this.Id))
                {
                    locationSuffix =
                        string.Format("{0}.getElementById(\"{1}\")", FireFoxClientPort.DocumentVariableName, this.Id);
                }
                else if (!string.IsNullOrEmpty(this.knownElementId))
                {
                    locationSuffix =
                        string.Format("{0}.getElementById(\"{1}\").{2}", FireFoxClientPort.DocumentVariableName,
                                      this.knownElementId, this.findByPath);
                }
                else
                {
                    locationSuffix =
                        string.Format("{0}.body.{1}", FireFoxClientPort.DocumentVariableName, this.findByPath);
                }

                return locationSuffix;
            }
        }
        #endregion

        #region private instance methods


        #endregion

    }
}
