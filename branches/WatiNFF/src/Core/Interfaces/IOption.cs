namespace WatiN.Core.Interfaces
{
    public interface IOption : IElement
    {
        /// <summary>
        /// Returns the value.
        /// </summary>
        /// <value>The value.</value>
        string Value { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Option"/> is selected.
        /// </summary>
        /// <value><c>true</c> if selected; otherwise, <c>false</c>.</value>
        bool Selected { get; }

        /// <summary>
        /// Returns the index of this <see cref="Option"/> in the <see cref="SelectList"/>.
        /// </summary>
        /// <value>The index.</value>
        int Index { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Option"/> is selected by default.
        /// </summary>
        /// <value><c>true</c> if selected by default; otherwise, <c>false</c>.</value>
        bool DefaultSelected { get; }

        /// <summary>
        /// De-selects this option in the selectlist (if selected),
        /// fires the "onchange" event on the selectlist and waits for it
        /// to complete.
        /// </summary>
        void Clear();

        /// <summary>
        /// Selects this option in the selectlist (if not selected),
        /// fires the "onchange" event on the selectlist and waits for it
        /// to complete.
        /// </summary>
        void Select();

        /// <summary>
        /// Gets the parent <see cref="SelectList"/>.
        /// </summary>
        /// <value>The parent <see cref="SelectList"/>.</value>
        ISelectList ParentSelectList { get; }

        void SelectNoWait();
    }
}