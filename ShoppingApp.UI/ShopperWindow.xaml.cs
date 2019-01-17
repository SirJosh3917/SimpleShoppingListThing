using ShoppingApp.Core;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace ShoppingApp.UI
{
	/// <summary>
	/// Interaction logic for Shopper.xaml
	/// </summary>
	public partial class ShopperWindow : Window
	{
		private class ShopperWindowBindingSource : BindingSource
		{
			private readonly Shopper _shopper;

			public ShopperWindowBindingSource(Shopper shopper)
			{
				_shopper = shopper;
				ShoppingLists = new ObservableCollection<ShoppingList>(_shopper.ShoppingLists.ToList());
			}

			private ObservableCollection<ShoppingList> _shoppingLists;
			public ObservableCollection<ShoppingList> ShoppingLists
			{
				get => _shoppingLists;
				set => SetField(ref _shoppingLists, value);
			}
		}

		private readonly ShopperWindowBindingSource _data;
		private readonly Shopper _shopper;

		public ShopperWindow(Shopper shopper)
		{
			_shopper = shopper;
			DataContext = _data = new ShopperWindowBindingSource(shopper);
			InitializeComponent();
		}
	}
}