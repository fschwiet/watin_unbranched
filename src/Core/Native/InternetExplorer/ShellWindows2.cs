﻿using System;
using System.Collections;
using System.Collections.Generic;
using mshtml;
using SHDocVw;
using WatiN.Core.Native.Windows;
using IServiceProvider = WatiN.Core.Native.Windows.IServiceProvider;

namespace WatiN.Core.Native.InternetExplorer
{
    public class ShellWindows2 : IEnumerable<IWebBrowser2>
    {
        private List<IWebBrowser2> _browsers;

        private Guid SID_STopLevelBrowser = new Guid(0x4C96BE40, 0x915C, 0x11CF, 0x99, 0xD3, 0x00, 0xAA, 0x00, 0x4A, 0xE8, 0x37);
        private Guid SID_SWebBrowserApp = new Guid(0x0002DF05, 0x0000, 0x0000, 0xC0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x46);

        public ShellWindows2()
        {
            CollectInternetExplorerInstances();
        }

        public int Count
        {
            get { return _browsers.Count; }
        }

        private void CollectInternetExplorerInstances()
        {
            var enumerator = new WindowsEnumerator();
            _browsers = new List<IWebBrowser2>();

            var topLevelWindows = enumerator.GetTopLevelWindows("IEFrame");
            foreach (var mainBrowserWindow in topLevelWindows)
            {
                var windows = enumerator.GetChildWindows(mainBrowserWindow.Hwnd, "TabWindowClass");

                // IE6 has no TabWindowClass so use the IEFrame as starting point
                if (windows.Count == 0)
                {
                    windows.Add(mainBrowserWindow);
                }

                foreach (var window in windows)
                {
                    var document2 = IEUtils.IEDOMFromhWnd(window.Hwnd);
                    if (document2 == null) continue;

                    var parentWindow = document2.parentWindow;
                    if (parentWindow == null) continue;

                    var webBrowser2 = RetrieveIWebBrowser2FromIHtmlWindw2Instance(parentWindow);
                    if (webBrowser2 == null) continue;

                    _browsers.Add(webBrowser2);
                }
            }
        }

        /// <exclude />
        public IEnumerator GetEnumerator()
        {
            foreach (var browser in _browsers)
            {
                yield return browser;
            }
        }

        IEnumerator<IWebBrowser2> IEnumerable<IWebBrowser2>.GetEnumerator()
        {
            foreach (var browser in _browsers)
            {
                yield return browser;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IWebBrowser2 RetrieveIWebBrowser2FromIHtmlWindw2Instance(IHTMLWindow2 ihtmlWindow2)
        {
            var guidIServiceProvider = typeof(IServiceProvider).GUID;

            var serviceProvider = ihtmlWindow2 as IServiceProvider;
            if (serviceProvider == null) return null;

            object objIServiceProvider;
            serviceProvider.QueryService(ref SID_STopLevelBrowser, ref guidIServiceProvider, out objIServiceProvider);

            serviceProvider = objIServiceProvider as IServiceProvider;
            if (serviceProvider == null) return null;

            object objIWebBrowser;
            var guidIWebBrowser = typeof(IWebBrowser2).GUID;
            serviceProvider.QueryService(ref SID_SWebBrowserApp, ref guidIWebBrowser, out objIWebBrowser);
            var webBrowser = objIWebBrowser as IWebBrowser2;

            return webBrowser;
        }
    }

    /// <summary> 
    /// Enumerate top-level and child windows 
    /// </summary> 
    public class WindowsEnumerator
    {
        private List<Window> _windows;
        private string _classNameFilter;

        /// <summary> 
        /// Get all top-level window information 
        /// </summary> 
        /// <returns>List of window information objects</returns> 
        public List<Window> GetTopLevelWindows()
        {
            return GetTopLevelWindows(null);
        }

        public List<Window> GetTopLevelWindows(string className)
        {
            _classNameFilter = className;
            _windows = new List<Window>();

            var hwnd = IntPtr.Zero;
            NativeMethods.EnumWindows(EnumWindowProc, hwnd);
            return _windows;
        }

        /// <summary> 
        /// Get all child windows for the specific windows handle (hwnd). 
        /// </summary> 
        /// <returns>List of child windows for parent window</returns> 
        public List<Window> GetChildWindows(IntPtr hwnd)
        {
            return GetChildWindows(hwnd, null);
        }

        public List<Window> GetChildWindows(IntPtr hwnd, string childClass)
        {
            _classNameFilter = childClass;
            _windows = new List<Window>();

            var hWnd = IntPtr.Zero;
            NativeMethods.EnumChildWindows(hwnd, EnumChildWindowProc, ref hWnd);

            return _windows;
        }

        /// <summary> 
        /// Callback function that does the work of enumerating top-level windows. 
        /// </summary> 
        /// <param name="hwnd">Discovered Window handle</param> 
        /// <param name="lParam"></param>
        /// <returns>1=keep going, 0=stop</returns> 
        private bool EnumWindowProc(IntPtr hwnd, ref IntPtr lParam)
        {
            var window = new Window(hwnd);

            // Eliminate windows that are not top-level. 
            if (!window.HasParentWindow) MatchWindow(window);

            return true;
        }

        /// <summary> 
        /// Callback function that does the work of enumerating child windows. 
        /// </summary> 
        /// <param name="hwnd">Discovered Window handle</param> 
        /// <param name="lParam"></param>
        /// <returns>1=keep going, 0=stop</returns> 
        private bool EnumChildWindowProc(IntPtr hwnd, ref IntPtr lParam)
        {
            MatchWindow(new Window(hwnd));
            return true;
        }

        private void MatchWindow(Window window)
        {
            // Match the class name if searching for a specific window class. 
            if (_classNameFilter.Length == 0 || window.ClassName.ToLower() == _classNameFilter.ToLower())
            {
                _windows.Add(window);
            }
        }
    }
}
