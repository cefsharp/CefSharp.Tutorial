namespace KnowledgeBase.ViewModel
{
	using System.Reflection;
	using System.Windows.Input;
	using CefSharp;
	using KnowledgeBase.ViewModels.Commands;

	/// <summary>
	/// ApplicationViewModel manages the appplications state and its main objects.
	/// </summary>
	public class AppViewModel : Base.ViewModelBase
	{
		#region fields
		public const string TestResourceUrl = "http://test/resource/load";
		public const string TestUnicodeResourceUrl = "http://test/resource/loadUnicode";

		private ICommand mTestUrlCommand = null;
		private ICommand mTestUrl1Command = null;

		private string mBrowserAddress;
		private string mAssemblyTitle;
		#endregion fields

		#region constructors
		/// <summary>
		/// Class Constructor
		/// </summary>
		public AppViewModel()
		{
			this.mAssemblyTitle = Assembly.GetEntryAssembly().GetName().Name;

			this.BrowserAddress = AppViewModel.TestResourceUrl;
		}
		#endregion constructors

		#region properties
		/// <summary>
		/// Get/set current address of web browser URI.
		/// </summary>
		public string BrowserAddress
		{
			get
			{
				return this.mBrowserAddress;
			}

			set
			{
				if (this.mBrowserAddress != value)
				{
					this.mBrowserAddress = value;
					this.RaisePropertyChanged(() => this.BrowserAddress);
					this.RaisePropertyChanged(() => this.BrowserTitle);
				}
			}
		}

		/// <summary>
		/// Get "title" - "Uri" string of current address of web browser URI
		/// for display in UI - to let the user know what his looking at.
		/// </summary>
		public string BrowserTitle
		{
			get
			{
				return string.Format("{0} - {1}", this.mAssemblyTitle, this.mBrowserAddress);
			}
		}

		/// <summary>
		/// Get test Command to browse to a test URL ...
		/// </summary>
		public ICommand TestUrlCommand
		{
			get
			{
				if (this.mTestUrlCommand == null)
				{
					this.mTestUrlCommand = new RelayCommand(() => 
					{
						// Setting this address sets the current address of the browser
						// control via bound BrowserAddress property
						this.BrowserAddress = AppViewModel.TestResourceUrl;
					});
				}

				return this.mTestUrlCommand;
			}
		}

		/// <summary>
		/// Get test Command to browse to a test URL 1 ...
		/// </summary>
		public ICommand TestUrl1Command
		{
			get
			{
				if (this.mTestUrl1Command == null)
				{
					this.mTestUrl1Command = new RelayCommand(() =>
					{
						// Setting this address sets the current address of the browser
						// control via bound BrowserAddress property
						this.BrowserAddress = AppViewModel.TestUnicodeResourceUrl;
					});
				}

				return this.mTestUrl1Command;
			}
		}
		#endregion properties

		#region methods
		/// <summary>
		/// Registers 2 Test URIs with HTML loaded from strings
		/// </summary>
		/// <param name="browser"></param>
		public static void RegisterTestResources(IWebBrowser browser)
		{
			var handler = browser.ResourceHandlerFactory;

			if (handler != null)
			{
				const string responseBody =
				"<html>"
					+ "<body><h1>About</h1>"
					+ "<p>This sample application implements a <b>ResourceHandler</b> "
					+ "which can be used to fullfil custom network requests as explained here:"
					+ "<a href=\"http://www.codeproject.com/Articles/881315/Display-HTML-in-WPF-and-CefSharp-Tutorial-Part 2\">http://www.codeproject.com/Articles/881315/Display-HTML-in-WPF-and-CefSharp-Tutorial-Part 2</a>"
					+ ".</p>"
					+ "<hr/><p>"
					+ "This sample is based on the Continues Integration (CI) for CefSharp from MyGet: <a href=\"https://www.myget.org/F/cefsharp/\">https://www.myget.org/F/cefsharp/</a>"
					+ " since it relies on the resolution of some known problems in the current release version: <b>37.0.0</b>.</p>"
					+ "<ul>"
					+ "<li><a href=\"https://github.com/cefsharp/CefSharp/commit/54b1520761da125b29322670504e98a2eb56c855\">https://github.com/cefsharp/CefSharp/commit/54b1520761da125b29322670504e98a2eb56c855</a></li>"
					+ "<li><a href=\"https://github.com/cefsharp/CefSharp/pull/857\">https://github.com/cefsharp/CefSharp/pull/857</a></li>"
					+ "</ul>"
					+ "<hr/><p>"
					+ "Feel free to switch over to NuGet: <a href=\"https://www.nuget.org/packages/CefSharp.Wpf/\">https://www.nuget.org/packages/CefSharp.Wpf/</a>"
					+ " when version 39.0.0 or later is released.</p>"
					+ "<hr/>"
					+ "<p>See also CefSharp on GitHub: <a href=\"https://github.com/cefsharp\">https://github.com/cefsharp</a><br/>"
					+ "<p>and Cef at Google: <a href=\"https://code.google.com/p/chromiumembedded/wiki/GeneralUsage#Request_Handling\">https://code.google.com/p/chromiumembedded/wiki/GeneralUsage#Request_Handling</a>"
					+ "</body></html>";

				handler.RegisterHandler(TestResourceUrl, ResourceHandler.FromString(responseBody));

				const string unicodeResponseBody = "<html><body>整体满意度</body></html>";
				handler.RegisterHandler(TestUnicodeResourceUrl, ResourceHandler.FromString(unicodeResponseBody));
			}
		}
		#endregion methods
	}
}
