#region WatiN Copyright (C) 2006-2008 Jeroen van Menen

//Copyright 2006-2008 Jeroen van Menen
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
using System.Collections.Generic;
using NUnit.Framework;
using WatiN.Core.Exceptions;
using WatiN.Core.Logging;
using WatiN.Core.UnitTests.Interfaces;

namespace WatiN.Core.UnitTests
{
	public abstract class BaseWithBrowserTests : BaseWatiNTest
	{
        /// <summary>
        /// The test method to execute.
        /// </summary>
        public delegate void BrowserTest(Browser browser);

	    private readonly IBrowserTestManager ieManager = new IEBrowserTestManager();
	    private readonly IBrowserTestManager ffManager = new FFBrowserTestManager();

	    public readonly List<IBrowserTestManager> BrowsersToTestWith = new List<IBrowserTestManager>();

        // TODO: remove this property in time
        public IE Ie
        {
            get
            {
                if (InsideExecuteTest) throw new WatiNException("Specific test for IE detected inside call to ExecuteTest");
                return (IE)ieManager.GetBrowser(TestEventsURI);
            }
        }

        // TODO: remove this property in time
        public FireFox Firefox
        {
            get
            {
                if (InsideExecuteTest) throw new WatiNException("Specific test for Firefox detected inside call to ExecuteTest");
                return (FireFox)ffManager.GetBrowser(TestEventsURI);
            }
        }

		[TestFixtureSetUp]
		public override void FixtureSetup()
		{
            base.FixtureSetup();

            BrowsersToTestWith.Add(ieManager);
            BrowsersToTestWith.Add(ffManager);

            Logger.LogWriter = new ConsoleLogWriter();
		}

	    [TestFixtureTearDown]
		public override void FixtureTearDown()
	    {
            BrowsersToTestWith.ForEach(manager => manager.CloseBrowser());
	        base.FixtureTearDown();
	    }

	    [SetUp]
		public virtual void TestSetUp()
	    {
	        Settings.Reset();
            BrowsersToTestWith.ForEach(browser => GoToTestPage(browser.GetBrowser(TestPageUri)));
	    }

	    private void GoToTestPage(Browser browser)
	    {
	        if ( browser != null && !browser.Uri.Equals(TestPageUri))
	        {
	            browser.GoTo(TestPageUri);
	        }
	    }

	    public abstract Uri TestPageUri { get; }

        /// <summary>
        /// Executes the test using both FireFox and Internet Explorer.
        /// </summary>
        /// <param name="testMethod">The test method.</param>
        public void ExecuteTest(BrowserTest testMethod)
        {
            BrowsersToTestWith.ForEach(browser => ExecuteTest(testMethod, browser.GetBrowser(TestPageUri)));
        }

        private static void ExecuteTest(BrowserTest testMethod, Browser browser)
        {
            InsideExecuteTest = true;
            try
            {
                testMethod.Invoke(browser);
            }
            catch (WatiNException e)
            {
                Logger.LogAction(browser.GetType() + " exception: " + e.Message);
                throw;
            }
            catch(Exception e)
            {
                throw new WatiNException(browser.GetType() + " exception", e);
            }
            finally
            {
                InsideExecuteTest = false;
            }
        }

	    private static bool InsideExecuteTest { get; set; }
	}
}