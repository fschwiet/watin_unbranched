using mshtml;

using WatiN.Logging;

namespace WatiN
{
  public class Form : ElementsContainer
  {
    public Form(DomContainer ie, HTMLFormElement htmlFormElement) : base(ie, (IHTMLElement) htmlFormElement)
    {}

    public void Submit()
    {
      Logger.LogAction("Submitting " + GetType().Name + " '" + ToString() + "'");

      HtmlFormElement.submit();
      WaitForComplete();
    }

    public override string ToString()
    {
      if (!IsNullOrEmpty(Title))
      {
        return Title;
      }
      if (!IsNullOrEmpty(Id))
      {
        return Id;
      }
      if (!IsNullOrEmpty(Name))
      {
        return Name;
      }
      return base.ToString ();
    }

    public string Name
    {
      get
      {
        return HtmlFormElement.name;
      }
    }

    private HTMLFormElement HtmlFormElement
    {
      get {
        return (HTMLFormElement)element;
      }
    }
  }
}