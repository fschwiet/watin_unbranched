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

using System.Collections;
using System.Text.RegularExpressions;
using mshtml;
using WatiN.Core.Interfaces;

namespace WatiN.Core
{
	/// <summary>
	/// Summary description for ElementsContainer.
	/// </summary>
	public class ElementsContainer : Element, IElementsContainer, IElementsContainerTemp, IElementCollection
	{
		public ElementsContainer(DomContainer ie, object element) : base(ie, element) {}

		public ElementsContainer(DomContainer ie, ElementFinder finder) : base(ie, finder) {}

		public ElementsContainer(Element element, ArrayList elementTags) : base(element, elementTags) {}

		#region IElementsContainer

		public Area Area(string elementId)
		{
			return Area(Find.ById(elementId));
		}

		public Area Area(Regex elementId)
		{
			return Area(Find.ById(elementId));
		}

		public Area Area(AttributeConstraint findBy)
		{
			return ElementsSupport.Area(DomContainer, findBy, this);
		}

		public AreaCollection Areas
		{
			get { return ElementsSupport.Areas(DomContainer, this); }
		}

		public Button Button(string elementId)
		{
			return Button(Find.ById(elementId));
		}

		public Button Button(Regex elementId)
		{
			return Button(Find.ById(elementId));
		}

		public Button Button(AttributeConstraint findBy)
		{
			return ElementsSupport.Button(DomContainer, findBy, this);
		}

		public ButtonCollection Buttons
		{
			get { return ElementsSupport.Buttons(DomContainer, this); }
		}

		public CheckBox CheckBox(string elementId)
		{
			return CheckBox(Find.ById(elementId));
		}

		public CheckBox CheckBox(Regex elementId)
		{
			return CheckBox(Find.ById(elementId));
		}

		public CheckBox CheckBox(AttributeConstraint findBy)
		{
			return ElementsSupport.CheckBox(DomContainer, findBy, this);
		}

		public CheckBoxCollection CheckBoxes
		{
			get { return ElementsSupport.CheckBoxes(DomContainer, this); }
		}

		public Element Element(string elementId)
		{
			return Element(Find.ById(elementId));
		}

		public Element Element(Regex elementId)
		{
			return Element(Find.ById(elementId));
		}

		public Element Element(AttributeConstraint findBy)
		{
			return ElementsSupport.Element(DomContainer, findBy, this);
		}

		public Element Element(string tagname, AttributeConstraint findBy, params string[] inputtypes)
		{
			return ElementsSupport.Element(DomContainer, tagname, findBy, this, inputtypes);
		}

		public ElementCollection Elements
		{
			get { return ElementsSupport.Elements(DomContainer, this); }
		}

		public FileUpload FileUpload(string elementId)
		{
			return FileUpload(Find.ById(elementId));
		}

		public FileUpload FileUpload(Regex elementId)
		{
			return FileUpload(Find.ById(elementId));
		}

		public FileUpload FileUpload(AttributeConstraint findBy)
		{
			return ElementsSupport.FileUpload(DomContainer, findBy, this);
		}

		public FileUploadCollection FileUploads
		{
			get { return ElementsSupport.FileUploads(DomContainer, this); }
		}

		public Form Form(string elementId)
		{
			return Form(Find.ById(elementId));
		}

		public Form Form(Regex elementId)
		{
			return Form(Find.ById(elementId));
		}

		public Form Form(AttributeConstraint findBy)
		{
			return ElementsSupport.Form(DomContainer, findBy, this);
		}

		public FormCollection Forms
		{
			get { return ElementsSupport.Forms(DomContainer, this); }
		}

		public Label Label(string elementId)
		{
			return Label(Find.ById(elementId));
		}

		public Label Label(Regex elementId)
		{
			return Label(Find.ById(elementId));
		}

		public Label Label(AttributeConstraint findBy)
		{
			return ElementsSupport.Label(DomContainer, findBy, this);
		}

		public LabelCollection Labels
		{
			get { return ElementsSupport.Labels(DomContainer, this); }
		}

		public Link Link(string elementId)
		{
			return Link(Find.ById(elementId));
		}

		public Link Link(Regex elementId)
		{
			return Link(Find.ById(elementId));
		}

		public Link Link(AttributeConstraint findBy)
		{
			return ElementsSupport.Link(DomContainer, findBy, this);
		}

		public LinkCollection Links
		{
			get { return ElementsSupport.Links(DomContainer, this); }
		}

		public Para Para(string elementId)
		{
			return Para(Find.ById(elementId));
		}

		public Para Para(Regex elementId)
		{
			return Para(Find.ById(elementId));
		}

		public Para Para(AttributeConstraint findBy)
		{
			return ElementsSupport.Para(DomContainer, findBy, this);
		}

		public ParaCollection Paras
		{
			get { return ElementsSupport.Paras(DomContainer, this); }
		}

		public RadioButton RadioButton(string elementId)
		{
			return RadioButton(Find.ById(elementId));
		}

