using System;
using System.Collections.Specialized;
using mshtml;
using WatiN.Core.Exceptions;
using WatiN.Core.Interfaces;

namespace WatiN.Core
{
	/// <summary>
	/// Summary description for IEElement.
	/// </summary>
	public class IEElement
	{
		private object _element;
		private readonly DomContainer _domContainer;
		private readonly ElementFinder _elementFinder;
		private ElementAttributeBag attributeBag = new ElementAttributeBag();

		public IEElement(object element, DomContainer domcontainer, ElementFinder elementFinder)
		{
			_element = element;
			_domContainer = domcontainer;
			_elementFinder = elementFinder;
		}

		/// <summary>
		/// Returns the text displayed after this element when it's wrapped
		/// in a Label element; otherwise it returns <c>null</c>.
		/// </summary>
		public string TextAfter
		{
			get { return htmlElement2.getAdjacentText("afterEnd"); }
		}

		/// <summary>
		/// Returns the text displayed before this element when it's wrapped
		/// in a Label element; otherwise it returns <c>null</c>.
		/// </summary>
		public string TextBefore
		{
			get { return htmlElement2.getAdjacentText("beforeBegin"); }
		}

		/// <summary>
		/// Gets the next sibling of this element in the Dom of the HTML page.
		/// </summary>
		/// <value>The next sibling.</value>
		public Element NextSibling
		{
			get
			{
				IHTMLDOMNode node = domNode.nextSibling;
				while (node != null)
				{
					IHTMLElement nextSibling = node as IHTMLElement;
					if (nextSibling != null)
					{
						return WatiN.Core.Element.GetTypedElement(_domContainer, nextSibling);
					}
					else
					{
						node = node.nextSibling;
					}
				}
				return null;
			}
		}

		/// <summary>
		/// Gets the previous sibling of this element in the Dom of the HTML page.
		/// </summary>
		/// <value>The previous sibling.</value>
		public Element PreviousSibling
		{
			get
			{
				IHTMLDOMNode node = domNode.previousSibling;
				while (node != null)
				{
					IHTMLElement previousSibling = node as IHTMLElement;
					if (previousSibling != null)
					{
						return WatiN.Core.Element.GetTypedElement(_domContainer, previousSibling);
					}
					else
					{
						node = node.previousSibling;
					}
				}
				return null;
			}
		}

		/// <summary>
		/// Gets the parent element of this element.
		/// If the parent type is known you can cast it to that type.
		/// </summary>
		/// <value>The parent.</value>
		/// <example>
		/// The following example shows you how to use the parent property.
		/// Assume your web page contains a bit of html that looks something
		/// like this:
		/// 
		/// div
		///   a id="watinlink" href="http://watin.sourceforge.net" /
		///   a href="http://sourceforge.net/projects/watin" /
		/// /div
		/// div
		///   a id="watinfixturelink" href="http://watinfixture.sourceforge.net" /
		///   a href="http://sourceforge.net/projects/watinfixture" /
		/// /div
		/// Now you want to click on the second link of the watin project. Using the 
		/// parent property the code to do this would be:
		/// 
		/// <code>
		/// Div watinDiv = (Div) ie.Link("watinlink").Parent;
		/// watinDiv.Links[1].Click();
		/// </code>
		/// </example>
		public Element Parent
		{
			get
			{
				IHTMLElement parentNode = domNode.parentNode as IHTMLElement;
				if (parentNode != null)
				{
					return WatiN.Core.Element.GetTypedElement(_domContainer, parentNode);
				}
				return null;
			}
		}

		public Style Style
		{
			get { return new Style(htmlElement.style); }
		}

		/// <summary>
		/// This methode can be used if the attribute isn't available as a property of
		/// Element or a subclass of Element.
		/// </summary>
		/// <param name="attributeName">The attribute name. This could be different then named in
		/// the HTML. It should be the name of the property exposed by IE on it's element object.</param>
		/// <returns>The value of the attribute if available; otherwise <c>null</c> is returned.</returns>
		public string GetAttributeValue(string attributeName)
		{
			object attributeValue = htmlElement.getAttribute(attributeName, 0);

			if (attributeValue == DBNull.Value || attributeValue == null)
			{
				return null;
			}

			return attributeValue.ToString();
		}

		public void ClickOnElement()
		{
			DispHtmlBaseElement.click();
		}

		public void SetFocus() 
		{
			DispHtmlBaseElement.focus();
		}

		public void FireEvent(string eventName, NameValueCollection eventProperties)
		{
			if (eventProperties == null)
			{
				UtilityClass.FireEvent(DispHtmlBaseElement, eventName);
			}
			else
			{
				UtilityClass.FireEvent(DispHtmlBaseElement, eventName, eventProperties);
			}
		}

		protected IHTMLElement htmlElement
		{
			get { return (IHTMLElement) _element; }
		}

		private IHTMLElement2 htmlElement2
		{
			get { return (IHTMLElement2) _element; }
		}

		private IHTMLDOMNode domNode
		{
			get { return (IHTMLDOMNode) _element; }
		}

		/// <summary>
		/// Gets the DispHtmlBaseElement />.
		/// </summary>
		/// <value>The DispHtmlBaseElement.</value>
		protected DispHTMLBaseElement DispHtmlBaseElement
		{
			get { return (DispHTMLBaseElement) _element; }
		}

		public string BackgroundColor
		{
			get { return (string) htmlElement.style.backgroundColor; }
			set { htmlElement.style.backgroundColor = value; }
		}

		public object Element
		{
			get { return _element; }
		}

		public IAttributeBag AttributeBag
		{
			get 
			{
				attributeBag.IHTMLElement = htmlElement;
				return attributeBag;
			}
		}

		public bool HasReferenceToAnElement
		{
			get { return _element != null; }
		}

		public bool IsElementReferenceStillValid
		{
			get
			{
				if (htmlElement.sourceIndex < 0)
				{
					return false;
				}
				else
				{
					if (htmlElement.offsetParent == null)
					{
						return false;
					}
				}
				return true;
			}
		}

		public ElementNotFoundException CreateElementNotFoundException()
		{
			return _elementFinder.CreateElementNotFoundException();
		}

		public bool HasElementFinder
		{
			get
			{
				return _elementFinder != null;
			}
		}

		public void FindElement()
		{
			_element = _elementFinder.FindFirst();
		}

		public void ClearElementReference()
		{
			_element = null;
		}
	}
}
