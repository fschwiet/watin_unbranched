using mshtml;

using WatiN.Logging;

namespace WatiN
{
  public class TableRow : ElementsContainer
  {
    public TableRow(DomContainer ie, HTMLTableRow htmlTableRow) : base(ie, (IHTMLElement) htmlTableRow)
    {}
  }
}