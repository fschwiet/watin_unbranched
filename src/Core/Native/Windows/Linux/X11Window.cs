using System;
using System.Collections.Generic;
using WatiN.Core.Native.Windows;

namespace WatiN.Core.Native.Windows.Linux
{
    internal class X11Window : Window
    {
        private int _processId = 0;
        private int _itemId = 0;
        private IntPtr _handle = IntPtr.Zero;
        private IntPtr _parentHandle = IntPtr.Zero;
        private AtSpiObject _accessibleObject = null;
        
        internal X11Window(int processId)
        {
            _processId = processId;
            //XQueryTree returns windows in Z-order, with topmost windows at
            //the end. GetProcessTopLevelWindows uses XQueryTree to build the
            //list, so the topmost window should be the one at the end of the
            //list.
            IList<IntPtr> windowList = X11WindowsNativeMethods.FindTopLevelWindowsForProcess(processId);
            if (windowList.Count > 0)
            {
                _handle = windowList[windowList.Count - 1];
            }
            ChildEnumerationMethod = WindowEnumerationMethod.AssistiveTechnologyApi;
        }
        
        internal X11Window(int processId, IntPtr parentWindowHandle, int itemId, AtSpiObject accessibleObject)
        {
            _processId = processId;
            _parentHandle = parentWindowHandle;
            _itemId = itemId;
            _accessibleObject = accessibleObject;
            _enumerationMethod = WindowEnumerationMethod.AssistiveTechnologyApi;
        }

        internal X11Window(IntPtr handle)
        {
            _handle = handle;
            _processId = X11WindowsNativeMethods.GetProcessIdForWindow(handle);
            ChildEnumerationMethod = WindowEnumerationMethod.AssistiveTechnologyApi;
        }

        public static IList<Window> GetAllTopLevelWindows()
        {
            List<Window> allWindowList = new List<Window>();
            IList<IntPtr> windowPointerList = X11WindowsNativeMethods.FindTopLevelWindowsForProcess(X11WindowsNativeMethods.AllProcesses);
            foreach (IntPtr windowPointer in windowPointerList)
            {
                allWindowList.Add(new X11Window(windowPointer));
            }
            return allWindowList;
        }

        public override IntPtr Handle
        {
            get { return _handle; }
        }

        public override IntPtr ParentHandle
        {
            get { return _parentHandle; }
        }

        public override IntPtr OwnerHandle
        {
            get { return ParentHandle; }
        }

        public override string ClassName
        {
            get
            {
                string windowClass = string.Empty;
                if (Exists)
                {
                    if (EnumerationMethod == WindowEnumerationMethod.WindowManagementApi)
                        windowClass = X11WindowsNativeMethods.GetWindowClass(_handle);
                    else
                        windowClass = AccessibleObject.Role.ToString();
                }
                return windowClass;
            }
        }

        public override string Text
        {
            get
            { 
                string windowText = string.Empty;
                if (Exists)
                {
                    if (EnumerationMethod == WindowEnumerationMethod.WindowManagementApi)
                        windowText = X11WindowsNativeMethods.GetWindowText(_handle);
                    else
                    {
                        AtSpiObject nativeAccObj = AccessibleObject as AtSpiObject;
                        if (nativeAccObj != null)
                        {
                            if (nativeAccObj.SupportsText)
                                windowText = nativeAccObj.Text;
                            else
                                windowText = nativeAccObj.Name;
                        }
                    }
                }
                return windowText;
            }
        }

        public override bool Visible
        {
            get
            {
                bool windowIsVisible = false;
                if (Exists)
                {
                    if (EnumerationMethod == WindowEnumerationMethod.WindowManagementApi)
                        windowIsVisible = X11WindowsNativeMethods.IsWindowViewable(_handle);
                    else
                        windowIsVisible = AccessibleObject.StateSet.Contains(AccessibleState.Visible);
                }
                return windowIsVisible;
            }
        }

        public override bool Enabled
        {
            get
            { 
                bool windowIsEnabled = false;
                if (Exists)
                {
                    if (EnumerationMethod == WindowEnumerationMethod.WindowManagementApi)
                        windowIsEnabled = true;
                    else
                        windowIsEnabled = AccessibleObject.StateSet.Contains(AccessibleState.Enabled);
                }
                return windowIsEnabled;
            }
        }

        public override bool Exists
        {
            get
            {
                bool windowExists = false;
                if (EnumerationMethod == WindowEnumerationMethod.WindowManagementApi)
                    windowExists = X11WindowsNativeMethods.IsWindowValid(_handle);
                else
                {
                    AtSpiObject exisitingAccessible = AccessibleObject as AtSpiObject;
                    if (exisitingAccessible != null)
                        windowExists = exisitingAccessible.IsValid;
                }
                return windowExists;
            }
        }

