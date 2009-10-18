using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core.Native.Windows;

namespace WatiN.Core.Native.Windows.Microsoft
{
    internal class MsWindowsEnumerator
    {
        /// <summary> 
        /// Get all top-level window information 
        /// </summary> 
        /// <returns>List of window information objects</returns> 
        internal IList<Window> GetTopLevelWindows()
        {
            return GetWindows(window => true);
        }

        //internal IList<Window> GetTopLevelWindows(string className)
        //{
        //    return GetWindows(window => MsWindowsNativeMethods.CompareClassNames(window.Handle, className));
        //}

        internal IList<Window> GetWindows(WindowCriteriaConstraint constraint)
        {
            var windows = new List<Window>();

            MsWindowsNativeMethods.EnumWindows((hwnd, lParam) =>
            {
                Window window = new MsWindowsWindow(hwnd);
                if (constraint == null || constraint(window))
                    windows.Add(window);

                return true;
            }, IntPtr.Zero);

            return windows;
        }

        /// <summary> 
        /// Get all child windows for the specific windows handle (hwnd). 
        /// </summary> 
        /// <returns>List of child windows for parent window</returns> 
        internal IList<Window> GetChildWindows(IntPtr hwnd)
        {
            return GetChildWindows(hwnd, (string)null);
        }

        internal IList<Window> GetChildWindows(IntPtr hwnd, string childClass)
        {
            return GetChildWindows(hwnd, window => childClass == null || MsWindowsNativeMethods.CompareClassNames(window.Handle, childClass));
        }

        internal IList<Window> GetChildWindows(IntPtr hwnd, WindowCriteriaConstraint constraint)
        {
            var childWindows = new List<Window>();

            MsWindowsNativeMethods.EnumChildWindows(hwnd, (childHwnd, lParam) =>
            {
                MsWindowsWindow childWindow = new MsWindowsWindow(childHwnd, lParam);
                if (constraint == null || constraint(childWindow))
                {
                    childWindows.Add(childWindow);
                }

                return true;
            }, hwnd);

            return childWindows;
        }
    }
}
