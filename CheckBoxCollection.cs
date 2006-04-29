using System.Collections;
using mshtml;

namespace WatiN
{
	public class CheckBoxCollection : IEnumerable
	{
		ArrayList elements;
		
		public CheckBoxCollection(DomContainer ie, IHTMLElementCollection elements) 
		{
			this.elements = new ArrayList();
      IHTMLElementCollection inputElements = (IHTMLElementCollection)elements.tags("input");

			foreach (IHTMLInputElement item in inputElements)
			{
        if (item.type == "checkbox")
        {
            CheckBox v = new CheckBox(ie, item);
            this.elements.Add(v);
        }
			}
		}
		public int length { get { return elements.Count; } }

		public CheckBox this[int index] { get { return (CheckBox)elements[index]; } }

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

			public CheckBox Current 
			{
				get 
				{
					return (CheckBox)children[index];
				}
			}

			object IEnumerator.Current { get { return Current; } }
		}
	}
}