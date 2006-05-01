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
      return SubElementsSupport.Button(Ie, findBy, elementCollection);
    }
  
    public ButtonCollection Buttons
    {
      get { return SubElementsSupport.Buttons(Ie, elementCollection); }
    }

    public CheckBox CheckBox(string elementID)
    {
      return this.CheckBox(Find.ByID(elementID));
    }

    public CheckBox CheckBox(AttributeValue findBy)
    {
      return SubElementsSupport.CheckBox(Ie, findBy, elementCollection);
    }

    public CheckBoxCollection CheckBoxs
    {
      get { return SubElementsSupport.Checkboxes(Ie, elementCollection); }
    }

    public Form Form(string elementID)
    {
      return this.Form(Find.ByID(elementID));
    }

    public Form Form(AttributeValue findBy)
    {
      return SubElementsSupport.Form(Ie, findBy, elementCollection);
    }

	  public FormCollection Forms
	  {
	    get { return SubElementsSupport.Forms(Ie, elementCollection); }
	  }

	  public Label Label(string elementID)
	  {
      return this.Label(Find.ByID(elementID));
    }

	  public Label Label(AttributeValue findBy)
	  {
      return SubElementsSupport.Label(Ie, findBy, elementCollection);
    }

	  public LabelCollection Labels
	  {
      get { return SubElementsSupport.Labels(Ie, elementCollection); }
    }

	  public Link Link(string elementID)
    {
      return this.Link(Find.ByID(elementID));
    }

    public Link Link(AttributeValue findBy)
    {
      return SubElementsSupport.Link(Ie, findBy, elementCollection);
    }

    public LinkCollection Links
    {
      get { return SubElementsSupport.Links(Ie, elementCollection); }
    }

	  public Para Para(string elementID)
	  {
	    return this.Para(Find.ByID(elementID));
	  }

	  public Para Para(AttributeValue findBy)
	  {
	    return SubElementsSupport.Para(Ie, findBy, elementCollection);
	  }

	  public ParaCollection Paras
	  {
	    get { return SubElementsSupport.Paras(Ie, elementCollection); }
	  }

	  public RadioButton RadioButton(string elementID)
	  {
	    return this.RadioButton(Find.ByID(elementID));
	  }

	  public RadioButton RadioButton(AttributeValue findBy)
	  {
      return SubElementsSupport.RadioButton(Ie, findBy, elementCollection);
    }

	  public RadioButtonCollection RadioButtons
	  {
      get { return SubElementsSupport.RadioButtons(Ie, elementCollection); }
    }

	  public SelectList SelectList(string elementID)
    {
      return this.SelectList(Find.ByID(elementID));
    }

    public SelectList SelectList(AttributeValue findBy)
    {
      return SubElementsSupport.SelectList(Ie, findBy, elementCollection);
    }

    public SelectListCollection SelectLists
    {
      get { return SubElementsSupport.SelectLists(Ie, elementCollection); }
    }

    public Table Table(string elementID)
    {
      return this.Table(Find.ByID(elementID));
    }

    public Table Table(AttributeValue findBy)
    {
      return SubElementsSupport.Table(Ie, findBy, elementCollection);
    }

    public TableCollection Tables
    {
      get { return SubElementsSupport.Tables(Ie, elementCollection); }
    }

    //    public TableSectionCollection TableSections
    //    {
    //      get { return SubElementsSupport.TableSections(Ie, elementCollection); }
    //    }

    public TableCell TableCell(string elementID)
    {
      return this.TableCell(Find.ByID(elementID));
    }

    public TableCell TableCell(AttributeValue findBy)
    {
      return SubElementsSupport.TableCell(Ie, findBy, elementCollection);
    }

    public TableCell TableCell(string elementId, int occurence)
    {
      return SubElementsSupport.TableCell(Ie, elementId, occurence, elementCollection);
    }

    public TableCellCollection TableCells
    {
      get { return SubElementsSupport.TableCells(Ie, elementCollection); }
    }

    public TableRow TableRow(string elementID)
    {
      return this.TableRow(Find.ByID(elementID));
    }

    public TableRow TableRow(AttributeValue findBy)
    {
      return SubElementsSupport.TableRow(Ie, findBy, elementCollection);
    }

    public virtual TableRowCollection TableRows
    {
      get { return SubElementsSupport.TableRows(Ie,elementCollection); }
    }

    public TextField TextField(string elementID)
    {
      return this.TextField(Find.ByID(elementID));
    }

    public TextField TextField(AttributeValue findBy)
    {
      return SubElementsSupport.TextField(Ie, findBy, elementCollection);
    }

    public TextFieldCollection TextFields
    {
      get { return SubElementsSupport.TextFields(Ie, elementCollection); }
    }

	  public Span Span(string elementID)
	  {
	    return this.Span(Find.ByID(elementID));
	  }

	  public Span Span(AttributeValue findBy)
	  {
      return SubElementsSupport.Span(Ie, findBy, elementCollection);
    }

	  public SpanCollection Spans
    {
      get { return SubElementsSupport.Spans(Ie, elementCollection); }
    }

	  public Div Div(string elementID)
	  {
	    return this.Div(Find.ByID(elementID));
	  }

	  public Div Div(AttributeValue findBy)
	  {
      return SubElementsSupport.Div(Ie, findBy, elementCollection);
    }

	  public DivCollection Divs
    {
      get { return SubElementsSupport.Divs(Ie, elementCollection); }
    }

	  public Image Image(string elementID)
	  {
	    return this.Image(Find.ByID(elementID));
	  }

	  public Image Image(AttributeValue findBy)
	  {
      return SubElementsSupport.Image(Ie, findBy, elementCollection);
    }

	  public ImageCollection Images
    {
      get { return SubElementsSupport.Images(Ie, elementCollection); }
    }
    #endregion

    private IHTMLElementCollection elementCollection
    {
      get
      {
        return (IHTMLElementCollection)((IHTMLElement)element).all;
      }
    }
	}
}
