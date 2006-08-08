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
using WatiN.Core.Exceptions;
using WatiN.Core.Logging;

namespace WatiN.UnitTests
{
  [TestFixture]
  public class IEAndMainDocument : WatiNTest
  {
    [TestFixtureSetUp]
    public void Setup()
    {
      Logger.LogWriter = new DebugLogWriter();
    }

    [Test]
    public void NUnitGUI()
    {
      using (new IE(MainURI.ToString(), true))
      {
      }
    }

    [Test]
    public void Google()
    {
      // Instantiate a new DebugLogger to output "user" events to
      // the debug window in VS
      Logger.LogWriter = new DebugLogWriter();

      using (IE ie = new IE(GoogleURI.ToString(), true))
      {
        ie.TextField(Find.ByName("q")).TypeText("WatiN");
        ie.Button(Find.ByName("btnG")).Click();

        Assert.IsTrue(ie.ContainsText("WatiN"));
      }
    }
    
    [Test]
    public void GoogleFormSubmit()
    {
      // Instantiate a new DebugLogger to output "user" events to
      // the debug window in VS
      Logger.LogWriter = new DebugLogWriter();

      using (IE ie = new IE(GoogleURI.ToString(), true))
      {
        ie.TextField(Find.ByName("q")).TypeText("WatiN");
        ie.Form(Find.ByName("f")).Submit();

        Assert.IsTrue(ie.ContainsText("WatiN"));
      }
    }

    [Test]
    public void ModelessDialog()
    {
      using (IE ie = new IE(MainURI.ToString(), true))
      {
        ie.Button("popupid").Click();
        Document dialog = ie.HtmlDialogs[0];

        Assert.AreEqual("47", dialog.TextField("dims").Value);
      }
    }

    [Test]
    public void ContainsText()
    {
      using (IE ie = new IE(MainURI.ToString(), true))
      {
        Assert.IsTrue(ie.ContainsText("Contains text in DIV"), "Text not found");
        Assert.IsFalse(ie.ContainsText("abcde"), "Text incorrectly found");
      }
    }
    [Test]
    public void Alert()
    {
      using (IE ie = new IE(MainURI.ToString(), true))
      {
        ie.Button("helloid").Click();

        // getting alert text
        Assert.AreEqual("hello", ie.PopAlert());

        // expected alert was missing
        try
        {
          ie.PopAlert();
          Assert.Fail("Expected MissingAlertException");
        }
        catch (MissingAlertException)
        {
          // expected; ignore
        }
      }
    }

    [Test]
    public void URL()
    {
      using (IE ie = new IE(MainURI.ToString(), true))
      {
        Assert.AreEqual(MainURI,new Uri(ie.Url));
      }
    }

    [Test]
    public void GoToUrl()
    {
      using (IE ie = new IE())
      {
        ie.GoTo(MainURI.ToString());
        Assert.AreEqual(MainURI, new Uri(ie.Url));
      }
    }
    
    [Test]
    public void GoToUri()
    {
      using (IE ie = new IE())
      {
        ie.GoTo(MainURI);
        Assert.AreEqual(MainURI, new Uri(ie.Url));
      }
    }

    [Test]
    public void BackAndForward()
    {
      using (IE ie = new IE())
      {
        ie.GoTo(MainURI);
        Assert.AreEqual(MainURI, new Uri(ie.Url));
        
        ie.Link(Find.ByUrl(IndexURI.ToString())).Click();
        Assert.AreEqual(IndexURI, new Uri(ie.Url));

        ie.Back();
        Assert.AreEqual(MainURI, new Uri(ie.Url));

        ie.Forward();
        Assert.AreEqual(IndexURI, new Uri(ie.Url));
      }      
    }

    /// <summary>
    /// Attaches to IE with a zero timeout interval. Allthough the timeout
    /// interval is zero the existing IE instance should be found.
    /// </summary>
    [Test]
    public void AttachToIEWithZeroTimeout()
    {
      // Create a new IE instance so we can find it.
      using(new IE(MainURI.ToString()))
      {
        DateTime startTime = DateTime.Now;
        IE.AttachToIE(new UrlValue(MainURI), 0);

        // Should return (within 1 second).
        Assert.Greater(1, DateTime.Now.Subtract(startTime).TotalSeconds);       
      }
    }

