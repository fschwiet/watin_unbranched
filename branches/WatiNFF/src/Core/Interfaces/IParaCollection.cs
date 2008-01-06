using WatiN.Core.Constraints;

namespace WatiN.Core.Interfaces
{
    public interface IParaCollection : IBaseElementCollection
    {
        /// <summary>
        /// Gets the <see cref="Para"/> at the specified index.
        /// </summary>
        /// <value></value>
        IPara this[int index] { get; }

        IParaCollection Filter(BaseConstraint findBy);
    }
}