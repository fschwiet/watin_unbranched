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

using System;
using System.Collections.Generic;
using System.Text;
using WatiN.Core.Mozilla;

namespace WatiN.Core.Interfaces
{
    /// <summary>
    /// Temp. interface, will be removed and <see cref="IDocument"/> will use <see cref="IElementsContainer"/>
    /// when the implementation of <see cref="Mozilla.Document" /> supports all of the functionality.
    /// </summary>
    public interface IElementsContainerTemp
    {
        /// <summary>
        /// Finds an area element using the specified id.
        /// </summary>
        /// <param name="id">The area element being sought.</param>
        /// <returns></returns>
        IArea Area(string id);

        /// <summary>
        /// Finds a div element using the specified id.
        /// </summary>
        /// <param name="id">The id div element being sought.</param>
        /// <returns></returns>
        IDiv Div(string id);

        /// <summary>
        /// Finds a div element using the specified id.
        /// </summary>
        /// <param name="id">The id of the link element being sought.</param>
        /// <returns></returns>
        ILink Link(string id);

        /// <summary>
        /// Finds a paragraph element using the specified id.
        /// </summary>
        /// <param name="id">The id of the paragraph element being sought.</param>
        /// <returns></returns>
        IPara Para(string id);

        /// <summary>
        /// Finds a text field using the Id.
        /// </summary>
        /// <param name="id">The id of the text field element being sought.</param>
        /// <returns>A text field for the specified id</returns>
        ITextField TextField(string id);

        /// <summary>
        /// Finds an element matching the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        IElement Element(string id);
    }
}
