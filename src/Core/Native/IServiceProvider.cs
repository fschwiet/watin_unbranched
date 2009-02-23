﻿using System;
using System.Runtime.InteropServices;

namespace WatiN.Core.Native
{
    [ComVisible(true), ComImport, Guid("6d5140c1-7436-11ce-8034-00aa006009fa"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IServiceProvider
    {
        [return: MarshalAs(UnmanagedType.I4)]
        [PreserveSig]
        uint QueryService(
            ref Guid guidService,
            ref Guid riid,
            [MarshalAs(UnmanagedType.Interface)]out object ppvObject);
    }
}
