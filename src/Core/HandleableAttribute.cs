using System;
using System.Collections.Generic;
using System.Text;

namespace WatiN.Core
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=false)]
    public class HandleableAttribute : Attribute
    {
        public string Identifier { get; private set; }

        public HandleableAttribute(string kind)
        {
            if (!string.IsNullOrEmpty(kind))
                Identifier = kind;
        }
    }
}