		public RadioButton RadioButton(Regex elementId)
		{
			return RadioButton(Find.ById(elementId));
		}

		public RadioButton RadioButton(AttributeConstraint findBy)
		{
			return ElementsSupport.RadioButton(DomContainer, findBy, this);
		}

		public RadioButtonCollection RadioButtons
		{
			get { return ElementsSupport.RadioButtons(DomContainer, this); }
		}

		public SelectList SelectList(string elementId)
		{
			return SelectList(Find.ById(elementId));
		}

		public SelectList SelectList(Regex elementId)
		{
			return SelectList(Find.ById(elementId));
		}

		public SelectList SelectList(AttributeConstraint findBy)
		{
			return ElementsSupport.SelectList(DomContainer, findBy, this);
		}

		public SelectListCollection SelectLists
		{
			get { return ElementsSupport.SelectLists(DomContainer, this); }
		}

		public Table Table(string elementId)
		{
			return Table(Find.ById(elementId));
		}

		public Table Table(Regex elementId)
		{
			return Table(Find.ById(elementId));
		}

		public Table Table(AttributeConstraint findBy)
		{
			return ElementsSupport.Table(DomContainer, findBy, this);
		}

		public TableCollection Tables
		{
			get { return ElementsSupport.Tables(DomContainer, this); }
		}

		//    public TableSectionCollection TableSections
		//    {
		//      get { return SubElementsSupport.TableSections(Ie, this); }
		//    }

		public TableCell TableCell(string elementId)
		{
			return TableCell(Find.ById(elementId));
		}

		public TableCell TableCell(Regex elementId)
		{
			return TableCell(Find.ById(elementId));
		}

		public TableCell TableCell(AttributeConstraint findBy)
		{
			return ElementsSupport.TableCell(DomContainer, findBy, this);
		}

		/// <summary>
		/// Finds a TableCell by the n-th index of an id. 
		/// index counting is zero based.
		/// </summary>  
		/// <example>
		/// This example will get the Text of the third(!) tablecell 
		/// with "tablecellid" as it's id value. 
		/// <code>ie.TableCell("tablecellid", 2).Text</code>
		/// </example>
		public TableCell TableCell(string elementId, int index)
		{
			return ElementsSupport.TableCell(DomContainer, elementId, index, this);
		}

		public TableCell TableCell(Regex elementId, int index)
		{
			return ElementsSupport.TableCell(DomContainer, elementId, index, this);
		}

		public ITableCellCollection TableCells
		{
			get { return ElementsSupport.TableCells(DomContainer, this); }
		}

		public TableRow TableRow(string elementId)
		{
			return TableRow(Find.ById(elementId));
		}

		public TableRow TableRow(Regex elementId)
		{
			return TableRow(Find.ById(elementId));
		}

		public virtual TableRow TableRow(AttributeConstraint findBy)
		{
			return ElementsSupport.TableRow(DomContainer, findBy, this);
		}

		public virtual ITableRowCollection TableRows
		{
			get { return ElementsSupport.TableRows(DomContainer, this); }
		}

		public TableBody TableBody(string elementId)
		{
			return TableBody(Find.ById(elementId));
		}

		public TableBody TableBody(Regex elementId)
		{
			return TableBody(Find.ById(elementId));
		}

		public virtual TableBody TableBody(AttributeConstraint findBy)
		{
			return ElementsSupport.TableBody(DomContainer, findBy, this);
		}

		public virtual TableBodyCollection TableBodies
		{
			get { return ElementsSupport.TableBodies(DomContainer, this); }
		}

		public TextField TextField(string elementId)
		{
			return TextField(Find.ById(elementId));
		}

		public TextField TextField(Regex elementId)
		{
			return TextField(Find.ById(elementId));
		}

		public TextField TextField(AttributeConstraint findBy)
		{
			return ElementsSupport.TextField(DomContainer, findBy, this);
		}

		public TextFieldCollection TextFields
		{
			get { return ElementsSupport.TextFields(DomContainer, this); }
		}

		public Span Span(string elementId)
		{
			return Span(Find.ById(elementId));
		}

		public Span Span(Regex elementId)
		{
			return Span(Find.ById(elementId));
		}

		public Span Span(AttributeConstraint findBy)
		{
			return ElementsSupport.Span(DomContainer, findBy, this);
		}

		public SpanCollection Spans
		{
			get { return ElementsSupport.Spans(DomContainer, this); }
		}

		public Div Div(string elementId)
		{
			return Div(Find.ById(elementId));
		}

		public Div Div(Regex elementId)
		{
			return Div(Find.ById(elementId));
		}

		public Div Div(AttributeConstraint findBy)
		{
			return ElementsSupport.Div(DomContainer, findBy, this);
		}

		public DivCollection Divs
		{
			get { return ElementsSupport.Divs(DomContainer, this); }
		}

		public Image Image(string elementId)
		{
			return Image(Find.ById(elementId));
		}

