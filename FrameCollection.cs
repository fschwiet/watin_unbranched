using System.Collections;
using mshtml;

namespace WatiN
{
	public class FrameCollection : IEnumerable
	{
		ArrayList elements;
		
		public FrameCollection(DomContainer ie, IHTMLDocument2 htmlDocument) 
		{
			this.elements = new ArrayList();
      IHTMLElementCollection frameElements = (IHTMLElementCollection)htmlDocument.all.tags("FRAME");

      for (int index = 0; index < htmlDocument.frames.length; index++)
      {
        // Get the frame
        DispHTMLWindow2 thisFrame = Frame.GetFrameFromHTMLDocument(index, htmlDocument);
        
        // Get the frame element from the parent document
        IHTMLElement frameElement = (IHTMLElement)frameElements.item(index, null);

        // Create new Frame instance
        Frame frame = new Frame(ie, thisFrame.document, thisFrame.name, frameElement.id);

        this.elements.Add(frame);
			}
		}

		public int length { get { return elements.Count; } }

		public Frame this[int index] { get { return (Frame)elements[index]; } }

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

			public Frame Current 
			{
				get 
				{
					return (Frame)children[index];
				}
			}

			object IEnumerator.Current { get { return Current; } }
		}
	}
}