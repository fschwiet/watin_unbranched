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

namespace WatiN.Core
{
  using System;
  using System.Text.RegularExpressions;
  using WatiN.Core.Comparers;
  using WatiN.Core.Interfaces;

  /// <summary>
  /// This class provides factory methods for de most commonly used attributes
  /// to find an element on a web page.
  /// </summary>
  public class Find
  {
		internal const string altAttribute = "alt";
    internal const string idAttribute = "id";
    internal const string forAttribute = "htmlfor";
    internal const string nameAttribute = "name";
    internal const string srcAttribute = "src";
    internal const string styleBaseAttribute = "style.";
    internal const string textAttribute = "innertext";    
    internal const string titleAttribute = "title";    
    internal const string valueAttribute = "value";
    internal const string hrefAttribute = "href";

		/// <summary>
		/// Finds an element by its alt text.
		/// </summary>
		/// <param name="alt">The alt text to find.</param>
		/// <returns>The AttributeConstraint</returns>
		/// <example>
		/// <code>ie.Image(Find.ByAlt("alt text")).Name</code>
		/// </example>
		public static AttributeConstraint ByAlt(string alt)
		{
		  return new AttributeConstraint(altAttribute, alt);
		}

		/// <summary>
		/// Finds an element by its alt text.
		/// </summary>
		/// <param name="regex">The regular expression for the alt text to find.</param>
		/// <returns>The AttributeConstraint</returns>
		/// <example>
		/// <code>ie.Image(Find.ByAlt(new Regex("pattern goes here")))).Name</code>
		/// </example>
		public static AttributeConstraint ByAlt(Regex regex)
		{
			return new AttributeConstraint(altAttribute, regex);
		}

		/// <summary>
		/// Finds an element by its alt text.
		/// </summary>
		/// <param name="compare">The compare.</param>
		/// <returns>The AttributeConstraint</returns>
		/// <example>
		/// 	<code>Image img = ie.Image(Find.ByAlt(new StringContainsAndCaseInsensitiveComparer("alt text")));</code>
		/// </example>
		public static AttributeConstraint ByAlt(ICompare compare)
		{
			return new AttributeConstraint(altAttribute, compare);
		}

		#if NET20
		/// <summary>
		/// Finds an element by its alt text.
		/// </summary>
		/// <param name="predicate">The predicate method to call to make the comparison.</param>
		/// <returns>The AttributeConstraint</returns>
		/// <example>
		/// 	<code>Image img = ie.Image(Find.ByAlt(MyOwnCompareMethod));</code>
		/// </example>
		public static AttributeConstraint ByAlt(Predicate<string> predicate)
		{
			return new AttributeConstraint(altAttribute, new PredicateComparer(predicate));
		}
		#endif
		
    /// <summary>
    /// Find a Label element by the id of the element it's linked with.
    /// </summary>
    /// <param name="forId">Id of the element the label is linked with.</param>
    /// <returns><see cref="AttributeConstraint" /></returns>
    /// <example>
    /// <code>ie.Label(Find.ByFor("optionbuttonid")).Text</code>
    /// </example>
    public static AttributeConstraint ByFor(string forId)
    {
      return new AttributeConstraint(forAttribute, forId);
    }

    /// <param name="regex">Regular expression to find the matching Id of the element
    ///  the label is linked with.</param>
    /// <returns><see cref="AttributeConstraint" /></returns>
    /// <example>
    /// <code>ie.Label(Find.ByFor(new Regex("pattern goes here")).Text</code>
    /// </example>
    public static AttributeConstraint ByFor(Regex regex)
    {
      return new AttributeConstraint(forAttribute, regex);
    }

    /// <param name="element">The element to which the Label element is attached. This element must an Id value.</param>
    /// <returns><see cref="AttributeConstraint" /></returns>
    /// <example>
    /// <code>
    /// CheckBox checkbox = ie.CheckBox("checkboxid");
    /// ie.Label(Find.ByFor(checkbox).Text</code>
    /// </example>
    public static AttributeConstraint ByFor(Element element)
    {
      return new AttributeConstraint(forAttribute, element.Id);
    }

