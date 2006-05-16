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

using WatiN.Core.Exceptions;
using WatiN.Core.Interfaces;
using WatiN.Core.Logging;

namespace WatiN.Core
{
  public class TextField : Element
  {

    private ITextElement textElement = null;

    public TextField(DomContainer ie, HTMLInputElement htmlInputElement) : base(ie, htmlInputElement)
    {
      textElement = new TextFieldElement(htmlInputElement);
    }

    public TextField(DomContainer ie, HTMLTextAreaElement htmlTextAreaElement) : base(ie, htmlTextAreaElement)
    {
      textElement = new TextAreaElement(htmlTextAreaElement);
    }

    public int MaxLength
    {
      get { return textElement.MaxLength; }
    }

    public bool ReadOnly
    {
      get { return textElement.ReadOnly; }
    }

    public void TypeText(string value)
    {
      Logger.LogAction("Typing '" + value + "' into " + GetType().Name + " '" + ToString() + "'");

      TypeAppendClearText(value, false, false);
    }

    public void AppendText(string value)
    {
      Logger.LogAction("Appending '" + value + "' to " + GetType().Name + " '" + ToString() + "'");

      TypeAppendClearText(value, true, false);
    }

    public void Clear()
    {
      Logger.LogAction("Clearing " + GetType().Name + " '" + ToString() + "'");

      TypeAppendClearText(null, false, true);
    }

    private void TypeAppendClearText(string value, bool append, bool clear)
    {
      if (!Enabled) { throw new ElementDisabledException(ToString()); }
      if (ReadOnly) { throw new ElementReadOnlyException(ToString()); }
      
      HighLight(true);
      Focus();
      if (!append) Select();
      if (!append) setValue("");
      if (!append) KeyPress();
      if (!clear) doKeyPress(value);
      HighLight(false);
      if (!append) Change();
      try
      {
        if (!append) Blur();
      }
      catch {}
    }

    public string Value
    {
      get { return textElement.Value; }
      // Don't use this set property internally (in this class) but use setValue. 
      set
      {
        Logger.LogAction("Setting " + GetType().Name + " '" + ToString() + "' to '" + value + "'");

        setValue(value);
      }
    }

    /// <summary>
    /// Returns the same as the Value property
    /// </summary>
    public override string Text
    {
      get
      {
        return Value;
      }
    }

    public void Select()
    {
      textElement.Select();
      FireEvent("onSelect");
    }

    public override string ToString()
    {
      if (!IsNullOrEmpty(Title))
      {
        return Title;
      }
      if (!IsNullOrEmpty(Id))
      {
        return Id;
      }
      if (!IsNullOrEmpty(Name))
      {
        return Name;
      }
      return base.ToString ();
    }

    public string Name
    {
      get
      {
        return textElement.Name;
      }
    }

    private void setValue(string value)
    {
      textElement.SetValue(value);
    }

    private void doKeyPress(string value)
    {
      bool doKeydown = findEvent("onkeydown");
      bool doKeyPress = findEvent("onkeypress");
      bool doKeyUp = findEvent("onkeyup");

      for (int i = 0; i < value.Length; i++)
      {
        //TODO: Make typing speed a variable
        //        Thread.Sleep(0); 

        setValue(Value + value.Substring(i, 1));

        if (doKeydown) { KeyDown(); }
        if (doKeyPress) { KeyPress(); }
        if (doKeyUp) { KeyUp(); }
      }
    }

    private bool findEvent(string eventName)
    {
      return (textElement.OuterHtml.IndexOf(eventName) > 0);
    }

    /// <summary>
    /// Summary description for TextFieldElement.
    /// </summary>
    private class TextAreaElement : ITextElement
    {
      private HTMLTextAreaElement htmlTextAreaElement = null;
      public TextAreaElement(HTMLTextAreaElement htmlTextAreaElement)
      {
        this.htmlTextAreaElement = htmlTextAreaElement;
      }

      public int MaxLength
      {
        get { return 0; }
      }

      public bool ReadOnly
      {
        get { return htmlTextAreaElement.readOnly; }
      }

      public string Value
      {
        get { return htmlTextAreaElement.value; } 
      }

      public void Select()
      {
        htmlTextAreaElement.select();
      }

      public void SetValue(string value)
      {
        htmlTextAreaElement.value = value;
      }

      public string Name
      {
        get { return htmlTextAreaElement.name; }
      }

      public string OuterHtml
      {
        get { return htmlTextAreaElement.outerHTML; }
      }
    }

    private class TextFieldElement : ITextElement
    {
      private HTMLInputElement inputElement = null;

      public TextFieldElement(HTMLInputElement htmlInputElement)
      {
        inputElement = htmlInputElement;
      }

      public int MaxLength
      {
        get { return inputElement.maxLength; }
      }

      public bool ReadOnly
      {
        get { return inputElement.readOnly; }
      }

      public string Value
      {
        get { return inputElement.value; } // Don't use this set property internally (in this class) but use setValue. 
      }

      public void Select()
      {
        inputElement.select();
      }

      public void SetValue(string value)
      {
        inputElement.value = value;
      }

      public string Name
      {
        get { return inputElement.name; }
      }

      public string OuterHtml
      {
        get { return inputElement.outerHTML; }
      }
    }
  }
}
