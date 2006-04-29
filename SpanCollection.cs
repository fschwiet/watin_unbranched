using System.Collections;
using mshtml;

namespace WatiN
{
	public class SpanCollection : IEnumerable
	{
		ArrayList elements;
		
		public SpanCollection(DomContainer ie, IHTMLElementCollection elements) 
		{
			this.elements = new ArrayList();
      IHTMLElementCollection spans = (IHTMLElementCollection)elements.tags("span");
			
      foreach (HTMLSpanElement span in spans)
			{
				Span v = new Span(ie, span);
				this.elements.Add(v);
			}
		}

		public int length { get { return elements.Count; } }

		public Span this[int index] { get { return (Span)elements[index]; } }

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

			public Span Current 
			{
				get 
				{
					return (Span)children[index];
				}
			}

			object IEnumerator.Current { get { return Current; } }
		}
	}
}