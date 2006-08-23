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

using WatiN.Core;

namespace WatiN.Core.Interfaces
{
  /// <summary>
  /// This interface is used by <see cref="TextField"/> to support both
  /// HTML input element of type text password textarea hidden and 
  /// for a HTML textarea element.
  /// </summary>
  internal interface ITextElement
  {
    int MaxLength
    {
      get;
    }

    bool ReadOnly
    {
      get;
    }

    string Value
    {
      get;
    }

    void Select();
    
    void SetValue(string value);
    
    string ToString();

    string Name
    {
      get;
    }

    string OuterHtml
    {
      get;
    }
  }
}