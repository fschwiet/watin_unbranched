using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using WatiN.Core.Interfaces;
using WatiN.Core.Logging;

namespace WatiN.Core.UnitTests.CrossBrowserTests
{
    [TestFixture]
    public abstract class WatiNCrossBrowserTest : CrossBrowserTest
    {
        public static Uri MainURI = WatiNTest.MainURI;
        public static Uri TablesURI = WatiNTest.TablesURI;
        public static Uri ImagesURI = WatiNTest.ImagesURI;
        public static Uri FramesetURI = WatiNTest.FramesetURI;
        public static Uri FormSubmitURI = WatiNTest.FormSubmitURI;
        public static Uri IFramesMainURI = WatiNTest.IFramesMainURI;
		
    }
}
