using mshtml;

namespace WatiN
{
	/// <summary>
	/// Summary description for ElementsContainer.
	/// </summary>
	public class ElementsContainer : Element, ISubElements
	{
	  public ElementsContainer(DomContainer ie, object element): base(ie, element) 
		{}

    #region ISubElements

    public Button Button(string elementID)
    {
      return this.Button(Find.ByID(elementID));
    }

    public Button Button(AttributeValue findBy)
    {
      return SubElementsSupport.Button(base.Ie, findBy, elementCollection);
    }

    public ButtonCollection Buttons
    {
      get { return SubElementsSupport.Buttons(base.Ie, elementCollection); }
    }

    public CheckBox CheckBox(string elementID)
    {
      return this.CheckBox(Find.ByID(elementID));
    }

    public CheckBox CheckBox(AttributeValue findBy)
    {
      return SubElementsSupport.CheckBox(base.Ie, findBy, elementCollection);
    }

    public CheckBoxCollection CheckBoxs
    {
      get { return SubElementsSupport.Checkboxes(base.Ie, elementCollection); }
    }

    public Form Form(string elementID)
    {
      return this.Form(Find.ByID(elementID));
    }

    public Form Form(AttributeValue findBy)
    {
      return SubElementsSupport.Form(base.Ie, findBy, elementCollection);
    }

	  public FormCollection Forms
	  {
	    get { return SubElementsSupport.Forms(base.Ie, elementCollection); }
	  }

	  public Link Link(string elementID)
    {
      return this.Link(Find.ByID(elementID));
    }

    public Link Link(AttributeValue findBy)
    {
      return SubElementsSupport.Link(base.Ie, findBy, elementCollection);
    }

    public LinkCollection Links
    {
      get { return SubElementsSupport.Links(base.Ie, elementCollection); }
    }

	  public Para Para(string elementID)
	  {
	    return this.Para(Find.ByID(elementID));
	  }

	  public Para Para(AttributeValue findBy)
	  {
	    return SubElementsSupport.Para(base.Ie, findBy, elementCollection);
	  }

	  public ParaCollection Paras
	  {
	    get { return SubElementsSupport.Paras(base.Ie, elementCollection); }
	  }

	  public RadioButton RadioButton(string elementID)
	  {
	    return this.RadioButton(Find.ByID(elementID));
	  }

	  public RadioButton RadioButton(AttributeValue findBy)
	  {
      return SubElementsSupport.RadioButton(base.Ie, findBy, elementCollection);
    }

	  public RadioButtonCollection RadioButtons
	  {
      get { return SubElementsSupport.RadioButtons(base.Ie, elementCollection); }
    }

	  public SelectList SelectList(string elementID)
    {
      return this.SelectList(Find.ByID(elementID));
    }

    public SelectList SelectList(AttributeValue findBy)
    {
      return SubElementsSupport.SelectList(base.Ie, findBy, elementCollection);
    }

    public SelectListCollection SelectLists
    {
      get { return SubElementsSupport.SelectLists(base.Ie, elementCollection); }
    }

    public Table Table(string elementID)
    {
      return this.Table(Find.ByID(elementID));
    }

    public Table Table(AttributeValue findBy)
    {
      return SubElementsSupport.Table(base.Ie, findBy, elementCollection);
    }

    public TableCollection Tables
    {
      get { return SubElementsSupport.Tables(base.Ie, elementCollection); }
    }

    //    public TableSectionCollection TableSections
    //    {
    //      get { return SubElementsSupport.TableSections(base.Ie, elementCollection); }
    //    }

    public TableCell TableCell(string elementID)
    {
      return this.TableCell(Find.ByID(elementID));
    }

    public TableCell TableCell(AttributeValue findBy)
    {
      return SubElementsSupport.TableCell(base.Ie, findBy, elementCollection);
    }

    public TableCell TableCell(string elementId, int occurence)
    {
      return SubElementsSupport.TableCell(base.Ie, elementId, occurence, elementCollection);
    }

    public TableCellCollection TableCells
    {
      get { return SubElementsSupport.TableCells(base.Ie, elementCollection); }
    }

    public TableRow TableRow(string elementID)
    {
      return this.TableRow(Find.ByID(elementID));
    }

    public TableRow TableRow(AttributeValue findBy)
    {
      return SubElementsSupport.TableRow(base.Ie, findBy, elementCollection);
    }

    public virtual TableRowCollection TableRows
    {
      get { return SubElementsSupport.TableRows(base.Ie,elementCollection); }
    }

    public TextField TextField(string elementID)
    {
      return this.TextField(Find.ByID(elementID));
    }

    public TextField TextField(AttributeValue findBy)
    {
      return SubElementsSupport.TextField(base.Ie, findBy, elementCollection);
    }

    public TextFieldCollection TextFields
    {
      get { return SubElementsSupport.TextFields(base.Ie, elementCollection); }
    }

	  public Span Span(string elementID)
	  {
	    return this.Span(Find.ByID(elementID));
	  }

	  public Span Span(AttributeValue findBy)
	  {
      return SubElementsSupport.Span(base.Ie, findBy, elementCollection);
    }

	  public SpanCollection Spans
    {
      get { return SubElementsSupport.Spans(base.Ie, elementCollection); }
    }

	  public Div Div(string elementID)
	  {
	    return this.Div(Find.ByID(elementID));
	  }

	  public Div Div(AttributeValue findBy)
	  {
      return SubElementsSupport.Div(base.Ie, findBy, elementCollection);
    }

	  public DivCollection Divs
    {
      get { return SubElementsSupport.Divs(base.Ie, elementCollection); }
    }

	  public Image Image(string elementID)
	  {
	    return this.Image(Find.ByID(elementID));
	  }

	  public Image Image(AttributeValue findBy)
	  {
      return SubElementsSupport.Image(base.Ie, findBy, elementCollection);
    }

	  public ImageCollection Images
    {
      get { return SubElementsSupport.Images(base.Ie, elementCollection); }
    }
    #endregion

    private IHTMLElementCollection elementCollection
    {
      get
      {
        return (IHTMLElementCollection)((IHTMLElement)base.element).all;
      }
    }

	}
}
