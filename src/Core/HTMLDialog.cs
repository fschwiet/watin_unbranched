#region WatiN Copyright (C) 2006-2009 Jeroen van Menen

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

#endregion Copyright

using System;
using System.Collections.Generic;
using WatiN.Core.Native.InternetExplorer;
using WatiN.Core.Native;
using WatiN.Core.Native.Windows;
using WatiN.Core.UtilityClasses;
using StringComparer = WatiN.Core.Comparers.StringComparer;
using WatiN.Core.Exceptions;
using WatiN.Core.Dialogs;
using WatiN.Core.Interfaces;

namespace WatiN.Core
{
	/// <summary>
	/// This is the main class to access a webpage within a modal or modeless
	/// HTML dialog.
	/// </summary>
	public class HtmlDialog : DomContainer
	{
        private readonly Window dialogHostWindow;
        private IEDialogManager hostWindowDialogManager;

		public HtmlDialog(Window dialogWindow)
		{
            dialogHostWindow = dialogWindow;
            hostWindowDialogManager = new IEDialogManager(dialogHostWindow, WindowEnumerationMethod.WindowManagementApi);
		}

        /// <inheritdoc />
        public override Window HostWindow
        {
            get { return dialogHostWindow; }
        }

		protected override void Dispose(bool disposing)
		{
			Close();
		}

	    public override void WaitForComplete(int waitForCompleteTimeOut)
	    {
	        WaitForComplete(new IEWaitForComplete((IEDocument) NativeDocument, waitForCompleteTimeOut));
	    }

	    public virtual void Close()
		{
            if (dialogHostWindow.Visible)
			{
                dialogHostWindow.ForceClose();
                dialogHostWindow.Dispose();
			}
			base.Dispose(true);
		}

		public override INativeDocument OnGetNativeDocument()
		{
            return new IEDocument(IEUtils.IEDOMFromhWnd(dialogHostWindow));
		}

        /// <inheritdoc />
        protected override string GetAttributeValueImpl(string attributeName)
        {
			string value = null;

            if (StringComparer.AreEqual(attributeName, "href", true))
			{
                UtilityClass.TryActionIgnoreException(() => value = Url);
			}
            else if (StringComparer.AreEqual(attributeName, "title", true))
			{
                UtilityClass.TryActionIgnoreException(() => value = Title);
			}
			else
			{
                throw new InvalidAttributeException(attributeName, "HTMLDialog");
			}

			return value;
		}

        protected override IWatcher CreateWatcher(Type watchableType)
        {
            if (typeof(Dialog).IsAssignableFrom(watchableType))
                return new DialogWatcher(hostWindowDialogManager, watchableType);

            //if (typeof(InfoBar).IsAssignableFrom(watchableType))
            //    return new InfoBarWatcher(_dialogManager, watchableType);

            //if (typeof(Page).IsAssignableFrom(watchableType))
            //    return new PageWatcher(this, watchableType);

            throw new NotSupportedException("Unsupported watcher type.");
        }
    }
}