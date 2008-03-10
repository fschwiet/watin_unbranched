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

using System.Text.RegularExpressions;
using NUnit.Framework;
using WatiN.Core.Interfaces;

namespace WatiN.Core.UnitTests.CrossBrowserTests
{
    /// <summary>
    /// Tests the behaviour of the <see cref="IRadioButtonCollection"/> interface.
    /// </summary>
    public class IRadioButtonCollectionTests : WatiNCrossBrowserTest
    {
        #region Public instance test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="IRadioButtonCollection.Item"/>.
        /// </summary>
        [Test]
        public void Index()
        {
            ExecuteTest(IndexTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IRadioButtonCollection.Filter"/>
        /// </summary>
        [Test]
        public void Filter()
        {
            ExecuteTest(FilterTest);
        }

        #endregion

        #region Private static test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="IRadioButtonCollection.Filter"/>
        /// </summary>
        private static void FilterTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            IRadioButtonCollection radioButtons = browser.RadioButtons;
            Assert.AreEqual(3, radioButtons.Length);
            radioButtons = radioButtons.Filter(Find.ByValue(new Regex("^Radio2")));
            Assert.AreEqual(2, radioButtons.Length, GetErrorMessage("Incorrect no. of radio buttons returned from Filter method.", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IRadioButtonCollection.Item"/>.
        /// </summary>
        private static void IndexTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            IRadioButtonCollection radioButtons = browser.RadioButtons;
            IRadioButton radioButton = radioButtons[1];
            Assert.IsTrue(radioButton.Exists);
            Assert.AreEqual("Radio2", radioButton.Id);
        }

        #endregion
    }
}