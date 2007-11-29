using System.Collections;

namespace WatiN.Core.Interfaces
{
    public interface IBaseElementCollection : IEnumerable
    {
        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>The length.</value>
        int Length { get; }
    }
}