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
using WatiN.Core.Mozilla;
using WatiN.Core.UnitTests.CrossBrowserTests;

namespace WatiN.Core.UnitTests.Mozilla
{
    [TestFixture]
    public class ElementsTests : CrossBrowserTest
    {
        #region Public instance test methods

        /// <summary>
        /// Tests retrieving most of the standard element attributes.
        /// </summary>
        [Test]
        public void GetAttributes()
        {
            ExecuteTest(GetAttributesTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="Core.Mozilla.Element.ChildNodes"/> property.
        /// </summary>
        [Test]
        public void ChildNodes()
        {
           ChildNodesTest(this.Firefox);
        }

        [Test]
        public void GetAttributeValueOfEmptyStringThrowsArgumentNullExceptionTest()
        {
            ExecuteTest(GetAttributeValueOfEmptyStringThrowsArgumentNullExceptionTest);
        }

        [Test]
        public void GetAttributeValueOfNullThrowsArgumentNullException()
        {
            ExecuteTest(GetAttributeValueOfNullThrowsArgumentNullExceptionTest);
        }

        [Test]
        public void GetInvalidAttribute()
        {
            ExecuteTest(GetInvalidAttributeTest);
        }

        [Test]
        public void GetValidButUndefiniedAttribute()
        {
            ExecuteTest(GetValidButUndefiniedAttributeTest);
        }

        #endregion


        #region Private static test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="Core.Mozilla.Element.ChildNodes"/> property.
        /// </summary>
        private static void ChildNodesTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            Core.Mozilla.Element tableElement = (Core.Mozilla.Element)browser.Element("table1");
            Assert.AreEqual(1, tableElement.ChildNodes.Count, GetErrorMessage("Incorrect number of child nodes found.", browser));
            Assert.AreEqual(NodeType.Element, tableElement.ChildNodes[0].NodeType, GetErrorMessage("Incorrect node type encountered", browser));
        }

        private static void GetValidButUndefiniedAttributeTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            IElement helloButton = browser.Element("helloid");
            Assert.IsNull(helloButton.GetAttributeValue("title"));
        }

        private static void GetAttributeValueOfEmptyStringThrowsArgumentNullExceptionTest(IBrowser browser)
        {
            try
            {
                browser.GoTo(MainURI);
                IElement helloButton = browser.Element("helloid");
                Assert.IsNull(helloButton.GetAttributeValue(String.Empty));
                Assert.Fail("Expected ArgumentNullException.");
            }
            catch (ArgumentNullException)
            {
                // As expected
            }
            catch
            {
                throw;
            }
        }

        private static void GetAttributeValueOfNullThrowsArgumentNullExceptionTest(IBrowser browser)
        {
            try
            {
                browser.GoTo(MainURI);
                IElement helloButton = browser.Element("helloid");
                Assert.IsNull(helloButton.GetAttributeValue(null));
                Assert.Fail("Expected ArgumentNullException.");
            }
            catch (ArgumentNullException)
            {
                // As expected
            }
        }

        private static void GetAttributesTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            IElement element = browser.Element("testElementAttributes");
            Assert.AreEqual("testElementAttributes", element.Id, "Id attribute incorrect");
            Assert.AreEqual("p1main", element.ClassName, "css attribute incorrect");
        }

        private static void GetInvalidAttributeTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            IElement helloButton = browser.Element("helloid");
            Assert.IsNull(helloButton.GetAttributeValue("NONSENCE"));
        }

        #endregion

    }
}
