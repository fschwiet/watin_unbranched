
namespace WatiN.Core.Interfaces
{
    public interface ISpanCollection : IBaseElementCollection
    {
        /// <summary>
        /// Gets the <see cref="Span"/> at the specified index.
        /// </summary>
        /// <value></value>
        ISpan this[int index] { get; }

        ISpanCollection Filter(AttributeConstraint findBy);
    }
}