        public override bool IsTopLevelWindow
        {
            get { return _handle != IntPtr.Zero; }
        }

        public override bool IsDialog
        {
            get
            {
                bool windowIsDialog = false;
                if (Exists && IsTopLevelWindow)
                    windowIsDialog = X11WindowsNativeMethods.IsDialogWindow(_handle);
                return windowIsDialog; 
            }
        }

        public override WindowEnumerationMethod ChildEnumerationMethod
        {
            get
            {
                return base.ChildEnumerationMethod;
            }
            set
            {
                base.ChildEnumerationMethod = WindowEnumerationMethod.AssistiveTechnologyApi;
            }
        }

        public override Int64 Style
        {
            get { return 0L; }
        }

        public override string StyleDescriptor
        {
            get { return Style.ToString("X"); }
        }

        public override int ProcessId
        {
            get { return _processId; }
        }

        public override WindowShowStyle WindowStyle
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override int ItemId
        {
            get { return _itemId; }
        }

        internal override AssistiveTechnologyObject AccessibleObject
        {
            get
            {
                if (_accessibleObject == null && _handle != IntPtr.Zero)
                {
                    //The window returned may or may not include the window manager
                    //frame. Get the window frame extents to pass for window matching.
                    WindowManagerFrameExtents extents = X11WindowsNativeMethods.GetFrameExtents(_handle);
                    _accessibleObject = new AtSpiObject(_processId, X11WindowsNativeMethods.GetWindowRectangle(_handle), extents);
                }
                return _accessibleObject;
            }
        }

        public override bool IsPressable
        {
            get
            {
                string pushButtonClass = WindowFactory.GetWindowClassForRole(AccessibleRole.PushButton, EnumerationMethod == WindowEnumerationMethod.WindowManagementApi);
                bool isClickableObject = false;
                isClickableObject = (!IsTopLevelWindow && ClassName == pushButtonClass);
                return isClickableObject;
            }
        }

        public override void Press()
        {
            if (IsPressable && AccessibleObject != null && AccessibleObject.SupportsActions)
                AccessibleObject.DoAction(0);
        }

        public override bool SetFocus()
        {
            bool focusWasSet = false;
            if (Exists)
            {
                if (IsTopLevelWindow)
                {
                    X11WindowsNativeMethods.ActivateWindow(_handle);
                    focusWasSet = true;
                }
                else
                {
                    AtSpiObject exisitingAccessible = AccessibleObject as AtSpiObject;
                    if (exisitingAccessible != null)
                        focusWasSet = exisitingAccessible.SetFocus();
                }
            }
            return focusWasSet;
        }

        public override IList<Window> GetChildWindows(WindowCriteriaConstraint constraint)
        {
            List<Window> childWindowList = new List<Window>();
            IList<AssistiveTechnologyObject> childObjectList = AccessibleObject.GetChildrenByRole(AccessibleRole.AnyRole, true, true);
            foreach (AssistiveTechnologyObject childObject in childObjectList)
            {
                int itemIndex = childObjectList.IndexOf(childObject);
                Window candidateWindow = WindowFromAccessibleObject(itemIndex, (AtSpiObject)childObject);
                if (constraint(candidateWindow))
                {
                    childWindowList.Add(candidateWindow);
                }
                else
                {
                    candidateWindow.Dispose();
                }
            }
            return childWindowList;
        }

        public override void ForceClose()
        {
            if (IsTopLevelWindow)
                X11WindowsNativeMethods.CloseWindow(_handle);
            else
                X11WindowsNativeMethods.CloseWindow(_parentHandle);
       }

        public override void SendKeystrokes(string keystrokes)
        {
            if (!IsTopLevelWindow)
                _accessibleObject.SetText(keystrokes);
        }

        public override bool IsDialogWindowFor(Window ownerWindow)
        {
            bool windowIsDialogForCandidate = false;
            IntPtr owner = X11WindowsNativeMethods.GetTransientForWindow(_handle);
            if (owner == ownerWindow.Handle)
                windowIsDialogForCandidate = true;
            return windowIsDialogForCandidate;
        }

        public override System.Drawing.Image CaptureImage()
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (_accessibleObject != null)
                {
                    _accessibleObject.UnreferenceObject();
                    _accessibleObject = null;
                }
                IsDisposed = true;
            }
            base.Dispose(disposing);
        }

        private X11Window WindowFromAccessibleObject(int id, AtSpiObject accessibleObject)
        {
            IntPtr parentWindowHandle = _handle;
            if (!IsTopLevelWindow)
                parentWindowHandle = _parentHandle;
            
            X11Window returnedWindow = new X11Window(_processId, parentWindowHandle, id, accessibleObject);
            return returnedWindow;
        }
    }
}
