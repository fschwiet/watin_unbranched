using System.Threading;

using mshtml;

using WatiN.Exceptions;
using WatiN.Logging;

namespace WatiN
{
  public class Element
  {
    private DomContainer ie;
    protected object element = null;
    
    private string originalcolor;

    public Element(DomContainer ie, object element)
    {
      this.ie = ie;
      this.element = element;
    }

    public string ClassName
    {
      get { return htmlElement.className; }
    }

    public bool Enabled
    {
      get { return !htmlElement3.disabled; }
    }

    public string Id
    {
      get { return htmlElement.id; }
    }

    public virtual string Text
    {
      get { return htmlElement.innerText; }
    }

    public string InnerHtml
    {
      get { return htmlElement.innerHTML; }
    }

    public string OuterText
    {
      get { return htmlElement.outerText; }
    }

    public string OuterHtml
    {
      get { return htmlElement.outerHTML; }
    }

    public string TagName
    {
      get { return htmlElement.tagName; }
    }

    public string Title
    {
      get { return htmlElement.title; }
    }

    public Element NextSibling
    {
      get { return new Element(ie, domNode.nextSibling); }
    }

    public Element Parent
    {
      get { return new Element(ie, domNode.parentNode); }
    }

    /// <summary>
    /// This methode can be used if the attribute isn't available as a property of
    /// Element or a subclass of Element.
    /// </summary>
    /// <param name="attributename"></param>
    /// <returns></returns>
    public string GetAttributeValue(string attributename)
    {
      try
      {
        return (string)htmlElement.getAttribute(attributename, 0);
      }
      catch
      {
        return null;
      }
    }

    public override string ToString()
    {
      if (!IsNullOrEmpty(Title))
      {
        return Title;
      }
      return Text;
    }

    public void Click()
    {
      if (!Enabled) { throw new ElementDisabledException(Id); }

      Logger.LogAction("Clicking " + GetType().Name + " '" + ToString() + "'");
      HighLight(true);

      dispHtmlBaseElement.click();

      try
      {
        // When Click is called by ClickNoWait, WaitForComplete throws
        // an exception in get_Frames. Uncomment catch to see the details 
        WaitForComplete();
      }
//      catch (Exception e)
//      {
//        Debug.WriteLine(e.ToString());
//      }
      finally
      {
        HighLight(false);
      }
    }

    public void ClickNoWait()
    {
      if (!Enabled) { throw new ElementDisabledException(Id); }

      Logger.LogAction("Clicking (no wait) " + GetType().Name + " '" + ToString() + "'");

      HighLight(true);

      Thread clickButton = new Thread(new ThreadStart(this.Click));
      clickButton.Start();
      clickButton.Join(500);

      HighLight(false);
    }

    public void Focus()
    {
      if (!Enabled) { throw new ElementDisabledException(Id); }

      dispHtmlBaseElement.focus();
      FireEvent("onFocus");
    }

    public void DoubleClick()
    {
      if (!Enabled) { throw new ElementDisabledException(Id); }

      Logger.LogAction("Doubleclicking " + GetType().Name + " '" + ToString() + "'");

      FireEvent("onDblClick");
    }

    public void KeyDown()
    {
      FireEvent("onKeyDown");
    }

    public void KeyPress()
    {
      FireEvent("onKeyPress");
    }

    public void KeyUp()
    {
      FireEvent("onKeyUp");
    }

    public void MouseEnter()
    {
      FireEvent("onMouseEnter");
    }

    public void Blur()
    {
      FireEvent("onBlur");
    }

    public void Change()
    {
      FireEvent("onChange");
    }

    public void MouseDown()
    {
      FireEvent("onmousedown");
    }

    public void FireEvent(string eventName)
    {
      if (!Enabled) { throw new ElementDisabledException(Id); }

      HighLight(true);
      ie.FireEvent(dispHtmlBaseElement, eventName);
      WaitForComplete();
      HighLight(false);
    }

    public void Flash()
    {
      for (int counter = 0; counter < 5; counter++)
      {
        HighLight(true);
        Thread.Sleep(250);
        HighLight(false);
        Thread.Sleep(250);
      }
    }

    protected void HighLight(bool doHighlight)
    {
      if (doHighlight)
      {
        try
        {
          originalcolor = (string)htmlElement.style.backgroundColor;
          htmlElement.style.backgroundColor = "yellow";
        }
        catch
        {
          originalcolor = null;
        }
      }
      else
      {
        try
        {
          if (originalcolor != null)
          {
            htmlElement.style.backgroundColor = originalcolor;
          }
          else
          {
            htmlElement.style.backgroundColor = "transparent";
          }
        }

        catch {}
        finally
        {
          originalcolor = null;
        }
      }
    }

    protected static bool IsNullOrEmpty(string name)
    {
      return name == null || name == string.Empty;
    }

    private IHTMLElement htmlElement
    {
      get { return (IHTMLElement) element; }
    }

    private IHTMLElement3 htmlElement3
    {
      get { return (IHTMLElement3) element; }
    }

    private IHTMLDOMNode domNode
    {
      get { return (IHTMLDOMNode) element; }
    }

    protected DispHTMLBaseElement dispHtmlBaseElement
    {
      get { return (DispHTMLBaseElement) element; }
    }

    protected DomContainer Ie
    {
      get { return ie; }
    }

    public void WaitForComplete()
    {
      ie.WaitForComplete();
    }
  }
}