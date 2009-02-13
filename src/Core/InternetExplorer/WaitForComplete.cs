#region WatiN Copyright (C) 2006-2009 Jeroen van Menen

//Copyright 2006-2009 Jeroen van Menen
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

using System.Runtime.InteropServices;
using mshtml;
using SHDocVw;
using WatiN.Core.UtilityClasses;

namespace WatiN.Core.InternetExplorer
{
    public class WaitForComplete : WaitForCompleteBase
    {
        protected DomContainer _domContainer;

        /// <summary>
        /// Waits until the given <paramref name="domContainer"/> is ready loading the webpage. It will timeout after
        /// <seealso cref="Settings.WaitForCompleteTimeOut"/> seconds.
        /// </summary>
        /// <param name="domContainer">The page to wait for in this domcontainer</param>
        public WaitForComplete(DomContainer domContainer) : this(domContainer, Settings.WaitForCompleteTimeOut) { }

        /// Waits until the given <paramref name="domContainer"/> is ready loading the webpage. It will timeout after
        /// <paramref name="waitForCompleteTimeOut"> seconds.</paramref>
        /// <param name="domContainer">The page to wait for in this domcontainer</param>
        /// <param name="waitForCompleteTimeOut">Time to wait in seconds</param>
        public WaitForComplete(DomContainer domContainer, int waitForCompleteTimeOut) : base(waitForCompleteTimeOut)
        {
            _domContainer = domContainer;
        }

        protected virtual void WaitForFramesToComplete(IHTMLDocument2 maindocument)
        {
            var mainHtmlDocument = (HTMLDocument) maindocument;

            var framesCount = FrameCountProcessor.GetFrameCountFromHTMLDocument(mainHtmlDocument);

            for (var i = 0; i != framesCount; ++i)
            {
                var frame = FrameByIndexProcessor.GetFrameFromHTMLDocument(i, mainHtmlDocument);

                if (frame == null) continue;
			    
                IHTMLDocument2 document;

                try
                {
                    WaitWhileIEBusy(frame);
                    waitWhileIEStateNotComplete(frame);
                    WaitWhileFrameDocumentNotAvailable(frame);

                    document = (IHTMLDocument2) frame.Document;
                }
                finally
                {
                    // free frame
                    Marshal.ReleaseComObject(frame);
                }

                WaitWhileDocumentStateNotComplete(document);
                WaitForFramesToComplete(document);
            }
        }

        protected virtual void WaitWhileDocumentStateNotComplete(IHTMLDocument2 htmlDocument)
        {
            var document = (HTMLDocument) htmlDocument;
            while (document.readyState != "complete")
            {
                ThrowExceptionWhenTimeout("waiting for document state complete. Last state was '" + document.readyState + "'");
                Sleep("WaitWhileDocumentStateNotComplete");
            }
        }

        protected virtual void WaitWhileMainDocumentNotAvailable(DomContainer domContainer)
        {
            while (!IsDocumentReadyStateAvailable(GetDomContainerDocument(domContainer)))
            {
                ThrowExceptionWhenTimeout("waiting for main document becoming available");

                Sleep("WaitWhileMainDocumentNotAvailable");
            }
        }

        protected virtual void WaitWhileFrameDocumentNotAvailable(IWebBrowser2 frame)
        {
            while (!IsDocumentReadyStateAvailable(GetFrameDocument(frame)))
            {
                ThrowExceptionWhenTimeout("waiting for frame document becoming available");

                Sleep("WaitWhileFrameDocumentNotAvailable");
            }
        }

        protected virtual IHTMLDocument2 GetFrameDocument(IWebBrowser2 frame)
        {
            try
            {
                return frame.Document as IHTMLDocument2;
            }
            catch
            {
                return null;
            }
        }

        protected virtual IHTMLDocument2 GetDomContainerDocument(DomContainer domContainer)
        {
            try
            {
                return (IHTMLDocument2) domContainer.NativeDocument.Object;
            }
            catch
            {
                return null;
            }
        }

        protected virtual bool IsDocumentReadyStateAvailable(IHTMLDocument2 document)
        {
            if (document == null) return false;

            // Sometimes an OutOfMemoryException or ComException occurs while accessing
            // the readystate property of IHTMLDocument2. Giving MSHTML some time
            // to do further processing seems to solve this problem.
            return UtilityClass.TryFuncIgnoreException(() =>
                                                           {
                                                               var readyState = document.readyState;
                                                               return true;
                                                           });
        }

        protected virtual void waitWhileIEStateNotComplete(IWebBrowser2 ie)
        {
            while (IsIEReadyStateComplete(ie))
            {
                ThrowExceptionWhenTimeout("Internet Explorer state not complete");

                Sleep("waitWhileIEStateNotComplete");
            }
        }

        protected virtual bool IsIEReadyStateComplete(IWebBrowser2 ie)
        {
            try
            {
                return ie.ReadyState != tagREADYSTATE.READYSTATE_COMPLETE;
            }
            catch
            {
                return false;
            }
        }

        protected virtual void WaitWhileIEBusy(IWebBrowser2 ie)
        {
            while (IsIEBusy(ie))
            {
                ThrowExceptionWhenTimeout("Internet Explorer busy");

                Sleep("WaitWhileIEBusy 2");
            }
        }

        protected virtual bool IsIEBusy(IWebBrowser2 ie)
        {
            try
            {
                return ie.Busy;
            }
            catch
            {
                return false;
            }
        }

        protected override void WaitForCompleteOrTimeout()
        {
            WaitWhileMainDocumentNotAvailable(_domContainer);
            WaitWhileDocumentStateNotComplete((IHTMLDocument2)_domContainer.NativeDocument.Object);
            WaitForFramesToComplete((IHTMLDocument2)_domContainer.NativeDocument.Object);
        }
    }
}