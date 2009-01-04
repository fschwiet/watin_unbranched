#region WatiN Copyright (C) 2006-2008 Jeroen van Menen

//Copyright 2006-2008 Jeroen van Menen
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
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using WatiN.Core.Comparers;
using WatiN.Core.Constraints;
using WatiN.Core.Interfaces;

namespace WatiN.Core.UnitTests
{
	[TestFixture]
	public class ModalPopupExtenderTests : BaseWithBrowserTests
	{
		[Test, Category("InternetConnectionNeeded")]
		public void ModalPopupExtenderTest()
		{
            Div modalDialog = Ie.Div("ctl00_SampleContent_Panel1");
		    Assert.That(modalDialog.Parent.Style.Display, Is.EqualTo("none"), "modaldialog should not be visible");

			// Show the modaldialog
            Ie.Link("ctl00_SampleContent_LinkButton1").Click();

			modalDialog.WaitUntil(new VisibleAttribute(true), 5);
			Assert.IsTrue(modalDialog.Style.Display != "none", "modaldialog should be visible");

			// Hide the modaldialog
            Button cancel = modalDialog.Button("ctl00_SampleContent_CancelButton");
			cancel.Click();
			modalDialog.WaitUntil(new VisibleAttribute(false), 5);
            Assert.That(modalDialog.Parent.Style.Display, Is.EqualTo("none"), "modaldialog should be visible again");
		}

	    public override Uri TestPageUri
	    {
            get { return new Uri("http://www.asp.net/AJAX/AjaxControlToolkit/Samples/ModalPopup/ModalPopup.aspx"); }
	    }
	}

	public class VisibleAttribute : AttributeConstraint
	{
		public VisibleAttribute(bool visible) : base("visible", new BoolComparer(visible)) {}

		protected override bool DoCompare(IAttributeBag attributeBag)
		{
            ElementAttributeBag bag = attributeBag as ElementAttributeBag;

            if (bag == null) return false;

			return comparer.Compare(IsVisible(bag.Element).ToString());
		}

		public bool IsVisible(Element element)
		{
			bool isVisible = true;
			if (element.Parent != null)
			{
				isVisible = IsVisible(element.Parent);
			}

			if (isVisible)
			{
				isVisible = (element.Style.Display != "none");
			}

			return isVisible;
		}
	}

	public class MyIE : IE
	{
		public MyIE(string url) : base(url) {}

		public override void WaitForComplete()
		{
			Link("showModalPopupClientButton").WaitUntilExists();
		}
	}
}