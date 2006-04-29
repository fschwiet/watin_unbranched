using System;

using mshtml;

using WatiN.Exceptions;

namespace WatiN
{
  public class Frame : Document
  {
    private string frameName = string.Empty;
    private string frameId = string.Empty;

    /// <summary>
    /// This constructor will mainly be used by the constructor of FrameCollection
    /// to create an instance of a Frame.
    /// </summary>
    /// <param name="ie"></param>
    /// <param name="htmlDocument"></param>
    /// <param name="name"></param>
    /// <param name="id"></param>
    public Frame(DomContainer ie, IHTMLDocument2 htmlDocument, string name, string id) : base(ie, htmlDocument)
    {
      Init(name, id);
    }

    /// <summary>
    /// This constructor will mainly be used by Document.Frame to find
    /// a Frame. A FrameNotFoundException will be thrown if the Frame isn't found.
    /// </summary>
    /// <param name="frames">Collection of frames to find the frame in</param>
    /// <param name="findBy">The name of the frame</param>
    public static Frame Find(FrameCollection frames, NameValue findBy)
    {
      return findFrame(frames, findBy);
    }
    /// <summary>
    /// This constructor will mainly be used by Document.Frame to find
    /// a Frame. A FrameNotFoundException will be thrown if the Frame isn't found.
    /// </summary>
    /// <param name="frames">Collection of frames to find the frame in</param>
    /// <param name="findBy">The Url of the Frame html page</param>
    public static Frame Find(FrameCollection frames, UrlValue findBy)
    {
      return findFrame(frames, findBy);
    }
    /// <summary>
    /// This constructor will mainly be used by Document.Frame to find
    /// a Frame. A FrameNotFoundException will be thrown if the Frame isn't found.
    /// </summary>
    /// <param name="frames">Collection of frames to find the frame in</param>
    /// <param name="findBy">The Id of the Frame</param>
    public static Frame Find(FrameCollection frames, IDValue findBy)
    {
      return findFrame(frames, findBy);
    }

    private static Frame findFrame(FrameCollection frames, AttributeValue findBy)
    {
      foreach (Frame frame in frames)
      {
        string compareValue = string.Empty;

        if (findBy is NameValue)
        {
          compareValue = frame.Name;
        }

        else if(findBy is UrlValue)
        {
            compareValue = frame.Url;
        }

        else if(findBy is IDValue)
        {
          compareValue = frame.Id;
        }

        if (findBy.Compare(compareValue))
        {
          // Reset
          return frame;
        }
      }

      throw new FrameNotFoundException(findBy.AttributeName, findBy.Value);
    }

    public string Name
    {
      get { return frameName; }
    }

    public string Id
    {
      get { return frameId; }
    }

    internal static DispHTMLWindow2 GetFrameFromHTMLDocument(int i, IHTMLDocument2 htmlDocument)
    {
      Object o = i;
      return (DispHTMLWindow2) htmlDocument.frames.item(ref o);
    }

    private void Init(string name, string id)
    {
      this.frameName = name;
      this.frameId = id;
    }
  }
}