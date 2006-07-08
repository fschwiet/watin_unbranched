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
  /// This class provides specialized functionality for HTML input elements of type 
  /// checkbox.
  /// </summary>
  public class CheckBox : RadioCheck
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="CheckBox"/> class.
    /// Mainly used by WatiN internally.
    /// </summary>
    /// <param name="domContainer">The domContainer.</param>
    /// <param name="inputElement">The input element.</param>
    public CheckBox(DomContainer domContainer, IHTMLInputElement inputElement) : base(domContainer, inputElement)
    {}
  }
}