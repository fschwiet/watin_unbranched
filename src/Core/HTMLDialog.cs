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
using WatiN.Core.Native.InternetExplorer;
using WatiN.Core.Native;
using WatiN.Core.Native.Windows;
using WatiN.Core.UtilityClasses;
using StringComparer = WatiN.Core.Comparers.StringComparer;
using WatiN.Core.Exceptions;
using System.Collections.Generic;
using WatiN.Core.Interfaces;
using WatiN.Core.WatchableObjects;

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
        private Dictionary<Type, IWatcher> watchers;

        public override Window HostWindow
        {
            get { return dialogHostWindow; }
        }
        //public override IntPtr hWnd
        //{
        //    get { return _dialogWindow.Handle; }
        //}

        //public override int ProcessID
        //{
        //    get { return _dialogWindow.ProcessId; }
        //}

		public HtmlDialog(Window dialogWindow)
		{
            dialogHostWindow = dialogWindow;
            hostWindowDialogManager = new IEDialogManager(dialogHostWindow, WindowEnumerationMethod.WindowManagementApi);
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

        /// <inheritdoc />
        public override void SetHandler<TWatchable>(Action<TWatchable> action)
        {
            IWatcher watcher = GetWatcher(typeof(TWatchable), true);
            watcher.SetHandler(action);
        }

        /// <inheritdoc />
        public override void ClearHandler<TWatchable>()
        {
            IWatcher watcher = GetWatcher(typeof(TWatchable), false);
            if (watcher != null)
                watcher.ClearHandler<TWatchable>();
        }

        /// <inheritdoc />
        public override void ResetHandler<TWatchable>()
        {
            IWatcher watcher = GetWatcher(typeof(TWatchable), false);
            if (watcher != null)
                watcher.ResetHandler<TWatchable>();
        }

        /// <inheritdoc />
        public override Expectation<TWatchable> Expect<TWatchable>()
        {
            return Expect<TWatchable>(Expectation<TWatchable>.DefaultTimeout);
        }

        /// <inheritdoc />
        public override Expectation<TWatchable> Expect<TWatchable>(int timeout)
        {
            IWatcher watcher = GetWatcher(typeof(TWatchable), true);
            return watcher.Expect<TWatchable>(timeout);
        }

        private IWatcher GetWatcher(Type watchableType, bool createIfAbsent)
        {
            if (watchers == null)
                watchers = new Dictionary<Type, IWatcher>();

            Type baseWatchableType = GetBaseWatchableType(watchableType);

            IWatcher watcher;
            if (!watchers.TryGetValue(baseWatchableType, out watcher) && createIfAbsent)
            {
                watcher = CreateWatcher(baseWatchableType);
                watchers.Add(baseWatchableType, watcher);
            }

            return watcher;
        }

        private Type GetBaseWatchableType(Type watchableType)
        {
            Type baseWatchableType = typeof(object);

            if (typeof(Dialog).IsAssignableFrom(watchableType))
                baseWatchableType = typeof(Dialog);

            //else if (typeof(InfoBar).IsAssignableFrom(watchableType))
            //    baseWatchableType = typeof(InfoBar);

            //else if (typeof(Page).IsAssignableFrom(watchableType))
            //    baseWatchableType = typeof(Page);

            else
                throw new NotSupportedException("Unsupported watcher type.");

            return baseWatchableType;
        }

        private IWatcher CreateWatcher(Type watchableType)
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