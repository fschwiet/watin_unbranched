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

using System;
using System.Collections;
using System.Threading;
using mshtml;
using WatiN.Core.Constraints;
using WatiN.Core.InternetExplorer;
using StringComparer = WatiN.Core.Comparers.StringComparer;
using WatiN.Core.Exceptions;
using WatiN.Core.Interfaces;

namespace WatiN.Core.InternetExplorer
{
	/// <summary>
	/// This class is mainly used internally by WatiN to find elements in
	/// an <see cref="IHTMLElementCollection"/> or <see cref="ArrayList"/> matching
	/// the given <see cref="BaseConstraint"/>.
	/// </summary>
	public class IEElementFinder : INativeElementFinder
	{
		private ArrayList tagsToFind = new ArrayList();

		protected readonly BaseConstraint findBy;
		protected readonly IElementCollection elementCollection;

		public IEElementFinder(ArrayList elementTags, BaseConstraint findBy, IElementCollection elementCollection)
		{
			if (elementCollection == null)
			{
				throw new ArgumentNullException("elementCollection");
			}

			this.findBy = getFindBy(findBy);
			this.elementCollection = elementCollection;

			if (elementTags != null)
			{
				tagsToFind = elementTags;
			}
			else
			{
				AddElementTag(null, null);
			}
		}

		public IEElementFinder(ArrayList elementTags, IElementCollection elementCollection) : this(elementTags, null, elementCollection) {}

		public IEElementFinder(string tagName, string inputType, BaseConstraint findBy, IElementCollection elementCollection)
		{
			if (elementCollection == null)
			{
				throw new ArgumentNullException("elementCollection");
			}

			this.findBy = getFindBy(findBy);
			this.elementCollection = elementCollection;

			AddElementTag(tagName, inputType);
		}

		public IEElementFinder(string tagName, string inputType, IElementCollection elementCollection) : this(tagName, inputType, null, elementCollection) {}

		public virtual INativeElement FindFirst()
		{
			foreach (ElementTag elementTag in tagsToFind)
			{
				ArrayList elements = findElementsByAttribute(elementTag, findBy, true);

				if (elements.Count > 0)
				{
					return new IEElement(elements[0]);
				}
			}

			return null;
		}

		public string ElementTagsToString
		{
			get { return ElementTag.ElementTagsToString(tagsToFind); }
		}

		public string ConstriantToString
		{
			get { return findBy.ConstraintToString(); }
		}

		internal ElementNotFoundException CreateElementNotFoundException(Exception innerexception)
		{
			return new ElementNotFoundException(ElementTag.ElementTagsToString(tagsToFind), findBy.ConstraintToString(), innerexception);
		}

		public void AddElementTag(string tagName, string inputType)
		{
			tagsToFind.Add(new ElementTag(tagName, inputType));
		}

		public ArrayList FindAll()
		{
			return FindAll(findBy);
		}

		public ArrayList FindAll(BaseConstraint findBy)
		{
			if (tagsToFind.Count == 1)
			{
				return findElementsByAttribute((ElementTag) tagsToFind[0], findBy, false);
			}
			else
			{
				ArrayList elements = new ArrayList();

				foreach (ElementTag elementTag in tagsToFind)
				{
					elements.AddRange(findElementsByAttribute(elementTag, findBy, false));
				}

				return elements;
			}
		}

		private static BaseConstraint getFindBy(BaseConstraint findBy)
		{
			if (findBy == null)
			{
				return new AlwaysTrueConstraint();
			}
			return findBy;
		}

		private ArrayList findElementsByAttribute(ElementTag elementTag, BaseConstraint findBy, bool returnAfterFirstMatch)
		{
			// Get elements with the tagname from the page
			ArrayList children = new ArrayList();
			IHTMLElementCollection elements = elementTag.GetElementCollection(elementCollection.Elements);

			findBy.Reset();

			if (elements != null)
			{
				ElementAttributeBag attributeBag = new ElementAttributeBag();

				// Loop through each element and evaluate
				foreach (IHTMLElement element in elements)
				{
					IEElement ieElement = new IEElement(element, null);
					waitUntilElementReadyStateIsComplete(element);

					attributeBag.IHTMLElement = element;

					if (elementTag.Compare(ieElement) && findBy.Compare(attributeBag))
					{
						children.Add(element);
						if (returnAfterFirstMatch)
						{
							return children;
						}
					}
				}
			}

			return children;
		}

		private static void waitUntilElementReadyStateIsComplete(IHTMLElement element)
		{
			//TODO: See if this method could be dropped, it seems to give
			//      more trouble (uninitialized state of elements)
			//      then benefits (I just introduced this method to be on 
			//      the save side)

			if (ElementTag.IsValidElement(element, Image.ElementTags))
			{
				return;
			}

			// Wait if the readystate of an element is BETWEEN
			// Uninitialized and Complete. If it's uninitialized,
			// it's quite probable that it will never reach Complete.
			// Like for elements that could not load an image or ico
			// or some other bits not part of the HTML page.     
			SimpleTimer timeoutTimer = new SimpleTimer(30);

			do
			{
				int readyState = ((IHTMLElement2) element).readyStateValue;

				if (readyState == 0 || readyState == 4)
				{
					return;
				}

				Thread.Sleep(100);
			} while (!timeoutTimer.Elapsed);

			throw new WatiNException("Element didn't reach readystate = complete within 30 seconds: " + element.outerText);
		}
	}
}