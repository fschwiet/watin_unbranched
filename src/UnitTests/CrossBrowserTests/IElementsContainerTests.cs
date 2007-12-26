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
        public void AreaByAttributeConstraint()
        {
            ExecuteTest(AreaByAttributeConstraintTest);
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
        public void ButtonByAttributeConstraint()
        {
            ExecuteTest(ButtonByAttributeConstraintTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Buttons"/> property.
        /// </summary>
        [Test]
        public void Buttons()
        {
            ExecuteTest(ButtonsTest);
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
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.CheckBox(Regex)"/> method.
        /// </summary>
        [Test]
        public void CheckBoxByRegex()
        {
            ExecuteTest(CheckBoxByRegexTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.CheckBox(AttributeConstraint)"/> method.
        /// </summary>
        [Test]
        public void CheckBoxByAttributeConstraint()
        {
            ExecuteTest(CheckBoxByAttributeConstraintTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.CheckBoxes"/> property.
        /// </summary>
        [Test]
        public void CheckBoxes()
        {
            ExecuteTest(CheckBoxesTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Div(Regex)"/> method.
        /// </summary>
        [Test]
        public void DivByRegex()
        {
            ExecuteTest(DivByRegexTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Div(AttributeConstraint)"/> method.
        /// </summary>
        [Test]
        public void DivByAttributeConstraint()
        {
            ExecuteTest(DivByAttributeConstraintTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Divs"/> property.
        /// </summary>
        [Test]
        public void Divs()
        {
            ExecuteTest(DivsTest);
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
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Element(Regex)"/> method.
        /// </summary>
        [Test]
        public void ElementByRegex()
        {
            ExecuteTest(ElementByRegexTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Element(AttributeConstraint)"/> method.
        /// </summary>
        [Test]
        public void ElementByAttributeConstraint()
        {
            ExecuteTest(ElementByAttributeConstraintTest);
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
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Form(Regex)"/> method.
        /// </summary>
        [Test]
        public void FormByRegex()
        {
            ExecuteTest(FormByRegexTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Form(AttributeConstraint)"/> method.
        /// </summary>
        [Test]
        public void FormByAttributeConstraint()
        {
            ExecuteTest(FormByAttributeConstraintTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Forms"/> property.
        /// </summary>
        [Test]
        public void Forms()
        {
            ExecuteTest(FormsTest);   
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
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Image(Regex)"/> method.
        /// </summary>
        [Test]
        public void ImageByRegex()
        {
            ExecuteTest(ImageByRegexTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Image(AttributeConstraint)"/> method.
        /// </summary>
        [Test]
        public void ImageByAttributeConstraint()
        {
            ExecuteTest(ImageByAttributeConstraintTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Images"/> property.
        /// </summary>
        [Test]
        public void Images()
        {
            ExecuteTest(ImagesTest);
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
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Label(Regex)"/> method.
        /// </summary>
        [Test]
        public void LabelByRegex()
        {
            ExecuteTest(LabelByRegexTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Label(AttributeConstraint)"/> method.
        /// </summary>
        [Test]
        public void LabelByAttributeConstraint()
        {
            ExecuteTest(LabelByAttributeConstraintTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Labels"/> property.
        /// </summary>
        [Test]
        public void Labels()
        {
            ExecuteTest(LabelsTest);
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
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Link(Regex)"/> method.
        /// </summary>
        [Test]
        public void LinkByRegex()
        {
            ExecuteTest(LinkByRegexTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Link(AttributeConstraint)"/> method.
        /// </summary>
        [Test]
        public void LinkByAttributeConstraint()
        {
            ExecuteTest(LinkByAttributeConstraintTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Links"/> property.
        /// </summary>
        [Test]
        public void Links()
        {
            ExecuteTest(LinksTest);
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
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Para(Regex)"/> method.
        /// </summary>
        [Test]
        public void ParaByRegex()
        {
            ExecuteTest(ParaByRegexTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Para(AttributeConstraint)"/> method.
        /// </summary>
        [Test]
        public void ParaByAttributeConstraint()
        {
            ExecuteTest(ParaByAttributeConstraintTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Paras"/> property.
        /// </summary>
        [Test]
        public void Paras()
        {
            ExecuteTest(ParasTest);
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
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.SelectList(Regex)"/> method.
        /// </summary>
        [Test]
        public void SelectListByRegex()
        {
            ExecuteTest(SelectListByRegexTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.SelectList(AttributeConstraint)"/> method.
        /// </summary>
        [Test]
        public void SelectListByAttributeConstraint()
        {
            ExecuteTest(SelectListByAttributeConstraintTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.SelectLists"/> property.
        /// </summary>
        [Test]
        public void SelectLists()
        {
            ExecuteTest(SelectListsTest);
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
        public void SpanByAttributeConstraint()
        {
            ExecuteTest(SpanByAttributeConstraintTest);
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
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Spans"/> property.
        /// </summary>
        [Test]
        public void Spans()
        {
            ExecuteTest(SpansTest);
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
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Table(AttributeConstraint)"/> method.
        /// </summary>
        [Test]
        public void TableByAttributeConstraint()
        {
            ExecuteTest(TableByAttributeConstraintTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Table(Regex)"/> method.
        /// </summary>
        [Test]
        public void TableByRegex()
        {
            ExecuteTest(TableByRegexTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Tables"/> property.
        /// </summary>
        [Test]
        public void Tables()
        {
            ExecuteTest(TablesTest);
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
            GoTo(ImagesURI, browser);
            IArea area = browser.Area(new Regex("^Are"));

            Assert.IsNotNull(area, GetErrorMessage("The area with id Area1 could not be found", browser));
            Assert.IsTrue(area.Exists);
            Assert.AreEqual("Area1", area.Id);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Area(AttributeConstraint)"/> method.
        /// </summary>        
        private static void AreaByAttributeConstraintTest(IBrowser browser)
        {
            GoTo(ImagesURI, browser);
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
            GoTo(ImagesURI, browser);
            IAreaCollection areas = browser.Areas;

            Assert.IsNotNull(areas, GetErrorMessage("The areas could not be found", browser));
            Assert.AreEqual(2, areas.Length);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Buttons"/> property.
        /// </summary>
        private static void ButtonsTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            IButtonCollection buttons = browser.Buttons;

            Assert.AreEqual(5, buttons.Length, GetErrorMessage("Incorrect no. of button elements returned." , browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Label(string)"/> method.
        /// Tests that a label can be located based on the value of it's Id.
        /// </summary>
        private static void LabelByIdTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            ILabel label = browser.Label("lblB");

            Assert.IsNotNull(label, GetErrorMessage("The table with id lblB could not be found", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Label(Regex)"/> method.
        /// </summary>
        private static void LabelByRegexTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            ILabel label = browser.Label(new Regex("^lblB"));

            Assert.IsNotNull(label, GetErrorMessage("The label sought using the regular expression ^lblB could not be found", browser));
            Assert.IsTrue(label.Exists);
            Assert.AreEqual("lblB", label.Id);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Label(AttributeConstraint)"/> method.
        /// </summary>        
        private static void LabelByAttributeConstraintTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            ILabel label = browser.Label(Find.By("accesskey", "C"));

            Assert.IsNotNull(label, GetErrorMessage("The label sought using attribute accesskey could not be found", browser));
            Assert.IsTrue(label.Exists);
            Assert.AreEqual("Checkbox21", label.For);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Labels"/> property.
        /// </summary>
        private static void LabelsTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            ILabelCollection labels = browser.Labels;
            Assert.AreEqual(3, labels.Length, GetErrorMessage("Incorrect no. of labels returned", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Table(string)"/> method.
        /// Tests that a table can be located based on the value of it's Id.
        /// </summary>
        private static void TableByIdTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            ITable table = browser.Table("table1");

            Assert.IsNotNull(table, GetErrorMessage("The table with id table1 could not be found", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.TableBody(string)"/> method.
        /// Tests that a table body can be located based on the value of it's Id.
        /// </summary>
        private static void TableBodyByIdTest(IBrowser browser)
        {
            GoTo(TablesURI, browser);
            ITableBody tableBody = browser.TableBody("tbody2");

            Assert.IsNotNull(tableBody, GetErrorMessage("The table body with id tbody2 could not be found", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Span(string)"/> method.
        /// </summary>
        private static void SpanByIdTest(IBrowser browser)
        {
            GoTo(MainURI, browser);

            ISpan span = browser.Span("spanid1");
            Assert.IsNotNull(span);
            Assert.IsTrue(span.Exists);

            Assert.AreEqual("File", span.Text.Trim(), GetErrorMessage("Incorrect value retrieved from span", browser));
                 
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Span(AttributeConstraint)"/> method.
        /// </summary>
        private static void SpanByAttributeConstraintTest(IBrowser browser)
        {
            GoTo(MainURI, browser);

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
            GoTo(MainURI, browser);

            ISpan span = browser.Span(new Regex("^spanid"));
            Assert.IsNotNull(span);
            Assert.IsTrue(span.Exists);

            Assert.AreEqual("File", span.Text.Trim(), GetErrorMessage("Incorrect value retrieved from span", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Table(AttributeConstraint)"/> method.
        /// </summary>
        private static void TableByAttributeConstraintTest(IBrowser browser)
        {
            GoTo(TablesURI, browser);

            ITable table = browser.Table(Find.By("cellSpacing", "5"));
            Assert.IsNotNull(table);
            Assert.IsTrue(table.Exists);

            Assert.AreEqual("Table1", table.Id, GetErrorMessage("Incorrect id retrieved from table", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Table(Regex)"/> method.
        /// </summary>
        private static void TableByRegexTest(IBrowser browser)
        {
            GoTo(TablesURI, browser);

            ITable table = browser.Table(new Regex("^Table2"));
            Assert.IsNotNull(table);
            Assert.IsTrue(table.Exists);

            Assert.AreEqual("Table2", table.Id, GetErrorMessage("Incorrect id retrieved from table", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Tables"/> property.
        /// </summary>
        private static void TablesTest(IBrowser browser)
        {
            GoTo(TablesURI, browser);
            ITableCollection tables = browser.Tables;
            Assert.AreEqual(2, tables.Length, GetErrorMessage("Incorrect no. of tables returned", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.TableRow(string)"/> method.
        /// Tests that a table row can be located based on the value of it's Id.
        /// </summary>
        private static void TableRowByIdTest(IBrowser browser)
        {
            GoTo(TablesURI, browser);
            ITableRow row = browser.TableRow("1");

            Assert.IsNotNull(row, GetErrorMessage("The row with id 1 could not be found", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.TableCell(string)"/> method.
        /// Tests that a table row can be located based on the value of it's Id.
        /// </summary>
        private static void TableCellByIdTest(IBrowser browser)
        {
            GoTo(TablesURI, browser);
            ITableCell cell = browser.TableCell("innerCell1");

            Assert.IsNotNull(cell, GetErrorMessage("The cell with id innerCell1 could not be found", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Button(string)"/> method.
        /// Tests that a button can be located based on the value of it's Id.
        /// </summary>
        private static void ButtonByIdTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            IButton button = browser.Button("helloid");

            Assert.IsNotNull(button, GetErrorMessage("The button with id helloid could not be found", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Button(Regex)"/> method.
        /// </summary>
        private static void ButtonByRegexTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            IButton button = browser.Button(new Regex("^hello"));

            Assert.IsNotNull(button, GetErrorMessage("The button sought using the regular expression ^Hello could not be found", browser));
            Assert.IsTrue(button.Exists);
            Assert.AreEqual("helloid", button.Id);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Button(AttributeConstraint)"/> method.
        /// </summary>        
        private static void ButtonByAttributeConstraintTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            IButton button = browser.Button(Find.ByValue("Show alert"));

            Assert.IsNotNull(button, GetErrorMessage("The button sought using the value Show alert could not be found", browser));
            Assert.IsTrue(button.Exists);
            Assert.AreEqual("helloid", button.Id);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.CheckBox(string)"/> method.
        /// Tests that a check box can be located based on the value of it's Id.
        /// </summary>
        private static void CheckBoxByIdTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            ICheckBox checkBox = browser.CheckBox("Checkbox21");
            Assert.IsNotNull(checkBox, GetErrorMessage("The checkbox with the id Checkbox21 could not be found.", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.CheckBox(Regex)"/> method.
        /// </summary>
        private static void CheckBoxByRegexTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            ICheckBox checkBox = browser.CheckBox(new Regex("^Checkbox3"));

            Assert.IsNotNull(checkBox, GetErrorMessage("The checkbox sought using the regular expression ^Checkbox3 could not be found", browser));
            Assert.IsTrue(checkBox.Exists);
            Assert.AreEqual("Checkbox3", checkBox.Id);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.CheckBox(AttributeConstraint)"/> method.
        /// </summary>        
        private static void CheckBoxByAttributeConstraintTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            ICheckBox checkBox = browser.CheckBox(Find.ByValue("Effe teste"));

            Assert.IsNotNull(checkBox, GetErrorMessage("The checkbox sought using value Effe teste could not be found", browser));
            Assert.IsTrue(checkBox.Exists);
            Assert.AreEqual("Checkbox3", checkBox.Id);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.CheckBoxes"/> property.
        /// </summary>
        private static void CheckBoxesTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            ICheckBoxCollection checkBoxes = browser.CheckBoxes;
            Assert.AreEqual(5, checkBoxes.Length, GetErrorMessage("Incorrect no. of checkboxes returned", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Div(Regex)"/> method.
        /// </summary>
        private static void DivByRegexTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            IDiv div = browser.Div(new Regex("^div"));

            Assert.IsNotNull(div, GetErrorMessage("The div sought using the regular expression ^div could not be found", browser));
            Assert.IsTrue(div.Exists);
            Assert.AreEqual("divid", div.Id);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Div(AttributeConstraint)"/> method.
        /// </summary>        
        private static void DivByAttributeConstraintTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            IDiv div = browser.Div(Find.By("ms_positioning", "FlowLayout"));

            Assert.IsNotNull(div, GetErrorMessage("The div sought using attribute ms_positioning could not be found", browser));
            Assert.IsTrue(div.Exists);
            Assert.AreEqual("divid", div.Id);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Divs"/> property.
        /// </summary>
        private static void DivsTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            IDivCollection divs = browser.Divs;
            Assert.AreEqual(1, divs.Length, GetErrorMessage("Incorrect no. of divs returned", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Element(Regex)"/> method.
        /// </summary>
        private static void ElementByRegexTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            IElement element = browser.Element(new Regex("^name"));

            Assert.IsNotNull(element, GetErrorMessage("The element sought using the regular expression ^name could not be found", browser));
            Assert.IsTrue(element.Exists);
            Assert.AreEqual("name", element.Id);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Element(AttributeConstraint)"/> method.
        /// </summary>        
        private static void ElementByAttributeConstraintTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            IElement element = browser.Element(Find.ByTitle("Textfield title"));

            Assert.IsNotNull(element, GetErrorMessage("The element sought using title \"Textfield title\" could not be found", browser));
            Assert.IsTrue(element.Exists);
            Assert.AreEqual("name", element.Id);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Form(string)"/> method.
        /// Tests that a check box can be located based on the value of it's Id.
        /// </summary>
        private static void FormByIdTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            IForm form = browser.Form("Form");
            Assert.IsNotNull(form, GetErrorMessage("The form with the id \"Form\" could not be found.", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Form(Regex)"/> method.
        /// </summary>
        private static void FormByRegexTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            IForm form = browser.Form(new Regex("^For"));

            Assert.IsNotNull(form, GetErrorMessage("The form sought using the regular expression ^For could not be found", browser));
            Assert.IsTrue(form.Exists);
            Assert.AreEqual("Form", form.Id);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Form(AttributeConstraint)"/> method.
        /// </summary>        
        private static void FormByAttributeConstraintTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            IForm form = browser.Form(Find.By("method", "get"));

            Assert.IsNotNull(form, GetErrorMessage("The form sought using attribute method and the value get could not be found", browser));
            Assert.IsTrue(form.Exists);
            Assert.AreEqual("Form", form.Id);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Divs"/> property.
        /// </summary>
        private static void FormsTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            IDivCollection divs = browser.Divs;
            Assert.AreEqual(1, divs.Length, GetErrorMessage("Incorrect no. of divs returned", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Image(Regex)"/> method.
        /// </summary>
        private static void ImageByRegexTest(IBrowser browser)
        {
            GoTo(ImagesURI, browser);
            IImage image = browser.Image(new Regex("^Image1"));

            Assert.IsNotNull(image, GetErrorMessage("The image sought using the regular expression ^Image1 could not be found", browser));
            Assert.IsTrue(image.Exists);
            Assert.AreEqual("Image1", image.Id);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Image(AttributeConstraint)"/> method.
        /// </summary>        
        private static void ImageByAttributeConstraintTest(IBrowser browser)
        {
            GoTo(ImagesURI, browser);
            IImage image = browser.Image(Find.ByAlt("Picture does not exist."));

            Assert.IsNotNull(image, GetErrorMessage("The image sought using attribute alt could not be found", browser));
            Assert.IsTrue(image.Exists);
            Assert.AreEqual("Image3", image.Id);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Images"/> property.
        /// </summary>
        private static void ImagesTest(IBrowser browser)
        {
            GoTo(ImagesURI, browser);
            IImageCollection images = browser.Images;
            Assert.AreEqual(4, images.Length, GetErrorMessage("Incorrect no. of images returned", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Link(Regex)"/> method.
        /// </summary>
        private static void LinkByRegexTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            ILink link = browser.Link(new Regex("^testlink"));

            Assert.IsNotNull(link, GetErrorMessage("The link sought using the regular expression ^testlink could not be found", browser));
            Assert.IsTrue(link.Exists);
            Assert.AreEqual("testlinkid", link.Id);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Link(AttributeConstraint)"/> method.
        /// </summary>        
        private static void LinkByAttributeConstraintTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            ILink link = browser.Link(Find.By("target", "_blank"));

            Assert.IsNotNull(link, GetErrorMessage("The link sought using attribute target could not be found", browser));
            Assert.IsTrue(link.Exists);
            Assert.AreEqual("testlinkid", link.Id);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Links"/> property.
        /// </summary>
        private static void LinksTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            ILinkCollection links = browser.Links;
            Assert.AreEqual(3, links.Length, GetErrorMessage("Incorrect no. of links returned", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Para(Regex)"/> method.
        /// </summary>
        private static void ParaByRegexTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            IPara para = browser.Para(new Regex("^testElement"));

            Assert.IsNotNull(para, GetErrorMessage("The paragraph sought using the regular expression ^testlink could not be found", browser));
            Assert.IsTrue(para.Exists);
            Assert.AreEqual("testElementAttributes", para.Id);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Para(AttributeConstraint)"/> method.
        /// </summary>        
        private static void ParaByAttributeConstraintTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            IPara para = browser.Para(Find.ByClass("p1main"));

            Assert.IsNotNull(para, GetErrorMessage("The paragraph sought using class attribute could not be found", browser));
            Assert.IsTrue(para.Exists);
            Assert.AreEqual("testElementAttributes", para.Id);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Paras"/> property.
        /// </summary>
        private static void ParasTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            IParaCollection paras = browser.Paras;
            Assert.AreEqual(9, paras.Length, GetErrorMessage("Incorrect no. of paragraphs returned", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Area(string)"/> method.
        /// Tests that an area can be located based on the value of it's Id.
        /// </summary>
        private static void AreaByIdTest(IBrowser browser)
        {
            GoTo(ImagesURI, browser);
            IArea area = browser.Area("Area1");

            Assert.IsNotNull(area, GetErrorMessage("The area with id Area1 could not be found", browser));
        }

        /// <summary>
        /// Test that we can obtain a reference to a paragraph using the elements Id to look it up.
        /// Once the element is found we assert properties unique to the <see cref="IPara"/> interface.
        /// </summary>
        private static void ParaByIdTest(IBrowser browser)
        {
            GoTo(MainURI, browser);

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
            GoTo(MainURI, browser);
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
            GoTo(ImagesURI, browser);
            IImage imageElement = browser.Image("Image1");
            Assert.IsNotNull(imageElement, GetErrorMessage("The image element with the id Image1 could not be found.", browser));
        }

        /// <summary>
        /// Test that we can obtain a reference to a div using the elements Id to look it up.
        /// Once the element is found we assert properties unique to the <see cref="IDiv"/> interface.
        /// </summary>
        private static void DivByIdTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
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
            GoTo(MainURI, browser);
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
            GoTo(MainURI, browser);
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
            GoTo(MainURI, browser);
            ISelectList selectList = browser.SelectList("Select1");
            Assert.IsNotNull(selectList, GetErrorMessage("Select list with id \"Select1\" could not be found.", browser));            
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.SelectList(Regex)"/>.
        /// </summary>
        private static void SelectListByRegexTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            ISelectList selectList = browser.SelectList(new Regex("^Select1"));

            Assert.IsNotNull(selectList, GetErrorMessage("The select list sought using the regular expression ^Select1 could not be found", browser));
            Assert.IsTrue(selectList.Exists);
            Assert.AreEqual("Select1", selectList.Id);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.SelectList(AttributeConstraint)"/> method.
        /// </summary>        
        private static void SelectListByAttributeConstraintTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            ISelectList selectList = browser.SelectList(Find.By("size", "2"));

            Assert.IsNotNull(selectList, GetErrorMessage("The select list sought using size attribute could not be found", browser));
            Assert.IsTrue(selectList.Exists);
            Assert.AreEqual("Select2", selectList.Id);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.SelectLists"/> property.
        /// </summary>
        private static void SelectListsTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            ISelectListCollection selectListCollection = browser.SelectLists;
            Assert.AreEqual(5, selectListCollection.Length, GetErrorMessage("Incorrect no. of select lists returned", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IElementsContainerTemp.Spans"/> property.
        /// </summary>
        private static void SpansTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            ISpanCollection spanCollection = browser.Spans;
            Assert.AreEqual(2, spanCollection.Length, GetErrorMessage("Incorrect no. of spans returned", browser));
        }

        #endregion     
    }
}
