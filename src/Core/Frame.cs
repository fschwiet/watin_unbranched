#region WatiN Copyright (C) 2006-2009 Jeroen van Menen

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

using WatiN.Core.Constraints;
using WatiN.Core.Exceptions;
using WatiN.Core.Interfaces;

namespace WatiN.Core
{
	/// <summary>
	/// This class provides specialized functionality for a Frame or IFrame.
	/// </summary>
	public class Frame : Document, IAttributeBag
	{
		private readonly Element _frameElement;

		/// <summary>
		/// This constructor will mainly be used by the constructor of FrameCollection
		/// to create an instance of a Frame.
		/// </summary>
		/// <param name="domContainer">The domContainer.</param>
		/// <param name="htmlDocument">The HTML document.</param>
		/// <param name="frameElement"></param>
		public Frame(DomContainer domContainer, INativeDocument htmlDocument, Element frameElement) : base(domContainer, htmlDocument)
		{
		    _frameElement = frameElement;
		}

		/// <summary>
		/// This constructor will mainly be used by Document.Frame to find
		/// a Frame. A FrameNotFoundException will be thrown if the Frame isn't found.
		/// </summary>
		/// <param name="frames">Collection of frames to find the frame in</param>
		/// <param name="findBy">The <see cref="AttributeConstraint"/> of the Frame to find (Find.ByUrl, Find.ByName and Find.ById are supported)</param>
		public static Frame Find(FrameCollection frames, BaseConstraint findBy)
		{
			foreach (Frame frame in frames)
			{
				if (findBy.Compare(frame))
				{
					// Return
					return frame;
				}
			}

			throw new FrameNotFoundException(findBy.ConstraintToString());
		}

		public string Name
		{
			get { return GetAttributeValue("name"); }
		}

		public string Id
		{
			get { return GetAttributeValue("id"); }
		}

		public string GetAttributeValue(string attributename)
		{
            switch (attributename.ToLowerInvariant())
            {
                case "url":
                    return Url;

                case "href":
                    return Url;

                default:
                    return _frameElement.GetAttributeValue(attributename);
            }
		}

	    public string GetValue(string attributename)
		{
			return GetAttributeValue(attributename);
		}
	}
}
