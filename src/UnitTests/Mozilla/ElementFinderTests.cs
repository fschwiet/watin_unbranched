/*
 * Created by SharpDevelop.
 * User: J.vanMenen
 * Date: 13-11-2007
 * Time: 23:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using System;
using WatiN.Core.Mozilla;

namespace WatiN.Core.UnitTests.Mozilla
{
	[TestFixture]
	public class ElementFinderTests : BaseElementsTests
	{
		[Test]
		public void TestMethod()
		{
			using (FireFox browser = new FireFox(MainURI))
			{
				WatiN.Core.Mozilla.Element element = (WatiN.Core.Mozilla.Element)browser.Element("testElementAttributes");
				
				Assert.That(element.StoredElementReferenceExists(), Is.True);
				Assert.AreEqual("testElementAttributes", element.Id, "Id attribute incorrect");
				
				browser.GoTo(MainURI);
				Assert.That(element.StoredElementReferenceExists(), Is.False);
				
			}
		}
		
		[Test]
		public void TestFindElementByTagName()
		{
			using (FireFox browser = new FireFox(MainURI))
			{
				WatiN.Core.Mozilla.ElementFinder elementFinder = new WatiN.Core.Mozilla.ElementFinder("table", Find.ById("table2"), browser.ClientPort);
				string element = elementFinder.FindFirst();

				Assert.That(element, Is.Not.Null, "Shouldn't be null");
			}
		}

		[Test]
		public void TestFindElement()
		{
			using (FireFox browser = new FireFox(MainURI))
			{
				WatiN.Core.Mozilla.ElementFinder elementFinder = new WatiN.Core.Mozilla.ElementFinder("", Find.ById("table2"), browser.ClientPort);
				string element = elementFinder.FindFirst();

				Assert.That(element, Is.Not.Null, "Shouldn't be null");
			}
		}
	}
}
