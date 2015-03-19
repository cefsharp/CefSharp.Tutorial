namespace KnowledgeBase
{
	using System.Windows;
	using CefSharp;

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			Cef.Initialize();
		}
	}
}
