using mshtml;

namespace WatiN
{
  public class Label : ElementsContainer
  {
    public Label(DomContainer ie, HTMLLabelElement labelElement) : base(ie, (IHTMLElement) labelElement)
    {}

    public string AccesKey
    {
      get {return ((IHTMLLabelElement)element).accessKey; }
    }

    public string For
    {
      get {return ((IHTMLLabelElement)element).htmlFor; }
    }
  }
}
