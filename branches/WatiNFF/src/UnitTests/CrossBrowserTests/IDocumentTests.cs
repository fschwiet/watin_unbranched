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
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using WatiN.Core.Interfaces;
using WatiN.Core.Logging;
using WatiN.Core.Mozilla;

namespace WatiN.Core.UnitTests.CrossBrowserTests
{
    /// <summary>
    /// Test the functionality of the <see cref="IDocument"/> interface using both IE and FireFox
    /// implementations <see cref="WatiN.Core.Document">IE Document</see> and <see cref="WatiN.Core.Mozilla.Document">Mozilla Document</see>.
    /// </summary>
    [TestFixture]
    public class IDocumentTests : CrossBrowserTest
    {
        /// <summary>
        /// Tests the behaviour of the <see cref="IDocument.ActiveElement"/> property.
        /// </summary>
        [Test]
        public void ActiveElement()
        {
            ExecuteTest(ActiveElementTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IDocument.ActiveElement"/> property.
        /// </summary>
        private static void ActiveElementTest(IBrowser browser)
        {
            GoTo(MainURI, browser);

            IElement activeElement = browser.ActiveElement;
            Assert.AreEqual("body", activeElement.TagName.ToLowerInvariant(), GetErrorMessage("ActiveElement when the document first loads for main.html should be body.", browser));
            
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IDocument.Html"/> method.
        /// </summary>
        [Test]
        public void Html()
        {
            ExecuteTest(HtmlTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IDocument.Text"/> method.
        /// </summary>
        [Test]
        public void Text()
        {
            ExecuteTest(TextTest);
        }

        /// <summary>
        /// Test that we can reference the title of the webpage.
        /// </summary>
        [Test]
        public void Title()
        {
            ExecuteTest(TitleTest);            
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IDocument.Url"/> and <see cref="IDocument.Uri"/> methods.
        /// </summary>
        [Test]
        public void UrlUri()
        {
            ExecuteTest(UrlTest);
        }

        #region Private static methods

        /// <summary>
        /// Tests the behaviour of the <see cref="IDocument.Text"/> method.
        /// </summary>
        /// <param name="browser">The browser.</param>
        private static void TextTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            string text = browser.Text.Trim();
            Assert.IsTrue(text.Contains("Goto Index page"), GetErrorMessage("Text value of IDocument incorrect", browser));
        }


        /// <summary>
        /// Tests the behaviour of the <see cref="IDocument.Html"/> method.
        /// </summary>
        /// <param name="browser">The browser.</param>
        private static void HtmlTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            string bodyHtml = browser.Html.Trim();
            Assert.IsTrue(bodyHtml.StartsWith("<body>", StringComparison.OrdinalIgnoreCase), GetErrorMessage("Html value of IDocument incorrect", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IDocument.Url"/> and <see cref="IDocument.Uri"/> methods.
        /// </summary>
        /// <param name="browser">The browser.</param>
        private static void UrlTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            Assert.AreEqual(MainURI, browser.Url);
        }

        /// <summary>
        /// Test that we can reference the title of the webpage.
        /// </summary>
        private static void TitleTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            Assert.AreEqual("Main", browser.Title, GetErrorMessage("The Title retrieved was not the expected value.", browser));
        }

        #endregion
    }
}
