using System;
using System.Collections.Generic;
using System.Text;

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
