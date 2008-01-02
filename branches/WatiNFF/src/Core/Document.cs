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
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Expando;
using System.Text.RegularExpressions;
using System.Threading;
using mshtml;
using WatiN.Core.Exceptions;
using WatiN.Core.Interfaces;

namespace WatiN.Core
{
	/// <summary>
	/// This class gives access to all contained elements of the webpage or the
	/// frames within this webpage.
	/// </summary>
	///     /// <example>
	/// This example opens a webpage, types some text and submits it by clicking
	/// the submit button.
	/// <code>
	/// using WatiN.Core;
	/// 
	/// namespace NewIEExample
	/// {
	///    public class WatiNWebsite
	///    {
	///      public WatiNWebsite()
	///      {
	///        IE ie = new IE("http://www.example.net");
	/// 
	///        ie.TextField(Find.ById("textFieldComment")).TypeText("This is a comment to submit");
	///        ie.Button(Find.ByText("Submit")).Click;
	/// 
	///        ie.Close;
	///      }
	///    }
	///  }
	/// </code>
	/// </example>
	public abstract class Document : IElementsContainer, IDisposable, IElementCollection, IDocument
	{
		private DomContainer domContainer;
		private IHTMLDocument2 htmlDocument;

		/// <summary>
		/// Initializes a new instance of the <see cref="Document"/> class.
		/// Mainly used by WatiN internally. You should override HtmlDocument
		/// and set DomContainer before accessing any method or property of 
		/// this class.
		/// </summary>
		public Document() {}

		/// <summary>
		/// Initializes a new instance of the <see cref="Document"/> class.
		/// Mainly used by WatiN internally.
		/// </summary>
		/// <param name="domContainer">The DOM container.</param>
		/// <param name="htmlDocument">The HTML document.</param>
		public Document(DomContainer domContainer, IHTMLDocument2 htmlDocument)
		{
			ArgumentRequired(domContainer, "domContainer");
			ArgumentRequired(htmlDocument, "htmlDocument");

			DomContainer = domContainer;
			this.htmlDocument = htmlDocument;
		}

		public void Dispose()
		{
			Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			DomContainer = null;
			htmlDocument = null;
		}

		/// <summary>
		/// Gives access to the wrapped IHTMLDocument2 interface. This makes it
		/// possible to get even more control of the webpage by using the MSHTML
		/// Dom objectmodel.
		/// </summary>
		/// <value>The HTML document.</value>
		public virtual IHTMLDocument2 HtmlDocument
		{
			get { return htmlDocument; }
		}

		/// <summary>
		/// Gets the HTML of the Body part of the webpage.
		/// </summary>
		/// <value>The HTML.</value>
		public string Html
		{
			get
			{
				IHTMLElement body = HtmlDocument.body;

				if (body == null) return null;

				return body.outerHTML;
			}
		}

		/// <summary>
		/// Gets the inner text of the Body part of the webpage.
		/// </summary>
		/// <value>The inner text.</value>
		public string Text
		{
			get
			{
				IHTMLElement body = HtmlDocument.body;

				if (body == null) return null;

				return body.innerText;
			}
		}

		/// <summary>
		/// Returns a System.Uri instance of the url displayed in the address bar of the browser, 
		/// of the currently displayed web page.
		/// </summary>
		/// <example>
		/// The following example creates a new Internet Explorer instances, navigates to
		/// the WatiN Project website on SourceForge and writes the Uri of the
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
		///        Debug.WriteLine(ie.Uri.ToString());
		///      }
		///    }
		///  }
		/// </code>
		/// </example>
		public Uri Uri
		{
			get { return new Uri(HtmlDocument.url); }
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
			get { return HtmlDocument.url; }
		}

		/// <summary>
		/// Determines whether the text inside the HTML Body element contains the given <paramref name="text" />.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns>
		///     <c>true</c> if the specified text is contained in <see cref="Html"/>; otherwise, <c>false</c>.
		/// </returns>
		public bool ContainsText(string text)
		{
			string innertext = Text;

			if (innertext == null) return false;

			return (innertext.IndexOf(text) >= 0);
		}

		/// <summary>
		/// Determines whether the text inside the HTML Body element contains the given <paramref name="regex" />.
		/// </summary>
		/// <param name="regex">The regular expression to match with.</param>
		/// <returns>
		///     <c>true</c> if the specified text is contained in <see cref="Html"/>; otherwise, <c>false</c>.
		/// </returns>
		public bool ContainsText(Regex regex)
		{
			string innertext = Text;

			if (innertext == null) return false;

			return (regex.Match(innertext).Success);
		}


