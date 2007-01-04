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
  /// This class provides specialized functionality for a HTML input element of type 
  /// checkbox.
  /// </summary>
  public class CheckBox : RadioCheck
  {
    private static ArrayList elementTags;

    public static ArrayList ElementTags
    {
      get
      {
        if (elementTags == null)
        {
          elementTags = new ArrayList();
          elementTags.Add(new ElementTag("input", "checkbox"));
        }

        return elementTags;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CheckBox"/> class.
    /// Mainly used by WatiN internally.
    /// </summary>
    /// <param name="domContainer">The domContainer.</param>
    /// <param name="inputElement">The input element.</param>
    public CheckBox(DomContainer domContainer, IHTMLInputElement inputElement) : base(domContainer, inputElement)
    {}
    
    /// <summary>
    /// Initializes a new instance of the <see cref="CheckBox"/> class.
    /// Mainly used by WatiN internally.
    /// </summary>
    /// <param name="domContainer">The domContainer.</param>
    /// <param name="finder">The finder.</param>
    public CheckBox(DomContainer domContainer, ElementFinder finder) : base(domContainer, finder)
    {}

    /// <summary>
    /// Initialises a new instance of the <see cref="CheckBox"/> class based on <paramref name="element"/>.
    /// </summary>
    /// <param name="element">The element.</param>
    public CheckBox(Element element) : base(element, ElementTags)
    {}
  }
}
