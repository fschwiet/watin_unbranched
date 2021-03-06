#region WatiN Copyright (C) 2006-2010 Jeroen van Menen

//Copyright 2006-2010 Jeroen van Menen
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
using System.Runtime.Serialization;
using WatiN.Core.Logging;

namespace WatiN.Core.Exceptions
{
	/// <summary>
	/// Base class for Exceptions thrown by WatiN.
	/// </summary>
    [Serializable]
	public class WatiNException : Exception
	{
		public WatiNException(){}
		public WatiNException(string message) : base(message)
		{
		    Logger.LogDebug(string.Format("Exception: {0}, {1}\n{2}", GetType().Name, message, StackTrace!=null?StackTrace:""));
		}
        public WatiNException(string message, Exception innerexception) : base(message, innerexception)
        {
            Logger.LogDebug(string.Format("Exception: {0}, {1}\nInner: {2}\n{3}\n{4}", GetType().Name, message, innerexception.Message, innerexception.Source, StackTrace != null ? StackTrace : ""));
        }
        public WatiNException(SerializationInfo info, StreamingContext context) : base(info, context) {}
	}
}