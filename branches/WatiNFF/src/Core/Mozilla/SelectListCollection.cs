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

namespace WatiN.Core.Mozilla
{
    /// <summary>
    /// FireFox implementation of <see cref="ISelectListCollection"/>.
    /// </summary>
    public class SelectListCollection : BaseElementCollection, ISelectListCollection
    {
        public SelectListCollection(List<Element> elements, FireFoxClientPort clientPort) : base(elements, clientPort)
        {
        }

        public SelectListCollection(FireFoxClientPort clientPort, ElementFinder elementFinder)
            : base(clientPort, elementFinder)
        {
        }

        protected override List<Element> FindElements()
        {
            List<Element> selectLists = new List<Element>();
            foreach (string selectListVariable in this.ElementFinder.FindAll())
            {
                selectLists.Add(new SelectList(selectListVariable, this.ClientPort));
            }

            return selectLists;
        }

        /// <summary>
        /// Gets the <see cref="ISelectList"/> at the specified index.
        /// </summary>
        /// <value></value>
        public ISelectList this[int index]
        {
            get { return (ISelectList)this.Elements[index]; }
        }

        public ISelectListCollection Filter(AttributeConstraint findBy)
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

            return new SelectListCollection(filteredElements, this.ClientPort);
        }
    }
}