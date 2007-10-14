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

namespace WatiN.Core.UnitTests
{
  using NUnit.Framework;
  using Rhino.Mocks;
  using WatiN.Core.Interfaces;

  [TestFixture]
  public class EvenMoreComplexMultipleAttributeConstraintsTests
  {
    private MockRepository mocks;
    private IAttributeBag mockAttributeBag;

    private AttributeConstraint findBy1;
    private AttributeConstraint findBy2;
    private AttributeConstraint findBy3;
    private AttributeConstraint findBy4;
    private AttributeConstraint findBy5;
    private AttributeConstraint findBy6;
    private AttributeConstraint findBy7;
    private AttributeConstraint findBy8;
    private AttributeConstraint findBy;

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
      findBy6 = Find.By("6", "true");
      findBy7 = Find.By("7", "true");
      findBy8 = Find.By("8", "true");
    }

    [Test]
    public void WithOperators()
    {
      findBy = findBy1 & findBy2 & findBy3 | findBy4 & findBy5 & findBy6 | findBy7 & findBy8;
    }

    [Test]
    public void WithoutBrackets()
    {
      findBy = findBy1.And(findBy2).And(findBy3).Or(findBy4).And(findBy5).And(findBy6).Or(findBy7).And(findBy8);
    }

    [Test]
    public void WithBrackets()
    {
      findBy = findBy1.And(findBy2.And(findBy3)).Or(findBy4.And(findBy5.And(findBy6))).Or(findBy7.And(findBy8));
    }

    [TearDown]
    public void TearDown()
    {
      Expect.Call(mockAttributeBag.GetValue("1")).Return("true");
      Expect.Call(mockAttributeBag.GetValue("2")).Return("false");
      Expect.Call(mockAttributeBag.GetValue("4")).Return("true");
      Expect.Call(mockAttributeBag.GetValue("5")).Return("false");
      Expect.Call(mockAttributeBag.GetValue("7")).Return("true");
      Expect.Call(mockAttributeBag.GetValue("8")).Return("true");

      mocks.ReplayAll();

      Assert.IsTrue(findBy.Compare(mockAttributeBag));

      mocks.VerifyAll();
    }
  }
}