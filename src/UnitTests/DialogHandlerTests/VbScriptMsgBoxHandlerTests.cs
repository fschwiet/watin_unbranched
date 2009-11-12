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
using NUnit.Framework.SyntaxHelpers;
using WatiN.Core.UnitTests.TestUtils;
using WatiN.Core.Dialogs;
using WatiN.Core.UtilityClasses;

namespace WatiN.Core.UnitTests.DialogHandlerTests
{
	[TestFixture]
	public class VbScriptMsgBoxDialogHandlerTests : BaseWithBrowserTests
	{
		[Test]
		public void TestOkOnly()
		{
			const int buttons = 0;
            string result = GetResultFromMsgBox<VBScriptOkOnlyDialog>(buttons, (d) => { d.ClickOkButton(); });
			Assert.That(result, Is.EqualTo("1"), "Unexpected return value from message box");
		}

		[Test]
		public void TestOkCancel()
		{
			const int buttons = 1;
            string result = GetResultFromMsgBox<VBScriptOkCancelDialog>(buttons, (d) => { d.ClickOkButton(); });
            Assert.That(result, Is.EqualTo("1"), "Unexpected return value from message box");

            result = GetResultFromMsgBox<VBScriptOkCancelDialog>(buttons, (d) => { d.ClickCancelButton(); });
            Assert.That(result, Is.EqualTo("2"), "Unexpected return value from message box");
		}

		[Test]
		public void TestAbortRetryIgnore()
		{
			const int buttons = 2;
            string result = GetResultFromMsgBox<VBScriptAbortRetryIgnoreDialog>(buttons, (d) => { d.ClickAbortButton(); });
            Assert.That(result, Is.EqualTo("3"), "Unexpected return value from message box");

            result = GetResultFromMsgBox<VBScriptAbortRetryIgnoreDialog>(buttons, (d) => { d.ClickRetryButton(); });
			Assert.That(result, Is.EqualTo("4"), "Unexpected return value from message box");

            result = GetResultFromMsgBox<VBScriptAbortRetryIgnoreDialog>(buttons, (d) => { d.ClickIgnoreButton(); });
			Assert.That(result, Is.EqualTo("5"), "Unexpected return value from message box");
		}

		[Test]
		public void TestYesNoCancel ()
		{
			const int buttons = 3;
            string result = GetResultFromMsgBox<VBScriptYesNoCancelDialog>(buttons, (d) => { d.ClickYesButton(); });
            Assert.That(result, Is.EqualTo("6"), "Unexpected return value from message box");

            result = GetResultFromMsgBox<VBScriptYesNoCancelDialog>(buttons, (d) => { d.ClickNoButton(); });
            Assert.That(result, Is.EqualTo("7"), "Unexpected return value from message box");

            result = GetResultFromMsgBox<VBScriptYesNoCancelDialog>(buttons, (d) => { d.ClickCancelButton(); });
            Assert.That(result, Is.EqualTo("2"), "Unexpected return value from message box");
		}
		[Test]
		public void TestYesNo ()
		{
			const int buttons = 4;
            string result = GetResultFromMsgBox<VBScriptYesNoDialog>(buttons, (d) => { d.ClickYesButton(); });
            Assert.That(result, Is.EqualTo("6"), "Unexpected return value from message box");

            result = GetResultFromMsgBox<VBScriptYesNoDialog>(buttons, (d) => { d.ClickNoButton(); });
            Assert.That(result, Is.EqualTo("7"), "Unexpected return value from message box");
        }

		[Test]
		public void TestRetryCancel ()
		{
			const int buttons = 5;
            string result = GetResultFromMsgBox<VBScriptRetryCancelDialog>(buttons, (d) => { d.ClickRetryButton(); });
			Assert.That(result, Is.EqualTo("4"), "Unexpected return value from message box");

            result = GetResultFromMsgBox<VBScriptRetryCancelDialog>(buttons, (d) => { d.ClickCancelButton(); });
            Assert.That(result, Is.EqualTo("2"), "Unexpected return value from message box");
        }

		private string GetResultFromMsgBox<T>(int buttons, Action<T> dialogDismissalDelegate) where T : VBScriptMsgBoxDialog
		{
            //IE only test. Do not attempt with FireFox (does not understand VBScript).
			Ie.TextField("msgBoxButtons").TypeText(buttons.ToString());
            Ie.SetHandler<T>(dialogDismissalDelegate);

            Ie.Button("vbScriptMsgBox").ClickNoWait();
            bool handled = TryFuncUntilTimeOut.Try<bool>(TimeSpan.FromSeconds(10), () => { return Ie.HandlerExecutedCount<T>() > 0; });

            Assert.That(handled, "Should have handled dialog");
            Ie.ClearHandler<T>();
			return Ie.TextField("msgBoxReturnValue").Value;
		}

	    public override Uri TestPageUri
		{
			get { return TestEventsURI; }
		}
	}
}