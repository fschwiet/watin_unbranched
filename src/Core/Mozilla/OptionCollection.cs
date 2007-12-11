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
using WatiN.Core.Interfaces;

namespace WatiN.Core.Mozilla
{
    /// <summary>
    /// The FireFox implementation of <see cref="IOptionCollection"/>.
    /// </summary>
    public class OptionCollection : BaseElementCollection, IOptionCollection
    {
        private readonly Element parent;

        public OptionCollection(Element parentSelectList, FireFoxClientPort clientPort, ElementFinder elementFinder): base(clientPort, elementFinder)
        {
            this.parent = parentSelectList;
        }

        private OptionCollection(Element parentSelectList, List<Element> elements, FireFoxClientPort port) : base(elements, port)
        {
            this.parent = parentSelectList;
        }

        protected override List<Element> FindElements()
        {
            int optionCount;
            List<Element> elements = new List<Element>();

            if (int.TryParse(this.parent.GetProperty("options.length"), out optionCount))
            {
                for (int i = 0; i < optionCount; i++)
                {
                    Element optionElement = (Element)this.parent.GetElementByProperty(string.Format("options[{0}]", i));
                    elements.Add(new Option(optionElement.ElementVariable, this.ClientPort));
                }
            }

            return elements;
        }

        /// <summary>
        /// Gets the <see cref="Span"/> at the specified index.
        /// </summary>
        /// <value></value>
        public IOption this[int index]
        {
            get { return (IOption) this.Elements[index]; }
        }

        public IOptionCollection Filter(AttributeConstraint constraint)
        {
            List<Element> filteredElements =  new List<Element>();            
            foreach (Element element in this.Elements)
            {
                FireFoxElementAttributeBag attributeBag = new FireFoxElementAttributeBag(element.ElementVariable, this.ClientPort);

                if (constraint.Compare(attributeBag))
                {
                    filteredElements.Add(element);
                }
            }

            return new OptionCollection(this.parent, filteredElements, this.ClientPort);
            
        }
    }
}