namespace ShoppingApp.Core.Middlemen
{
	public interface IShoppingListSaver
	{
		void AddList(Shopper toShopper, ShoppingList shoppingList);

		void EditList(Shopper toShopper, ShoppingList shoppingList);
	}
}