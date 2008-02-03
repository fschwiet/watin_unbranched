using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using WatiN.Core;
using WatiN.Core.Interfaces;

namespace WatiN.Samples.Console
{
    public class Program : BaseTest
    {
        [STAThread]
        static void Main(string[] args)
        {
            using (Program program = new Program())
            {
                // Simple method
                program.SearchForWatiNOnGoogleVerbose();
                
                // Generic method
                program.ExecuteTest(program.SearchForWatiNOnGoogleUsingBaseTest);
            }
        }

        /// <summary>
        /// Searches for WatiN on google using both Internet Explorer and Firefox.
        /// </summary>        
        private void SearchForWatiNOnGoogleVerbose()
        {
            using (IBrowser ie = BrowserFactory.Create(BrowserType.InternetExplorer))
            {
                GoTo("http://www.google.com", ie);
                ie.TextField(Find.ByName("q")).Value = "WatiN";
                ie.Button(Find.ByName("btnG")).Click();
                Debug.Assert(ie.ContainsText("WatiN"));
            }

            using (IBrowser firefox = BrowserFactory.Create(BrowserType.FireFox))
            {
                GoTo("http://www.google.com", firefox);
                firefox.TextField(Find.ByName("q")).Value = "WatiN";
                firefox.Button(Find.ByName("btnG")).Click();
                Debug.Assert(firefox.ContainsText("WatiN"));
            }
        }

        /// <summary>
        /// Searches for WatiN on google using the passed in <paramref name="browser"/>.
        /// </summary>
        /// <param name="browser">The browser.</param>
        private void SearchForWatiNOnGoogleUsingBaseTest(IBrowser browser)
        {
            GoTo("http://www.google.com", browser);                
            browser.TextField(Find.ByName("q")).Value = "WatiN";
            browser.Button(Find.ByName("btnG")).Click();
            Debug.Assert(browser.ContainsText("WatiN"));
        }
    }
}
