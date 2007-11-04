using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using WatiN.Core.Interfaces;
using WatiN.Core.Logging;

namespace WatiN.Core.UnitTests.CrossBrowserTests
{
    [TestFixture]
    public abstract class CrossBrowserTest : WatiNTest
    {
        /// <summary>
        /// The test method to execute.
        /// </summary>
        protected delegate void BrowserTest(IBrowser browser);

        [TestFixtureSetUp]
        public void Setup()
        {
            Logger.LogWriter = new DebugLogWriter();
        }

        /// <summary>
        /// Executes the test using both FireFox and Internet Explorer.
        /// </summary>
        /// <param name="testMethod">The test method.</param>
        protected static void ExecuteTest(BrowserTest testMethod)
        {
            using (IBrowser browser = BrowserFactory.Create(BrowserType.FireFox))
            {
                testMethod.Invoke(browser);
            }

            using (IBrowser browser = BrowserFactory.Create(BrowserType.InternetExplorer))
            {
                testMethod.Invoke(browser);
            }
        }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="browser">The browser.</param>
        /// <returns>The error message with the browser type appended to the end.</returns>
        protected static string GetErrorMessage(string message, IBrowser browser)
        {
            return string.Format("{0} . For browser type: {1}", message, browser.BrowserType);
        }
    }
}
