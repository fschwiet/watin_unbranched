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
using System.Text.RegularExpressions;
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

                WatiNElementCollection elements = new WatiNElementCollection(this.ClientPort, new ElementFinder(this, null, null, this.ClientPort));
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
        	Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "area", Find.ById(id), this.ClientPort);
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
        	return this.Button(Find.ById(id));
        }

                /// <summary>
        /// Finds a button element using the specified regular expression.
        /// </summary>
        /// <param name="regex">The regular expression for the id of the button element being sought.</param>
        /// <returns>The button element which id matches the regular expression, or null if none is found</returns>
        public IButton Button(Regex regex)
        {
        	return this.Button(Find.ById(regex));
        }

        /// <summary>
        /// Finds a button element using the specified <see cref="AttributeConstraint" />.
        /// </summary>
        /// <param name="constraint">The <see cref="AttributeConstraint" /> for the button element being sought.</param>
        /// <returns>The button element for the matches the <see cref="AttributeConstraint" />, or null if none is found</returns>
        public IButton Button(AttributeConstraint constraint)
        {
        	Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "input", "button submit image reset", constraint, this.ClientPort);
            return new Button(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Finds a div element using the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public IDiv Div(string id)
        {
        	Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "div", Find.ById(id), this.ClientPort);
        	return new Div(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Finds a div element using the specified id.
        /// </summary>
        /// <param name="id">The id of the link element being sought.</param>
        /// <returns></returns>
        public ILink Link(string id)
        {
        	Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "a", Find.ById(id), this.ClientPort);
        	return new Link(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Finds a paragraph element using the specified id.
        /// </summary>
        /// <param name="id">The id of the paragraph element being sought.</param>
        /// <returns></returns>
        public IPara Para(string id)
        {
        	Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "p", Find.ById(id), this.ClientPort);
        	return new Para(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Finds a table using the specified Id.
        /// </summary>
        /// <param name="id">The id of the table element being sought.</param>
        /// <returns>The table element for the corresponding id, or null if none is found</returns>
        public ITable Table(string id)
        {
            Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "table", Find.ById(id), this.ClientPort);
            return new Table(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Finds a table row using the specified Id.
        /// </summary>
        /// <param name="id">The id of the table row element being sought.</param>
        /// <returns>The table row element for the corresponding id, or null if none is found</returns>
        public ITableRow TableRow(string id)
        {
            Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "tr", Find.ById(id), this.ClientPort);
            return new TableRow(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Finds a table cell using the specified Id.
        /// </summary>
        /// <param name="id">The id of the table cell element being sought.</param>
        /// <returns>The table cell element for the corresponding id, or null if none is found</returns>
        public ITableCell TableCell(string id)
        {
            Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "td", Find.ById(id), this.ClientPort);
            return new TableCell(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Finds a text field using the Id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A text field for the specified id</returns>
        public ITextField TextField(string id)
        {
        	return this.TextField(Find.ById(id));
        }

		/// <summary>
        /// Finds a text field element using the specified regular expression.
        /// </summary>
        /// <param name="regex">The regular expression for the id of the text field element being sought.</param>
        /// <returns>The text field element which id matches the regular expression, or null if none is found</returns>
        public ITextField TextField(Regex regex)
        {
        	return this.TextField(Find.ById(regex));
        }

        /// <summary>
        /// Finds a text field element using the specified <see cref="AttributeConstraint" />.
        /// </summary>
        /// <param name="constraint">The <see cref="AttributeConstraint" /> for the text field element being sought.</param>
        /// <returns>The text field element for the matches the <see cref="AttributeConstraint" />, or null if none is found</returns>
        public ITextField TextField(AttributeConstraint constraint)
        {
        	Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "input", "text password textarea hidden", constraint, this.ClientPort);
        	return new TextField(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Finds an element matching the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public IElement Element(string id)
        {
        	Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, null, Find.ById(id), this.ClientPort);
        	return new Element(finder.FindFirst(), this.ClientPort);
        }

        #endregion

        #region Protected instance methods

        #endregion

    }
}
