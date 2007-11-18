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
using WatiN.Core.Interfaces;

namespace WatiN.Core.Mozilla
{
    /// <summary>
    /// The FireFox implementation of an HTML button
    /// </summary>
    public class Button : Element, IButton
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        /// <param name="elementVariable">The javascript variable name referencing this element.</param>
        /// <param name="clientPort">The client port.</param>
        public Button(string elementVariable, FireFoxClientPort clientPort)
            : base(elementVariable, clientPort)
        {
        }

        #endregion

        #region Public instance properties

        public string Value
        {
            get
            {
                return GetAttributeValue("value");
            }
        }

        public override string Text
        {
            get
            {
                return this.Value;
            }
        }

        public override string ToString()
        {
            return this.Value;
        }

        #endregion

    }
}