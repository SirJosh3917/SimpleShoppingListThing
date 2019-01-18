using ShoppingApp.Core;

using System.Collections.Generic;
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
			private readonly List<ShoppingItem> _shoppingItems;
			private readonly ShoppingList _input;

			public ShoppingListWindowBindingSource(ShoppingList input)
			{
				_shoppingItems = new List<ShoppingItem>();

				_input = input ?? new ShoppingList
				{
					Name = "Untitled List",
					Items = _shoppingItems
				};

				Title = $"{_input.Name} - {_input.CreationDate}";
			}

			private string _title;

			public string Title
			{
				get => _title;
				set => SetField(ref _title, value);
			}
		}

		private readonly ShoppingList _shoppingList;
		private readonly ShoppingListWindowBindingSource _data;

		public ShoppingListWindow() : this(null)
		{
		}

		public ShoppingListWindow(ShoppingList shoppingList)
		{
			_shoppingList = shoppingList;

			DataContext = _data = new ShoppingListWindowBindingSource(_shoppingList);
			InitializeComponent();
		}
	}
}