using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;

using NUnit.Framework;

using WatiN.Exceptions;

namespace WatiN.Tests
{
  [TestFixture]
  public class Elements
  {
    private static Uri testDataBaseURI ;
    private static Uri mainURI;
    private static Uri googleURI;
    private IE ie;

    [TestFixtureSetUp]
    public void Setup()
    {
      System.Threading.Thread.CurrentThread.ApartmentState = System.Threading.ApartmentState.STA;

      string testDataLocation = string.Format(@"{0}\testdata\", new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName);
      testDataBaseURI = new Uri(testDataLocation);
      mainURI = new Uri(testDataBaseURI, "main.html");

      googleURI = new Uri("http://www.google.com");

      ie = new IE(mainURI.ToString());
    }

    [TestFixtureTearDown]
    public void tearDown()
    {
      ie.Close();
    }

    [Test]
    public void Table()
    {
      const string tableId = "table1";

      Assert.AreEqual(tableId,  ie.MainDocument.Table(Find.ByID(tableId)).Id);
      
      Table table = ie.MainDocument.Table(tableId);
      Assert.AreEqual(tableId,  table.Id);
      Assert.AreEqual(tableId,  table.ToString());
      Assert.AreEqual(2, table.TableRows.length, "Unexpected number of rows");

      TableRow row = table.FindRow("a1",0);
      Assert.IsNotNull(row, "Row with a1 expected");
      Assert.AreEqual("a1", row.TableCells[0].Text, "Unexpected text in cell");
      
      row = table.FindRow("A2",1);
      Assert.IsNotNull(row, "Row with a1 expected");
      Assert.AreEqual("a2", row.TableCells[1].Text, "Unexpected text in cell");

      row = table.FindRow("b2",1);
      Assert.IsNotNull(row, "Row with b1 expected");
      Assert.AreEqual("b2", row.TableCells[1].Text, "Unexpected text in cell");

      row = table.FindRow("c1",0);
      Assert.IsNull(row, "No row with c1 expected");
    }

    [Test]
    public void Tables()
    {
      // Collection.length
      TableCollection tables = ie.MainDocument.Tables;
      
      Assert.AreEqual(2, tables.length);

      // Collection items by index
      Assert.AreEqual("table1", tables[0].Id);
      Assert.AreEqual("table2", tables[1].Id);

      IEnumerable tableEnumerable = tables;
      IEnumerator tableEnumerator = tableEnumerable.GetEnumerator();

      // Collection iteration and comparing the result with Enumerator
      int count = 0;
      foreach (Table table in tables)
      {
        tableEnumerator.MoveNext();
        object enumTable = tableEnumerator.Current;
        
        Assert.IsInstanceOfType(table.GetType(), enumTable, "Types are not the same");
        Assert.AreEqual(table.OuterHtml, ((Table)enumTable).OuterHtml, "foreach en IEnumator don't act the same.");
        ++count;
      }
      
      Assert.IsFalse(tableEnumerator.MoveNext(), "Expected last item");
      Assert.AreEqual(2, count);
    }

    [Test]
    public void TableRows()
    {
      // Collection.length
      TableRowCollection rows = ie.MainDocument.Table("table1").TableRows;
      
      Assert.AreEqual(2, rows.length);

      // Collection items by index
      Assert.AreEqual("row0", rows[0].Id);
      Assert.AreEqual("row1", rows[1].Id);

      IEnumerable rowEnumerable = rows;
      IEnumerator rowEnumerator = rowEnumerable.GetEnumerator();

      // Collection iteration and comparing the result with Enumerator
      int count = 0;
      foreach (TableRow row in rows)
      {
        rowEnumerator.MoveNext();
        object enumTable = rowEnumerator.Current;
        
        Assert.IsInstanceOfType(row.GetType(), enumTable, "Types are not the same");
        Assert.AreEqual(row.OuterHtml, ((TableRow)enumTable).OuterHtml, "foreach en IEnumator don't act the same.");
        ++count;
      }
      
      Assert.IsFalse(rowEnumerator.MoveNext(), "Expected last item");
      Assert.AreEqual(2, count);
    }

    [Test]
    public void TableCellByIndex()
    {
      // accessing several occurences with equal id
      Assert.AreEqual("a1", ie.MainDocument.TableCell("td1", 0).Text);
      Assert.AreEqual("b1", ie.MainDocument.TableCell("td1", 1).Text);
    }

    [Test]
    public void TableCells()
    {
      // Collection.length
      TableCellCollection cells = ie.MainDocument.Table("table1").TableRows[0].TableCells;
      
      Assert.AreEqual(2, cells.length);

      // Collection items by index
      Assert.AreEqual("td1", cells[0].Id);
      Assert.AreEqual("td2", cells[1].Id);

      IEnumerable cellEnumerable = cells;
      IEnumerator cellEnumerator = cellEnumerable.GetEnumerator();

      // Collection iteration and comparing the result with Enumerator
      int count = 0;
      foreach (TableCell cell in cells)
      {
        cellEnumerator.MoveNext();
        object enumTable = cellEnumerator.Current;
        
        Assert.IsInstanceOfType(cell.GetType(), enumTable, "Types are not the same");
        Assert.AreEqual(cell.OuterHtml, ((TableCell)enumTable).OuterHtml, "foreach en IEnumator don't act the same.");
        ++count;
      }
      
      Assert.IsFalse(cellEnumerator.MoveNext(), "Expected last item");
      Assert.AreEqual(2, count);
    }

    [Test]
    public void Button()
    {
      const string popupValue = "Show modeless dialog";
      Assert.AreEqual(popupValue, ie.MainDocument.Button(Find.ByID("popupid")).Value);
      Assert.AreEqual(popupValue, ie.MainDocument.Button("popupid").Value);
      Assert.AreEqual(popupValue, ie.MainDocument.Button("popupid").ToString());
      Assert.AreEqual(popupValue, ie.MainDocument.Button(Find.ByName("popupname")).Value);
      Assert.AreEqual(popupValue, ie.MainDocument.Button(Find.ByValue(popupValue)).Value);
      
      Button helloButton = ie.MainDocument.Button("helloid");
      Assert.AreEqual("Show allert", helloButton.Value);
      Assert.AreEqual(helloButton.Value, helloButton.Text);
    }

    [Test, ExpectedException(typeof(ElementDisabledException))]
    public void ButtonDisabledException()
    {
      ie.MainDocument.Button("disabledid").Click();
    }

    [Test]
    public void Buttons()
    {
      const int expectedButtonsCount = 4;
      Assert.AreEqual(expectedButtonsCount, ie.MainDocument.Buttons.length, "Unexpected number of buttons");

      Form form = ie.MainDocument.Form("Form");

      // Collection.length
      ButtonCollection formButtons = form.Buttons;
      
      Assert.AreEqual(expectedButtonsCount, formButtons.length);

      // Collection items by index
      Assert.AreEqual("popupid", form.Buttons[0].Id);
      Assert.AreEqual("modalid", form.Buttons[1].Id);
      Assert.AreEqual("helloid", form.Buttons[2].Id);

      IEnumerable buttonEnumerable = formButtons;
      IEnumerator buttonEnumerator = buttonEnumerable.GetEnumerator();

      // Collection iteration and comparing the result with Enumerator
      int count = 0;
      foreach (Button inputButton in formButtons)
      {
        buttonEnumerator.MoveNext();
        object enumButton = buttonEnumerator.Current;
        
        Assert.IsInstanceOfType(inputButton.GetType(), enumButton, "Types are not the same");
        Assert.AreEqual(inputButton.OuterHtml, ((Button)enumButton).OuterHtml, "foreach en IEnumator don't act the same.");
        ++count;
      }
      
      Assert.IsFalse(buttonEnumerator.MoveNext(), "Expected last item");
      Assert.AreEqual(expectedButtonsCount, count);
    }

    [Test]
    public void CheckBox()
    {
      CheckBox checkbox1 = ie.MainDocument.CheckBox("Checkbox1");

      Assert.AreEqual("Checkbox1", checkbox1.Id, "Found wrong checkbox");
      Assert.AreEqual("Checkbox1", checkbox1.ToString(), "ToString didn't return Id");
      Assert.IsTrue(checkbox1.Checked, "Should initially be checked");
      
      checkbox1.Checked = false;
      Assert.IsFalse(checkbox1.Checked, "Should not be checked");

      checkbox1.Checked = true;
      Assert.IsTrue(checkbox1.Checked, "Should be checked");
    }

    [Test]
    public void CheckBoxes()
    {
      Assert.AreEqual(5, ie.MainDocument.CheckBoxs.length, "Unexpected number of checkboxes");

      CheckBoxCollection formCheckBoxs = ie.MainDocument.Form("FormCheckboxes").CheckBoxs;

      // Collection items by index
      Assert.AreEqual(3,formCheckBoxs.length, "Wrong number off checkboxes");
      Assert.AreEqual("Checkbox1", formCheckBoxs[0].Id);
      Assert.AreEqual("Checkbox2", formCheckBoxs[1].Id);
      Assert.AreEqual("Checkbox4", formCheckBoxs[2].Id);      

      // Collection iteration and comparing the result with Enumerator
      IEnumerable checkboxEnumerable = formCheckBoxs;
      IEnumerator checkboxEnumerator = checkboxEnumerable.GetEnumerator();

      int count = 0;
      foreach (CheckBox checkBox in formCheckBoxs)
      {
        checkboxEnumerator.MoveNext();
        object enumCheckbox = checkboxEnumerator.Current;
        
        Assert.IsInstanceOfType(checkBox.GetType(), enumCheckbox, "Types are not the same");
        Assert.AreEqual(checkBox.OuterHtml, ((CheckBox)enumCheckbox).OuterHtml, "foreach en IEnumator don't act the same.");
        ++count;
      }
      
      Assert.IsFalse(checkboxEnumerator.MoveNext(), "Expected last item");
      Assert.AreEqual(3, count);
    }

    [Test]
    public void Element()
    {
      const string tableId = "table1";

      Element element = ie.MainDocument.Table(Find.ByID(tableId));

      Assert.IsNotNull(element,  "Element not found");
      Assert.AreEqual(tableId, element.GetAttributeValue("id"), "GetAttributeValue id failed");
      Assert.IsNull(element.GetAttributeValue("watin"), "GetAttributeValue watin should return null");
      Assert.AreEqual("table", element.TagName.ToLower(), "Invalid tagname");

      // Textbefore and TextAfter tests
      CheckBox checkBox = ie.MainDocument.CheckBox("Checkbox21");
      Assert.AreEqual("Test label before: ", checkBox.TextBefore, "Unexpected checkBox.TextBefore");
      Assert.AreEqual(" Test label after", checkBox.TextAfter, "Unexpected checkBox.TextAfter");
    }

    [Test]
    public void LabelByFor()
    {
      Label label = ie.MainDocument.Label(Find.ByFor("Checkbox21"));

      Assert.AreEqual("Checkbox21", label.For, "Unexpected label.For id");
      Assert.AreEqual("label for Checkbox21", label.Text, "Unexpected label.Text");
    }

    [Test]
    public void LabelWrapped()
    {
      LabelCollection labelCollection = ie.MainDocument.Labels;
      Assert.AreEqual(2, ie.MainDocument.Labels.length, "Unexpected number of labels");

      Label label = labelCollection[1];

      Assert.AreEqual(null, label.For, "Unexpected label.For id");
      Assert.AreEqual("Test label before:  Test label after", label.Text, "Unexpected label.Text");

      // Element.TextBefore and Element.TextAfter are tested in test method Element
    }

    [Test]
    public void Labels()
    {
      const int expectedLabelCount = 2;

      Assert.AreEqual(expectedLabelCount, ie.MainDocument.Labels.length, "Unexpected number of labels");

      LabelCollection labelCollection = ie.MainDocument.Labels;

      // Collection items by index
      Assert.AreEqual(expectedLabelCount,labelCollection.length, "Wrong number of labels");

      // Collection iteration and comparing the result with Enumerator
      IEnumerable labelEnumerable = labelCollection;
      IEnumerator labelEnumerator = labelEnumerable.GetEnumerator();

      int count = 0;
      foreach (Label label in labelCollection)
      {
        labelEnumerator.MoveNext();
        object enumCheckbox = labelEnumerator.Current;
        
        Assert.IsInstanceOfType(label.GetType(), enumCheckbox, "Types are not the same");
        Assert.AreEqual(label.OuterHtml, ((Label)enumCheckbox).OuterHtml, "foreach en IEnumator don't act the same.");
        ++count;
      }
      
      Assert.IsFalse(labelEnumerator.MoveNext(), "Expected last item");
      Assert.AreEqual(expectedLabelCount, count);
    }

    [Test]
    public void Link()
    {
      Assert.AreEqual(googleURI.ToString(), ie.MainDocument.Link(Find.ByID("testlinkid")).Url);
      Assert.AreEqual(googleURI.ToString(), ie.MainDocument.Link("testlinkid").Url);
      Assert.AreEqual(googleURI.ToString(), ie.MainDocument.Link(Find.ByName("testlinkname")).Url);
      Assert.AreEqual(googleURI.ToString(), ie.MainDocument.Link(Find.ByUrl(googleURI.ToString())).Url);
      Assert.AreEqual("Microsoft", ie.MainDocument.Link(Find.ByText("Microsoft")).Text);
    }

    [Test, ExpectedException(typeof(ElementNotFoundException),"Could not find a 'A' tag containing attribute id with value 'noexcistinglinkid'")]
    public void LinkFindByInvalidEnum()
    {
      Assert.AreEqual(googleURI.ToString(), ie.MainDocument.Link(Find.ByID("noexcistinglinkid")).Url);
    }

    [Test]
    public void Links()
    {
      const int expectedLinkCount = 3;

      Assert.AreEqual(expectedLinkCount, ie.MainDocument.Links.length, "Unexpected number of links");

      LinkCollection links = ie.MainDocument.Links;

      // Collection items by index
      Assert.AreEqual(expectedLinkCount,links.length, "Wrong number off links");
      Assert.AreEqual("testlinkid", links[0].Id);
      Assert.AreEqual("testlinkid1", links[1].Id);

      // Collection iteration and comparing the result with Enumerator
      IEnumerable linksEnumerable = links;
      IEnumerator linksEnumerator = linksEnumerable.GetEnumerator();

      int count = 0;
      foreach (Link link in links)
      {
        linksEnumerator.MoveNext();
        object enumLink = linksEnumerator.Current;
        
        Assert.IsInstanceOfType(link.GetType(), enumLink, "Types are not the same");
        Assert.AreEqual(link.OuterHtml, ((Link)enumLink).OuterHtml, "foreach en IEnumator don't act the same.");
        ++count;
      }
      
      Assert.IsFalse(linksEnumerator.MoveNext(), "Expected last item");
      Assert.AreEqual(expectedLinkCount, count);
    }


    [Test]
    public void MultiSelectList()
    {
      SelectList selectList = ie.MainDocument.SelectList("Select2");

      Assert.IsNotNull(selectList,"SelectList niet aangetroffen");

      StringCollection items = selectList.AllContents;

      Assert.AreEqual(4, items.Count);
      Assert.AreEqual("First Listitem",items[0],"First Listitem not found");
      Assert.AreEqual("Second Listitem",items[1],"Second Listitem not found");
      Assert.AreEqual("Third Listitem",items[2],"Third Listitem not found");
      Assert.AreEqual("Fourth Listitem",items[3],"Fourth Listitem not found");

      Assert.IsTrue(selectList.Multiple, "'Select 2' must be allow multiple selection to pass the next tests");
              
      // Second Listitem is selected by default/loading the page
      Assert.AreEqual(1,selectList.SelectedItems.Count, "SelectedItem not selected on page load");

      selectList.ClearList();
      Assert.AreEqual(0,selectList.SelectedItems.Count, "After ClearList there are still items selected");
      Assert.IsFalse(selectList.HasSelectedItems, "No selected items expected");

      selectList.Select("Third Listitem");

      Assert.IsTrue(selectList.HasSelectedItems, "Selecteditems expected after first select");
      Assert.AreEqual(1,selectList.SelectedItems.Count, "Wrong number of items selected after Select Third Listitem");
      Assert.AreEqual("Third Listitem",selectList.SelectedItems[0],"Third Listitem not selected after Select");

      selectList.Select("First Listitem");

      Assert.IsTrue(selectList.HasSelectedItems, "Selecteditems expected after second select");
      Assert.AreEqual(2,selectList.SelectedItems.Count, "Wrong number of items selected after Select First Listitem");
      Assert.AreEqual("First Listitem",selectList.SelectedItems[0],"First Listitem not selected after Select");
      Assert.AreEqual("Third Listitem",selectList.SelectedItems[1],"Third Listitem not selected after Select");
    }

    [Test]
    public void RadioButton()
    {
      RadioButton RadioButton1 = ie.MainDocument.RadioButton("Radio1");

      Assert.AreEqual("Radio1", RadioButton1.Id, "Found wrong RadioButton.");
      Assert.AreEqual("Radio1", RadioButton1.ToString(), "ToString didn't return the Id.");
      Assert.IsTrue(RadioButton1.Checked, "Should initially be checked");
      
      RadioButton1.Checked = false;
      Assert.IsFalse(RadioButton1.Checked, "Should not be checked");

      RadioButton1.Checked = true;
      Assert.IsTrue(RadioButton1.Checked, "Should be checked");
    }

    [Test]
    public void RadioButtons()
    {
      Assert.AreEqual(3, ie.MainDocument.RadioButtons.length, "Unexpected number of RadioButtons");

      RadioButtonCollection formRadioButtons = ie.MainDocument.Form("FormRadioButtons").RadioButtons;

      Assert.AreEqual(2,formRadioButtons.length, "Wrong number off RadioButtons");
      Assert.AreEqual("Radio2", formRadioButtons[0].Id);
      Assert.AreEqual("Radio3", formRadioButtons[1].Id);

      // Collection iteration and comparing the result with Enumerator
      IEnumerable radiobuttonEnumerable = formRadioButtons;
      IEnumerator radiobuttonEnumerator = radiobuttonEnumerable.GetEnumerator();

      int count = 0;
      foreach (RadioButton radioButton in formRadioButtons)
      {
        radiobuttonEnumerator.MoveNext();
        object enumRadioButton = radiobuttonEnumerator.Current;
        
        Assert.IsInstanceOfType(radioButton.GetType(), enumRadioButton, "Types are not the same");
        Assert.AreEqual(radioButton.OuterHtml, ((RadioButton)enumRadioButton).OuterHtml, "foreach en IEnumator don't act the same.");
        ++count;
      }
      
      Assert.IsFalse(radiobuttonEnumerator.MoveNext(), "Expected last item");
      Assert.AreEqual(2, count);

    }

    [Test]
    public void SingleSelectList()
    {
      SelectList selectList = ie.MainDocument.SelectList("Select1");

      Assert.IsNotNull(selectList,"SelectList niet aangetroffen");

      Assert.IsFalse(selectList.Multiple, "Select 1 must not allow multiple selection to pass the next tests");

      Assert.AreEqual(1,selectList.SelectedItems.Count, "'First text' not selected on page load");
      Assert.AreEqual("First text", selectList.SelectedItems[0]);
              
      selectList.ClearList();
      Assert.AreEqual(1,selectList.SelectedItems.Count, "SelectedItem should still be selected after ClearList");
      Assert.IsTrue(selectList.HasSelectedItems, "Selected item expected");

      selectList.Select("Second text");
      Assert.IsTrue(selectList.HasSelectedItems, "Selected item expected");
      Assert.AreEqual(1,selectList.SelectedItems.Count, "Unexpected count");
      Assert.AreEqual("Second text", selectList.SelectedItems[0], "Unexpected SelectedItem by index");
      Assert.AreEqual("Second text", selectList.SelectedItem, "Unexpected SelectedItem");

      selectList.SelectByValue("3");
      Assert.AreEqual("Third text", selectList.SelectedItem, "Unexpected SelectedItem");
    }

    [Test, ExpectedException(typeof(SelectListItemNotFoundException), "No item with text or value 'None excisting item' was found in the selectlist")]
    public void SingleSelectItemNotFoundException()
    {
      SelectList selectList = ie.MainDocument.SelectList("Select1");
      selectList.Select("None excisting item");
    }

    [Test]
    public void SelectLists()
    {
      Assert.AreEqual(2, ie.MainDocument.SelectLists.length);

      // Collections
      SelectListCollection selectLists = ie.MainDocument.SelectLists;

      Assert.AreEqual(2, selectLists.length);

      // Collection items by index
      Assert.AreEqual("Select1", selectLists[0].Id);
      Assert.AreEqual("Select2", selectLists[1].Id);

      IEnumerable selectListEnumerable = selectLists;
      IEnumerator selectListEnumerator = selectListEnumerable.GetEnumerator();

      // Collection iteration and comparing the result with Enumerator
      int count = 0;
      foreach (SelectList selectList in selectLists)
      {
        selectListEnumerator.MoveNext();
        object enumSelectList = selectListEnumerator.Current;
        
        Assert.IsInstanceOfType(selectList.GetType(), enumSelectList, "Types are not the same");
        Assert.AreEqual(selectList.OuterHtml, ((SelectList)enumSelectList).OuterHtml, "foreach en IEnumator don't act the same.");
        ++count;
      }
      
      Assert.IsFalse(selectListEnumerator.MoveNext(), "Expected last item");
      Assert.AreEqual(2, count);
    }

    [Test]
    public void TextField()
    {
      const string value = "Hello world!";
      const string appendValue = " This is WatiN!";
      TextField textfieldName = ie.MainDocument.TextField("name");

      Assert.AreEqual("name", textfieldName.Id);
      Assert.AreEqual("textinput1", textfieldName.Name);

      Assert.AreEqual(200, textfieldName.MaxLength, "Unexpected maxlenght");

      Assert.IsNull(textfieldName.Value, "Initial value should be null");
      
      textfieldName.TypeText(value);
      Assert.AreEqual(value,textfieldName.Value, "TypeText not OK");

      textfieldName.AppendText(appendValue) ;
      Assert.AreEqual(value + appendValue,textfieldName.Value, "AppendText not OK");

      textfieldName.Clear();
      Assert.IsNull(textfieldName.Value, "After Clear value should by null");

      textfieldName.Value = value;
      Assert.AreEqual(value,textfieldName.Value, "Value not OK");
      Assert.AreEqual(value,textfieldName.Text, "Text not OK");
    }

    [Test]
    public void TextFieldTextAreaElement()
    {
      const string value = "Hello world!";
      const string appendValue = " This is WatiN!";
      TextField textfieldName = ie.MainDocument.TextField("Textarea1");

      Assert.AreEqual("Textarea1", textfieldName.Id);
      Assert.AreEqual("TextareaName", textfieldName.Name);

      Assert.AreEqual(0, textfieldName.MaxLength, "Unexpected maxlenght");

      Assert.IsNull(textfieldName.Value, "Initial value should be null");
      
      textfieldName.TypeText(value);
      Assert.AreEqual(value,textfieldName.Value, "TypeText not OK");

      textfieldName.AppendText(appendValue) ;
      Assert.AreEqual(value + appendValue,textfieldName.Value, "AppendText not OK");

      textfieldName.Clear();
      Assert.IsNull(textfieldName.Value, "After Clear value should by null");

      textfieldName.Value = value;
      Assert.AreEqual(value,textfieldName.Value, "Value not OK");
      Assert.AreEqual(value,textfieldName.Text, "Text not OK");
    }

    [Test]
    public void TextFieldFlash()
    {
      ie.MainDocument.TextField("name").Flash();
    }

    [Test, ExpectedException(typeof(ElementReadOnlyException),"Element with Id:readonlytext is readonly")]
    public void TextFieldReadyOnlyException()
    {
      TextField textField = ie.MainDocument.TextField("readonlytext");
      textField.TypeText("This should go wrong");
    }

    [Test, ExpectedException(typeof(ElementDisabledException),"Element with Id:disabledtext is disabled")]
    public void TextFieldDisabledException()
    {
      TextField textField = ie.MainDocument.TextField("disabledtext");
      textField.TypeText("This should go wrong");
    }

    [Test]
    public void TextFields()
    {
      Assert.AreEqual(6, ie.MainDocument.TextFields.length, "Number of TextFields should by 4");

      // Collection items by index
      Form mainForm = ie.MainDocument.Form("FormHiddens");
      Assert.AreEqual(2, mainForm.TextFields.length, "Wrong number of textfields in collectionTestForm");
      Assert.AreEqual("first", mainForm.TextFields[0].Value);
      Assert.AreEqual("second", mainForm.TextFields[1].Value);

      Form form = ie.MainDocument.Form("Form");

      // Collection.length
      TextFieldCollection textfields = form.TextFields;
      Assert.AreEqual(1, textfields.length);

      // Collection iteration and comparing the result with Enumerator
      IEnumerable textfieldEnumerable = textfields;
      IEnumerator textfieldEnumerator = textfieldEnumerable.GetEnumerator();

      int count = 0;
      foreach (TextField textField in textfields)
      {
        textfieldEnumerator.MoveNext();
        object enumTextfield = textfieldEnumerator.Current;
        
        Assert.IsInstanceOfType(textField.GetType(), enumTextfield, "Types are not the same");
        Assert.AreEqual(textField.OuterHtml, ((TextField)enumTextfield).OuterHtml, "foreach en IEnumator don't act the same.");
        ++count;
      }
      
      Assert.IsFalse(textfieldEnumerator.MoveNext(), "Expected last item");

      Assert.AreEqual(1, count);
    }
  }
}