using KnowledgeBase.ViewModels;

namespace KnowledgeBase
{
	using System;
	using System.Windows;
	using System.Windows.Threading;
	using CefSharp;

    /// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		#region contructors
		/// <summary>
		/// Class Constructor
		/// </summary>
		public MainWindow()
		{
			this.InitializeComponent();

			AppViewModel.RegisterTestResources(this.browser);

			DataContext = new AppViewModel();

			browser.StatusMessage += BrowserStatusMessage;
			browser.NavStateChanged += BrowserNavStateChanged;
		}

		/// <summary>
		/// Is called on change of browser load state and notifies the statusbar
		/// to say 'Loading...' or 'Loading done.'
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BrowserNavStateChanged(object sender, NavStateChangedEventArgs e)
		{
			// Do this on the UI thread since it otherwise throws an exception ...
			Dispatcher.BeginInvoke
			(
				new Action(() =>
				{
					Status.Text = e.CanReload ? "Loading done." : "Loading...";
				}
			), DispatcherPriority.Background);
		}

		private void BrowserStatusMessage(object sender, StatusMessageEventArgs e)
		{
			// Do this on the UI thread since it otherwise throws an exception ...
			Dispatcher.BeginInvoke
			(
				new Action(() =>
				{
					Status.Text = e.Value;
				}
			), DispatcherPriority.Background );
		}
		#endregion contructors
	}
}
