using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using WatiN.Core.UnitTests.TestUtils;
using WatiN.Core.Dialogs;
using WatiN.Core.UtilityClasses;

namespace WatiN.Core.UnitTests
{
    [TestFixture]
    public class DialogWatcherTests : BaseWithBrowserTests
    {
        [Test]
        public void SetHandlerShouldCreateDialogHandler()
        {
            ExecuteTest(browser =>
                {
                    browser.SetHandler<ConfirmDialog>(d => d.ClickCancelButton());
                    int handlerCount = browser.GetHandlerCount(typeof(ConfirmDialog), true);

                    Assert.AreEqual(handlerCount, 1, "Only one handler expected");
                    browser.Button(Find.ByValue("Show confirm dialog")).ClickNoWait();

                    bool handled = TryFuncUntilTimeOut.Try<bool>(TimeSpan.FromSeconds(10), () => { return browser.HandlerExecutedCount<ConfirmDialog>() > 0; });

                    Assert.AreEqual("Cancel", browser.TextField("ReportConfirmResult").Text, "Cancel button expected.");
                    browser.ClearHandler<ConfirmDialog>();
                });
        }

        [Test]
        public void SetHandlerOverwritesExistingHandler()
        {
            ExecuteTest(browser =>
            {
                browser.SetHandler<ConfirmDialog>(d => d.ClickCancelButton());
                browser.ReplaceHandler<ConfirmDialog>(d => d.ClickOkButton());

                browser.Button(Find.ByValue("Show confirm dialog")).ClickNoWait();
                bool handled = TryFuncUntilTimeOut.Try<bool>(TimeSpan.FromSeconds(10), () => { return browser.HandlerExecutedCount<ConfirmDialog>() > 0; });

                Assert.AreEqual("OK", browser.TextField("ReportConfirmResult").Text, "OK button expected.");
                Assert.AreEqual(browser.GetHandlerCount(typeof(ConfirmDialog), false), 1, "Only one handler expected");

                browser.ClearHandler<ConfirmDialog>();
            });
        }

        [Test]
        public void ReplaceHandlerShouldCreateNewHandler()
        {
            ExecuteTest(browser =>
            {
                browser.SetHandler<ConfirmDialog>(d => d.ClickCancelButton());
                Action<ConfirmDialog> previousHandler = browser.ReplaceHandler<ConfirmDialog>(d => d.ClickOkButton());

                browser.Button(Find.ByValue("Show confirm dialog")).ClickNoWait();
                bool handled = TryFuncUntilTimeOut.Try<bool>(TimeSpan.FromSeconds(10), () => { return browser.HandlerExecutedCount<ConfirmDialog>() > 0; });

                Assert.AreEqual("OK", browser.TextField("ReportConfirmResult").Text, "OK button expected.");

                browser.ReplaceHandler<ConfirmDialog>(previousHandler);

                browser.Button(Find.ByValue("Show confirm dialog")).ClickNoWait();
                handled = TryFuncUntilTimeOut.Try<bool>(TimeSpan.FromSeconds(10), () => { return browser.HandlerExecutedCount<ConfirmDialog>() > 0; });

                Assert.AreEqual("Cancel", browser.TextField("ReportConfirmResult").Text, "Cancel button expected.");
                Assert.AreEqual(browser.GetHandlerCount(typeof(ConfirmDialog), false), 1, "Only one handler expected");
                browser.ClearHandler<ConfirmDialog>();
            });
        }

        [Test]
        public void HandlerHandlesMultipleDialogInstances()
        {
            ExecuteTest(browser =>
                {
                    browser.SetHandler<ConfirmDialog>(d => d.ClickCancelButton());
                    int handlerCount = browser.GetHandlerCount(typeof(ConfirmDialog), true);

                    for (int i = 0; i < 5; i++)
                    {
                        browser.Button(Find.ByValue("Show confirm dialog")).ClickNoWait();

                        bool handled = TryFuncUntilTimeOut.Try<bool>(TimeSpan.FromSeconds(10), () => { return browser.HandlerExecutedCount<ConfirmDialog>() > i; });
                        Assert.AreEqual("Cancel", browser.TextField("ReportConfirmResult").Text, "Cancel button expected.");
                        browser.TextField("ReportConfirmResult").Clear();
                    }
                    Assert.AreEqual(handlerCount, 1, "Only one handler expected");
                    browser.ClearHandler<ConfirmDialog>();
                });
        }

