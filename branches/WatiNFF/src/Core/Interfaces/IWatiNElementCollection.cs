using WatiN.Core.Interfaces;

namespace WatiN.Core
{
    public interface IWatiNElementCollection
    {
        /// <summary>
        /// Gets the <see cref="Element"/> at the specified index.
        /// </summary>
        /// <value></value>
        IElement this[int index] { get; }
    }
}