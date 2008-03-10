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

using WatiN.Core;
using WatiN.Core.Interfaces;

namespace WatiN.Core.Mozilla
{
    /// <summary>
    /// The FireFox implementation of <see cref="IForm"/>.
    /// </summary>
    public class Form : Element, IForm
    {
        public Form(string elementVariable, FireFoxClientPort clientPort) : base(elementVariable, clientPort)
        {
        }

        public void Submit()
        {
            this.ClientPort.Write("{0}.submit();", this.ElementVariable);
        }

        public string Name
        {
            get
            {
                return GetProperty("name");
            }
        }
    }
}