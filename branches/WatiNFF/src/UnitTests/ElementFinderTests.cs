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

using NUnit.Framework;
using Rhino.Mocks;
using WatiN.Core.Interfaces;

namespace WatiN.Core.UnitTests
{
	[TestFixture]
	public class ElementFinderTests
	{
		private MockRepository mocks;
		private IElementCollection stubElementCollection;

		[SetUp]
		public void SetUp()
		{
			mocks = new MockRepository();
			stubElementCollection = (IElementCollection) mocks.DynamicMock(typeof (IElementCollection));

			SetupResult.For(stubElementCollection.Elements).Return(null);

			mocks.ReplayAll();
		}

		[TearDown]
		public void TearDown()
		{
			mocks.VerifyAll();
		}

		[Test]
		public void FindFirstShoudlReturnNullIfIElementCollectionIsNull()
		{
			ElementFinder finder = new ElementFinder("input", "text", stubElementCollection);

			Assert.IsNull(finder.FindFirst());
		}

		[Test]
		public void FindAllShouldReturnEmptyArrayListIfIElementCollectionIsNull()
		{
			ElementFinder finder = new ElementFinder("input", "text", stubElementCollection);

			Assert.AreEqual(0, finder.FindAll().Count);
		}
	}
}