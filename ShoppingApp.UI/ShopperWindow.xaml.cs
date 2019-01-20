using ShoppingApp.Core;
using ShoppingApp.Core.Middlemen;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

		public IShoppingListSaver ShoppingListSaver { get; } = New.ShoppingListSaver();
		public IShopperFinder ShopperFinder { get; } = New.ShopperFinder();

		private async void NewList(object sender, RoutedEventArgs e)
			=> await OpenShoppingWindow(new ShoppingListWindow(), true).ConfigureAwait(false);

		private async void EditList(object sender, RoutedEventArgs e)
			=> await OpenShoppingWindow(new ShoppingListWindow(_data.SelectedItem), false).ConfigureAwait(false);

		private async Task OpenShoppingWindow(ShoppingListWindow shoppingListWindow, bool newList)
		{
			await WindowContext.State.WaitUntilChildWindowCloses(shoppingListWindow);
			var shoppingList = shoppingListWindow.GetShoppingList();

			if (newList)
			{
				ShoppingListSaver.AddList(_shopper, shoppingList);
			}
			else
			{
				ShoppingListSaver.EditList(_shopper, shoppingList);
			}

			_data.ShoppingLists = ShopperFinder.GetBy(_shopper.Id).ShoppingLists;
		}
	}
}