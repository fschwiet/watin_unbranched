#region WatiN Copyright (C) 2006 Jeroen van Menen

// WatiN (Web Application Testing In dotNet)
// Copyright (C) 2006 Jeroen van Menen
//
// This library is free software; you can redistribute it and/or modify it under the terms of the GNU 
// Lesser General Public License as published by the Free Software Foundation; either version 2.1 of 
// the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without 
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU 
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License along with this library; 
// if not, write to the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 
// 02111-1307 USA 

#endregion Copyright

using mshtml;

namespace WatiN.Core
{
	/// <summary>
	/// Summary description for Utils.
	/// </summary>
	public sealed class UtilityClass
	{
    /// <summary>
    /// Prevent creating an instance of this class (contains only static members)
    /// </summary>
    private UtilityClass(){}

    public static void DumpElements(Document document)
    {
      System.Diagnostics.Debug.WriteLine("Dump:");
      IHTMLElementCollection elements = elementCollection(document);
      foreach (IHTMLElement e in elements)
      {
        System.Diagnostics.Debug.WriteLine("id = " + e.id);
      }
    }

    public static void DumpElementsWithHtmlSource(Document document)
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

      System.Diagnostics.Debug.WriteLine("There are " + frames.Length.ToString() + " Frames", "WatiN");
      
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
