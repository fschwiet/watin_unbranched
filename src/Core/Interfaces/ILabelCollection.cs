namespace WatiN.Core.Interfaces
{
    public interface ILabelCollection : IBaseElementCollection
    {
        /// <summary>
        /// Gets the <see cref="ILabel"/> at the specified index.
        /// </summary>
        /// <value></value>
        ILabel this[int index] { get; }

        ILabelCollection Filter(AttributeConstraint findBy);
    }
}