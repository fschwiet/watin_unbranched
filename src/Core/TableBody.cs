#region WatiN Copyright (C) 2006-2007 Jeroen van Menen

//Copyright 2006-2007 Jeroen van Menen
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

#endregion Copyright

namespace WatiN.Core
{
  using System;
  using System.Collections;
  using mshtml;
  using WatiN.Core.Interfaces;

  /// <summary>
  /// This class provides specialized functionality for a HTML tbody element. 
  /// </summary>
  public class TableBody : ElementsContainer
  {
    private static ArrayList elementTags;

    public TableBody(DomContainer ie, ElementFinder finder) : base(ie, finder)
    {
    }

    public TableBody(DomContainer ie, IHTMLTableSection element) : base(ie, (IHTMLElement) element)
    {
    }

    public TableBody(Element element) : base(element, elementTags)
    {
    }

    /// <summary>
    /// Returns the table rows belonging to this table body (not including table rows 
    /// from tables nested in this table body).
    /// </summary>
    /// <value>The table rows.</value>
    public override TableRowCollection TableRows
    {
      get
      {
        return new TableRowCollection(DomContainer, UtilityClass.IHtmlElementCollectionToArrayList(HtmlBody.rows));
      }
    }

    /// <summary>
    /// Returns the table row belonging to this table body (not including table rows 
    /// from tables nested in this table body).
    /// </summary>
    /// <param name="findBy">The find by.</param>
    /// <returns></returns>
    public override TableRow TableRow(Attribute findBy)
    {
      return ElementsSupport.TableRow(DomContainer, findBy, new Rows(this));
    }


    public static ArrayList ElementTags
    {
      get
      {
        if (elementTags == null)
        {
          elementTags = new ArrayList();
          elementTags.Add(new ElementTag("tbody"));
        }

        return elementTags;
      }
    }

    private IHTMLTableSection HtmlBody
    {
      get { return (IHTMLTableSection) HTMLElement; }
    }

    public class Rows : IElementCollection
    {
      private TableBody tableBody;

      public Rows(TableBody tableBody)
      {
        this.tableBody = tableBody;
      }

      public IHTMLElementCollection Elements
      {
        get
        {
          return tableBody.HtmlBody.rows;
        }
      }
    }
  }
}
