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

using System.Collections;
using mshtml;

namespace WatiN.Core
{
  /// <summary>
  /// This class provides specialized functionality for a HTML tr element.
  /// </summary>
  public class TableRow : ElementsContainer
  {
    private static ArrayList elementTags;

    public static ArrayList ElementTags
    {
      get
      {
        if (elementTags == null)
        {
          elementTags = new ArrayList();
          elementTags.Add(new ElementTag("tr"));
        }

        return elementTags;
      }
    }

    public TableRow(DomContainer ie, IHTMLTableRow htmlTableRow) : base(ie, (IHTMLElement) htmlTableRow)
    {}
    
    public TableRow(DomContainer ie, ElementFinder finder) : base(ie, finder)
    {}
 
    /// <summary>
    /// Initialises a new instance of the <see cref="TableRow"/> class based on <paramref name="element"/>.
    /// </summary>
    /// <param name="element">The element.</param>
    public TableRow(Element element) : base(element, ElementTags)
    {}

  }
}