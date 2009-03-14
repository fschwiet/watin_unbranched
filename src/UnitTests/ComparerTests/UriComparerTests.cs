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
using System.Globalization;
using System.Threading;
using System.Web;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using WatiN.Core.Comparers;
using WatiN.Core.Interfaces;

namespace WatiN.Core.UnitTests
{
	[TestFixture]
	public class UriComparerTests
	{
		[Test]
		public void ConstructorWithValueAndStringCompare()
		{
			ICompare comparer = new UriComparer(new Uri("http://watin.sourceforge.net"));

			// String Compare
			Assert.IsTrue(comparer.Compare("http://watin.sourceforge.net"), "Exact match should pass.");
			Assert.IsTrue(comparer.Compare("HTTP://watin.Sourceforge.net"), "Match should not be case sensitive");

			Assert.IsFalse(comparer.Compare("http://watin.sourceforge.net/index.html"),
			               "Exact match plus more should not pass.");
			Assert.IsFalse(comparer.Compare("http://watin"), "Partial match should not match");
			Assert.IsFalse(comparer.Compare("file://html/main.html"), "Something completely different should not match");
			Assert.IsFalse(comparer.Compare(String.Empty), "String.Empty should not match");
			Assert.IsFalse(comparer.Compare(null), "String: null should not match");
		}

		[Test]
		public void ConstructorWithValueAndUriCompare()
		{
			UriComparer comparer = new UriComparer(new Uri("http://watin.sourceforge.net"));

			// Uri Compare
			Assert.IsTrue(comparer.Compare(new Uri("http://watin.sourceforge.net")), "Uri: Exact match should pass.");
			Assert.IsTrue(comparer.Compare(new Uri("HTTP://watin.Sourceforge.net")), "Uri: Match should not be case sensitive");

			Assert.IsFalse(comparer.Compare(new Uri("http://watin.sourceforge.net/index.html")),
			               "Uri: Exact match plus more should not pass.");
			Assert.IsFalse(comparer.Compare(new Uri("http://watin")), "Uri: Partial match should not match");
			Assert.IsFalse(comparer.Compare(new Uri("file://html/main.html")),
			               "Uri: Something completely different should not match");
			Assert.IsFalse(comparer.Compare(null), "Uri: null should not match");
		}

		[Test]
		public void IgnoreQueryStringCompareWithQueryStringInValueToBeFound()
		{
			UriComparer comparer = new UriComparer(new Uri("http://watin.sourceforge.net/here.aspx?query"), true);

			Assert.IsTrue(comparer.Compare("http://watin.sourceforge.net/here.aspx"), "Uri: Match ignoring querystring.");
			Assert.IsTrue(comparer.Compare("http://watin.sourceforge.net/here.aspx?query"),
			              "Uri: Match ignoring querystring (include querystring in compare).");
			Assert.IsTrue(comparer.Compare("http://watin.sourceforge.net/here.aspx?badquery"),
			              "Uri: Match ignoring querystring (include non-matching querystring).");
			Assert.IsFalse(comparer.Compare("http://watin.sourceforge.net"),
			               "Uri: Match incorrectly when ignoring querystring.");
			Assert.IsFalse(comparer.Compare("http://www.something.completely.different.net"),
			               "Uri: Match incorrectly when ignoring querystring.");
		}

		[Test]
		public void IgnoreQueryStringCompareWithNoQueryStringInValueToBeFound()
		{
			UriComparer comparer = new UriComparer(new Uri("http://watin.sourceforge.net"), true);

			Assert.IsTrue(comparer.Compare("http://watin.sourceforge.net/"), "Same site should match");

			Assert.IsFalse(comparer.Compare("http://watin.sourceforge.net/here.aspx?query"), "Should ignore query string");
			Assert.IsFalse(comparer.Compare("http://www.microsoft.com/"), "Should ignore completely different site");
		}

		[Test, ExpectedException(typeof (ArgumentNullException))]
		public void ConstructorWithNullShouldThrowArgumentNullException()
		{
			new UriComparer(null);
		}

		[Test]
		public void ToStringTest()
		{
			UriComparer comparer = new UriComparer(new Uri("http://watin.sourceforge.net"));

			Assert.AreEqual("http://watin.sourceforge.net/", comparer.ToString());
		}

		[Test]
		public void ToStringTestWithEncodedQueryString()
		{
            string url = string.Format("http://www.google.com/search?q={0}", HttpUtility.UrlEncode("a+b"));

			UriComparer comparer = new UriComparer(new Uri(url));

			Assert.That(comparer.ToString(), Is.EqualTo(url));
		}

		[Test]
		public void CompareShouldBeCultureInvariant()
		{
			// Get the tr-TR (Turkish-Turkey) culture.
			CultureInfo turkish = new CultureInfo("tr-TR");

			// Get the culture that is associated with the current thread.
			CultureInfo thisCulture = Thread.CurrentThread.CurrentCulture;

			try
			{
				// Set the culture to Turkish
				Thread.CurrentThread.CurrentCulture = turkish;

				UriComparer comparer = new UriComparer(new Uri("http://watin.sourceforge.net"), true);

				Assert.IsTrue(comparer.Compare("http://WATIN.sourceforge.net/"), "Same site should match");
			}
			finally
			{
				// Set the culture back to the original
				Thread.CurrentThread.CurrentCulture = thisCulture;
			}
		}

        [Test]
        public void ShoudlFindMatchUrlWithEncodedQueryString()
        {
            string url = string.Format("http://www.google.com/search?q={0}", HttpUtility.UrlEncode("a+b"));

            ICompare comparer = new UriComparer(new Uri(url));
            Assert.That(comparer.Compare(url), Is.True);
        }

        [Test]
        public void WhenEncounteringABadUrlCompareShouldReturnFalse()
        {
            // GIVEN
            string badUrl = "bad.formated@url";
            try
            {
                new Uri(badUrl);
                Assert.Fail("Precondition failed");
            }
            catch (UriFormatException)
            {
                // OK;
            }
            catch (Exception e)
            {
                Assert.Fail("Precondition: Unexpected exception " + e);
            }

            UriComparer comparer = new UriComparer(new Uri("http://www.watin.net"));

            // WHEN
            bool compare = comparer.Compare(badUrl);

            // THEN
            Assert.That(compare, Is.False);

        }
	}
}
