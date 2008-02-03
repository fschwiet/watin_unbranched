using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using WatiN.Core.Logging;
using WatiN.Core.Mozilla;
using WatiN.Core.UnitTests.CrossBrowserTests;

namespace WatiN.Core.UnitTests.Mozilla
{
    [TestFixture]
    public class DocumentTests : CrossBrowserTest
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            Logger.LogWriter = new DebugLogWriter();
        }

        [Test]
        public void Title()
        {
            GoTo(MainURI, Firefox);
            Assert.AreEqual("Main", Firefox.Title);
        }

        /// <summary>
        /// Test you can find a text field by id
        /// </summary>
        [Test]
        public void FindTextFieldById()
        {
            GoTo(MainURI, Firefox);
            Assert.AreEqual(BaseElementsTests.MainURI, Firefox.Url);

            WatiN.Core.Interfaces.ITextField nameTextField = Firefox.TextField("name") as Core.Mozilla.TextField;
            Assert.IsNotNull(nameTextField, "Text field should not be null");
            Assert.AreEqual("name", nameTextField.Id);
        }

        /// <summary>
        /// Test the behaviour of the <see cref="Core.Mozilla.Document.Text"/> property.
        /// </summary>
        [Test]
        public void Text()
        {
            GoTo(MainURI, Firefox);
            string documentText = Firefox.Text;

            Assert.IsTrue(documentText.Length > 2000, string.Format("Error occured retrieving the Document.Text value. Expected the length of the result to be greater than 2000 bytes, instead length was: {0}", documentText.Length));
        }
    }
}
