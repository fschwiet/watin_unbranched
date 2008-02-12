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
using WatiN.Core.Interfaces;

namespace WatiN.Core.UnitTests.CrossBrowserTests
{
    /// <summary>
    /// Tests the behaviour of the concrete implementations of <see cref="IImage"/>.
    /// </summary>
    public class IImageTests : WatiNCrossBrowserTest
    {
        #region Public instance test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="IImage.Alt"/> property.
        /// </summary>
        [Test]
        public void Alt()
        {
            ExecuteTest(AltTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IImage.Name"/> property.
        /// </summary>
        [Test]
        public void Name()
        {
            ExecuteTest(NameTest);
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IImage.Src"/> property.
        /// </summary>
        [Test]
        public void Src()
        {
            ExecuteTest(SrcTest);
        }

        #endregion


        #region Private static test methods

        /// <summary>
        /// Tests the behaviour of the <see cref="IImage.Alt"/> property.
        /// </summary>
        private static void AltTest(IBrowser browser)
        {
            browser.GoTo(ImagesURI);
            IImage image = browser.Image("Image1");

            Assert.AreEqual("WatiN logo", image.Alt, GetErrorMessage("Incorrect Alt value returned", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IImage.Name"/> property.
        /// </summary>
        private static void NameTest(IBrowser browser)
        {
            browser.GoTo(ImagesURI);
            IImage image = browser.Image("Image2");

            Assert.AreEqual("ImageName2", image.Name, GetErrorMessage("Incorrect name value returned", browser));
        }

        /// <summary>
        /// Tests the behaviour of the <see cref="IImage.Src"/> property.
        /// </summary>
        private static void SrcTest(IBrowser browser)
        {
            browser.GoTo(ImagesURI);
            IImage image = browser.Image("Image1");

            Assert.IsTrue(image.Src.EndsWith("images/watin.jpg"), GetErrorMessage("Incorrect src value returned", browser));
        }

        #endregion

    }
}