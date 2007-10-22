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

using System.Collections;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace WatiN.Core.UnitTests
{
	[TestFixture]
	public class DivTests : BaseElementsTests
	{
		[Test]
		public void DivElementTags()
		{
			Assert.AreEqual(1, Div.ElementTags.Count, "1 elementtags expected");
			Assert.AreEqual("div", ((ElementTag) Div.ElementTags[0]).TagName);
		}

		[Test]
		public void CreateDivFromElement()
		{
			Element element = ie.Element("divid");
			Div div = new Div(element);
			Assert.AreEqual("divid", div.Id);
		}

		[Test]
		public void DivExists()
		{
			Assert.IsTrue(ie.Div("divid").Exists);
			Assert.IsTrue(ie.Div(new Regex("divid")).Exists);
			Assert.IsFalse(ie.Div("noneexistingdivid").Exists);
		}

		[Test]
		public void DivTest()
		{
			Assert.AreEqual("divid", ie.Div(Find.ById("divid")).Id, "Find Div by Find.ById");
			Assert.AreEqual("divid", ie.Div("divid").Id, "Find Div by ie.Div()");
		}

		[Test]
		public void Divs()
		{
			Assert.AreEqual(1, ie.Divs.Length, "Unexpected number of Divs");

			DivCollection divs = ie.Divs;

			// Collection items by index
			Assert.AreEqual("divid", divs[0].Id);

			// Collection iteration and comparing the result with Enumerator
			IEnumerable divEnumerable = divs;
			IEnumerator divEnumerator = divEnumerable.GetEnumerator();

			int count = 0;
			foreach (Div div in divs)
			{
				divEnumerator.MoveNext();
				object enumDiv = divEnumerator.Current;

				Assert.IsInstanceOfType(div.GetType(), enumDiv, "Types are not the same");
				Assert.AreEqual(div.OuterHtml, ((Div) enumDiv).OuterHtml, "foreach and IEnumator don't act the same.");
				++count;
			}

			Assert.IsFalse(divEnumerator.MoveNext(), "Expected last item");
			Assert.AreEqual(1, count);
		}
	}
}