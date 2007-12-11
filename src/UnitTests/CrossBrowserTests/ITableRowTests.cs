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

using NUnit.Framework;
using WatiN.Core.Interfaces;

namespace WatiN.Core.UnitTests.CrossBrowserTests
{
    /// <summary>
    /// Tests the behaviour of the concrete implementations of <see cref="ITableRow"/>
    /// </summary>
    public class ITableRowTests : CrossBrowserTest
    {
        #region Public instance test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="ITableRow.TableCells"/> property.
        /// </summary>
        [Test]
        public void TableCells()
        {
            ExecuteTest(TableCellsTest, false);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="ITableRow.ParentTable"/> property.
        /// </summary>
        [Test]
        public void ParentTable()
        {
            ExecuteTest(ParentTableTest, false);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="ITableRow.Index"/> property.
        /// </summary>
        [Test]
        public void Index()
        {
            ExecuteTest(IndexTest, false);   
        }

        #endregion

        #region Private static methods

        /// <summary>
        /// Tests the behaviour of the <see cref="ITableRow.ParentTable"/> property.
        /// </summary>
        private static void ParentTableTest(IBrowser browser)
        {
            browser.GoTo(TablesURI);

            ITable table = browser.Table("Table1");
            ITableRow row = table.TableRows[0];
            
            Assert.AreEqual(table.Id, row.ParentTable.Id, GetErrorMessage("ParentTable did not return the correct object.", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="ITableRow.TableCells"/>
        /// </summary>
        private static void TableCellsTest(IBrowser browser)
        {
            browser.GoTo(TablesURI);

            ITable table = browser.Table("Table1");
            ITableRow row = table.TableRows[0];
            ITableCellCollection cells = row.TableCells;

            Assert.IsNotNull(cells, GetErrorMessage("Cells collecetion should not be null", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="ITableRow.Index"/> property.
        /// </summary>
        private static void IndexTest(IBrowser browser)
        {
            browser.GoTo(TablesURI);

            ITable table = browser.Table("Table1");
            ITableRow row = table.TableRows[0];
            
            Assert.AreEqual(0, row.Index, GetErrorMessage("Incorrect index value found.", browser));
        }

        #endregion

    }
}