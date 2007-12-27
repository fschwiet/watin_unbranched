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
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;
using Thought.Net.Telnet;
using WatiN.Core.Exceptions;
using WatiN.Core.Logging;

namespace WatiN.Core.Mozilla
{
    public class FireFoxClientPort : IDisposable
    {
        #region Private fields

        /// <summary>
        /// Used by CreateElementVariableName
        /// </summary>
        private static long elementCounter = 0;
        
        /// <summary>
        /// The path to FireFox executable
        /// </summary>
        private string pathToExe;

        /// <summary>
        /// <c>true</c> if we have successfully connected to FireFox
        /// </summary>
        private bool connected;

        /// <summary>
        /// Telnet client used to communicate with FireFox jssh over port 9997
        /// </summary>
        private TelnetClient telnetClient;

        /// <summary>
        /// Underlying socket used by <see cref="telnetClient"/>.
        /// </summary>
        private Socket telnetSocket;

        /// <summary>
        /// The last reponse recieved from the jssh server
        /// </summary>
        private string lastResponse;

        /// <summary>
        /// The entire response from the jssh server so far.
        /// </summary>
        private StringBuilder response;

        /// <summary>
        /// <c>true</c> if the <see cref="Dispose()"/> method has been called to release resources.
        /// </summary>
        private bool disposed;

        /// <summary>
        /// The FireFox process
        /// </summary>
        private Process ffProcess;

        #endregion

        #region Public constants

        /// <summary>
        /// Name of the javascript variable that references the XUL:browser object.
        /// </summary>
        public const string BrowserVariableName = "browser";

        /// <summary>
        /// Name of the javascript variable that references the DOM:document object.
        /// </summary>
        public const string DocumentVariableName = "doc";

        /// <summary>
        /// Name of the javascript variable that references the DOM:window object.
        /// </summary>
        public const string WindowVariableName = "window";

        #endregion

        #region Constructors / destructors

        public FireFoxClientPort()
        {
            InitalizeExecutablePath();
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="FireFox"/> is reclaimed by garbage collection.
        /// </summary>
        ~FireFoxClientPort()
        {
            Dispose(false);
        }

        #endregion

        #region Public instance properties

        /// <summary>
        /// Gets the path to FireFox executable.
        /// </summary>
        /// <value>The path to exe.</value>
        public string PathToExe
        {
            get { return this.pathToExe; }
        }

        /// <summary>
        /// <c>true</c> if we have successfully connected to FireFox
        /// </summary>
        public bool Connected
        {
            get { return connected; }
        }

        /// <summary>
        /// The last reponse recieved from the jssh server
        /// </summary>
        public string LastResponse
        {
            get { return lastResponse; }
        }

        /// <summary>
        /// The entire response from the jssh server so far.
        /// </summary>
        public string Response
        {
            get { return response.ToString(); }
        }

        /// <summary>
        /// Gets a value indicating whether last response is null.
        /// </summary>
        /// <value><c>true</c> if last response is null; otherwise, <c>false</c>.</value>
        public bool LastResponseIsNull
        {
            get
            {
                return this.LastResponse.Equals("null", StringComparison.OrdinalIgnoreCase);
            }
        }

        /// <summary>
        /// Retruns the last reponse as a Boolen, default to false if converting <see cref="LastResponse"/> fails.
        /// </summary>
        public bool LastResponseAsBool
        {
            get
            {
                bool lastBoolResponse;
                Boolean.TryParse(this.LastResponse, out lastBoolResponse);
                return lastBoolResponse;
            }
        }

        #endregion

        #region Internal instance properties

        internal Process Process
        {
            get { return this.ffProcess; }
        }

        #endregion

        #region Public static methods

        /// <summary>
        /// Creates a unique variable name
        /// </summary>
        /// <returns></returns>
        public static string CreateVariableName()
        {
            elementCounter++;
            return string.Format("{0}.watin{1}", DocumentVariableName, elementCounter);
        }

        #endregion
        
        #region Public instance methods

        /// <summary>
        /// Reloads the javascript variables that are scoped at the document level.
        /// </summary>
        public void InitializeDocument()
        {
            this.Write(string.Format("var {0} = {1}.document;", DocumentVariableName, WindowVariableName));
        }

        public void Connect()
        {
            if (!this.connected)
            {
                this.disposed = false;
                Logger.LogAction("Attempting to connect to jssh server on localhost port 9997.");
                this.lastResponse = string.Empty;
                this.response = new StringBuilder();

                this.ffProcess = new Process();
                this.ffProcess.StartInfo.FileName = this.PathToExe;
                this.ffProcess.StartInfo.Arguments = "-jssh";
                this.ffProcess.Start();                
                this.ffProcess.WaitForInputIdle(100);

                this.telnetSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    this.telnetSocket.Connect(IPAddress.Parse("127.0.0.1"), 9997);
                }
                catch(SocketException sockException)
                {
                    Logger.LogAction(string.Format("Failed connecting to jssh server.\nError code:{0}\nError message:{1}", sockException.ErrorCode, sockException.Message));
                    throw new FireFoxException("Unable to connect to jssh server, please make sure you have correctly installed the jssh.xpi plugin", sockException);
                }

                this.telnetClient = new Thought.Net.Telnet.TelnetClient(this.telnetSocket);                
                this.WriteLine();
                Logger.LogAction("Successfully connected to FireFox using jssh.");
                this.DefineDefaultJSVariables();
            }

            this.connected = true;
        }

