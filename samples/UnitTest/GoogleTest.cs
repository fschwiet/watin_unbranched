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

using NUnit.Framework;
using WatiN.Core;
using WatiN.Core.Interfaces;

namespace WatiN.Samples.UnitTest
{
    /// <summary>
    /// This class demonstrates how to use the WatiN test framework to perform some basic browser automation tests.
    /// 
    /// <see cref="SearchForWatiNOnGoogleUsingBaseTest"/> shows a generic way to execute a test on both browsers. 
    /// The <see cref="BaseTest.ExecuteTest"/>  method takes a <see cref="BaseTest.BrowserTest">delegate</see>, which
    /// it will call once per browser type.
    /// 
    /// <see cref="SearchForWatiNOnGoogleVerbose"/> shows a less elegant, but simpler, way of executing a test using
    /// multiple browsers.
    /// </summary>
    [TestFixture]
    public class GoogleTest : BaseTest
    {
        /// <summary>
        /// Searches for WatiN on google using both Internet Explorer and Firefox.
        /// </summary>
        [Test]
        public void SearchForWatiNOnGoogleUsingBaseTest()
        {
            // Call ExecuteTest in the base class, the base class handles creating and disposing
            // of browser instances, and will call the passed in delegate 
            // SearchForWatiNOnGoogleUsingBaseTest(IBrowser browser) once for each type of 
            // browser supported by WatiN (Internet Explorer and Firefox at the moment).
            base.ExecuteTest(SearchForWatiNOnGoogleUsingBaseTest);
        }

        /// <summary>
        /// Searches for WatiN on google using the passed in <paramref name="browser"/>.
        /// </summary>
        /// <param name="browser">The browser.</param>
        private void SearchForWatiNOnGoogleUsingBaseTest(IBrowser browser)
        {
            GoTo("http://www.google.com", browser);                
            browser.TextField(Find.ByName("q")).Value = "WatiN";
            browser.Button(Find.ByName("btnG")).Click();
            Assert.IsTrue(browser.ContainsText("WatiN"));            
        }

        [Test]
        public void SearchForWatiNOnGoogleVerbose()
        {
            using (IBrowser ie = BrowserFactory.Create(BrowserType.InternetExplorer))
            {
                ie.GoTo("http://www.google.com");
                ie.TextField(Find.ByName("q")).Value = "WatiN";
                ie.Button(Find.ByName("btnG")).Click();
                Assert.IsTrue(ie.ContainsText("WatiN"));
            }

            using (IBrowser firefox = BrowserFactory.Create(BrowserType.FireFox))
            {
                firefox.GoTo("http://www.google.com");
                firefox.TextField(Find.ByName("q")).Value = "WatiN";
                firefox.Button(Find.ByName("btnG")).Click();
                Assert.IsTrue(firefox.ContainsText("WatiN"));
            }
        }
    }
}