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
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Win32;
using WatiN.Core.Exceptions;
using WatiN.Core.Interfaces;
using WatiN.Core.Logging;

namespace WatiN.Core.Mozilla
{
    public class FireFox : Document, IDisposable, IBrowser
    {
        /// <summary>
        /// <c>true</c> if the <see cref="Dispose()"/> method has been called to release resources.
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Wrapper for the XUL:Browser object see: http://developer.mozilla.org/en/docs/XUL:browser
        /// </summary>
        private XULBrowser xulBrowser;

        #region Public constructors / destructor

        /// <summary>
        /// Initializes a new instance of the <see cref="FireFox"/> class.
        /// </summary>
        public FireFox() : base(new FireFoxClientPort())
        {
        	CreateFireFoxInstance();
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="FireFox"/> class.
        /// </summary>
        /// <param name="url">The url to go to</param>
        public FireFox(string url) : base(new FireFoxClientPort())
        {
        	CreateFireFoxInstance();            
            GoTo(url);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FireFox"/> class.
        /// </summary>
        /// <param name="uri">The url to go to</param>
        public FireFox(Uri uri) : base(new FireFoxClientPort())
        {
        	CreateFireFoxInstance();            
            GoTo(uri);
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="FireFox"/> is reclaimed by garbage collection.
        /// </summary>
        ~FireFox()
        {
            Dispose(false);
        }

        #endregion

        #region Public instance properties

        public BrowserType BrowserType
        {
            get { return Core.BrowserType.FireFox; }
        }

        /// <summary>
        /// Determines whether the text inside the HTML Body element contains the given <paramref name="text" />.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        ///     <c>true</c> if the specified text is contained in <see cref="IDocument.Html"/>; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsText(string text)
        {
            string innertext = Text;

            if (innertext == null) return false;

            return (innertext.IndexOf(text) >= 0);
        }

        /// <summary>
        /// Determines whether the text inside the HTML Body element contains the given <paramref name="regex" />.
        /// </summary>
        /// <param name="regex">The regular expression to match with.</param>
        /// <returns>
        ///     <c>true</c> if the specified text is contained in <see cref="IDocument.Html"/>; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsText(Regex regex)
        {
            string innertext = Text;

            if (innertext == null) return false;

            return (regex.Match(innertext).Success);
        }

        /// <summary>
        /// Evaluates the specified JavaScript code within the scope of this
        /// document. Returns the value computed by the last expression in the
        /// code block after implicit conversion to a string.
        /// </summary>
        /// <example>
        /// The following example shows an alert dialog then returns the string "4".
        /// <code>
        /// Eval("window.alert('Hello world!'); 2 + 2");
        /// </code>
        /// </example>
        /// <param name="javaScriptCode">The JavaScript code</param>
        /// <returns>The result converted to a string</returns>
        /// <exception cref="JavaScriptException">Thrown when the JavaScript code cannot be evaluated
        /// or throws an exception during evaluation</exception>
        public string Eval(string javaScriptCode)
        {
            return this.ClientPort.WriteAndRead(javaScriptCode);
        }

        /// <summary>
        /// Gets the text inside the HTML Body element that matches the regular expression.
        /// </summary>
        /// <param name="regex">The regular expression to match with.</param>
        /// <returns>The matching text, or null if none.</returns>
        public string FindText(Regex regex)
        {
            Match match = regex.Match(Text);

            return match.Success ? match.Value : null;
        }

        #endregion Public instance properties

        #region Public instance methods

        /// <summary>
        /// Navigates the browser back to the previously displayed Url (like the back
        /// button in Internet Explorer).
        /// </summary>
        public void Back()
        {
            this.xulBrowser.Back();
        }

        /// <summary>
        /// Brings the current browser to the front (makes it the top window)
        /// </summary>
        public void BringToFront()
        {
            this.xulBrowser.BringToFront();
        }

        /// <summary>
        /// Navigates the browser forward to the next displayed Url (like the forward
        /// button in Internet Explorer).
        /// </summary>
        public void Forward()
        {
            this.xulBrowser.Forward();    
        }

        /// <summary>
        /// Gets the window style.
        /// </summary>
        /// <returns>The style currently applied to the ie window.</returns>
        public NativeMethods.WindowShowStyle GetWindowStyle()
        {
            NativeMethods.WINDOWPLACEMENT placement = new NativeMethods.WINDOWPLACEMENT();
            placement.length = Marshal.SizeOf(placement);

            NativeMethods.GetWindowPlacement(hWnd, ref placement);

            return (NativeMethods.WindowShowStyle)placement.showCmd;
        }

        /// <summary>
        /// Navigates to the given <paramref name="url" />.
        /// </summary>
        /// <param name="url">The URL to GoTo.</param>
        /// <example>
        /// The following example creates a new Internet Explorer instance and navigates to
        /// the WatiN Project website on SourceForge.
        /// <code>
        /// using WatiN.Core;
        /// 
        /// namespace NewIEExample
        /// {
        ///    public class WatiNWebsite
        ///    {
        ///      public WatiNWebsite()
        ///      {
        ///        FireFox ff = new FireFox();
        ///        ff.GoTo("http://watin.sourceforge.net");
        ///      }
        ///    }
        ///  }
        /// </code>
        /// </example>
        public void GoTo(string url)
        {
            this.GoTo(CreateUri(url));
        }

        /// <summary>
        /// Navigates Internet Explorer to the given <paramref name="url" />.
        /// </summary>
        /// <param name="url">The URL specified as a wel formed Uri.</param>
        /// <example>
        /// The following example creates an Uri and Internet Explorer instance and navigates to
        /// the WatiN Project website on SourceForge.
        /// <code>
        /// using WatiN.Core;
        /// using System;
        /// 
        /// namespace NewIEExample
        /// {
        ///    public class WatiNWebsite
        ///    {
        ///      public WatiNWebsite()
        ///      {
        ///        Uri URL = new Uri("http://watin.sourceforge.net");
        ///        IE ie = new IE();
        ///        ie.GoTo(URL);
        ///      }
        ///    }
        ///  }
        /// </code>
        /// </example>
        public void GoTo(Uri url)
        {
            this.xulBrowser.LoadUri(url);
        }

        public IntPtr hWnd
        {
            get { return this.xulBrowser.Handle; }
        }

        /// <summary>
        /// Sends a Tab key to the current browser window to simulate tabbing through
        /// the elements (and address bar).
        /// </summary>
        public void PressTab()
        {
            if (!Debugger.IsAttached)
            {
                int intThreadIDIE;
                int intCurrentThreadID;

                NativeMethods.WindowShowStyle currentStyle = GetWindowStyle();

                ShowWindow(NativeMethods.WindowShowStyle.Restore);
                BringToFront();

                intThreadIDIE = ProcessID;
                intCurrentThreadID = NativeMethods.GetCurrentThreadId();

                NativeMethods.AttachThreadInput(intCurrentThreadID, intThreadIDIE, true);

                NativeMethods.keybd_event(NativeMethods.KEYEVENTF_TAB, 0x45, NativeMethods.KEYEVENTF_EXTENDEDKEY, 0);
                NativeMethods.keybd_event(NativeMethods.KEYEVENTF_TAB, 0x45, NativeMethods.KEYEVENTF_EXTENDEDKEY | NativeMethods.KEYEVENTF_KEYUP, 0);

                NativeMethods.AttachThreadInput(intCurrentThreadID, intThreadIDIE, false);

                ShowWindow(currentStyle);
            }
        }

        /// <summary>
        /// Gets the process ID the current browser is running in.
        /// </summary>
        /// <value>The process ID the current browser is running in.</value>
        public int ProcessID
        {
            get { return this.xulBrowser.ProcessId; }
        }

        /// <summary>
        /// Gets the number of running FireFox processes.
        /// </summary>
        /// <value>The number of running FireFox processes.</value>
        internal static int CurrentProcessCount
        {
            get
            {
                int ffCount = 0;

                foreach (Process process in System.Diagnostics.Process.GetProcesses())
                {
                    if (process.ProcessName.Equals("firefox", StringComparison.OrdinalIgnoreCase))
                    {
                        ffCount++;
                    }
                }

                return ffCount;
            }
        }

        /// <summary>
        /// Gets the current FireFox process.
        /// </summary>
        /// <value>The current FireFox process or null if none is found.</value>
        internal static Process CurrentProcess
        {
            get
            {
                Process ffProcess = null;

                foreach (Process process in System.Diagnostics.Process.GetProcesses())
                {
                    if (process.ProcessName.Equals("firefox", StringComparison.OrdinalIgnoreCase))
                    {
                        ffProcess = process;
                    }
                }

                return ffProcess;
            }
        }

        private static string pathToExe;
        /// <summary>
        /// Gets the path to FireFox executable.
        /// </summary>
        /// <value>The path to exe.</value>
        public static string PathToExe
        {
            get
            {
                if (pathToExe == null)
                {
                    pathToExe = GetExecutablePath();
                }

                return pathToExe;
            }
        }

        internal static Process CreateProcess()
        {
            return CreateProcess(string.Empty);
        }

        internal static Process CreateProcess(string arguments)
        {
            return CreateProcess(arguments, true);
        }

        internal static Process CreateProcess(string arguments, bool waitForMainWindow)
        {
            Process ffProcess = new Process();
            ffProcess.StartInfo.FileName = PathToExe;
            ffProcess.StartInfo.Arguments = arguments;
            ffProcess.Start();
            ffProcess.WaitForInputIdle(5000);
            ffProcess.Refresh();

            if (waitForMainWindow)
            {
                SimpleTimer timer = new SimpleTimer(5);
                while (!timer.Elapsed)
                {
                    if (!ffProcess.HasExited && ffProcess.MainWindowHandle != IntPtr.Zero)
                    {
                        Logger.LogAction("Waited for FireFox, main window handle found.");
                        break;    
                    }

                    Thread.Sleep(100);
                    ffProcess.Refresh();
                }

                if (timer.Elapsed)
                {
                    Debug.WriteLine("Timer elapsed waiting for FireFox to start.");
                }
            }

            return ffProcess;
        }


        /// <summary>
        /// Initalizes the executable path.
        /// </summary>
        private static string GetExecutablePath()
        {
            string path = null;
            RegistryKey mozillaKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Mozilla\Mozilla Firefox");
            if (mozillaKey != null)
            {
                path = GetExecutablePathUsingRegistry(mozillaKey);
            }
            else
            {
                // We try and guess common locations where FireFox might be installed
                string tempPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles), @"Mozilla FireFox\FireFox.exe");
                if (File.Exists(tempPath))
                {
                    path = tempPath;
                }
                else
                {
                    tempPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles) + " (x86)", @"Mozilla FireFox\FireFox.exe");
                    if (File.Exists(tempPath))
                    {
                        path = tempPath;
                    }
                    else
                    {
                        throw new FireFoxException("Unable to determine the current version of FireFox tried looking in the registry and the common locations on disk, please make sure you have installed FireFox and Jssh correctly");
                    }
                }
            }

