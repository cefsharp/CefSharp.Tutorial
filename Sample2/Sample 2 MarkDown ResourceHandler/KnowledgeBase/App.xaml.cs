namespace KnowledgeBase
{
	using System.Windows;
	using CefSharp;
	using KnowledgeBase.ViewModel;

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			Cef.Initialize();
		}

		/// <summary>
		/// This is the first bit of code being executed when the application is invoked (main entry point).
		/// 
		/// Use the <paramref name="e"/> parameter to evaluate command line options.
		/// Invoking a program with an associated file type extension (eg.: *.txt) in Explorer
		/// results, for example, in executing this function with the path and filename being
		/// supplied in <paramref name="e"/>.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			var mainWindow = new MainWindow();
			var viewModel = new AppViewModel();

			viewModel.RegisterTestResources(mainWindow.browser);

			mainWindow.DataContext = viewModel;

			mainWindow.Show();
		}
	}
}