        /// <summary>
        /// Waits until the text is inside the HTML Body element contains the given <paramref name="text" />.
        /// Will time out after <see name="IE.Settings.WaitUntilExistsTimeOut" />
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        ///     <see name="TimeoutException"/> if the specified text is not found within the time out period.
        /// </returns>
        public void WaitUntilContainsText(string text)
        {
            SimpleTimer timer = new SimpleTimer(IE.Settings.WaitUntilExistsTimeOut);

            do
            {
                if (ContainsText(text)) { return; }

                Thread.Sleep(50);
            } while (!timer.Elapsed);

            throw new WatiN.Core.Exceptions.TimeoutException(string.Format("waiting {0} seconds for document to contain text '{1}'.", IE.Settings.WaitUntilExistsTimeOut, text));
        }

        /// <summary>
        /// Waits until the <paramref name="regex" /> matches some text inside the HTML Body element contains the given <paramref name="text" />.
        /// Will time out after <see name="IE.Settings.WaitUntilExistsTimeOut" />
        /// </summary>
        /// <param name="regex">The regular expression to match with.</param>
        /// <returns>
        ///     <see name="TimeoutException"/> if the specified text is not found within the time out period.
        /// </returns>
        public void WaitUntilContainsText(Regex regex)
        {
            SimpleTimer timer = new SimpleTimer(IE.Settings.WaitUntilExistsTimeOut);

            do
            {
                if (ContainsText(regex)) { return; }

                Thread.Sleep(50);
            } while (!timer.Elapsed);

            throw new WatiN.Core.Exceptions.TimeoutException(string.Format("waiting {0} seconds for document to contain regex '{1}'.", IE.Settings.WaitUntilExistsTimeOut, regex));
        }

		/// <summary>
		/// Gets the text inside the HTML Body element that matches the regular expression.
		/// </summary>
		/// <param name="regex">The regular expression to match with.</param>
		/// <returns>The matching text, or null if none.</returns>
		public string FindText(Regex regex)
		{
			Match match = regex.Match(Text);

			return match.Success ? match.Value : null;
		}

		/// <summary>
		/// Gets the title of the webpage.
		/// </summary>
		/// <value>The title.</value>
		public string Title
		{
			get { return HtmlDocument.title; }
		}

	    /// <summary>
		/// Gets the active element in this webpage.
		/// </summary>
		/// <value>The active element or <c>null</c> if no element has the focus.</value>
		public IElement ActiveElement
		{
			get
			{
				IHTMLElement activeElement = HtmlDocument.activeElement;
				if (activeElement != null)
				{
					return Core.Element.GetTypedElement(domContainer, activeElement);
				}
				return null;
			}
		}

		/// <summary>
		/// Gets the specified frame by its id.
		/// </summary>
		/// <param name="id">The id of the frame.</param>
		/// <exception cref="FrameNotFoundException">Thrown if the given <paramref name="id" /> isn't found.</exception>
		public Frame Frame(string id)
		{
			return Frame(Find.ById(id));
		}

		/// <summary>
		/// Gets the specified frame by its id.
		/// </summary>
		/// <param name="id">The regular expression to match with the id of the frame.</param>
		/// <exception cref="FrameNotFoundException">Thrown if the given <paramref name="id" /> isn't found.</exception>
		public Frame Frame(Regex id)
		{
			return Frame(Find.ById(id));
		}

		/// <summary>
		/// Gets the specified frame by its name.
		/// </summary>
		/// <param name="findBy">The name of the frame.</param>
		/// <exception cref="FrameNotFoundException">Thrown if the given name isn't found.</exception>
		public Frame Frame(AttributeConstraint findBy)
		{
			return Core.Frame.Find(Frames, findBy);
		}

		/// <summary>
		/// Gets a typed collection of <see cref="WatiN.Core.Frame"/> opend within this <see cref="Document"/>.
		/// </summary>
		public FrameCollection Frames
		{
			get { return new FrameCollection(DomContainer, HtmlDocument); }
		}

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

        IElement IElementsContainerTemp.Element(Regex elementId)
        {
            return Element(Find.ById(elementId));
        }

