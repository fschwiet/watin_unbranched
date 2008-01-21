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
using System.Collections.Specialized;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using mshtml;
using WatiN.Core.Comparers;
using WatiN.Core.Constraints;
using WatiN.Core.Exceptions;
using WatiN.Core.Interfaces;
using WatiN.Core.Logging;

namespace WatiN.Core
{
	/// <summary>
	/// This is the base class for all other element types in this project, like
	/// Button, Checkbox etc.. It provides common functionality to all these elements
	/// </summary>
	public class Element : IAttributeBag
	{
		private static Hashtable _elementConstructors = null;

		private DomContainer _domContainer;
		private IBrowserElement _browserElement;

		private Stack _originalcolor;

		/// <summary>
		/// This constructor is mainly used from within WatiN.
		/// </summary>
		/// <param name="domContainer"><see cref="DomContainer" /> this element is located in</param>
		/// <param name="element">The element</param>
		public Element(DomContainer domContainer, object element)
		{
			init(domContainer, element, null);
		}

		/// <summary>
		/// This constructor is mainly used from within WatiN.
		/// </summary>
		/// <param name="domContainer"><see cref="DomContainer" /> this element is located in</param>
		/// <param name="browserElement">The element</param>
		public Element(DomContainer domContainer, IBrowserElement browserElement)
		{
			initElement(domContainer, browserElement);
		}

		/// <summary>
		/// This constructor is mainly used from within WatiN.
		/// </summary>
		/// <param name="domContainer"><see cref="DomContainer"/> this element is located in</param>
		/// <param name="elementFinder">The element finder.</param>
		public Element(DomContainer domContainer, ElementFinder elementFinder)
		{
			init(domContainer, null, elementFinder);
		}

		/// <summary>
		/// This constructor is mainly used from within WatiN.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <param name="elementTags">The element tags the element should match with.</param>
		public Element(Element element, ArrayList elementTags)
		{
			if (ElementTag.IsValidElement(element.BrowserElement, elementTags))
			{
				initElement(element._domContainer, element._browserElement);
			}
			else
			{
				throw new ArgumentException(String.Format("Expected element {0}", ElementFinder.GetExceptionMessage(elementTags)), "element");
			}
		}

		private void initElement(DomContainer domContainer, IBrowserElement browserElement) 
		{
			_browserElement = browserElement;
			_domContainer = domContainer;
		}

		// TODO: This method should be removed
		private void init(DomContainer domContainer, object element, ElementFinder elementFinder)
		{
			initElement(domContainer, new IEElement(element, elementFinder));
		}

