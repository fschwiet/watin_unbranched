using WatiN.Core.Constraints;

namespace WatiN.Core.Interfaces
{
    public interface ICheckBoxCollection : IBaseElementCollection
    {
        /// <summary>
        /// Gets the <see cref="CheckBox"/> at the specified index.
        /// </summary>
        /// <value></value>
        ICheckBox this[int index] { get; }

        ICheckBoxCollection Filter(BaseConstraint findBy);
    }
}