#region WatiN Copyright (C) 2006 Jeroen van Menen

// WatiN (Web Application Testing In dotNet)
// Copyright (C) 2006 Jeroen van Menen
//
// This library is free software; you can redistribute it and/or modify it under the terms of the GNU 
// Lesser General Public License as published by the Free Software Foundation; either version 2.1 of 
// the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without 
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU 
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along with this library; 
// if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 
// 02111-1307 USA 

#endregion Copyright

using System;

using NUnit.Framework;
using WatiN.Core;

namespace WatiN.UnitTests
{
  [TestFixture]
  public class FindElementBy : WatiNTest
  {
    [Test]
    public void FindByFor()
    {
      ForValue value = Find.ByFor("foridvalue");
      Assert.AreEqual("htmlfor", value.AttributeName, "Wrong attributename");
      Assert.AreEqual("foridvalue", value.Value, "Wrong value");
    }

    [Test]
    public void FindByID()
    {
      IdValue value = Find.ById("idvalue");
      Assert.AreEqual("id", value.AttributeName, "Wrong attributename");
      Assert.AreEqual("idvalue", value.Value, "Wrong value");
    }

    [Test]
    public void FindByName()
    {
      NameValue value = Find.ByName("namevalue");
      Assert.AreEqual("name", value.AttributeName, "Wrong attributename");
      Assert.AreEqual("namevalue", value.Value, "Wrong value");
    }

    [Test]
    public void FindByText()
    {
      TextValue value = Find.ByText("textvalue");
      Assert.AreEqual("text", value.AttributeName, "Wrong attributename");
      Assert.AreEqual("textvalue", value.Value, "Wrong value");
    }

    [Test]
    public void FindByUrl()
    {
      UrlValue value = Find.ByUrl("http://www.google.nl");
      Assert.AreEqual("href", value.AttributeName, "Wrong attributename");
      Assert.AreEqual("http://www.google.nl", value.Value, "Wrong value");
      Assert.IsTrue(value.Compare("http://www.google.nl/"), "Should match Google");
      Assert.IsFalse(value.Compare("http://www.microsoft.com"), "Shouldn't match Microsoft");
    }

    [Test, ExpectedException(typeof(UriFormatException))]
    public void FindByUrlInvalidParam()
    {
      Find.ByUrl("www.google.nl");
    }

    [Test, ExpectedException(typeof(UriFormatException))]
    public void FindByUrlInvalidCompare()
    {
      UrlValue value = Find.ByUrl("http://www.google.nl");
      value.Compare("google.com");
    }

    [Test]
    public void FindByTitle()
    {
      TitleValue value = Find.ByTitle("titlevalue");
      Assert.AreEqual("title", value.AttributeName, "Wrong attributename");
      Assert.AreEqual("titlevalue", value.Value, "Wrong value");
      
      Assert.IsTrue(value.Compare("titlevalue"), "Compare should match");
      
      value = Find.ByTitle("tit");
      Assert.IsTrue(value.Compare("titlevalue"), "Compare should partial match tit");
      
      value = Find.ByTitle("lev");
      Assert.IsTrue(value.Compare("titlevalue"), "Compare should partial match lev");
      
      value = Find.ByTitle("alue");
      Assert.IsTrue(value.Compare("titlevalue"), "Compare should partial match alue");

      value = Find.ByTitle("titlevalue");
      Assert.IsFalse(value.Compare("title"), "Compare should not match title");
    }

    [Test]
    public void FindByValue()
    {
      ValueValue value = Find.ByValue("valuevalue");
      Assert.AreEqual("value", value.AttributeName, "Wrong attributename");
      Assert.AreEqual("valuevalue", value.Value, "Wrong value");
    }

    [Test, ExpectedException(typeof(ArgumentNullException))]
    public void NewElementAttributeValueWithNullAttribute()
    {
      Find.ByCustom(null,"idvalue");
    }

    [Test, ExpectedException(typeof(ArgumentNullException))]
    public void NewElementAttributeValueWithNullValue()
    {
      Find.ByCustom("id",null);
    }

    [Test, ExpectedException(typeof(ArgumentNullException))]
    public void NewElementAttributeValueWithNulls()
    {
      Find.ByCustom(null,null);
    }

    [Test, ExpectedException(typeof(ArgumentNullException))]
    public void NewElementAttributeValueWithEmptyAttribute()
    {
      Find.ByCustom(string.Empty,"idvalue");
    }

    [Test, ExpectedException(typeof(ArgumentNullException))]
    public void NewElementAttributeValueWithEmptyValue()
    {
      Find.ByCustom("id",string.Empty);
    }

    [Test, ExpectedException(typeof(ArgumentNullException))]
    public void NewElementAttributeValueWithEmpties()
    {
      Find.ByCustom(string.Empty,string.Empty);
    }
    
    [Test]
    public void FindByCustom()
    {
      AttributeValue value = Find.ByCustom("id","idvalue");
      Assert.AreEqual("id", value.AttributeName, "Wrong attributename");
      Assert.AreEqual("idvalue", value.Value, "Wrong value");
      
      Assert.IsTrue(value.Compare("idvalue"), "Compare should match");
      Assert.IsFalse(value.Compare("id"), "Compare should not partial match id");
      Assert.IsFalse(value.Compare("val"), "Compare should not partial match val");
      Assert.IsFalse(value.Compare("value"), "Compare should not partial match value");
    }

    [Test]
    public void ElementUrlPartialValue()
    {
      ElementUrlPartialValue value = new ElementUrlPartialValue("google.com");
      Assert.AreEqual("href", value.AttributeName, "Wrong attributename");
      Assert.AreEqual("google.com", value.Value, "Wrong value");
      
      Assert.IsTrue(value.Compare("google.com"), "Compare should match");
      
      value = new ElementUrlPartialValue("google.com");
      Assert.IsTrue(value.Compare("www.google.com"), "Compare should partial match tit");
      
      value = new ElementUrlPartialValue("google.com");
      Assert.IsFalse(value.Compare("www.microsoft.com"), "Compare should not match title");

      using (IE ie = new IE(MainURI.ToString()))
      {
        ie.MainDocument.Link("testlinkid").Click();
        IE ieGoogle = IE.AttachToIE(new ElementUrlPartialValue("google.com"));
        Assert.AreEqual(GoogleURI.ToString(), ieGoogle.Url);
        ieGoogle.Close();
      }
    }
  }

  public class ElementUrlPartialValue : UrlValue
  {
    private string url = null;

    public ElementUrlPartialValue(string url) : base("http://www.fakeurl.com")
    {
      this.url = url;  
    }

    public override string Value
    {
       get { return url; }
    }

    public override bool Compare(string value)
    {
      bool containedInValue = value.ToLower().IndexOf(Value.ToLower()) >= 0;

      if (!IsNullOrEmpty(value) && containedInValue)
      {
        return true;
      }
      return false;
    }
  }
}