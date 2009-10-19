using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core.Native.Windows;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;

namespace WatiN.Core.Native.Windows.Microsoft
{
    internal class MsWindowsWindow : Window
    {
        private IntPtr _handle = IntPtr.Zero;
        private IntPtr _parentWindowHandle = IntPtr.Zero;
        private static readonly List<string> _dialogClassNames = new List<string>();
        private MsaaObject _accessibleObject = null;
        private int _itemId = 0;

        internal MsWindowsWindow(IntPtr handle)
            : this(handle, IntPtr.Zero)
        {
        }

        internal MsWindowsWindow(IntPtr handle, IntPtr parentWindowHandle)
        {
            _handle = handle;
            _parentWindowHandle = parentWindowHandle;
            if (_dialogClassNames.Count == 0)
            {
                _dialogClassNames.Add("#32770");
                _dialogClassNames.Add("MozillaDialogClass");
            }
        }

        internal MsWindowsWindow(MsaaObject accessibleObject, IntPtr parentWindowHandle, int itemId)
        {
            _accessibleObject = accessibleObject;
            _parentWindowHandle = parentWindowHandle;
            _itemId = itemId;
            _enumerationMethod = WindowEnumerationMethod.AssistiveTechnologyApi;
            ChildEnumerationMethod = WindowEnumerationMethod.AssistiveTechnologyApi;
        }

        public static IList<Window> GetAllTopLevelWindows()
        {
            MsWindowsEnumerator enumerator = new MsWindowsEnumerator();
            IList<Window> allWindowList = enumerator.GetWindows(window => true);
            return allWindowList;
        }

        public override IntPtr Handle
        {
            get { return _handle; }
        }

        public override IntPtr ParentHandle
        {
            get { return _parentWindowHandle; }
        }

        public override IntPtr OwnerHandle
        {
            get { return MsWindowsNativeMethods.GetOwnerHandle(_handle); }
        }

