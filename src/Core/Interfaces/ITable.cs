namespace WatiN.Core.Interfaces
{
    /// <summary>
    /// This class provides specialized functionality for a HTML table element.
    /// </summary>
    public interface ITable
    {
        /// <summary>
        /// Returns all rows in the first TBODY section of a table. If no
        /// explicit sections are defined in the table (like THEAD, TBODY 
        /// and/or TFOOT sections), it will return all the rows in the table.
        /// This method also returns rows from nested tables.
        /// </summary>
        /// <value>The table rows.</value>
        ITableRowCollection TableRows { get; }

        /// <summary>
        /// Returns the table body sections belonging to this table (not including table body sections 
        /// from tables nested in this table).
        /// </summary>
        /// <value>The table bodies.</value>
        ITableBodyCollection TableBodies { get; }

        string ToString();
    }
}