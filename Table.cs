using mshtml;

using WatiN.Logging;

namespace WatiN
{
  public class Table : ElementsContainer
  {
    public Table(DomContainer ie, HTMLTable htmlTable) : base(ie, (IHTMLElement) htmlTable)
    {}

    public override TableRowCollection TableRows
    {
      get {
        IHTMLElement firstTBody = (IHTMLElement)((HTMLTable)base.element).tBodies.item(0,null);
        return new TableRowCollection(base.Ie, (IHTMLElementCollection)(firstTBody.all)); }
    }

    /// <summary>
    /// Finds te first row that matches findText in inColumn. If no match is found, null is returned.
    /// </summary>
    /// <param name="findText">The text to find</param>
    /// <param name="inColumn">Index of the column to find the text in</param>
    /// <returns></returns>
    public TableRow FindRow(string findText, int inColumn)
    {
      Logger.LogAction("Searching for '" + findText + "' in column " + inColumn + " of " + GetType().Name + " '" + Id + "'");

      foreach (TableRow tableRow in this.TableRows)
      {
        TableCellCollection tableCells = tableRow.TableCells;

        if (tableCells[inColumn].Text.ToLower() == findText.ToLower())
        {
          return tableRow;
        }
      }
      return null;
    }

    public override string ToString()
    {
      return Id;
    }

  }
}