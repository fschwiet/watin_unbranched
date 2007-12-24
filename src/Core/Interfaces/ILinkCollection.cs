namespace WatiN.Core.Interfaces
{
    public interface ILinkCollection : IBaseElementCollection
    {
        /// <summary>
        /// Gets the <see cref="ILink"/> at the specified index.
        /// </summary>
        /// <value></value>
        ILink this[int index] { get; }

        ILinkCollection Filter(AttributeConstraint findBy);
    }
}