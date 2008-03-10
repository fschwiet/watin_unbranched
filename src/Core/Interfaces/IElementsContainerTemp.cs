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
using WatiN.Core.Mozilla;
using WatiN.Core.Constraints;

namespace WatiN.Core.Interfaces
{
    /// <summary>
    /// Temp. interface, will be removed and <see cref="IDocument"/> will use <see cref="IElementsContainer"/>
    /// when the implementation of <see cref="Mozilla.Document" /> supports all of the functionality.
    /// </summary>
    public interface IElementsContainerTemp
    {
        IWatiNElementCollection Elements { get; } 

        /// <summary>
        /// Finds an area element using the specified id.
        /// </summary>
        /// <param name="id">The id of the area element being sought.</param>
        /// <returns>The area element for the corresponding id, or null if none is found</returns>
        IArea Area(string id);

        /// <summary>
        /// Finds a area element using the specified regular expression to match the element id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the area element being sought.</param>
        /// <returns>The area element for the corresponding regular expression, or null if none is found</returns>
        IArea Area(Regex id);

        /// <summary>
        /// Finds a area element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the area element being sought.</param>
        /// <returns>The area element for the corresponding attribute constraint, or null if none is found</returns>
        IArea Area(BaseConstraint findBy);

        /// <summary>
        /// Returns all the area elements for the current document
        /// </summary>
        IAreaCollection Areas { get;}

        /// <summary>
        /// Finds a button element using the specified id.
        /// </summary>
        /// <param name="id">The id of the button element being sought.</param>
        /// <returns>The button element for the corresponding id, or null if none is found</returns>
        IButton Button(string id);

        /// <summary>
        /// Finds a button element using the specified regular expression.
        /// </summary>
        /// <param name="regex">The regular expression for the id of the button element being sought.</param>
        /// <returns>The button element which id matches the regular expression, or null if none is found</returns>
        IButton Button(Regex regex);

        /// <summary>
        /// Finds a button element using the specified <see cref="BaseConstraint" />.
        /// </summary>
        /// <param name="constraint">The <see cref="BaseConstraint" /> for the button element being sought.</param>
        /// <returns>The button element for the matches the <see cref="BaseConstraint" />, or null if none is found</returns>
        IButton Button(BaseConstraint constraint);

        /// <summary>
        /// Returns all the button elements for the current document
        /// </summary>
        IButtonCollection Buttons { get; }

        /// <summary>
        /// Finds a checkbox element using the specified id.
        /// </summary>
        /// <param name="id">The id of the checkbox element being sought.</param>
        /// <returns>The checkbox element for the corresponding id, or null if none is found</returns>
        ICheckBox CheckBox(string id);

        /// <summary>
        /// Finds a checkbox element using the specified regular expression to match the element id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the checkbox element being sought.</param>
        /// <returns>The checkbox element for the corresponding regular expression, or null if none is found</returns>
        ICheckBox CheckBox(Regex id);

        /// <summary>
        /// Finds a checkbox element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the checkbox element being sought.</param>
        /// <returns>The checkbox element for the corresponding attribute constraint, or null if none is found</returns>
        ICheckBox CheckBox(BaseConstraint findBy);

        /// <summary>
        /// Returns all the checkbox elements for the current document
        /// </summary>
        ICheckBoxCollection CheckBoxes { get; }

        /// <summary>
        /// Finds a div element using the specified id.
        /// </summary>
        /// <param name="id">The id of the div element being sought.</param>
        /// <returns>The div element for the corresponding id, or null if none is found</returns>
        IDiv Div(string id);

        /// <summary>
        /// Finds a div element using the specified regular expression to match the div's id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the div element being sought.</param>
        /// <returns>The div element for the corresponding regular expression, or null if none is found</returns>
        IDiv Div(Regex id);

        /// <summary>
        /// Finds a div element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the div element being sought.</param>
        /// <returns>The div element for the corresponding attribute constraint, or null if none is found</returns>
        IDiv Div(BaseConstraint findBy);

        /// <summary>
        /// Returns all the div elements for the current document
        /// </summary>
        IDivCollection Divs { get; }

