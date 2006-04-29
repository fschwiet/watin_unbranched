using System.Collections;
using mshtml;

namespace WatiN
{
	public class FormCollection : IEnumerable
	{
		ArrayList elements;
		
		public FormCollection(DomContainer ie, IHTMLElementCollection elements) 
		{
			this.elements = new ArrayList();
      IHTMLElementCollection forms = (IHTMLElementCollection)elements.tags("form");

			foreach (HTMLFormElement form in forms)
			{
				Form v = new Form(ie, form);
				this.elements.Add(v);
			}
		}

		public int length { get { return elements.Count; } }

		public Form this[int index] { get { return (Form)elements[index]; } }

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

			public Form Current 
			{
				get 
				{
					return (Form)children[index];
				}
			}

			object IEnumerator.Current { get { return Current; } }
		}
	}
}