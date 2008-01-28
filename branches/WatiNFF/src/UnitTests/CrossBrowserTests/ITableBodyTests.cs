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
    /// Tests the behaviour of the concrete implementations of <see cref="ITableBody"/>. 
    /// </summary>
    public class ITableBodyTests : CrossBrowserTest
    {
        #region Public instance test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="ITableBody.TableRows"/> property.
        /// </summary>
        [Test]
        public void TableRows()
        {
            ExecuteTest(TableRowsTest);
        }

        #endregion

        #region Private static test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="ITableBody.TableRows"/> property.
        /// </summary>
        private static void TableRowsTest(IBrowser browser)
        {
            browser.GoTo(TablesURI);
            ITableBody tableBody = browser.TableBody("tbody1");
            Assert.AreEqual(2, tableBody.TableRows.Length, "ITableBody.TableRows returned the incorrect number of rows.", browser);
        }

        #endregion

    }
}