		/// <summary>
		/// Gets the name of the stylesheet class assigned to this ellement (if any).
		/// </summary>
		/// <value>The name of the class.</value>
		public string ClassName
		{
			get { return GetAttributeValue("className"); }
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="Element"/> is completely loaded.
		/// </summary>
		/// <value><c>true</c> if complete; otherwise, <c>false</c>.</value>
		public bool Complete
		{
			get
		{
				string readyStateValue = GetAttributeValue("readyStateValue"); 
				if (readyStateValue != null)
				{
					return int.Parse(readyStateValue) == 4;
				}
				return false;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="Element"/> is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		public bool Enabled
		{
			get { return !bool.Parse(GetAttributeValue("disabled")); }
		}

		/// <summary>
		/// Gets the id of this element as specified in the HTML.
		/// </summary>
		/// <value>The id.</value>
		public string Id
		{
			get { return GetAttributeValue("id"); }
		}

		/// <summary>
		/// Gets the innertext of this element (and the innertext of all the elements contained
		/// in this element).
		/// </summary>
		/// <value>The innertext.</value>
		public virtual string Text
		{
			get { return GetAttributeValue("innertext"); }
		}

		/// <summary>
		/// Returns the text displayed after this element when it's wrapped
		/// in a Label element; otherwise it returns <c>null</c>.
		/// </summary>
		public string TextAfter
		{
			get { return BrowserElement.TextAfter; }
		}

		/// <summary>
		/// Returns the text displayed before this element when it's wrapped
		/// in a Label element; otherwise it returns <c>null</c>.
		/// </summary>
		public string TextBefore
		{
			get { return BrowserElement.TextBefore; }
		}

		/// <summary>
		/// Gets the inner HTML of this element.
		/// </summary>
		/// <value>The inner HTML.</value>
		public string InnerHtml
		{
			get { return GetAttributeValue("innerHTML"); }
		}

		/// <summary>
		/// Gets the outer text.
		/// </summary>
		/// <value>The outer text.</value>
		public string OuterText
		{
			get { return GetAttributeValue("outerText"); }
		}

		/// <summary>
		/// Gets the outer HTML.
		/// </summary>
		/// <value>The outer HTML.</value>
		public string OuterHtml
		{
			get { return GetAttributeValue("outerHTML"); }
		}

		/// <summary>
		/// Gets the tag name of this element.
		/// </summary>
		/// <value>The name of the tag.</value>
		public string TagName
		{
			get { return GetAttributeValue("tagName"); }
		}

		/// <summary>
		/// Gets the title.
		/// </summary>
		/// <value>The title.</value>
		public string Title
		{
			get { return GetAttributeValue("title"); }
		}

		/// <summary>
		/// Gets the next sibling of this element in the Dom of the HTML page.
		/// </summary>
		/// <value>The next sibling.</value>
		public Element NextSibling
		{
			get
			{
				return GetTypedElement(_domContainer, BrowserElement.NextSibling);
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
                return GetTypedElement(_domContainer, BrowserElement.PreviousSibling);
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
                return GetTypedElement(_domContainer, BrowserElement.Parent);
            }
        }

		public Style Style
		{
			//TODO: Style class should also delegate to a browser specific instance
			get { return BrowserElement.Style; }
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
			if (UtilityClass.IsNullOrEmpty(attributeName))
			{
				throw new ArgumentNullException("attributeName", "Null or Empty not allowed.");
			}

			if (string.Compare(attributeName, "style", true) == 0)
			{
				return Style.CssText;
			}

			return BrowserElement.GetAttributeValue(attributeName);
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </returns>
		public override string ToString()
		{
			if (UtilityClass.IsNotNullOrEmpty(Title))
			{
				return Title;
			}
			return Text;
		}

		/// <summary>
		/// Clicks this element and waits till the event is completely finished (page is loaded 
		/// and ready) .
		/// </summary>
		public void Click()
		{
			if (!Enabled)
			{
				throw new ElementDisabledException(Id);
			}

			Logger.LogAction("Clicking " + GetType().Name + " '" + ToString() + "'");
			Highlight(true);

			BrowserElement.ClickOnElement();

			try
			{
				WaitForComplete();
			}
			finally
			{
				Highlight(false);
			}
		}

		/// <summary>
		/// Clicks this instance and returns immediately. Use this method when you want to continue without waiting
		/// for the click event to be finished. Mostly used when a 
		/// HTMLDialog is displayed after clicking the element.
		/// </summary>
		public void ClickNoWait()
		{
			if (!Enabled)
			{
				throw new ElementDisabledException(Id);
			}

			Logger.LogAction("Clicking (no wait) " + GetType().Name + " '" + ToString() + "'");

			Highlight(true);

			Thread clickButton = new Thread(new ThreadStart(BrowserElement.ClickOnElement));
			clickButton.Start();
			clickButton.Join(500);

			Highlight(false);
		}

		/// <summary>
		/// Gives the (input) focus to this element.
		/// </summary>
		public void Focus()
		{
			if (!Enabled)
			{
				throw new ElementDisabledException(Id);
			}

			BrowserElement.SetFocus();
			FireEvent("onFocus");
		}

		/// <summary>
		/// Doubleclicks this element.
		/// </summary>
		public void DoubleClick()
		{
			if (!Enabled)
			{
				throw new ElementDisabledException(Id);
			}

			Logger.LogAction("Double clicking " + GetType().Name + " '" + ToString() + "'");

			FireEvent("onDblClick");
		}

		/// <summary>
		/// Does a keydown on this element.
		/// </summary>
		public void KeyDown()
		{
			FireEvent("onKeyDown");
		}

		/// <summary>
		/// Does a keydown on this element.
		/// </summary>
		public void KeyDown(char character)
		{
			FireEvent("onKeyDown", GetKeyCodeEventProperty(character));
		}

		/// <summary>
		/// Does a keyspress on this element.
		/// </summary>
		public void KeyPress()
		{
			FireEvent("onKeyPress");
		}

		public void KeyPress(char character)
		{
			FireEvent("onKeyPress", GetKeyCodeEventProperty(character));
		}

		private static NameValueCollection GetKeyCodeEventProperty(char character)
		{
			NameValueCollection eventProperties = new NameValueCollection(1);
			eventProperties.Add("keyCode", ((int) character).ToString());
			return eventProperties;
		}

		/// <summary>
		/// Does a keyup on this element.
		/// </summary>
		public void KeyUp()
		{
			FireEvent("onKeyUp");
		}

		/// <summary>
		/// Does a keyup on this element.
		/// </summary>
		/// <param name="character">The character.</param>
		public void KeyUp(char character)
		{
			FireEvent("onKeyUp", GetKeyCodeEventProperty(character));
		}


		/// <summary>
		/// Fires the blur event on this element.
		/// </summary>
		public void Blur()
		{
			FireEvent("onBlur");
		}

		/// <summary>
		/// Fires the change event on this element.
		/// </summary>
		public void Change()
		{
			FireEvent("onChange");
		}

		/// <summary>
		/// Fires the mouseenter event on this element.
		/// </summary>
		public void MouseEnter()
		{
			FireEvent("onMouseEnter");
		}

		/// <summary>
		/// Fires the mousedown event on this element.
		/// </summary>
		public void MouseDown()
		{
			FireEvent("onmousedown");
		}

		/// <summary>
		/// Fires the mouseup event on this element.
		/// </summary>
		public void MouseUp()
		{
			FireEvent("onmouseup");
		}

		/// <summary>
		/// Fires the specified <paramref name="eventName"/> on this element
		/// and waits for it to complete.
		/// </summary>
		/// <param name="eventName">Name of the event.</param>
		public void FireEvent(string eventName)
		{
			fireEvent(eventName, true, null);
		}

		/// <summary>
		/// Fires the event. The <paramref name="eventProperties" /> collection
		/// can be used to set values of the event object in the browser to 
		/// full fill the needs of javascript attached to the event handler.
		/// </summary>
		/// <param name="eventName">Name of the event.</param>
		/// <param name="eventProperties">The event properties that need to be set.</param>
		public void FireEvent(string eventName, NameValueCollection eventProperties)
		{
			fireEvent(eventName, true, eventProperties);
		}

		/// <summary>
		/// Only fires the specified <paramref name="eventName"/> on this element.
		/// </summary>
		public void FireEventNoWait(string eventName)
		{
			fireEvent(eventName, false, null);
		}

		private void fireEvent(string eventName, bool waitForComplete, NameValueCollection eventProperties)
		{
			if (!Enabled)
			{
				throw new ElementDisabledException(Id);
			}

			Highlight(true);

			BrowserElement.FireEvent(eventName, eventProperties);

			if (waitForComplete)
			{
				WaitForComplete();
			}
			Highlight(false);
		}

		/// <summary>
		/// Flashes this element 5 times.
		/// </summary>
		public void Flash()
		{
			Flash(5);
		}

		/// <summary>
		/// Flashes this element the specified number of flashes.
		/// </summary>
		/// <param name="numberOfFlashes">The number of flashes.</param>
		public void Flash(int numberOfFlashes)
		{
			for (int counter = 0; counter < numberOfFlashes; counter++)
			{
				Highlight(true);
				Thread.Sleep(250);
				Highlight(false);
				Thread.Sleep(250);
			}
		}

		/// <summary>
		/// Highlights this element.
		/// </summary>
		/// <param name="doHighlight">if set to <c>true</c> the element is highlighted; otherwise it's not.</param>
		public void Highlight(bool doHighlight)
		{
			if (IE.Settings.HighLightElement)
			{
				if (_originalcolor == null)
				{
					_originalcolor = new Stack();
				}

				if (doHighlight)
				{
					_originalcolor.Push(BrowserElement.BackgroundColor);
					SetBackgroundColor(IE.Settings.HighLightColor);
				}
				else
				{
					if(_originalcolor.Count > 0)
					{
						SetBackgroundColor(_originalcolor.Pop() as string);
					}
				}
			}
		}

		private void SetBackgroundColor(string color) 
		{
			try
			{
				if (color != null)
				{
					BrowserElement.BackgroundColor = color;
				}
				else
				{
					BrowserElement.BackgroundColor = "";
				}				
			}
			catch{}
		}

		// TODO: Remove this property or move it to IEElement
		protected IHTMLElement htmlElement
		{
			get { return (IHTMLElement) HTMLElement; }
		}

		/// <summary>
		/// Gets the DOMcontainer for this element.
		/// </summary>
		/// <value>The DOM container.</value>
		public DomContainer DomContainer
		{
			get { return _domContainer; }
		}

		//TODO: Should return IBrowserElement instead of object

		/// <summary>
		/// Gets the DOM HTML element for this instance as an object. Cast it to 
		/// the interface you need. Most of the time the object supports IHTMLELement, 
		/// IHTMLElement2 and IHTMLElement3 but you can also cast it to a more
		/// specific interface. You should reference the microsoft.MSHTML.dll 
		/// assembly to cast it to a valid type.
		/// </summary>
		/// <value>The DOM element.</value>
		public object HTMLElement
		{
			get
			{
				return ((IEElement)BrowserElement).Element;
			}
		}

		/// <summary>
		/// Gets the DOM HTML element for this instance as an object. Cast it to 
		/// the interface you need. Most of the time the object supports IHTMLELement, 
		/// IHTMLElement2 and IHTMLElement3 but you can also cast it to a more
		/// specific interface. You should reference the microsoft.MSHTML.dll 
		/// assembly to cast it to a valid type.
		/// </summary>
		/// <value>The DOM element.</value>
		public IBrowserElement BrowserElement
		{
			get
			{
				if (!_browserElement.HasReferenceToAnElement)
				{
					try
					{
						WaitUntilExists();
					}
					catch (WatiN.Core.Exceptions.TimeoutException)
					{
						throw _browserElement.CreateElementNotFoundException();
					}
				}

				return _browserElement;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="Element"/> exists.
		/// </summary>
		/// <value><c>true</c> if exists; otherwise, <c>false</c>.</value>
		public virtual bool Exists
		{
			get
			{
				return RefreshBrowserElement().HasReferenceToAnElement;
			}
		}

		/// <summary>
		/// Waits until the element exists or will time out after 30 seconds.
		/// To change the default time out, set <see cref="P:WatiN.Core.IE.Settings.WaitUntilExistsTimeOut"/>
		/// </summary>
		public void WaitUntilExists()
		{
			// Wait 30 seconds max
			WaitUntilExists(IE.Settings.WaitUntilExistsTimeOut);
		}

		/// <summary>
		/// Waits until the element exists. Wait will time out after <paramref name="timeout"/> seconds.
		/// </summary>
		/// <param name="timeout">The timeout in seconds.</param>
		public void WaitUntilExists(int timeout)
		{
			waitUntilExistsOrNot(timeout, true);
		}

		/// <summary>
		/// Waits until the element no longer exists or will time out after 30 seconds.
		/// To change the default time out, set <see cref="P:WatiN.Core.IE.Settings.WaitUntilExistsTimeOut"/>
		/// </summary>
		public void WaitUntilRemoved()
		{
			// Wait 30 seconds max
			WaitUntilRemoved(IE.Settings.WaitUntilExistsTimeOut);
		}

		/// <summary>
		/// Waits until the element no longer exists. Wait will time out after <paramref name="timeout"/> seconds.
		/// </summary>
		/// <param name="timeout">The timeout in seconds.</param>
		public void WaitUntilRemoved(int timeout)
		{
			waitUntilExistsOrNot(timeout, false);
		}

		/// <summary>
		/// Waits until the given <paramref name="attributename" /> matches <paramref name="expectedValue" />.
		/// Wait will time out after <see cref="Settings.WaitUntilExistsTimeOut"/> seconds.
		/// </summary>
		/// <param name="attributename">The attributename.</param>
		/// <param name="expectedValue">The expected value.</param>
		public void WaitUntil(string attributename, string expectedValue)
		{
			WaitUntil(attributename, expectedValue, IE.Settings.WaitUntilExistsTimeOut);
		}

		/// <summary>
		/// Waits until the given <paramref name="attributename" /> matches <paramref name="expectedValue" />.
		/// Wait will time out after <paramref name="timeout"/> seconds.
		/// </summary>
		/// <param name="attributename">The attributename.</param>
		/// <param name="expectedValue">The expected value.</param>
		/// <param name="timeout">The timeout.</param>
		public void WaitUntil(string attributename, string expectedValue, int timeout)
		{
			WaitUntil(new AttributeConstraint(attributename, expectedValue), timeout);
		}

		/// <summary>
		/// Waits until the given <paramref name="attributename" /> matches <paramref name="expectedValue" />.
		/// Wait will time out after <see cref="Settings.WaitUntilExistsTimeOut"/> seconds.
		/// </summary>
		/// <param name="attributename">The attributename.</param>
		/// <param name="expectedValue">The expected value.</param>
		public void WaitUntil(string attributename, Regex expectedValue)
		{
			WaitUntil(attributename, expectedValue, IE.Settings.WaitUntilExistsTimeOut);
		}

		/// <summary>
		/// Waits until the given <paramref name="attributename" /> matches <paramref name="expectedValue" />.
		/// Wait will time out after <paramref name="timeout"/> seconds.
		/// </summary>
		/// <param name="attributename">The attributename.</param>
		/// <param name="expectedValue">The expected value.</param>
		/// <param name="timeout">The timeout.</param>
		public void WaitUntil(string attributename, Regex expectedValue, int timeout)
		{
			WaitUntil(new AttributeConstraint(attributename, expectedValue), timeout);
		}

		/// <summary>
		/// Waits until the given <paramref name="constraint" /> matches.
		/// Wait will time out after <see cref="Settings.WaitUntilExistsTimeOut"/> seconds.
		/// </summary>
		/// <param name="constraint">The BaseConstraint.</param>
		public void WaitUntil(BaseConstraint constraint)
		{
			WaitUntil(constraint, IE.Settings.WaitUntilExistsTimeOut);
		}

		/// <summary>
		/// Waits until the given <paramref name="constraint" /> matches.
		/// </summary>
		/// <param name="constraint">The BaseConstraint.</param>
		/// <param name="timeout">The timeout.</param>
		public void WaitUntil(BaseConstraint constraint, int timeout)
		{
			Exception lastException;


			SimpleTimer timeoutTimer = new SimpleTimer(timeout);

			do
			{
				lastException = null;

				try
				{
					// Calling Exists will refresh the reference to the html element
					// so the compare is against the current html element (and not 
					// against some cached reference.
					if (Exists)
					{
						if (constraint.Compare(BrowserElement.AttributeBag))
						{
							return;
						}
					}
				}
				catch (Exception e)
				{
					lastException = e;
				}

				Thread.Sleep(200);
			} while (!timeoutTimer.Elapsed);

			ThrowTimeOutException(lastException, string.Format("waiting {0} seconds for element matching constraint: {1}", timeout, constraint.ConstraintToString()));
		}

		private static void ThrowTimeOutException(Exception lastException, string message)
		{
			if (lastException != null)
			{
				throw new WatiN.Core.Exceptions.TimeoutException(message, lastException);
			}
			else
			{
				throw new WatiN.Core.Exceptions.TimeoutException(message);
			}
		}

		private void waitUntilExistsOrNot(int timeout, bool waitUntilExists)
		{
			// Does it make sense to go into the do loop?
			if (waitUntilExists)
			{
				if (_browserElement.HasReferenceToAnElement)
				{
					return;
				}
				else if (_browserElement.HasElementFinder == false)
				{
					throw new WatiNException("It's not possible to find the element because no elementFinder is available.");
				}
			}
			else
			{
				if (!_browserElement.HasReferenceToAnElement)
				{
					return;
				}
			}

			Exception lastException;
			SimpleTimer timeoutTimer = new SimpleTimer(timeout);

			do
			{
				lastException = null;

				try
				{
					if (Exists == waitUntilExists)
					{
						return;
					}
				}
				catch (Exception e)
				{
					lastException = e;
				}

				Thread.Sleep(200);
			} while (!timeoutTimer.Elapsed);

			ThrowTimeOutException(lastException, string.Format("waiting {0} seconds for element to {1}.", timeout, waitUntilExists ? "show up" : "disappear"));
		}

		/// <summary>
		/// Call this method to make sure the cached reference to the html element on the web page
		/// is refreshed on the next call you make to a property or method of this element.
		/// When you want to check if an element still <see cref="Exists"/> you don't need 
		/// to call the <see cref="Refresh"/> method since <see cref="Exists"/> calls it internally.
		/// </summary>
		/// <example>
		/// The following code shows in which situation you can make use of the refresh mode.
		/// The code selects an option of a selectlist and then checks if this option
		/// is selected.
		/// <code>
		/// SelectList select = ie.SelectList(id);
		/// 
		/// // Lets assume calling Select causes a postback, 
		/// // which would invalidate the reference to the html selectlist.
		/// select.Select(val);
		/// 
		/// // Refresh will clear the cached reference to the html selectlist.
		/// select.Refresh(); 
		/// 
		/// // B.t.w. executing:
		/// // select = ie.SelectList(id); 
		/// // would have the same effect as calling: 
		/// // select.Refresh().
		/// 
		/// // Assert against a freshly fetched reference to the html selectlist.
		/// Assert.AreEqual(val, select.SelectedItem);
		/// </code>
		/// If you need to use refresh on an element retrieved from a collection of elements you 
		/// need to rewrite your code a bit.
		/// <code>
		/// SelectList select = ie.Spans[1].SelectList(id);
		/// select.Refresh(); // this will not have the expected effect
		/// </code>
		/// Rewrite your code as follows:
		/// <code>
		/// SelectList select = ie.Span(Find.ByIndex(1)).SelectList(id);
		/// select.Refresh(); // this will have the expected effect
		/// </code>
		/// </example>
		public void Refresh()
		{
			if (_browserElement.HasElementFinder)
			{
				_browserElement.ClearElementReference();
			}
		}

		/// <summary>
		/// Calls <see cref="Refresh"/> and returns the element.
		/// </summary>
		/// <returns></returns>
		protected IBrowserElement RefreshBrowserElement()
		{
			if (_browserElement.HasElementFinder)
			{
				_browserElement.FindElement();
			}
			else
			{
				// This will set element to null if some specific properties are
				// a certain value. These values indicate that the element is no longer
				// on the/a valid web page.
				// These checks are only necessary if element field
				// is set during the construction of an ElementCollection
				// or a more specialized element collection (like TextFieldCollection)
				if (_browserElement.HasReferenceToAnElement)
				{
					try
					{
						if (_browserElement.IsElementReferenceStillValid == false)
						{
							_browserElement.ClearElementReference();
						}
					}
					catch
					{
						_browserElement.ClearElementReference();
					}
				}
			}

			return _browserElement;
		}

		/// <summary>
		/// Waits till the page load is complete. This should only be used on rare occasions
		/// because WatiN calls this function for you when it handles events (like Click etc..)
		/// To change the default time out, set <see cref="P:WatiN.Core.IE.Settings.WaitForCompleteTimeOut"/>
		/// </summary>
		public void WaitForComplete()
		{
			_domContainer.WaitForComplete();
		}

		internal static Element GetTypedElement(DomContainer domContainer, IBrowserElement ieBrowserElement)
		{
			if (ieBrowserElement == null) return null;

			if (_elementConstructors == null)
			{
				_elementConstructors = CreateElementConstructorHashTable();
			}

			ElementTag elementTag = new ElementTag(ieBrowserElement);
			Element returnElement = new ElementsContainer(domContainer, ieBrowserElement);

			if (_elementConstructors.Contains(elementTag))
			{
				ConstructorInfo constructorInfo = (ConstructorInfo) _elementConstructors[elementTag];
				if (constructorInfo != null)
				{
					return (Element) constructorInfo.Invoke(new object[] {returnElement});
				}
			}

			return returnElement;
		}

		internal static Hashtable CreateElementConstructorHashTable()
		{
			Hashtable elementConstructors = new Hashtable();
			Assembly assembly = Assembly.GetExecutingAssembly();

			foreach (Type type in assembly.GetTypes())
			{
				if (type.IsSubclassOf(typeof (Element)))
				{
					PropertyInfo property = type.GetProperty("ElementTags");
					if (property != null)
					{
						ConstructorInfo constructor = type.GetConstructor(new Type[] {typeof (Element)});
						if (constructor != null)
						{
							ArrayList elementTags = (ArrayList) property.GetValue(type, null);
							if (elementTags != null)
							{
								elementTags = CreateUniqueElementTagsForInputTypes(elementTags);
								foreach (ElementTag elementTag in elementTags)
								{
									// This is a terrible hack, but it will do for now.
									// Button and Image both support input/image. If
									// an element is input/image I prefer to return
									// an Image object.
									try
									{
										elementConstructors.Add(elementTag, constructor);
									}
									catch (ArgumentException)
									{
										if (type.Equals(typeof (Image)))
										{
											elementConstructors.Remove(elementTag);
											elementConstructors.Add(elementTag, constructor);
										}
									}
								}
							}
						}
					}
				}
			}

			return elementConstructors;
		}

		private static ArrayList CreateUniqueElementTagsForInputTypes(ArrayList elementTags)
		{
			ArrayList uniqueElementTags = new ArrayList();

			foreach (ElementTag elementTag in elementTags)
			{
				if (elementTag.IsInputElement)
				{
					string[] inputtypes = elementTag.InputTypes.Split(" ".ToCharArray());
					foreach (string inputtype in inputtypes)
					{
						ElementTag inputtypeElementTag = new ElementTag(elementTag.TagName, inputtype);
						uniqueElementTags.Add(inputtypeElementTag);
					}
				}
				else
				{
					uniqueElementTags.Add(elementTag);
				}
			}

			return uniqueElementTags;
		}

#if NET20
	/// <summary>
	/// Gets the closest ancestor of the specified type.
	/// </summary>
	/// <returns>An instance of the ancestorType. If no ancestor of ancestorType is found <code>null</code> is returned.</returns>
	///<example>
	/// The following example returns the Div a textfield is located in.
	/// <code>
	/// IE ie = new IE("http://www.example.com");
	/// Div mainDiv = ie.TextField("firstname").Ancestor&lt;Div&gt;;
	/// </code>
	/// </example>
   public T Ancestor<T>() where T : Element
    {
    	return (T)Ancestor(typeof(T));
    }

        /// <summary>
    /// Gets the closest ancestor of the specified Type and constraint.
    /// </summary>
    /// <param name="findBy">The constraint to match with.</param>
    /// <returns>
    /// An instance of the ancestorType. If no ancestor of ancestorType is found <code>null</code> is returned.
    /// </returns>
    /// <example>
    /// The following example returns the Div a textfield is located in.
    /// <code>
    /// IE ie = new IE("http://www.example.com");
    /// Div mainDiv = ie.TextField("firstname").Ancestor&lt;Div&gt;(Find.ByText("First name"));
    /// </code>
    /// </example>
    public T Ancestor<T>(BaseConstraint findBy) where T : Element
    {
    	return (T)Ancestor(typeof(T), findBy);
    }
#endif

		/// <summary>
		/// Gets the closest ancestor of the specified type.
		/// </summary>
		/// <param name="ancestorType">The ancestorType.</param>
		/// <returns>An instance of the ancestorType. If no ancestor of ancestorType is found <code>null</code> is returned.</returns>
		///<example>
		/// The following example returns the Div a textfield is located in.
		/// <code>
		/// IE ie = new IE("http://www.example.com");
		/// Div mainDiv = ie.TextField("firstname").Ancestor(typeof(Div));
		/// </code>
		/// </example>
		public Element Ancestor(Type ancestorType)
		{
			return Ancestor(ancestorType, new AlwaysTrueConstraint());
		}

		/// <summary>
		/// Gets the closest ancestor of the specified AttributConstraint.
		/// </summary>
		/// <param name="findBy">The AttributConstraint to match with.</param>
		/// <returns>An Element. If no ancestor of ancestorType is found <code>null</code> is returned.</returns>
		///<example>
		/// The following example returns the Div a textfield is located in.
		/// <code>
		/// IE ie = new IE("http://www.example.com");
		/// Div mainDiv = ie.TextField("firstname").Ancestor(Find.ByText("First name"));
		/// </code>
		/// </example>
		public Element Ancestor(BaseConstraint findBy)
		{
			Element parentElement = Parent;

			while (parentElement != null)
			{
				if (findBy.Compare(parentElement))
				{
					return parentElement;
				}
				parentElement = parentElement.Parent;
			}

			return null;
		}

		/// <summary>
		/// Gets the closest ancestor of the specified Type and BaseConstraint.
		/// </summary>
		/// <param name="ancestorType">Type of the ancestor.</param>
		/// <param name="findBy">The BaseConstraint to match with.</param>
		/// <returns>
		/// An instance of the ancestorType. If no ancestor of ancestorType is found <code>null</code> is returned.
		/// </returns>
		/// <example>
		/// The following example returns the Div a textfield is located in.
		/// <code>
		/// IE ie = new IE("http://www.example.com");
		/// Div mainDiv = ie.TextField("firstname").Ancestor(typeof(Div), Find.ByText("First name"));
		/// </code>
		/// </example>
		public Element Ancestor(Type ancestorType, BaseConstraint findBy)
		{
			if (!ancestorType.IsSubclassOf(typeof (Element)) && (ancestorType != typeof (Element)))
			{
				throw new ArgumentException("Type should inherit from Element", "ancestorType");
			}

			Element parentElement = Parent;

			if (parentElement == null)
			{
				return null;
			}
			else if (parentElement.GetType() == ancestorType && findBy.Compare(parentElement))
			{
				return parentElement;
			}
			else
			{
				return parentElement.Ancestor(ancestorType, findBy);
			}
		}

		/// <summary>
		/// Gets the closest ancestor of the specified Tag and AttributConstraint.
		/// </summary>
		/// <param name="tagName">The tag of the ancestor.</param>
		/// <param name="findBy">The AttributConstraint to match with.</param>
		/// <returns>
		/// <returns>An typed instance of the element matching the Tag and the AttributeConstriant.
		/// If no specific type is available, an element of type ElementContainer will be returned. 
		/// If there is no ancestor that matches Tag and BaseConstraint, <code>null</code> is returned.</returns>
		/// </returns>
		/// <example>
		/// The following example returns the Div a textfield is located in.
		/// <code>
		/// IE ie = new IE("http://www.example.com");
		/// Div mainDiv = ie.TextField("firstname").Ancestor("Div", Find.ByText("First name"));
		/// </code>
		/// </example>
		public Element Ancestor(string tagName, BaseConstraint findBy)
		{
			BaseConstraint findAncestor = Find.By("tagname", new StringEqualsAndCaseInsensitiveComparer(tagName))
			                                   && findBy;

			return Ancestor(findAncestor);
		}

		/// <summary>
		/// Gets the closest ancestor of the specified Tag.
		/// </summary>
		/// <param name="tagName">The tag of the ancestor.</param>
		/// <returns>An typed instance of the element matching the Tag. If no specific type is
		/// available, an element of type ElementContainer will be returned. 
		/// If there is no ancestor that matches Tag, <code>null</code> is returned.</returns>
		///<example>
		/// The following example returns the Div a textfield is located in.
		/// <code>
		/// IE ie = new IE("http://www.example.com");
		/// Div mainDiv = ie.TextField("firstname").Ancestor("Div");
		/// </code>
		/// </example>
		public Element Ancestor(string tagName)
		{
			return Ancestor(tagName, new AlwaysTrueConstraint());
		}

		public string GetValue(string attributename)
		{
			return BrowserElement.AttributeBag.GetValue(attributename);
		}

		public static Element New(DomContainer domContainer, IHTMLElement element)
		{
			return Element.GetTypedElement(domContainer, new IEElement(element, null));
		}
	}
}