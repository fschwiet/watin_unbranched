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

namespace WatiN.Core.UnitTests.DialogHandlerTests
{
  using NUnit.Framework;
  using WatiN.Core.DialogHandlers;

  [TestFixture]
  public class SimpleJavaDialogHandlerTests : WatiNTest
  {
    [Test]
    public void AlertDialogSimpleJavaDialogHandler()
    {
      using (IE ie = new IE(TestEventsURI))
      {
        Assert.AreEqual(0, ie.DialogWatcher.Count, "DialogWatcher count should be zero");

        SimpleJavaDialogHandler dialogHandler = new SimpleJavaDialogHandler();

        Assert.IsFalse(dialogHandler.HasHandledDialog, "Alert Dialog should not be handled.");
        Assert.IsNull(dialogHandler.Message, "Message should be null");

        using (new UseDialogOnce(ie.DialogWatcher, dialogHandler))
        {
          ie.Button(Find.ByValue("Show alert dialog")).Click();

          Assert.IsTrue(dialogHandler.HasHandledDialog, "Alert Dialog should be handled.");
          Assert.AreEqual("This is an alert!", dialogHandler.Message, "Unexpected message");
        }
      }
    }

    [Test]
    public void AlertDialogSimpleJavaDialogHandler2()
    {
      using (IE ie = new IE(TestEventsURI))
      {
        SimpleJavaDialogHandler dialogHandler = new SimpleJavaDialogHandler();

        using (new UseDialogOnce(ie.DialogWatcher, dialogHandler))
        {
          ie.Button(Find.ByValue("Show alert dialog")).Click();

          Assert.AreEqual("This is an alert!", dialogHandler.Message, "Unexpected message");
        }
      }
    }

    [Test]
    public void ConfirmDialogSimpleJavaDialogHandlerCancel()
    {
      using (IE ie = new IE(TestEventsURI))
      {
        Assert.AreEqual(0, ie.DialogWatcher.Count, "DialogWatcher count should be zero");

        SimpleJavaDialogHandler dialogHandler = new SimpleJavaDialogHandler(true);
        using (new UseDialogOnce(ie.DialogWatcher, dialogHandler))
        {
          ie.Button(Find.ByValue("Show confirm dialog")).Click();

          Assert.IsTrue(dialogHandler.HasHandledDialog, "Confirm Dialog should be handled.");
          Assert.AreEqual("Do you want to do xyz?", dialogHandler.Message);
          Assert.AreEqual("Cancel", ie.TextField("ReportConfirmResult").Text, "Cancel button expected.");
        }
      }
    }

    [Test]
    public void IEUseOnceDialogHandler()
    {
      using (IE ie = new IE(TestEventsURI))
      {
        Assert.AreEqual(0, ie.DialogWatcher.Count, "DialogWatcher count should be zero");

        SimpleJavaDialogHandler dialogHandler = new SimpleJavaDialogHandler();

        using (new UseDialogOnce(ie.DialogWatcher, dialogHandler))
        {
          ie.Button(Find.ByValue("Show alert dialog")).Click();

          Assert.IsTrue(dialogHandler.HasHandledDialog, "Alert Dialog should be handled.");
          Assert.AreEqual("This is an alert!", dialogHandler.Message, "Unexpected message");
        }
      }
    }
  }
}