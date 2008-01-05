#region WatiN Copyright (C) 2006-2008 Jeroen van Menen

//Copyright 2006-2008 Jeroen van Menen
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

using System.Collections;
using mshtml;

namespace WatiN.Core
{
	/// <summary>
	/// Represents an area of an image map.
	/// </summary>
	public class Area : Element
	{
		private static ArrayList elementTags;

		public static ArrayList ElementTags
		{
			get
			{
				if (elementTags == null)
				{
					elementTags = new ArrayList();
					elementTags.Add(new ElementTag("area"));
				}

				return elementTags;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Area" /> class.
		/// Mainly used by WatiN internally.
		/// </summary>
		/// <param name="domContainer">The <see cref="DomContainer" /> the element is in.</param>
		/// <param name="element">The element.</param>
		public Area(DomContainer domContainer, IHTMLAreaElement element) : base(domContainer, element) {}

		/// <summary>
		/// Initializes a new instance of the <see cref="Area" /> class.
		/// Mainly used by WatiN internally.
		/// </summary>
		/// <param name="domContainer">The <see cref="DomContainer" /> the element is in.</param>
		/// <param name="elementFinder">The element finder.</param>
		public Area(DomContainer domContainer, ElementFinder elementFinder) : base(domContainer, elementFinder) {}

		/// <summary>
		/// Initializes a new instance of the <see cref="Area"/> class based on <paramref name="element"/>.
		/// </summary>
		/// <param name="element">The element.</param>
		public Area(Element element) : base(element, ElementTags) {}

		/// <summary>
		/// Gets the alt-text of the area element.
		/// </summary>
		public string Alt
		{
			get { return ((IHTMLAreaElement) HTMLElement).alt; }
		}

		/// <summary>
		/// Gets the target url of the area element.
		/// </summary>
		public string Url
		{
			get { return ((IHTMLAreaElement) HTMLElement).href; }
		}

		/// <summary>
		/// Gets the coordinates the area element.
		/// </summary>
		public string Coords
		{
			get { return ((IHTMLAreaElement) HTMLElement).coords; }
		}

		/// <summary>
		/// Gets the shape of the area element.
		/// </summary>
		public string Shape
		{
			get { return ((IHTMLAreaElement) HTMLElement).shape; }
		}
	}
}