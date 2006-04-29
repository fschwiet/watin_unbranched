using System.Collections;
using mshtml;

namespace WatiN
{
	public class DivCollection : IEnumerable
	{
		ArrayList elements;
		
		public DivCollection(DomContainer ie, IHTMLElementCollection elements) 
		{
			this.elements = new ArrayList();
      IHTMLElementCollection divs = (IHTMLElementCollection)elements.tags("div");

			foreach (HTMLDivElement div in divs)
			{
				Div v = new Div(ie, div);
				this.elements.Add(v);
			}
		}

		public int length { get { return elements.Count; } }

		public Div this[int index] { get { return (Div)elements[index]; } }

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

			public Div Current 
			{
				get 
				{
					return (Div)children[index];
				}
			}

			object IEnumerator.Current { get { return Current; } }
		}
	}
}