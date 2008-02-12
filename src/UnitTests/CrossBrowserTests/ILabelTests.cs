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
    /// Tests the behaviour of the concrete implementations of <see cref="ILabel"/>.
    /// </summary>
    public class ILabelTests : WatiNCrossBrowserTest
    {
        /// <summary>
        /// Tests the behaviour of the <see cref="ILabel.AccessKey"/> property.
        /// </summary>

        #region Public instance test methods

        [Test]
        public void AccessKey()
        {
            ExecuteTest(AccessKeyTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="ILabel.AccessKey"/> property.
        /// </summary>
        [Test]
        public void For()
        {
            ExecuteTest(ForTest);
        }

        #endregion


        #region Private static test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="ILabel.AccessKey"/> property.
        /// </summary>
        private static void AccessKeyTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            ILabel label = browser.Label("lblB");
            Assert.AreEqual("B", label.AccessKey, GetErrorMessage("Incorrect value for access key returned.", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="ILabel.AccessKey"/> property.
        /// </summary>
        private static void ForTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            ILabel label = browser.Label("lblB");
            Assert.AreEqual("txtLabelB", label.For, GetErrorMessage("Incorrect value for access key returned.", browser));
        }

        #endregion

    }
}