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
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WatiN.Core.Interfaces;
using WatiN.Core.Constraints;

namespace WatiN.Core.Mozilla
{
    /// <summary>
    /// The FireFox implementation of the <see cref="IBaseElementCollection"/>
    /// </summary>
    public abstract class BaseElementCollection : IBaseElementCollection
    {
        #region Private fields

        /// <summary>
        ///  Client port used to communicate with the jssh server
        /// </summary>
        private readonly FireFoxClientPort clientPort;

        /// <summary>
        /// The FireFox element finder
        /// </summary>
        private readonly ElementFinder elementFinder;

        /// <summary>
        /// Collection of FireFox elements
        /// </summary>
        private List<Element> elements;

        #endregion

        #region Constructors

        public BaseElementCollection(FireFoxClientPort clientPort, ElementFinder elementFinder)
        {
            this.clientPort = clientPort;   
            this.elementFinder = elementFinder;
        }

        protected BaseElementCollection(List<Element> elements, FireFoxClientPort clientPort) : this(clientPort, null)
        {
            this.elements = elements;
        }

        #endregion

        #region Public instance properties

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>The length.</value>
        public int Length
        {
            get
            {
                return this.Elements.Count;
            }
        }

        public bool Exists(Regex elementId)
        {
            return Exists(Find.ById(elementId));
        }

        public bool Exists(string elementId)
        {
            return Exists(Find.ById(elementId));
        }

        public bool Exists(BaseConstraint findBy)
        {            
            foreach (Element element in Elements)
            {
                FireFoxElementAttributeBag attributeBag = new FireFoxElementAttributeBag(element.ElementVariable, this.ClientPort);
                if (findBy.Compare(attributeBag))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Protected instance properties

        protected List<Element> Elements
        {
            get
            {
                if (elements == null)
                {
                    if (ElementFinder != null)
                    {
                        elements = this.FindElements();
                    }
                    else
                    {
                        elements = new List<Element>();
                    }
                }

                return elements;
            }
        }

        protected abstract List<Element> FindElements();

        /// <summary>
        /// Gets the client port used to communicate with the jssh server.
        /// </summary>
        /// <value>The client port.</value>
        protected FireFoxClientPort ClientPort
        {
            get
            {
                return this.clientPort;
            }
        }

        /// <summary>
        /// The FireFox element finder
        /// </summary>
        protected ElementFinder ElementFinder
        {
            get { return elementFinder; }
        }

        #endregion

        #region Public instance methods

        ///<summary>
        ///Returns an enumerator that iterates through a collection.
        ///</summary>
        ///
        ///<returns>
        ///An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        public IEnumerator GetEnumerator()
        {
            return Elements.GetEnumerator();
        }

        #endregion

        #region Internal instance methods

        internal ArrayList ConvertToArrayList()
        {
            ArrayList array = new ArrayList(this.Length);
            for (int i = 0; i < this.Length; i++)
            {
                array.Add(this.Elements[i]);
            }
            return array;
        }

        #endregion
    }
}