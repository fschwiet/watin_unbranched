using System.Collections;
using mshtml;

namespace WatiN
{
	public class TableRowCollection : IEnumerable
	{
		ArrayList elements;
		
		public TableRowCollection(DomContainer ie, IHTMLElementCollection elements) 
		{
			this.elements = new ArrayList();
		  IHTMLElementCollection tableRows = (IHTMLElementCollection)elements.tags("TR");
		  
      foreach (HTMLTableRow tableRow in tableRows)
			{
        TableRow v = new TableRow(ie, tableRow);
        this.elements.Add(v);
			}
		}

		public int length { get { return elements.Count; } }

		public TableRow this[int index] { get { return (TableRow)elements[index]; } }

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

			public TableRow Current 
			{
				get 
				{
					return (TableRow)children[index];
				}
			}

			object IEnumerator.Current { get { return Current; } }
		}
	}
}