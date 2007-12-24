namespace WatiN.Core.Interfaces
{
    public interface IImageCollection : IBaseElementCollection
    {
        /// <summary>
        /// Gets the <see cref="IImage"/> at the specified index.
        /// </summary>
        /// <value></value>
        IImage this[int index] { get; }

        IImageCollection Filter(AttributeConstraint findBy);
    }
}