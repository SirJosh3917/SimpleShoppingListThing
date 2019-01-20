using System.Linq;

namespace ShoppingApp.Core.Middlemen
{
	internal class ShoppingListSaver : IShoppingListSaver
	{
		public void AddList(Shopper toShopper, ShoppingList shoppingList)
		{
			using (var shopCtx = new ShoppingContext())
			{
				var shopper = shopCtx.FindShopper(toShopper.Id);

				shopCtx.Attach(shoppingList);

				shopper.ShoppingLists.Add(shoppingList);

				shopCtx.SaveChanges();
			}
		}

		public void EditList(Shopper toShopper, ShoppingList shoppingList)
		{
			using (var shopCtx = new ShoppingContext())
			{
				var shopper = shopCtx.FindShopper(toShopper.Id);

				// replace existing shopping list
				shopCtx.ShoppingLists.Remove
				(
					shopper.ShoppingLists.First(x => x.Id == shoppingList.Id)
				);

				shopper.ShoppingLists.Add(shoppingList);

				// replace every old item
				foreach (var item in shoppingList.Items)
				{
					if (item.Id != 0)
					{
						shopCtx.ShoppingItems.Remove
						(
							shopCtx.ShoppingItems.First(x => x.Id == item.Id)
						);

						shopCtx.ShoppingItems.Add(item);
					}
				}

				shopCtx.SaveChanges();
			}
		}
	}
}