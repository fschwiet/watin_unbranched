using System.Collections;
using mshtml;

namespace WatiN
{
	public class ParaCollection : IEnumerable
	{
		ArrayList elements;
		
		public ParaCollection(DomContainer ie, IHTMLElementCollection elements) 
		{
			this.elements = new ArrayList();
      IHTMLElementCollection Paras = (IHTMLElementCollection)elements.tags("p");
			
      foreach (HTMLParaElement Para in Paras)
			{
				Para v = new Para(ie, Para);
				this.elements.Add(v);
			}
		}

		public int length { get { return elements.Count; } }

		public Para this[int index] { get { return (Para)elements[index]; } }

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

			public Para Current 
			{
				get 
				{
					return (Para)children[index];
				}
			}

			object IEnumerator.Current { get { return Current; } }
		}
	}
}