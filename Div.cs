using mshtml;

namespace WatiN
{
  public class Div : ElementsContainer
  {
    public Div(DomContainer ie, HTMLDivElement HTMLDivElement) : base(ie, (IHTMLElement) HTMLDivElement)
    {}
  }
}