        /// <summary>
        /// Finds an element matching the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The element for the corresponding id, or null if none is found</returns>
        IElement Element(string id);

        /// <summary>
        /// Finds an element using the specified regular expression to match the elements's id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the element being sought.</param>
        /// <returns>The element for the corresponding regular expression, or null if none is found</returns>
        IElement Element(Regex id);

        /// <summary>
        /// Finds an element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the element being sought.</param>
        /// <returns>The element for the corresponding attribute constraint, or null if none is found</returns>
        IElement Element(BaseConstraint findBy);

        /// <summary>
        /// Finds a form element using the specified id.
        /// </summary>
        /// <param name="id">The id of the form element being sought.</param>
        /// <returns>The form element for the corresponding id, or null if none is found</returns>
        IForm Form(string id);

        /// <summary>
        /// Finds a form element using the specified regular expression to match the forms's id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the form element being sought.</param>
        /// <returns>The form element for the corresponding regular expression, or null if none is found</returns>
        IForm Form(Regex id);

        /// <summary>
        /// Finds a form element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the form element being sought.</param>
        /// <returns>The form element for the corresponding attribute constraint, or null if none is found</returns>
        IForm Form(BaseConstraint findBy);

        /// <summary>
        /// Returns all the form elements for the current document
        /// </summary>
        IFormsCollection Forms { get; }

        /// <summary>
        /// Finds a frame element using the specified id.
        /// </summary>
        /// <param name="id">The id of the frame element being sought.</param>
        /// <returns>The frame element for the corresponding id, or null if none is found</returns>
        IFrame Frame(string id);

        /// <summary>
        /// Finds a frame element using the specified regular expression to match the frame's id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the frame element being sought.</param>
        /// <returns>The frame element for the corresponding regular expression, or null if none is found</returns>
        IFrame Frame(Regex id);

        /// <summary>
        /// Finds a frame element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the frame element being sought.</param>
        /// <returns>The frame element for the corresponding attribute constraint, or null if none is found</returns>
        IFrame Frame(BaseConstraint findBy);

        /// <summary>
        /// Returns all the frame elements for the current document
        /// </summary>
        IFrameCollection Frames { get; }

        /// <summary>
        /// Finds an image element using the specified id.
        /// </summary>
        /// <param name="id">The id of the image element being sought.</param>
        /// <returns>The image element for the corresponding id, or null if none is found</returns>
        IImage Image(string id);

        /// <summary>
        /// Finds an image element using the specified regular expression to match the image's id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the image element being sought.</param>
        /// <returns>The image element for the corresponding regular expression, or null if none is found</returns>
        IImage Image(Regex id);

        /// <summary>
        /// Finds an image element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the image element being sought.</param>
        /// <returns>The image element for the corresponding attribute constraint, or null if none is found</returns>
        IImage Image(BaseConstraint findBy);

        /// <summary>
        /// Returns all the image elements for the current document
        /// </summary>
        IImageCollection Images { get; }

        /// <summary>
        /// Finds a label element using the specified id.
        /// </summary>
        /// <param name="id">The id of the label element being sought.</param>
        /// <returns>The label element for the corresponding id, or null if none is found</returns>
        ILabel Label(string id);

        /// <summary>
        /// Finds a label element using the specified regular expression to match the label's id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the label element being sought.</param>
        /// <returns>The label element for the corresponding regular expression, or null if none is found</returns>
        ILabel Label(Regex id);

        /// <summary>
        /// Finds a label element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the label element being sought.</param>
        /// <returns>The label element for the corresponding attribute constraint, or null if none is found</returns>
        ILabel Label(BaseConstraint findBy);

        /// <summary>
        /// Returns all the label elements for the current document
        /// </summary>
        ILabelCollection Labels { get; }

        /// <summary>
        /// Finds a link element using the specified id.
        /// </summary>
        /// <param name="id">The id of the link element being sought.</param>
        /// <returns>The link element for the corresponding id, or null if none is found</returns>
        ILink Link(string id);

        /// <summary>
        /// Finds a link element using the specified regular expression to match the link's id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the link element being sought.</param>
        /// <returns>The link element for the corresponding regular expression, or null if none is found</returns>
        ILink Link(Regex id);

