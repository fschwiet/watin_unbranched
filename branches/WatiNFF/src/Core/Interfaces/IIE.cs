using System;
using mshtml;
using WatiN.Core.DialogHandlers;
using WatiN.Core.Interfaces;
using System.Text.RegularExpressions;
using WatiN.Core.Exceptions;

namespace WatiN.Core
{
    public interface IIE
    {
        /// <summary>
        /// Navigates Internet Explorer to the given <paramref name="url" />.
        /// </summary>
        /// <param name="url">The URL specified as a wel formed Uri.</param>
        /// <example>
        /// The following example creates an Uri and Internet Explorer instance and navigates to
        /// the WatiN Project website on SourceForge.
        /// <code>
        /// using WatiN.Core;
        /// using System;
        /// 
        /// namespace NewIEExample
        /// {
        ///    public class WatiNWebsite
        ///    {
        ///      public WatiNWebsite()
        ///      {
        ///        Uri URL = new Uri("http://watin.sourceforge.net");
        ///        IE ie = new IE();
        ///        ie.GoTo(URL);
        ///      }
        ///    }
        ///  }
        /// </code>
        /// </example>
        void GoTo(Uri url);

        /// <summary>
        /// Navigates Internet Explorer to the given <paramref name="url" />.
        /// </summary>
        /// <param name="url">The URL to GoTo.</param>
        /// <example>
        /// The following example creates a new Internet Explorer instance and navigates to
        /// the WatiN Project website on SourceForge.
        /// <code>
        /// using WatiN.Core;
        /// 
        /// namespace NewIEExample
        /// {
        ///    public class WatiNWebsite
        ///    {
        ///      public WatiNWebsite()
        ///      {
        ///        IE ie = new IE();
        ///        ie.GoTo("http://watin.sourceforge.net");
        ///      }
        ///    }
        ///  }
        /// </code>
        /// </example>
        void GoTo(string url);

        /// <summary>
        /// Use this method to gain access to the full Internet Explorer object.
        /// Do this by referencing the Interop.SHDocVw assembly (supplied in the WatiN distribution)
        /// and cast the return value of this method to type SHDocVw.InternetExplorer.
        /// </summary>
        object InternetExplorer { get; }

        /// <summary>
        /// Navigates the browser back to the previously displayed Url (like the back
        /// button in Internet Explorer). 
        /// </summary>
        void Back();

        /// <summary>
        /// Navigates the browser forward to the next displayed Url (like the forward
        /// button in Internet Explorer). 
        /// </summary>
        void Forward();

        /// <summary>
        /// Reloads the currently displayed webpage (like the Refresh button in 
        /// Internet Explorer).
        /// </summary>
        void Refresh();

        /// <summary>
        /// Sends a Tab key to the IE window to simulate tabbing through
        /// the elements (and adres bar).
        /// </summary>
        void PressTab();

        /// <summary>
        /// Brings the referenced Internet Explorer to the front (makes it the top window)
        /// </summary>
        void BringToFront();

        /// <summary>
        /// Make the referenced Internet Explorer full screen, minimized, maximized and more.
        /// </summary>
        /// <param name="showStyle">The style to apply.</param>
        void ShowWindow(NativeMethods.WindowShowStyle showStyle);

        /// <summary>
        /// Gets the window style.
        /// </summary>
        /// <returns>The style currently applied to the ie window.</returns>
        NativeMethods.WindowShowStyle GetWindowStyle();

        /// <summary>
        /// Closes the referenced Internet Explorer. Almost
        /// all other functionality in this class and the element classes will give
        /// exceptions when used after closing the browser.
        /// </summary>
        /// <example>
        /// The following example creates a new Internet Explorer instances and navigates to
        /// the WatiN Project website on SourceForge. 
        /// <code>
        /// using WatiN.Core;
        /// using System.Diagnostics;
        /// 
        /// namespace NewIEExample
        /// {
        ///    public class WatiNWebsite
        ///    {
        ///      public WatiNWebsite()
        ///      {
        ///        IE ie = new IE("http://watin.sourceforge.net");
        ///        Debug.WriteLine(ie.Html);
        ///        ie.Close;
        ///      }
        ///    }
        ///  }
        /// </code>
        /// </example>
        void Close();

