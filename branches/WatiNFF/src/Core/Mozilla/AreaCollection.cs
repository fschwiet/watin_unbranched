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

using System.Collections.Generic;
using WatiN.Core;
using WatiN.Core.Interfaces;
using WatiN.Core.Constraints;

namespace WatiN.Core.Mozilla
{
    /// <summary>
    /// FireFox implementation of the <see cref="IAreaCollection"/> interface.
    /// </summary>
    public class AreaCollection : BaseElementCollection, IAreaCollection
    {
        public AreaCollection(FireFoxClientPort clientPort, ElementFinder elementFinder)
            : base(clientPort, elementFinder)
        {
        }

        public AreaCollection(List<Element> elements, FireFoxClientPort clientPort) 
            : base(elements, clientPort)
        {            
        }

        protected override List<Element> FindElements()
        {
            List<Element> areas = new List<Element>();
            foreach (string areaVariable in this.ElementFinder.FindAll())
            {
                areas.Add(new Area(areaVariable, this.ClientPort));
            }

            return areas;
        }

        /// <summary>
        /// Returns a new <see cref="Core.AreaCollection" /> filtered by the <see cref="BaseConstraint" />.
        /// </summary>
        /// <param name="findBy">The attribute to filter by.</param>
        /// <returns>The filtered collection.</returns>
        public IAreaCollection Filter(BaseConstraint findBy)
        {
            List<Element> filteredElements = new List<Element>();

            foreach (Element element in this.Elements)
            {
                FireFoxElementAttributeBag attributeBag = new FireFoxElementAttributeBag(element.ElementVariable, this.ClientPort);
                if (findBy.Compare(attributeBag))
                {
                    filteredElements.Add(element);
                }
            }

            return new AreaCollection(filteredElements, this.ClientPort);
        }

        /// <summary>
        /// Gets the <see cref="Core.Area" /> at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The area.</returns>
        public IArea this[int index]
        {
            get
            {
                return (IArea) this.Elements[index];
            }
        }
    }
}