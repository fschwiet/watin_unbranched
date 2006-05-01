using System.Collections;
using mshtml;

namespace WatiN
{
	public class LabelCollection : IEnumerable
	{
		ArrayList elements;
		
		public LabelCollection(DomContainer ie, IHTMLElementCollection elements) 
		{
			this.elements = new ArrayList();
      IHTMLElementCollection labels = (IHTMLElementCollection)elements.tags("label");
			
      foreach (HTMLLabelElement label in labels)
			{
				Label v = new Label(ie, label);
				this.elements.Add(v);
			}
		}

		public int length { get { return elements.Count; } }

		public Label this[int index] { get { return (Label)elements[index]; } }

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

			public Label Current 
			{
				get 
				{
					return (Label)children[index];
				}
			}

			object IEnumerator.Current { get { return Current; } }
		}
	}
}