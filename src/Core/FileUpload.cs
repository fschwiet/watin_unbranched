#region WatiN Copyright (C) 2006-2007 Jeroen van Menen

//Copyright 2006-2007 Jeroen van Menen
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
using System.IO;
using mshtml;
using WatiN.Core;
using WatiN.Core.DialogHandlers;

namespace WatiN.Core
{
  public class FileUpload : Element
  {
    private static ArrayList elementTags;

    public static ArrayList ElementTags
    {
      get
      {
        if (elementTags == null)
        {
          elementTags = new ArrayList();
          elementTags.Add(new ElementTag("input", "file"));
        }

        return elementTags;
      }
    }

    public FileUpload(DomContainer domContainer, IHTMLInputFileElement inputFileElement): base(domContainer, inputFileElement)
    {}
    
    public FileUpload(DomContainer domContainer, ElementFinder finder): base(domContainer, finder)
    {}

    /// <summary>
    /// Initialises a new instance of the <see cref="FileUpload"/> class based on <paramref name="element"/>.
    /// </summary>
    /// <param name="element">The element.</param>
    public FileUpload(Element element) : base(element, ElementTags)
    {}

    public string FileName
    {
      get
      {
        return IHTMLInputFileElement.value;
      }
    }

    public void Set(string fileName)
    { 
      
      FileInfo info = new FileInfo(fileName);
      if (!info.Exists)
      {
        throw new FileNotFoundException("File does not exist", fileName);
      }

      FileUploadDialogHandler uploadDialogHandler = new FileUploadDialogHandler(fileName);
      DomContainer.AddDialogHandler(uploadDialogHandler);

      try
      {
        Click();
      }
      finally
      {
        DomContainer.RemoveDialogHandler(uploadDialogHandler);
      }      
    }

    private IHTMLInputFileElement IHTMLInputFileElement
    {
      get { return ((IHTMLInputFileElement) HTMLElement); }
    }
  }
}
