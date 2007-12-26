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

using NUnit.Framework;
using WatiN.Core.Interfaces;

namespace WatiN.Core.UnitTests.CrossBrowserTests
{
    /// <summary>
    /// Tests the behaviour of the <see cref="IRadioButton"/> interface.
    /// </summary>
    public class IRadioButtonTests : CrossBrowserTest
    {
        #region Public instance test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="IRadioCheck.Checked"/> property.
        /// </summary>
        [Test]
        public void Checked()
        {
            ExecuteTest(CheckedTest);
        }        

        #endregion

        #region Private static test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="IRadioCheck.Checked"/> property.
        /// </summary>
        private static void CheckedTest(IBrowser browser)
        {
            GoTo(MainURI, browser);

            IRadioButton radioButton1 = browser.RadioButton("Radio1");
            IRadioButton radioButton2 = browser.RadioButton("Radio2");
            IRadioButton radioButton3 = browser.RadioButton("Radio3");

            Assert.IsTrue(radioButton1.Checked, "Radio1 radio button should be checked");
            Assert.IsFalse(radioButton2.Checked, "Radio2 radio button should not be checked");
            Assert.IsFalse(radioButton3.Checked, "Radio3 radio button should not be checked");

            radioButton3.Checked = true;
            Assert.IsFalse(radioButton2.Checked, "Radio2 radio button should not be checked");
            Assert.IsTrue(radioButton3.Checked, "Radio3 radio button should be checked");

        }
        
        #endregion
    }
}