using ShoppingApp.Core;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ShoppingApp.UI
{
	/// <summary>
	/// Interaction logic for ShoppingListWindow.xaml
	/// </summary>
	public partial class ShoppingListWindow : Window
	{
		private class ShoppingListWindowBindingSource : BindingSource
		{
			private readonly ShoppingList _input;

			public ShoppingListWindowBindingSource(ShoppingList input)
			{
				_input = input ?? new ShoppingList
				{
					Name = "Untitled List",
					Items = new List<ShoppingItem>()
				};

				_shoppingItems = new ObservableCollection<ShoppingItem>(_input.Items);
			}

			public ShoppingList GetShoppingList() => _input;

			public string WindowTitle => $"{_input.Name} - {_input.CreationDate}";

			public string Title
			{
				get => _input.Name;
				set
				{
					_input.Name = value;
					InvokePropertyChanged();
					InvokePropertyChanged(nameof(WindowTitle));
				}
			}

			private ObservableCollection<ShoppingItem> _shoppingItems;

			public ObservableCollection<ShoppingItem> ShoppingItems
			{
				get => _shoppingItems;
				set => SetField(ref _shoppingItems, value);
			}
		}

		private readonly ShoppingList _shoppingList;
		private readonly ShoppingListWindowBindingSource _data;

		public ShoppingListWindow() : this(null)
		{
		}

		public ShoppingList GetShoppingList()
		{
			var shoppingList = _data.GetShoppingList();

			var shoppingItems = _data.ShoppingItems;

			// set the items in the list to the items set by the user
			shoppingList.Items = shoppingItems;

			return shoppingList;
		}

		public ShoppingListWindow(ShoppingList shoppingList)
		{
			_shoppingList = shoppingList;

			DataContext = _data = new ShoppingListWindowBindingSource(_shoppingList);
			InitializeComponent();
		}
	}
}