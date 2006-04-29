using System.Collections;
using mshtml;

namespace WatiN
{
	public class ButtonCollection : IEnumerable
	{
		ArrayList elements;
		
		public ButtonCollection(DomContainer ie, IHTMLElementCollection elements) 
		{
			this.elements = new ArrayList();
      IHTMLElementCollection inputElements = (IHTMLElementCollection)elements.tags("input");
			
      foreach (IHTMLInputElement inputElement in inputElements)
			{
        if ("button submit image reset".IndexOf(inputElement.type) >= 0)
        {
            Button v = new Button(ie, (HTMLInputElement)inputElement);
            this.elements.Add(v);
        }
			}
		}

		public int length { get { return elements.Count; } }

		public Button this[int index] { get { return (Button)elements[index]; } }

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

			public Button Current 
			{
				get 
				{
					return (Button)children[index];
				}
			}

			object IEnumerator.Current { get { return Current; } }
		}
	}
}