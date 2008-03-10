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
using System.Text.RegularExpressions;
using NUnit.Framework;
using WatiN.Core.Interfaces;

namespace WatiN.Core.UnitTests.CrossBrowserTests
{
    /// <summary>
    /// Tests the behaviour of the <see cref="IAreaCollection"/> interface.
    /// </summary>
    public class IAreaCollectionTests : WatiNCrossBrowserTest
    {
        #region Public instance test methods.

        /// <summary>
        /// Tests the behaviour of the <see cref="IAreaCollection.Filter"/> method.
        /// </summary>
        [Test]
        public void Filtered()
        {
            ExecuteTest(FilteredTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IAreaCollection.Item"/> property.
        /// </summary>
        [Test]
        public void Index()
        {
            ExecuteTest(IndexTest);
        }

        #endregion

        #region Private static test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="IAreaCollection.Filter"/> method.
        /// </summary>
        private static void FilteredTest(IBrowser browser)
        {
            browser.GoTo(ImagesURI);
            IAreaCollection areas = browser.Areas;
            Assert.AreEqual(2, areas.Length);

            areas = areas.Filter(Find.ByAlt(new Regex("^Web")));
            Assert.AreEqual(1, areas.Length, GetErrorMessage("Incorrect no. of areas returned from AreaCollection.Filter.", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IAreaCollection.Item"/> property.
        /// </summary>
        private static void IndexTest(IBrowser browser)
        {
            browser.GoTo(ImagesURI);
            IAreaCollection areas = browser.Areas;
            IArea area = areas[1];

            Assert.IsNotNull(area, GetErrorMessage("Area.Item failed to return the expected area object", browser));
            Assert.AreEqual("Area2", area.Id);
        }

        #endregion
    }
}