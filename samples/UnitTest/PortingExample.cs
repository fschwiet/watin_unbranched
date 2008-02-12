using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using WatiN.Core;
using WatiN.Core.Interfaces;
using WatiN.Core.UnitTests;

namespace WatiN.Samples.UnitTest
{
    [TestFixture]
    public class PortingExample : CrossBrowserTest 
    {
        [Test]
        public void IETest()
        {
            using (IE internetExplorer = new IE())
            {
                internetExplorer.GoTo(Path.Combine(Environment.CurrentDirectory, "Tables.html"));
                Table table = internetExplorer.Table("Table1");

                Assert.IsNotNull(table, "Table not located");
                Assert.AreEqual("Table1", table.Id, "Incorrect table element found.");
                Assert.AreEqual(2, table.TableBodies.Length, "Incorrect no. of table bodies found.");

                TableCell cell = (TableCell) table.TableRows[0].TableCells[0];
                Assert.IsTrue(cell.Text.StartsWith("1"), "Cell's text did not start with the expected" 
                    + " character 1. Instead found: " + cell.Text);
            }
        }

        [Test]
        public void PortedTest()
        {
            ExecuteTest(PortedTest);
        }

        private void PortedTest(IBrowser browser)
        {
            browser.GoTo(Path.Combine(Environment.CurrentDirectory, "Tables.html"));
            ITable table = browser.Table("Table1");

            Assert.IsNotNull(table, "Table not located");
            Assert.AreEqual("Table1", table.Id, "Incorrect table element found.");
            Assert.AreEqual(2, table.TableBodies.Length, "Incorrect no. of table bodies found.");

            ITableCell cell = table.TableRows[0].TableCells[0];
            Assert.IsTrue(cell.Text.StartsWith("1"), "Cell's text did not start with the expected" 
                + " character 1. Instead found: " + cell.Text);
        }
    }
}
