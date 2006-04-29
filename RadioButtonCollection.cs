using System.Collections;
using mshtml;

namespace WatiN
{
  public class RadioButtonCollection : IEnumerable
  {
    ArrayList elements;
		
    public RadioButtonCollection(DomContainer ie, IHTMLElementCollection elements) 
    {
      this.elements = new ArrayList();
      IHTMLElementCollection inputElements = (IHTMLElementCollection)elements.tags("input");

      foreach (IHTMLInputElement item in inputElements)
      {
        if (item.type == "radio")
        {
          RadioButton v = new RadioButton(ie, item);
          this.elements.Add(v);
        }
      }
    }
    public int length { get { return elements.Count; } }

    public RadioButton this[int index] { get { return (RadioButton)elements[index]; } }

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

      public RadioButton Current 
      {
        get 
        {
          return (RadioButton)children[index];
        }
      }

      object IEnumerator.Current { get { return Current; } }
    }
  }
}