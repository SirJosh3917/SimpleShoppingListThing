namespace ShoppingApp.Core
{
	public class ShoppingItem : DataItem
	{
		public int Quantity { get; set; } = 1;

		public int AmountBought { get; set; } = 0;

		public string Name { get; set; }

		public long Price { get; set; }
	}
}