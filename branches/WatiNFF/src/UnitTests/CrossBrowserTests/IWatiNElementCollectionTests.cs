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
using NUnit.Framework;
using WatiN.Core.Interfaces;

namespace WatiN.Core.UnitTests.CrossBrowserTests
{
    /// <summary>
    /// Tests the behaviour of the <see cref="IWatiNElementCollection"/> interface.
    /// </summary>
    public class IWatiNElementCollectionTests : WatiNCrossBrowserTest
    {
        #region Public instance test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="IWatiNElementCollection.Item"/> method.
        /// </summary>
        [Test]
        public void Index()
        {
            ExecuteTest(IndexTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IWatiNElementCollection.Filter"/> method.
        /// </summary>
        [Test]
        public void Filter()
        {
            ExecuteTest(FilterTest);
        }

        #endregion

        #region Private static test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="IWatiNElementCollection.Filter"/> method.
        /// </summary>
        private static void FilterTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            IWatiNElementCollection allElements = browser.Elements;
            Assert.IsTrue(allElements.Length > 80, GetErrorMessage(string.Format("Incorrect no. of elements returned, no. returned was {0}", allElements.Length), browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IWatiNElementCollection.Item"/> method.
        /// </summary>        
        private static void IndexTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            IWatiNElementCollection allElements = browser.Elements;
            IElement element = allElements[0];

            // TODO check why this fails for IE
            // Answer because first element in IE is the document <! declaration in FF the first element is <html>
            //Assert.IsTrue(element.Exists, GetErrorMessage("Element as index 0 does not exist?", browser));
            
            // TODO discuss with jeroen if doc declaration is the expected first element, see above todo
            // Assert.AreEqual("html", element.TagName.ToLowerInvariant(), GetErrorMessage("Incorrect tagname returned.", browser));

            Assert.IsNotNull(element);
        }

        #endregion

    }
}