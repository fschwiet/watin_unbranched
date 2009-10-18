using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WatiN.Core.Native.Windows;
using System.Reflection;

namespace WatiN.Core.Native
{
    public abstract class DialogManager : INativeDialogManager
    {
        private static object lockObject = new object();

        private List<Type> registeredDialogTypeList = new List<Type>();
        private bool keepRunning = true;
        private readonly Thread watcherThread;
        private Window mainWindow = null;
        private bool useWindowManagementApi = false;

        /// <summary>
        /// Protected constructor to prevent instantiation of abstract base class.
        /// </summary>
        /// <param name="monitoredWindow">A <see cref="WatiN.Core.Native.Windows.Window"/> object representing the window to be monitored for dialogs.</param>
        /// <param name="childEnumerationMethod">A <see cref="WatiN.Core.Native.Windows.WindowEnumerationMethod"/> value representing the method used to enumerate child windows on the dialog.</param>
        protected DialogManager(Window monitoredWindow, WindowEnumerationMethod childEnumerationMethod)
        {
            mainWindow = monitoredWindow;
            RegisterDialogs();
            watcherThread = new Thread(Start);
            watcherThread.Start();
            useWindowManagementApi = (childEnumerationMethod == WindowEnumerationMethod.WindowManagementApi);
        }

        protected List<Type> RegisteredDialogTypes
        {
            get { return registeredDialogTypeList; }
        }

        protected abstract void RegisterDialogs();

        /// <summary>
        /// Called by the constructor to start watching popups
        /// on a separate thread.
        /// </summary>
        private void Start()
        {
            while (keepRunning)
            {
                if (mainWindow.Exists)
                {
                    IList<Window> windows = WindowFactory.GetWindows(win => true, useWindowManagementApi);

                    foreach (var activeWindow in windows)
                    {
                        if (activeWindow.IsDialogWindowFor(mainWindow))
                        {
                            INativeDialog dialog;
                            if (TryMatchWindow(activeWindow, out dialog))
                            {
                                OnDialogFound(new NativeDialogFoundEventArgs(dialog));
                            }
                        }
                        else
                        {
                            activeWindow.Dispose();
                        }
                    }
                    if (!keepRunning) return;

                    // Keep DialogWatcher responsive during 1 second sleep period
                    var count = 0;
                    while (keepRunning && count < 5)
                    {
                        Thread.Sleep(200);
                        count++;
                    }
                }
                else
                {
                    keepRunning = false;
                }

            }
        }

        private bool TryMatchWindow(Window activeWindow, out INativeDialog dialog)
        {
            bool windowMatched = false;
            dialog = null;
            foreach (Type knownWindowType in registeredDialogTypeList)
            {
                ConstructorInfo ctor = knownWindowType.GetConstructor(Type.EmptyTypes);
                INativeDialog candidateDialog = ctor.Invoke(null) as INativeDialog;
                if (candidateDialog != null)
                {
                    if (candidateDialog.WindowIsDialogInstance(activeWindow))
                    {
                        dialog = candidateDialog;
                        dialog.DialogWindow = activeWindow;
                        windowMatched = true;
                        break;
                    }
                    else
                    {
                        candidateDialog.Dispose();
                    }
                }
            }
            return windowMatched;
        }

        private bool IsRunning
        {
            get { return watcherThread.IsAlive; }
        }

        #region IDisposable Members
        public void Dispose()
        {
            lock (lockObject)
            {
                keepRunning = false;
            }
            if (IsRunning)
            {
                watcherThread.Join();
            }
        }
        #endregion

        #region INativeDialogManager Members
        /// <inheritdoc />
        public event EventHandler<NativeDialogFoundEventArgs> DialogFound;
        #endregion

        protected void OnDialogFound(NativeDialogFoundEventArgs e)
        {
            if (DialogFound != null)
                DialogFound(this , e);
        }
    }
}
