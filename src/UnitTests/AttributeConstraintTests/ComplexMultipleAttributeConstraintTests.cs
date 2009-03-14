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

using NUnit.Framework;
using Rhino.Mocks;
using WatiN.Core.Constraints;
using WatiN.Core.Interfaces;

namespace WatiN.Core.UnitTests.AttributeConstraintTests
{
    [TestFixture]
    public class ComplexMultipleAttributeConstraintTests
    {
        private MockRepository mocks;
        private IAttributeBag mockAttributeBag;

        private BaseConstraint findBy1;
        private BaseConstraint findBy2;
        private BaseConstraint findBy3;
        private BaseConstraint findBy4;
        private BaseConstraint findBy5;

        private BaseConstraint findBy;

        [SetUp]
        public void Setup()
        {
            mocks = new MockRepository();
            mockAttributeBag = (IAttributeBag) mocks.CreateMock(typeof (IAttributeBag));
            findBy = null;

            findBy1 = Find.By("1", "true");
            findBy2 = Find.By("2", "true");
            findBy3 = Find.By("3", "true");
            findBy4 = Find.By("4", "true");
            findBy5 = Find.By("5", "true");
        }

        [Test]
        public void WithoutBrackets()
        {
            findBy = findBy1.And(findBy2).And(findBy3).Or(findBy4).And(findBy5);
        }

        [Test]
        public void WithBrackets()
        {
            findBy = findBy1.And(findBy2.And(findBy3)).Or(findBy4.And(findBy5));
        }

        [Test]
        public void WithBracketsOperators1()
        {
            findBy = findBy1 & findBy2 & findBy3 | findBy4 & findBy5;
        }

        [Test]
        public void WithBracketsOperators2()
        {
            findBy = findBy1 && findBy2 && findBy3 || findBy4 && findBy5;
        }

        [TearDown]
        public void TearDown()
        {
            Expect.Call(mockAttributeBag.GetValue("1")).Return("true");
            Expect.Call(mockAttributeBag.GetValue("2")).Return("false");
            Expect.Call(mockAttributeBag.GetValue("4")).Return("true");
            Expect.Call(mockAttributeBag.GetValue("5")).Return("false");

            mocks.ReplayAll();

            Assert.IsFalse(findBy.Compare(mockAttributeBag));

            mocks.VerifyAll();
        }

//    [Test]
//    public void testAndOr()
//    {
//      Assert.IsTrue(EchoBoolean(1) && EchoBoolean(5) && EchoBoolean(3) || EchoBoolean(2) && EchoBoolean(6));
//    }
//
//    public bool EchoBoolean(int value)
//    {
//      System.Diagnostics.Debug.WriteLine(value.ToString());
//      if (value==1) return true;
//      if (value==2) return true;
//      if (value==3) return true;
//      if (value==4) return true;
//      return false;
//    }
    }
}