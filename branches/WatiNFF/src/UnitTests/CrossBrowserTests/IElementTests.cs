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
    public class IElementTests : WatiNCrossBrowserTest
    {
        #region Public instance test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="IElement.Enabled"/> property.
        /// </summary>
        [Test]
        public void Enabled()
        {
            ExecuteTest(EnabledTest);    
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElement.Focus"/> method.
        /// </summary>
        [Test]
        public void Focus()
        {
            ExecuteTest(FocusTest);
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
        /// Tests the behaviour of the <see cref="IElement.PreviousSibling"/>
        /// </summary>
        [Test]
        public void PreviousSibling()
        {
            ExecuteTest(PreviousSiblingTest);
        }

        /// <summary>
        /// Tests that the previous sibling for the first node is null.
        /// </summary>
        [Test]
        public void PreviousSiblingShouldBeNullForFirstNode()
        {
            ExecuteTest(PreviousSiblingIsNullTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElement.TagName"/> property.
        /// </summary>
        [Test]
        public void TagName()
        {
            ExecuteTest(TagNameTest);    
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
        /// Tests the behaviour of the <see cref="IElement.Enabled"/> property.
        /// </summary>
        private static void EnabledTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            Assert.IsTrue(browser.Element("name").Enabled, GetErrorMessage("Element with id name should report as being enabled", browser));
            Assert.IsFalse(browser.Element("disabledid").Enabled, GetErrorMessage("Element with id disableid should report as not being enabled", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElement.Focus"/> method.
        /// </summary>
        private static void FocusTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            browser.Element("popupid").Focus();

            Assert.AreEqual("popupid", browser.ActiveElement.Id, GetErrorMessage("Focus method did not operate as expected", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElement.TagName"/> property.
        /// </summary>
        private static void TagNameTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            IElement element = browser.Element("Form");
            Assert.AreEqual("FORM", element.TagName.ToUpper(), GetErrorMessage("Incorrect tag name retrieved.", browser));
        }

        /// <summary>
        /// Tests that the previous sibling for the first node is null.
        /// </summary>
        private static void PreviousSiblingIsNullTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            IElement element = browser.Element("Form");
            Assert.IsNull(element.PreviousSibling, GetErrorMessage("Previous sibling should return null for the first node in a branch of the DOM", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElement.PreviousSibling"/>
        /// </summary>
        private static void PreviousSiblingTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            IElement element = browser.Element("testParaNextSibling");
            Assert.AreEqual("testElementAttributes", element.PreviousSibling.Id);
        }

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