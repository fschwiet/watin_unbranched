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

namespace WatiN.Core.Mozilla
{
    public abstract class Document
    {
        public string Title
        {
            get
            {
                this.ClientPort.Write(string.Format("{0}.title", FireFoxClientPort.DocumentVariableName));
                return this.ClientPort.LastResponse;
            }
        }

        protected abstract FireFoxClientPort ClientPort { get; }

        /// <summary>
        /// Finds a text field using the Id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>A text field for the specified id</returns>
        public TextField TextField(string id)
        {
            SendGetElementById(id);
            return new TextField(this.ClientPort.LastResponse, this.ClientPort);
        }

        /// <summary>
        /// Finds an element matching the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public Element Element(string id)
        {
            SendGetElementById(id);
            return new Element(this.ClientPort.LastResponse, this.ClientPort);
        }

        protected void SendGetElementById(string id)
        {
            this.ClientPort.Write(string.Format("domDumpFull({0}.getElementById(\"{1}\"));", FireFoxClientPort.DocumentVariableName, id));
        }       
    }
}