        /// <summary>
        /// Closes then reopens Internet Explorer with a blank page.
        /// </summary>
        /// <remarks>
        /// You could also use one of the overloaded methods.
        /// </remarks>
        /// <example>
        /// The following example creates a new Internet Explorer instances and navigates to
        /// the WatiN Project website on SourceForge leaving the created Internet Explorer open.
        /// <code>
        /// using WatiN.Core;
        /// 
        /// namespace NewIEExample
        /// {
        ///    public class WatiNWebsite
        ///    {
        ///      public WatiNWebsite()
        ///      {
        ///        LogonDialogHandler logon = new LogonDialogHandler("username", "password");
        ///        IE ie = new IE(new Uri("http://watin.sourceforge.net"), logon);
        ///        ie.Reopen();
        ///      }
        ///    }
        ///  }
        /// </code>
        /// </example>
        void Reopen();

        /// <summary>
        /// Closes then reopens Internet Explorer and navigates to the given <paramref name="uri"/>.
        /// </summary>
        /// <param name="uri">The Uri to open</param>
        /// <param name="logonDialogHandler">A <see cref="LogonDialogHandler"/> class instanciated with the logon credentials.</param>
        /// <param name="createInNewProcess">if set to <c>true</c> the IE instance is created in a new process.</param>
        /// <remarks>
        /// You could also use one of the overloaded methods.
        /// </remarks>
        /// <example>
        /// The following example creates a new Internet Explorer instances and navigates to
        /// the WatiN Project website on SourceForge leaving the created Internet Explorer open.
        /// <code>
        /// using WatiN.Core;
        /// 
        /// namespace NewIEExample
        /// {
        ///    public class WatiNWebsite
        ///    {
        ///      public WatiNWebsite()
        ///      {
        ///        LogonDialogHandler logon = new LogonDialogHandler("username", "password");
        ///        IE ie = new IE(new Uri("http://watin.sourceforge.net"), logon);
        ///        ie.Reopen();
        ///      }
        ///    }
        ///  }
        /// </code>
        /// </example>
        void Reopen(Uri uri, LogonDialogHandler logonDialogHandler, bool createInNewProcess);

        /// <summary>
        /// Closes <i>all</i> running instances of Internet Explorer by killing the
        /// process these instances run in. 
        /// </summary>
        void ForceClose();

        /// <summary>
        /// Clears all browser cookies.
        /// </summary>
        /// <remarks>
        /// Internet Explorer maintains an internal cookie cache that does not immediately
        /// expire when cookies are cleared.  This is the case even when the cookies are
        /// cleared using the Internet Options dialog.  If cookies have been used by
        /// the current browser session it may be necessary to <see cref="Reopen()" /> the
        /// browser to ensure the internal cookie cache is flushed.  Therefore it is
        /// recommended to clear cookies at the beginning of the test before navigating
        /// to any pages (other than "about:blank") to avoid having to reopen the browser.
        /// </remarks>
        /// <example>
        /// <code>
        /// // Clear cookies first.
        /// IE ie = new IE();
        /// ie.ClearCookies();
        /// 
        /// // Then go to the site and sign in.
        /// ie.GoTo("http://www.example.com/");
        /// ie.Link(Find.ByText("Sign In")).Click();
        /// </code>
        /// </example>
        /// <seealso cref="Reopen()"/>
        void ClearCookies();

