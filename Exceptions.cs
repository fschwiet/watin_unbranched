using System;

namespace WatiN.Exceptions
{
  public class WatiNException : ApplicationException
  {
    public WatiNException() : base()
    {}

    public WatiNException(string message) : base(message)
    {}
  }

  public class MissingAlertException : WatiNException
  {}

  public class ElementNotFoundException : WatiNException
  {
    private string message = "";

    public ElementNotFoundException(string tagName, string attributeName, string value) : base()
    {
      message = "Could not find a '" + tagName + "' tag containing attribute " + attributeName + " with value '" + value + "'";
    }

    public override string Message
    {
      get { return message; }
    }
  }

  public class FrameNotFoundException : WatiNException
  {
    private string message = "";

    public FrameNotFoundException(string attributeName, string value) : base()
    {
      message = "Could not find a frame by " + attributeName + " with value '" + value + "'";
    }

    public override string Message
    {
      get { return message; }
    }
  }

  public class HTMLDialogNotFoundException : WatiNException
  {
    private string message = "";

    public HTMLDialogNotFoundException(string attributeName, string value, int waitTimeInSeconds) : base()
    {
      message = "Could not find a HTMLDialog by " + attributeName + " with value '" + value + "'. (Search expired after '" + waitTimeInSeconds.ToString() + "' seconds)";
    }

    public override string Message
    {
      get { return message; }
    }
  }

  public class IENotFoundException : WatiNException
  {
    private string message = "";

    public IENotFoundException(string findBy, string value, int waitTimeInSeconds) : base()
    {
      message = "Could not find an IE window by " + findBy + " with value '" + value + "'. (Search expired after '" + waitTimeInSeconds.ToString() + "' seconds)";
    }

    public override string Message
    {
      get { return message; }
    }
  }

  public class ElementDisabledException : WatiNException
  {
    private string message = "";

    public ElementDisabledException(string elementId) : base()
    {
      message = "Element with Id:" + elementId + " is disabled";
    }

    public override string Message
    {
      get { return message; }
    }
  }

  public class ElementReadOnlyException : WatiNException
  {
    private string message = "";

    public ElementReadOnlyException(string elementId) : base()
    {
      message = "Element with Id:" + elementId + " is readonly";
    }

    public override string Message
    {
      get { return message; }
    }
  }

  public class SelectListItemNotFoundException : WatiNException
  {
    private string message = "";

    public SelectListItemNotFoundException(string value) : base()
    {
      message = "No item with text or value '" + value + "' was found in the selectlist";
    }

    public override string Message
    {
      get { return message; }
    }
  }

  public class TimeOutException : WatiNException
  {
    private string message = "";

    public TimeOutException(string value) : base()
    {
      message = "Timeout while '" + value + "'";
    }

    public override string Message
    {
      get { return message; }
    }
  }
}
