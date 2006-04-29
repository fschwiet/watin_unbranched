using System.Collections;
using mshtml;

namespace WatiN
{
	public class TextFieldCollection : IEnumerable
	{
		ArrayList children;
		
    public TextFieldCollection(DomContainer ie, IHTMLElementCollection elements) 
    {
      this.children = new ArrayList();
      IHTMLElementCollection inputElements = (IHTMLElementCollection)elements.tags("input");

      foreach (IHTMLInputElement inputElement in inputElements)
      {
        if ("text password textarea hidden".IndexOf(inputElement.type) >= 0)
        {
          TextField v = new TextField(ie, (HTMLInputElement)inputElement);
          this.children.Add(v);
        }
      }

      IHTMLElementCollection textElements = (IHTMLElementCollection)elements.tags("textarea");

      foreach (IHTMLElement textElement in textElements)
      {
        TextField v = new TextField(ie, (HTMLInputElement)textElement);
        this.children.Add(v);
      }
    }

		public int length { get { return children.Count; } }

		public TextField this[int index] { get { return (TextField)children[index]; } }

		public Enumerator GetEnumerator() 
		{
			return new Enumerator(children);
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

			public TextField Current 
			{
				get 
				{
					return (TextField)children[index];
				}
			}

			object IEnumerator.Current { get { return Current; } }
		}
	}
}