            return path;
        }

        /// <summary>
        /// Initializes the executable path to FireFox using the registry.
        /// </summary>
        /// <param name="mozillaKey">The mozilla key.</param>
        private static string GetExecutablePathUsingRegistry(RegistryKey mozillaKey)
        {
            string path = string.Empty;
            string currentVersion = (string)mozillaKey.GetValue("CurrentVersion");
            if (string.IsNullOrEmpty(currentVersion))
            {
                throw new FireFoxException("Unable to determine the current version of FireFox using the registry, please make sure you have installed FireFox and Jssh correctly");
            }

            RegistryKey currentMain = mozillaKey.OpenSubKey(string.Format(@"{0}\Main", currentVersion));
            if (currentMain == null)
            {
                throw new FireFoxException(
                    "Unable to determine the current version of FireFox using the registry, please make sure you have installed FireFox and Jssh correctly");
            }

            path = (string)currentMain.GetValue("PathToExe");
            if (!File.Exists(path))
            {
                throw new FireFoxException(
                    "FireFox executable listed in the registry does not exist, please make sure you have installed FireFox and Jssh correctly");
            }

            return path;
        }

        /// <summary>
        /// Reloads the currently displayed webpage.
        /// </summary>
        public void Refresh()
        {
            this.xulBrowser.Reload();
        }

