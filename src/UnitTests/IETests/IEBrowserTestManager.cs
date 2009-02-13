using System;
using WatiN.Core.UnitTests.Interfaces;

namespace WatiN.Core.UnitTests.IETests
{
    public class IEBrowserTestManager : IBrowserTestManager
    {
        private IE ie;

        public Browser CreateBrowser(Uri uri)
        {
            return new IE(uri);
        }

        public Browser GetBrowser(Uri uri)
        {
            if (ie == null)
            {
                ie = (IE) CreateBrowser(uri);
            }

            return ie;
        }

        public void CloseBrowser()
        {
            if (ie == null) return;
            ie.Close();
            ie = null;
        }
    }
}