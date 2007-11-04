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

namespace WatiN.Core.UnitTests.Mozilla
{
    [TestFixture]
    public class FireFoxClientPortTests
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            Logger.LogWriter = new DebugLogWriter();
        }

        /// <summary>
        /// This test requires firefox and jssh installed. To install jssh go to: http://code.google.com/p/firewatir/
        /// </summary>
        [Test]
        public void Connect()
        {
            using (FireFoxClientPort ffPort = new FireFoxClientPort())
            {                          
                ffPort.Connect();
                Assert.IsTrue(ffPort.Response.Contains("Welcome to the Mozilla JavaScript Shell!"));
            }
            
        }

        /// <summary>
        /// Test that we can retrieve the path to the FireFox executable using the registry
        /// </summary>
        [Test]
        public void PathToFireFoxExecutable()
        {
            using (FireFoxClientPort ffPort = new FireFoxClientPort())
            {
                Assert.IsNotNull(ffPort.PathToExe, "Did not find the path to the FireFox executable");
                Assert.IsTrue(ffPort.PathToExe.Contains("firefox.exe"), "Did not find the expected value for the path to the FireFox executable");
                Assert.IsTrue(File.Exists(ffPort.PathToExe), string.Format("{0} does not exist.", ffPort.PathToExe));
            }
        }
    }
}