        [Test]
        public void ClearHandlerShouldRemoveHandler()
        {
            ExecuteTest(browser =>
            {
                browser.SetHandler<ConfirmDialog>(d => d.ClickCancelButton());
                browser.ClearHandler<ConfirmDialog>();

                Assert.AreEqual(browser.GetHandlerCount(typeof(ConfirmDialog), false), 0, "No handlers should exist.");
            });
        }

        [Test]
        public void ResetHandlerShouldReinstateHandler()
        {
            ExecuteTest(browser =>
            {
                browser.SetHandler<ConfirmDialog>(d => d.ClickCancelButton());
                Expectation<ConfirmDialog> expectation = browser.Expect<ConfirmDialog>();

                browser.Button(Find.ByValue("Show confirm dialog")).ClickNoWait();
                expectation.Object.ClickOkButton();

                Assert.AreEqual("OK", browser.TextField("ReportConfirmResult").Text, "OK button expected.");
                Assert.IsFalse(browser.HandlerIsEnabled<ConfirmDialog>(), "Handler not disabled by Expectation");
                Assert.AreEqual(browser.GetHandlerCount(typeof(ConfirmDialog), false), 1, "One handler should still exist.");
                Assert.AreEqual(browser.HandlerExecutedCount<ConfirmDialog>(), 0, "Handler executed when not expected.");
                browser.ResetHandler<ConfirmDialog>();

                browser.Button(Find.ByValue("Show confirm dialog")).ClickNoWait();

                bool handled = TryFuncUntilTimeOut.Try<bool>(TimeSpan.FromSeconds(10), () => { return browser.HandlerExecutedCount<ConfirmDialog>() > 0; });

                Assert.AreEqual("Cancel", browser.TextField("ReportConfirmResult").Text, "Cancel button expected.");
                Assert.IsTrue(browser.HandlerIsEnabled<ConfirmDialog>(), "Handler not enabled by ResetHandler");
                Assert.AreEqual(browser.GetHandlerCount(typeof(ConfirmDialog), true), 1, "Handler should still exist.");
                Assert.AreEqual(browser.HandlerExecutedCount<ConfirmDialog>(), 1, "Handler executed incorrect number of times.");
                browser.ClearHandler<ConfirmDialog>();
            });
        }

        [Test]
        public void ResetHandlerShouldRemovePendingExpectation()
        {
            ExecuteTest(browser =>
            {
                browser.SetHandler<ConfirmDialog>(d => d.ClickCancelButton());
                Expectation<ConfirmDialog> expectation = browser.Expect<ConfirmDialog>();
                browser.ResetHandler<ConfirmDialog>();

                browser.Button(Find.ByValue("Show confirm dialog")).ClickNoWait();
                bool handled = TryFuncUntilTimeOut.Try<bool>(TimeSpan.FromSeconds(10), () => { return browser.HandlerExecutedCount<ConfirmDialog>() > 0; });

                Assert.AreEqual("Cancel", browser.TextField("ReportConfirmResult").Text, "OK button expected.");
                Assert.IsFalse(expectation.IsSatisfied, "Expectation should be unsatisfied");
                Assert.IsFalse(browser.IsExpecting<ConfirmDialog>(), "Should no longer be expecting dialog.");
                browser.ClearHandler<ConfirmDialog>();
            });
        }

