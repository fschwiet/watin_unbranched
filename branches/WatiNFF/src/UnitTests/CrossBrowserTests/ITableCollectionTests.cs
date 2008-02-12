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

using System.Text.RegularExpressions;
using NUnit.Framework;
using WatiN.Core.Interfaces;

namespace WatiN.Core.UnitTests.CrossBrowserTests
{
    /// <summary>
    /// Tests the behaviour of the <see cref="ITableCollection"/> interface.
    /// </summary>
    public class ITableCollectionTests : WatiNCrossBrowserTest
    {
        #region Public instance test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="ITableCollection.Item"/>.
        /// </summary>
        [Test]
        public void Index()
        {
            ExecuteTest(IndexTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="ITableCollection.Filter"/>
        /// </summary>
        [Test]
        public void Filter()
        {
            ExecuteTest(FilterTest);
        }

        #endregion

        #region Private static test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="ITableCollection.Filter"/>
        /// </summary>
        private static void FilterTest(IBrowser browser)
        {
            GoTo(TablesURI, browser);
            ITableCollection tables = browser.Tables;
            Assert.AreEqual(2, tables.Length);
            tables = tables.Filter(Find.By("border", "6"));
            Assert.AreEqual(1, tables.Length, GetErrorMessage("Incorrect no. of tables returned from Filter method.", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="ITableCollection.Item"/>.
        /// </summary>
        private static void IndexTest(IBrowser browser)
        {
            GoTo(TablesURI, browser);
            ITableCollection tables = browser.Tables;
            ITable table = tables[1];
            Assert.IsTrue(table.Exists);
            Assert.AreEqual("Table2", table.Id);
        }

        #endregion
    }
}