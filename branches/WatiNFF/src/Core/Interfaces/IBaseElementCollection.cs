using System.Collections;
using System.Text.RegularExpressions;
using WatiN.Core.Constraints;

namespace WatiN.Core.Interfaces
{
    public interface IBaseElementCollection : IEnumerable
    {
        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>The length.</value>
        int Length { get; }

        bool Exists(BaseConstraint constraint);

        bool Exists(Regex elementId);

        bool Exists(string elementId);
    }
}