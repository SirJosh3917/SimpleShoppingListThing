using System.Linq;

namespace ShoppingApp.Core.Middlemen
{
	internal class UserRegistration : IUserRegistration
	{
		public Shopper Register(Shopper information)
		{
			using (var shopCtx = new ShoppingContext())
			{
				shopCtx.Shoppers.Add(information);

				shopCtx.SaveChanges();

				return shopCtx.MakeQuery(information.Username, information.PasswordHash)
					.First();
			}
		}
	}
}