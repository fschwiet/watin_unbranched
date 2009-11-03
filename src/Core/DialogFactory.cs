using System;
using System.Collections.Generic;
using System.Text;
using WatiN.Core.Native;
using System.Reflection;
using WatiN.Core.Dialogs;
using WatiN.Core.Interfaces;
using WatiN.Core.Exceptions;

namespace WatiN.Core
{
    internal static class DialogFactory
    {
        private static Dictionary<string, ConstructorInfo> _dialogConstructors = new Dictionary<string, ConstructorInfo>();

        static DialogFactory()
        {
            Type[] exportedTypes = Assembly.GetExecutingAssembly().GetExportedTypes();
            foreach (Type exportedType in exportedTypes)
            {
                if (exportedType.IsSubclassOf(typeof(Dialog)))
                {
                    object[] attributes = exportedType.GetCustomAttributes(typeof(HandleableAttribute), false);
                    if (attributes.Length > 0)
                    {
                        HandleableAttribute attribute = attributes[0] as HandleableAttribute;
                        if (attribute != null && !string.IsNullOrEmpty(attribute.Identifier))
                        {
                            ConstructorInfo ctor = exportedType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, Type.DefaultBinder, new Type[] { typeof(INativeDialog) }, null);
                            if (_dialogConstructors.ContainsKey(attribute.Identifier))
                            {
                                throw new WatiNException(string.Format("Duplicate dialog kind for '{0}'", attribute.Identifier));
                            }
                            if (ctor == null)
                            {
                                throw new WatiNException(string.Format("No constructor with parameter INativeDialog found for '{0}'", attribute.Identifier));
                            }
                            _dialogConstructors.Add(attribute.Identifier, ctor);
                        }
                    }
                }
            }
        }

        internal static Dialog CreateDialog(INativeDialog nativeDialog)
        {
            Dialog dialogInstance = null;
            if (_dialogConstructors.ContainsKey(nativeDialog.Kind))
            {
                dialogInstance = _dialogConstructors[nativeDialog.Kind].Invoke(new object[] { nativeDialog }) as Dialog;
            }
            return dialogInstance;
        }


    }
}
