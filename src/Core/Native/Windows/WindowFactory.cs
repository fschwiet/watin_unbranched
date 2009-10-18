using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using WatiN.Core.Native.Windows.Linux;
using WatiN.Core.Native.Windows.Microsoft;

namespace WatiN.Core.Native.Windows
{
    public delegate bool WindowCriteriaConstraint(Window window);

    public static class WindowFactory
    {
        private static readonly string MsWindowsButtonClassName = "Button";
        private static readonly string MsWindowsEditableTextBoxClassName = "Edit";
        private static readonly string MsWindowsStaticLabelClassName = "Static";

        /// <summary>
        /// Gets the string representing the window class for an AccessibleRole
        /// </summary>
        /// <param name="role">The AccessibleRole to get the window class for</param>
        /// <param name="useNativeWindowApi">True to use the native window system API for window classes; False to use the accessibility API.</param>
        /// <returns>A string for the window class.</returns>
        public static string GetWindowClassForRole(AccessibleRole role, bool useNativeWindowApi)
        {
            string windowClass = string.Empty;
            if ((Environment.OSVersion.Platform != PlatformID.MacOSX && Environment.OSVersion.Platform != PlatformID.Unix) && useNativeWindowApi)
            {
                switch (role)
                {
                    case AccessibleRole.PushButton:
                        windowClass = MsWindowsButtonClassName;
                        break;
                    case AccessibleRole.Text:
                        windowClass = MsWindowsEditableTextBoxClassName;
                        break;
                    case AccessibleRole.Label:
                        windowClass = MsWindowsStaticLabelClassName;
                        break;
                    default:
                        windowClass = role.ToString();
                        break;
                }
            }
            else
                windowClass = role.ToString();
            return windowClass;
        }

        /// <summary>
        /// Gets a Window object for a specific window identifier
        /// </summary>
        /// <param name="windowHandle">The OS-specific window identifier</param>
        /// <param name="enumChildrenByNativeWindowApi">True if child windows are enumerated by the windowing system API; False if they are enumerated with the accessibility API.</param>
        /// <returns>The Window object having the OS-specific identifier.</returns>
        public static Window GetWindow(IntPtr windowHandle, bool enumChildrenByNativeWindowApi)
        {
            Window foundWindow = null;
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                    foundWindow = new X11Window(windowHandle);
                    break;

                default:
                    foundWindow = new MsWindowsWindow(windowHandle);
                    break;
            }
            if (foundWindow != null && !enumChildrenByNativeWindowApi)
                foundWindow.ChildEnumerationMethod = WindowEnumerationMethod.AssistiveTechnologyApi;

            return foundWindow;
        }

        /// <summary>
        /// Gets a list of top-level windows matching the specified constraint.
        /// </summary>
        /// <param name="constraint">A WindowCriteriaConstraint containing the matching information about the window.</param>
        /// <param name="enumChildrenByNativeWindowApi">True if child windows are enumerated by the windowing system API; False if they are enumerated with the accessibility API.</param>
        /// <returns>A list of Window object matching the criteria.</returns>
        public static IList<Window> GetWindows(WindowCriteriaConstraint constraint, bool enumChildrenByNativeWindowApi)
        {
            IList<Window> windowList = new List<Window>();
            IList<Window> allWindowList = GetAllTopLevelWindows();
            foreach (Window candidateWindow in allWindowList)
            {
                if (constraint(candidateWindow))
                    windowList.Add(candidateWindow);
                else
                    candidateWindow.Dispose();
            }
            if (!enumChildrenByNativeWindowApi)
            {
                foreach (Window foundWindow in windowList)
                    foundWindow.ChildEnumerationMethod = WindowEnumerationMethod.AssistiveTechnologyApi;
            }
            return windowList;
        }

        /// <summary>
        /// Gets a list of top-level windows matching the specified constraint.
        /// </summary>
        /// <param name="constraint">A WindowCriteriaConstraint containing the matching information about the window.</param>
        /// <returns>A list of Window object matching the criteria.</returns>
        /// <remarks>This overload assumes the windowing system API will always be used.</remarks>
        public static IList<Window> GetWindows(WindowCriteriaConstraint constraint)
        {
            return GetWindows(constraint, true);
        }

        /// <summary>
        /// Disposes of a list of windows.
        /// </summary>
        /// <param name="windowList">The list of windows to dispose.</param>
        public static void DisposeWindows(IList<Window> windowList)
        {
            foreach (Window windowToDispose in windowList)
            {
                windowToDispose.Dispose();
            }
        }

        private static IList<Window> GetAllTopLevelWindows()
        {
            IList<Window> windowList = new List<Window>();
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                    windowList = X11Window.GetAllTopLevelWindows();
                    break;

                default:
                    windowList = MsWindowsWindow.GetAllTopLevelWindows();
                    break;
            }
            return windowList;
        }

        //public static Window GetMainWindow(Process mainProcess)
        //{
        //    Window windowObject = null;
        //    mainProcess.WaitForInputIdle(5000);
        //    mainProcess.Refresh();
        //    switch (Environment.OSVersion.Platform)
        //    {
        //        case PlatformID.MacOSX:
        //        case PlatformID.Unix:
        //            windowObject = new X11Window(mainProcess.Id);
        //            while (windowObject.Handle == IntPtr.Zero)
        //            {
        //                System.Threading.Thread.Sleep(200);
        //                windowObject = new X11Window(mainProcess.Id);
        //            }
        //            break;
        //
        //        default:
        //            while (mainProcess.MainWindowHandle == IntPtr.Zero)
        //            {
        //                System.Threading.Thread.Sleep(200);
        //                mainProcess.Refresh();
        //            }
        //            windowObject = new MsWindowsWindow(mainProcess.MainWindowHandle);
        //            break;
        //    }
        //    return windowObject;
        //}

        //public static IList<Window> GetAllTopLevelWindowsForProcess(Process mainProcess, bool enumChildrenByNativeWindowApi)
        //{
        //    IList<Window> windowList = new List<Window>();
        //    switch (Environment.OSVersion.Platform)
        //    {
        //        case PlatformID.MacOSX:
        //        case PlatformID.Unix:
        //            windowList = GetWindows(candidateWindow => candidateWindow.ProcessId == mainProcess.Id && candidateWindow.Visible);
        //            break;
        //
        //        default:
        //            windowList = GetWindows(candidateWindow => (candidateWindow.Handle == mainProcess.MainWindowHandle || candidateWindow.OwnerHandle == mainProcess.MainWindowHandle || candidateWindow.ParentHandle == mainProcess.MainWindowHandle) && candidateWindow.Visible, enumChildrenByNativeWindowApi);
        //            break;
        //    }
        //    return windowList;
        //}
    }
}
