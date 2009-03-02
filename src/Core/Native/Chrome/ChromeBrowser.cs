// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="ChromeBrowser.cs">
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
//   Defines the ChromeBrowser type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace WatiN.Core.Native.Chrome
{
    /// <summary>
    /// Native driver the communicates with the Chrome browser using a
    /// telnet session <see cref="ClientPort"/>.
    /// </summary>
    public class ChromeBrowser : INativeBrowser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChromeBrowser"/> class.
        /// </summary>
        /// <param name="clientPort">The client port.</param>
        public ChromeBrowser(ChromeClientPort clientPort)
        {
            this.ClientPort = clientPort;
        }

        /// <summary>
        /// Gets the client port.
        /// </summary>
        /// <value>The client port.</value>
        public ChromeClientPort ClientPort { get; private set; }
    }
}