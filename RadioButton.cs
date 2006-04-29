using mshtml;

namespace WatiN
{
  public class RadioButton : RadioCheck
  {
    public RadioButton(DomContainer ie, IHTMLInputElement inputElement) : base(ie, inputElement)
    {}
  }
}