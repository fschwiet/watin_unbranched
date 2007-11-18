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
using System.Globalization;
using NUnit.Framework;
using WatiN.Core.Interfaces;

namespace WatiN.Core.UnitTests.CrossBrowserTests
{
    /// <summary>
    /// Tests the behaviour of the <see cref="IArea"/> class.
    /// </summary>
    [TestFixture]
    public class IAreaTests : CrossBrowserTest
    {
        #region Public instance test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="IArea"/> properties.
        /// </summary>
        [Test]
        public void AllAttributes()
        {
            ExecuteTest(AllAttributesTest, false);
        }

        /// <summary>
        /// Test the behaviour of some of the common methods likely to be called against this interface.
        /// Even though some of these methods are defined in the <see cref="IElement"/> base class and
        /// might not be implemented by concreate <see cref="IArea"/> classes.
        /// </summary>
        [Test]
        public void Methods()
        {
            ExecuteTest(MethodsTest, false);

        }

        #endregion

        #region Private static test methods

        /// <summary>
        /// Test the behaviour of some of the common methods likely to be called against this interface.
        /// Even though some of these methods are defined in the <see cref="IElement"/> base class and
        /// might not be implemented by concreate <see cref="IArea"/> classes.
        /// </summary>
        private static void MethodsTest(IBrowser browser)
        {
            browser.GoTo(ImagesURI);
            IArea area = browser.Area("Area1");
            area.Click();

            Assert.AreEqual(MainURI, browser.Url, GetErrorMessage("Did not correctly navigate to the destination page after the click event was fired", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IArea"/> properties.
        /// </summary>
        private static void AllAttributesTest(IBrowser browser)
        {
            browser.GoTo(ImagesURI);
            IArea area = browser.Area("Area1");
            Assert.AreEqual("WatiN", area.Alt, GetErrorMessage("Incorrect Alt value found.", browser));
            Assert.AreEqual("0,0,110,45", area.Coords, GetErrorMessage("Incorrect Coords value found.", browser));
            Assert.AreEqual("rect", area.Shape.ToLower(CultureInfo.InvariantCulture), GetErrorMessage("Incorrect Shape value found.", browser));
            Assert.IsTrue(area.Url.EndsWith("main.html", StringComparison.OrdinalIgnoreCase), GetErrorMessage("Incorrect Url value found.", browser));
        }

        #endregion
    }
}