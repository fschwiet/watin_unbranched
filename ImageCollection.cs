using System.Collections;
using mshtml;

namespace WatiN
{
	public class ImageCollection : IEnumerable
	{
		ArrayList elements;
		
		public ImageCollection(DomContainer ie, IHTMLElementCollection elements) 
		{
			this.elements = new ArrayList();
      IHTMLElementCollection inputElements = (IHTMLElementCollection)elements.tags("img");
			
      foreach (IHTMLInputElement inputElement in inputElements)
      {
        Image v = new Image(ie, (IHTMLImgElement)inputElement);
			  this.elements.Add(v);
			}
		}

		public int length { get { return elements.Count; } }

		public Image this[int index] { get { return (Image)elements[index]; } }

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

			public Image Current 
			{
				get 
				{
					return (Image)children[index];
				}
			}

			object IEnumerator.Current { get { return Current; } }
		}
	}
}