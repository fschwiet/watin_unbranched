﻿#region WatiN Copyright (C) 2006-2009 Jeroen van Menen

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
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace WatiN.Core.UnitTests
{
    [TestFixture]
    public class ControlCollectionTests : BaseWithBrowserTests
    {
        public override Uri TestPageUri
        {
            get { return MainURI; }
        }

        [Test]
        public void ControlCollectionShouldRespectElementConstraintOfControl()
        {
            ExecuteTestWithAnyBrowser(browser =>
            {
                // GIVEN
                Assert.That(browser.Forms.Count, Is.EqualTo(6), "Pre-condition: Unexpected number of forms");

                // WHEN
                var formsWithId = browser.Controls<FormWithElemenConstriantControl>();

                // THEN
                Assert.That(formsWithId.Count, Is.EqualTo(5), "Unexpected number of forms with Id starting with 'Form'");
            });
        }

        [Test]
        public void ControlCollectionShouldRespectElementConstraintOfControlWhenApplyingFilter()
        {
            ExecuteTestWithAnyBrowser(browser =>
            {
                // GIVEN
                Assert.That(browser.Forms.Count, Is.EqualTo(6), "Pre-condition: Unexpected number of forms");

                // WHEN
                var formsWithId = browser.Controls<FormWithElemenConstriantControl>().Filter(Find.ById("noneExistingId"));

                // THEN
                Assert.That(formsWithId.Count, Is.EqualTo(0), "Unexpected number of forms");
            });
        }

        [Test]
        public void ElementCollectionAsShouldRespectElementConstraintOfControl()
        {
            ExecuteTestWithAnyBrowser(browser =>
            {
                // GIVEN
                Assert.That(browser.Forms.Count, Is.EqualTo(6), "Pre-condition: Unexpected number of forms");

                // WHEN
                var formsWithId = browser.Forms.As<FormWithElemenConstriantControl>();

                // THEN
                Assert.That(formsWithId.Count, Is.EqualTo(5), "Unexpected number of forms with Id starting with 'Form'");
            });
        }

        [Test]
        public void ElementCollectionAsShouldRespectElementConstraintOfControlWhenApplyingFilter()
        {
            ExecuteTestWithAnyBrowser(browser =>
            {
                // GIVEN
                Assert.That(browser.Forms.Count, Is.EqualTo(6), "Pre-condition: Unexpected number of forms");

                // WHEN
                var formsWithId = browser.Forms.As<FormWithElemenConstriantControl>().Filter(Find.ById("noneExistingId"));

                // THEN
                Assert.That(formsWithId.Count, Is.EqualTo(0), "Unexpected number of forms");
            });
        }

        private class FormWithElemenConstriantControl : Control<Form>
        {
            public override Constraints.Constraint ElementConstraint
            {
                get { return Find.ByElement(e => !string.IsNullOrEmpty(e.Id) && e.Id.StartsWith("Form")); }
            }
        }

    }
}
