//using System;
//using System.Collections;
//using mshtml;
//
//namespace WatiN
//{
//	public class TableSectionCollection : IEnumerable
//	{
//		ArrayList elements;
//		
//		public TableSectionCollection(IDomContainer ie, IHTMLElementCollection elements) 
//		{
//			this.elements = new ArrayList();
//			for(int i = 0; i != elements.length; ++i)
//			{
//				try
//				{
//					TableSection v = new TableSection(ie, (HTMLTableSection)elements.item(i, null));
//					this.elements.Add(v);
//				}
//				catch(Exception)
//				{
//					// When an exception is caught, type casting failed so we don't add this element
//					// to our list.
//				}
//			}
//		}
//
//		public int length { get { return elements.Count; } }
//
//		public TableSection this[int index] { get { return (TableSection)elements[index]; } }
//
//		public Enumerator GetEnumerator() 
//		{
//			return new Enumerator(elements);
//		}
//
//		IEnumerator IEnumerable.GetEnumerator() 
//		{
//			return GetEnumerator();
//		}
//
//		public class Enumerator: IEnumerator 
//		{
//			ArrayList children;
//			int index;
//			public Enumerator(ArrayList children) 
//			{
//				this.children = children;
//				Reset();
//			}
//
//			public void Reset() 
//			{
//				index = -1;
//			}
//
//			public bool MoveNext() 
//			{
//				++index;
//				return index < children.Count;
//			}
//
//			public TableSection Current 
//			{
//				get 
//				{
//					return (TableSection)children[index];
//				}
//			}
//
//			object IEnumerator.Current { get { return Current; } }
//		}
//	}
//}