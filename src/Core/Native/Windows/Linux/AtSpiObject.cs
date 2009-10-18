using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using WatiN.Core.Native.Windows;
    
namespace WatiN.Core.Native.Windows.Linux
{
    internal class AtSpiObject : AssistiveTechnologyObject
    {
        #region Private members
        private IntPtr _accessibleObject = IntPtr.Zero;
        #endregion

        #region Constructors
        internal AtSpiObject(int processId, Rectangle windowRect, WindowManagerFrameExtents frameExtents)
        {
            Process applicationProcess = Process.GetProcessById(processId);
            string moduleName = applicationProcess.MainModule.ModuleName;
            _accessibleObject = AtSpi.Instance.MatchTopLevelWindowForApplication(moduleName, windowRect, frameExtents);
        }

        internal AtSpiObject(IntPtr accessibleObjectPtr)
        {
            _accessibleObject = accessibleObjectPtr;
        }
        #endregion

        #region Overridden properties and methods
        internal override int ChildCount
        {
            get { return AtSpi.Instance.GetChildCount(_accessibleObject); }
        }

        internal override string Name
        {
            get { return AtSpi.Instance.GetName(_accessibleObject); }
        }

        internal override AccessibleRole Role
        {
            get { return AtSpi.Instance.GetRole(_accessibleObject); }
        }

        internal override IList<AccessibleState> StateSet
        {
            get { return AtSpi.Instance.GetStateList(_accessibleObject); }
        }

        internal override bool SupportsActions
        {
            get { return AtSpi.Instance.IsActionObject(_accessibleObject); }
        }

        internal override IList<AssistiveTechnologyObject> GetChildrenByRole(AccessibleRole matchingRole, bool visibleChildrenOnly, bool recursiveSearch)
        {
            List<AssistiveTechnologyObject> childObjectList = new List<AssistiveTechnologyObject>();
            IList<IntPtr> childObjectPointerList = AtSpi.Instance.FindChildrenWithRole(_accessibleObject, matchingRole, visibleChildrenOnly, recursiveSearch);
            foreach(IntPtr childObjectPointer in childObjectPointerList)
            {
                childObjectList.Add(new AtSpiObject(childObjectPointer));
            }
            return childObjectList;
        }

        internal override void DoAction(int actionIndex)
        {
            AtSpi.Instance.PerformAction(_accessibleObject, actionIndex);
        }
        #endregion
        
        //Should these be promoted to the base AssistiveTechnologyObject class?
        internal bool SupportsText
        {
            get { return AtSpi.Instance.IsTextObject(_accessibleObject); }
        }
        
        internal string Text
        {
            get
            {
                string textValue = string.Empty;
                if (SupportsText)
                {
                    textValue = AtSpi.Instance.GetText(_accessibleObject);
                }
                else
                {
                    textValue = Name;
                }
                return textValue;
            }
        }

        #region Public properties
        public bool IsValid
        {
            get { return Role != AccessibleRole.Invalid; }
        }
        #endregion

        #region Public methods
        public bool SetFocus()
        {
            return AtSpi.Instance.SetFocus(_accessibleObject);
        }

        public void SetText(string text)
        {
            AtSpi.Instance.SetText(_accessibleObject, text);
        }

        public void UnreferenceObject()
        {
            AtSpi.Instance.UnreferenceAccessibleObject(_accessibleObject);
        }
        #endregion
    }
}
