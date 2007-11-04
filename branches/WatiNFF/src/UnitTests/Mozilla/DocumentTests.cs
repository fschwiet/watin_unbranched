using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using WatiN.Core.Logging;
using WatiN.Core.Mozilla;

namespace WatiN.Core.UnitTests.Mozilla
{
    [TestFixture]
    public class DocumentTests
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            Logger.LogWriter = new DebugLogWriter();
        }

        [Test]
        public void Title()
        {
            using (FireFox ff = new FireFox())
            {
                ff.GoTo(BaseElementsTests.MainURI.ToString());
                Assert.AreEqual("Main", ff.Title);
            }
        }
        
        /// <summary>
        /// Test you can find a text field by id
        /// </summary>
        [Test]
        public void FindTextFieldById()
        {
            using (FireFox ff = new FireFox())
            {
                ff.GoTo(BaseElementsTests.MainURI.ToString());
                Assert.AreEqual(BaseElementsTests.MainURI, ff.Url); 
                
                WatiN.Core.Mozilla.TextField nameTextField = ff.TextField("name") as Core.Mozilla.TextField;
                Assert.IsNotNull(nameTextField, "Text field should not be null");
                Assert.AreEqual("name", nameTextField.Id);
            }
        }
    }
}
