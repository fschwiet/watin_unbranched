namespace WatiN.Core.Interfaces
{
    public interface ITableBody : IElementsContainerTemp, IElement
    {
        /// <summary>
        /// Returns the table rows belonging to this table body (not including table rows 
        /// from tables nested in this table body).
        /// </summary>
        /// <value>The table rows.</value>
        ITableRowCollection TableRows { get; }
    }
}