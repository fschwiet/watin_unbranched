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

namespace WatiN.Core.Interfaces
{
    public interface IElement
    {
        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <value>The id.</value>
        string Id { get; }

        /// <summary>
        /// Gets the name of the element's css class.
        /// </summary>
        /// <value>The name of the element's class.</value>
        string ClassName { get; }
        
        /// <summary>
        /// Gets the value of the attribute
        /// </summary>
        /// <param name="attributeName">The attribute name</param>
        /// <returns></returns>
        string GetAttributeValue(string attributeName);
    }
}