        /// <summary>
        /// Finds a link element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the link element being sought.</param>
        /// <returns>The link element for the corresponding attribute constraint, or null if none is found</returns>
        ILink Link(BaseConstraint findBy);

        /// <summary>
        /// Returns all the label elements for the current document
        /// </summary>
        ILinkCollection Links { get; }

        /// <summary>
        /// Finds a paragraph element using the specified id.
        /// </summary>
        /// <param name="id">The id of the paragraph element being sought.</param>
        /// <returns>The paragraph element for the corresponding id, or null if none is found</returns>
        IPara Para(string id);

        /// <summary>
        /// Finds a paragraph element using the specified regular expression to match the paragraph's id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the paragraph element being sought.</param>
        /// <returns>The paragraph element for the corresponding regular expression, or null if none is found</returns>
        IPara Para(Regex id);

        /// <summary>
        /// Finds a paragraph element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the paragraph element being sought.</param>
        /// <returns>The paragraph element for the corresponding attribute constraint, or null if none is found</returns>
        IPara Para(BaseConstraint findBy);

        /// <summary>
        /// Returns all the paragraph elements for the current document
        /// </summary>
        IParaCollection Paras { get; }

        /// <summary>
        /// Finds a radio button element using the specified id.
        /// </summary>
        /// <param name="id">The id of the radio button element being sought.</param>
        /// <returns>The radio button element for the corresponding id, or null if none is found</returns>
        IRadioButton RadioButton(string id);

        /// <summary>
        /// Finds a radio button element using the specified regular expression to match the paragraph's id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the radio button element being sought.</param>
        /// <returns>The radio button element for the corresponding regular expression, or null if none is found</returns>
        IRadioButton RadioButton(Regex id);

        /// <summary>
        /// Finds a radio button element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the radio button element being sought.</param>
        /// <returns>The radio button element for the corresponding attribute constraint, or null if none is found</returns>
        IRadioButton RadioButton(BaseConstraint findBy);

        /// <summary>
        /// Returns all the radio button elements for the current document
        /// </summary>
        IRadioButtonCollection RadioButtons { get; }

        /// <summary>
        /// Finds a select element using the specified id.
        /// </summary>
        /// <param name="id">The id of the select element being sought.</param>
        /// <returns>The select element for the corresponding id, or null if none is found</returns>
        ISelectList SelectList(string id);

        /// <summary>
        /// Finds a select list element using the specified regular expression to match the select list's id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the select list element being sought.</param>
        /// <returns>The select list element for the corresponding regular expression, or null if none is found</returns>
        ISelectList SelectList(Regex id);

        /// <summary>
        /// Finds a select list element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the select list element being sought.</param>
        /// <returns>The select list element for the corresponding attribute constraint, or null if none is found</returns>
        ISelectList SelectList(BaseConstraint findBy);

        /// <summary>
        /// Returns all the select list elements for the current document
        /// </summary>
        ISelectListCollection SelectLists { get; }

        /// <summary>
        /// Finds a span element using the specified id.
        /// </summary>
        /// <param name="id">The id of the span element being sought.</param>
        /// <returns>The span element for the corresponding id, or null if none is found</returns>
        ISpan Span(string id);

        /// <summary>
        /// Finds a span element using the specified regular expression to match the element id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the span element being sought.</param>
        /// <returns>The span element for the corresponding regular expression, or null if none is found</returns>
        ISpan Span(Regex id);

        /// <summary>
        /// Finds a span element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the span element being sought.</param>
        /// <returns>The span element for the corresponding attribute constraint, or null if none is found</returns>
        ISpan Span(BaseConstraint findBy);

        /// <summary>
        /// Returns all the span elements for the current document
        /// </summary>
        ISpanCollection Spans { get; }

        /// <summary>
        /// Finds a table using the specified Id.
        /// </summary>
        /// <param name="id">The id of the table element being sought.</param>
        /// <returns>The table element for the corresponding id, or null if none is found</returns>
        ITable Table(string id);

        /// <summary>
        /// Finds a table element using the specified regular expression to match the element id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the table element being sought.</param>
        /// <returns>The table element for the corresponding regular expression, or null if none is found</returns>
        ITable Table(Regex id);

