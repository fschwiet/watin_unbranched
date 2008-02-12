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

using System;
using NUnit.Framework;
using WatiN.Core.Interfaces;

namespace WatiN.Core.UnitTests.CrossBrowserTests
{
    /// <summary>
    /// Tests the concrete implementations of the <see cref="ITableCell"/> interface.
    /// </summary>
    public class ITableCellTests : WatiNCrossBrowserTest
    {
        #region Public instance test methods

        /// <summary>
        /// Tests the <see cref="ITableCell.ParentTableRow"/> property.
        /// </summary>
        [Test]
        public void ParentTableRow()
        {
            ExecuteTest(ParentTableRowTest);
        }

        /// <summary>
        /// Tests the <see cref="ITableCell.Index"/> property.
        /// </summary>
        [Test]
        public void Index()
        {
            ExecuteTest(IndexTest);
        }

        #endregion

        #region Private static methods

        /// <summary>
        /// Tests the <see cref="ITableCell.ParentTableRow"/> property.
        /// </summary>
        private static void ParentTableRowTest(IBrowser browser)
        {
            browser.GoTo(TablesURI);
            ITable table = browser.Table("Table1");
            ITableRow row = table.TableRows[0];
            ITableCell cell = row.TableCells[0];

            Assert.AreEqual("1", cell.ParentTableRow.Id);
        }

        /// <summary>
        /// Tests the <see cref="ITableCell.Index"/> property.
        /// </summary>
        private static void IndexTest(IBrowser browser)
        {
            browser.GoTo(TablesURI);
            ITable table = browser.Table("Table1");
            ITableRow row = table.TableRows[0];
            ITableCell cell = row.TableCells[0];

            Assert.AreEqual(0, cell.Index);
        }

        #endregion

    }
}