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

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using WatiN.Core.Interfaces;
using WatiN.Core.Logging;
using WatiN.Core.Mozilla;
using WatiN.Core.UnitTests.CrossBrowserTests;

namespace WatiN.Core.UnitTests.Mozilla
{
    [TestFixture]
    public class ElementsTests : CrossBrowserTest
    {
        /// <summary>
        /// Tests retrieving most of the standard element attributes.
        /// </summary>
        [Test]
        public void GetAttributes()
        {
        	ExecuteTest(GetAttributesTest, false);
        }
        
        private void GetAttributesTest(IBrowser browser)
        {
        	browser.GoTo(MainURI);
            IElement element = browser.Element("testElementAttributes");
            Assert.AreEqual("testElementAttributes", element.Id, "Id attribute incorrect");
            Assert.AreEqual("p1main", element.ClassName, "css attribute incorrect");
        }
        
        [Test]
		public void GetAttributeValueOfEmptyStringThrowsArgumentNullExceptionTest()
		{
			ExecuteTest(GetAttributeValueOfEmptyStringThrowsArgumentNullExceptionTest, false);
		}
		
		private void GetAttributeValueOfEmptyStringThrowsArgumentNullExceptionTest(IBrowser browser)
		{
			try
			{
	        	browser.GoTo(MainURI);
				IElement helloButton = browser.Element("helloid");
				Assert.IsNull(helloButton.GetAttributeValue(String.Empty));
				Assert.Fail("Expected ArgumentNullException.");
			}
			catch(ArgumentNullException)
			{
				// As expected
			}
			catch
			{
				throw;
			}
		}

		[Test]
		public void GetAttributeValueOfNullThrowsArgumentNullException()
		{
			ExecuteTest(GetAttributeValueOfNullThrowsArgumentNullExceptionTest, false);
		}
		
		private void GetAttributeValueOfNullThrowsArgumentNullExceptionTest(IBrowser browser)
		{
			try
			{
	        	browser.GoTo(MainURI);
				IElement helloButton = browser.Element("helloid");
				Assert.IsNull(helloButton.GetAttributeValue(null));
				Assert.Fail("Expected ArgumentNullException.");
			}
			catch(ArgumentNullException)
			{
				// As expected
			}
			catch
			{
				throw;
			}
		}

		[Test]
		public void GetInvalidAttribute()
		{
			ExecuteTest(GetInvalidAttributeTest, false);
		}
		
		private void GetInvalidAttributeTest(IBrowser browser)
		{
		  	browser.GoTo(MainURI);
			IElement helloButton = browser.Element("helloid");
			Assert.IsNull(helloButton.GetAttributeValue("NONSENCE"));
		}

		[Test]
		public void GetValidButUndefiniedAttribute()
		{
			ExecuteTest(GetValidButUndefiniedAttributeTest, false);
		}
		
		private void GetValidButUndefiniedAttributeTest(IBrowser browser)
		{
		   	browser.GoTo(MainURI);
			IElement helloButton = browser.Element("helloid");
			Assert.IsNull(helloButton.GetAttributeValue("title"));
		}
    }
}
