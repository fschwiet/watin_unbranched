#region WatiN Copyright (C) 2006-2007 Jeroen van Menen

//Copyright 2006-2007 Jeroen van Menen
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

using System.Collections.Specialized;
using WatiN.Core.Interfaces;

namespace WatiN.Core.UnitTests
{
	public class MockAttributeBag : IAttributeBag
	{
		public NameValueCollection attributeValues = new NameValueCollection();

		public MockAttributeBag(string attributeName, string value)
		{
			Add(attributeName, value);
		}

		public void Add(string attributeName, string value)
		{
			attributeValues.Add(attributeName.ToLower(), value);
		}

		public string GetValue(string attributename)
		{
			return attributeValues.Get(attributename.ToLower());
		}
	}
}