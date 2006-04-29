using System.Collections;
using mshtml;

namespace WatiN
{
	public class TableCollection : IEnumerable
	{
		ArrayList elements;
		
		public TableCollection(DomContainer ie, IHTMLElementCollection elements) 
		{
			this.elements = new ArrayList();
      IHTMLElementCollection tables = (IHTMLElementCollection)elements.tags("table");
			
      foreach (HTMLTable table in tables)
			{
					Table v = new Table(ie, table);
					this.elements.Add(v);
			}
		}

		public int length { get { return elements.Count; } }

		public Table this[int index] { get { return (Table)elements[index]; } }

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

			public Table Current 
			{
				get 
				{
					return (Table)children[index];
				}
			}

			object IEnumerator.Current { get { return Current; } }
		}
	}
}