        IElement IElementsContainerTemp.Element(AttributeConstraint findBy)
        {
            return ElementsSupport.Element(DomContainer, findBy, this);
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

        IImage IElementsContainerTemp.Image(Regex elementId)
        {
            return Image(Find.ById(elementId));
        }

        IImage IElementsContainerTemp.Image(AttributeConstraint findBy)
        {
            return ElementsSupport.Image(DomContainer, findBy, this);
        }

        IImageCollection IElementsContainerTemp.Images
        {
            get { return ElementsSupport.Images(DomContainer, this); }
        }

        ILabel IElementsContainerTemp.Label(Regex elementId)
        {
            return Label(Find.ById(elementId));
        }

        ILabel IElementsContainerTemp.Label(AttributeConstraint findBy)
        {
            return ElementsSupport.Label(DomContainer, findBy, this);
        }

        ILabelCollection IElementsContainerTemp.Labels
        {
            get { return ElementsSupport.Labels(DomContainer, this); }
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

		public TableBody TableBody(string elementId)
		{
			return TableBody(Find.ById(elementId));
		}

		public TableBody TableBody(Regex elementId)
		{
			return TableBody(Find.ById(elementId));
		}

		public TableBody TableBody(AttributeConstraint findBy)
		{
			return ElementsSupport.TableBody(DomContainer, findBy, this);
		}

		public TableBodyCollection TableBodies
		{
			get { return ElementsSupport.TableBodies(DomContainer, this); }
		}

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

		public TableRow TableRow(AttributeConstraint findBy)
		{
			return ElementsSupport.TableRow(DomContainer, findBy, this);
		}

		public ITableRowCollection TableRows
		{
			get { return ElementsSupport.TableRows(DomContainer, this); }
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

        IArea IElementsContainerTemp.Area(string elementId)
        {
            return Area(Find.ById(elementId));
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

        IForm IElementsContainerTemp.Form(string id)
        {
            return Form(Find.ById(id));
        }

        IForm IElementsContainerTemp.Form(Regex elementId)
        {
            return Form(Find.ById(elementId));
        }

        IForm IElementsContainerTemp.Form(AttributeConstraint findBy)
        {
            return ElementsSupport.Form(DomContainer, findBy, this);
        }

        IFormsCollection IElementsContainerTemp.Forms
        {
            get { return ElementsSupport.Forms(DomContainer, this); }
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

        ITable IElementsContainerTemp.Table(Regex id)
        {
            return Table(Find.ById(id));
        }

        ITable IElementsContainerTemp.Table(AttributeConstraint findBy)
        {
            return ElementsSupport.Table(DomContainer, findBy, this);
        }

        ITableCollection IElementsContainerTemp.Tables
        {
            get { return ElementsSupport.Tables(DomContainer, this); }
        }

        ITableBody IElementsContainerTemp.TableBody(string id)
        {
            return TableBody(Find.ById(id));
        }

        ITableBody IElementsContainerTemp.TableBody(Regex id)
        {
            return TableBody(Find.ById(id));
        }

        ITableBody IElementsContainerTemp.TableBody(AttributeConstraint findBy)
        {
            return ElementsSupport.TableBody(DomContainer, findBy, this);
        }

        ITableBodyCollection IElementsContainerTemp.TableBodies
        {
            get { return ElementsSupport.TableBodies(DomContainer, this); }
        }

        ITableRow IElementsContainerTemp.TableRow(string id)
        {
            return TableRow(Find.ById(id));
        }

        ITableRow IElementsContainerTemp.TableRow(Regex id)
        {
            return TableRow(Find.ById(id));
        }

        ITableRow IElementsContainerTemp.TableRow(AttributeConstraint findBy)
        {
            return ElementsSupport.TableRow(DomContainer, findBy, this);
        }

        ITableRowCollection IElementsContainerTemp.TableRows
        {
            get { return ElementsSupport.TableRows(DomContainer, this); }
        }

        ITableCell IElementsContainerTemp.TableCell(string id)
        {
            return TableCell(Find.ById(id));
        }

        ITableCell IElementsContainerTemp.TableCell(Regex id)
        {
            return TableCell(Find.ById(id));
        }

        ITableCell IElementsContainerTemp.TableCell(AttributeConstraint findBy)
        {
            return ElementsSupport.TableCell(DomContainer, findBy, this);
        }

        ITableCellCollection IElementsContainerTemp.TableCells
        {
            get { return ElementsSupport.TableCells(DomContainer, this); }
        }

        IWatiNElementCollection IElementsContainerTemp.Elements
        {
            get { return ElementsSupport.Elements(DomContainer, this); }
        }

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

        ILink IElementsContainerTemp.Link(Regex elementId)
        {
            return Link(Find.ById(elementId));
        }

        ILink IElementsContainerTemp.Link(AttributeConstraint findBy)
        {
            return ElementsSupport.Link(DomContainer, findBy, this);
        }

        ILinkCollection IElementsContainerTemp.Links
        {
            get { return ElementsSupport.Links(DomContainer, this); }
        }

        IImage IElementsContainerTemp.Image(string id)
        {
            return Image(Find.ById(id));
        }

        IPara IElementsContainerTemp.Para(string elementId)
        {
            return Para(Find.ById(elementId));
        }

        IPara IElementsContainerTemp.Para(Regex elementId)
        {
            return Para(Find.ById(elementId));
        }

        IPara IElementsContainerTemp.Para(AttributeConstraint findBy)
        {
            return ElementsSupport.Para(DomContainer, findBy, this);
        }

        IParaCollection IElementsContainerTemp.Paras
        {
            get { return ElementsSupport.Paras(DomContainer, this); }
        }

        IRadioButton IElementsContainerTemp.RadioButton(string elementId)
        {
            return RadioButton(Find.ById(elementId));
        }

        IRadioButton IElementsContainerTemp.RadioButton(Regex elementId)
        {
            return RadioButton(Find.ById(elementId));
        }

        IRadioButton IElementsContainerTemp.RadioButton(AttributeConstraint findBy)
        {
            return ElementsSupport.RadioButton(DomContainer, findBy, this);
        }

        IRadioButtonCollection IElementsContainerTemp.RadioButtons
        {
            get { return ElementsSupport.RadioButtons(DomContainer, this); }
        }

        ISelectList IElementsContainerTemp.SelectList(Regex elementId)
        {
            return SelectList(Find.ById(elementId));
        }

        ISelectList IElementsContainerTemp.SelectList(AttributeConstraint findBy)
        {
            return ElementsSupport.SelectList(DomContainer, findBy, this);
        }

        ISelectListCollection IElementsContainerTemp.SelectLists
        {
            get { return ElementsSupport.SelectLists(DomContainer, this); }
        }

        ISpanCollection IElementsContainerTemp.Spans
        {
            get { return ElementsSupport.Spans(DomContainer, this); }
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

        ITextFieldCollection IElementsContainerTemp.TextFields
        {
            get { return ElementsSupport.TextFields(DomContainer, this); }
        }

        #endregion

        protected DomContainer DomContainer
		{
			get { return domContainer; }
			set { domContainer = value; }
		}

		IHTMLElementCollection IElementCollection.Elements
		{
			get
			{
				try
				{
					return HtmlDocument.all;
				}
				catch
				{
					return null;
				}
			}
		}

		private static void ArgumentRequired(object requiredObject, string name)
		{
			if (requiredObject == null)
			{
				throw new ArgumentNullException(name);
			}
		}

		/// <summary>
		/// Runs the javascript code in IE.
		/// </summary>
		/// <param name="javaScriptCode">The javascript code.</param>
		public void RunScript(string javaScriptCode)
		{
			RunScript(javaScriptCode, "javascript");
		}

		/// <summary>
		/// Runs the script code in IE.
		/// </summary>
		/// <param name="scriptCode">The script code.</param>
		/// <param name="language">The language.</param>
		public void RunScript(string scriptCode, string language)
		{
			UtilityClass.RunScript(scriptCode, language, HtmlDocument.parentWindow);
		}

		/// <summary>
		/// Fires the given event on the given element.
		/// </summary>
		/// <param name="element">Element to fire the event on</param>
		/// <param name="eventName">Name of the event to fire</param>
		public virtual void FireEvent(DispHTMLBaseElement element, string eventName)
		{
			UtilityClass.FireEvent(element, eventName);
		}

		/// <summary>
		/// Evaluates the specified JavaScript code within the scope of this
		/// document. Returns the value computed by the last expression in the
		/// code block after implicit conversion to a string.
		/// </summary>
		/// <example>
		/// The following example shows an alert dialog then returns the string "4".
		/// <code>
		/// Eval("window.alert('Hello world!'); 2 + 2");
		/// </code>
		/// </example>
		/// <param name="javaScriptCode">The JavaScript code</param>
		/// <returns>The result converted to a string</returns>
		/// <exception cref="JavaScriptException">Thrown when the JavaScript code cannot be evaluated
		/// or throws an exception during evaluation</exception>
		public string Eval(string javaScriptCode)
		{
			const string resultPropertyName = "___expressionResult___";
			const string errorPropertyName = "___expressionError___";

			string exprWithAssignment = "try {\n"
			                            + "document." + resultPropertyName + "= String(eval('" + javaScriptCode.Replace("'", "\\'") + "'))\n"
			                            + "} catch (error) {\n"
			                            + "document." + errorPropertyName + "= 'message' in error ? error.name + ': ' + error.message : String(error)\n"
			                            + "}";

			// Run the script.
			RunScript(exprWithAssignment);

			// See if an error occured.
			string error = GetPropertyValue(errorPropertyName);
			if (error != null)
			{
				throw new JavaScriptException(error);
			}

			// Return the result
			return GetPropertyValue(resultPropertyName);
		}

		private string GetPropertyValue(string propertyName)
		{
			IExpando domDocumentExpando = (IExpando) HtmlDocument;

			PropertyInfo errorProperty = domDocumentExpando.GetProperty(propertyName, BindingFlags.Default);
			if (errorProperty != null)
			{
				try
				{
					return (string) errorProperty.GetValue(domDocumentExpando, null);
				}
				catch (COMException)
				{
					return null;
				}
			}

			return null;
		}	    
	}
}