    /// <param name="comparer">The comparer.</param>
    /// <returns><see cref="AttributeConstraint"/></returns>
    /// <example>
    /// 	<code>
    /// Label label = ie.Label(Find.ByFor(new StringContainsAndCaseInsensitiveComparer("optionbuttonid")));</code>
    /// </example>
    public static AttributeConstraint ByFor(ICompare comparer)
    {
      return new AttributeConstraint(forAttribute, comparer);
    }

    #if NET20
		/// <param name="predicate">The predicate method to call to make the comparison.</param>
		/// <returns>The AttributeConstraint</returns>
		/// <example>
		/// <code>
		/// Label label = ie.Label(Find.ByFor(MyOwnCompareMethod));
		/// </code>
		/// </example>
		public static AttributeConstraint ByFor(Predicate<string> predicate)
		{
			return new AttributeConstraint(forAttribute, new PredicateComparer(predicate));
		}
		#endif

    /// <summary>
    /// Find an element by its id.
    /// </summary>
    /// <param name="id">Element id to find.</param>
    /// <returns><see cref="AttributeConstraint" /></returns>
    /// <example>
    /// <code>ie.Link(Find.ById("testlinkid")).Url</code>
    /// </example>
    public static AttributeConstraint ById(string id)
    {
      return new AttributeConstraint(idAttribute, id);
    }

    /// <param name="regex">Regular expression to find a matching Id.</param>
    /// <returns><see cref="AttributeConstraint" /></returns>
    /// <example>
    /// <code>ie.Link(Find.ById(new Regex("pattern goes here"))).Url</code>
    /// </example>
    public static AttributeConstraint ById(Regex regex)
    {
      return new AttributeConstraint(idAttribute, regex);
    }

    /// <param name="compare">The compare.</param>
    /// <returns><see cref="AttributeConstraint"/></returns>
    /// <example>
    /// 	<code>Link link = ie.Link(Find.ById(new StringContainsAndCaseInsensitiveComparer("linkId1")));</code>
    /// </example>
    public static AttributeConstraint ById(ICompare compare)
    {
      return new AttributeConstraint(idAttribute, compare);
    }

    #if NET20
		/// <param name="predicate">The predicate method to call to make the comparison.</param>
		/// <returns>The AttributeConstraint</returns>
		/// <example>
		/// <code>
		/// Link link = ie.Link(Find.ById(MyOwnCompareMethod));
		/// </code>
		/// </example>
		public static AttributeConstraint ById(Predicate<string> predicate)
		{
			return new AttributeConstraint(idAttribute, new PredicateComparer(predicate));
		}
		#endif

    /// <summary>
    /// Find an element by its index.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns></returns>
    public static IndexAttributeConstraint ByIndex(int index)
    {
      return new IndexAttributeConstraint(index);
    }

    /// <summary>
    /// Find an element by its name.
    /// </summary>
    /// <param name="name">Name to find.</param>
    /// <returns><see cref="AttributeConstraint" /></returns>
    /// <example>
    /// <code>ie.Link(Find.ByName("testlinkname")).Url</code>
    /// </example>
    public static AttributeConstraint ByName(string name)
    {
      return new AttributeConstraint(nameAttribute, name);
    }

    /// <param regex="regex">Regular expression to find a matching Name.</param>
    /// <returns><see cref="AttributeConstraint" /></returns>
    /// <example>
    /// <code>ie.Link(Find.ByName(new Regex("pattern goes here")))).Url</code>
    /// </example>
    public static AttributeConstraint ByName(Regex regex)
    {
      return new AttributeConstraint(nameAttribute, regex);
    }

