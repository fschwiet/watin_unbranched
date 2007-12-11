namespace WatiN.Core.Interfaces
{
    public interface ITableCell : IElement
    {
        /// <summary>
        /// Gets the parent <see cref="TableRow"/> of this <see cref="TableCell"/>.
        /// </summary>
        /// <value>The parent table row.</value>
        ITableRow ParentTableRow { get; }

        /// <summary>
        /// Gets the index of the <see cref="TableCell"/> in the <see cref="TableCellCollection"/> of the parent <see cref="TableRow"/>.
        /// </summary>
        /// <value>The index of the cell.</value>
        int Index { get; }
    }
}