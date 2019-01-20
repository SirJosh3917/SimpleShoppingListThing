using System.Linq;

namespace ShoppingApp.Core.Middlemen
{
	internal class UserValidator : IUserValidator
	{
		public bool IsValid(string username, string password)
		{
			using (var shopCtx = new ShoppingContext())
			{
				return shopCtx.MakeQuery(username, password)
						.Any();
			}
		}

		public Shopper FetchUser(string username, string password)
		{
			using (var shopCtx = new ShoppingContext())
			{
				return shopCtx.MakeQuery(username, password)
					.First();
			}
		}
	}
}