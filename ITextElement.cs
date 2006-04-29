namespace WatiN.Interfaces
{
  public interface ITextElement
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

    string OuterHTML
    {
      get;
    }
  }
}