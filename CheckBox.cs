using mshtml;

namespace WatiN
{
  public class CheckBox : RadioCheck
  {
    public CheckBox(DomContainer ie, IHTMLInputElement inputElement) : base(ie, inputElement)
    {}
  }
}