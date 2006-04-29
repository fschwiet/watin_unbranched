using System.Collections;

using SHDocVw;

namespace WatiN
{
  public class IECollection : IEnumerable
  {
    ArrayList internetExplorers;
		
    public IECollection() 
    {
      this.internetExplorers = new ArrayList();

      ShellWindows allBrowsers = new ShellWindows();

      foreach(InternetExplorer internetExplorer in allBrowsers)
      {
        try
        {
          IE ie = new IE(internetExplorer);
          this.internetExplorers.Add(ie);
        }
        catch
        {}
      }
    }

    public int length 
    { 
      get
      {
        return internetExplorers.Count;
      } 
    }

    public IE this[int index]
    {
      get
      {
        return (IE)internetExplorers[index];
      }
    }

    public Enumerator GetEnumerator() 
    {
      return new Enumerator(internetExplorers);
    }

    IEnumerator IEnumerable.GetEnumerator() 
    {
      return GetEnumerator();
    }

    public class Enumerator: IEnumerator 
    {
      ArrayList children;
      int index;
      public Enumerator(ArrayList children) 
      {
        this.children = children;
        Reset();
      }

      public void Reset() 
      {
        index = -1;
      }

      public bool MoveNext() 
      {
        ++index;
        return index < children.Count;
      }

      public IE Current 
      {
        get 
        {
          return (IE)children[index];
        }
      }

      object IEnumerator.Current
      {
        get
        {
          return Current;
        }
      }
    }
  }
}