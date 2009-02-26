#region WatiN Copyright (C) 2006-2009 Jeroen van Menen

//   Copyright 2006-2009 Jeroen van Menen
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
using System.Collections;
using System.Collections.Generic;
using WatiN.Core.Constraints;
using WatiN.Core.Native;

namespace WatiN.Core
{
	/// <summary>
	/// A typed collection of <see cref="Frame" /> instances within a <see cref="Document"/>.
	/// </summary>
	public class FrameCollection : IEnumerable
	{
        private readonly List<Frame> frames;

		public FrameCollection(DomContainer domContainer, INativeDocument htmlDocument)
		{
            frames = new List<Frame>();

            foreach (INativeDocument frameDocument in htmlDocument.Frames)
                frames.Add(new Frame(domContainer, frameDocument));
		}

        [Obsolete("Use Count property instead.")]
		public int Length
		{
			get { return Count; }
		}

        public int Count
		{
			get { return frames.Count; }
		}

		public Frame this[int index]
		{
			get { return frames[index]; }
		}

		public bool Exists(Constraint findBy)
		{
		    return (First(findBy) != null);
		}

        /// <summary>
        /// Returns the first frame of this collection. If colection contains
        /// no frames, null will be returned.
        /// </summary>
        public Frame First()
        {
            return First(Find.First());
        }

        /// <summary>
        /// Find a frame within this collection. If no match is found it will return null.
        /// </summary>
        /// <param name="findBy">The <see cref="AttributeConstraint"/> of the Frame to find.</param>
        public Frame First(Constraint findBy)
        {
            foreach (var frame in frames)
            {
                if (frame.Matches(findBy))
                {
                    // Return
                    return frame;
                }
            }

            return null;
        }

		/// <exclude />
        public IEnumerator GetEnumerator()
		{
			foreach(var element in frames)
			{
			    yield return element;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}