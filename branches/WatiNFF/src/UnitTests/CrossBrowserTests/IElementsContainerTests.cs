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
    /// <summary>
    /// Test the functionality of the <see cref="IElementsContainerTemp"/> interface using both IE and FireFox
    /// implementations <see cref="WatiN.Core.Document">IE Document</see> and <see cref="WatiN.Core.Mozilla.Document">Mozilla Document</see>.
    /// </summary>
    [TestFixture]
    public class IElementsContainerTests : CrossBrowserTest
    {
        #region Public test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Table(string)"/> method.
        /// Tests that a button can be located based on the value of it's Id.
        /// </summary>
        [Test]
        public void TableById()
        {
            ExecuteTest(TableByIdTest, false);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.TableRow(string)"/> method.
        /// Tests that a button can be located based on the value of it's Id.
        /// </summary>
        [Test]
        public void TableRowById()
        {
            ExecuteTest(TableRowByIdTest, false);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.TableCell(string)"/> method.
        /// Tests that a button can be located based on the value of it's Id.
        /// </summary>
        [Test]
        public void TableCellById()
        {
            ExecuteTest(TableCellByIdTest, false);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Button(string)"/> method.
        /// Tests that a button can be located based on the value of it's Id.
        /// </summary>
        [Test]
        public void ButtonById()
        {
            ExecuteTest(ButtonByIdTest, false);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Area(string)"/> method.
        /// Tests that an area can be located based on the value of it's Id.
        /// </summary>
        [Test]
        public void AreaById()
        {
            ExecuteTest(AreaByIdTest, false);
        }

        /// <summary>
        /// Test that we can obtain a reference to a paragraph using the elements Id to look it up.
        /// Once the element is found we assert properties unique to the <see cref="IPara"/> interface.
        /// </summary>
        [Test]
        public void ParaById()
        {
            ExecuteTest(ParaByIdTest, false);
        }

        /// <summary>
        /// Test that we can obtain a reference to a link using the elements Id to look it up.
        /// Once the element is found we assert properties unique to the <see cref="ILink"/> interface.
        /// </summary>
        [Test]
        public void LinkById()
        {
            ExecuteTest(LinkByIdTest, false);
        }

        /// <summary>
        /// Test that we can obtain a reference to a div using the elements Id to look it up.
        /// Once the element is found we assert properties unique to the <see cref="IDiv"/> interface.
        /// </summary>
        [Test]
        public void DivById()
        {
            ExecuteTest(DivByIdTest, false);
        }

        /// <summary>
        /// Test that we can obtain a reference to a text field using the elements Id to look it up.
        /// Once the element is found we assert properties unique to the <see cref="ITextField"/> interface.
        /// </summary>
        [Test]
        public void TextFieldById()
        {
            ExecuteTest(TextFieldByIdTest, false);
        }

        /// <summary>
        /// Test that we can obtain a reference to an element using the element's id to look it up.
        /// Once the element is found we assert properties unique to the <see cref="IElement"/> interface.
        /// </summary>
        [Test]
        public void ElementById()
        {
            ExecuteTest(ElementTest, false);            
        }

        #endregion

        #region Private static methods

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Table(string)"/> method.
        /// Tests that a table can be located based on the value of it's Id.
        /// </summary>
        private static void TableByIdTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            ITable table = browser.Table("table1");

            Assert.IsNotNull(table, GetErrorMessage("The table with id table1 could not be found", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.TableRow(string)"/> method.
        /// Tests that a table row can be located based on the value of it's Id.
        /// </summary>
        private static void TableRowByIdTest(IBrowser browser)
        {
            browser.GoTo(TablesURI);
            ITableRow row = browser.TableRow("1");

            Assert.IsNotNull(row, GetErrorMessage("The row with id 1 could not be found", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.TableCell(string)"/> method.
        /// Tests that a table row can be located based on the value of it's Id.
        /// </summary>
        private static void TableCellByIdTest(IBrowser browser)
        {
            browser.GoTo(TablesURI);
            ITableCell cell = browser.TableCell("innerCell1");

            Assert.IsNotNull(cell, GetErrorMessage("The cell with id innerCell1 could not be found", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Button(string)"/> method.
        /// Tests that a button can be located based on the value of it's Id.
        /// </summary>
        private static void ButtonByIdTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            IButton button = browser.Button("helloid");

            Assert.IsNotNull(button, GetErrorMessage("The button with id helloid could not be found", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Area(string)"/> method.
        /// Tests that an area can be located based on the value of it's Id.
        /// </summary>
        private static void AreaByIdTest(IBrowser browser)
        {
            browser.GoTo(ImagesURI);
            IArea area = browser.Area("Area1");

            Assert.IsNotNull(area, GetErrorMessage("The area with id Area1 could not be found", browser));
        }

        /// <summary>
        /// Test that we can obtain a reference to a paragraph using the elements Id to look it up.
        /// Once the element is found we assert properties unique to the <see cref="IPara"/> interface.
        /// </summary>
        private static void ParaByIdTest(IBrowser browser)
        {
            browser.GoTo(MainURI);

            IPara linkPara = browser.Para("links");
            Assert.IsNotNull(linkPara, GetErrorMessage("The link element could not be found.", browser));
            Assert.AreEqual("links", linkPara.Id, GetErrorMessage("The paragraph id had an incorrect value.", browser));

            // Test that we can get a reference to the inner link
            ILink linkElement = linkPara.Link("testlinkid1");
            Assert.IsNotNull(linkElement, GetErrorMessage("The link element could not be found.", browser));
            Assert.AreEqual("testlinkid1", linkElement.Id, GetErrorMessage("The link id had an incorrect value.", browser));
            Assert.AreEqual("http://www.microsoft.com/", linkElement.Url, GetErrorMessage("The link url had an incorrect value.", browser));
        }

        /// <summary>
        /// Test that we can obtain a reference to a link using the elements Id to look it up.
        /// Once the element is found we assert properties unique to the <see cref="ILink"/> interface.
        /// </summary>
        private static void LinkByIdTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            ILink linkElement = browser.Link("testlinkid1");
            Assert.IsNotNull(linkElement, GetErrorMessage("The link element could not be found.", browser));
            Assert.AreEqual("testlinkid1", linkElement.Id, GetErrorMessage("The link id had an incorrect value.", browser));
            Assert.AreEqual("http://www.microsoft.com/", linkElement.Url, GetErrorMessage("The link url had an incorrect value.", browser));
        }

        /// <summary>
        /// Test that we can obtain a reference to a div using the elements Id to look it up.
        /// Once the element is found we assert properties unique to the <see cref="IDiv"/> interface.
        /// </summary>
        private static void DivByIdTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            IDiv divElement = browser.Div("divid");
            Assert.IsNotNull(divElement, GetErrorMessage("The div element could not be found.", browser));
            Assert.AreEqual("divid", divElement.Id, GetErrorMessage("The div id had an incorrect value.", browser));
        }

        /// <summary>
        /// Tests that we can reference a text field.
        /// </summary>
        /// <param name="browser">The browser.</param>
        private static void TextFieldByIdTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            ITextField textField = browser.TextField("readonlytext");
            Assert.IsNotNull(textField, GetErrorMessage("The text field could not be found.", browser));
            Assert.AreEqual("readonly", textField.Value, GetErrorMessage("The text field had an incorrect value.", browser));
        }

        /// <summary>
        /// Test that we can obtain a reference to an element.
        /// </summary>
        /// <param name="browser">The browser.</param>
        private static void ElementTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            IElement element = browser.Element("testElementAttributes");
            Assert.IsNotNull(element, GetErrorMessage("Element with id testElementAttributes could not be found.", browser));
            Assert.AreEqual("p1main", element.ClassName, GetErrorMessage("Css class name was not the expected value.", browser));
            Assert.AreEqual("testElementAttributes", element.Id, GetErrorMessage("Id attribute was not the expected value.", browser));
        }

        #endregion     
    }
}
