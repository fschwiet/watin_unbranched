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
using WatiN.Core;
using WatiN.Core.Interfaces;

namespace WatiN.Core.UnitTests.CrossBrowserTests
{
    /// <summary>
    /// Tests the behaviour specific to the <see cref="IForm"/> interface.
    /// </summary>
    public class IFormTests : CrossBrowserTest
    {
        #region Public instance test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="IForm.Name"/> property.
        /// </summary>
        [Test]
        public void Name()
        {
            ExecuteTest(NameTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IForm.Submit"/> method.
        /// </summary>
        [Test]
        public void Submit()
        {
            ExecuteTest(SubmitTest);
        }

        #endregion

        #region Private static test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="IForm.Name"/> property.
        /// </summary>
        private static void NameTest(IBrowser browser)
        {
            browser.GoTo(FormSubmitURI);
            IForm form = browser.Form("Form2");

            Assert.IsNotNull(form);
            Assert.AreEqual("form2name", form.Name, GetErrorMessage("Incorrect name value found.", browser));
        }
            
        /// <summary>
        /// Tests the behaviour of the <see cref="IForm.Submit"/> method.
        /// </summary>
        private static void SubmitTest(IBrowser browser)
        {
            browser.GoTo(FormSubmitURI);
            IForm form = browser.Form("Form1");

            Assert.IsNotNull(form);
            form.Submit();

            Assert.AreEqual(MainURI, browser.Url, GetErrorMessage("Did not correctly navigate to the form submit page.", browser));
        }

        #endregion
    }
}