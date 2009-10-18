using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core.Native.Windows;
using WatiN.Core.Native;

namespace WatiN.Core
{
    public interface INativeBrowser : IDisposable
    {
        /// <summary>
        /// Navigates to the specified <paramref name="url"/>.
        /// </summary>
        /// <param name="url">The URL to navigate to.</param>
        void NavigateTo(Uri url);

        /// <summary>
        /// Navigates to the specified <paramref name="url"/> without waiting for the page to finish loading.
        /// </summary>
        /// <param name="url">The URL to navigate to.</param>
        void NavigateToNoWait(Uri url);

        /// <summary>
        /// Navigates the browser back to the previously display Url
        /// </summary>
        /// <returns><c>True</c> if succeded otherwise <c>false</c>.</returns>
        bool GoBack();

        /// <summary>
        /// Navigates the browser forward to the next displayed Url (like the forward
        /// button in Internet Explorer). 
        /// </summary>
        /// <returns><c>True</c> if succeded otherwise <c>false</c>.</returns>
        bool GoForward();

        /// <summary>
        /// Closes and then reopens the browser with a blank page.
        /// </summary>
        void Reopen();

        /// <summary>
        /// Reloads the currently displayed webpage (like the Refresh/reload button in 
        /// a browser).
        /// </summary>
        void Refresh();

        INativeDocument NativeDocument { get; }
        Window HostWindow { get; }
        INativeDialogManager NativeDialogManager { get; }
    }
}
