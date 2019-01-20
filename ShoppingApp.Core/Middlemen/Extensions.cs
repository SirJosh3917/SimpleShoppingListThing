using System.Linq;

namespace ShoppingApp.Core.Middlemen
{
	public static class Extensions
	{
		// TODO: don't copy and paste so much code
		public static IQueryable<Shopper> MakeQuery(this ShoppingContext shopCtx, string username, string password)
			=> shopCtx.Shoppers
					.Where(x => x.Username == username || x.Email == username)
					.Where(x => x.PasswordHash.Equivalent(password.Hash(x.Username)))
					.IncludeShopperItems();

		public static IQueryable<Shopper> MakeQuery(this ShoppingContext shopCtx, string username, byte[] passwordHash)
			=> shopCtx.Shoppers
					.Where(x => x.Username == username || x.Email == username)
					.Where(x => x.PasswordHash.Equivalent(passwordHash))
					.IncludeShopperItems();

		public static Shopper FindShopper(this ShoppingContext shopCtx, int shopperId)
			=> shopCtx.Shoppers
				.Where(x => x.Id == shopperId)
				.IncludeShopperItems()
				.FirstOrDefault();
	}
}