        /// <summary>
        /// Clears the browser cookies associated with a particular site and to
        /// any of the site's subdomains.
        /// </summary>
        /// <remarks>
        /// Internet Explorer maintains an internal cookie cache that does not immediately
        /// expire when cookies are cleared.  This is the case even when the cookies are
        /// cleared using the Internet Options dialog.  If cookies have been used by
        /// the current browser session it may be necessary to <see cref="Reopen()" /> the
        /// browser to ensure the internal cookie cache is flushed.  Therefore it is
        /// recommended to clear cookies at the beginning of the test before navigating
        /// to any pages (other than "about:blank") to avoid having to reopen the browser.
        /// </remarks>
        /// <param name="url">The site url associated with the cookie.</param>
        /// <example>
        /// <code>
        /// // Clear cookies first.
        /// IE ie = new IE();
        /// ie.ClearCookies("http://www.example.com/");
        /// 
        /// // Then go to the site and sign in.
        /// ie.GoTo("http://www.example.com/");
        /// ie.Link(Find.ByText("Sign In")).Click();
        /// </code>
        /// </example>
        /// <seealso cref="Reopen()"/>
        void ClearCookies(string url);

        /// <summary>
        /// Clears the browser cache but leaves cookies alone.
        /// </summary>
        /// <example>
        /// <code>
        /// // Clear the cache and cookies.
        /// IE ie = new IE();
        /// ie.ClearCache();
        /// ie.ClearCookies();
        /// 
        /// // Then go to the site and sign in.
        /// ie.GoTo("http://www.example.com/");
        /// ie.Link(Find.ByText("Sign In")).Click();
        /// </code>
        /// </example>
        /// <seealso cref="Reopen()"/>
        void ClearCache();

        /// <summary>
        /// Gets the value of a cookie.
        /// </summary>
        /// <remarks>
        /// This method cannot retrieve the value of cookies protected by the <c>httponly</c> security option.
        /// </remarks>
        /// <param name="url">The site url associated with the cookie.</param>
        /// <param name="cookieName">The cookie name.</param>
        /// <returns>The cookie data of the form:
        /// &lt;name&gt;=&lt;value&gt;[; &lt;name&gt;=&lt;value&gt;]...
        /// [; expires=&lt;date:DAY, DD-MMM-YYYY HH:MM:SS GMT&gt;][; domain=&lt;domain_name&gt;]
        /// [; path=&lt;some_path&gt;][; secure][; httponly].  Returns null if there are no associated cookies.</returns>
        /// <seealso cref="ClearCookies()"/>
        /// <seealso cref="SetCookie(string,string)"/>
        string GetCookie(string url, string cookieName);

        /// <summary>
        /// Sets the value of a cookie.
        /// </summary>
        /// <remarks>
        /// If no expiration date is specified, the cookie expires when the session ends.
        /// </remarks>
        /// <param name="url">The site url associated with the cookie.</param>
        /// <param name="cookieData">The cookie data of the form:
        /// &lt;name&gt;=&lt;value&gt;[; &lt;name&gt;=&lt;value&gt;]...
        /// [; expires=&lt;date:DAY, DD-MMM-YYYY HH:MM:SS GMT&gt;][; domain=&lt;domain_name&gt;]
        /// [; path=&lt;some_path&gt;][; secure][; httponly].</param>
        /// <seealso cref="ClearCookies()"/>
        /// <seealso cref="GetCookie(string,string)"/>
        void SetCookie(string url, string cookieData);

        /// <summary>
        /// Gets or sets a value indicating whether to auto close IE after destroying
        /// a reference to the corresponding IE instance.
        /// </summary>
        /// <value><c>true</c> when to auto close IE (this is the default); otherwise, <c>false</c>.</value>
        bool AutoClose { get; set; }

        /// <summary>
        /// Returns a collection of open HTML dialogs (modal as well as modeless).
        /// </summary>
        /// <value>The HTML dialogs.</value>
        HtmlDialogCollection HtmlDialogs { get; }

        /// <summary>
        /// Returns a collection of open HTML dialogs (modal as well as modeless).
        /// When itterating through this collection WaitForComplete will not be
        /// called on a HTML dialog before returning it from the collection.
        /// </summary>
        /// <value>The HTML dialogs.</value>
        HtmlDialogCollection HtmlDialogsNoWait { get; }

        /// <summary>
        /// Find a HtmlDialog by an attribute. Currently 
        /// Find.ByUrl and Find.ByTitle are supported.
        /// </summary>
        /// <param name="findBy">The url of the html page shown in the dialog</param>
        HtmlDialog HtmlDialog(AttributeConstraint findBy);

