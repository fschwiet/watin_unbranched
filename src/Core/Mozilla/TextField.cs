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
using System.Collections.Generic;
using System.Text;
using WatiN.Core.Interfaces;

namespace WatiN.Core.Mozilla
{
    public class TextField : Element, ITextField
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextField"/> class.
        /// </summary>
        /// <param name="elementVariable">The elementVariable.</param>
        /// <param name="clientPort">The client port.</param>
        public TextField(string elementVariable, FireFoxClientPort clientPort) : base(elementVariable, clientPort)
        {
        	if (elementVariable == null)
        	{
        		throw new ArgumentNullException("elementVariable");
        	}
        }        

        public string Value
        {
            get
            {
                return GetAttributeValue("value");
            }
            set
            {
                SetAttributeValue("value", value);
            }
        }
    }
}
