using System;
using System.Threading;

using mshtml;

using WatiN.Exceptions;

namespace WatiN
{
  /// <summary>
  /// This class hosts functionality for classes which are an entry point
  /// to a document and its elements and/or frames. Currently implemented
  /// by IE and HTMLDialog.
  /// </summary>
  public abstract class DomContainer
  {
    private IHTMLDocument2 htmlDocument = null;
    private Document mainDocument = null;
    private DateTime startWaitForComplete;

    /// <summary>
    /// This method must be overriden by all sub classes
    /// </summary>
    public virtual IHTMLDocument2 OnGetHTMLDocument()
    {
      throw new NotImplementedException("This method must be overriden by all sub classes");
    }

    /// <summary>
    /// Returns the 'raw' html document for the internet explorer DOM.
    /// </summary>
    public HTMLDocument HtmlDocument
    {
      get
      {
        if (htmlDocument == null)
        {
          htmlDocument = OnGetHTMLDocument();
        }

        return (HTMLDocument)htmlDocument;
      }
    }

    /// <summary>
    /// Returns the main document of DomContainer
    /// </summary>
    public Document MainDocument
    {
      get
      {
        if (mainDocument == null)
        {
          mainDocument = new Document(this, (IHTMLDocument2) HtmlDocument);
        }

        return mainDocument;
      }
    }

    /// <summary>
    /// Fires the given event on the given element.
    /// </summary>
    /// <param name="element">Element to fire the event on</param>
    /// <param name="eventName">Name of the event to fire</param>
    public virtual void FireEvent(DispHTMLBaseElement element, string eventName)
    {
      object dummyEvt = null;
      object parentEvt = ((IHTMLDocument4)element.document).CreateEventObject(ref dummyEvt);
      //      IHTMLEventObj2 mouseDownEvent = (IHTMLEventObj2)parentEvt;
      //      mouseDownEvent.button = 1;
      element.FireEvent(eventName, ref parentEvt);
    }

    /// <summary>
    /// This method must be called by its inheritor to dispose references
    /// to internal resources.
    /// </summary>
    public void Dispose()
    {
      htmlDocument = null;
      if (mainDocument != null) mainDocument.Close();
      mainDocument = null;
    }

    /// <summary>
    /// This method calls InitTimeOut and wait till IE is ready
    /// processing or the timeout periode has expired.
    /// </summary>
    public virtual void WaitForComplete()
    {
      InitTimeOut();
      WaitForCompleteTimeOutIsInitialized();
    }

    /// <summary>
    /// This method waits till IE is ready
    /// processing or the timeout periode has expired.
    /// </summary>
    protected internal void WaitForCompleteTimeOutIsInitialized()
    {
      WaitWhileMainDocumentNotAvailable(this);
      WaitWhileDocumentStateNotComplete((IHTMLDocument2) HtmlDocument);

      int framesCount = HtmlDocument.frames.length;
      for (int i = 0; i != framesCount; ++i)
      {
        DispHTMLWindow2 frame = Frame.GetFrameFromHTMLDocument(i, (IHTMLDocument2) HtmlDocument);
        IHTMLDocument2 document = WaitWhileFrameDocumentNotAvailable(frame);

        WaitWhileDocumentStateNotComplete(document);
      }
    }

    /// <summary>
    /// This method is called to initialise the start time for a
    /// determining a time out at the current time.
    /// </summary>
    /// <returns></returns>
    protected internal DateTime InitTimeOut()
    {
      return startWaitForComplete = DateTime.Now;
    }

    /// <summary>
    /// This method checks the return value of IsTimedOut. When true, it will
    /// throw a TimeOutException with the timeOutMessage param as message.
    /// </summary>
    /// <param name="timeOutMessage">The message to present when the TimeOutException is thrown</param>
    protected internal void ThrowExceptionWhenTimeOut(string timeOutMessage)
    {
      if (IsTimedOut())
      {
        throw new TimeOutException(timeOutMessage);
      }
    }

    /// <summary>
    /// This method evaluates the time between the last call to InitTimeOut
    /// and the current time. If the timespan is more than 30 seconds, the
    /// return value will be true.
    /// </summary>
    /// <returns>If the timespan is more than 30 seconds, the
    /// return value will be true</returns>
    protected internal bool IsTimedOut()
    {
      return IsTimedOut(startWaitForComplete, 30);
    }

    protected static internal bool IsTimedOut(DateTime startTime, int durationInSeconds)
    {
      return DateTime.Now.Subtract(startTime).TotalSeconds > durationInSeconds;
    }

    private void WaitWhileDocumentStateNotComplete(IHTMLDocument2 htmlDocument)
    {
      while (((HTMLDocument)htmlDocument).readyState != "complete")
      {
        ThrowExceptionWhenTimeOut("waiting for document state complete. Last state was '" + ((HTMLDocument)htmlDocument).readyState + "'");
        Thread.Sleep(100);
      }
    }

    private void WaitWhileMainDocumentNotAvailable(DomContainer domContainer)
    {
      IHTMLDocument2 maindocument = null;

      while(maindocument == null)
      {
        ThrowExceptionWhenTimeOut("waiting for main document becoming available");

        try
        {
          maindocument = (IHTMLDocument2) domContainer.HtmlDocument;
        }
        catch
        {        
          Thread.Sleep(100);
        }

        if (!IsDocumentReadyStateAvailable(maindocument))
        {
          maindocument=null;
        }
      }
    }

    private static bool IsDocumentReadyStateAvailable(IHTMLDocument2 document)
    {
      if (document != null)
      {
        /// Sometimes an OutOfMemoryException or ComException occurs while accessing
        /// the readystate property of IHTMLDocument2. Giving MSHTML some time
        /// to do further processing seems to solve this problem.
        try
        {
          string readyState = ((HTMLDocument)document).readyState;
          return true;
        }
//        catch(System.OutOfMemoryException)
//        {
//          Thread.Sleep(500);
//        }
//        catch(System.Runtime.InteropServices.COMException)
//        {
//          Thread.Sleep(500);
//        }
        catch
        {
          Thread.Sleep(500);
        }
      }

      return false;
    }

    private IHTMLDocument2 WaitWhileFrameDocumentNotAvailable(DispHTMLWindow2 frame)
    {
      IHTMLDocument2 document = null;

      while (document == null)
      {
        bool isTimedout = IsTimedOut();

        try
        {
          document = frame.document;
        }
        catch(UnauthorizedAccessException)
        {
          if (isTimedout)
          { // TODO: Implement solution for Cross-domain scripting security.
            // Following urls provide more info:
            // KB Article KB196340: http://support.microsoft.com/default.aspx?scid=kb;en-us;196340
            // C# implementation of KB196340: http://www.colinneller.com/blog/PermaLink,guid,64eac67e-df2a-4a20-82f0-16b0c5ce9615.aspx

            throw new WatiNException("Could be Cross-domain scripting security. See KB Article KB196340 'HOWTO: Get the WebBrowser Object Model of an HTML Frame' - this technique bypasses security checks");
          }

          Thread.Sleep(400);

        }
        catch
        {
          if (isTimedout)
          {
            throw;
          }
        }

        if (isTimedout && frame == null)
        {
          throw new TimeOutException("waiting for frame document becoming available");
        }


        if (document == null)
        {
          Thread.Sleep(100);
        }
        else if(!IsDocumentReadyStateAvailable(document))
        {
          Thread.Sleep(500);
        }
      }

      return document;
    }
  }
}