using System.Collections.ObjectModel;

namespace WatiN.Core.Interfaces
{
    public interface ITableRow: IElementsContainerTemp, IElement
    {
        ITable ParentTable { get; }

        /// <summary>
        /// Gets the index of the <see cref="TableRow"/> in the <see cref="TableRowCollection"/> of the parent <see cref="Table"/>.
        /// </summary>
        /// <value>The index of the row.</value>
        int Index { get; }

        ITableCellCollection TableCells { get; }
    }
}