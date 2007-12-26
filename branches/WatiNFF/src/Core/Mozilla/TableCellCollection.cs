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

using System.Collections.Generic;
using WatiN.Core.Interfaces;

namespace WatiN.Core.Mozilla
{
    /// <summary>
    /// The FireFox implementation of <see cref="ITableCellCollection"/>.
    /// </summary>
    public class TableCellCollection : BaseElementCollection, ITableCellCollection
    {
        private readonly TableRow parentRow;

        public TableCellCollection(ITableRow row, FireFoxClientPort clientPort, ElementFinder elementFinder)
            : base(clientPort, elementFinder)
        {
            this.parentRow = (TableRow) row;
        }

        public TableCellCollection(FireFoxClientPort clientPort, ElementFinder elementFinder) : base(clientPort, elementFinder)
        {
            
        }

        protected override List<Element> FindElements()
        {
            int cellCount;
            List<Element> elements = new List<Element>();

            if (this.parentRow == null)
            {
                foreach (string cellVariable in this.ElementFinder.FindAll())
                {
                    elements.Add(new TableCell(cellVariable, this.ClientPort));
                }
            }
            else if (int.TryParse(this.parentRow.GetProperty("cells.length"), out cellCount))
            {
                for (int i = 0; i < cellCount; i++)
                {
                    Element rowElement = (Element)this.parentRow.GetElementByProperty(string.Format("cells[{0}]", i));
                    elements.Add(new TableCell(rowElement.ElementVariable, this.ClientPort));
                }
            }

            return elements;    
        }

        /// <summary>
        /// Gets the <see cref="TableCell"/> at the specified index.
        /// </summary>
        /// <value></value>
        public ITableCell this[int index]
        {
            get { return (ITableCell) this.Elements[index]; }
        }
    }
}