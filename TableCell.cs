using mshtml;

namespace WatiN
{
  public class TableCell : ElementsContainer
  {
    public TableCell(DomContainer ie, HTMLTableCell htmlTableCell) : base(ie, (IHTMLElement) htmlTableCell)
    {}
  }
}