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
    public abstract class ElementsContainer : Element, IElementsContainerTemp
    {
        protected ElementsContainer(string elementVariable, FireFoxClientPort clientPort) : base(elementVariable, clientPort)
        {
        }

        #region Public instance properties

        public IWatiNElementCollection Elements
        {
            get 
            {

                WatiNElementCollection elements = new WatiNElementCollection(this.ClientPort, new ElementFinder(this, "*", null, this.ClientPort));
                return elements;
            }
        }

        #endregion


        #region Public instance methods

        /// <summary>
        /// Finds an area element using the specified id.
        /// </summary>
        /// <param name="id">The area element being sought.</param>
        /// <returns></returns>
        public IArea Area(string id)
        {
        	Mozilla.ElementFinder finder = new Mozilla.ElementFinder("area", Find.ById(id), this.ClientPort);
        	return new Area(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Finds a button element using the specified id.
        /// </summary>
        /// <param name="id">The id of the button element being sought.</param>
        /// <returns>
        /// The button element for the corresponding id, or null if none is found
        /// </returns>
        public IButton Button(string id)
        {
            Mozilla.ElementFinder finder = new Mozilla.ElementFinder("input", "button", Find.ById(id), this.ClientPort);
            return new Button(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Finds a div element using the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public IDiv Div(string id)
        {
        	Mozilla.ElementFinder finder = new Mozilla.ElementFinder("div", Find.ById(id), this.ClientPort);
        	return new Div(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Finds a div element using the specified id.
        /// </summary>
        /// <param name="id">The id of the link element being sought.</param>
        /// <returns></returns>
        public ILink Link(string id)
        {
        	Mozilla.ElementFinder finder = new Mozilla.ElementFinder("a", Find.ById(id), this.ClientPort);
        	return new Link(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Finds a paragraph element using the specified id.
        /// </summary>
        /// <param name="id">The id of the paragraph element being sought.</param>
        /// <returns></returns>
        public IPara Para(string id)
        {
        	Mozilla.ElementFinder finder = new Mozilla.ElementFinder("p", Find.ById(id), this.ClientPort);
        	return new Para(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Finds a table using the specified Id.
        /// </summary>
        /// <param name="id">The id of the table element being sought.</param>
        /// <returns>The table element for the corresponding id, or null if none is found</returns>
        public ITable Table(string id)
        {
            Mozilla.ElementFinder finder = new Mozilla.ElementFinder("table", Find.ById(id), this.ClientPort);
            return new Table(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Finds a text field using the Id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A text field for the specified id</returns>
        public ITextField TextField(string id)
        {
        	Mozilla.ElementFinder finder = new Mozilla.ElementFinder("input", "text", Find.ById(id), this.ClientPort);
        	return new TextField(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Finds an element matching the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public IElement Element(string id)
        {
        	Mozilla.ElementFinder finder = new Mozilla.ElementFinder("", Find.ById(id), this.ClientPort);
        	return new Element(finder.FindFirst(), this.ClientPort);
        }

        #endregion

        #region Protected instance methods

        #endregion

    }
}
