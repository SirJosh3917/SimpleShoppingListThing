using ShoppingApp.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShoppingApp.UI
{
	/// <summary>
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		private readonly SampleBindingSource _data;

		public LoginWindow()
		{
			_data = new SampleBindingSource();
			DataContext = _data;
			InitializeComponent();
		}

		public IUserValidator UserValidator { get; } = New.UserValidator();

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			if (UserValidator.IsValid(_data.UsernameOrEmail, _data.Password))
			{
				WindowContext.State.ChangeWindow(new ShopperWindow());
			}
			else
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

	public class SampleBindingSource
	{
		public string UsernameOrEmail { get; set; }
		public string Password { get; set; }
	}
}
