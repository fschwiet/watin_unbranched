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
        /// Test that we can reference the title of the webpage.
        /// </summary>
        [Test]
        public void Title()
        {
            ExecuteTest(TitleTest);            
        }

        #region Private static methods

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
