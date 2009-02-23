﻿#region WatiN Copyright (C) 2006-2009 Jeroen van Menen

//Copyright 2006-2009 Jeroen van Menen
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

// ---------------------------------------------------------------------------
//
// Copyright (c) Doug Weems.
// http://www.codeproject.com/cs/media/IECapture.asp
// 
// You may use this code for fun and knowledge.
// You can compile and use the application as is or copy out what you need.  
// This code makes for a really useful tool.
// 
// ---------------------------------------------------------------------------

#endregion Copyright

using System;
using System.Runtime.InteropServices;
using mshtml;
using WatiN.Core.UtilityClasses;

namespace WatiN.Core.Native.InternetExplorer
{
    internal class IEUtils
    {
        public static IHTMLDocument2 IEDOMFromhWnd(IntPtr hWnd)
        {
            var IID_IHTMLDocument2 = new Guid("626FC520-A41E-11CF-A731-00A0C9082637");

            var lRes = 0;

            if (!IsIEServerWindow(hWnd))
            {
                // Get 1st child IE server window
                hWnd = NativeMethods.GetChildWindowHwnd(hWnd, "Internet Explorer_Server");
            }

            if (IsIEServerWindow(hWnd))
            {
                // Register the message
                var lMsg = NativeMethods.RegisterWindowMessage("WM_HTML_GETOBJECT");
                // Get the object
                NativeMethods.SendMessageTimeout(hWnd, lMsg, 0, 0, NativeMethods.SMTO_ABORTIFHUNG, 1000, ref lRes);
                if (lRes != 0)
                {
                    // Get the object from lRes
                    IHTMLDocument2 ieDOMFromhWnd = null;
                    var hr = NativeMethods.ObjectFromLresult(lRes, ref IID_IHTMLDocument2, 0, ref ieDOMFromhWnd);
                    if (hr != 0)
                    {
                        throw new COMException("ObjectFromLresult has thrown an exception", hr);
                    }
                    return ieDOMFromhWnd;
                }
            }
            return null;
        }

        public static bool IsIETridentDlgFrame(IntPtr hWnd)
        {
            return UtilityClass.CompareClassNames(hWnd, "Internet Explorer_TridentDlgFrame");
        }

        public static bool IsIEServerWindow(IntPtr hWnd)
        {
            return UtilityClass.CompareClassNames(hWnd, "Internet Explorer_Server");
        }
    }
}
