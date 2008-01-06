using WatiN.Core.Constraints;

namespace WatiN.Core.Interfaces
{
    public interface IDivCollection : IBaseElementCollection
    {
        /// <summary>
        /// Gets the <see cref="Div"/> at the specified index.
        /// </summary>
        /// <value></value>
        IDiv this[int index] { get; }

        IDivCollection Filter(BaseConstraint findBy);
    }
}