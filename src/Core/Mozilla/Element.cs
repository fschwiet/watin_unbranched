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
using System.Collections.ObjectModel;
using WatiN.Core.Exceptions;
using WatiN.Core.Interfaces;

namespace WatiN.Core.Mozilla
{
    public class Element : IElement
    {
        private readonly FireFoxClientPort clientPort;
        private readonly string elementVariable;

        /// <summary>
        /// List of html attributes that have to retrieved as properties in order to get the correct value.
        /// I.e. for options myOption.getAttribute("selected"); returns nothing if it's selected. 
        /// However  myOption.selected returns true.
        /// </summary>
        private static readonly List<string> knownAttributeOverrides = new List<string>(
            new string[]
                {
                    "selected", "textContent", "className", "disabled", "checked", "readOnly", "multiple", "value"
                });

        private static readonly Dictionary<string, string> watiNAttributeMap = new Dictionary<string, string>();

        #region Constructors

        static Element()
        {
            watiNAttributeMap.Add("innertext", "textContent");
            watiNAttributeMap.Add("classname", "className");
        }

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
        /// Gets a collection of child nodes for the current element.
        /// </summary>
        public Collection<Element> ChildNodes
        {
            get
            {
                Collection<Element> childNodes = new Collection<Element>();
                int childNodeCount;
                if (int.TryParse(this.GetProperty("childNodes.length"), out childNodeCount))
                {
                    for (int i = 0; i < childNodeCount; i++)
                    {
                        Element element = (Element)this.GetElementByProperty(string.Format("childNodes[{0}]", i));

                        if (element.NodeType != Mozilla.NodeType.Text)
                        {
                            childNodes.Add(element);
                        }
                    }
                }

                return childNodes;
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
                    if (element == null || element.NodeType != NodeType.Text)
                    {
                        return element;
                    }

                    element = (Element)element.NextSibling;
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
        /// Gets the tag name of this element.
        /// </summary>
        /// <value>The name of the tag.</value>
        public string TagName
        {
            get { return this.GetProperty("tagName"); }
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

        /// <summary>
        /// Gets a value indicating whether this <see cref="IElement"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        public bool Enabled
        {
            get
            {
                bool disabled;
                Boolean.TryParse(GetProperty("disabled"), out disabled);
                return !disabled;
            }
        }

        /// <summary>
        /// Gives the element focus.
        /// </summary>
        /// <exception cref="ElementDisabledException">if the element is disabled and can not recieve focus.</exception>
        public void Focus()
        {
            if (!Enabled)
            {
                throw new ElementDisabledException(Id);
            }

            ExecuteMethod("focus");
        }

        #endregion

        #region Public instance methods

        /// <summary>
        /// Gets the client port used to communicate with the jssh server..
        /// </summary>
        /// <value>The client port.</value>
        public FireFoxClientPort ClientPort
        {
            get { return clientPort; }
        }

        /// <summary>
        /// Clicks this element and waits till the event is completely finished (page is loaded
        /// and ready) .
        /// </summary>
        public void Click()
        {
            this.FireEvent("click");
            XULBrowser.WaitForComplete(this.ClientPort);
        }

        /// Fires the specified <paramref name="eventName"/> on this element
        /// and waits for it to complete.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        public void FireEvent(string eventName)
        {
            this.ExecuteEvent(eventName);
        }

        /// <summary>
        /// Only fires the specified <paramref name="eventName"/> on this element.
        /// </summary>
        public void FireEventNoWait(string eventName)
        {
            throw new NotImplementedException();
        }

        public bool Exists
        {
            get
            {
                if (UtilityClass.IsNullOrEmpty(elementVariable)) return false;

                string command = string.Format("{0} != null; ", elementVariable);
                return clientPort.WriteAndReadAsBool(command);
            }
        }

        #endregion

        #region Internal instance properties

        internal string ElementVariable
        {
            get { return elementVariable; }
        }

        #endregion


        #region Protected instance methods

        /// <summary>
        /// Executes the event.
        /// </summary>
        /// <param name="eventName">Name of the event to fire.</param>
        protected void ExecuteEvent(string eventName)
        {
            this.ClientPort.Write(
                    "var event = " + FireFoxClientPort.DocumentVariableName + ".createEvent(\"MouseEvents\");" +
                    "event.initEvent(\"" + eventName + "\",true,true);" +
                    "var res = " + this.ElementVariable + ".dispatchEvent(event); if(res){true;}else{false};");
        }

        /// <summary>
        /// Executes a method with no parameters.
        /// </summary>
        /// <param name="methodName">Name of the method to execute.</param>
        protected void ExecuteMethod(string methodName)
        {
            this.ClientPort.Write("{0}.{1}();", this.elementVariable, methodName);
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

            if (string.IsNullOrEmpty(this.elementVariable))
            {
                throw new FireFoxException("Element does not exist, element variable was empty");
            }

            // Translate to FireFox html syntax
            if (watiNAttributeMap.ContainsKey(attributeName))
            {
                attributeName = watiNAttributeMap[attributeName];
            }

            // Handle properties different from attributes
            if (knownAttributeOverrides.Contains(attributeName) || attributeName.StartsWith("style", StringComparison.OrdinalIgnoreCase))
            {
                return GetProperty(attributeName);
            }

            // Retrieve attribute value
            string getAttributeWrite = string.Format("{0}.getAttribute(\"{1}\");", elementVariable, attributeName);
            string lastResponse = ClientPort.WriteAndRead(getAttributeWrite);

            // Post processing
            if (attributeName.ToLowerInvariant() == "type") { lastResponse = lastResponse ?? "text"; }

            // return result
            return lastResponse;
        }

        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        internal string GetProperty(string propertyName)
        {
            if (UtilityClass.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName", "Null or Empty not allowed.");
            }

            if (string.IsNullOrEmpty(this.elementVariable))
            {
                throw new FireFoxException("Element does not exist, element variable was empty");
            }

            string command = string.Format("{0}.{1};", elementVariable, propertyName);
            return ClientPort.WriteAndRead(command);
        }

        /// <summary>
        /// Gets the element by property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Returns the element that is returned by the specified property</returns>
        internal IElement GetElementByProperty(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }

            string elementvar = FireFoxClientPort.CreateVariableName();
            string command = string.Format("{0}={1}.{2};{0}==null;", elementvar, elementVariable, propertyName);
            bool result = ClientPort.WriteAndReadAsBool(command);

            if (!result)
            {
                return new Element(elementvar, this.ClientPort);
            }
            return null;
        }

        /// <summary>
        /// Sets the attribute.
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="value">The value.</param>
        protected void SetAttributeValue(string attributeName, string value)
        {
            // Translate to FireFox html syntax
            if (watiNAttributeMap.ContainsKey(attributeName))
            {
                attributeName = watiNAttributeMap[attributeName];
            }

            // Handle properties different from attributes
            if (knownAttributeOverrides.Contains(attributeName) || attributeName.StartsWith("style", StringComparison.OrdinalIgnoreCase))
            {
                SetProperty(attributeName, value);
                return;
            }

            string command = string.Format("{0}.setAttribute(\"{1}\", \"{2}\");", this.ElementVariable, attributeName, value);
            this.ClientPort.Write(command);
        }

        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value.</param>
        protected void SetProperty(string propertyName, string value)
        {
            string command = string.Format("{0}.{1} = \"{2}\";", this.ElementVariable, propertyName, value);
            this.ClientPort.Write(command);
        }

        #endregion

        #region private instance methods


        #endregion
    }
}
