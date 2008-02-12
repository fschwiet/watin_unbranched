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

using System.IO;
using NUnit.Framework;
using WatiN.Core.Logging;
using WatiN.Core.Mozilla;
using WatiN.Core.Interfaces;
using System.Threading;
using WatiN.Core.UnitTests.CrossBrowserTests;

namespace WatiN.Core.UnitTests.Mozilla
{
    [TestFixture]
    public class FireFoxTests : WatiNTest
    {       
        /// <summary>
        /// Test that you can navigate a Url.
        /// </summary>
        [Test]
        public void GotoUrl()
        {
            using (IBrowser fireFox = BrowserFactory.Create(BrowserType.FireFox))
            {
                fireFox.GoTo(MainURI.ToString());
                Assert.AreEqual(MainURI, fireFox.Url);
            }
        }

        /// <summary>
        /// Test that we can retrieve the path to the FireFox executable using the registry
        /// </summary>
        [Test]
        public void PathToFireFoxExecutable()
        {

            Assert.IsNotNull(FireFox.PathToExe, "Did not find the path to the FireFox executable");
            Assert.IsTrue(FireFox.PathToExe.Contains("firefox.exe"), "Did not find the expected value for the path to the FireFox executable");
            Assert.IsTrue(File.Exists(FireFox.PathToExe), string.Format("{0} does not exist.", FireFox.PathToExe));
        }

        /// <summary>
        /// Test that you can navigate a Url.
        /// </summary>
        [Test]
        public void CreateFireFoxWithUrlInConstructor()
        {
            using (FireFox ff = new FireFox(BaseElementsTests.MainURI.ToString()))
            {
                Assert.AreEqual(BaseElementsTests.MainURI, ff.Url);
            }
        }

        /// <summary>
        /// Test that you can navigate a Uri.
        /// </summary>
        [Test]
        public void CreateFireFoxWithUriInConstructor()
        {
            using (FireFox ff = new FireFox(BaseElementsTests.MainURI.ToString()))
            {
                Assert.AreEqual(BaseElementsTests.MainURI, ff.Url);
            }
        }

        [Test, Category("InternetConnectionNeeded")]
        public void Google()
        {
            using (FireFox ff = new FireFox(BaseElementsTests.MainURI.ToString()))
            {
                ff.GoTo("http://www.google.com");
                ITextField q = ff.TextField(Find.ByName("q"));
                Assert.That(q.Exists);
                q.Value = "WatiN";
                ff.Button(Find.ByName("btnG")).Click();

                string text = ff.Text;
                System.Console.WriteLine(text);
                Assert.IsTrue(text.Contains("WatiN"));
            }
        }
    }
}
