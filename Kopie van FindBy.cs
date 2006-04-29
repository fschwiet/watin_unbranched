using System;

namespace WatiN
{
  public class AttributeValue
  {
    private string attributeName;
    private string value;

    public AttributeValue(string attributeName, string value)
    {
      ArgumentNotNullOrEmpty(attributeName, "attributeName");
      ArgumentNotNullOrEmpty(value, "value");

      this.attributeName = attributeName;
      this.value = value;
    }

    public virtual string AttributeName
    {
      get { return attributeName; }
    }

    public virtual string Value
    {
      get { return value; }
    }

    /// <summary>
    /// This methode implements an exact match comparison. If you want
    /// different behaviour, inherit this class or one of its subclasses and 
    /// override Compare with a specific implementation.
    /// </summary>
    /// <param name="value">Value to compare with (not null or string.empty)</param>
    /// <returns>True if the searched for value equals the actual title</returns>
    public virtual bool Compare(string value)
    {
      if (!IsNullOrEmpty(value) && Value.Equals(value))
      {
        return true;
      }

      return false;
    }

    private static void ArgumentNotNullOrEmpty(string requiredObject, string name)
    {
      if (IsNullOrEmpty(requiredObject))
      {
        throw new ArgumentNullException(name, "Null or empty are not allowed");
      }
    }

    protected static bool IsNullOrEmpty(string value)
    {
      return (value == null || value == string.Empty);
    }
  }

  public class IDValue : AttributeValue
  {
    public IDValue(string id) : base("id", id)
    {}
  }

  public class NameValue : AttributeValue
  {
    public NameValue(string name) : base("name", name)
    {}
  }

  public class TextValue : AttributeValue
  {
    public TextValue(string text) : base("text", text)
    {}
  }

  public class UrlValue : AttributeValue
  {
    Uri findUrl = null;

    public UrlValue(string url) : base("href", url)
    {
      findUrl = new Uri(url);
    }

    /// <summary>
    /// This methode implements an exact match comparison based on comparing both Urls
    /// with System.Uri.Equals. If you want different behaviour, inherit this class or 
    /// one of its subclasses and override Compare with a specific implementation.
    /// </summary>
    /// <param name="value">A valid Url to compare with</param>
    /// <returns>True if the searched for Url is equal with the actual Url</returns>
    public override bool Compare(string value)
    {
      Uri ieUrl = new Uri(value);
                
      if (ieUrl.Equals(findUrl))
      {
        return true;
      }

      return false;
    }
  }

  public class TitleValue : AttributeValue
  {
    public TitleValue(string title) : base("title", title)
    {}

    /// <summary>
    /// This override implements a 'contains' instead of een exact match. If you want
    /// an exact match, inherit this class and override Compare with a specific
    /// implementation.
    /// </summary>
    /// <param name="value">Value to compare with (not null or string.empty)</param>
    /// <returns>True if the searched for title is equal with or is contained by the actual title</returns>
    public override bool Compare(string value)
    {
      bool containedInValue = value.ToLower().IndexOf(Value.ToLower()) >= 0;

      if (!IsNullOrEmpty(value) && containedInValue)
      {
        return true;
      }

      return false;
    }
  }

  public class ValueValue : AttributeValue
  {
    public ValueValue(string value) : base("value", value)
    {}
  }

  /// <summary>
  /// This class provides factory methods for de most commonly used attributes
  /// to find an element on a web page.
  /// </summary>
  public class Find
  {
    /// <summary>
    /// Find an element by its id
    /// </summary>
    /// <param name="id">Element id</param>
    /// <returns></returns>
    public static IDValue ByID(string id)
    {
      return new IDValue(id);
    }

    /// <summary>
    /// Find an element by its name
    /// </summary>
    /// <param name="name">Element name</param>
    /// <returns></returns>
    public static NameValue ByName(string name)
    {
      return new NameValue(name);
    }

    /// <summary>
    /// Find an element by its (inner) text
    /// </summary>
    /// <param name="text">Element text</param>
    /// <returns></returns>
    public static TextValue ByText(string text)
    {
      return new TextValue(text);
    }

    /// <summary>
    /// Find an element, frame, IE instance or HTMLDialog by its Url
    /// </summary>
    /// <param name="url">The url to match exactly</param>
    /// <returns></returns>
    public static UrlValue ByUrl(string url)
    {
      return new UrlValue(url);
    }

    /// <summary>
    /// Find an element, frame, IE instance or HTMLDialog by its Title
    /// </summary>
    /// <param name="title">The title to match partially</param>
    /// <returns></returns>
    public static TitleValue ByTitle(string title)
    {
      return new TitleValue(title);
    }

    /// <summary>
    /// Find an element by its value attribute.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static ValueValue ByValue(string value)
    {
      return new ValueValue(value);
    }

    /// <summary>
    /// Find an element by an attribute
    /// </summary>
    /// <param name="attributename">The attribute to compare the value with</param>
    /// <param name="value">The exact matching value of the attribute</param>
    /// <returns></returns>
    public static AttributeValue ByCustom(string attributename, string value)
    {
      return new AttributeValue(attributename, value);
    }
  }
}
