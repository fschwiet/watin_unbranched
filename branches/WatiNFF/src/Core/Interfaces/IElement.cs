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

namespace WatiN.Core.Interfaces
{
    public interface IElement
    {
        #region Properties

        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <value>The id.</value>
        string Id { get; }

        /// <summary>
        /// Gets the name of the element's css class.
        /// </summary>
        /// <value>The name of the element's class.</value>
        string ClassName { get; }
        
        /// <summary>
        /// Gets the value of the attribute
        /// </summary>
        /// <param name="attributeName">The attribute name</param>
        /// <returns></returns>
        string GetAttributeValue(string attributeName);
        /// <summary>
        /// Gets the innertext of this element (and the innertext of all the elements contained
        /// in this element).
        /// </summary>
        /// <value>The inner text of this element</value>
        string Text { get; }

        /// <summary>
        /// Gets the title of the element.
        /// </summary>
        /// <value>The title of this element.</value>
        string Title { get; }

        /// <summary>
        /// Gets the inner HTML of the element.
        /// </summary>
        /// <value>The inner HTML of the element.</value>
        string InnerHtml { get; }

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
        IElement Parent { get; }

        /// <summary>
        /// Gets the next sibling of this element in the Dom of the HTML page.
        /// </summary>
        /// <value>The next sibling.</value>		
        IElement NextSibling { get; }

        /// <summary>
        /// Gets the previous sibling of this element in the Dom of the HTML page.
        /// </summary>
        /// <value>The previous sibling.</value>		
        IElement PreviousSibling { get; }

        /// <summary>
        /// Gets the tag name of this element.
        /// </summary>
        /// <value>The name of the tag.</value>
        string TagName { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Clicks this element and waits till the event is completely finished (page is loaded 
        /// and ready) .
        /// </summary>
        void Click();

        #endregion


    }
}
