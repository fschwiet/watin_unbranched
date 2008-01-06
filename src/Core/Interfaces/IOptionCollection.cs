using WatiN.Core.Constraints;

namespace WatiN.Core.Interfaces
{
    public interface IOptionCollection : IBaseElementCollection
    {
        /// <summary>
        /// Gets the <see cref="Span"/> at the specified index.
        /// </summary>
        /// <value></value>
        IOption this[int index] { get; }

        IOptionCollection Filter(BaseConstraint constraint);        
    }
}