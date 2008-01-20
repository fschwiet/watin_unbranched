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

using System.Diagnostics;
using System.IO;
using NUnit.Framework;
using WatiN.Core.Logging;
using WatiN.Core.Mozilla;

namespace WatiN.Core.UnitTests.Mozilla
{
    [TestFixture]
    public class FireFoxClientPortTests : WatiNTest
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            Logger.LogWriter = new DebugLogWriter();
        }

        /// <summary>
        /// Tests that if the FireFox path is specified in the app config
        /// It uses this instead of the registry
        /// </summary>
        [Test]
        public void FireFoxPathSpecifiedInSettings()
        {
            // TODO Add this setting in App.config dynamically so that other tests
            // can run with out this bit being set.

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
        /// Test that an error is thrown if you try to connect with an instance of FireFox already open
        /// </summary>
        [Test, ExpectedException(typeof(FireFoxException))] 
        public void ShouldNotConnectWithRunningInstances()
        {

            using (FireFoxClientPort ffPort = new FireFoxClientPort())
            {
                using (Process existingInstance = new Process())
                {
                    existingInstance.StartInfo.FileName = ffPort.PathToExe;
                    existingInstance.Start();
                    ffPort.Connect();
                }
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

        /// <summary>
        /// This test tries to stress the communication between WaitN and the ssh server to ensure that our networking code
        /// is stable
        /// </summary>
        [Test]
        public void Stress()
        {
            using (FireFoxClientPort ffPort = new FireFoxClientPort())
            {
                ffPort.Connect();
                ffPort.Write("{0}.loadURI(\"{1}\")", FireFoxClientPort.BrowserVariableName, MainURI);
                ffPort.Write("for (i = 0; i < 1000; i++){links = doc.getElementsByTagName(\"a\");links.length;} true;");
                Assert.IsTrue(ffPort.LastResponseAsBool);

                for (int i =0 ; i < 50; i++)
                {
                    ffPort.Write("{0}.loadURI(\"{1}\")", FireFoxClientPort.BrowserVariableName, MainURI);                
                    ffPort.Write("for (i = 0; i < 1000; i++){links = doc.getElementsByTagName(\"a\");links.length;} true;");
                    Assert.IsTrue(ffPort.LastResponseAsBool);
                }
            }
        }

        /// <summary>
        /// If FireFox does not shut down correctly from a previous session you may get
        /// a dialog appear with the window text "Firefox - Restore Previous Session".
        /// This test simulates a bad Firefox shutdown and checks that our connect method
        /// can deal with this dialog when shelling a new instance of Firefox.
        /// </summary>
        [Test]
        public void HandleStartNewSessionDialog()
        {
            using (Process process = new Process())
            {
                using (FireFoxClientPort clientPort = new FireFoxClientPort())
                {
                    // Simulate a bad shut down.
                    process.StartInfo.FileName = clientPort.PathToExe;
                    process.Start();
                    process.WaitForInputIdle(5000);
                    process.Kill();

                    // Now try and connect
                    clientPort.Connect();

                    Assert.IsTrue(clientPort.Connected, "FireFoxClientPort.Connect did not handle the restore previous dialig scenario.");
                }
            }
        }


        /// <summary>
        /// Test that an exception is raised if an attempt to write is 
        /// made before connecting successfully to the jssh server.
        /// </summary>
        [Test, ExpectedException(typeof(FireFoxException))]
        public void WriteBeforeConnect()
        {
            using (FireFoxClientPort ffPort = new FireFoxClientPort())
            {
                ffPort.Write("for (i = 0; i < 1000; i++){links = doc.getElementsByTagName(\"a\");links.length;} return true;");
                Assert.IsTrue(ffPort.LastResponseAsBool);
            }
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="FireFoxClientPort.IsMainWindowVisible"/> property.
        /// </summary>
        [Test]
        public void IsMainWindowVisible()
        {
            using (FireFoxClientPort ffPort = new FireFoxClientPort())
            {
                ffPort.Connect();
                Assert.IsTrue(ffPort.IsMainWindowVisible);
            }
        }
    }
}