    /// <summary>
    /// Bies the name.
    /// </summary>
    /// <param name="comparer">The comparer.</param>
    /// <returns><see cref="AttributeConstraint"/></returns>
    /// <example>
    /// 	<code>ie.Link(Find.ByName(new StringContainsAndCaseInsensitiveComparer("linkname")))).Url</code>
    /// </example>
    public static AttributeConstraint ByName(ICompare comparer)
    {
      return new AttributeConstraint(nameAttribute, comparer);
    }

    #if NET20
		/// <param name="predicate">The predicate method to call to make the comparison.</param>
		/// <returns>The AttributeConstraint</returns>
		/// <example>
		/// <code>
		/// Link link = ie.Link(Find.ByName(MyOwnCompareMethod));
		/// </code>
		/// </example>
		public static AttributeConstraint ByName(Predicate<string> predicate)
		{
			return new AttributeConstraint(nameAttribute, new PredicateComparer(predicate));
		}
		#endif

    /// <summary>
    /// Find an element by its (inner) text
    /// </summary>
    /// <param name="text">Element text</param>
    /// <returns><see cref="AttributeConstraint" /></returns>
    /// <example>
    /// <code>ie.Link(Find.ByText("my link")).Url</code>
    /// </example>
    public static AttributeConstraint ByText(string text)
    {
      return new AttributeConstraint(textAttribute, new StringContainsAndCaseInsensitiveComparer(text));
    }

    /// <param name="regex">Regular expression to find a matching Text.</param>
    /// <returns><see cref="AttributeConstraint" /></returns>
    /// <example>
    /// <code>ie.Link(Find.ByText(new Regex("pattern goes here"))).Url</code>
    /// </example>
    public static AttributeConstraint ByText(Regex regex)
    {
      return new AttributeConstraint(textAttribute, regex);
    }

    /// <param name="comparer">The comparer.</param>
    /// <returns><see cref="AttributeConstraint"/></returns>
    /// <example>
    /// 	<code>Link link = ie.Link(Find.ByText(new StringContainsAndCaseInsensitiveComparer("my li"))).Url</code>
    /// </example>
    public static AttributeConstraint ByText(ICompare comparer)
    {
      return new AttributeConstraint(textAttribute, comparer);
    }

    #if NET20
		/// <param name="predicate">The predicate method to call to make the comparison.</param>
		/// <returns>The AttributeConstraint</returns>
		/// <example>
		/// <code>
		/// Link link = ie.Link(Find.ByText(MyOwnCompareMethod));
		/// </code>
		/// </example>
		public static AttributeConstraint ByText(Predicate<string> predicate)
		{
			return new AttributeConstraint(textAttribute, new PredicateComparer(predicate));
		}
		#endif

		/// <summary>
    /// Find an element, frame, IE instance or HTMLDialog by its Url.
    /// </summary>
    /// <param name="url">The well-formed url to find.</param>
    /// <returns><see cref="AttributeConstraint" /></returns>
    /// <example>
    /// <code>ie.Link(Find.ByUrl("http://watin.sourceforge.net")).Url</code>
    /// </example>
    public static AttributeConstraint ByUrl(string url)
    {
      return ByUrl(new Uri(url));
    }

    /// <param name="url">The well-formed url to find.</param>
    /// <param name="ignoreQuery">Set to true to ignore querystring when matching.</param>
    /// <returns><see cref="AttributeConstraint" /></returns>
    /// <example>
    /// <code>ie.Link(Find.ByUrl("http://watin.sourceforge.net", true)).Url</code>
    /// </example>
    public static AttributeConstraint ByUrl(string url, bool ignoreQuery)
    {
      return ByUrl(new Uri(url), ignoreQuery);
    }

    /// <param name="uri">The uri to find.</param>
    /// <returns><see cref="AttributeConstraint" /></returns>
    /// <example>
    /// <code>ie.Link(Find.ByUrl(new Uri("watin.sourceforge.net"))).Url</code>
    /// </example>
    public static AttributeConstraint ByUrl(Uri uri)
    {
      return ByUrl(uri, false);
    }

