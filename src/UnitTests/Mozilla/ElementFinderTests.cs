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
				
				Assert.That(element.Exists, Is.True);
				Assert.AreEqual("testElementAttributes", element.Id, "Id attribute incorrect");
				
				browser.GoTo(MainURI);
				Assert.That(element.Exists, Is.False);
				
			}
		}
		
		[Test]
		public void TestFindElementByTagName()
		{
			using (FireFox browser = new FireFox(MainURI))
			{
				WatiN.Core.Mozilla.ElementFinder elementFinder = new WatiN.Core.Mozilla.ElementFinder(null, "table", Find.ById("table2"), browser.ClientPort);
				string element = elementFinder.FindFirst();

				Assert.That(element, Is.Not.Null, "Shouldn't be null");
			}
		}

		[Test]
		public void ShouldFindInputByTagNameAndTypeAndId()
		{
			using (FireFox browser = new FireFox(MainURI))
			{
				WatiN.Core.Mozilla.ElementFinder elementFinder = new WatiN.Core.Mozilla.ElementFinder(null, "input", "TEXT", Find.ById("name"), browser.ClientPort);
				string element = elementFinder.FindFirst();

				Assert.That(element, Is.Not.Null, "Shouldn't be null");
			}
		}

		[Test]
		public void ShouldFindInputByTagNameInUpperCaseAndTypeAndId()
		{
			using (FireFox browser = new FireFox(MainURI))
			{
				WatiN.Core.Mozilla.ElementFinder elementFinder = new WatiN.Core.Mozilla.ElementFinder(null, "INPUT", "TEXT", Find.ByName("textinput1"), browser.ClientPort);
				string element = elementFinder.FindFirst();

				Assert.That(element, Is.Not.Null, "Shouldn't be null");
			}
		}

		[Test]
		public void ShouldNotFindInputByTagNameAndIncorrectTypeAndId()
		{
			using (FireFox browser = new FireFox(MainURI))
			{
				WatiN.Core.Mozilla.ElementFinder elementFinder = new WatiN.Core.Mozilla.ElementFinder(null, "input", "text", Find.ById("Checkbox3"), browser.ClientPort);
				string element = elementFinder.FindFirst();

				Assert.That(element, Is.Null, "Should be null");
			}
		}

		[Test]
		public void TestFindElement()
		{
			using (FireFox browser = new FireFox(MainURI))
			{
				WatiN.Core.Mozilla.ElementFinder elementFinder = new WatiN.Core.Mozilla.ElementFinder(null, null, Find.ById("table2"), browser.ClientPort);
				string element = elementFinder.FindFirst();

				Assert.That(element, Is.Not.Null, "Shouldn't be null");
			}
		}
	}
}