        /// <summary>
        /// Find a HtmlDialog by an attribute within the given <paramref name="timeout" /> period.
        /// Currently Find.ByUrl and Find.ByTitle are supported.
        /// </summary>
        /// <param name="findBy">The url of the html page shown in the dialog</param>
        /// <param name="timeout">Number of seconds before the search times out.</param>
        HtmlDialog HtmlDialog(AttributeConstraint findBy, int timeout);

        IntPtr hWnd { get; }

        /// <summary>
        /// Gets the process ID the Internet Explorer or HTMLDialog is running in.
        /// </summary>
        /// <value>The process ID.</value>
        int ProcessID { get; }

        /// <summary>
        /// Returns the 'raw' html document for the internet explorer DOM.
        /// </summary>
        IHTMLDocument2 HtmlDocument { get; }

        /// <summary>
        /// Gets the dialog watcher.
        /// </summary>
        /// <value>The dialog watcher.</value>
        DialogWatcher DialogWatcher { get; }

        /// <summary>
        /// Adds the dialog handler.
        /// </summary>
        /// <param name="handler">The dialog handler.</param>
        void AddDialogHandler(IDialogHandler handler);

        /// <summary>
        /// Removes the dialog handler.
        /// </summary>
        /// <param name="handler">The dialog handler.</param>
        void RemoveDialogHandler(IDialogHandler handler);

        /// <summary>
        /// Waits for the page to be completely loaded
        /// </summary>
        void WaitForComplete();

        /// <summary>
        /// Waits for the page to be completely loaded
        /// </summary>
        /// <param name="waitForComplete">The wait for complete.</param>
        void WaitForComplete(IWait waitForComplete);

        /// <summary>
        /// Captures the web page to file. The file extension is used to 
        /// determine the image format. The following image formats are
        /// supported (if the encoder is available on the machine):
        /// jpg, tif, gif, png, bmp.
        /// If you want more controle over the output, use <seealso cref="CaptureWebPage.CaptureWebPageToFile(string,bool,bool,int,int)"/>
        /// </summary>
        /// <param name="filename">The filename.</param>
        void CaptureWebPageToFile(string filename);

        void Dispose();

        /// <summary>
        /// Gets the HTML of the Body part of the webpage.
        /// </summary>
        /// <value>The HTML.</value>
        string Html { get; }

        /// <summary>
        /// Gets the inner text of the Body part of the webpage.
        /// </summary>
        /// <value>The inner text.</value>
        string Text { get; }

        /// <summary>
        /// Returns a System.Uri instance of the url displayed in the address bar of the browser, 
        /// of the currently displayed web page.
        /// </summary>
        /// <example>
        /// The following example creates a new Internet Explorer instances, navigates to
        /// the WatiN Project website on SourceForge and writes the Uri of the
        /// currently displayed webpage to the debug window.
        /// <code>
        /// using WatiN.Core;
        /// using System.Diagnostics;
        ///
        /// namespace NewIEExample
        /// {
        ///    public class WatiNWebsite
        ///    {
        ///      public WatiNWebsite()
        ///      {
        ///        IE ie = new IE("http://watin.sourceforge.net");
        ///        Debug.WriteLine(ie.Uri.ToString());
        ///      }
        ///    }
        ///  }
        /// </code>
        /// </example>
        Uri Uri { get; }

        /// <summary>
        /// Returns the url, as displayed in the address bar of the browser, of the currently
        /// displayed web page.
        /// </summary>
        /// <example>
        /// The following example creates a new Internet Explorer instances, navigates to
        /// the WatiN Project website on SourceForge and writes the Url of the
        /// currently displayed webpage to the debug window.
        /// <code>
        /// using WatiN.Core;
        /// using System.Diagnostics;
        ///
        /// namespace NewIEExample
        /// {
        ///    public class WatiNWebsite
        ///    {
        ///      public WatiNWebsite()
        ///      {
        ///        IE ie = new IE("http://watin.sourceforge.net");
        ///        Debug.WriteLine(ie.Url);
        ///      }
        ///    }
        ///  }
        /// </code>
        /// </example>
        string Url { get; }