    /// <param name="uri">The uri to find.</param>
    /// <param name="ignoreQuery">Set to true to ignore querystring when matching.</param>
    /// <returns><see cref="AttributeConstraint" /></returns>
    /// <example>
    /// <code>ie.Link(Find.ByUrl(new Uri("watin.sourceforge.net", true))).Url</code>
    /// </example>
    public static AttributeConstraint ByUrl(Uri uri, bool ignoreQuery)
    {
      return new AttributeConstraint(hrefAttribute, new UriComparer(uri, ignoreQuery));
    }

    /// <param name="regex">Regular expression to find a matching Url.</param>
    /// <returns><see cref="AttributeConstraint" /></returns>
    /// <example>
    /// <code>ie.Link(Find.ByUrl(new Regex("pattern goes here"))).Url</code>
    /// </example>
    public static AttributeConstraint ByUrl(Regex regex)
    {
      return new AttributeConstraint(hrefAttribute, regex);
    }

    /// <param name="comparer">The comparer.</param>
    /// <returns><see cref="AttributeConstraint"/></returns>
    /// <example>
    /// 	<code>ie.Link(Find.ByUrl(new UriComparer(uri, ignoreQuery))).Url</code>
    /// </example>
    public static AttributeConstraint ByUrl(ICompare comparer)
    {
      return new AttributeConstraint(hrefAttribute, comparer);
    }

    #if NET20
		/// <param name="predicate">The predicate method to call to make the comparison.</param>
		/// <returns>The AttributeConstraint</returns>
		/// <example>
		/// <code>
		/// Link link = ie.Link(Find.ByUrl(MyOwnCompareMethod));
		/// </code>
		/// </example>
		public static AttributeConstraint ByUrl(Predicate<string> predicate)
		{
			return new AttributeConstraint(hrefAttribute, new PredicateComparer(predicate));
		}
		#endif

		/// <summary>
    /// Find an element, frame, IE instance or HTMLDialog by its Title.
    /// </summary>
    /// <param name="title">The title to match partially.</param>
    /// <returns><see cref="AttributeConstraint"/></returns>
    /// <example>
    /// <code>IE ie = IE.AttachToIE(Find.ByTitle("WatiN Home Page"))</code>
    /// </example>
    public static AttributeConstraint ByTitle(string title)
    {
      return new AttributeConstraint(titleAttribute, new StringContainsAndCaseInsensitiveComparer(title));
    }

    /// <param name="regex">Regular expression to find a matching Title.</param>
    /// <returns><see cref="AttributeConstraint"/></returns>
    /// <example>
    /// <code>IE ie = IE.AttachToIE(Find.ByTitle(new Regex("pattern goes here")))</code>
    /// </example>
    public static AttributeConstraint ByTitle(Regex regex)
    {
      return new AttributeConstraint(titleAttribute, regex);
    }

    /// <param name="comparer">The comparer.</param>
    /// <returns><see cref="AttributeConstraint"/></returns>
    /// <example>
    /// 	<code>IE ie = IE.AttachToIE(Find.ByTitle(new StringContainsAndCaseInsensitiveComparer("part of the title")));</code>
    /// </example>
    public static AttributeConstraint ByTitle(ICompare comparer)
    {
      return new AttributeConstraint(titleAttribute, comparer);
    }

    #if NET20
		/// <param name="predicate">The predicate method to call to make the comparison.</param>
		/// <returns>The AttributeConstraint</returns>
		/// <example>
		/// <code>
		/// IE ie = IE.AttachToIE(Find.ByTitle(MyOwnCompareMethod));
		/// </code>
		/// </example>
		public static AttributeConstraint ByTitle(Predicate<string> predicate)
		{
			return new AttributeConstraint(titleAttribute, new PredicateComparer(predicate));
		}
		#endif

