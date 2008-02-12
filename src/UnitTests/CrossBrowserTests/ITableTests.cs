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

using System.Globalization;
using NUnit.Framework;
using WatiN.Core.Interfaces;

namespace WatiN.Core.UnitTests.CrossBrowserTests
{
    /// <summary>
    /// Tests the behaviour of the <see cref="ITable"/> interface
    /// </summary>
    public class ITableTests : WatiNCrossBrowserTest
    {
        #region Public test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="ITable.TableRows"/> method
        /// </summary>
        [Test]
        public void TableRows()
        {
            ExecuteTest(TableRowsTest);
        }

        /// <summary>
        /// Tests that you can correctly retrieve table header cells
        /// </summary>
        [Test]
        public void TableHeader()
        {
            ExecuteTest(TableHeaderTest);
        }

        /// <summary>
        /// Tests that you can correctly retreive the table bodies 
        /// for a given table
        /// </summary>
        [Test]
        public void TableBodies()
        {
            ExecuteTest(TableBodiesTest);
        }

        #endregion

        #region Private static methods

        /// <summary>
        /// Tests that you can correctly retreive the table bodies 
        /// for a given table
        /// </summary>
        private static void TableBodiesTest(IBrowser browser)
        {
            browser.GoTo(TablesURI);
            ITable table = browser.Table("Table1");
            Assert.AreEqual(2, table.TableBodies.Length, GetErrorMessage("Incorrect number of table bodies returned.", browser));
        }

        /// <summary>
        /// Tests that you can correctly retrieve table header cells
        /// </summary>
        private static void TableHeaderTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            ITable table = browser.Table("table1");
            Assert.AreEqual("TH", table.TableRows[0].Elements[0].TagName.ToUpper(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="ITable.TableRows"/> method
        /// </summary>
        private static void TableRowsTest(IBrowser browser)
        {
            browser.GoTo(TablesURI);
            ITable table = browser.Table("Table1");

            Assert.IsNotNull(table.TableRows, GetErrorMessage("Table rows should not be null", browser));
            Assert.AreEqual(3, table.TableRows.Length, GetErrorMessage("Incorrect number of table rows found.", browser));
        }

        #endregion

    }
}