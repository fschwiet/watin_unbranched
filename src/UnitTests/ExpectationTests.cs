using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using WatiN.Core.UnitTests.TestUtils;
using WatiN.Core.Dialogs;

namespace WatiN.Core.UnitTests
{
    [TestFixture]
    public class ExpectationTests : BaseWithBrowserTests
    {
        [Test]
        public void ExpectationShouldTimeoutIfNotMet()
        {
            ExecuteTest(browser =>
                {
                    Expectation<ConfirmDialog> expect = browser.Expect<ConfirmDialog>(TimeSpan.FromSeconds(1));
                    expect.WaitUntilSatisfied();
                    Assert.IsTrue(expect.TimeoutReached, "Timeout should have been reached");
                    Assert.IsFalse(expect.IsSatisfied, "Expecation should not be satisified");
                    browser.ResetHandler<ConfirmDialog>();
                });
        }

        [Test]
        public void ExpectationCanBeResetAfterTimeout()
        {
            ExecuteTest(browser =>
                {
                    Expectation<ConfirmDialog> expect = browser.Expect<ConfirmDialog>(TimeSpan.FromSeconds(2));
                    expect.WaitUntilSatisfied();
                    Assert.IsTrue(expect.TimeoutReached, "Timeout should have been reached");
                    Assert.IsFalse(expect.IsSatisfied, "Expecation should not be satisified");

                    expect.Reset();
                    browser.Button(Find.ByValue("Show confirm dialog")).ClickNoWait();
                    expect.Object.ClickOkButton();

                    Assert.AreEqual("OK", browser.TextField("ReportConfirmResult").Text, "OK button expected.");
                    Assert.IsFalse(expect.TimeoutReached, "Timeout should not have been reached");
                    Assert.IsTrue(expect.IsSatisfied, "Expecation should be satisified");
                    Assert.IsFalse(browser.IsExpecting<ConfirmDialog>(), "Expectation should no longer be in effect");
                });
        }

        public override Uri TestPageUri
        {
            get { return TestEventsURI; }
        }
    }
}