        [Test]
        public void ResetHandlerShouldRemovePendingExpectationWhenNoHandlerSet()
        {
            ExecuteTest(browser =>
            {
                Assert.AreEqual(browser.GetHandlerCount(typeof(ConfirmDialog), true), 0, "No handler should exist.");
                Expectation<ConfirmDialog> expectation = browser.Expect<ConfirmDialog>();
                Assert.IsTrue(browser.IsExpecting<ConfirmDialog>(), "Should be expecting dialog.");
                browser.ResetHandler<ConfirmDialog>();
                Assert.IsFalse(browser.IsExpecting<ConfirmDialog>(), "Should no longer be expecting dialog.");
                Assert.IsFalse(browser.HandlerExists<ConfirmDialog>(), "No handler should exist");
            });
        }

        [Test]
        public void ExpectationShouldTakePrecedenceOverHandler()
        {
            ExecuteTest(browser =>
            {
                browser.SetHandler<ConfirmDialog>(d => d.ClickCancelButton());
                Expectation<ConfirmDialog> expectation = browser.Expect<ConfirmDialog>();

                browser.Button(Find.ByValue("Show confirm dialog")).ClickNoWait();
                expectation.Object.ClickOkButton();

                Assert.AreEqual("OK", browser.TextField("ReportConfirmResult").Text, "OK button expected.");
                Assert.AreEqual(browser.GetHandlerCount(typeof(ConfirmDialog), false), 1, "One handler should still exist.");
                Assert.IsFalse(browser.HandlerIsEnabled<ConfirmDialog>(), "Handler not disabled by Expectation");
                Assert.AreEqual(browser.HandlerExecutedCount<ConfirmDialog>(), 0, "Handler executed when not expected.");
                browser.ClearHandler<ConfirmDialog>();
            });
        }

        [Test]
        public void ExistingExpectationShouldTakePrecedenceOverNewHandler()
        {
            ExecuteTest(browser =>
            {
                Expectation<ConfirmDialog> expectation = browser.Expect<ConfirmDialog>(TimeSpan.FromSeconds(10));
                browser.SetHandler<ConfirmDialog>(d => d.ClickCancelButton());
                Assert.AreEqual(browser.GetHandlerCount(typeof(ConfirmDialog), false), 1, "One handler should still exist.");
                Assert.IsFalse(browser.HandlerIsEnabled<ConfirmDialog>(), "Handler not disabled by Expectation");

                browser.Button(Find.ByValue("Show confirm dialog")).ClickNoWait();
                expectation.Object.ClickOkButton();

                Assert.AreEqual("OK", browser.TextField("ReportConfirmResult").Text, "OK button expected.");
                Assert.IsFalse(browser.IsExpecting<ConfirmDialog>(), "Expectation should no longer be in effect");
                Assert.AreEqual(browser.HandlerExecutedCount<ConfirmDialog>(), 0, "Handler executed when not expected.");
                browser.ClearHandler<ConfirmDialog>();
            });
        }

        [Test]
        public void DoDefaultActionShouldDismissDialog()
        {
            ExecuteTest(browser =>
            {
                browser.TextField("ReportConfirmResult").Clear();
                browser.SetHandler<ConfirmDialog>(d => d.DoDefaultAction());

                browser.Button(Find.ByValue("Show confirm dialog")).ClickNoWait();
                bool handled = TryFuncUntilTimeOut.Try<bool>(TimeSpan.FromSeconds(10), () => { return browser.HandlerExecutedCount<ConfirmDialog>() > 0; });

                Assert.AreEqual("Cancel", browser.TextField("ReportConfirmResult").Text, "Cancel button expected.");
                Assert.AreEqual(browser.GetHandlerCount(typeof(ConfirmDialog), false), 1, "One handler should still exist.");
                Assert.IsTrue(browser.HandlerIsEnabled<ConfirmDialog>(), "Handler should still be active");
                Assert.AreEqual(browser.HandlerExecutedCount<ConfirmDialog>(), 1, "Handler not executed when expected.");
                browser.ClearHandler<ConfirmDialog>();
            });
        }

        public override Uri TestPageUri
        {
            get { return TestEventsURI; }
        }
    }
}
