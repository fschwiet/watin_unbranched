namespace WatiN.Core.Interfaces
{
    public interface ITableCellCollection : IBaseElementCollection
    {
        /// <summary>
        /// Gets the <see cref="TableCell"/> at the specified index.
        /// </summary>
        /// <value></value>
        TableCell this[int index] { get; }
    }
}