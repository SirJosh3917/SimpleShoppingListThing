namespace ShoppingApp.Core.Middlemen
{
	public static class New
	{
		public static IUserValidator UserValidator() => new UserValidator();

		public static IShoppingListSaver ShoppingListSaver() => new ShoppingListSaver();

		public static IUserRegistration UserRegistration() => new UserRegistration();

		public static IShopperFinder ShopperFinder() => new ShopperFinder();
	}
}