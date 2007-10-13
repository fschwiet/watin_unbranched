namespace WatiN.Core.UnitTests
{
  using System.Collections;
  using System.Text.RegularExpressions;
  using NUnit.Framework;

  [TestFixture]
  public class LinkTests : BaseElementsTests
  {
    [Test]
    public void LinkElementTags()
    {
      Assert.AreEqual(1, Link.ElementTags.Count, "1 elementtags expected");
      Assert.AreEqual("a", ((ElementTag) Link.ElementTags[0]).TagName);
    }

    [Test]
    public void LinkFromElement()
    {
      Element element = ie.Element("testlinkid");
      Link link = new Link(element);
      Assert.AreEqual("testlinkid", link.Id);
    }

    [Test]
    public void LinkExists()
    {
      Assert.IsTrue(ie.Link("testlinkid").Exists);
      Assert.IsTrue(ie.Link(new Regex("testlinkid")).Exists);
      Assert.IsFalse(ie.Link("nonexistingtestlinkid").Exists);
    }

    [Test]
    public void LinkTest()
    {
      Assert.AreEqual(WatiNURI, ie.Link(Find.ById("testlinkid")).Url);
      Assert.AreEqual(WatiNURI, ie.Link("testlinkid").Url);
      Assert.AreEqual(WatiNURI, ie.Link(Find.ByName("testlinkname")).Url);
      Assert.AreEqual(WatiNURI, ie.Link(Find.ByUrl(WatiNURI)).Url);
      Assert.AreEqual("Microsoft", ie.Link(Find.ByText("Microsoft")).Text);
    }

    [Test]
    public void Links()
    {
      const int expectedLinkCount = 3;

      Assert.AreEqual(expectedLinkCount, ie.Links.Length, "Unexpected number of links");

      LinkCollection links = ie.Links;

      // Collection items by index
      Assert.AreEqual(expectedLinkCount, links.Length, "Wrong number off links");
      Assert.AreEqual("testlinkid", links[0].Id);
      Assert.AreEqual("testlinkid1", links[1].Id);

      // Collection iteration and comparing the result with Enumerator
      IEnumerable linksEnumerable = links;
      IEnumerator linksEnumerator = linksEnumerable.GetEnumerator();

      int count = 0;
      foreach (Link link in links)
      {
        linksEnumerator.MoveNext();
        object enumLink = linksEnumerator.Current;

        Assert.IsInstanceOfType(link.GetType(), enumLink, "Types are not the same");
        Assert.AreEqual(link.OuterHtml, ((Link) enumLink).OuterHtml, "foreach and IEnumator don't act the same.");
        ++count;
      }

      Assert.IsFalse(linksEnumerator.MoveNext(), "Expected last item");
      Assert.AreEqual(expectedLinkCount, count);
    }
  }
}