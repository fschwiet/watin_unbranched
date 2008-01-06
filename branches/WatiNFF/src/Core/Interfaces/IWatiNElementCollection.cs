using WatiN.Core.Constraints;

namespace WatiN.Core.Interfaces
{
    public interface IWatiNElementCollection : IBaseElementCollection
    {
        /// <summary>
        /// Returns a new <see cref="IWatiNElementCollection" /> filtered by the <see cref="BaseConstraint" />.
        /// </summary>
        /// <param name="findBy">The attribute to filter by.</param>
        /// <returns>The filtered collection.</returns>
        IWatiNElementCollection Filter(BaseConstraint findBy);

        /// <summary>
        /// Gets the <see cref="Element"/> at the specified index.
        /// </summary>
        /// <value></value>
        IElement this[int index] { get; }
    }
}