    [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void AttachToIEWithNegativeTimeoutNotAllowed()
    {
      IE.AttachToIE(Find.ByTitle("Bogs title"), -1);
    }

    [Test]
    public void AttachToIEByParialTitle()
    {
      using (IE ie = new IE(MainURI.ToString()))
      {
        Assert.IsFalse(IsGoogleIEWindowOpen(), "An Internet Explorer with 'gOo' in it's title allready exists. AttachToIEByParialTitle can't be correctly tested. Please close the window.");
        
        ie.Link("testlinkid").Click();
        using (IE ieGoogle = IE.AttachToIE(Find.ByTitle("gOo")))
        {
          Assert.AreEqual(GoogleURI.ToString(), ieGoogle.Url);
        }
        
        Assert.IsFalse(IsGoogleIEWindowOpen(), "The Internet Explorer with 'gOo' in it's title should be closed.");
      }
    }

    [Test]
    public void AttachToIEByURL()
    {
      
      using (IE ie = new IE(MainURI.ToString()))
      {
        Assert.IsFalse(IsGoogleIEWindowOpen(), "An Internet Explorer with url " + GoogleURI.ToString() + " is allready open. AttachToIEByURL can't be correctly tested. Please close the window.");
        
        ie.Link("testlinkid").Click();
        using (IE ieGoogle = IE.AttachToIE(Find.ByUrl(GoogleURI.ToString())))
        {
          Assert.AreEqual(GoogleURI.ToString(), ieGoogle.Url);
        }
        
        Assert.IsFalse(IsGoogleIEWindowOpen(), "The Internet Explorer with 'gOo' in it's title should be closed.");
      }
    }

    [Test]
    public void IENotFoundException()
    {
      DateTime startTime = DateTime.Now;
      const int timeoutTime = 5;
      const string ieTitle = "Non Excisting IE Title";
      const string expectedMessage = "Could not find an IE window by title with value '" + ieTitle + "'. (Search expired after '5' seconds)";
      
      try
      {
        // Time out after timeoutTime seconds
        startTime = DateTime.Now;
        IE.AttachToIE(Find.ByTitle(ieTitle),timeoutTime);
        Assert.Fail(string.Format("Internet Explorer with title '{0}' should not be found", ieTitle));
      }
      catch (Exception e)
      {
        Assert.IsInstanceOfType(typeof(IENotFoundException), e);
        // add 1 second to give it some slack.
        Assert.Greater(timeoutTime + 1, DateTime.Now.Subtract(startTime).TotalSeconds);
        Assert.AreEqual(expectedMessage, e.Message, "Unexpected exception message");
      }
    }

    [Test]
    public void HTMLDialog()
    {
      IE ie = new IE(MainURI.ToString());

      ie.Button("modalid").ClickNoWait();

      HtmlDialog htmlDialog = ie.HtmlDialog(Find.ByTitle("PopUpTest"));
  
      Assert.IsNotNull(htmlDialog, "Dialog niet aangetroffen");
      Assert.AreEqual("PopUpTest", htmlDialog.Title, "Unexpected title");
  
      htmlDialog.TextField("name").TypeText("Textfield in HTMLDialog");
      htmlDialog.Button("hello").Click();

      htmlDialog.Close();

      ie.WaitForComplete();
      ie.Close();
    }

    [Test]
    public void HTMLDialogFindByTitle()
    {
      IE ie = new IE(MainURI.ToString());

      ie.Button("modalid").ClickNoWait();

      HtmlDialog htmlDialog = ie.HtmlDialog(Find.ByTitle("PopUpTest"));
  
      Assert.IsNotNull(htmlDialog, "Dialog niet aangetroffen");
      Assert.AreEqual("PopUpTest", htmlDialog.Title, "Unexpected title");
  
      htmlDialog.Close();

      ie.WaitForComplete();
      ie.Close();
    }

    [Test]
    public void HTMLDialogFindByUrl()
    {
      IE ie = new IE(MainURI.ToString());

      ie.Button("modalid").ClickNoWait();

      HtmlDialog htmlDialog = ie.HtmlDialog(Find.ByUrl(PopUpURI.ToString()));
  
      Assert.IsNotNull(htmlDialog, "Dialog niet aangetroffen");
      Assert.AreEqual("PopUpTest", htmlDialog.Title, "Unexpected title");
  
      htmlDialog.Close();

      ie.WaitForComplete();
      ie.Close();
    }

    [Test]
    public void HTMLDialogNotFoundException()
    {
      using (IE ie = new IE(MainURI.ToString()))
      {
        DateTime startTime = DateTime.Now;
        const int timeoutTime = 5;
        string expectedMessage = "Could not find a HTMLDialog by title with value 'PopUpTest'. (Search expired after '5' seconds)";

        try
        {
          // Time out after timeoutTime seconds
          startTime = DateTime.Now;
          ie.HtmlDialog(Find.ByTitle("PopUpTest"), timeoutTime);
          Assert.Fail("PopUpTest should not be found");
        }
        catch (Exception e)
        {
          Assert.IsInstanceOfType(typeof(HtmlDialogNotFoundException), e);
          // add 1 second to give it some slack.
          Assert.Greater(timeoutTime + 1, DateTime.Now.Subtract(startTime).TotalSeconds);
          Assert.AreEqual(expectedMessage, e.Message, "Unexpected exception message");
        }
      }
    }

    [Test]
    public void NewUri()
    {
      Uri uri = new Uri("about:blank");
      Assert.AreEqual("about:blank", uri.ToString());
    }

    [Test]
    public void FireKeyDownEventOnElementWithNoId()
    {
      using (IE ie = new IE(TestEventsURI.ToString()))
      {
        TextField report = ie.TextField("Report");
        Assert.IsNull(report.Text, "Report not empty");
        
        Button button = ie.Button(Find.ByValue("Button"));
        Assert.IsNull(button.Id, "Button id not null before click event");
        
        button.KeyDown();
        
        Assert.IsNotNull(report.Text, "No keydown event fired (report is empty )");
        StringAssert.StartsWith("button.id = ", report.Text, "Report should start with 'button.id = '");
        Assert.Greater(report.Text.Length, "button.id = ".Length, "No assigned id report");
        
        Assert.IsNull(button.Id, "Button id not null after click event");
      }
    }
    
    private static bool IsGoogleIEWindowOpen()
    {
      try
      {
        IE.AttachToIE(Find.ByTitle("gOo"), 5);
      }
      catch (IENotFoundException)
      {
        return false;
      }
      return true;
    }
  }
}