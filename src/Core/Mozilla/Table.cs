#region WatiN Copyright (C) 2006-2008 Jeroen van Menen

//Copyright 2006-2008 Jeroen van Menen
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

#endregion Copyright

using System;
using WatiN.Core.Interfaces;

namespace WatiN.Core.Mozilla
{
    /// <summary>
    /// This class provides specialized functionality for a HTML table element.
    /// </summary>
    public class Table : Element, ITable
    {
        public Table(string elementVariable, FireFoxClientPort clientPort) : base(elementVariable, clientPort)
        {
        }

        /// <summary>
        /// Returns all rows in the first TBODY section of a table. If no
        /// explicit sections are defined in the table (like THEAD, TBODY 
        /// and/or TFOOT sections), it will return all the rows in the table.
        /// This method also returns rows from nested tables.
        /// </summary>
        /// <value>The table rows.</value>
        public ITableRowCollection TableRows
        {
            get
            {
                TableRowCollection rows = new TableRowCollection(this, this.ClientPort, new ElementFinder(this, "tr", null, this.ClientPort));
                return rows;
            }
        }

        /// <summary>
        /// Returns the table body sections belonging to this table (not including table body sections 
        /// from tables nested in this table).
        /// </summary>
        /// <value>The table bodies.</value>
        public ITableBodyCollection TableBodies
        {
            get
            {
                TableBodyCollection bodies = new TableBodyCollection(this, this.ClientPort, new ElementFinder(this, "tbody", null, this.ClientPort));
                return bodies;
            }
        }
    }
}