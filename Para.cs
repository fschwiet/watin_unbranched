using mshtml;

namespace WatiN
{
  public class Para : ElementsContainer
  {
    public Para(DomContainer ie, HTMLParaElement HTMLParaElement) : base(ie, (IHTMLElement) HTMLParaElement)
    {}
  }
}