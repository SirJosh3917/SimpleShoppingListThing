using ShoppingApp.Core.Middlemen;
using System.Windows;

namespace ShoppingApp.UI
{
	/// <summary>
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		private class LoginWindowBindingSource
		{
			public string UsernameOrEmail { get; set; }
			public string Password { get; set; }
		}

		private readonly LoginWindowBindingSource _data;

		public LoginWindow()
		{
			_data = new LoginWindowBindingSource();
			DataContext = _data;
			InitializeComponent();
		}

		public IUserValidator UserValidator { get; } = New.UserValidator();

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (UserValidator.IsValid(_data.UsernameOrEmail, _data.Password))
			{
				WindowContext.State.ChangeWindow(new ShopperWindow());
			}
			else
			{
				ShowInvalidCredentialsMessage();
			}
		}

		private static void ShowInvalidCredentialsMessage()
		{
			MessageBox.Show
			(
				"Invalid Credentials",
				"The credentials you specified are invalid.",
				MessageBoxButton.OK,
				MessageBoxImage.Warning,
				MessageBoxResult.OK
			);
		}
	}
}