

namespace WatiN.Core.Interfaces
{
    public interface IWatiNElementCollection : IBaseElementCollection
    {
        /// <summary>
        /// Returns a new <see cref="IWatiNElementCollection" /> filtered by the <see cref="AttributeConstraint" />.
        /// </summary>
        /// <param name="findBy">The attribute to filter by.</param>
        /// <returns>The filtered collection.</returns>
        IWatiNElementCollection Filter(AttributeConstraint findBy);

        /// <summary>
        /// Gets the <see cref="Element"/> at the specified index.
        /// </summary>
        /// <value></value>
        IElement this[int index] { get; }
    }
}