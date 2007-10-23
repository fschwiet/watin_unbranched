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

using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using mshtml;
using SHDocVw;
using WatiN.Core.DialogHandlers;
using WatiN.Core.Exceptions;
using WatiN.Core.Logging;

namespace WatiN.Core
{
	/// <summary>
	/// This is the main class to access a webpage in Internet Explorer to 
	/// get to all the elements and (i)frames on the page.
	/// 
	/// </summary>
	/// <example>
	/// The following example creates a new Internet Explorer instances and navigates to
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
	///        IE ie = new IE("http://watin.sourceforge.net");
	///        ie.Link(Find.ByText("RSS Feeds")).Click;
	///        ie.Close;
	///      }
	///    }
	///  }
	/// </code>
	/// </example>
	public class IE : DomContainer
	{
		private InternetExplorer ie;

		private bool autoClose = true;
		private bool isDisposed = false;
		private static Settings settings = new Settings();

		public static Settings Settings
		{
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}

				settings = value;
			}
			get { return settings; }
		}

		/// <summary>
		/// Attach to an existing Internet Explorer. 
		/// The first instance that matches the given <paramref name="findBy"/> will be returned.
		/// The attached Internet Explorer will be closed after destroying the IE instance.
		/// </summary>
		/// <param name="findBy">The <see cref="AttributeConstraint"/> of the IE window to find. 
		/// <c>Find.ByUrl()</c>, <c>Find.ByUri()</c>, <c>Find.ByTitle()</c> and <c>Find.By("hwnd", windowHandle)</c> are supported.</param>
		/// <returns>An <see cref="IE"/> instance.</returns>
		/// <exception cref="WatiN.Core.Exceptions.IENotFoundException" >
		/// IENotFoundException will be thrown if an Internet Explorer window with the given <paramref name="findBy"/> isn't found within 30 seconds.
		/// To change this default, set <see cref="P:WatiN.Core.IE.Settings.AttachToIETimeOut"/>
		/// </exception>
		/// <example>
		/// The following example searches for an Internet Exlorer instance with "Example"
		/// in it's titlebar (showing up as "Example - Microsoft Internet Explorer").
		/// When found, ieExample will hold a pointer to this Internet Explorer instance and
		/// can be used to test the Example page.
		/// <code>
		/// IE ieExample = IE.AttachToIE(Find.ByTitle("Example"));
		/// </code>
		/// A partial match should also work
		/// <code>
		/// IE ieExample = IE.AttachToIE(Find.ByTitle("Exa"));
		/// </code>
		/// </example>
		public static IE AttachToIE(AttributeConstraint findBy)
		{
			return findIE(findBy, Settings.AttachToIETimeOut);
		}

		/// <summary>
		/// Attach to an existing Internet Explorer. 
		/// The first instance that matches the given <paramref name="findBy"/> will be returned.
		/// The attached Internet Explorer will be closed after destroying the IE instance.
		/// </summary>
		/// <param name="findBy">The <see cref="AttributeConstraint"/> of the IE window to find. 
		/// <c>Find.ByUrl()</c>, <c>Find.ByUri()</c>, <c>Find.ByTitle()</c> and <c>Find.By("hwnd", windowHandle)</c> are supported.</param>
		/// <param name="timeout">The number of seconds to wait before timing out</param>
		/// <returns>An <see cref="IE"/> instance.</returns>
		/// <exception cref="WatiN.Core.Exceptions.IENotFoundException" >
		/// IENotFoundException will be thrown if an Internet Explorer window with the given 
		/// <paramref name="findBy"/> isn't found within <paramref name="timeout"/> seconds.
		/// </exception>
		/// <example>
		/// The following example searches for an Internet Exlorer instance with "Example"
		/// in it's titlebar (showing up as "Example - Microsoft Internet Explorer").
		/// It will try to find an Internet Exlorer instance for 60 seconds maxs.
		/// When found, ieExample will hold a pointer to this Internet Explorer instance and
		/// can be used to test the Example page.
		/// <code>
		/// IE ieExample = IE.AttachToIE(Find.ByTitle("Example"), 60);
		/// </code>
		/// A partial match should also work
		/// <code>
		/// IE ieExample = IE.AttachToIE(Find.ByTitle("Exa"), 60);
		/// </code>
		/// </example>
		public static IE AttachToIE(AttributeConstraint findBy, int timeout)
		{
			return findIE(findBy, timeout);
		}

		/// <summary>
		/// Does the specified Internet Explorer exist.
		/// </summary>
		/// <param name="findBy">The <see cref="AttributeConstraint"/> of the IE window to find. 
		/// <c>Find.ByUrl()</c>, <c>Find.ByUri()</c>, <c>Find.ByTitle()</c> and <c>Find.By("hwnd", windowHandle)</c> are supported.</param>
		/// <returns><c>true</c> if an Internet Explorer instance matches the given <paramref name="findBy"/> <see cref="AttributeConstraint"/>. Otherwise it returns <c>false</c>. </returns>
		public static bool Exists(AttributeConstraint findBy)
		{
			return (null != findInternetExplorer(findBy));
		}

		/// <summary>
		/// Creates a collection of new IE instances associated with open Internet Explorer windows.
		/// </summary>
		/// <example>
		/// This code snippet illustrates the use of this method to found out the number of open
		/// Internet Explorer windows.
		/// <code>int IECount = IE.InternetExplorers.Length;</code>
		/// </example>
		public static IECollection InternetExplorers()
		{
			return new IECollection();
		}

		/// <summary>
		/// Opens a new Internet Explorer with a blank page. 
		/// <note>
		/// When the <see cref="WatiN.Core.IE" />
		/// instance is destroyed the created Internet Explorer window will also be closed.
		/// </note>
		/// </summary>
		/// <remarks>
		/// You could also use one of the overloaded constructors.
		/// </remarks>
		/// <example>
		/// The following example creates a new Internet Explorer instances and navigates to
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
		public IE()
		{
			CreateNewIEAndGoToUri(new Uri("about:blank"), null, false);
		}

		/// <summary>
		/// Opens a new Internet Explorer with a blank page. 
		/// <note>
		/// When the <see cref="WatiN.Core.IE" />
		/// instance is destroyed the created Internet Explorer window will also be closed.
		/// </note>
		/// </summary>
		/// <param name="createInNewProcess">if set to <c>true</c> the IE instance is created in a new process.</param>
		/// <remarks>
		/// You could also use one of the overloaded constructors.
		/// </remarks>
		/// <example>
		/// The following example creates a new Internet Explorer instances and navigates to
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
		public IE(bool createInNewProcess)
		{
			CreateNewIEAndGoToUri(new Uri("about:blank"), null, createInNewProcess);
		}

		/// <summary>
		/// Opens a new Internet Explorer and navigates to the given <paramref name="url"/>.
		/// <note>
		/// When the <see cref="WatiN.Core.IE" />
		/// instance is destroyed the created Internet Explorer window will also be closed.
		/// </note>
		/// </summary>
		/// <param name="url">The URL te open</param>
		/// <remarks>
		/// You could also use one of the overloaded constructors.
		/// </remarks>
		/// <example>
		/// The following example creates a new Internet Explorer instances and navigates to
		/// the WatiN Project website on SourceForge.
		/// <code>
		/// using WatiN.Core;
		/// 
		/// namespace NewIEExample
		/// {
		///    public class WatiNWebsite
		///    {
		///      public OpenWatiNWebsite()
		///      {
		///        IE ie = new IE("http://watin.sourceforge.net");
		///      }
		///    }
		///  }
		/// </code>
		/// </example>
		public IE(string url)
		{
			CreateNewIEAndGoToUri(CreateUri(url), null, false);
		}

		private static Uri CreateUri(string url)
		{
			Uri uri;
			try
			{
				uri = new Uri(url);
			}
			catch (UriFormatException)
			{
				uri = new Uri("http://" + url);
			}
			return uri;
		}

		/// <summary>
		/// Opens a new Internet Explorer and navigates to the given <paramref name="url"/>.
		/// <note>
		/// When the <see cref="WatiN.Core.IE" />
		/// instance is destroyed the created Internet Explorer window will also be closed.
		/// </note>
		/// </summary>
		/// <param name="url">The URL te open</param>
		/// <param name="createInNewProcess">if set to <c>true</c> the IE instance is created in a new process.</param>
		/// <remarks>
		/// You could also use one of the overloaded constructors.
		/// </remarks>
		/// <example>
		/// The following example creates a new Internet Explorer instances and navigates to
		/// the WatiN Project website on SourceForge.
		/// <code>
		/// using WatiN.Core;
		/// 
		/// namespace NewIEExample
		/// {
		///    public class WatiNWebsite
		///    {
		///      public OpenWatiNWebsite()
		///      {
		///        IE ie = new IE("http://watin.sourceforge.net");
		///      }
		///    }
		///  }
		/// </code>
		/// </example>
		public IE(string url, bool createInNewProcess)
		{
			CreateNewIEAndGoToUri(CreateUri(url), null, createInNewProcess);
		}

		/// <summary>
		/// Opens a new Internet Explorer and navigates to the given <paramref name="uri"/>.
		/// <note>
		/// When the <see cref="WatiN.Core.IE" />
		/// instance is destroyed the created Internet Explorer window will also be closed.
		/// </note>
		/// </summary>
		/// <param name="uri">The Uri te open</param>
		/// <remarks>
		/// You could also use one of the overloaded constructors.
		/// </remarks>
		/// <example>
		/// The following example creates a new Internet Explorer instances and navigates to
		/// the WatiN Project website on SourceForge.
		/// <code>
		/// using System;
		/// using WatiN.Core;
		/// 
		/// namespace NewIEExample
		/// {
		///    public class WatiNWebsite
		///    {
		///      public OpenWatiNWebsite()
		///      {
		///        IE ie = new IE(new Uri("http://watin.sourceforge.net"));
		///      }
		///    }
		///  }
		/// </code>
		/// </example>
		public IE(Uri uri)
		{
			CreateNewIEAndGoToUri(uri, null, false);
		}

		/// <summary>
		/// Opens a new Internet Explorer and navigates to the given <paramref name="uri"/>.
		/// <note>
		/// When the <see cref="WatiN.Core.IE" />
		/// instance is destroyed the created Internet Explorer window will also be closed.
		/// </note>
		/// </summary>
		/// <param name="uri">The Uri te open</param>
		/// <param name="createInNewProcess">if set to <c>true</c> the IE instance is created in a new process.</param>
		/// <remarks>
		/// You could also use one of the overloaded constructors.
		/// </remarks>
		/// <example>
		/// The following example creates a new Internet Explorer instances and navigates to
		/// the WatiN Project website on SourceForge.
		/// <code>
		/// using System;
		/// using WatiN.Core;
		/// 
		/// namespace NewIEExample
		/// {
		///    public class WatiNWebsite
		///    {
		///      public OpenWatiNWebsite()
		///      {
		///        IE ie = new IE(new Uri("http://watin.sourceforge.net"));
		///      }
		///    }
		///  }
		/// </code>
		/// </example>
		public IE(Uri uri, bool createInNewProcess)
		{
			CreateNewIEAndGoToUri(uri, null, createInNewProcess);
		}

		/// <summary>
		/// Opens a new Internet Explorer and navigates to the given <paramref name="url"/>.
		/// </summary>
		/// <param name="url">The Url te open</param>
		/// <param name="logonDialogHandler">A <see cref="LogonDialogHandler"/> class instanciated with the logon credentials.</param>
		/// <remarks>
		/// You could also use one of the overloaded constructors.
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
		///        IE ie = new IE("http://watin.sourceforge.net", logon);
		///      }
		///    }
		///  }
		/// </code>
		/// </example>
		public IE(string url, LogonDialogHandler logonDialogHandler)
		{
			CreateNewIEAndGoToUri(CreateUri(url), logonDialogHandler, false);
		}

		/// <summary>
		/// Opens a new Internet Explorer and navigates to the given <paramref name="url"/>.
		/// </summary>
		/// <param name="url">The Url te open</param>
		/// <param name="logonDialogHandler">A <see cref="LogonDialogHandler"/> class instanciated with the logon credentials.</param>
		/// <param name="createInNewProcess">if set to <c>true</c> the IE instance is created in a new process.</param>
		/// <remarks>
		/// You could also use one of the overloaded constructors.
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
		///        IE ie = new IE("http://watin.sourceforge.net", logon);
		///      }
		///    }
		///  }
		/// </code>
		/// </example>
		public IE(string url, LogonDialogHandler logonDialogHandler, bool createInNewProcess)
		{
			CreateNewIEAndGoToUri(CreateUri(url), logonDialogHandler, createInNewProcess);
		}

		/// <summary>
		/// Opens a new Internet Explorer and navigates to the given <paramref name="uri"/>.
		/// </summary>
		/// <param name="uri">The Uri te open</param>
		/// <param name="logonDialogHandler">A <see cref="LogonDialogHandler"/> class instanciated with the logon credentials.</param>
		/// <remarks>
		/// You could also use one of the overloaded constructors.
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
		///      }
		///    }
		///  }
		/// </code>
		/// </example>
		public IE(Uri uri, LogonDialogHandler logonDialogHandler)
		{
			CreateNewIEAndGoToUri(uri, logonDialogHandler, false);
		}

		/// <summary>
		/// Opens a new Internet Explorer and navigates to the given <paramref name="uri"/>.
		/// </summary>
		/// <param name="uri">The Uri te open</param>
		/// <param name="logonDialogHandler">A <see cref="LogonDialogHandler"/> class instanciated with the logon credentials.</param>
		/// <param name="createInNewProcess">if set to <c>true</c> the IE instance is created in a new process.</param>
		/// <remarks>
		/// You could also use one of the overloaded constructors.
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
		///      }
		///    }
		///  }
		/// </code>
		/// </example>
		public IE(Uri uri, LogonDialogHandler logonDialogHandler, bool createInNewProcess)
		{
			CreateNewIEAndGoToUri(uri, logonDialogHandler, createInNewProcess);
		}

		/// <summary>
		/// Use existing InternetExplorer object. The param is of type
		/// object because otherwise all projects using WatiN should also
		/// reference the Interop.SHDocVw assembly.
		/// </summary>
		/// <param name="shDocVwInternetExplorer">The Interop.SHDocVw.InternetExplorer object to use</param>
		public IE(object shDocVwInternetExplorer)
		{
			CheckThreadApartmentStateIsSTA();

			InternetExplorer internetExplorer = shDocVwInternetExplorer as InternetExplorer;

			if (shDocVwInternetExplorer == null)
			{
				throw new ArgumentException("SHDocVwInternetExplorer needs to be of type Interop.SHDocVw.InternetExplorer");
			}

			InitIEAndStartDialogWatcher(internetExplorer);
		}

		private void CreateNewIEAndGoToUri(Uri uri, LogonDialogHandler logonDialogHandler, bool createInNewProcess)
		{
			CheckThreadApartmentStateIsSTA();

			MoveMousePoinerToTopLeft();

			if (createInNewProcess)
			{
				Logger.LogAction("Creating IE instance in a new process");
				InitIEAndStartDialogWatcher(CreateIEInNewProcess(), uri);
			}
			else
			{
				Logger.LogAction("Creating IE instance");
				InitIEAndStartDialogWatcher(new InternetExplorerClass(), uri);
			}

			if (logonDialogHandler != null)
			{
				// remove other logon dialog handlers since only one handler
				// can effectively handle the logon dialog.
				DialogWatcher.RemoveAll(new LogonDialogHandler("a", "b"));

				// Add the (new) logonHandler
				DialogWatcher.Add(logonDialogHandler);
			}

			WaitForComplete();
		}

		private InternetExplorer CreateIEInNewProcess()
		{
			Process m_Proc = Process.Start("IExplore.exe", "about:blank");

			const int timeout = 5000;
			SimpleTimer timeoutTimer = new SimpleTimer(timeout);

			do
			{
				m_Proc.Refresh();
				int mainWindowHandle = (int) m_Proc.MainWindowHandle;

				if (mainWindowHandle != 0)
				{
					return findInternetExplorer(new AttributeConstraint("hwnd", mainWindowHandle.ToString()), settings.AttachToIETimeOut);
				}
				else
				{
					Thread.Sleep(500);
				}
			} while (!timeoutTimer.Elapsed);

			throw new IENotFoundException("hwnd", "0", timeout);
		}

		private static void CheckThreadApartmentStateIsSTA()
		{
#if NET11      
			// Code for .Net 1.1
			bool isSTA = (Thread.CurrentThread.ApartmentState == ApartmentState.STA);
#elif NET20
	// Code for .Net 2.0
      bool isSTA = (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA);
#endif
			if (!isSTA)
			{
				throw new ThreadStateException("The CurrentThread needs to have it's ApartmentState set to ApartmentState.STA to be able to automate Internet Explorer.");
			}
		}

		private void InitIEAndStartDialogWatcher(InternetExplorer internetExplorer)
		{
			InitIEAndStartDialogWatcher(internetExplorer, null);
		}

		private void InitIEAndStartDialogWatcher(InternetExplorer internetExplorer, Uri uri)
		{
			ie = internetExplorer;

			// Due to UAC in Vista the navigate has to be done
			// before showing the new Internet Explorer instance
			if (uri != null)
			{
				navigateTo(uri);
			}
			ie.Visible = Settings.MakeNewIeInstanceVisible;
			StartDialogWatcher();
		}

		private static IE findIE(AttributeConstraint findBy, int timeout)
		{
			InternetExplorer internetExplorer = findInternetExplorer(findBy, timeout);

			if (internetExplorer != null)
			{
				IE ie = new IE(internetExplorer);
				ie.WaitForComplete();

				return ie;
			}

			throw new IENotFoundException(findBy.AttributeName, findBy.Value, timeout);
		}

		private static InternetExplorer findInternetExplorer(AttributeConstraint findBy, int timeout)
		{
			Logger.LogAction("Busy finding Internet Explorer with " + findBy.AttributeName + " '" + findBy.Value + "'");

			SimpleTimer timeoutTimer = new SimpleTimer(timeout);

			do
			{
				Thread.Sleep(500);

				InternetExplorer internetExplorer = findInternetExplorer(findBy);

				if (internetExplorer != null)
				{
					return internetExplorer;
				}
			} while (!timeoutTimer.Elapsed);

			return null;
		}

		private static InternetExplorer findInternetExplorer(AttributeConstraint findBy)
		{
			ShellWindows allBrowsers = new ShellWindows();

			int browserCount = allBrowsers.Count;
			int browserCounter = 0;

			IEAttributeBag attributeBag = new IEAttributeBag();

			while (browserCounter < browserCount)
			{
				attributeBag.InternetExplorer = (InternetExplorer) allBrowsers.Item(browserCounter);

				if (findBy.Compare(attributeBag))
				{
					return attributeBag.InternetExplorer;
				}

				browserCounter++;
			}

			return null;
		}

		private void MoveMousePoinerToTopLeft()
		{
			if (Settings.AutoMoveMousePointerToTopLeft)
			{
				Cursor.Position = new Point(0, 0);
			}
		}

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
		public void GoTo(Uri url)
		{
			navigateTo(url);
			WaitForComplete();
		}

		private void navigateTo(Uri url)
		{
			Logger.LogAction("Navigating to '" + url + "'");

			object nil = null;
			ie.Navigate(url.ToString(), ref nil, ref nil, ref nil, ref nil);
		}

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
		public void GoTo(string url)
		{
			GoTo(CreateUri(url));
		}

		/// <summary>
		/// Use this method to gain access to the full Internet Explorer object.
		/// Do this by referencing the Interop.SHDocVw assembly (supplied in the WatiN distribution)
		/// and cast the return value of this method to type SHDocVw.InternetExplorer.
		/// </summary>
		public object InternetExplorer
		{
			get { return ie; }
		}

		/// <summary>
		/// Navigates the browser back to the previously displayed Url (like the back
		/// button in Internet Explorer). 
		/// </summary>
		public void Back()
		{
			ie.GoBack();
			WaitForComplete();
			Logger.LogAction("Navigated Back to '" + Url + "'");
		}

		/// <summary>
		/// Navigates the browser forward to the next displayed Url (like the forward
		/// button in Internet Explorer). 
		/// </summary>
		public void Forward()
		{
			ie.GoForward();
			WaitForComplete();
			Logger.LogAction("Navigated Forward to '" + Url + "'");
		}

		/// <summary>
		/// Reloads the currently displayed webpage (like the Refresh button in 
		/// Internet Explorer).
		/// </summary>
		public void Refresh()
		{
			Logger.LogAction("Refreshing browser from '" + Url + "'");

			object REFRESH_COMPLETELY = 3;
			ie.Refresh2(ref REFRESH_COMPLETELY);
			WaitForComplete();
		}

		/// <summary>
		/// Sends a Tab key to the IE window to simulate tabbing through
		/// the elements (and adres bar).
		/// </summary>
		public void PressTab()
		{
			if (!Debugger.IsAttached)
			{
				int intThreadIDIE;
				int intCurrentThreadID;

				NativeMethods.WindowShowStyle currentStyle = GetWindowStyle();

				ShowWindow(NativeMethods.WindowShowStyle.Restore);
				BringToFront();

				intThreadIDIE = ProcessID;
				intCurrentThreadID = NativeMethods.GetCurrentThreadId();

				NativeMethods.AttachThreadInput(intCurrentThreadID, intThreadIDIE, true);

				NativeMethods.keybd_event(NativeMethods.KEYEVENTF_TAB, 0x45, NativeMethods.KEYEVENTF_EXTENDEDKEY, 0);
				NativeMethods.keybd_event(NativeMethods.KEYEVENTF_TAB, 0x45, NativeMethods.KEYEVENTF_EXTENDEDKEY | NativeMethods.KEYEVENTF_KEYUP, 0);

				NativeMethods.AttachThreadInput(intCurrentThreadID, intThreadIDIE, false);

				ShowWindow(currentStyle);
			}
		}

		/// <summary>
		/// Brings the referenced Internet Explorer to the front (makes it the top window)
		/// </summary>
		public void BringToFront()
		{
			if (NativeMethods.GetForegroundWindow() != hWnd)
			{
				NativeMethods.SetForegroundWindow(hWnd);
			}
		}

		/// <summary>
		/// Make the referenced Internet Explorer full screen, minimized, maximized and more.
		/// </summary>
		/// <param name="showStyle">The style to apply.</param>
		public void ShowWindow(NativeMethods.WindowShowStyle showStyle)
		{
			NativeMethods.ShowWindow(hWnd, (int) showStyle);
		}

		/// <summary>
		/// Gets the window style.
		/// </summary>
		/// <returns>The style currently applied to the ie window.</returns>
		public NativeMethods.WindowShowStyle GetWindowStyle()
		{
			NativeMethods.WINDOWPLACEMENT placement = new NativeMethods.WINDOWPLACEMENT();
			placement.length = Marshal.SizeOf(placement);

			NativeMethods.GetWindowPlacement(hWnd, ref placement);

			return (NativeMethods.WindowShowStyle) placement.showCmd;
		}

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
		public void Close()
		{
			if (!isDisposed)
			{
				if (IsInternetExplorerStillAvailable())
				{
					Logger.LogAction("Closing browser '" + Title + "'");
				}

				DisposeAndCloseIE(true);
			}
		}

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
		public void Reopen()
		{
			Reopen(new Uri("about:blank"), null, false);
		}

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
		public void Reopen(Uri uri, LogonDialogHandler logonDialogHandler, bool createInNewProcess)
		{
			Close();
			Recycle();
			CreateNewIEAndGoToUri(uri, logonDialogHandler, createInNewProcess);
		}

		protected override void Recycle()
		{
			base.Recycle();
			isDisposed = false;
		}

		private void DisposeAndCloseIE(bool closeIE)
		{
			if (!isDisposed)
			{
				if (closeIE && IsInternetExplorerStillAvailable())
				{
					//TODO: Since HTMLDialog collection contains all HTMLDialogs
					//      within the processId of this IE instance, there might be
					//      other HTMLDialogs not created by this IE instance. Closing
					//      also those HTMLDialogs seems not right.
					//      So how will we handle this? For now we keep the "old"
					//      implementation.

					// Close all open HTMLDialogs
					HtmlDialogs.CloseAll();
				}

				base.Dispose(true);

				if (closeIE && IsInternetExplorerStillAvailable())
				{
					// Ask IE to close
					ie.Quit();
				}

				ie = null;

				if (closeIE)
				{
					// Wait for IE to close to prevent RPC errors when creating
					// a new WatiN.Core.IE instance.
					Thread.Sleep(1000);
				}

				isDisposed = true;
			}
		}

		/// <summary>
		/// Closes <i>all</i> running instances of Internet Explorer by killing the
		/// process these instances run in. 
		/// </summary>
		public virtual void ForceClose()
		{
			if (isDisposed)
			{
				throw new ObjectDisposedException("Internet Explorer", "The Internet Explorer instance is already disposed. ForceClose can't be performed.");
			}

			Logger.LogAction("Force closing all IE instances");

			int iePid = ProcessID;

			DisposeAndCloseIE(true);

			try
			{
				// Force Internet Explorer instances to close
				Process.GetProcessById(iePid).Kill();
			}
			catch (ArgumentException)
			{
				// ignore: IE is no longer running
			}
		}

		/// <summary>
		/// Clears all browser cookies.
		/// </summary>
		/// <remarks>
		/// Internet Explorer maintains an internal cookie cache that does not immediately
		/// expire when cookies are cleared.  This is the case even when the cookies are
		/// cleared using the Internet Options dialog.  If cookies have been used by
		/// the current browser session it may be necessary to <see cref="Reopen" /> the
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
		/// <seealso cref="Reopen"/>
		public void ClearCookies()
		{
			Logger.LogAction("Clearing cookies for all sites.");

			WinInet.ClearCookies(null);
		}

		/// <summary>
		/// Clears the browser cookies associated with a particular site and to
		/// any of the site's subdomains.
		/// </summary>
		/// <remarks>
		/// Internet Explorer maintains an internal cookie cache that does not immediately
		/// expire when cookies are cleared.  This is the case even when the cookies are
		/// cleared using the Internet Options dialog.  If cookies have been used by
		/// the current browser session it may be necessary to <see cref="Reopen" /> the
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
		/// <seealso cref="Reopen"/>
		public void ClearCookies(string url)
		{
			if (url == null)
				throw new ArgumentNullException("url");

			Logger.LogAction(String.Format("Clearing cookies for site '{0}'.", url));

			WinInet.ClearCookies(url);
		}

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
		/// <seealso cref="Reopen"/>
		public void ClearCache()
		{
			Logger.LogAction("Clearing browser cache.");

			WinInet.ClearCache();
		}

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
		public string GetCookie(string url, string cookieName)
		{
			return WinInet.GetCookie(url, cookieName);
		}

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
		public void SetCookie(string url, string cookieData)
		{
			WinInet.SetCookie(url, cookieData);
		}

		private bool IsInternetExplorerStillAvailable()
		{
			try
			{
				// Call a property of the
				// ie instance to see of it isn't disposed by 
				// another IE instance.
				int hwndDummy = ie.HWND;
			}
			catch
			{
				return false;
			}

			return true;
		}

		internal override IHTMLDocument2 OnGetHtmlDocument()
		{
			return (IHTMLDocument2) ie.Document;
		}

		/// <summary>
		/// Waits till the webpage, it's frames and all it's elements are loaded. This
		/// function is called by WatiN after each action (like clicking a link) so you
		/// should have to use this function on rare occasions.
		/// To change the default time out, set <see cref="P:WatiN.Core.IE.Settings.WaitForCompleteTimeOut"/>
		/// </summary>
		public override void WaitForComplete()
		{
			WaitForComplete(new IEWaitForComplete(this));
		}

		#region IDisposable Members

		/// <summary>
		/// This method must be called by its inheritor to dispose references
		/// to internal resources.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			DisposeAndCloseIE(AutoClose);
		}

		#endregion

		/// <summary>
		/// Gets or sets a value indicating whether to auto close IE after destroying
		/// a reference to the corresponding IE instance.
		/// </summary>
		/// <value><c>true</c> when to auto close IE (this is the default); otherwise, <c>false</c>.</value>
		public bool AutoClose
		{
			get { return autoClose; }
			set { autoClose = value; }
		}

		/// <summary>
		/// Returns a collection of open HTML dialogs (modal as well as modeless).
		/// </summary>
		/// <value>The HTML dialogs.</value>
		public HtmlDialogCollection HtmlDialogs
		{
			get
			{
				Process p = Process.GetProcessById(ProcessID);
				HtmlDialogCollection htmlDialogCollection = new HtmlDialogCollection(p);

				return htmlDialogCollection;
			}
		}

		public override IntPtr hWnd
		{
			get { return new IntPtr(ie.HWND); }
		}

		/// <summary>
		/// Find a HtmlDialog by an attribute. Currently 
		/// Find.ByUrl and Find.ByTitle are supported.
		/// </summary>
		/// <param name="findBy">The url of the html page shown in the dialog</param>
		public HtmlDialog HtmlDialog(AttributeConstraint findBy)
		{
			return findHtmlDialog(findBy, Settings.AttachToIETimeOut);
		}

		/// <summary>
		/// Find a HtmlDialog by an attribute within the given <paramref name="timeout" /> period.
		/// Currently Find.ByUrl and Find.ByTitle are supported.
		/// </summary>
		/// <param name="findBy">The url of the html page shown in the dialog</param>
		/// <param name="timeout">Number of seconds before the search times out.</param>
		public HtmlDialog HtmlDialog(AttributeConstraint findBy, int timeout)
		{
			return findHtmlDialog(findBy, timeout);
		}

		private HtmlDialog findHtmlDialog(AttributeConstraint findBy, int timeout)
		{
			Logger.LogAction("Busy finding HTMLDialog with " + findBy.AttributeName + " '" + findBy.Value + "'");

			SimpleTimer timeoutTimer = new SimpleTimer(timeout);

			do
			{
				Thread.Sleep(500);

				foreach (HtmlDialog htmlDialog in HtmlDialogs)
				{
					if (findBy.Compare(htmlDialog))
					{
						return htmlDialog;
					}
				}
			} while (!timeoutTimer.Elapsed);

			throw new HtmlDialogNotFoundException(findBy.AttributeName, findBy.Value, timeout);
		}
	}
}