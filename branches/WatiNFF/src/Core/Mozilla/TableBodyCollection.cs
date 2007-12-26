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
    /// FireFox implementation of the <see cref="ITableBodyCollection"/>
    /// </summary>
    public class TableBodyCollection : BaseElementCollection, ITableBodyCollection
    {
        private readonly Table parentTable;

        public TableBodyCollection(ITable parentTable, FireFoxClientPort clientPort, ElementFinder elementFinder)
            : base(clientPort, elementFinder)
        {
            this.parentTable = (Table) parentTable;
        }

        public TableBodyCollection(FireFoxClientPort clientPort, ElementFinder elementFinder) : base(clientPort, elementFinder)
        {
        }

        protected override List<Element> FindElements()
        {
            int rowCount;
            List<Element> elements = new List<Element>();

            if (this.parentTable == null)
            {
                foreach (string tbodyVariable in this.ElementFinder.FindAll())
                {
                    elements.Add(new TableBody(tbodyVariable, this.ClientPort));
                }
            }
            else if (int.TryParse(this.parentTable.GetProperty("tBodies.length"), out rowCount))
            {
                for (int i = 0; i < rowCount; i++)
                {
                    Element element = (Element)this.parentTable.GetElementByProperty(string.Format("tBodies[{0}]", i));
                    elements.Add(new TableBody(element.ElementVariable, this.ClientPort));
                }
            }

            return elements;
        }

        /// <summary>
        /// Gets the <see cref="ITableBody"/> at the specified index.
        /// </summary>
        /// <value></value>
        public ITableBody this[int index]
        {
            get { return (ITableBody) this.Elements[index]; }
        }
    }
}