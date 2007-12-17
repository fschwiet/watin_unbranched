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
    /// Tests the behaviour of the <see cref="IOption"/> interface.
    /// </summary>
    public class IOptionTests : CrossBrowserTest
    {
        #region Public instance test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="IOption.Value"/> property.
        /// </summary>
        [Test]
        public void Value()
        {
            ExecuteTest(ValueTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IOption.Selected"/> property.
        /// </summary>
        [Test]
        public void Selected()
        {
            ExecuteTest(SelectedTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IOption.Index"/> property.
        /// </summary>
        [Test]
        public void Index()
        {
            ExecuteTest(IndexTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IOption.DefaultSelected"/> property.
        /// </summary>
        [Test]
        public void DefaultSelected()
        {
            ExecuteTest(DefaultSelectedTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IOption.Clear"/> method.
        /// </summary>
        [Test]
        public void Clear()
        {
            ExecuteTest(ClearTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IOption.Select"/> method.
        /// </summary>
        [Test]
        public void Select()
        {
            ExecuteTest(SelectTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IOption.ParentSelectList"/> property.
        /// </summary>
        [Test]
        public void ParentSelectList()
        {
            ExecuteTest(ParentSelectListTest);
        }

        #endregion

        #region Private static test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="IOption.Clear"/> method.
        /// </summary>
        private static void ClearTest(IBrowser browser)
        {
            browser.GoTo(MainURI);

            ISelectList selectList = browser.SelectList("Select2");
            Assert.IsTrue(selectList.Exists);
            IOption option = selectList.Options[1];
            Assert.IsTrue(option.Selected);
            
            option.Clear();
            Assert.IsFalse(option.Selected);

        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IOption.Select"/> method.
        /// </summary>
        private static void SelectTest(IBrowser browser)
        {
            browser.GoTo(MainURI);

            ISelectList selectList = browser.SelectList("Select2");
            Assert.IsTrue(selectList.Exists);
            IOption option = selectList.Options[0];
            Assert.IsFalse(option.Selected);

            option.Select();
            Assert.IsTrue(option.Selected);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IOption.ParentSelectList"/> property.
        /// </summary>
        private static void ParentSelectListTest(IBrowser browser)
        {
            browser.GoTo(MainURI);

            ISelectList selectList = browser.SelectList("Select2");
            Assert.IsTrue(selectList.Exists);
            IOption option = selectList.Options[0];
            ISelectList parentList = option.ParentSelectList;

            Assert.IsNotNull(parentList);
            Assert.AreEqual(selectList.Id, parentList.Id, GetErrorMessage("Option.ParentSelectList did not return the correct object, Id's differ.", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IOption.DefaultSelected"/> property.
        /// </summary>
        private static void DefaultSelectedTest(IBrowser browser)
        {
            browser.GoTo(MainURI);

            ISelectList selectList = browser.SelectList("Select1");
            Assert.IsTrue(selectList.Exists);
            Assert.IsTrue(selectList.Options[0].DefaultSelected);
            Assert.IsFalse(selectList.Options[1].DefaultSelected);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IOption.Value"/> property.
        /// </summary>       
        private static void ValueTest(IBrowser browser)
        {
            browser.GoTo(MainURI);

            ISelectList selectList = browser.SelectList("Select1");
            Assert.IsTrue(selectList.Exists);
            Assert.AreEqual("tweede tekst", selectList.Options[1].Value);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IOption.Selected"/> property.
        /// </summary>
        private static void SelectedTest(IBrowser browser)
        {
            browser.GoTo(MainURI);

            ISelectList selectList = browser.SelectList("Select2");
            Assert.IsTrue(selectList.Exists);
            Assert.IsTrue(selectList.Options[1].Selected);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IOption.Index"/> property.
        /// </summary>
        private static void IndexTest(IBrowser browser)
        {
            browser.GoTo(MainURI);

            ISelectList selectList = browser.SelectList("Select2");
            Assert.IsTrue(selectList.Exists);
            Assert.AreEqual(2, selectList.Options[2].Index);
        }

        #endregion

    }
}