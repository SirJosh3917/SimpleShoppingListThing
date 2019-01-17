using ShoppingApp.Core.Middlemen;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace ShoppingApp.UI
{
	/// <summary>
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		private class LoginWindowBindingSource : BindingSource
		{
			private string _usernameOrEmail;
			public string UsernameOrEmail
			{
				get => _usernameOrEmail;
				set => SetField(ref _usernameOrEmail, value);
			}

			private string _password;
			public string Password
			{
				get => _password;
				set => SetField(ref _password, value);
			}
		}

		private readonly LoginWindowBindingSource _data;

		public LoginWindow()
		{
			DataContext = _data = new LoginWindowBindingSource();
			InitializeComponent();
		}

		public IUserValidator UserValidator { get; } = New.UserValidator();

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var usernameOrEmail = _data.UsernameOrEmail;
			var password = _data.Password;

			if (UserValidator.IsValid(usernameOrEmail, password))
			{
				var shopper = UserValidator.FetchUser(usernameOrEmail, password);

				WindowContext.State.ChangeWindow(new ShopperWindow(shopper));
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