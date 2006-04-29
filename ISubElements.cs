namespace WatiN
{
  public interface ISubElements
  {
    Button Button(string elementID);
    Button Button(AttributeValue findBy);
    ButtonCollection Buttons { get; }

    CheckBox CheckBox(string elementID);
    CheckBox CheckBox(AttributeValue findBy);
    CheckBoxCollection CheckBoxs { get; }

    Form Form(string elementID);
    Form Form(AttributeValue findBy);
    FormCollection Forms { get; }

    Link Link(string elementID);
    Link Link(AttributeValue findBy);
    LinkCollection Links { get; }

    Para Para(string elementID);
    Para Para(AttributeValue findBy);
    ParaCollection Paras { get; }

    RadioButton RadioButton(string elementID);
    RadioButton RadioButton(AttributeValue findBy);
    RadioButtonCollection RadioButtons { get; }

    SelectList SelectList(string elementID);
    SelectList SelectList(AttributeValue findBy);
    SelectListCollection SelectLists { get; }

    Table Table(string elementID);
    Table Table(AttributeValue findBy);
    TableCollection Tables { get; }
//    TableSectionCollection TableSections { get; }

    TableCell TableCell(string elementID);
    TableCell TableCell(AttributeValue findBy);
    TableCell TableCell(string elementId, int occurence);
    TableCellCollection TableCells { get; }

    TableRow TableRow(string elementID);
    TableRow TableRow(AttributeValue findBy);
    TableRowCollection TableRows { get; }
    
    TextField TextField(string elementID);
    TextField TextField(AttributeValue findBy);
    TextFieldCollection TextFields { get; }

    Span Span(string elementID);
    Span Span(AttributeValue findBy);
    SpanCollection Spans { get; }

    Div Div(string elementID);
    Div Div(AttributeValue findBy);
    DivCollection Divs { get; }

    Image Image(string elementID);
    Image Image(AttributeValue findBy);
    ImageCollection Images { get; }
  }
}