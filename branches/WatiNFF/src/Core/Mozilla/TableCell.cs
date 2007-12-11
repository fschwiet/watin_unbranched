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
    /// FireFox implementation of <see cref="ITableCell"/>.
    /// </summary>
    public class TableCell : Element, ITableCell
    {
    
        public TableCell(string elementVariable, FireFoxClientPort clientPort): base(elementVariable, clientPort)
        {
        }

        /// <summary>
        /// Gets the parent <see cref="Core.TableRow"/> of this <see cref="Core.TableCell"/>.
        /// </summary>
        /// <value>The parent table row.</value>
        public ITableRow ParentTableRow
        {
            get { return new TableRow(((Element)this.Parent).ElementVariable, this.ClientPort); }
        }

        /// <summary>
        /// Gets the index of the <see cref="Core.TableCell"/> in the <see cref="Core.TableCellCollection"/> of the parent <see cref="Core.TableRow"/>.
        /// </summary>
        /// <value>The index of the cell.</value>
        public int Index
        {
            get 
            {
                int index;
                int.TryParse(this.GetProperty("rowIndex"), out index);

                return index;
            }
        }
    }
}