using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatiN.Core.Native.Windows
{
    internal abstract class AssistiveTechnologyObject
    {
        internal abstract string Name { get; }
        internal abstract AccessibleRole Role { get; }
        internal abstract IList<AccessibleState> StateSet { get; }
        internal abstract int ChildCount { get; }
        internal abstract bool SupportsActions { get; }

        internal abstract void DoAction(int actionIndex);
        internal abstract IList<AssistiveTechnologyObject> GetChildrenByRole(AccessibleRole matchingRole, bool visibleChildrenOnly, bool recursiveSearch);
    }
}