		public Image Image(Regex elementId)
		{
			return Image(Find.ById(elementId));
		}

		public Image Image(AttributeConstraint findBy)
		{
			return ElementsSupport.Image(DomContainer, findBy, this);
		}

		public ImageCollection Images
		{
			get { return ElementsSupport.Images(DomContainer, this); }
		}

		#endregion

        #region IElementsContainerTemp

        IElement IElementsContainerTemp.Element(string id)
        {
            return Element(Find.ById(id));
        }

        ILabel IElementsContainerTemp.Label(string id)
        {
            return Label(Find.ById(id));    
        }

        ILink IElementsContainerTemp.Link(string elementId)
        {
            return Link(Find.ById(elementId));
        }

        IImage IElementsContainerTemp.Image(string id)
        {
            return Image(Find.ById(id));
        }

        IPara IElementsContainerTemp.Para(string elementId)
        {
            return Para(Find.ById(elementId));
        }

	    IArea IElementsContainerTemp.Area(string id)
	    {
            return Area(Find.ById(id));
	    }

        IArea IElementsContainerTemp.Area(AttributeConstraint findBy)
        {
            return ElementsSupport.Area(DomContainer, findBy, this);
        }

        IArea IElementsContainerTemp.Area(Regex id)
        {
            return Area(Find.ById(id));
        }

        IAreaCollection IElementsContainerTemp.Areas
        {
            get
            {
                return ElementsSupport.Areas(DomContainer, this);
            }
        }

        IWatiNElementCollection IElementsContainerTemp.Elements
        {
            get { return ElementsSupport.Elements(DomContainer, this); }
        }

	    IDiv IElementsContainerTemp.Div(string id)
        {
            return Div(Find.ById(id));
        }

        ITextField IElementsContainerTemp.TextField(string id)
        {
            return TextField(Find.ById(id));
        }

        ITextField IElementsContainerTemp.TextField(Regex regex)
        {
            return TextField(Find.ById(regex));
        }

		ITextField IElementsContainerTemp.TextField(AttributeConstraint constraint)
        {
            return TextField(constraint);
        }

        IButton IElementsContainerTemp.Button(string id)
        {
            return Button(Find.ById(id));
        }

        IButton IElementsContainerTemp.Button(Regex regex)
        {
            return Button(Find.ById(regex));
        }

		IButton IElementsContainerTemp.Button(AttributeConstraint constraint)
        {
            return Button(constraint);
        }

        IButtonCollection IElementsContainerTemp.Buttons
        {
            get { return ElementsSupport.Buttons(DomContainer, this); }
        }

        ICheckBox IElementsContainerTemp.CheckBox(string elementId)
        {
            return CheckBox(Find.ById(elementId));
        }

        ICheckBox IElementsContainerTemp.CheckBox(Regex elementId)
        {
            return CheckBox(Find.ById(elementId));
        }

        ICheckBox IElementsContainerTemp.CheckBox(AttributeConstraint findBy)
        {
            return ElementsSupport.CheckBox(DomContainer, findBy, this);
        }

        ICheckBoxCollection IElementsContainerTemp.CheckBoxes
        {
            get { return ElementsSupport.CheckBoxes(DomContainer, this); }
        }

        IDiv IElementsContainerTemp.Div(Regex elementId)
        {
            return Div(Find.ById(elementId));
        }

        IDiv IElementsContainerTemp.Div(AttributeConstraint findBy)
        {
            return ElementsSupport.Div(DomContainer, findBy, this);
        }

        IDivCollection IElementsContainerTemp.Divs
        {
            get { return ElementsSupport.Divs(DomContainer, this); }
        }

        IForm IElementsContainerTemp.Form(string id)
        {
            return Form(Find.ById(id));
        }

        ISelectList IElementsContainerTemp.SelectList(string id)
        {
            return SelectList(Find.ById(id));
        }

        ISpan IElementsContainerTemp.Span(string id)
        {
            return Span(Find.ById(id));
        }

        ISpan IElementsContainerTemp.Span(Regex id)
        {
            return Span(Find.ById(id));
        }

        ISpan IElementsContainerTemp.Span(AttributeConstraint findBy)
        {
            return ElementsSupport.Span(DomContainer, findBy, this);
        }

        ITable IElementsContainerTemp.Table(string id)
        {
            return Table(Find.ById(id));
        }

        ITableBody IElementsContainerTemp.TableBody(string id)
        {
            return TableBody(Find.ById(id));
        }

        ITableRow IElementsContainerTemp.TableRow(string id)
        {
            return TableRow(Find.ById(id));
        }

        ITableCell IElementsContainerTemp.TableCell(string id)
        {
            return TableCell(Find.ById(id));
        }

        #endregion

		IHTMLElementCollection IElementCollection.Elements
		{
			get
			{
				try
				{
					if (Exists)
					{
						return (IHTMLElementCollection) htmlElement.all;
					}

					return null;
				}
				catch
				{
					return null;
				}
			}
		}
	}
}
