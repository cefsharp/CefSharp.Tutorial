using System.Reflection;
using System.Windows.Input;
using CefSharp;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace KnowledgeBase.ViewModels
{
    /// <summary>
    /// AppViewModel manages the appplications state and its main objects.
    /// </summary>
    public class AppViewModel : ViewModelBase
    {
        #region fields

        public const string TestResourceUrl = "http://test/resource/load";
        public const string TestUnicodeResourceUrl = "http://test/resource/loadUnicode";


        private readonly string mAssemblyTitle;
        private string mBrowserAddress;

        /// <summary>
        /// Get test Command to browse to a test URL ...
        /// </summary>
        public ICommand TestUrlCommand { get; private set; }

        /// <summary>
        /// Get test Command to browse to a test URL 1 ...
        /// </summary>
        public ICommand TestUrl1Command { get; private set; }

        #endregion fields

        #region Constructor

        /// <summary>
        /// AppViewModel Constructor
        /// </summary>
        public AppViewModel()
        {
            mAssemblyTitle = Assembly.GetEntryAssembly().GetName().Name;

            BrowserAddress = TestResourceUrl;

            TestUrlCommand = new RelayCommand(() =>
            {
                // Setting this address sets the current address of the browser
                // control via bound BrowserAddress property
                BrowserAddress = TestResourceUrl;
            });

            TestUrl1Command = new RelayCommand(() =>
            {
                // Setting this address sets the current address of the browser
                // control via bound BrowserAddress property
                BrowserAddress = TestUnicodeResourceUrl;
            });
        }

        #endregion Constructor

        #region properties

        /// <summary>
        /// Get/set current address of web browser URI.
        /// </summary>
        public string BrowserAddress
        {
            get { return mBrowserAddress; }

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

        #endregion properties

        #region methods

        /// <summary>
        /// Registers 2 Test URIs with HTML loaded from strings
        /// </summary>
        /// <param name="browser"></param>
        public static void RegisterTestResources(IWebBrowser browser)
        {
            var factory = browser.ResourceHandlerFactory;

            if (factory != null)
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

                factory.RegisterHandler(TestResourceUrl, ResourceHandler.FromString(responseBody));

                const string unicodeResponseBody = "<html><body>整体满意度</body></html>";
                factory.RegisterHandler(TestUnicodeResourceUrl, ResourceHandler.FromString(unicodeResponseBody));
            }
        }

        #endregion methods
    }
}