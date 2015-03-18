using System.IO;
using System.Reflection;
using System.Windows.Input;
using CefSharp;
using CefSharp.Wpf;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MarkdownSharp;

namespace KnowledgeBase.ViewModel
{
	/// <summary>
	/// ApplicationViewModel manages the appplications state and its main objects.
	/// </summary>
	public class AppViewModel : ViewModelBase
	{
		#region fields
		public const string TestResourceUrl = "http://test/resource/about";
		public const string TestMarkDown2HTMLConversion = "http://test/resource/markdown";
		public const string TestMarkDownStyleURL = "http://test/resource/github-markdown.css";

		private string markdownStyle;
		private string markdownContent;
		private string markdownHTMLOutput;

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
			mAssemblyTitle = Assembly.GetEntryAssembly().GetName().Name;

			BrowserAddress = AppViewModel.TestResourceUrl;

			RegisterMarkDownContent();

			mTestUrlCommand = new RelayCommand(() =>
			{
				// Setting this address sets the current address of the browser
				// control via bound BrowserAddress property
				BrowserAddress = AppViewModel.TestResourceUrl;
			});

			mTestUrl1Command = new RelayCommand<object>((p) =>
			{
				var browser = p as IWpfWebBrowser;

				if (browser == null)
					return;

				RefreshMarkDownRegistration(browser.ResourceHandlerFactory);

				// Setting this address sets the current address of the browser
				// control via bound BrowserAddress property

				BrowserAddress = AppViewModel.TestMarkDown2HTMLConversion;
			});
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
				return mBrowserAddress;
			}

			set
			{
				if (mBrowserAddress != value)
				{
					mBrowserAddress = value;
					RaisePropertyChanged(() => BrowserAddress);
					RaisePropertyChanged(() => BrowserTitle);
				}
			}
		}

		/// <summary>
		/// Get "title" - "Uri" string of current address of web browser URI
		/// for display in UI - to let the user know what his looking at.
		/// </summary>
		public string BrowserTitle
		{
			get { return string.Format("{0} - {1}", mAssemblyTitle, mBrowserAddress); }
		}

		/// <summary>
		/// Get test Command to browse to a test URL ...
		/// </summary>
		public ICommand TestUrlCommand
		{
			get { return mTestUrlCommand; }
		}

		/// <summary>
		/// Get test Command to browse to a test URL 1 ...
		/// </summary>
		public ICommand TestUrl1Command
		{
			get { return mTestUrl1Command; }
		}
		#endregion properties

		#region methods
		/// <summary>
		/// Registers 2 Test URIs with HTML loaded from strings
		/// </summary>
		/// <param name="browser"></param>
		public void RegisterTestResources(IWebBrowser browser)
		{
			var factory = browser.ResourceHandlerFactory;

			if (factory != null)
			{
				factory.RegisterHandler(TestMarkDownStyleURL, ResourceHandler.FromString(markdownStyle));

				const string responseBody =
				"<html><head><link rel=\"stylesheet\" href=\"github-markdown.css\"></head>"
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

				factory.RegisterHandler(TestResourceUrl, ResourceHandler.FromString(responseBody));

				RefreshMarkDownRegistration(factory);
			}
		}

		/// <summary>
		/// Unrigisters the old markdown registration (if any) and renews
		/// the markdown registration with current content.
		/// 
		/// Source: https://github.com/cefsharp/CefSharp/pull/857
		/// </summary>
		/// <param name="handler"></param>
		public void RefreshMarkDownRegistration(IResourceHandlerFactory handler)
		{
			handler.UnregisterHandler(TestMarkDown2HTMLConversion);

			RegisterMarkDownContent();

			handler.RegisterHandler(TestMarkDown2HTMLConversion, ResourceHandler.FromString(markdownHTMLOutput));
		}

		#region MarkDown Sample Methods
		/// <summary>
		/// Reload the markdown file and register its content at the correct URL.
		/// </summary>
		/// <param name="This"></param>
		/// <returns></returns>
		public bool RegisterMarkDownContent()
		{
			try
			{
				var markDown = new Markdown();

				markdownStyle = AppViewModel.FileContents("SampleData/github-markdown.css");

				markdownContent = AppViewModel.FileContents("SampleData/README.md");
				markdownHTMLOutput = markDown.Transform(markdownContent);

				return true;
			}
			catch (System.Exception)
			{
				return false;
			}
		}

		/// <summary>
		/// returns the root path of the currently executing assembly
		/// 
		/// Source: http://code.google.com/p/markdownsharp/
		/// </summary>
		private static string ExecutingAssemblyPath
		{
			get
			{
				string path = System.Reflection.Assembly.GetExecutingAssembly().Location;

				// removes executable part
				path = Path.GetDirectoryName(path);
				return path;
			}
		}

		/// <summary>
		/// returns the contents of the specified file as a string  
		/// assumes the file is relative to the root of the project
		/// 
		/// Source: http://code.google.com/p/markdownsharp/
		/// </summary>
		private static string FileContents(string filename)
		{
			try
			{
				return File.ReadAllText(Path.Combine(ExecutingAssemblyPath, filename));
			}
			catch (FileNotFoundException)
			{
				return string.Empty;
			}

		}
		#endregion MarkDown Sample Methods
		#endregion methods
	}
}
