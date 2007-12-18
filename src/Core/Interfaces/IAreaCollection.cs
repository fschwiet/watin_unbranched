namespace WatiN.Core.Interfaces
{
    public interface IAreaCollection : IBaseElementCollection
    {
        /// <summary>
        /// Returns a new <see cref="AreaCollection" /> filtered by the <see cref="AttributeConstraint" />.
        /// </summary>
        /// <param name="findBy">The attribute to filter by.</param>
        /// <returns>The filtered collection.</returns>
        IAreaCollection Filter(AttributeConstraint findBy);

        /// <summary>
        /// Gets the <see cref="Area" /> at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The area.</returns>
        IArea this[int index] { get; }
    }
}