        /// <summary>
        /// Finds a table element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the table element being sought.</param>
        /// <returns>The table element for the corresponding attribute constraint, or null if none is found</returns>
        ITable Table(BaseConstraint findBy);

        /// <summary>
        /// Returns all the table elements for the current document
        /// </summary>
        ITableCollection Tables { get; }

        /// <summary>
        /// Finds a table body using the specified Id.
        /// </summary>
        /// <param name="id">The id of the table body element being sought.</param>
        /// <returns>The table body element for the corresponding id, or null if none is found</returns>
        ITableBody TableBody(string id);

        /// <summary>
        /// Finds a table body element using the specified regular expression to match the element id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the table body element being sought.</param>
        /// <returns>The table body element for the corresponding regular expression, or null if none is found</returns>
        ITableBody TableBody(Regex id);

        /// <summary>
        /// Finds a table body element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the table body element being sought.</param>
        /// <returns>The table body element for the corresponding attribute constraint, or null if none is found</returns>
        ITableBody TableBody(BaseConstraint findBy);

        /// <summary>
        /// Returns all the table body elements for the current document
        /// </summary>
        ITableBodyCollection TableBodies { get; }

        /// <summary>
        /// Finds a table row using the specified Id.
        /// </summary>
        /// <param name="id">The id of the table row element being sought.</param>
        /// <returns>The table row element for the corresponding id, or null if none is found</returns>
        ITableRow TableRow(string id);

        /// <summary>
        /// Finds a table row element using the specified regular expression to match the element id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the table row element being sought.</param>
        /// <returns>The table row element for the corresponding regular expression, or null if none is found</returns>
        ITableRow TableRow(Regex id);

        /// <summary>
        /// Finds a table row element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the table row element being sought.</param>
        /// <returns>The table row element for the corresponding attribute constraint, or null if none is found</returns>
        ITableRow TableRow(BaseConstraint findBy);

        /// <summary>
        /// Returns all the table row elements for the current document
        /// </summary>
        ITableRowCollection TableRows { get; }

        /// <summary>
        /// Finds a table cell using the specified Id.
        /// </summary>
        /// <param name="id">The id of the table cell element being sought.</param>
        /// <returns>The table cell element for the corresponding id, or null if none is found</returns>
        ITableCell TableCell(string id);

        /// <summary>
        /// Finds a table cell element using the specified regular expression to match the element id.
        /// </summary>
        /// <param name="id">The regular expression that matches the element id of the table cell element being sought.</param>
        /// <returns>The table cell element for the corresponding regular expression, or null if none is found</returns>
        ITableCell TableCell(Regex id);

        /// <summary>
        /// Finds a table cell element using the specified attribute constraint.
        /// </summary>
        /// <param name="findBy">The attibute contraint used to match an attribute of the table cell element being sought.</param>
        /// <returns>The table cell element for the corresponding attribute constraint, or null if none is found</returns>
        ITableCell TableCell(BaseConstraint findBy);

        /// <summary>
        /// Returns all the table cell elements for the current document
        /// </summary>
        ITableCellCollection TableCells { get; }

        /// <summary>
        /// Finds a text field using the Id.
        /// </summary>
        /// <param name="id">The id of the text field element being sought.</param>
        /// <returns>The text field element for the corresponding id, or null if none is found</returns>
        ITextField TextField(string id);

        /// <summary>
        /// Finds a text field element using the specified regular expression.
        /// </summary>
        /// <param name="regex">The regular expression for the id of the text field element being sought.</param>
        /// <returns>The text field element which id matches the regular expression, or null if none is found</returns>
        ITextField TextField(Regex regex);

        /// <summary>
        /// Finds a text field element using the specified <see cref="BaseConstraint" />.
        /// </summary>
        /// <param name="constraint">The <see cref="BaseConstraint" /> for the text field element being sought.</param>
        /// <returns>The text field element for the matches the <see cref="BaseConstraint" />, or null if none is found</returns>
        ITextField TextField(BaseConstraint constraint);

        /// <summary>
        /// Returns all the text fields elements for the current document
        /// </summary>
        ITextFieldCollection TextFields { get; }
    }
}
