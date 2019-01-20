namespace ShoppingApp.Core.Middlemen
{
	internal class ShopperFinder : IShopperFinder
	{
		public Shopper GetBy(int id)
		{
			using (var shopCtx = new ShoppingContext())
			{
				return shopCtx.FindShopper(id);
			}
		}
	}
}