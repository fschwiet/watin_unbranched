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
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using WatiN.Core.Interfaces;
using WatiN.Core.Mozilla;

namespace WatiN.Core.UnitTests.CrossBrowserTests
{
    [TestFixture]
    public class IBrowserTests : WatiNCrossBrowserTest
    {
        #region Public instance test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.Back()"/> method.
        /// </summary>
        [Test]
        public void Back()
        {
            ExecuteTest(BackTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.BringToFront"/> method.
        /// </summary>
        [Test]
        public void BringToFront()
        {
            ExecuteTest(BringToFrontTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.ContainsText(string)"/> and <see cref="IBrowser.ContainsText(Regex)"/>
        /// </summary>        
        [Test]
        public void ContainsText()
        {
            ExecuteTest(ContainsTextTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.Eval"/> method.
        /// </summary>
        [Test]
        public void Eval()
        {
            ExecuteTest(EvalTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.FindText"/> method.
        /// </summary>
        [Test]
        public void FindText()
        {
            ExecuteTest(FindTextTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.Forward()"/> method.
        /// </summary>
        [Test]
        public void Forward()
        {
            ExecuteTest(ForwardTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.hWnd"/> property.
        /// </summary>
        [Test]
        public void hWnd()
        {
            ExecuteTest(hWndTest);
        }

        [Test]
        public void PressTab()
        {
            ExecuteTest(PressTabTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.ProcessID"/> property.
        /// </summary>
        [Test]
        public void ProcessID()
        {
            ExecuteTest(ProcessIDTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.Refresh"/> method.
        /// </summary>
        [Test]
        public void Refresh()
        {
            ExecuteTest(RefreshTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.Reopen()"/> method.
        /// </summary>
        [Test]
        public void Reopen()
        {
            ExecuteTest(ReopenTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.RunScript"/> method.
        /// </summary>
        [Test]
        public void RunScript()
        {
            ExecuteTest(RunScriptTest);
        }

        [Test]
        public void WindowStyle()
        {
            ExecuteTest(WindowStyleTest);
        }

        #endregion

        #region Private static methods

        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.Back()"/> method.
        /// </summary>
        /// <param name="browser">The browser.</param>
        private static void BackTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            GoTo(ImagesURI, browser);
            browser.Back();

            Assert.AreEqual(MainURI, browser.Url, GetErrorMessage("Url not the expected value, Back action failed", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.BringToFront"/> method.
        /// </summary>
        private static void BringToFrontTest(IBrowser browser)
        {

            using (Process calcProcess = new Process())
            {
                try
                {
                    calcProcess.StartInfo.FileName = "calc.exe";
                    calcProcess.Start();
                    calcProcess.WaitForInputIdle(5000);
                    NativeMethods.SetForegroundWindow(calcProcess.MainWindowHandle);

                    browser.BringToFront();
                    Thread.Sleep(1000);
                    int foregroundHandle = NativeMethods.GetForegroundWindow().ToInt32();
                    int browserHandle = browser.hWnd.ToInt32();
                    Assert.IsTrue(foregroundHandle.Equals(browserHandle),
                                  GetErrorMessage(string.Format("IBrowser.BringToFront() failed to operate as expected the first time. Expected {0} got {1}", browserHandle, foregroundHandle) , browser));

                    Assert.IsTrue(NativeMethods.SetForegroundWindow(calcProcess.MainWindowHandle), "Could not set calc back as the foreground window :(");

                    browser.BringToFront();
                    Thread.Sleep(1000);
                    
                    foregroundHandle = NativeMethods.GetForegroundWindow().ToInt32();
                    browserHandle = browser.hWnd.ToInt32();
                    Assert.IsTrue(foregroundHandle.Equals(browserHandle),
                                  GetErrorMessage(string.Format("IBrowser.BringToFront() failed to operate as expected the second time. Expected {0} got {1}", browserHandle, foregroundHandle), browser));

                }
                finally
                {
                    if (!calcProcess.HasExited)
                    {
                        calcProcess.CloseMainWindow();
                    }
                }
            }
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.ContainsText(string)"/> and <see cref="IBrowser.ContainsText(Regex)"/>
        /// </summary>        
        private static void ContainsTextTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            Assert.IsFalse(browser.ContainsText("This text does not exist."), GetErrorMessage("IBrowser.ContainsText returned an incorrect value.", browser));
            Assert.IsTrue(browser.ContainsText("label for txtLabelB"), GetErrorMessage("IBrowser.ContainsText returned an incorrect value.", browser));

            Assert.IsFalse(browser.ContainsText(new Regex("This text does not exist.")), GetErrorMessage("IBrowser.ContainsText returned an incorrect value.", browser));
            Assert.IsTrue(browser.ContainsText(new Regex("label for txtLabelB")), GetErrorMessage("IBrowser.ContainsText returned an incorrect value.", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.Eval"/> method.
        /// </summary>
        private static void EvalTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            string result = browser.Eval("2 + 2");
            Assert.AreEqual("4", result, GetErrorMessage("IBrowser.Eval returned an incorrect result", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.FindText"/> method.
        /// </summary>
        private static void FindTextTest(IBrowser browser)
        {
            GoTo(MainURI, browser);

            Assert.AreEqual("Contains text in DIV", browser.FindText(new Regex("Contains .* in DIV")), "IBrowser.FindText did not find the expected text.");
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.Forward()"/> method.
        /// </summary>
        public void ForwardTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            GoTo(ImagesURI, browser);
            browser.Back();
            browser.Forward();

            Assert.AreEqual(ImagesURI, browser.Url, GetErrorMessage("Url not the expected value, Forward action failed", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.hWnd"/> property.
        /// </summary>        
        private static void hWndTest(IBrowser browser)
        {
            Assert.AreNotEqual(IntPtr.Zero, browser.hWnd, GetErrorMessage("window handle should not be zero", browser));
        }

        private static void PressTabTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            
            browser.BringToFront();
            Thread.Sleep(500);
            Assert.IsTrue(NativeMethods.GetForegroundWindow().Equals(browser.hWnd),
                       GetErrorMessage(string.Format("IBrowser.BringToFront() failed to operate as expected. Expected hWnd {0} found {1}", browser.hWnd, NativeMethods.GetForegroundWindow()), browser));

            browser.TextField("name").Focus();

            IElement element = browser.ActiveElement;
            Assert.AreEqual("name", element.Id);

            browser.PressTab();

            element = browser.ActiveElement;
            Assert.AreEqual("popupid", element.Id, GetErrorMessage("Active element not changed appears press tab did not operate as expected", browser));

        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.ProcessID"/> property.
        /// </summary>
        private static void ProcessIDTest(IBrowser browser)
        {
            Assert.AreNotEqual(0, browser.ProcessID);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.Refresh"/> method.
        /// </summary>
        /// <param name="browser">The browser.</param>
        private static void RefreshTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            browser.Refresh();
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.Reopen()"/> method.
        /// </summary>
        private static void ReopenTest(IBrowser browser)
        {
            GoTo(MainURI, browser);
            browser.Reopen();
            Assert.AreEqual("about:blank", browser.Url, GetErrorMessage("Incorrect Url found after a Reopen action was performed.", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IBrowser.RunScript"/> method.
        /// </summary>
        private static void RunScriptTest(IBrowser browser)
        {
            GoTo(MainURI, browser);

            Assert.IsFalse(browser.ContainsText("java script has run"));
            browser.RunScript("window.document.write('java script has run');");
            Assert.IsTrue(browser.ContainsText("java script has run"), GetErrorMessage("IBrowser.RunScript method failed to complete correctly.", browser));
        }

        private static void WindowStyleTest(IBrowser browser)
        {
            GoTo(MainURI, browser);

            NativeMethods.WindowShowStyle currentStyle = browser.GetWindowStyle();

            if (currentStyle == NativeMethods.WindowShowStyle.Maximize)
            {
                currentStyle = NativeMethods.WindowShowStyle.ShowNormal;
            }

            browser.ShowWindow(NativeMethods.WindowShowStyle.Maximize);
            Assert.AreEqual(NativeMethods.WindowShowStyle.Maximize.ToString(), browser.GetWindowStyle().ToString(), "Not maximized");

            browser.ShowWindow(NativeMethods.WindowShowStyle.Restore);
            Assert.AreEqual(currentStyle.ToString(), browser.GetWindowStyle().ToString(), "Not Restored");

            browser.ShowWindow(NativeMethods.WindowShowStyle.Minimize);
            Assert.AreEqual(NativeMethods.WindowShowStyle.ShowMinimized.ToString(), browser.GetWindowStyle().ToString(), "Not Minimize");

            browser.ShowWindow(NativeMethods.WindowShowStyle.ShowNormal);
            Assert.AreEqual(NativeMethods.WindowShowStyle.ShowNormal.ToString(), browser.GetWindowStyle().ToString(), "Not ShowNormal");

        }

        #endregion
    }
}
