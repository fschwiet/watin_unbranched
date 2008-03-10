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
using WatiN.Core.Interfaces;
using WatiN.Core.Constraints;

namespace WatiN.Core.Mozilla
{
    public abstract class ElementsContainer : Element, IElementsContainerTemp
    {
        protected ElementsContainer(string elementVariable, FireFoxClientPort clientPort)
            : base(elementVariable, clientPort)
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
            return this.Area(Find.ById(id));
        }

        /// <summary>
        /// Finds a area element using the specified regular expression to match the element id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the area element being sought.</param>
        /// <returns>The area element for the corresponding regular expression, or null if none is found</returns>
        public IArea Area(Regex id)
        {
            return this.Area(Find.ById(id));
        }

        /// <summary>
        /// Finds a area element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the area element being sought.</param>
        /// <returns>The area element for the corresponding attribute constraint, or null if none is found</returns>
        public IArea Area(BaseConstraint findBy)
        {
            Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "area", findBy, this.ClientPort);
            return new Area(finder.FindFirst(), this.ClientPort);
        }

        public IAreaCollection Areas
        {
            get
            {
                Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "area", null, this.ClientPort);
                return new AreaCollection(this.ClientPort, finder);
            }
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
        /// Finds a button element using the specified <see cref="BaseConstraint" />.
        /// </summary>
        /// <param name="constraint">The <see cref="BaseConstraint" /> for the button element being sought.</param>
        /// <returns>The button element for the matches the <see cref="BaseConstraint" />, or null if none is found</returns>
        public IButton Button(BaseConstraint constraint)
        {
            Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "input", "button submit image reset", constraint, this.ClientPort);
            return new Button(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Returns all the button elements for the current document
        /// </summary>
        public IButtonCollection Buttons
        {
            get
            {
                Mozilla.ElementFinder finder = new Mozilla.ElementFinder(
                    this,
                    new List<ElementTag>(new ElementTag[] { new ElementTag("input", "button submit image reset"), new ElementTag("button") }), null, this.ClientPort);
                return new ButtonCollection(this.ClientPort, finder);
            }
        }

        /// <summary>
        /// Finds a checkbox element using the specified id.
        /// </summary>
        /// <param name="id">The id of the checkbox element being sought.</param>
        /// <returns>The checkbox element for the corresponding id, or null if none is found</returns>
        public ICheckBox CheckBox(string id)
        {
            return this.CheckBox(Find.ById(id));
        }

        /// <summary>
        /// Finds a checkbox element using the specified regular expression to match the element id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the checkbox element being sought.</param>
        /// <returns>The checkbox element for the corresponding regular expression, or null if none is found</returns>
        public ICheckBox CheckBox(Regex id)
        {
            return this.CheckBox(Find.ById(id));
        }

        /// <summary>
        /// Finds a check box element using the specified <see cref="BaseConstraint" />.
        /// </summary>
        /// <param name="constraint">The <see cref="BaseConstraint" /> for the check box element being sought.</param>
        /// <returns>The check box element for the matches the <see cref="BaseConstraint" />, or null if none is found</returns>
        public ICheckBox CheckBox(BaseConstraint constraint)
        {
            Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "input", "checkbox", constraint, this.ClientPort);
            return new CheckBox(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Returns all the checkbox elements for the current document
        /// </summary>
        public ICheckBoxCollection CheckBoxes
        {
            get
            {
                Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "input", "checkbox", null, this.ClientPort);
                return new CheckBoxCollection(this.ClientPort, finder);
            }
        }

        /// <summary>
        /// Finds a div element using the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public IDiv Div(string id)
        {
            return this.Div(Find.ById(id));
        }

        /// <summary>
        /// Finds a div element using the specified regular expression to match the div's id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the div element being sought.</param>
        /// <returns>The div element for the corresponding regular expression, or null if none is found</returns>
        public IDiv Div(Regex id)
        {
            return this.Div(Find.ById(id));
        }

        /// <summary>
        /// Finds a div element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the div element being sought.</param>
        /// <returns>The div element for the corresponding attribute constraint, or null if none is found</returns>
        public IDiv Div(BaseConstraint findBy)
        {
            Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "div", findBy, this.ClientPort);
            return new Div(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Returns all the div elements for the current document
        /// </summary>
        public IDivCollection Divs
        {
            get
            {
                Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "div", null, this.ClientPort);
                return new DivCollection(this.ClientPort, finder);
            }
        }

        /// <summary>
        /// Finds a form element using the specified id.
        /// </summary>
        /// <param name="id">The id of the form element being sought.</param>
        /// <returns>The form element for the corresponding id, or null if none is found</returns>
        public IForm Form(string id)
        {
            return this.Form(Find.ById(id));
        }

        /// <summary>
        /// Finds a form element using the specified regular expression to match the forms's id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the form element being sought.</param>
        /// <returns>The form element for the corresponding regular expression, or null if none is found</returns>
        public IForm Form(Regex id)
        {
            return this.Form(Find.ById(id));
        }

        /// <summary>
        /// Finds a form element using the specified <see cref="BaseConstraint" />.
        /// </summary>
        /// <param name="constraint">The <see cref="BaseConstraint" /> for the form element being sought.</param>
        /// <returns>The form element for the matches the <see cref="BaseConstraint" />, or null if none is found</returns>        
        public IForm Form(BaseConstraint constraint)
        {
            Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "form", constraint, this.ClientPort);
            return new Form(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Returns all the form elements for the current document
        /// </summary>
        public IFormsCollection Forms
        {
            get
            {
                Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "form", null, this.ClientPort);
                return new FormsCollection(this.ClientPort, finder);
            }
        }

        /// <summary>
        /// Finds a frame element using the specified id.
        /// </summary>
        /// <param name="id">The id of the frame element being sought.</param>
        /// <returns>The frame element for the corresponding id, or null if none is found</returns>
        public IFrame Frame(string id)
        {
            return this.Frame(Find.ById(id));
        }

        /// <summary>
        /// Finds a frame element using the specified regular expression to match the frame's id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the frame element being sought.</param>
        /// <returns>The frame element for the corresponding regular expression, or null if none is found</returns>
        public IFrame Frame(Regex id)
        {
            return this.Frame(Find.ById(id));
        }

        /// <summary>
        /// Finds a frame element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the frame element being sought.</param>
        /// <returns>The frame element for the corresponding attribute constraint, or null if none is found</returns>
        public IFrame Frame(BaseConstraint findBy)
        {
            Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "frame", findBy, this.ClientPort);
            return new Frame(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Returns all the frame elements for the current document
        /// </summary>
        public IFrameCollection Frames
        {
            get
            {
                Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "frame", null, this.ClientPort);
                return new FrameCollection(this.ClientPort, finder);
            }
        }

        /// <summary>
        /// Finds a label element using the specified id.
        /// </summary>
        /// <param name="id">The id of the label element being sought.</param>
        /// <returns>The label element for the corresponding id, or null if none is found</returns>
        public ILabel Label(string id)
        {
            return this.Label(Find.ById(id));
        }

        /// <summary>
        /// Finds a label element using the specified regular expression to match the label's id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the label element being sought.</param>
        /// <returns>The label element for the corresponding regular expression, or null if none is found</returns>
        public ILabel Label(Regex id)
        {
            return this.Label(Find.ById(id));
        }

        /// <summary>
        /// Finds a label element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the label element being sought.</param>
        /// <returns>The label element for the corresponding attribute constraint, or null if none is found</returns>
        public ILabel Label(BaseConstraint findBy)
        {
            Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "label", findBy, this.ClientPort);
            return new Label(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Returns all the label elements for the current document
        /// </summary>
        public ILabelCollection Labels
        {
            get
            {
                Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "label", null, this.ClientPort);
                return new LabelCollection(this.ClientPort, finder);
            }
        }

        /// <summary>
        /// Finds a link element using the specified id.
        /// </summary>
        /// <param name="id">The id of the link element being sought.</param>
        /// <returns></returns>
        public ILink Link(string id)
        {
            return this.Link(Find.ById(id));
        }

        /// <summary>
        /// Finds a link element using the specified regular expression to match the link's id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the link element being sought.</param>
        /// <returns>The link element for the corresponding regular expression, or null if none is found</returns>
        public ILink Link(Regex id)
        {
            return this.Link(Find.ById(id));
        }

        /// <summary>
        /// Finds a link element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the link element being sought.</param>
        /// <returns>The link element for the corresponding attribute constraint, or null if none is found</returns>
        public ILink Link(BaseConstraint findBy)
        {
            Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "a", findBy, this.ClientPort);
            return new Link(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Returns all the label elements for the current document
        /// </summary>
        public ILinkCollection Links
        {
            get
            {
                Mozilla.ElementFinder finder = new Mozilla.ElementFinder(
                    this,
                    new List<ElementTag>(new ElementTag[] { new ElementTag("a") }), null, this.ClientPort);

                return new LinkCollection(this.ClientPort, finder);
            }
        }

        /// <summary>
        /// Finds an image element using the specified id.
        /// </summary>
        /// <param name="id">The id of the image element being sought.</param>
        /// <returns>The image element for the corresponding id, or null if none is found</returns>
        public IImage Image(string id)
        {
            return this.Image(Find.ById(id));
        }

        /// <summary>
        /// Finds an image element using the specified regular expression to match the image's id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the image element being sought.</param>
        /// <returns>The image element for the corresponding regular expression, or null if none is found</returns>
        public IImage Image(Regex id)
        {
            return this.Image(Find.ById(id));
        }

        /// <summary>
        /// Finds an image element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the image element being sought.</param>
        /// <returns>The image element for the corresponding attribute constraint, or null if none is found</returns>
        public IImage Image(BaseConstraint findBy)
        {
            Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "img", findBy, this.ClientPort);
            return new Image(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Returns all the image elements for the current document
        /// </summary>
        public IImageCollection Images
        {
            get
            {
                Mozilla.ElementFinder finder = new Mozilla.ElementFinder(
                    this,
                    new List<ElementTag>(new ElementTag[] { new ElementTag("img"), new ElementTag("input", "image") }), null, this.ClientPort);

                return new ImageCollection(this.ClientPort, finder);
            }
        }

        /// <summary>
        /// Finds a paragraph element using the specified id.
        /// </summary>
        /// <param name="id">The id of the paragraph element being sought.</param>
        /// <returns></returns>
        public IPara Para(string id)
        {
            return this.Para(Find.ById(id));
        }

        /// <summary>
        /// Finds a paragraph element using the specified regular expression to match the paragraph's id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the paragraph element being sought.</param>
        /// <returns>The paragraph element for the corresponding regular expression, or null if none is found</returns>
        public IPara Para(Regex id)
        {
            return this.Para(Find.ById(id));
        }

        /// <summary>
        /// Finds a paragraph element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the paragraph element being sought.</param>
        /// <returns>The paragraph element for the corresponding attribute constraint, or null if none is found</returns>
        public IPara Para(BaseConstraint findBy)
        {
            Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "p", findBy, this.ClientPort);
            return new Para(finder.FindFirst(), this.ClientPort);            
        }

        /// <summary>
        /// Returns all the paragraph elements for the current document
        /// </summary>
        public IParaCollection Paras
        {
            get
            {
                Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "p", null, this.ClientPort);
                return new ParaCollection(this.ClientPort, finder);
            }
        }

        /// <summary>
        /// Finds a radio button element using the specified id.
        /// </summary>
        /// <param name="id">The id of the radio button element being sought.</param>
        /// <returns>The radio button element for the corresponding id, or null if none is found</returns>
        public IRadioButton RadioButton(string id)
        {
            return this.RadioButton(Find.ById(id));
        }

        /// <summary>
        /// Finds a radio button element using the specified regular expression to match the paragraph's id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the radio button element being sought.</param>
        /// <returns>The radio button element for the corresponding regular expression, or null if none is found</returns>
        public IRadioButton RadioButton(Regex id)
        {
            return this.RadioButton(Find.ById(id));
        }

        /// <summary>
        /// Finds a radio button element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the radio button element being sought.</param>
        /// <returns>The radio button element for the corresponding attribute constraint, or null if none is found</returns>
        public IRadioButton RadioButton(BaseConstraint findBy)
        {
            Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "input", "radio", findBy, this.ClientPort);
            return new RadioButton(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Returns all the radio button elements for the current document
        /// </summary>
        public IRadioButtonCollection RadioButtons
        {
            get
            {
                Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "input", "radio", null, this.ClientPort);
                return new RadioButtonCollection(this.ClientPort, finder);
            }
        }

        /// <summary>
        /// Finds a select element using the specified id.
        /// </summary>
        /// <param name="id">The id of the select element being sought.</param>
        /// <returns>The select element for the corresponding id, or null if none is found</returns>
        public ISelectList SelectList(string id)
        {
            return this.SelectList(Find.ById(id));
        }

        /// <summary>
        /// Finds a select list element using the specified regular expression to match the select list's id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the select list element being sought.</param>
        /// <returns>The select list element for the corresponding regular expression, or null if none is found</returns>
        public ISelectList SelectList(Regex id)
        {
            return this.SelectList(Find.ById(id));
        }

        /// <summary>
        /// Finds a select list using the specified <see cref="BaseConstraint" />.
        /// </summary>
        /// <param name="constraint">The <see cref="BaseConstraint" /> for the select list being sought.</param>
        /// <returns>The select list for the matches the <see cref="BaseConstraint" />, or null if none is found</returns>
        public ISelectList SelectList(BaseConstraint constraint)
        {
            Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "select", constraint, this.ClientPort);
            return new SelectList(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Returns all the select list elements for the current document
        /// </summary>
        public ISelectListCollection SelectLists
        {
            get 
            {
                Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "select", null, this.ClientPort);
                return new SelectListCollection(this.ClientPort, finder);
            }
        }

        /// <summary>
        /// Finds a span element using the specified id.
        /// </summary>
        /// <param name="id">The id of the span element being sought.</param>
        /// <returns>The span element for the corresponding id, or null if none is found</returns>
        public ISpan Span(string id)
        {
            return this.Span(Find.ById(id));
        }

        /// <summary>
        /// Finds a span element using the specified regular expression to match the element id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the span element being sought.</param>
        /// <returns>The span element for the corresponding regular expression, or null if none is found</returns>
        public ISpan Span(Regex id)
        {
            return this.Span(Find.ById(id));
        }

        /// <summary>
        /// Finds a select span using the specified <see cref="BaseConstraint" />.
        /// </summary>
        /// <param name="constraint">The <see cref="BaseConstraint" /> for the span being sought.</param>
        /// <returns>The span for the matches the <see cref="BaseConstraint" />, or null if none is found</returns>
        public ISpan Span(BaseConstraint constraint)
        {
            Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "span", constraint, this.ClientPort);
            return new Span(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Returns all the span elements for the current document
        /// </summary>
        public ISpanCollection Spans
        {
            get
            {
                Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "span", null, this.ClientPort);
                return new SpanCollection(this.ClientPort, finder);
            }
        }

        /// <summary>
        /// Finds a table using the specified Id.
        /// </summary>
        /// <param name="id">The id of the table element being sought.</param>
        /// <returns>The table element for the corresponding id, or null if none is found</returns>
        public ITable Table(string id)
        {
            return this.Table(Find.ById(id));            
        }

        /// <summary>
        /// Finds a table element using the specified regular expression to match the element id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the table element being sought.</param>
        /// <returns>The table element for the corresponding regular expression, or null if none is found</returns>
        public ITable Table(Regex id)
        {
            return this.Table(Find.ById(id));
        }

        /// <summary>
        /// Finds a table element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the table element being sought.</param>
        /// <returns>The table element for the corresponding attribute constraint, or null if none is found</returns>
        public ITable Table(BaseConstraint findBy)
        {
            Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "table", findBy, this.ClientPort);
            return new Table(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Returns all the table elements for the current document
        /// </summary>
        public ITableCollection Tables
        {
            get
            {
                Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "table", null, this.ClientPort);
                return new TableCollection(this.ClientPort, finder);
            }
        }

        /// <summary>
        /// Finds a table body using the specified Id.
        /// </summary>
        /// <param name="id">The id of the table body element being sought.</param>
        /// <returns>The table body element for the corresponding id, or null if none is found</returns>
        public ITableBody TableBody(string id)
        {
            return this.TableBody(Find.ById(id));
        }

        /// <summary>
        /// Finds a table body element using the specified regular expression to match the element id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the table body element being sought.</param>
        /// <returns>The table body element for the corresponding regular expression, or null if none is found</returns>
        public ITableBody TableBody(Regex id)
        {
            return this.TableBody(Find.ById(id));
        }

        /// <summary>
        /// Finds a table body element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the table body element being sought.</param>
        /// <returns>The table body element for the corresponding attribute constraint, or null if none is found</returns>
        public ITableBody TableBody(BaseConstraint findBy)
        {
            Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "tbody", findBy, this.ClientPort);
            return new TableBody(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Returns all the table body elements for the current document
        /// </summary>
        public ITableBodyCollection TableBodies
        {
            get
            {
                Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "tbody", null, this.ClientPort);
                return new TableBodyCollection(this.ClientPort, finder);
            }
        }

        /// <summary>
        /// Finds a table row using the specified Id.
        /// </summary>
        /// <param name="id">The id of the table row element being sought.</param>
        /// <returns>The table row element for the corresponding id, or null if none is found</returns>
        public ITableRow TableRow(string id)
        {
            return this.TableRow(Find.ById(id));
        }

        /// <summary>
        /// Finds a table row element using the specified regular expression to match the element id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the table row element being sought.</param>
        /// <returns>The table row element for the corresponding regular expression, or null if none is found</returns>
        public ITableRow TableRow(Regex id)
        {
            return this.TableRow(Find.ById(id));
        }

        /// <summary>
        /// Finds a table row element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the table row element being sought.</param>
        /// <returns>The table row element for the corresponding attribute constraint, or null if none is found</returns>
        public ITableRow TableRow(BaseConstraint findBy)
        {
            Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "tr", findBy, this.ClientPort);
            return new TableRow(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Returns all the table row elements for the current document
        /// </summary>
        public ITableRowCollection TableRows
        {
            get
            {
                Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "tr", null, this.ClientPort);
                return new TableRowCollection(this.ClientPort, finder);
            }
        }

        /// <summary>
        /// Finds a table cell using the specified Id.
        /// </summary>
        /// <param name="id">The id of the table cell element being sought.</param>
        /// <returns>The table cell element for the corresponding id, or null if none is found</returns>
        public ITableCell TableCell(string id)
        {
            return this.TableCell(Find.ById(id));
        }

        /// <summary>
        /// Finds a table cell element using the specified regular expression to match the element id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the table cell element being sought.</param>
        /// <returns>The table cell element for the corresponding regular expression, or null if none is found</returns>
        public ITableCell TableCell(Regex id)
        {
            return this.TableCell(Find.ById(id));
        }

        /// <summary>
        /// Finds a table cell element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the table cell element being sought.</param>
        /// <returns>The table cell element for the corresponding attribute constraint, or null if none is found</returns>
        public ITableCell TableCell(BaseConstraint findBy)
        {
            Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "td", findBy, this.ClientPort);
            return new TableCell(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Returns all the table cell elements for the current document
        /// </summary>
        public ITableCellCollection TableCells
        {
            get
            {
                Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "td", null, this.ClientPort);
                return new TableCellCollection(this.ClientPort, finder);
            }
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
        /// Finds a text field element using the specified <see cref="BaseConstraint" />.
        /// </summary>
        /// <param name="constraint">The <see cref="BaseConstraint" /> for the text field element being sought.</param>
        /// <returns>The text field element for the matches the <see cref="BaseConstraint" />, or null if none is found</returns>
        public ITextField TextField(BaseConstraint constraint)
        {
            Mozilla.ElementFinder finder = new Mozilla.ElementFinder(
                    this,
                    new List<ElementTag>(new ElementTag[] { new ElementTag("input", "text password textarea hidden"), new ElementTag("textarea") }), constraint, this.ClientPort);
            
            return new TextField(finder.FindFirst(), this.ClientPort);
        }

        /// <summary>
        /// Returns all the text fields elements for the current document
        /// </summary>
        public ITextFieldCollection TextFields
        {
            get
            {                
                Mozilla.ElementFinder finder = new Mozilla.ElementFinder(
                    this,
                    new List<ElementTag>(new ElementTag[] { new ElementTag("input", "text password textarea hidden"), new ElementTag("textarea") }), null, this.ClientPort);
             
                return new TextFieldCollection(this.ClientPort, finder);
            }
        }

        /// <summary>
        /// Finds an element matching the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public IElement Element(string id)
        {
            return this.Element(Find.ById(id));
        }

        /// <summary>
        /// Finds an element using the specified regular expression to match the elements's id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the element being sought.</param>
        /// <returns>The element for the corresponding regular expression, or null if none is found</returns>
        public IElement Element(Regex id)
        {
            return this.Element(Find.ById(id));
        }

        /// <summary>
        /// Finds an element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the element being sought.</param>
        /// <returns>The element for the corresponding attribute constraint, or null if none is found</returns>
        public IElement Element(BaseConstraint findBy)
        {
            Mozilla.ElementFinder finder = new Mozilla.ElementFinder(this, "*", findBy, this.ClientPort);
            return new Element(finder.FindFirst(), this.ClientPort);
        }

        #endregion

        #region Protected instance methods

        #endregion

    }
}
