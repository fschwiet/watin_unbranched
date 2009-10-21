using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core.Native.Windows;
using System.Runtime.InteropServices;
using System.Threading;
using SHDocVw;
using mshtml;

namespace WatiN.Core.Native.InternetExplorer
{
    public class IEBrowser : INativeBrowser
    {
        #region Constants
        private const string IEWindowClassName = "IEFrame";
        #endregion

        #region Private members
        IEDialogManager _dialogManager = null;
        Window _hostWindow = null;
        private readonly IWebBrowser2 webBrowser;
        #endregion

        #region Constructor
        public IEBrowser(IWebBrowser2 webBrowser2)
        {
            webBrowser = webBrowser2;
            IList<Window> mainWindows = WindowFactory.GetWindows(candidateWindow => candidateWindow.ClassName == IEWindowClassName);
            if (mainWindows.Count >= 1)
            {
                _hostWindow = mainWindows[0];
                mainWindows.Remove(_hostWindow);
                _dialogManager = new IEDialogManager(_hostWindow, WindowEnumerationMethod.WindowManagementApi);
            }
            WindowFactory.DisposeWindows(mainWindows);
        }
        #endregion

        public IWebBrowser2 WebBrowser
        {
            get { return webBrowser; }
        }

        #region INativeBrowser Members

        /// <inheritdoc />
        public void NavigateTo(Uri url)
        {
            object nil = null;
            object absoluteUri = url.AbsoluteUri;
            webBrowser.Navigate2(ref absoluteUri, ref nil, ref nil, ref nil, ref nil);
        }

        /// <inheritdoc />
        public void NavigateToNoWait(Uri url)
        {
            var thread = new Thread(GoToNoWaitInternal);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start(url);
            thread.Join(500);
        }

        [STAThread]
        private void GoToNoWaitInternal(object uriIn)
        {
            var uri = (Uri)uriIn;
            NavigateTo(uri);
        }

        /// <inheritdoc />
        public bool GoBack()
        {
            try
            {
                webBrowser.GoBack();
                return true;
            }
            catch (COMException)
            {
                return false;
            }
        }

        /// <inheritdoc />
        public bool GoForward()
        {
            try
            {
                webBrowser.GoForward();
                return true;
            }
            catch (COMException)
            {
                return false;
            }
        }

        /// <inheritdoc />
        public void Reopen()
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public void Refresh()
        {
            object REFRESH_COMPLETELY = 3;
            webBrowser.Refresh2(ref REFRESH_COMPLETELY);
        }

        /// <inheritdoc />
        public IntPtr hWnd
        {
            get { return new IntPtr(webBrowser.HWND); }
        }

        public bool Visible
        {
            get { return webBrowser.Visible; }
            set { webBrowser.Visible = value; }
        }

        public void Quit()
        {
            webBrowser.Quit();
        }

        public INativeDocument NativeDocument
        {
            get { return new IEDocument((IHTMLDocument2)webBrowser.Document); }
        }

        public Window HostWindow
        {
            get { return _hostWindow; }
        }

        public INativeDialogManager NativeDialogManager
        {
            get { return _dialogManager; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (_hostWindow != null)
                _hostWindow.Dispose();
            if (_dialogManager != null)
                _dialogManager.Dispose();
        }

        #endregion
    }
}
