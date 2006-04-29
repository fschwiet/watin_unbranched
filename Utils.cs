using mshtml;

namespace WatiN.Utils
{
	/// <summary>
	/// Summary description for Utils.
	/// </summary>
	public class Utils
	{
    public static void dumpElements(Document document)
    {
      System.Diagnostics.Debug.WriteLine("Dump:");
      IHTMLElementCollection elements = elementCollection(document);
      foreach (IHTMLElement e in elements)
      {
        System.Diagnostics.Debug.WriteLine("id = " + e.id);
      }
    }

    public static void dumpElementsElab(Document document)
    {
      System.Diagnostics.Debug.WriteLine("Dump:==================================================");
      IHTMLElementCollection elements = elementCollection(document);
      foreach (IHTMLElement e in elements)
      {
        System.Diagnostics.Debug.WriteLine("------------------------- " + e.id);
        System.Diagnostics.Debug.WriteLine(e.outerHTML);
      }
    }

    public static void ShowFrames(Document document)
    {
      FrameCollection frames = document.Frames;

      System.Diagnostics.Debug.WriteLine("There are " + frames.length.ToString() + " Frames", "WatiN");
      
      int index = 0;
      foreach(Frame frame in frames)
      {
        System.Diagnostics.Debug.Write("Frame index: " + index.ToString());
        System.Diagnostics.Debug.Write(" name: " + frame.Name);
        System.Diagnostics.Debug.WriteLine(" scr: " + frame.Url);
        
        index++;
      }
    }

    private static IHTMLElementCollection elementCollection(Document document)
    {
      return document.HtmlDocument.all;
    }
  }
}
