namespace KnowledgeBase
{
	using System;
	using System.Windows;
	using System.Windows.Threading;
	using CefSharp;
	using KnowledgeBase.ViewModel;

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

			this.DataContext = new AppViewModel();

			browser.StatusMessage += browser_StatusMessage;
			browser.NavStateChanged += browser_NavStateChanged;
		}

		/// <summary>
		/// Is called on change of browser load state and notifies the statusbar
		/// to say 'Loading...' or 'Loading done.'
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void browser_NavStateChanged(object sender, NavStateChangedEventArgs e)
		{
			if (e == null)
				return;

			if (e.CanReload == false)
			{
				// Do this on the UI thread since it otherwise throws an exception ...
				Application.Current.Dispatcher.BeginInvoke
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
				Application.Current.Dispatcher.BeginInvoke
				(
					new Action(() =>
					{
						this.Status.Text = "Loading done.";
					}
				), DispatcherPriority.Background);
			}
		}

		void browser_StatusMessage(object sender, StatusMessageEventArgs e)
		{
			// Do this on the UI thread since it otherwise throws an exception ...
			Application.Current.Dispatcher.BeginInvoke
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
