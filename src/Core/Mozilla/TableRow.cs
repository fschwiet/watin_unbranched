#region WatiN Copyright (C) 2006-2007 Jeroen van Menen

//Copyright 2006-2007 Jeroen van Menen
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

using WatiN.Core;
using WatiN.Core.Interfaces;

namespace WatiN.Core.Mozilla
{
    /// <summary>
    /// Represents the FireFox implementation of table row
    /// </summary>
    public class TableRow : ElementsContainer, ITableRow
    {
        private readonly ITable parentTable;
        private readonly int index;

        public TableRow(string elementVariable, ITable parentTable, int index, FireFoxClientPort clientPort) : base(elementVariable, clientPort)
        {
            this.parentTable = parentTable;
            this.index = index;
        }

        public ITable ParentTable
        {
            get { return parentTable; }
        }

        /// <summary>
        /// Gets the index of the <see cref="Core.TableRow"/> in the <see cref="Core.TableRowCollection"/> of the parent <see cref="Core.Table"/>.
        /// </summary>
        /// <value>The index of the row.</value>
        public int Index
        {
            get { return index; }
        }

        public ITableCellCollection TableCells
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}