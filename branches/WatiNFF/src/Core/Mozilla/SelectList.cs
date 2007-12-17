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
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using WatiN.Core;
using WatiN.Core.Comparers;
using WatiN.Core.Exceptions;
using WatiN.Core.Interfaces;

namespace WatiN.Core.Mozilla
{
    /// <summary>
    /// FireFox implementation of the <see cref="ISelectList"/> interface.
    /// </summary>
    public class SelectList : Element, ISelectList
    {
        public SelectList(string elementVariable, FireFoxClientPort clientPort) : base(elementVariable, clientPort)
        {
        }

        
        #region Public instance methods

        /// <summary>
        /// This method clears the selected items in the select box and waits for the 
        /// onchange event to complete after the list is cleared
        /// </summary>
        public void ClearList()
        {
            ArrayList selectedOptions = this.SelectedOptions;

            foreach (Option option in selectedOptions)
            {
                option.Clear();
            }
        }

        /// <summary>
        /// Returns the <see cref="ISelectList.Options" /> which matches the specified <paramref name="text"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns><see cref="ISelectList.Options" /></returns>
        public IOption Option(string text)
        {
            return this.Option(Find.ByText(new StringEqualsAndCaseInsensitiveComparer(text)));
        }

        /// <summary>
        /// Returns the <see cref="ISelectList.Options" /> which matches the specified <paramref name="text"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns><see cref="ISelectList.Options" /></returns>
        public IOption Option(Regex text)
        {
            return this.Option(Find.ByText(text));
        }

        /// <summary>
        /// Returns the <see cref="ISelectList.Options" /> which matches the specified <paramref name="findBy"/>.
        /// </summary>
        /// <param name="findBy">The find by to use.</param>
        /// <returns></returns>
        public IOption Option(AttributeConstraint findBy)
        {
            IOptionCollection filteredOptions = this.Options.Filter(findBy);

            if (filteredOptions.Length > 0)
            {
                return filteredOptions[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// This method selects an item by text.
        /// Raises NoValueFoundException if the specified value is not found.
        /// </summary>
        /// <param name="text">The text.</param>
        public void Select(string text)
        {
            this.FindOption(Find.ByText(text)).Select();
        }

        /// <summary>
        /// This method selects an item by text using the supplied regular expression.
        /// Raises NoValueFoundException if the specified value is not found.
        /// </summary>
        /// <param name="regex">The regex.</param>
        public void Select(Regex regex)
        {
            this.FindOption(Find.ByText(regex)).Select();
        }

        /// <summary>
        /// Selects an item in a select box, by value.
        /// Raises NoValueFoundException if the specified value is not found.
        /// </summary>
        /// <param name="value">The value.</param>
        public void SelectByValue(string value)
        {
            this.FindOption(Find.ByValue(value)).Select();
        }

        /// <summary>
        /// Selects an item in a select box by value using the supplied regular expression.
        /// Raises NoValueFoundException if the specified value is not found.
        /// </summary>
        /// <param name="regex">The regex.</param>
        public void SelectByValue(Regex regex)
        {
            this.FindOption(Find.ByValue(regex)).Select();
        }

        #endregion

        #region Public instance properties

        /// <summary>
        /// Returns all the items in the select list as an array.
        /// An empty array is returned if the select box has no contents.
        /// </summary>
        public StringCollection AllContents
        {
            get
            {
                StringCollection allContents = new StringCollection();
                IOptionCollection options = this.Options;

                foreach (IOption option in options)
                {
                    allContents.Add(option.Text);
                }

                return allContents;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Core.SelectList"/> allows multiple select.
        /// </summary>
        /// <value><c>true</c> if multiple; otherwise, <c>false</c>.</value>
        public bool Multiple
        {
            get
            {
                bool multiple;
                Boolean.TryParse(this.GetProperty("multiple"), out multiple);

                return multiple;
            }
        }

        /// <summary>
        /// Returns all the <see cref="Core.Option"/> elements in the <see cref="Core.SelectList"/>.
        /// </summary>
        public IOptionCollection Options
        {
            get
            {
                return new OptionCollection(this, this.ClientPort, new ElementFinder(this, "option", null, this.ClientPort));
            }
        }

        /// <summary>
        /// Returns the selected option(s) in an array list.
        /// </summary>
        public ArrayList SelectedOptions
        {
            get
            {
                OptionCollection selectedOptions = (OptionCollection) this.Options.Filter(GetIsSelectedAttribute());
                return selectedOptions.ConvertToArrayList();
            }
        }

        /// <summary>
        /// Returns the selected item(s) in a <see cref="StringCollection"/>.
        /// </summary>
        public StringCollection SelectedItems
        {
            get
            {
                ArrayList selectedOptions = this.SelectedOptions;
                StringCollection selectedItems = new StringCollection();

                foreach (Option option in selectedOptions)
                {
                    selectedItems.Add(option.Text);
                }

                return selectedItems;
            }
        }

        /// <summary>
        /// Returns the first selected item in the selectlist. Other items may be selected.
        /// Use SelectedItems to get a StringCollection of all selected items.
        /// When there's no item selected, the return value will be null.
        /// </summary>
        public string SelectedItem
        {
            get 
            {
                string selectedItem = null;
                Option selectedOption = (Option) this.SelectedOption;
                
                if (selectedOption != null)
                {
                    selectedItem = selectedOption.Text;
                }

                return selectedItem;
            }
        }

        /// <summary>
        /// Returns the first selected option in the selectlist. Other options may be selected.
        /// Use SelectedOptions to get an ArrayList of all selected options.
        /// When there's no option selected, the return value will be null.
        /// </summary>
        public IOption SelectedOption
        {
            get 
            {
                IOption option = Option(GetIsSelectedAttribute());

                if (option != null && option.Exists)
                {
                    return option;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has selected items.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has selected items; otherwise, <c>false</c>.
        /// </value>
        public bool HasSelectedItems
        {
            get 
            {
                return this.SelectedOptions.Count > 0;     
            }
        }

        #endregion

        #region Private static methods

        private static AttributeConstraint GetIsSelectedAttribute()
        {
            return new AttributeConstraint("selected", true.ToString().ToLower());
        }

        #endregion

        #region Private instance methods

        /// <summary>
        /// Finds an option using the specified <paramref name="constraint"/>.
        /// If the option can not be found using the constraint throws a <see cref="SelectListItemNotFoundException"/>.
        /// </summary>
        /// <param name="constraint">Constraint used to locate the option.</param>
        /// <returns>Option that matches the specified <paramref name="constraint"/>.</returns>
        /// <exception cref="SelectListItemNotFoundException">if the option can not be located using the specified <paramref name="constraint"/>.</exception>
        private IOption FindOption(AttributeConstraint constraint)
        {
            IOption option;
            option = this.Option(constraint);
            if (option == null)
            {
                throw new SelectListItemNotFoundException(constraint.Value);   
            }

            return option;
        }

        #endregion

    }
}