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
    /// Tests the behaviour of the <see cref="IElementTests"/> for all supported browsers
    /// </summary>
    [TestFixture]
    public class IElementTests : CrossBrowserTest
    {
        #region Public instance test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="IElement.NextSibling"/> method.
        /// </summary>
        [Test]
        public void NextSibling()
        {
            ExecuteTest(NextSiblingTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElement.NextSibling"/> method when the sibling elements do not have Id's.
        /// </summary>
        [Test]
        public void NextSiblingNoIds()
        {
            ExecuteTest(NextSiblingNoIdsTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElement.Parent"/> method.
        /// </summary>
        [Test]
        public void Parent()
        {
            ExecuteTest(ParentTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElement.InnerHtml"/> property.
        /// </summary>
        [Test]
        public void InnerHtml()
        {
            ExecuteTest(InnerHtmlTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElement.Text"/> property.
        /// </summary>
        [Test]
        public void Text()
        {
            ExecuteTest(TextTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElement.Title"/> method.
        /// </summary>
        [Test] 
        public void Title()
        {
            ExecuteTest(TitleTest);
        }

        #endregion

        #region private static test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="IElement.NextSibling"/> method.
        /// </summary>
        private static void NextSiblingNoIdsTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            IElement element = browser.Element("testParaNextSibling");

            Assert.AreEqual("p4", element.NextSibling.NextSibling.NextSibling.Text.ToLower());
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElement.NextSibling"/> method.
        /// </summary>
        private static void NextSiblingTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            IElement element = browser.Element("Checkbox1");

            Assert.AreEqual("checkbox2", element.NextSibling.Id.ToLower());
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElement.Parent"/> method.
        /// </summary>
        private static void ParentTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            IElement childElement = browser.Element("name");

            Assert.AreEqual("form", childElement.Parent.Id.ToLower(), GetErrorMessage("Parent element not correctly retrieved.", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElement.Title"/> method.
        /// </summary>
        private static void TitleTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            IElement element = browser.Element("name");
            Assert.AreEqual("Textfield title", element.Title, GetErrorMessage("Incorrect value of title found.", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElement.InnerHtml"/> property.
        /// </summary>
        /// <param name="browser">The browser.</param>
        private static void InnerHtmlTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            IElement paraElement = browser.Para("links");
            Assert.IsTrue(paraElement.InnerHtml.StartsWith("<a id=", StringComparison.OrdinalIgnoreCase), GetErrorMessage("Incorrect InnerHtml value found", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElement.Text"/> property.
        /// </summary>
        /// <param name="browser">The browser.</param>
        private static void TextTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            IElement paraElement = browser.Para("links");
            Assert.IsTrue(paraElement.Text.StartsWith("WatiN"), GetErrorMessage("Incorrect Text value found", browser));
            Assert.IsTrue(paraElement.Text.EndsWith("Index page"), GetErrorMessage("Incorrect Text value found", browser));
        }

        #endregion
    }
}