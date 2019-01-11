using System;

namespace ShoppingApp.Core.Repositories.Interfaces
{
	public interface IShoppingListRepository
	{
		string Name { get; }

		DateTime Creation { get; }

		void AddItem(ShoppingItem item);

		ShoppingList ToList();
	}
}