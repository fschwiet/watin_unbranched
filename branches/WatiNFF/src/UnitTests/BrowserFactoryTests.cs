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

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using WatiN.Core;
using WatiN.Core.Interfaces;
using WatiN.Core.Logging;
using WatiN.Core.Mozilla;

namespace WatiN.Core.UnitTests
{
    [TestFixture]
    public class BrowserFactoryTests
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            Logger.LogWriter = new DebugLogWriter();
        }

        /// <summary>
        /// Test the default <see cref="BrowserFactory.Settings"/> values are as expected.
        /// </summary>
        [Test]
        public void BrowserSettingsDefaults()
        {
            Assert.AreEqual(BrowserType.InternetExplorer, BrowserFactory.Settings.BrowserType, "Incorrect default value for Browser.Settings.BrowserType");
        }

        /// <summary>
        /// Test creating a default browser instance, which should be IE
        /// </summary>
        [Test]
        public void CreateDefaultBrowserInstance()
        {
            using (IBrowser defaultBrowser = BrowserFactory.Create())
            {
                Assert.IsInstanceOfType(typeof(IE), defaultBrowser, "Incorrect default type created.");
            }
        }

        /// <summary>
        /// Test creating a non-default browser instance using <see cref="BrowserFactory.Create(BrowserType)"/>.
        /// </summary>
        [Test]
        public void CreateFireFoxBrowserInstance()
        {
            using (IBrowser fireFoxBrowser = BrowserFactory.Create(BrowserType.FireFox))
            {
                Assert.IsInstanceOfType(typeof(FireFox), fireFoxBrowser, "Incorrect default type created.");
            }
        }

        /// <summary>
        /// Test creating a FireFox browser instance, by changing the <see cref="BrowserFactory.Settings"/>.
        /// </summary>
        [Test]
        public void CreateFireFoxBrowserInstanceUsingSettings()
        {
            BrowserFactory.Settings.BrowserType = BrowserType.FireFox;
            using (IBrowser fireFoxBrowser = BrowserFactory.Create())
            {
                Assert.IsInstanceOfType(typeof(FireFox), fireFoxBrowser, "Incorrect default type created.");
            }
        }
    }
}
