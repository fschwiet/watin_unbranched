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

using WatiN.Core.Interfaces;

namespace WatiN.Core.Mozilla
{
    /// <summary>
    /// Represents an area of an image map.
    /// </summary>
    public class Area : Element, IArea
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Area"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="clientPort">The client port.</param>
        public Area(string id, FireFoxClientPort clientPort) : base(id, clientPort)
        {
        }

        /// <summary>
        /// Gets the alt-text of the area element.
        /// </summary>
        /// <value></value>
        public string Alt
        {
            get { return GetAttributeValue("alt"); }
        }

        /// <summary>
        /// Gets the target url of the area element.
        /// </summary>
        /// <value></value>
        public string Url
        {
            get { return GetAttributeValue("href"); }
        }

        /// <summary>
        /// Gets the coordinates the area element.
        /// </summary>
        /// <value></value>
        public string Coords
        {
            get { return GetAttributeValue("coords"); }
        }

        /// <summary>
        /// Gets the shape of the area element.
        /// </summary>
        /// <value></value>
        public string Shape
        {
            get { return GetAttributeValue("shape"); }
        }
    }
}