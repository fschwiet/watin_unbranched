using mshtml;

namespace WatiN
{
  public class Button : Element
  {
    public Button(DomContainer ie, HTMLInputElement inputButtonElement) : base(ie, (IHTMLElement) inputButtonElement)
    {}

    public string Value
    {
      get { return inputButtonElement.value; }
    }

    /// <summary>
    /// Returns the same as the Value property
    /// </summary>
    public override string Text
    {
      get { return this.Value; }
    }

    private HTMLInputElement inputButtonElement
    {
      get { return ((HTMLInputElement) base.element); }
    }

    public override string ToString()
    {
      return this.Value;
    }
  }
}