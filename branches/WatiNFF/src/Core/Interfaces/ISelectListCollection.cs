using WatiN.Core.Constraints;

namespace WatiN.Core.Interfaces
{
    public interface ISelectListCollection : IBaseElementCollection
    {
        /// <summary>
        /// Gets the <see cref="SelectList"/> at the specified index.
        /// </summary>
        /// <value></value>
        ISelectList this[int index] { get; }

        ISelectListCollection Filter(BaseConstraint findBy);
    }
}