namespace WatiN.Core.Interfaces
{
    public interface ITableRowCollection : IBaseElementCollection
    {
        /// <summary>
        /// Gets the <see cref="ITableRow"/> at the specified index.
        /// </summary>
        /// <value></value>
        ITableRow this[int index] { get; }
    }
}