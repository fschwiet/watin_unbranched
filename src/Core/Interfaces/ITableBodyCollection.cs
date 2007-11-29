namespace WatiN.Core.Interfaces
{
    public interface ITableBodyCollection : IBaseElementCollection
    {
        /// <summary>
        /// Gets the <see cref="ITableBody"/> at the specified index.
        /// </summary>
        /// <value></value>
        ITableBody this[int index] { get; }
    }
}