using System.Windows;

namespace ShoppingApp.UI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow() => InitializeComponent();

		private void LoginWindow(object sender, RoutedEventArgs e)
			=> WindowContext.State.OpenChildWindow(new LoginWindow());

		private void RegisterWindow(object sender, RoutedEventArgs e)
			=> WindowContext.State.OpenChildWindow(new RegisterWindow());
	}
}