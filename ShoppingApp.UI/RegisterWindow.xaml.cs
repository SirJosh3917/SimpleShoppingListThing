using ShoppingApp.Core;
using ShoppingApp.Core.Middlemen;

using System.Windows;

namespace ShoppingApp.UI
{
	// TODO: RegisterWindow is just a copied & pasted LoginWindow with a different button click,
	// in the future when i'm not lazy i can probably remove this code duplication

	/// <summary>
	/// Interaction logic for RegisterWindow.xaml
	/// </summary>
	public partial class RegisterWindow : Window
	{
		private class RegisterWindowBindingSource : BindingSource
		{
			private string _username;

			public string Username
			{
				get => _username;
				set => SetField(ref _username, value);
			}

			private string _email;

			public string Email
			{
				get => _email;
				set => SetField(ref _email, value);
			}

			private string _password;

			public string Password
			{
				get => _password;
				set => SetField(ref _password, value);
			}

			private bool _sendEmails;

			public bool SendEmails
			{
				get => _sendEmails;
				set => SetField(ref _sendEmails, value);
			}
		}

		private readonly RegisterWindowBindingSource _data;

		public RegisterWindow()
		{
			DataContext = _data = new RegisterWindowBindingSource();
			InitializeComponent();
		}

		public IUserRegistration UserRegistration { get; } = New.UserRegistration();

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var regResult = UserRegistration.Register
			(
				new Shopper
				{
					Email = _data.Email,
					Username = _data.Username,
					PasswordHash = _data.Password.Hash(_data.Username),
					SendEmails = _data.SendEmails
				}
			);

			WindowContext.State.ChangeWindow(new ShopperWindow(regResult));
		}
	}
}