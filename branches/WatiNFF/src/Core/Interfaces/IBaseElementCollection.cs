using System.Collections;
using System.Text.RegularExpressions;

namespace WatiN.Core.Interfaces
{
    public interface IBaseElementCollection : IEnumerable
    {
        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>The length.</value>
        int Length { get; }

        bool Exists(AttributeConstraint constraint);

        bool Exists(Regex elementId);

        bool Exists(string elementId);
    }
}