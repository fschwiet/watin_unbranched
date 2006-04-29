using mshtml;

using WatiN.Logging;

namespace WatiN
{
  public class RadioCheck : Element
  {
    public RadioCheck(DomContainer ie, IHTMLInputElement inputElement) : base(ie, (IHTMLElement) inputElement)
    {}

    public bool Checked
    {
      get { return inputElement.@checked; }
      set
      {
        Logger.LogAction("Selecting " + GetType().Name + " '" + ToString() + "'");
        
        HighLight(true);
        inputElement.@checked = value;
        FireEvent("onClick");
        HighLight(false);
      }
    }

    public override string ToString()
    {
      return Id;
    }

    private IHTMLInputElement inputElement
    {
      get { return ((IHTMLInputElement) base.element); }
    }
  }
}