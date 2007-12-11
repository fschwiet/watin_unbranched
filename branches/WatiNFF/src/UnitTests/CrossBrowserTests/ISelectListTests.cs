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
using System.Collections;
using NUnit.Framework;
using WatiN.Core.Interfaces;

namespace WatiN.Core.UnitTests.CrossBrowserTests
{
    /// <summary>
    /// Tests behaviour specific to the <see cref="ISelectList"/> interface.
    /// </summary>
    public class ISelectListTests : CrossBrowserTest
    {
        #region Public instance test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="ISelectList.Multiple"/> property
        /// Tests for when the multiple attribute has been specified and for when it hasn't.
        /// </summary>
        [Test]
        public void Multiple()
        {
            ExecuteTest(MultipleTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="ISelectList.Options"/> property.
        /// </summary>
        [Test]
        public void Options()
        {
            ExecuteTest(OptionsTest);
        }

        /// <summary>
        /// Test the behaviour of the <see cref="ISelectList.SelectedOption"/> property for the following cases:
        /// 
        /// 1) No option is selected ISelectList.SelectedOption returns null.
        /// 2) A single option is selected
        /// 3) Multiple options are selected, first option is returned.
        /// </summary>
        [Test]
        public void SelectedOption()
        {
            ExecuteTest(SelectedOptionTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="ISelectList.SelectedOptions"/> property for the following cases.
        /// 
        /// 1) None of the options are selected
        /// 2) More than one options have been selected
        /// </summary>
        [Test]
        public void SelectedOptions()
        {
            ExecuteTest(SelectedOptionsTest);    
        }

        #endregion


        #region Private static test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="ISelectList.Multiple"/> property.
        /// Tests for when the multiple attribute has been specified and for when it hasn't.
        /// </summary>
        private static void MultipleTest(IBrowser browser)
        {
            browser.GoTo(MainURI);

            // Attribute not delcared
            ISelectList selectList = browser.SelectList("Select1");
            Assert.IsNotNull(selectList);
            Assert.IsFalse(selectList.Multiple, GetErrorMessage("Incorrect value for SelectList.Multiple returned.", browser));

            // Attribute declared
            selectList = browser.SelectList("Select2");
            Assert.IsNotNull(selectList);
            Assert.IsTrue(selectList.Multiple, GetErrorMessage("Incorrect value for SelectList.Multiple returned.", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="ISelectList.Options"/> property.
        /// </summary>
        private static void OptionsTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            ISelectList selectList = browser.SelectList("Select1");
            Assert.IsNotNull(selectList);
            Assert.AreEqual(3, selectList.Options.Length, GetErrorMessage("Incorrect no. of options returned from select list.", browser));
        }

        /// <summary>
        /// Test the behaviour of the <see cref="ISelectList.SelectedOption"/> property for the following cases:
        /// 
        /// 1) No option is selected ISelectList.SelectedOption returns null.
        /// 2) A single option is selected
        /// 3) Multiple options are selected, first option is returned.
        /// </summary>
        private static void SelectedOptionTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            
            // Case 1
            ISelectList selectList = browser.SelectList("Select3");
            Assert.IsNotNull(selectList);
            IOption selectedOption = selectList.SelectedOption;
            Assert.IsNull(selectedOption, GetErrorMessage("Select3 list should not have any options selected", browser));

            // Case 2
            selectList = browser.SelectList("Select1");
            Assert.IsNotNull(selectList);
            selectedOption = selectList.SelectedOption;
            Assert.IsNotNull(selectedOption, GetErrorMessage("Select1 list should have the first option selected", browser));

            // Case 3
            selectList = browser.SelectList("Select4");
            Assert.IsNotNull(selectList);
            selectedOption = selectList.SelectedOption;
            Assert.IsNotNull(selectedOption, GetErrorMessage("Select4 list should have at least the first option selected", browser));
            Assert.AreEqual("1", selectedOption.Value, "Incorrect value returned for SelectList.SelectedOption.Value", browser);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="ISelectList.SelectedOptions"/> property for the following cases.
        /// 
        /// 1) None of the options are selected
        /// 2) More than one options have been selected
        /// </summary>        
        private static void SelectedOptionsTest(IBrowser browser)
        {
            // Case 1
            browser.GoTo(MainURI);
            ISelectList selectList = browser.SelectList("Select3");
            Assert.IsNotNull(selectList);
            Assert.IsTrue(selectList.Exists, GetErrorMessage("Select list does not exist, error retreiving reference.", browser));
            ArrayList selectedOptions = selectList.SelectedOptions;
            Assert.AreEqual(0, selectedOptions.Count, GetErrorMessage("Select3 list should not have any options selected", browser));

            // Case 2
            selectList = browser.SelectList("Select4");
            Assert.IsNotNull(selectList);
            Assert.IsTrue(selectList.Exists, GetErrorMessage("Select list does not exist, error retreiving reference.", browser));
            selectedOptions = selectList.SelectedOptions;
            Assert.AreEqual(3, selectedOptions.Count, GetErrorMessage("Select4 list should not have any options selected", browser));
        }

        #endregion

    }
}