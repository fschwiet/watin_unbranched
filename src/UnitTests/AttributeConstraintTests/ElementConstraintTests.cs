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
using mshtml;
using NUnit.Framework;
using WatiN.Core.Constraints;
using Rhino.Mocks;
using WatiN.Core.Interfaces;
using WatiN.Core.InternetExplorer;
using Iz = NUnit.Framework.SyntaxHelpers.Is;

namespace WatiN.Core.UnitTests.AttributeConstraintTests
{
    [TestFixture]
    public class ElementConstraintTest : BaseWithIETests
    {
        [Test]
        public void ElementConstraintShouldCallComparerAndReturnTrue()
        {
            VerifyComparerIsUsed("testtagname", true);
        }

        [Test]
        public void ElementConstraintShouldCallComparerAndReturnFalse()
        {
            VerifyComparerIsUsed("tagname", false);
        }

        private static void VerifyComparerIsUsed(string tagname, bool expectedResult)
        {
            MockRepository mocks = new MockRepository();
            IHTMLElement IHTMLElementStub = (IHTMLElement) mocks.DynamicMock(typeof(IHTMLElement));
            DomContainer domContainer = (DomContainer) mocks.DynamicMock(typeof (DomContainer));
            ElementAttributeBag elementAttributeBag = new ElementAttributeBag(domContainer, IHTMLElementStub);

            SetupResult.For(IHTMLElementStub.getAttribute("tagName", 0)).Return("testtagname");
            SetupResult.For(domContainer.NativeBrowser).Return(new IEBrowser(domContainer));
			
            mocks.ReplayAll();

            ElementComparerMock elementComparerMock = new ElementComparerMock(tagname);
            ElementConstraint elementConstraint = new ElementConstraint(elementComparerMock);
			
            Assert.That(elementConstraint.Compare(elementAttributeBag) == expectedResult);

            mocks.VerifyAll();
        }

#if !NET11
        [Test]
        public void ShouldEvaluateAlsoTheAndConstraint()
        {
            Link link1 = ie.Link(Find.ByElement(
                                     delegate(Element l) { return l.Id != null && l.Id.StartsWith("testlink"); }) && Find.ByUrl("http://watin.sourceforge.net"));
            Link link2 = ie.Link(Find.ByElement(
                                     delegate(Element l) { return l.Id != null && l.Id.StartsWith("testlink"); }) && Find.ByUrl("http://www.microsoft.com/"));

            Assert.That(link1.Url, Iz.EqualTo("http://watin.sourceforge.net/"));
            Assert.That(link2.Url, Iz.EqualTo("http://www.microsoft.com/"));
        }
#endif

        public class ElementComparerMock : ICompareElement 
        {
            private readonly string _expectedTagName;
			
            public ElementComparerMock(string expectedTagName)
            {
                _expectedTagName = expectedTagName;
            }
            public bool Compare(Element element)
            {
                return _expectedTagName == element.TagName;
            }
        }

        public override Uri TestPageUri
        {
            get { return MainURI; }
        }
    }
}