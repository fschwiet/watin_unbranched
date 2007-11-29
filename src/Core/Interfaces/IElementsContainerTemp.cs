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
        IWatiNElementCollection Elements { get; } 

        /// <summary>
        /// Finds an area element using the specified id.
        /// </summary>
        /// <param name="id">The id of the area element being sought.</param>
        /// <returns>The area element for the corresponding id, or null if none is found</returns>
        IArea Area(string id);

        /// <summary>
        /// Finds a button element using the specified id.
        /// </summary>
        /// <param name="id">The id of the button element being sought.</param>
        /// <returns>The button element for the corresponding id, or null if none is found</returns>
        IButton Button(string id);

        /// <summary>
        /// Finds a div element using the specified id.
        /// </summary>
        /// <param name="id">The id of the div element being sought.</param>
        /// <returns>The div element for the corresponding id, or null if none is found</returns>
        IDiv Div(string id);

        /// <summary>
        /// Finds a div element using the specified id.
        /// </summary>
        /// <param name="id">The id of the link element being sought.</param>
        /// <returns>The link element for the corresponding id, or null if none is found</returns>
        ILink Link(string id);

        /// <summary>
        /// Finds a paragraph element using the specified id.
        /// </summary>
        /// <param name="id">The id of the paragraph element being sought.</param>
        /// <returns>The paragraph element for the corresponding id, or null if none is found</returns>
        IPara Para(string id);

        /// <summary>
        /// Finds a table using the specified Id.
        /// </summary>
        /// <param name="id">The id of the table element being sought.</param>
        /// <returns>The table element for the corresponding id, or null if none is found</returns>
        ITable Table(string id);

        /// <summary>
        /// Finds a text field using the Id.
        /// </summary>
        /// <param name="id">The id of the text field element being sought.</param>
        /// <returns>The text field element for the corresponding id, or null if none is found</returns>
        ITextField TextField(string id);

        /// <summary>
        /// Finds an element matching the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The element for the corresponding id, or null if none is found</returns>
        IElement Element(string id);
    }
}
