using WatiN.Core.Constraints;

namespace WatiN.Core.Interfaces
{
    public interface ITableCollection : IBaseElementCollection
    {
        /// <summary>
        /// Gets the <see cref="ITable"/> at the specified index.
        /// </summary>
        /// <value></value>
        ITable this[int index] { get; }

        ITableCollection Filter(BaseConstraint findBy);
    }
}