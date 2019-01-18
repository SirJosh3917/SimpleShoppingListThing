using ShoppingApp.Core;

using System.Collections.Generic;
using System.Linq;
using System.Windows;

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
				ShoppingLists = _shopper.ShoppingLists.ToList();
			}

			private ICollection<ShoppingList> _shoppingLists;

			public ICollection<ShoppingList> ShoppingLists
			{
				get => _shoppingLists;
				set => SetField(ref _shoppingLists, value);
			}

			private bool _anythingSelected;

			public bool AnythingSelected
			{
				get => _anythingSelected;
				set => SetField(ref _anythingSelected, value);
			}

			private ShoppingList _selectedItem;

			public ShoppingList SelectedItem
			{
				get => _selectedItem;
				set
				{
					_selectedItem = value;

					AnythingSelected = _selectedItem != null;
				}
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

		private void NewList(object sender, RoutedEventArgs e)
			=> WindowContext.State.OpenChildWindow(new ShoppingListWindow());

		private void EditList(object sender, RoutedEventArgs e)
			=> WindowContext.State.OpenChildWindow(new ShoppingListWindow(_data.SelectedItem));
	}
}