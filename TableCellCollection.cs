using System.Collections;
using mshtml;

namespace WatiN
{
	public class TableCellCollection : IEnumerable
	{
		ArrayList elements;
		
		public TableCellCollection(DomContainer ie, IHTMLElementCollection elements) 
		{
			this.elements = new ArrayList();
      IHTMLElementCollection tableCells = (IHTMLElementCollection)elements.tags("TD");

			foreach(HTMLTableCell tableCell in tableCells)
			{
			  TableCell v = new TableCell(ie, tableCell);
				this.elements.Add(v);
			}
		}

		public int length { get { return elements.Count; } }

		public TableCell this[int index] { get { return (TableCell)elements[index]; } }

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

			public TableCell Current 
			{
				get 
				{
					return (TableCell)children[index];
				}
			}

			object IEnumerator.Current { get { return Current; } }
		}
	}
}