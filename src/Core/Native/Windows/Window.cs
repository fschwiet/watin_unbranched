using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WatiN.Core.Native.Windows
{
    public abstract class Window : IDisposable
    {
        protected WindowEnumerationMethod _enumerationMethod = WindowEnumerationMethod.WindowManagementApi;
        private WindowEnumerationMethod _childEnumerationMethod = WindowEnumerationMethod.WindowManagementApi;

        public abstract IntPtr Handle { get; }
        public abstract IntPtr ParentHandle { get; }
        public abstract IntPtr OwnerHandle { get; }
        public abstract string ClassName { get; }
        public abstract string Text { get; }
        public abstract bool Exists { get; }
        public abstract bool Visible { get; }
        public abstract bool Enabled { get; }
        public abstract bool IsTopLevelWindow { get; }
        public abstract bool IsDialog { get; }
        public abstract bool IsPressable { get; }
        public abstract WindowShowStyle WindowStyle { get; set; }
        public abstract Int64 Style { get; }
        public abstract string StyleDescriptor { get; }
        public abstract int ProcessId { get; }
        public abstract int ItemId { get; }
        internal abstract AssistiveTechnologyObject AccessibleObject { get; }

        public abstract bool SetFocus();
        public abstract bool IsDialogWindowFor(Window ownerWindow);
        public abstract IList<Window> GetChildWindows(WindowCriteriaConstraint constraint);
        public abstract void Press();
        public abstract void SendKeystrokes(string keystrokes);
        public abstract void ForceClose();
        public abstract System.Drawing.Image CaptureImage();

        public virtual WindowEnumerationMethod EnumerationMethod
        {
            get { return _enumerationMethod; }
        }

        public virtual WindowEnumerationMethod ChildEnumerationMethod
        {
            get { return _childEnumerationMethod; }
            set { _childEnumerationMethod = value; }
        }
		
		public virtual void Dispose()
		{
		}
    }
}
