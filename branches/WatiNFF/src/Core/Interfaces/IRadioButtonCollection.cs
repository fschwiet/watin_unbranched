using WatiN.Core.Constraints;

namespace WatiN.Core.Interfaces
{
    public interface IRadioButtonCollection : IBaseElementCollection
    {
        /// <summary>
        /// Gets the <see cref="IRadioButton"/> at the specified index.
        /// </summary>
        /// <value></value>
        IRadioButton this[int index] { get; }

        IRadioButtonCollection Filter(BaseConstraint findBy);

    }
}