        /// <summary>
        /// Determines whether the text inside the HTML Body element contains the given <paramref name="text" />.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        ///     <c>true</c> if the specified text is contained in <see cref="Html"/>; otherwise, <c>false</c>.
        /// </returns>
        bool ContainsText(string text);

        /// <summary>
        /// Determines whether the text inside the HTML Body element contains the given <paramref name="regex" />.
        /// </summary>
        /// <param name="regex">The regular expression to match with.</param>
        /// <returns>
        ///     <c>true</c> if the specified text is contained in <see cref="Html"/>; otherwise, <c>false</c>.
        /// </returns>
        bool ContainsText(Regex regex);

        /// <summary>
        /// Waits until the text is inside the HTML Body element contains the given <paramref name="text" />.
        /// Will time out after <see name="IE.Settings.WaitUntilExistsTimeOut" />
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        ///     <see name="TimeoutException"/> if the specified text is not found within the time out period.
        /// </returns>
        void WaitUntilContainsText(string text);

        /// <summary>
        /// Waits until the <paramref name="regex" /> matches some text inside the HTML Body element contains the given <paramref name="text" />.
        /// Will time out after <see name="IE.Settings.WaitUntilExistsTimeOut" />
        /// </summary>
        /// <param name="regex">The regular expression to match with.</param>
        /// <returns>
        ///     <see name="TimeoutException"/> if the specified text is not found within the time out period.
        /// </returns>
        void WaitUntilContainsText(Regex regex);

        /// <summary>
        /// Gets the text inside the HTML Body element that matches the regular expression.
        /// </summary>
        /// <param name="regex">The regular expression to match with.</param>
        /// <returns>The matching text, or null if none.</returns>
        string FindText(Regex regex);

        /// <summary>
        /// Gets the title of the webpage.
        /// </summary>
        /// <value>The title.</value>
        string Title { get; }

        /// <summary>
        /// Gets the active element in this webpage.
        /// </summary>
        /// <value>The active element or <c>null</c> if no element has the focus.</value>
        Element ActiveElement { get; }

        /// <summary>
        /// Gets the specified frame by its id.
        /// </summary>
        /// <param name="id">The id of the frame.</param>
        /// <exception cref="FrameNotFoundException">Thrown if the given <paramref name="id" /> isn't found.</exception>
        Frame Frame(string id);

        /// <summary>
        /// Gets the specified frame by its id.
        /// </summary>
        /// <param name="id">The regular expression to match with the id of the frame.</param>
        /// <exception cref="FrameNotFoundException">Thrown if the given <paramref name="id" /> isn't found.</exception>
        Frame Frame(Regex id);

        /// <summary>
        /// Gets the specified frame by its name.
        /// </summary>
        /// <param name="findBy">The name of the frame.</param>
        /// <exception cref="FrameNotFoundException">Thrown if the given name isn't found.</exception>
        Frame Frame(AttributeConstraint findBy);

        /// <summary>
        /// Gets a typed collection of <see cref="WatiN.Core.Frame"/> opend within this <see cref="Document"/>.
        /// </summary>
        FrameCollection Frames { get; }

        Area Area(string elementId);
        Area Area(Regex elementId);
        Area Area(AttributeConstraint findBy);

        AreaCollection Areas { get; }

        Button Button(string elementId);
        Button Button(Regex elementId);
        Button Button(AttributeConstraint findBy);

        ButtonCollection Buttons { get; }

        CheckBox CheckBox(string elementId);
        CheckBox CheckBox(Regex elementId);
        CheckBox CheckBox(AttributeConstraint findBy);

        CheckBoxCollection CheckBoxes { get; }

        Element Element(string elementId);
        Element Element(Regex elementId);
        Element Element(AttributeConstraint findBy);
        Element Element(string tagname, AttributeConstraint findBy, params string[] inputtypes);

