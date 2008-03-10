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
    /// Tests the behaviour of the concreate implementations of <see cref="IButton"/> 
    /// namely <see cref="WatiN.Core.Button"/> and <see cref="WatiN.Core.Mozilla.Button"/>.
    /// </summary>
    public class IButtonTests : WatiNCrossBrowserTest
    {
        #region Public test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="IButton.Value"/> property.
        /// </summary>
        [Test]
        public void Value()
        {
            ExecuteTest(ValueTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IButton.Text"/> property.
        /// </summary>
        [Test]
        public void Text()
        {
            ExecuteTest(TextTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IButton.ToString"/> method, 
        /// which should return the same value as <see cref="IButton.Text"/>.
        /// </summary>
        [Test]
        public void ToStringTest()
        {
            ExecuteTest(ToStringTest);
        }

        #endregion

        #region Private static test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="IButton.Value"/> property.
        /// </summary>
        private static void ValueTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            IButton button = browser.Button("helloid");
            Assert.AreEqual("Show alert", button.Value, GetErrorMessage("Incorrect value retrieved for IButton.Value", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IButton.Text"/> property.
        /// </summary>
        private static void TextTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            IButton button = browser.Button("helloid");
            Assert.AreEqual("Show alert", button.Text, GetErrorMessage("Incorrect value retrieved for IButton.Value", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IButton.ToString"/> method, 
        /// which should return the same value as <see cref="IButton.Text"/>.
        /// </summary>
        private static void ToStringTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            IButton button = browser.Button("helloid");
            Assert.AreEqual("Show alert", button.ToString(), GetErrorMessage("Incorrect value retrieved for IButton.Value", browser));
        }

        #endregion
    }
}