        public override string ClassName
        {
            get
            {
                string windowClass = string.Empty;
                if (_enumerationMethod == WindowEnumerationMethod.WindowManagementApi)
                {
                    windowClass = MsWindowsNativeMethods.GetWindowClass(_handle);
                }
                else
                {
                    if (Exists)
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
                if (_enumerationMethod == WindowEnumerationMethod.WindowManagementApi)
                {
                    windowText = MsWindowsNativeMethods.GetWindowText(_handle);
                }
                else
                {
                    if (Exists)
                        windowText = AccessibleObject.Name;
                }
                return windowText;
            }
        }

        public override bool Visible
        {
            get
            {
                bool isVisible = false;
                if (_enumerationMethod == WindowEnumerationMethod.WindowManagementApi)
                {
                    isVisible = MsWindowsNativeMethods.IsWindowVisible(_handle);
                }
                else
                {
                    if (Exists)
                        isVisible = AccessibleObject.StateSet.Contains(AccessibleState.Visible);
                }
                return isVisible;
            }
        }

        public override bool Enabled
        {
            get
            {
                bool isEnabled = false;
                if (_enumerationMethod == WindowEnumerationMethod.WindowManagementApi)
                {
                    isEnabled = MsWindowsNativeMethods.IsWindowEnabled(_handle);
                }
                else
                {
                    if (Exists)
                        isEnabled = AccessibleObject.StateSet.Contains(AccessibleState.Enabled);
                }
                return isEnabled;
            }
        }

        public override bool Exists
        {
            get
            {
                bool windowExists = false;
                if (_enumerationMethod == WindowEnumerationMethod.WindowManagementApi)
                {
                    windowExists = MsWindowsNativeMethods.IsWindowValid(_handle);
                }
                else
                {
                    windowExists = (AccessibleObject != null);
                }
                return windowExists;
            }
        }

        public override bool IsTopLevelWindow
        {
            get { return _parentWindowHandle == IntPtr.Zero; }
        }

        public override bool IsDialog
        {
            get { return _dialogClassNames.Contains(ClassName); }
        }

        public override Int64 Style
        {
            get { return MsWindowsNativeMethods.GetWindowStyle(_handle); }
        }

        public override string StyleDescriptor
        {
            get { return Style.ToString("X"); }
        }

        public override int ProcessId
        {
            get { return MsWindowsNativeMethods.GetProcessIdForWindow(_handle); }
        }

        public override WindowShowStyle WindowStyle
        {
            get { return MsWindowsNativeMethods.GetWindowShowStyle(_handle); }
            set { MsWindowsNativeMethods.SetWindowShowStyle(_handle, value); }
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

        internal override AssistiveTechnologyObject AccessibleObject
        {
            get
            {
                if (_accessibleObject == null && IsTopLevelWindow)
                    _accessibleObject = new MsaaObject(_handle);
                return _accessibleObject;
            }
        }

        public override int ItemId
        {
            get
            {
                int idOfItem = 0;
                if (_enumerationMethod == WindowEnumerationMethod.WindowManagementApi)
                {
                    idOfItem = MsWindowsNativeMethods.GetDlgCtrlID(_handle);
                }
                else
                {
                    idOfItem = _itemId;
                }
                return idOfItem;
            }
        }

        public override void Press()
        {
            if (IsPressable)
            {
                if (EnumerationMethod == WindowEnumerationMethod.WindowManagementApi)
                {
                    MsWindowsNativeMethods.SendMessage(Handle, MsWindowsNativeMethods.WM_ACTIVATE, MsWindowsNativeMethods.MA_ACTIVATE, 0);
                    MsWindowsNativeMethods.SendMessage(Handle, MsWindowsNativeMethods.BM_CLICK, 0, 0);
                }
                else
                {
                    if (AccessibleObject != null && AccessibleObject.SupportsActions)
                        AccessibleObject.DoAction(0);
                }
            }
        }

        public override bool SetFocus()
        {
            return MsWindowsNativeMethods.SetFocusToWindow(_handle, _parentWindowHandle);
        }

        public override IList<Window> GetChildWindows(WindowCriteriaConstraint constraint)
        {
            IList<Window> childWindowList = new List<Window>();
            if (ChildEnumerationMethod == WindowEnumerationMethod.WindowManagementApi)
            {
                MsWindowsEnumerator enumerator = new MsWindowsEnumerator();
                childWindowList = enumerator.GetChildWindows(_handle, constraint);
            }
            else
            {
                if (AccessibleObject != null)
                {
                    IList<AssistiveTechnologyObject> accessibleChildren = AccessibleObject.GetChildrenByRole(AccessibleRole.AnyRole, true, true);
                    foreach (AssistiveTechnologyObject accessibleChild in accessibleChildren)
                    {
                        int itemIndex = accessibleChildren.IndexOf(accessibleChild);
                        MsWindowsWindow foundWindow = new MsWindowsWindow((MsaaObject)accessibleChild, _handle, itemIndex);
                        if (constraint(foundWindow))
                            childWindowList.Add(foundWindow);
                        else
                            foundWindow.Dispose();
                    }
                }
            }
            return childWindowList;
        }

        public override void ForceClose()
        {
            MsWindowsNativeMethods.SendMessage(_handle, MsWindowsNativeMethods.WM_CLOSE, 0, 0);
        }

        public override void SendKeystrokes(string keystrokes)
        {
            foreach (var c in keystrokes)
            {
                System.Threading.Thread.Sleep(50);
                List<MsWindowsNativeMethods.INPUT> keySequence = new List<MsWindowsNativeMethods.INPUT>();
                MsWindowsNativeMethods.INPUT keyDown = new MsWindowsNativeMethods.INPUT();
                keyDown.type = MsWindowsNativeMethods.InputType.Keyboard;
                keyDown.ki.wVk = Convert.ToInt16(char.ToUpper(c));
                keyDown.ki.dwFlags = MsWindowsNativeMethods.KeyEventFlags.None;
                keyDown.ki.time = 0;
                keyDown.ki.wScan = 0;
                keyDown.ki.dwExtraInfo = IntPtr.Zero;

                MsWindowsNativeMethods.INPUT keyUp = new MsWindowsNativeMethods.INPUT();
                keyUp.type = MsWindowsNativeMethods.InputType.Keyboard;
                keyUp.ki.wVk = Convert.ToInt16(char.ToUpper(c));
                keyUp.ki.dwFlags = MsWindowsNativeMethods.KeyEventFlags.KeyUp;
                keyUp.ki.time = 0;
                keyUp.ki.wScan = 0;
                keyUp.ki.dwExtraInfo = IntPtr.Zero;

                keySequence.Add(keyDown);
                keySequence.Add(keyUp);

                if (c >= 'A' && c <= 'Z')
                {
                    MsWindowsNativeMethods.INPUT shiftKeyDown = new MsWindowsNativeMethods.INPUT();
                    shiftKeyDown.type = MsWindowsNativeMethods.InputType.Keyboard;
                    shiftKeyDown.ki.wVk = (short)MsWindowsNativeMethods.VK.Shift;

                    MsWindowsNativeMethods.INPUT shiftKeyUp = new MsWindowsNativeMethods.INPUT();
                    shiftKeyUp.type = MsWindowsNativeMethods.InputType.Keyboard;
                    shiftKeyUp.ki.wVk = (short)MsWindowsNativeMethods.VK.Shift;
                    shiftKeyUp.ki.dwFlags = MsWindowsNativeMethods.KeyEventFlags.KeyUp;

                    keySequence.Insert(0, shiftKeyDown);
                    keySequence.Add(shiftKeyUp);
                }
                SetFocus();
                MsWindowsNativeMethods.SendInput((uint)keySequence.Count, keySequence.ToArray(), Marshal.SizeOf(keySequence[0]));
            }
        }

        public override bool IsDialogWindowFor(Window ownerWindow)
        {
            bool windowIsDialogFor = false;
            windowIsDialogFor = ((ownerWindow.Handle == OwnerHandle || ownerWindow.Handle == ParentHandle) && Visible);
            return windowIsDialogFor;
        }

        public override System.Drawing.Image CaptureImage()
        {
            return MsWindowsNativeMethods.GetWindowImage(Handle);
        }

        internal Int32 RegisterWindowMessage(string message)
        {
            return MsWindowsNativeMethods.RegisterWindowMessage("WM_HTML_GETOBJECT");
        }

        internal Int32 SendMessageWithAbortIfHungTimeout(int msg, int wParam, int lParam, int timeout, ref int lpdwResult)
        {
            return SendMessageTimeout(msg, wParam, lParam, MsWindowsNativeMethods.SMTO_ABORTIFHUNG, timeout, ref lpdwResult);
        }

        private Int32 SendMessageTimeout(int msg, int wParam, int lParam, int fuFlags, int uTimeout, ref int lpdwResult)
        {
            return MsWindowsNativeMethods.SendMessageTimeout(_handle, msg, wParam, lParam, fuFlags, uTimeout, ref lpdwResult);
        }
    }
}