        ElementCollection Elements { get; }

        FileUpload FileUpload(string elementId);
        FileUpload FileUpload(Regex elementId);
        FileUpload FileUpload(AttributeConstraint findBy);

        FileUploadCollection FileUploads { get; }

        Form Form(string elementId);
        Form Form(Regex elementId);
        Form Form(AttributeConstraint findBy);

        FormCollection Forms { get; }

        Label Label(string elementId);
        Label Label(Regex elementId);
        Label Label(AttributeConstraint findBy);

        LabelCollection Labels { get; }

        Link Link(string elementId);
        Link Link(Regex elementId);
        Link Link(AttributeConstraint findBy);

        LinkCollection Links { get; }

        Para Para(string elementId);
        Para Para(Regex elementId);
        Para Para(AttributeConstraint findBy);

        ParaCollection Paras { get; }

        RadioButton RadioButton(string elementId);
        RadioButton RadioButton(Regex elementId);
        RadioButton RadioButton(AttributeConstraint findBy);

        RadioButtonCollection RadioButtons { get; }

        SelectList SelectList(string elementId);
        SelectList SelectList(Regex elementId);
        SelectList SelectList(AttributeConstraint findBy);

        SelectListCollection SelectLists { get; }

        Table Table(string elementId);
        Table Table(Regex elementId);
        Table Table(AttributeConstraint findBy);

        TableCollection Tables { get; }

        TableBody TableBody(string elementId);
        TableBody TableBody(Regex elementId);
        TableBody TableBody(AttributeConstraint findBy);

        TableBodyCollection TableBodies { get; }

        TableCell TableCell(string elementId);
        TableCell TableCell(Regex elementId);
        TableCell TableCell(AttributeConstraint findBy);
        TableCell TableCell(string elementId, int index);
        TableCell TableCell(Regex elementId, int index);

        TableCellCollection TableCells { get; }

        TableRow TableRow(string elementId);
        TableRow TableRow(Regex elementId);
        TableRow TableRow(AttributeConstraint findBy);

        TableRowCollection TableRows { get; }

        TextField TextField(string elementId);
        TextField TextField(Regex elementId);
        TextField TextField(AttributeConstraint findBy);

        TextFieldCollection TextFields { get; }

        Span Span(string elementId);
        Span Span(Regex elementId);
        Span Span(AttributeConstraint findBy);

        SpanCollection Spans { get; }

        Div Div(string elementId);
        Div Div(Regex elementId);
        Div Div(AttributeConstraint findBy);

        DivCollection Divs { get; }

        Image Image(string elementId);
        Image Image(Regex elementId);
        Image Image(AttributeConstraint findBy);

        ImageCollection Images { get; }

        /// <summary>
        /// Runs the javascript code in IE.
        /// </summary>
        /// <param name="javaScriptCode">The javascript code.</param>
        void RunScript(string javaScriptCode);

        /// <summary>
        /// Runs the script code in IE.
        /// </summary>
        /// <param name="scriptCode">The script code.</param>
        /// <param name="language">The language.</param>
        void RunScript(string scriptCode, string language);

        /// <summary>
        /// Fires the given event on the given element.
        /// </summary>
        /// <param name="element">Element to fire the event on</param>
        /// <param name="eventName">Name of the event to fire</param>
        void FireEvent(DispHTMLBaseElement element, string eventName);

        /// <summary>
        /// Evaluates the specified JavaScript code within the scope of this
        /// document. Returns the value computed by the last expression in the
        /// code block after implicit conversion to a string.
        /// </summary>
        /// <example>
        /// The following example shows an alert dialog then returns the string "4".
        /// <code>
        /// Eval("window.alert('Hello world!'); 2 + 2");
        /// </code>
        /// </example>
        /// <param name="javaScriptCode">The JavaScript code</param>
        /// <returns>The result converted to a string</returns>
        /// <exception cref="JavaScriptException">Thrown when the JavaScript code cannot be evaluated
        /// or throws an exception during evaluation</exception>
        string Eval(string javaScriptCode);
    }
}