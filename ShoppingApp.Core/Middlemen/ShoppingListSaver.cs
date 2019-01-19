using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingApp.Core.Middlemen
{
	internal class ShoppingListSaver : IShoppingListSaver
	{
		public void AddList(Shopper toShopper, ShoppingList shoppingList)
		{
			using (var shopCtx = new ShoppingContext())
			{
				var shopper = FindShopper(shopCtx, toShopper);

				shopper.ShoppingLists.Add(shoppingList);

				shopCtx.SaveChanges();
			}
		}

		public void EditList(Shopper toShopper, ShoppingList shoppingList) => throw new NotImplementedException();

		private static Shopper FindShopper(ShoppingContext shopCtx, Shopper finding)
			=> shopCtx.Shoppers
				.Where(x => x.Id == finding.Id)
				.IncludeShopperItems()
				.FirstOrDefault();
	}
}
