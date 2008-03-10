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

using System.Collections.Generic;
using WatiN.Core;
using WatiN.Core.Interfaces;
using WatiN.Core.Constraints;

namespace WatiN.Core.Mozilla
{
    /// <summary>
    /// Represents a collection of WatiN elements
    /// </summary>
    public class WatiNElementCollection : BaseElementCollection, IWatiNElementCollection
    {
        public WatiNElementCollection(FireFoxClientPort clientPort, ElementFinder elementFinder)
            : base(clientPort, elementFinder)
        {
        }

        protected override List<Element> FindElements()
        {
            List<string> elementVars = this.ElementFinder.FindAll();
            List<Element> elements = new List<Element>();
            
            foreach (string elementVar in elementVars)
            {
                elements.Add(new Element(elementVar, this.ClientPort));
            }

            return elements;
        }

        /// <summary>
        /// Returns a new <see cref="IWatiNElementCollection" /> filtered by the <see cref="BaseConstraint" />.
        /// </summary>
        /// <param name="findBy">The attribute to filter by.</param>
        /// <returns>The filtered collection.</returns>
        public IWatiNElementCollection Filter(BaseConstraint findBy)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets the <see cref="Core.Element"/> at the specified index.
        /// </summary>
        /// <value></value>
        public IElement this[int index]
        {
            get 
            {
                return this.Elements[index];
            }
        }
    }
}