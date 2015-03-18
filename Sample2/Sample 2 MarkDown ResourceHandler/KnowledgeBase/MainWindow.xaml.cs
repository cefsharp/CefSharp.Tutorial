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
		}

		public void RegisterBrowserEvents()
		{
			try
			{
				this.browser.StatusMessage += this.browser_StatusMessage;
				this.browser.NavStateChanged += this.browser_NavStateChanged;
			}
			catch (Exception)
			{
			}
		}

		/// <summary>
		/// Is called on change of browser load state and notifies the statusbar
		/// to say 'Loading...' or 'Loading done.'
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void browser_NavStateChanged(object sender, NavStateChangedEventArgs e)
		{
			if (e == null)
				return;

			if (e.CanReload == false)
			{
				// Do this on the UI thread since it otherwise throws an exception ...
				Dispatcher.BeginInvoke
				(
					new Action(() =>
					{
						this.Status.Text = "Loading...";
					}
				), DispatcherPriority.Background);
			}
			else
			{
				// Do this on the UI thread since it otherwise throws an exception ...
				Dispatcher.BeginInvoke
				(
					new Action(() =>
					{
						this.Status.Text = "Loading done.";
					}
				), DispatcherPriority.Background);
			}
		}

		/// <summary>
		/// Is invoked whenever the browser control updates its status.
		/// Result is displayed in status TextBlock control of Mainwindow.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void browser_StatusMessage(object sender, StatusMessageEventArgs e)
		{
			// Do this on the UI thread since it otherwise throws an exception ...
			Dispatcher.BeginInvoke
			(
				new Action(() =>
				{
					this.Status.Text = e.Value;
				}
			), DispatcherPriority.Background );
		}
		#endregion contructors
	}
}
