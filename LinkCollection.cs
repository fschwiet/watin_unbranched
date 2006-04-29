using System.Collections;
using mshtml;

namespace WatiN
{
  public class LinkCollection : IEnumerable
  {
    ArrayList elements;
		
    public LinkCollection(DomContainer ie, IHTMLElementCollection elements) 
    {
      this.elements = new ArrayList();
      IHTMLElementCollection links = (IHTMLElementCollection)elements.tags("a");

      foreach (HTMLAnchorElement link in links)
      {
        Link v = new Link(ie, link);
        this.elements.Add(v);
      }
    }

    public int length { get { return elements.Count; } }

    public Link this[int index] { get { return (Link)elements[index]; } }

    public Enumerator GetEnumerator() 
    {
      return new Enumerator(elements);
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

      public Link Current 
      {
        get 
        {
          return (Link)children[index];
        }
      }

      object IEnumerator.Current { get { return Current; } }
    }
  }
}