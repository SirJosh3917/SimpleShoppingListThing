namespace ShoppingApp.Core.Middlemen
{
	public interface IShopperFinder
	{
		Shopper GetBy(int id);
	}
}