using mshtml;

namespace WatiN
{
  public class Image : Element
  {
    //TODO: Add image specific properties and methodes
    //mshtml.DispHTMLImg
    //mshtml.IHTMLImgElement
    //mshtml.IHTMLImgElement2

    public Image(DomContainer ie, IHTMLImgElement HTMLImg) : base(ie, (IHTMLElement) HTMLImg)
    {}
  }
}