#region WatiN Copyright (C) 2006-2009 Jeroen van Menen

//Copyright 2006-2008 Jeroen van Menen
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

#endregion Copyright

using System.Collections;
using mshtml;
using SHDocVw;
using WatiN.Core.Interfaces;
using WatiN.Core.InternetExplorer;

namespace WatiN.Core
{
	internal class AllFramesProcessor : IWebBrowser2Processor
	{
		public ArrayList elements;

		private readonly HTMLDocument htmlDocument;
		private readonly IHTMLElementCollection frameElements;
		private int index;
		private readonly DomContainer _domContainer;

		public AllFramesProcessor(DomContainer domContainer, HTMLDocument htmlDocument)
		{
			elements = new ArrayList();

			frameElements = (IHTMLElementCollection) htmlDocument.all.tags(ElementsSupport.FrameTagName);

			// If the current document doesn't contain FRAME elements, it then
			// might contain IFRAME elements.
			if (frameElements.length == 0)
			{
                frameElements = (IHTMLElementCollection)htmlDocument.all.tags(ElementsSupport.IFrameTagName);
			}

			_domContainer = domContainer;
			this.htmlDocument = htmlDocument;
		}

		public HTMLDocument HTMLDocument()
		{
			return htmlDocument;
		}

		public void Process(IWebBrowser2 webBrowser2)
		{
			// Get the frame element from the parent document
			var frameElement = (IHTMLElement) frameElements.item(index, null);
			var frameElementUniqueId = ((DispHTMLBaseElement) frameElement).uniqueID;

			var frame = new Frame(_domContainer, new IEDocument(webBrowser2.Document), (IHTMLDocument3) htmlDocument, frameElementUniqueId);
			
			elements.Add(frame);

			index++;
		}

		public bool Continue()
		{
			return true;
		}
	}
}