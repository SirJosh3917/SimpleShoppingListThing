using ShoppingApp.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingApp.Core.Repositories.Implementations
{
	public class ShoppingListRepository : IShoppingListRepository
	{
		private readonly List<ShoppingItem> _shoppingItems = new List<ShoppingItem>();

		public ShoppingListRepository(string name)
		{
			Name = name;
			Creation = DateTime.UtcNow;
		}

		public string Name { get; }

		public DateTime Creation { get; }

		public void AddItem(ShoppingItem item) => _shoppingItems.Add(item);

		public ShoppingList ToList()
			=> new ShoppingList
			{
				CreationDate = Creation,
				Items = _shoppingItems,
				Name = Name
			};
	}

	public static class ShoppingListRepositoryExtensions
	{
		public static IShoppingListRepository Add(this IShoppingListRepository shoppingList, ShoppingItem add)
		{
			shoppingList.AddItem(add);
			return shoppingList;
		}
	}
}
