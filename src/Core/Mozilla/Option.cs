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
using WatiN.Core;
using WatiN.Core.Interfaces;

namespace WatiN.Core.Mozilla
{
    /// <summary>
    /// FireFox implementation of the <see cref="IOption"/> interface.
    /// </summary>
    public class Option : Element, IOption
    {
        public Option(string elementVariable, FireFoxClientPort clientPort) : base(elementVariable, clientPort)
        {
        }

        /// <summary>
        /// Returns the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value
        {
            get
            {
                return this.GetProperty("value");
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Core.Option"/> is selected.
        /// </summary>
        /// <value><c>true</c> if selected; otherwise, <c>false</c>.</value>
        public bool Selected
        {
            get
            {
                this.ClientPort.Write("{0}.selected;", this.ElementVariable);
                return this.ClientPort.LastResponseAsBool;
            }
        }

        /// <summary>
        /// Returns the index of this <see cref="Core.Option"/> in the <see cref="Core.SelectList"/>.
        /// </summary>
        /// <value>The index.</value>
        public int Index
        {
            get
            {
                return Convert.ToInt32(this.GetProperty("index"));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Core.Option"/> is selected by default.
        /// </summary>
        /// <value><c>true</c> if selected by default; otherwise, <c>false</c>.</value>
        public bool DefaultSelected
        {
            get { return Convert.ToBoolean(this.GetProperty("defaultSelected")); }
        }

        /// <summary>
        /// De-selects this option in the selectlist (if selected),
        /// fires the "onchange" event on the selectlist and waits for it
        /// to complete.
        /// </summary>
        public void Clear()
        {
            this.ClientPort.Write("{0}.selected=false;", this.ElementVariable);
            
            // TODO Is it necessary to fire the onchange event for FF?
            //this.FireEvent("onChange");
        }

        /// <summary>
        /// Selects this option in the selectlist (if not selected),
        /// fires the "onchange" event on the selectlist and waits for it
        /// to complete.
        /// </summary>
        public void Select()
        {
            this.ClientPort.Write("{0}.selected=true;", this.ElementVariable);
            
            // TODO Is it necessary to fire the onchange event for FF?
            //this.FireEvent("onChange");
        }

        /// <summary>
        /// Gets the parent <see cref="Core.SelectList"/>.
        /// </summary>
        /// <value>The parent <see cref="Core.SelectList"/>.</value>
        public ISelectList ParentSelectList
        {
            get
            {
                return new SelectList(((Element) this.Parent).ElementVariable, this.ClientPort);
            }
        }

        public void SelectNoWait()
        {
            throw new System.NotImplementedException();
        }
    }
}