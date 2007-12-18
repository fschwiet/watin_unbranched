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
using System.Text.RegularExpressions;
using NUnit.Framework;
using WatiN.Core;
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
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Area(string)"/> method.
        /// Tests that an area can be located based on the value of it's Id.
        /// </summary>
        [Test]
        public void AreaById()
        {
            ExecuteTest(AreaByIdTest, false);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Area(Regex)"/> method.
        /// </summary>
        [Test]
        public void AreaByRegex()
        {
            ExecuteTest(AreaByRegexTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Area(AttributeConstraint)"/> method.
        /// </summary>
        [Test]
        public void AreaByAttributeContraint()
        {
            ExecuteTest(AreaByAttributeContraintTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Areas"/> property.
        /// </summary>
        [Test]
        public void Areas()
        {
            ExecuteTest(AreasTest);
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
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Button(Regex)"/> method.
        /// </summary>
        [Test]
        public void ButtonByRegex()
        {
            ExecuteTest(ButtonByRegexTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Button(AttributeConstraint)"/> method.
        /// </summary>
        [Test]
        public void ButtonByAttributeContraint()
        {
            ExecuteTest(ButtonByAttributeContraintTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.CheckBox(string)"/> method.
        /// Tests that a check box can be located based on the value of it's Id.
        /// </summary>
        [Test]
        public void CheckBoxById()
        {
            ExecuteTest(CheckBoxByIdTest, false);
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
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Form(string)"/> method.
        /// Tests that a form can be located based on the value of it's Id.
        /// </summary>
        [Test]
        public void FormById()
        {
            ExecuteTest(FormByIdTest, false);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Label(string)"/> method.
        /// Tests that a label can be located based on the value of it's Id.
        /// </summary>
        [Test]
        public void LabelById()
        {
            ExecuteTest(LabelByIdTest, false);
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
        /// Test that we can obtain a reference to an image using the elements Id to look it up using <see cref="IElementsContainerTemp.Image(string)"/>.
        /// </summary>
        [Test]
        public void ImageById()
        {
            ExecuteTest(ImageByIdTest, false);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.SelectList(string)"/> method.
        /// Tests that a select list can be located based on the value of it's Id.
        /// </summary>
        [Test]
        public void SelectListById()
        {
            ExecuteTest(SelectListByIdTest, false);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Span(string)"/> method.
        /// </summary>
        [Test]
        public void SpanById()
        {
            ExecuteTest(SpanByIdTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Span(AttributeConstraint)"/> method.
        /// </summary>
        [Test]
        public void SpanByAttributeContraint()
        {
            ExecuteTest(SpanByAttributeContraintTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Span(Regex)"/> method.
        /// </summary>
        [Test]
        public void SpanByRegex()
        {
            ExecuteTest(SpanByRegexTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Table(string)"/> method.
        /// Tests that a table can be located based on the value of it's Id.
        /// </summary>
        [Test]
        public void TableById()
        {
            ExecuteTest(TableByIdTest, false);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.TableBody(string)"/> method.
        /// Tests that a table body can be located based on the value of it's Id.
        /// </summary>
        [Test]
        public void TableBodyById()
        {
            ExecuteTest(TableBodyByIdTest, false);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.TableCell(string)"/> method.
        /// Tests that a table cell can be located based on the value of it's Id.
        /// </summary>
        [Test]
        public void TableCellById()
        {
            ExecuteTest(TableCellByIdTest, false);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.TableRow(string)"/> method.
        /// Tests that a table row can be located based on the value of it's Id.
        /// </summary>
        [Test]
        public void TableRowById()
        {
            ExecuteTest(TableRowByIdTest, false);
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
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Area(Regex)"/> method.
        /// </summary>
        private static void AreaByRegexTest(IBrowser browser)
        {
            browser.GoTo(ImagesURI);
            IArea area = browser.Area(new Regex("^Are"));

            Assert.IsNotNull(area, GetErrorMessage("The area with id Area1 could not be found", browser));
            Assert.IsTrue(area.Exists);
            Assert.AreEqual("Area1", area.Id);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Area(AttributeConstraint)"/> method.
        /// </summary>        
        private static void AreaByAttributeContraintTest(IBrowser browser)
        {
            browser.GoTo(ImagesURI);
            IArea area = browser.Area(Find.ByAlt("WatiN"));

            Assert.IsNotNull(area, GetErrorMessage("The area with id Area1 could not be found", browser));
            Assert.IsTrue(area.Exists);
            Assert.AreEqual("Area1", area.Id);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Areas"/> method.
        /// </summary>
        private static void AreasTest(IBrowser browser)
        {
            browser.GoTo(ImagesURI);
            IAreaCollection areas = browser.Areas;

            Assert.IsNotNull(areas, GetErrorMessage("The areas could not be found", browser));
            Assert.AreEqual(2, areas.Length);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Label(string)"/> method.
        /// Tests that a label can be located based on the value of it's Id.
        /// </summary>
        private static void LabelByIdTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            ILabel label = browser.Label("lblB");

            Assert.IsNotNull(label, GetErrorMessage("The table with id lblB could not be found", browser));
        }

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
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.TableBody(string)"/> method.
        /// Tests that a table body can be located based on the value of it's Id.
        /// </summary>
        private static void TableBodyByIdTest(IBrowser browser)
        {
            browser.GoTo(TablesURI);
            ITableBody tableBody = browser.TableBody("tbody2");

            Assert.IsNotNull(tableBody, GetErrorMessage("The table body with id tbody2 could not be found", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Span(string)"/> method.
        /// </summary>
        private static void SpanByIdTest(IBrowser browser)
        {
            browser.GoTo(MainURI);

            ISpan span = browser.Span("spanid1");
            Assert.IsNotNull(span);
            Assert.IsTrue(span.Exists);

            Assert.AreEqual("File", span.Text.Trim(), GetErrorMessage("Incorrect value retrieved from span", browser));
                 
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Span(AttributeConstraint)"/> method.
        /// </summary>
        private static void SpanByAttributeContraintTest(IBrowser browser)
        {
            browser.GoTo(MainURI);

            ISpan span = browser.Span(Find.ByStyle("color", "green"));
            Assert.IsNotNull(span);
            Assert.IsTrue(span.Exists);

            Assert.AreEqual("File", span.Text.Trim(), GetErrorMessage("Incorrect value retrieved from span", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Span(Regex)"/> method.
        /// </summary>
        private static void SpanByRegexTest(IBrowser browser)
        {
            browser.GoTo(MainURI);

            ISpan span = browser.Span(new Regex("^spanid"));
            Assert.IsNotNull(span);
            Assert.IsTrue(span.Exists);

            Assert.AreEqual("File", span.Text.Trim(), GetErrorMessage("Incorrect value retrieved from span", browser));
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
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Button(Regex)"/> method.
        /// </summary>
        private static void ButtonByRegexTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            IButton button = browser.Button(new Regex("^hello"));

            Assert.IsNotNull(button, GetErrorMessage("The button sought using the regular expression ^Hello could not be found", browser));
            Assert.IsTrue(button.Exists);
            Assert.AreEqual("helloid", button.Id);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Button(AttributeConstraint)"/> method.
        /// </summary>        
        private static void ButtonByAttributeContraintTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            IButton button = browser.Button(Find.ByValue("Show alert"));

            Assert.IsNotNull(button, GetErrorMessage("The button sought using the regular expression ^Hello could not be found", browser));
            Assert.IsTrue(button.Exists);
            Assert.AreEqual("helloid", button.Id);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.CheckBox(string)"/> method.
        /// Tests that a check box can be located based on the value of it's Id.
        /// </summary>
        private static void CheckBoxByIdTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            ICheckBox checkBox = browser.CheckBox("Checkbox21");
            Assert.IsNotNull(checkBox, GetErrorMessage("The checkbox with the id Checkbox21 could not be found.", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Form(string)"/> method.
        /// Tests that a check box can be located based on the value of it's Id.
        /// </summary>
        private static void FormByIdTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            IForm form = browser.Form("Form");
            Assert.IsNotNull(form, GetErrorMessage("The form with the id \"Form\" could not be found.", browser));
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
        /// Test that we can obtain a reference to an image using the elements Id to look it up using <see cref="IElementsContainerTemp.Image(string)"/>.
        /// </summary>
        private static void ImageByIdTest(IBrowser browser)
        {
            browser.GoTo(ImagesURI);
            IImage imageElement = browser.Image("Image1");
            Assert.IsNotNull(imageElement, GetErrorMessage("The image element with the id Image1 could not be found.", browser));
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

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.SelectList(string)"/> method.
        /// Tests that a select list can be located based on the value of it's Id.
        /// </summary>        
        private static void SelectListByIdTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            ISelectList selectList = browser.SelectList("Select1");
            Assert.IsNotNull(selectList, GetErrorMessage("Select list with id \"Select1\" could not be found.", browser));
            
        }

        #endregion     
    }
}
