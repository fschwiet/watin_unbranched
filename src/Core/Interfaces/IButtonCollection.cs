using WatiN.Core.Constraints;

namespace WatiN.Core.Interfaces
{
    public interface IButtonCollection : IBaseElementCollection
    {
        /// <summary>
        /// Gets the <see cref="Button"/> at the specified index.
        /// </summary>
        /// <value></value>
        IButton this[int index] { get; }

        IButtonCollection Filter(BaseConstraint findBy);
    }
}