    /// <summary>
    /// Find an element by its value attribute.
    /// </summary>
    /// <param name="value">The value to find.</param>
    /// <returns><see cref="AttributeConstraint"/></returns>
    /// <example>
    /// <code>
    /// Button button = ie.Button(Find.ByValue("My Button"))
    /// </code>
    /// </example>
    public static AttributeConstraint ByValue(string value)
    {
      return new AttributeConstraint(valueAttribute, value);
    }

    /// <param name="regex">Regular expression to find a matching Value.</param>
    /// <returns><see cref="AttributeConstraint"/></returns>
    /// <example>
    /// <code>
    /// Button button = ie.Button(Find.ByValue(new Regex("pattern goes here")))
    /// </code>
    /// </example>
    public static AttributeConstraint ByValue(Regex regex)
    {
      return new AttributeConstraint(valueAttribute, regex);
    }

    /// <param name="comparer">The comparer.</param>
    /// <returns><see cref="AttributeConstraint"/></returns>
    /// <example>
    /// <code>
    /// Button button = ie.Button(Find.ByValue(new StringContainsAndCaseInsensitiveComparer("pattern goes here")));
    /// </code>
    /// </example>
    public static AttributeConstraint ByValue(ICompare comparer)
    {
      return new AttributeConstraint(valueAttribute, comparer);
    }

    #if NET20
		/// <param name="predicate">The predicate method to call to make the comparison.</param>
		/// <returns>The AttributeConstraint</returns>
		/// <example>
		/// <code>
		/// Button button = ie.Button(Find.ByValue(MyOwnCompareMethod));
		/// </code>
		/// </example>
		public static AttributeConstraint ByValue(Predicate<string> predicate)
		{
			return new AttributeConstraint(valueAttribute, new PredicateComparer(predicate));
		}
		#endif

    /// <summary>
    /// Find an <see cref="Image"/> by its source (src) attribute.
    /// </summary>
    /// <param name="src">Src to find.</param>
    /// <returns><see cref="AttributeConstraint" /></returns>
    /// <example>
    /// <code>ie.Image(Find.BySrc("image.gif"))</code>
    /// </example>
    public static AttributeConstraint BySrc(string src)
    {
      return new AttributeConstraint(srcAttribute, src);
    }

    /// <param regex="regex">Regular expression to find a matching Src.</param>
    /// <returns><see cref="AttributeConstraint" /></returns>
    /// <example>
    /// <code>ie.Image(Find.BySrc(new Regex("pattern goes here"))))</code>
    /// </example>
    public static AttributeConstraint BySrc(Regex regex)
    {
      return new AttributeConstraint(srcAttribute, regex);
    }

    /// <param name="comparer">The comparer.</param>
    /// <returns><see cref="AttributeConstraint"/></returns>
    /// <example>
    /// 	<code>Image image = ie.Image(Find.BySrc(new StringContainsAndCaseInsensitiveComparer("watin/sourceforge")));</code>
    /// </example>
    public static AttributeConstraint BySrc(ICompare comparer)
    {
      return new AttributeConstraint(srcAttribute, comparer);
    }

    #if NET20
		/// <param name="predicate">The predicate method to call to make the comparison.</param>
		/// <returns>The AttributeConstraint</returns>
		/// <example>
		/// <code>
		/// Image image = ie.Image(Find.BySrc(MyOwnCompareMethod));
		/// </code>
		/// </example>
		public static AttributeConstraint BySrc(Predicate<string> predicate)
		{
			return new AttributeConstraint(srcAttribute, new PredicateComparer(predicate));
		}
		#endif

		/// <summary>
    /// Find an element by an attribute.
    /// </summary>
    /// <param name="attributeName">The attribute name to compare the value with.</param>
    /// <param name="value">The exact matching value of the attribute.</param>
    /// <returns><see cref="AttributeConstraint" /></returns>
    /// <example>
    /// <code>ie.Link(Find.By("id", "testlinkid")).Url</code>
    /// </example>
    public static AttributeConstraint By(string attributeName, string value)
    {
      return new AttributeConstraint(attributeName, value);
    }

