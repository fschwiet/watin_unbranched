using System.Collections;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using WatiN.Core.Constraints;

namespace WatiN.Core.Interfaces
{
    public interface ISelectList : IElement
    {
        /// <summary>
        /// This method clears the selected items in the select box and waits for the 
        /// onchange event to complete after the list is cleared
        /// </summary>
        void ClearList();

        /// <summary>
        /// Gets a value indicating whether this <see cref="SelectList"/> allows multiple select.
        /// </summary>
        /// <value><c>true</c> if multiple; otherwise, <c>false</c>.</value>
        bool Multiple { get; }

        /// <summary>
        /// This method selects an item by text.
        /// Raises NoValueFoundException if the specified value is not found.
        /// </summary>
        /// <param name="text">The text.</param>
        void Select(string text);

        /// <summary>
        /// This method selects an item by text using the supplied regular expression.
        /// Raises NoValueFoundException if the specified value is not found.
        /// </summary>
        /// <param name="regex">The regex.</param>
        void Select(Regex regex);

        /// <summary>
        /// Selects an item in a select box, by value.
        /// Raises NoValueFoundException if the specified value is not found.
        /// </summary>
        /// <param name="value">The value.</param>
        void SelectByValue(string value);

        /// <summary>
        /// Selects an item in a select box by value using the supplied regular expression.
        /// Raises NoValueFoundException if the specified value is not found.
        /// </summary>
        /// <param name="regex">The regex.</param>
        void SelectByValue(Regex regex);

        /// <summary>
        /// Returns all the items in the select list as an array.
        /// An empty array is returned if the select box has no contents.
        /// </summary>
        StringCollection AllContents { get; }

        /// <summary>
        /// Returns the <see cref="Options" /> which matches the specified <paramref name="text"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns><see cref="Options" /></returns>
        IOption Option(string text);

        /// <summary>
        /// Returns the <see cref="Options" /> which matches the specified <paramref name="text"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns><see cref="Options" /></returns>
        IOption Option(Regex text);

        /// <summary>
        /// Returns the <see cref="Options" /> which matches the specified <paramref name="findBy"/>.
        /// </summary>
        /// <param name="findBy">The find by to use.</param>
        /// <returns></returns>
        IOption Option(BaseConstraint findBy);

        /// <summary>
        /// Returns all the <see cref="Core.Option"/> elements in the <see cref="SelectList"/>.
        /// </summary>
        IOptionCollection Options { get; }

        /// <summary>
        /// Returns the selected option(s) in an array list.
        /// </summary>
        ArrayList SelectedOptions { get; }

        /// <summary>
        /// Returns the selected item(s) in a <see cref="StringCollection"/>.
        /// </summary>
        StringCollection SelectedItems { get; }

        /// <summary>
        /// Returns the first selected item in the selectlist. Other items may be selected.
        /// Use SelectedItems to get a StringCollection of all selected items.
        /// When there's no item selected, the return value will be null.
        /// </summary>
        string SelectedItem { get; }

        /// <summary>
        /// Returns the first selected option in the selectlist. Other options may be selected.
        /// Use SelectedOptions to get an ArrayList of all selected options.
        /// When there's no option selected, the return value will be null.
        /// </summary>
        IOption SelectedOption { get; }

        /// <summary>
        /// Gets a value indicating whether this instance has selected items.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has selected items; otherwise, <c>false</c>.
        /// </value>
        bool HasSelectedItems { get; }
    }
}