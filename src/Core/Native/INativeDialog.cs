using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WatiN.Core.Native.Windows;

namespace WatiN.Core.Native
{
    internal interface INativeDialog : IDisposable
    {
        /// <summary>
        /// Gets or sets the <see cref="WatiN.Core.Native.Windows.Window"/> object representing the physical operating system window of the dialog.
        /// </summary>
        Window DialogWindow { get; set; }

        /// <summary>
        /// Gets a code that identifies the kind of dialog that this object represents, such as "AlertDialog", "ConfirmDialog",
        /// "IESecurityDialog", "FireFoxWidgetDialog".
        /// </summary>
        string Kind { get; }

        /// <summary>
        /// Gets a property of the dialog such as its title, message text, or other contents.
        /// </summary>
        /// <param name="propertyId">A <see cref="System.String"/> representing the property to get the value of.</param>
        /// <returns>A <see cref="System.Object"/> that conatains the property value.</returns>
        object GetProperty(string propertyId);

        /// <summary>
        /// Performs some action such as clicking on a particular button or entering some text into a field.
        /// </summary>
        /// <param name="actionId">A <see cref="System.String"/> representing the action to execute.</param>
        /// <param name="args">An array of <see cref="System.Object"/> representing the parameters to the actions.</param>
        void PerformAction(string actionId, object[] args);

        /// <summary>
        /// Performs the default action to dismiss the dialog such as closing or canceling it.
        /// </summary>
        void Dismiss();

        /// <summary>
        /// Gets a value indicating whether the <see cref="WatiN.Core.Native.Windows.Window"/> object is an instance of this dialog.
        /// </summary>
        /// <param name="candidateWindow">The <see cref="WatiN.Core.Native.Windows.Window"/> object to test.</param>
        /// <returns>true if the <see cref="WatiN.Core.Native.Windows.Window"/> is an instance of this dialog; false if it is not.</returns>
        bool WindowIsDialogInstance(Window candidateWindow);
    }
}
