using mshtml;

namespace WatiN
{
  public class Link : Element
  {
    public Link(DomContainer ie, HTMLAnchorElement htmlAnchorElement) : base(ie, htmlAnchorElement)
    {}

    public string Url
    {
      get { return ((IHTMLAnchorElement) base.element).href; }
    }
  }
}