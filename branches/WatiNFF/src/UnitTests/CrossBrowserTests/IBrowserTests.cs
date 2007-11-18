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

namespace WatiN.Core.UnitTests.CrossBrowserTests
{
    [TestFixture]
    public class IBrowserTests : CrossBrowserTest
    {
        #region Public instance test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.Back()"/> method.
        /// </summary>
        [Test]
        public void Back()
        {
            ExecuteTest(BackTest, false);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.Forward()"/> method.
        /// </summary>
        [Test]
        public void Forward()
        {
            ExecuteTest(ForwardTest, false);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.Refresh"/> method.
        /// </summary>
        [Test]
        public void Refresh()
        {
            ExecuteTest(RefreshTest, false);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.Reopen()"/> method.
        /// </summary>
        [Test]
        public void Reopen()
        {
            ExecuteTest(ReopenTest, false);
        }

        #endregion

        #region Private static methods

        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.Reopen()"/> method.
        /// </summary>
        private static void ReopenTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            browser.Reopen();
            Assert.AreEqual("about:blank", browser.Url, GetErrorMessage("Incorrect Url found after a Reopen action was performed.", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.Refresh"/> method.
        /// </summary>
        /// <param name="browser">The browser.</param>
        private static void RefreshTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            browser.Refresh();
        }


        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.Back()"/> method.
        /// </summary>
        /// <param name="browser">The browser.</param>
        private static void BackTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            browser.GoTo(ImagesURI);
            browser.Back();

            Assert.AreEqual(MainURI, browser.Url, GetErrorMessage("Url not the expected value, Back action failed", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.Forward()"/> method.
        /// </summary>
        public void ForwardTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            browser.GoTo(ImagesURI);
            browser.Back();
            browser.Forward();

            Assert.AreEqual(ImagesURI, browser.Url, GetErrorMessage("Url not the expected value, Forward action failed", browser));
        }

        #endregion
    }
}
