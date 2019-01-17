using ShoppingApp.Core;
using System;
using System.Linq;

namespace ShoppingApp.Logic
{
	public static class New
	{
		public static IUserValidator UserValidator() => new UserValidator();
	}

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
					.Where(x => x.PasswordHash.Equivalent(password.Hash(x.Username)));
	}

	public interface IUserValidator
	{
		bool IsValid(string username, string password);

		Shopper FetchUser(string username, string password);
	}
}