        ///<summary>
        ///Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ///</summary>
        ///<filterpriority>2</filterpriority>
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
                    if (this.telnetClient != null && this.telnetSocket.Connected)
                    {
                        Logger.LogAction("Closing connection to jssh");
                        this.Write(string.Format("{0}.close()", WindowVariableName), false);
                        this.telnetClient.Close();
                        this.ffProcess.WaitForExit(5000);
                    }
                }

                // Call the appropriate methods to clean up 
                // unmanaged resources here.
                if (this.ffProcess != null)
                {
                    if (!this.ffProcess.HasExited)
                    {
                        Logger.LogAction("Closing FireFox");
                        this.ffProcess.Kill();
                    }
                }
            }

            this.disposed = true;
            this.connected = false;
        }

        #endregion Protected instance methods

        #region Private static methods

        /// <summary>
        /// Cleans the response.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>Response from FireFox with out any of the telnet UI characters</returns>
        private static string CleanTelnetResponse(string response)
        {
            //HACK refactor in the future, should find a cleaner way of doing this.
            if (!string.IsNullOrEmpty(response))
            {
                if (response.EndsWith(string.Format("{0}>", "\n")))
                {
                    response = response.Substring(0, response.Length - 2);
                }
                else if (response.EndsWith(string.Format("?{0}> ", "\n")))
                {
                    response = response.Substring(0, response.Length - 4);
                }
                else if (response.EndsWith(string.Format("{0}> ", "\n")))
                {
                    response = response.Substring(0, response.Length - 3);
                }
                else if (response.EndsWith(string.Format("{0}> {0}", "\n")))
                {
                    response = response.Substring(0, response.Length - 4);
                }
                else if (response.EndsWith(string.Format("{0}> {0}{0}", "\n")))
                {
                    response = response.Substring(0, response.Length - 5);
                }
                else if (response.EndsWith(string.Format("{0}>", Environment.NewLine)))
                {
                    response = response.Substring(0, response.Length - 3);
                }
                else if (response.EndsWith(string.Format("{0}> ", Environment.NewLine)))
                {
                    response = response.Substring(0, response.Length - 4);
                }
                else if (response.EndsWith(string.Format("{0}> {0}", Environment.NewLine)))
                {
                    response = response.Substring(0, response.Length - 6);
                }
                else if (response.EndsWith(string.Format("{0}> {0}{0}", Environment.NewLine)))
                {
                    response = response.Substring(0, response.Length - 8);
                }

                if (response.StartsWith("> "))
                {
                    response = response.Substring(2);
                }
                else if (response.StartsWith(string.Format("{0}> ", "\n")))
                {
                    response = response.Substring(3);
                }

                response = response.Trim();
            }
            return response;
        }

        #endregion

        #region Private instance methods

        /// <summary>
        /// Defines the default JS variables used to automate FireFox.
        /// </summary>
        private void DefineDefaultJSVariables()
        {
            this.Write(string.Format("var w0 = getWindows()[0]; var {0} = w0.content;", WindowVariableName));
            this.Write(string.Format("var {0} = {1}.document; var {2} = w0.getBrowser()", DocumentVariableName, WindowVariableName, BrowserVariableName));
        }

        /// <summary>
        /// Writes a line to the jssh server.
        /// </summary>
        private void WriteLine()
        {
            this.telnetClient.Write(ASCIIEncoding.ASCII.GetBytes("\n")[0]);
            this.ReadResponse();
        }


        private void Write(string data, bool readResponse)
        {
            byte[] bytes = ASCIIEncoding.ASCII.GetBytes(data + "\n");
            this.telnetClient.Write(bytes, 0, bytes.Length);
            
            Logger.LogAction("sending: {0}", data);
            
            if (readResponse)
            {
                this.ReadResponse();
            }
        }

        /// <summary>
        /// Writes the specified data to the jssh server.
        /// </summary>
        /// <param name="data">The data.</param>
        internal void Write(string data)
        {
            this.Write(data, true);
        }

        /// <summary>
        /// Writes the specified data to the jssh server.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="args">Arguments to be passed to <see cref="string.Format(string,object[])"/></param>
        internal void Write(string data, params object[] args)
        {
        	this.Write(string.Format(data, args), true);
        }

        /// <summary>
        /// Reads the response from the jssh server.
        /// </summary>
        private void ReadResponse()
        {
            this.lastResponse = string.Empty;

            byte[] buffer = new byte[1024];
            int read = 0;
            NetworkStream stream = new NetworkStream(this.telnetSocket);
            while (!stream.DataAvailable)
            {
                // Hack: need to work out a better way for this
                System.Threading.Thread.Sleep(200);
            }

            do
            {
                read = this.telnetClient.Read(buffer, 0, 1024);
                string readData = ASCIIEncoding.ASCII.GetString(buffer, 0, read);
				
                // TODO: Sometimes this action raises a formatting exception
                //       For example when running FireFoxTests.Google test
                try
                {
                	Logger.LogAction(string.Format("jssh says: {0}", readData));
                }
                catch{}
                
                this.lastResponse += CleanTelnetResponse(readData);
            } while (read==1024);

            this.lastResponse = this.lastResponse.Trim();
            if (this.lastResponse.StartsWith("SyntaxError", StringComparison.InvariantCultureIgnoreCase) ||
                this.lastResponse.StartsWith("TypeError", StringComparison.InvariantCultureIgnoreCase) ||
                this.lastResponse.StartsWith("uncaught exception", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new FireFoxException(string.Format("Error sending last message to jssh server: {0}", this.lastResponse));    
            }

            this.response.Append(this.lastResponse);
            
        }

        /// <summary>
        /// Initalizes the executable path.
        /// </summary>
        private void InitalizeExecutablePath()
        {
            RegistryKey mozillaKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Mozilla\Mozilla Firefox");
            if (mozillaKey != null)
            {
                InitializeUsingRegistry(mozillaKey);
            }
            else
            {
                // We try and guess common locations where FireFox might be installed
                string tempPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles), @"Mozilla FireFox\FireFox.exe");
                if (File.Exists(tempPath))
                {
                    this.pathToExe = tempPath;                                
                }
                else
                {
                    tempPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles) + " (x86)", @"Mozilla FireFox\FireFox.exe");
                    if (File.Exists(tempPath))
                    {
                        this.pathToExe = tempPath;
                    }
                    else
                    {
                        throw new FireFoxException("Unable to determine the current version of FireFox tried looking in the registry and the common locations on disk, please make sure you have installed FireFox and Jssh correctly");
                    }        
                }
            }

        }

        /// <summary>
        /// Initializes the executable path to FireFox using the registry.
        /// </summary>
        /// <param name="mozillaKey">The mozilla key.</param>
        private void InitializeUsingRegistry(RegistryKey mozillaKey)
        {
            string currentVersion = (string) mozillaKey.GetValue("CurrentVersion");
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

            this.pathToExe = (string) currentMain.GetValue("PathToExe");
            if (!File.Exists(this.pathToExe))
            {
                throw new FireFoxException(
                    "FireFox executable listed in the registry does not exist, please make sure you have installed FireFox and Jssh correctly");
            }
        }        

        #endregion private instance methods       
    }
}
