using mshtml;

namespace WatiN
{
  public class Span : ElementsContainer
  {
    public Span(DomContainer ie, HTMLSpanElement HTMLSpanElement) : base(ie, (IHTMLElement) HTMLSpanElement)
    {}
  }
}