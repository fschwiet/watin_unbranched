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
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using NUnit.Framework;
using WatiN.Core;
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
        /// Tests the behaviour of the <see cref="ISelectList.AllContents"/> property for the following cases:
        /// 
        /// 1) Select list has no items, should return null.
        /// 2) List has more than one item.
        /// </summary>
        [Test]
        public void AllContents()
        {
            ExecuteTest(AllContentsTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="ISelectList.ClearList"/> method.
        /// </summary>
        [Test]
        public void ClearList()
        {
            ExecuteTest(ClearListTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="ISelectList.HasSelectedItems"/> property.
        /// </summary>
        [Test]
        public void HasSelectedItems()
        {
            ExecuteTest(HasSelectedItemsTest);
        }

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
        /// Tests the behaviour of <see cref="ISelectList.Option(string)"/>, <see cref="ISelectList.Option(Regex)"/>
        /// and <see cref="ISelectList.Option(AttributeConstraint)"/>.
        /// </summary>
        [Test]
        public void Option()
        {
            ExecuteTest(OptionTest);    
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
        /// Tests the behaviour of the <see cref="ISelectList.Select(string)"/> and <see cref="ISelectList.Select(Regex)"/> methods.
        /// </summary>
        [Test]
        public void Select()
        {
            ExecuteTest(SelectTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="ISelectList.SelectByValue(string)"/> and <see cref="ISelectList.SelectByValue(Regex)"/> methods.
        /// </summary>
        [Test]
        public void SelectByValue()
        {
            ExecuteTest(SelectByValueTest);
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
        /// Test the behaviour of the <see cref="ISelectList.SelectedItem"/> property for the following cases:
        /// 
        /// 1) No option is selected ISelectList.SelectedItem returns null.
        /// 2) A single option is selected
        /// 3) Multiple options are selected, first option is returned.
        /// </summary>
        [Test]
        public void SelectedItem()
        {
            ExecuteTest(SelectedItemTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="ISelectList.SelectedOptions"/> property for the following cases:
        /// 
        /// 1) None of the options are selected
        /// 2) More than one options have been selected
        /// </summary>
        [Test]
        public void SelectedOptions()
        {
            ExecuteTest(SelectedOptionsTest);    
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="ISelectList.SelectedItems"/> property for the following cases:
        /// 
        /// 1) None of the options are selected
        /// 2) More than one options have been selected
        /// </summary>
        [Test]
        public void SelectedItems()
        {
            ExecuteTest(SelectedItemsTest);    
        }

        #endregion


        #region Private static test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="ISelectList.AllContents"/> property for the following cases:
        /// 
        /// 1) Select list has no items, should return null.
        /// 2) List has more than one item.
        /// </summary>
        private static void AllContentsTest(IBrowser browser)
        {
            browser.GoTo(MainURI);

            ISelectList selectList = browser.SelectList("SelectEmpty");
            Assert.IsTrue(selectList.Exists);
            StringCollection allContent = selectList.AllContents;

            Assert.AreEqual(0, allContent.Count, GetErrorMessage("SelectList.AllContent should return an empty string collection for a select list with no options." ,browser));

            selectList = browser.SelectList("Select1");
            allContent = selectList.AllContents;
            Assert.IsTrue(selectList.Exists);
            Assert.AreEqual(3, allContent.Count, GetErrorMessage("Incorrect no. of items returned for SelectList.AllContents.",browser));

            Assert.AreEqual("First text", allContent[0], GetErrorMessage("Incorrect value returned for the first item of SelectList.AllContents", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="ISelectList.ClearList"/> method.
        /// </summary>
        private static void ClearListTest(IBrowser browser)
        {
            browser.GoTo(MainURI);

            // Attribute not delcared
            ISelectList selectList = browser.SelectList("Select4");
            Assert.IsNotNull(selectList);
            Assert.IsTrue(selectList.SelectedOptions.Count > 0);

            selectList.ClearList();
            Assert.AreEqual(0, selectList.SelectedOptions.Count, GetErrorMessage("Select list was not cleared after sending the ClearList message.",browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="ISelectList.HasSelectedItems"/> property.
        /// </summary>
        private static void HasSelectedItemsTest(IBrowser browser)
        {
            // Case 1
            browser.GoTo(MainURI);
            ISelectList selectList = browser.SelectList("Select3");
            Assert.IsNotNull(selectList);
            Assert.IsTrue(selectList.Exists, GetErrorMessage("Select list does not exist, error retreiving reference.", browser));
            Assert.IsFalse(selectList.HasSelectedItems, GetErrorMessage("Incorrect value returned from HasSelectedItems, Select3 should not have any selected items", browser));

            // Case 2
            selectList = browser.SelectList("Select4");
            Assert.IsNotNull(selectList);
            Assert.IsTrue(selectList.Exists, GetErrorMessage("Select list does not exist, error retreiving reference.", browser));
            Assert.IsTrue(selectList.HasSelectedItems, GetErrorMessage("Incorrect value returned from HasSelectedItems, Select4 should have selected items", browser));
        }

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
        /// Tests the behaviour of <see cref="ISelectList.Option(string)"/>, <see cref="ISelectList.Option(Regex)"/>
        /// and <see cref="ISelectList.Option(AttributeConstraint)"/>.
        /// </summary>
        private static void OptionTest(IBrowser browser)
        {
            browser.GoTo(MainURI);

            // 1.1 Find by text for an expected option
            ISelectList selectList = browser.SelectList("Select1");
            IOption option = selectList.Option("Second text");
            Assert.IsTrue(option.Exists, GetErrorMessage("ISelectList.Option(string) did not find an option with the text value of Second Text.", browser));

            // 1.2 Should be case-insensitive
            option = selectList.Option("SECOND TEXT");
            Assert.IsTrue(option.Exists, GetErrorMessage("ISelectList.Option(string) did not find an option with the text value of Second Text.", browser));

            // 2.1 Test using a regular expression
            option = selectList.Option(new Regex("^S"));
            Assert.IsTrue(option.Exists, GetErrorMessage("ISelectList.Option(RegEx) did not find an option with the regular expression of ^S.", browser));

            // 3.1 Test finding an option explicitly using an attribute contraint.
            option = selectList.Option(Find.ById("Select1Option3"));
            Assert.IsTrue(option.Exists, GetErrorMessage("ISelectList.Option(AttributeConstraint) did not find an option using FindById.(\"Select1Option3\").", browser));
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
        /// Tests the behaviour of the <see cref="ISelectList.Select(string)"/> and <see cref="ISelectList.Select(Regex)"/>
        /// </summary>
        private static void SelectTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            ISelectList selectList = browser.SelectList("Select3");
            Assert.IsNotNull(selectList);
            Assert.IsFalse(selectList.Options[2].Selected);

            selectList.Select("Third text");
            Assert.IsTrue(selectList.Options[2].Selected, GetErrorMessage("SelectList.Select failed.", browser));

            Assert.IsFalse(selectList.Options[1].Selected);
            selectList.Select(new Regex("^Second"));
            Assert.IsTrue(selectList.Options[1].Selected, GetErrorMessage("SelectList.Select using a regular expression failed.", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="ISelectList.SelectByValue(string)"/> and <see cref="ISelectList.SelectByValue(Regex)"/> methods.
        /// </summary>
        private static void SelectByValueTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            ISelectList selectList = browser.SelectList("Select3");
            Assert.IsNotNull(selectList);
            Assert.IsFalse(selectList.Options[1].Selected);

            selectList.SelectByValue("tweede tekst");
            Assert.IsTrue(selectList.Options[1].Selected, GetErrorMessage("SelectList.SelectByValue failed.", browser));

            Assert.IsFalse(selectList.Options[2].Selected);
            selectList.SelectByValue(new Regex("^3"));
            Assert.IsTrue(selectList.Options[2].Selected, GetErrorMessage("SelectList.SelectByValue using a regular expression failed.", browser));
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
        /// Test the behaviour of the <see cref="ISelectList.SelectedItem"/> property for the following cases:
        /// 
        /// 1) No option is selected ISelectList.SelectedItem returns null.
        /// 2) A single option is selected
        /// 3) Multiple options are selected, first option is returned.
        /// </summary>        
        private static void SelectedItemTest(IBrowser browser)
        {
            browser.GoTo(MainURI);

            // Case 1
            ISelectList selectList = browser.SelectList("Select3");
            Assert.IsNotNull(selectList);
            string selectedItem = selectList.SelectedItem;
            Assert.IsNull(selectedItem, GetErrorMessage("Select3 list should not have any options selected", browser));

            // Case 2
            selectList = browser.SelectList("Select1");
            Assert.IsNotNull(selectList);
            selectedItem = selectList.SelectedItem;
            Assert.IsNotNull(selectedItem, GetErrorMessage("Select1 list should have the first option selected", browser));

            // Case 3
            selectList = browser.SelectList("Select4");
            Assert.IsNotNull(selectList);
            selectedItem = selectList.SelectedItem;
            Assert.IsNotNull(selectedItem, GetErrorMessage("Select4 list should have at least the first option selected", browser));
            Assert.AreEqual("First text", selectedItem, "Incorrect value returned for SelectList.SelectedItem.Value", browser);
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
            Assert.AreEqual(3, selectedOptions.Count, GetErrorMessage("Select4 list should have 3 options selected", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="ISelectList.SelectedItems"/> property for the following cases:
        /// 
        /// 1) None of the options are selected
        /// 2) More than one options have been selected
        /// </summary>
        private static void SelectedItemsTest(IBrowser browser)
        {
            // Case 1
            browser.GoTo(MainURI);
            ISelectList selectList = browser.SelectList("Select3");
            Assert.IsNotNull(selectList);
            Assert.IsTrue(selectList.Exists, GetErrorMessage("Select list does not exist, error retreiving reference.", browser));
            StringCollection selectedItems = selectList.SelectedItems;
            Assert.AreEqual(0, selectedItems.Count, GetErrorMessage("Select3 list should not have any options selected", browser));

            // Case 2
            selectList = browser.SelectList("Select4");
            Assert.IsNotNull(selectList);
            Assert.IsTrue(selectList.Exists, GetErrorMessage("Select list does not exist, error retreiving reference.", browser));
            selectedItems = selectList.SelectedItems;
            Assert.AreEqual(3, selectedItems.Count, GetErrorMessage("Select4 list have 3 options selected", browser));
        }

        #endregion

    }
}