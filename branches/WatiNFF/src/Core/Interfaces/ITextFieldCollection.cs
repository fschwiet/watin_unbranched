using WatiN.Core.Constraints;

namespace WatiN.Core.Interfaces
{
    public interface ITextFieldCollection : IBaseElementCollection
    {
        /// <summary>
        /// Gets the <see cref="TextField"/> at the specified index.
        /// </summary>
        /// <value></value>
        ITextField this[int index] { get; }

        ITextFieldCollection Filter(BaseConstraint findBy);
    }
}