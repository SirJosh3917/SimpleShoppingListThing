namespace ShoppingApp.Core.Middlemen
{
	public static class New
	{
		public static IUserValidator UserValidator() => new UserValidator();
		public static IShoppingListSaver ShoppingListSaver() => new ShoppingListSaver();
	}
}