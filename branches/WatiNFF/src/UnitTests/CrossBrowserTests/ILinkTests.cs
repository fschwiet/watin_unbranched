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

using NUnit.Framework;
using WatiN.Core.Interfaces;

namespace WatiN.Core.UnitTests.CrossBrowserTests
{
    /// <summary>
    /// Tests the behaviour specific to the <see cref="ILink"/> interface.
    /// </summary>
    public class ILinkTests : WatiNCrossBrowserTest
    {
        /// <summary>
        /// Tests the <see cref="ILink.Url"/> property.
        /// </summary>
        [Test]
        public void Url()
        {
            ExecuteTest(UrlTest);
        }

        private static void UrlTest(IBrowser browser)
        {
            browser.GoTo(MainURI);
            ILink link = browser.Link("testlinkid");
            Assert.IsTrue(link.Url.StartsWith("http://watin.sourceforge.net"), GetErrorMessage("Incorrect url value found.", browser));
        }
    }
}