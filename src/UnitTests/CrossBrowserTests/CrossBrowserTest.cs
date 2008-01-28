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
        private IBrowser firefox = null;
        private IBrowser ie = null;

        /// <summary>
        /// The test method to execute.
        /// </summary>
        protected delegate void BrowserTest(IBrowser browser);

        protected IBrowser Firefox
        {
            get
            {
                if (this.firefox == null)
                {
                    this.firefox = GetBrowserInstance(null, BrowserType.FireFox, true);
                }

                return firefox;
            }
            set { firefox = value; }
        }

        protected IBrowser Ie
        {
            get
            {
                if (ie == null)
                {
                    ie = this.GetBrowserInstance(null, BrowserType.InternetExplorer, true);
                }

                return ie;
            }

            set { ie = value; }
        }

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            Logger.LogWriter = new DebugLogWriter();
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
            if (Firefox != null)
            {
                Firefox.Dispose();
            }

            if (Ie != null)
            {
                Ie.Dispose();
            }
        }

        /// <summary>
        /// Executes the test using both FireFox and Internet Explorer.
        /// </summary>
        /// <param name="testMethod">The test method.</param>
        protected void ExecuteTest(BrowserTest testMethod)
        {
            try
            {
                testMethod.Invoke(this.Firefox);
            }
            catch (Exception e)
            {
                throw new WatiN.Core.Exceptions.WatiNException("firefox exception", e);
            }

            try
            {
                testMethod.Invoke(Ie);
            }
            catch (Exception e)
            {
                throw new WatiN.Core.Exceptions.WatiNException("ie exception", e);
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

        /// <summary>
        /// Creates a new instance or returns an existing instance of a browser
        /// </summary>
        /// <param name="browser">The cached browser instance or null</param>
        /// <param name="browserType">The browser type</param>
        /// <param name="newBrowserInstance">If true, returns a new browser instance, otherwise the cached browser is returned</param>
        /// <returns>A browser instance</returns>
        protected IBrowser GetBrowserInstance(IBrowser browser, BrowserType browserType, bool newBrowserInstance)
        {
            if (browser == null || newBrowserInstance)
            {
                if (browser != null)
                {
                    browser.Dispose();
                }

                return BrowserFactory.Create(browserType);
            }

            return browser;
        }

        /// <summary>
        /// Navigates to the specified <paramref name="url"/> if the <paramref name="browser"/> is not currently
        /// on that url.
        /// </summary>
        /// <param name="url">Url to navigate to.</param>
        /// <param name="browser">browser to navigate with.</param>
        protected static void GoTo(Uri url, IBrowser browser)
        {
            GoTo(url.ToString(), browser);
        }

        /// <summary>
        /// Navigates to the specified <paramref name="url"/> if the <paramref name="browser"/> is not currently
        /// on that url.
        /// </summary>
        /// <param name="url">Url to navigate to.</param>
        /// <param name="browser">browser to navigate with.</param>
        protected static void GoTo(string url, IBrowser browser)
        {
            string currentUrl = browser.Url;

            if (browser.BrowserType == BrowserType.InternetExplorer && currentUrl.StartsWith("file://"))
            {
                currentUrl = "file:///" + currentUrl.Substring(7).Replace('\\', '/');
            }

            if (!currentUrl.Equals(url, StringComparison.OrdinalIgnoreCase))
            {
                Logger.LogAction("Navigating to {0}", url);
                browser.GoTo(url);
            }
        }
    }
}
