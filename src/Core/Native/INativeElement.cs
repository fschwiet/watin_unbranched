#region WatiN Copyright (C) 2006-2009 Jeroen van Menen

//Copyright 2006-2009 Jeroen van Menen
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
using System.Collections.Specialized;

namespace WatiN.Core.Native
{
    public interface INativeElement
    {
        /// <summary>
        /// Gets a collection consisting of the immediate children of this element.
        /// </summary>
        INativeElementCollection Children { get; }

        /// <summary>
        /// Gets a collection consisting of all descendants of this element.
        /// </summary>
        INativeElementCollection AllDescendants { get; }

        /// <summary>
        /// Gets a collection consisting of the table rows within a TABLE or TBODY element.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if applied to an element of the wrong type</exception>
        INativeElementCollection TableRows { get; }

        /// <summary>
        /// Gets a collection consisting of the table bodies within a TABLE element.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if applied to an element of the wrong type</exception>
        INativeElementCollection TableBodies { get; }

        /// <summary>
        /// Gets a collection consisting of the table cells within a TR element.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if applied to an element of the wrong type</exception>
        INativeElementCollection TableCells { get; }

        /// <summary>
        /// Gets a collection consisting of the options within a SELECT element.
        /// </summary>
        INativeElementCollection Options { get; }

        /// <summary>
        /// Returns the text displayed after this element when it's wrapped
        /// in a Label element; otherwise it returns <c>null</c>.
        /// </summary>
        string TextAfter { get; }

        /// <summary>
        /// Returns the text displayed before this element when it's wrapped
        /// in a Label element; otherwise it returns <c>null</c>.
        /// </summary>
        string TextBefore { get; }

        /// <summary>
        /// Gets the next sibling of this element in the Dom of the HTML page.
        /// </summary>
        /// <value>The next sibling.</value>
        INativeElement NextSibling { get; }

        /// <summary>
        /// Gets the previous sibling of this element in the Dom of the HTML page.
        /// </summary>
        /// <value>The previous sibling.</value>
        INativeElement PreviousSibling { get; }

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
        INativeElement Parent { get; }

        /// <summary>
        /// This methode can be used if the attribute isn't available as a property of
        /// Element or a subclass of Element.
        /// </summary>
        /// <param name="attributeName">The attribute name. This could be different then named in
        /// the HTML. It should be the name of the property exposed by IE on it's element object.</param>
        /// <returns>The value of the attribute if available; otherwise <c>null</c> is returned.</returns>
        string GetAttributeValue(string attributeName);

        void SetAttributeValue(string attributeName, string value);

        string GetStyleAttributeValue(string attributeName);
        void SetStyleAttributeValue(string attributeName, string value);
        
        void ClickOnElement();
        void SetFocus();
        void FireEvent(string eventName, NameValueCollection eventProperties);
        bool IsElementReferenceStillValid();
        string TagName { get; }
        void FireEventNoWait(string eventName, NameValueCollection eventProperties);
        void Select();
        void SubmitForm();
        void SetFileUploadFile(Element element, string fileName);

        void WaitUntilReady();
    }
}