    /// <param name="attributeName">The attribute name to compare the value with.</param>
    /// <param name="regex">Regular expression to find a matching value of the given attribute.</param>
    /// <returns><see cref="AttributeConstraint" /></returns>
    /// <example>
    /// <code>ie.Link(Find.By("id", new Regex("pattern goes here"))).Url</code>
    /// </example>
    public static AttributeConstraint By(string attributeName, Regex regex)
    {
      return new AttributeConstraint(attributeName, regex);
    }

    /// <param name="attributeName">The attribute to compare the value with.</param>
    /// <param name="comparer">The comparer to be used.</param>
    /// <returns><see cref="AttributeConstraint"/></returns>
    /// <example>
    /// 	<code>Link link = ie.Link(Find.By("innertext", new StringContainsAndCaseInsensitiveComparer("pattern goes here")));</code>
    /// </example>
    public static AttributeConstraint By(string attributeName, ICompare comparer)
    {
      return new AttributeConstraint(attributeName, comparer);
    }

    #if NET20
    /// <param name="attributeName">The attribute to compare the value with.</param>
    /// <param name="predicate">The predicate method to call to make the comparison.</param>
	/// <returns>The AttributeConstraint</returns>
	/// <example>
	/// <code>
	/// Link link = ie.Link(Find.By("innertext", MyOwnCompareMethod));
	/// </code>
	/// </example>
	public static AttributeConstraint By(string attributeName, Predicate<string> predicate)
	{
		return new AttributeConstraint(attributeName, new PredicateComparer(predicate));
	}
	#endif

    /// <summary>
    /// Find an element by a style attribute.
    /// </summary>
    /// <param name="styleAttributeName">Name of the style attribute.</param>
    /// <param name="value">The exact matching value of the attribute.</param>
    /// <returns><see cref="AttributeConstraint"/></returns>
    /// <example>
    /// 	<code>ie.Span(Find.ByStyle("background-color", "red"))</code>
    /// </example>
    public static AttributeConstraint ByStyle(string styleAttributeName, string value)
    {
      return new AttributeConstraint(styleBaseAttribute + styleAttributeName, value);
    }

    /// <param name="styleAttributeName">Name of the style attribute.</param>
    /// <param name="value">Regular expression to find a matching value of the given style attribute.</param>
    /// <returns><see cref="AttributeConstraint"/></returns>
    /// <example>
    /// 	<code>ie.Link(Find.ByStyle("font-family", new Regex("pattern goes here")))</code>
    /// </example>
    public static AttributeConstraint ByStyle(string styleAttributeName, Regex value)
    {
      return new AttributeConstraint(styleBaseAttribute + styleAttributeName, value);
    }

    /// <param name="styleAttributeName">Name of the style attribute.</param>
    /// <param name="comparer">The comparer.</param>
    /// <returns><see cref="AttributeConstraint"/></returns>
    /// <example>
    /// 	<code>Link link = ie.Link(Find.ByStyle("font-family", new StringContainsAndCaseInsensitiveComparer("aria")));</code>
    /// </example>
    public static AttributeConstraint ByStyle(string styleAttributeName, ICompare comparer)
    {
      return new AttributeConstraint(styleBaseAttribute + styleAttributeName, comparer);
    }
    
    #if NET20
    /// <param name="styleAttributeName">Name of the style attribute.</param>
    /// <param name="predicate">The predicate method to call to make the comparison.</param>
	/// <returns>The AttributeConstraint</returns>
	/// <example>
	/// <code>
	/// Link link = ie.Link(Find.ByStyle("font-family", MyOwnCompareMethod));
	/// </code>
	/// </example>
	public static AttributeConstraint ByStyle(string styleAttributeName, Predicate<string> predicate)
	{
		return new AttributeConstraint(styleBaseAttribute + styleAttributeName, new PredicateComparer(predicate));
	}
	#endif
  }
}
