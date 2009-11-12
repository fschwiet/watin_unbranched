#region WatiN Copyright (C) 2006-2009 Jeroen van Menen

//Copyright 2006-2009 Jeroen van Menen
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
using WatiN.Core.Dialogs;
using WatiN.Core.UnitTests.TestUtils;

namespace WatiN.Core.UnitTests.DialogHandlerTests
{
	[TestFixture]
	public class ConfirmDialogHandlerTests : BaseWithBrowserTests
	{
		[Test]
		public void ConfirmDialogHandlerOK()
		{
            ExecuteTest(browser =>
                {
                    var confirmDialogHandler = browser.Expect<ConfirmDialog>();

                    browser.Button(Find.ByValue("Show confirm dialog")).ClickNoWait();

                    var confirm = confirmDialogHandler.Object;

                    var message = confirm.Message;
                    confirm.ClickOkButton();

                    browser.WaitForComplete();

                    Assert.AreEqual("Do you want to do xyz?", message, "Unexpected message");
                    Assert.AreEqual("OK", browser.TextField("ReportConfirmResult").Text, "OK button expected.");
                });
		}

		[Test]
		public void ConfirmDialogHandlerCancel()
		{
            ExecuteTest(browser =>
                {
                    var confirmDialogHandler = browser.Expect<ConfirmDialog>();

                    browser.Button(Find.ByValue("Show confirm dialog")).ClickNoWait();

                    var confirm = confirmDialogHandler.Object;

                    var message = confirm.Message;
                    confirm.ClickCancelButton();

                    browser.WaitForComplete();

                    Assert.AreEqual("Do you want to do xyz?", message, "Unexpected message");
                    Assert.AreEqual("Cancel", browser.TextField("ReportConfirmResult").Text, "Cancel button expected.");
                });
		}

		public override Uri TestPageUri
		{
			get { return TestEventsURI; }
		}
	}
}