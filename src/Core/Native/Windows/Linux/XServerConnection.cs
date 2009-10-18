using System;
namespace WatiN.Core.Native.Windows.Linux
{
    internal class XServerConnection : IDisposable
    {
        private IntPtr _x11Display = IntPtr.Zero;

        internal XServerConnection()
        {
            _x11Display = X11WindowsNativeMethods.OpenServerConnection();
        }
        
        internal IntPtr Display
        {
            get { return _x11Display; }
        }

        #region IDisposable implementation
        public void Dispose ()
        {
            try
            {
                if (_x11Display != IntPtr.Zero)
                {
                    X11WindowsNativeMethods.CloseServerConnection(_x11Display);
                    _x11Display = IntPtr.Zero;
                }
            }
            catch(Exception)
            {
            }
        }
        #endregion
    }
}