        /// <summary>
        /// Closes then reopens the browser navigating to a blank page.
        /// </summary>
        /// <remarks>
        /// Useful when clearing the cookie cache and continuing execution to a test.
        /// </remarks>
        public void Reopen()
        {
            this.ClientPort.Dispose();
            this.ClientPort.Connect("");
        }

        /// <summary>
        /// Runs the javascript code in the current browser.
        /// </summary>
        /// <param name="javaScriptCode">The javascript code.</param>
        public void RunScript(string javaScriptCode)
        {
            this.ClientPort.Write(javaScriptCode);
        }

        /// <summary>
        /// Make the referenced Internet Explorer full screen, minimized, maximized and more.
        /// </summary>
        /// <param name="showStyle">The style to apply.</param>
        public void ShowWindow(NativeMethods.WindowShowStyle showStyle)
        {
            NativeMethods.ShowWindow(hWnd, (int)showStyle);
        }

        /// <summary>
        /// Waits until the page is completely loaded
        /// </summary>
        public void WaitForComplete()
        {
        	this.xulBrowser.WaitForComplete();
        }
        
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue 
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }
        
        #endregion Public instance methods

        #region Protected instance methods      

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    this.ClientPort.Dispose();
                }

                // Call the appropriate methods to clean up 
                // unmanaged resources here.
                
                
            }

            this.disposed = true;
        }

        #endregion Protected instance methods

        #region Private instance methods

        

        #endregion Private instance methods
        
        #region Private static methods

        private static Uri CreateUri(string url)
        {
            Uri uri;
            try
            {
                uri = new Uri(url);
            }
            catch (UriFormatException)
            {
                uri = new Uri("http://" + url);
            }
            return uri;
        }

        private void CreateFireFoxInstance()
        {
            Logger.LogAction("Creating new FireFox instance");
            this.ClientPort.Connect("");

            this.xulBrowser = new XULBrowser(this.ClientPort);
        }

        #endregion
    }
}
