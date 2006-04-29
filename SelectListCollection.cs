using System.Collections;
using mshtml;

namespace WatiN
{
  public class SelectListCollection : IEnumerable
  {
    ArrayList elements;
		
    public SelectListCollection(DomContainer ie, IHTMLElementCollection elements) 
    {
      this.elements = new ArrayList();
      IHTMLElementCollection selectlists = (IHTMLElementCollection)elements.tags("select");

      foreach (IHTMLElement selectlist in selectlists)
      {
        SelectList v = new SelectList(ie, selectlist);
        this.elements.Add(v);
      }
    }

    public int length { get { return elements.Count; } }

    public SelectList this[int index] { get { return (SelectList)elements[index]; } }

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

      public SelectList Current 
      {
        get 
        {
          return (SelectList)children[index];
        }
      }

      object IEnumerator.Current { get { return Current; } }
    }
  }
}