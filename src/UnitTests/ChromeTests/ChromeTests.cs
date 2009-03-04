﻿// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="ChromeTests.cs">
//   Copyright 2006-2009 Jeroen van Menen
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
// </copyright>
// <summary>
//   Tests behaviour specific to the Chrome class.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace WatiN.Core.UnitTests.ChromeTests
{
    using System.Web;

    using Logging;

    using NUnit.Framework;
    using NUnit.Framework.SyntaxHelpers;

    /// <summary>
    /// Tests behaviour specific to the <see cref="Chrome"/> class.
    /// </summary>
    [TestFixture]
    public class ChromeTests
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChromeTests"/> class.
        /// </summary>
        public ChromeTests()
        {
            Logger.LogWriter = new DebugLogWriter();
        }

        [Test]
        public void CheckFireFoxIsInheritingProperTypes()
        {
            using (var chrome = new Chrome())
            {
                Assert.IsInstanceOfType(typeof(Chrome), chrome, "Should be a Chrome instance");
                Assert.IsInstanceOfType(typeof(Browser), chrome, "Should be a Browser instance");
                Assert.IsInstanceOfType(typeof(DomContainer), chrome, "Should be a DomContainer instance");
            }
        }

        [Test, Category("InternetConnectionNeeded")]
        public void GoogleSearchWithEncodedQueryStringInConstructor()
        {
            var url = string.Format("http://www.google.com/search?q={0}", HttpUtility.UrlEncode("a+b"));

            using (var chrome = new Chrome(url))
            {
                Assert.That(chrome.TextField(Find.ByName("q")).Value, Is.EqualTo("a+b"));
            }
        }

        [Test]
        public void NewChromeWithoutUrlShouldStartAtAboutBlank()
        {
            using (var chrome = new Chrome())
            {
                Assert.AreEqual("about:blank", chrome.Url);
            }
        }
    }
}
