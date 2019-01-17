using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ShoppingApp.Core.Middlemen
{
	internal class UserValidator : IUserValidator
	{
		public bool IsValid(string username, string password)
		{
			using (var shopCtx = new ShoppingContext())
			{
				return MakeQuery(shopCtx, username, password)
						.Any();
			}
		}

		public Shopper FetchUser(string username, string password)
		{
			using (var shopCtx = new ShoppingContext())
			{
				return MakeQuery(shopCtx, username, password)
					.First();
			}
		}

		private IQueryable<Shopper> MakeQuery(ShoppingContext shopCtx, string username, string password)
			=> shopCtx.Shoppers
					.Where(x => x.Username == username || x.Email == username)
					.Where(x => x.PasswordHash.Equivalent(password.Hash(x.Username)))
					.Include(x => x.ShoppingLists)
						.ThenInclude(x => x.Items);
	}
}