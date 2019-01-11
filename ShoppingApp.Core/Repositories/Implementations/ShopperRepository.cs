using Microsoft.EntityFrameworkCore;
using ShoppingApp.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

namespace ShoppingApp.Core.Repositories.Implementations
{
	// TODO: refactor
	public static class HashingPasswordExtension
	{
		public static byte[] Hash(this string str, string salt)
		{
			using (var sha256 = new SHA256Managed())
			{
				var hashing = str.Salt(salt);

				return sha256.ComputeHash(hashing.GetBytes());
			}
		}

		public static byte[] GetBytes(this string str) => Encoding.UTF8.GetBytes(str);

		// TODO: make more advanced BEFORE PUTTING INTO PROD
		public static string Salt(this string str, string salt) => $"{salt}{str}{salt}";
	}

	public class ShopperRepository : IShopperRepository
	{
		public void Register(IRegistrationInformation registrationInformation)
		{
			var shopper = new Shopper
			{
				Balance = 0,
				Username = registrationInformation.Username,
				Email = registrationInformation.Email,
				PasswordHash = registrationInformation.Password.Hash(registrationInformation.Username),
				RegisterDate = DateTime.UtcNow,
				SendEmails = registrationInformation.SendEmails,
			};

			using (var ctx = new ShoppingContext())
			{
				ctx.Shoppers.Add(shopper);

				ctx.SaveChanges();
			}
		}

		// TODO: refactor both of these to not have redundant code
		public bool PasswordMatches(IUsernameLoginInformation usernameLogin)
		{
			return PasswordMatches
			(
				FindByUsername(usernameLogin.Username),
				usernameLogin.Password.Salt(usernameLogin.Password)
			);
		}

		public bool PasswordMatches(IEmailLoginInformation emailLogin)
		{
			return PasswordMatches
			(
				FindByEmail(emailLogin.Email),
				emailLogin.Password.Salt(emailLogin.Password)
			);
		}

		private bool PasswordMatches(Shopper shopper, string saltedPassword)
			=> shopper.PasswordHash == saltedPassword.GetBytes();

		// TODO: refactor to not have redundancies
		public Shopper FindByUsername(string username) => FindFirstOrDefault(x => x.Username == username);

		public Shopper FindByEmail(string email) => FindFirstOrDefault(x => x.Email == email);

		private Shopper FindFirstOrDefault(Expression<Func<Shopper, bool>> predicate)
		{
			using (var ctx = new ShoppingContext())
			{
				return GetShopper(ctx, predicate);
			}
		}

		public void AddListTo(ShoppingList shoppingList, Shopper toShopper)
		{
			using (var ctx = new ShoppingContext())

			{
				var shopper = GetShopper(ctx, x => x.Id == toShopper.Id);

				shopper.ShoppingLists.Add(shoppingList);

				ctx.SaveChanges();
			}
		}

		private static Shopper GetShopper(ShoppingContext ctx, Expression<Func<Shopper, bool>> predicate)
			=> ctx.Shoppers
			.Include(shopper => shopper.ShoppingLists)
				.ThenInclude(list => list.Items)
			.FirstOrDefault(predicate);
	}

	public class RegistrationInformation : IRegistrationInformation, IEmailLoginInformation, IUsernameLoginInformation
	{
		public RegistrationInformation(bool sendEmails, string username, string email, string password)
		{
			SendEmails = sendEmails;
			Username = username;
			Email = email;
			Password = password;
		}

		public bool SendEmails { get; }

		public string Username { get; }

		public string Email { get; }

		public string Password { get; }
	}

	public class UsernameLoginInformation : IUsernameLoginInformation
	{
		public UsernameLoginInformation(string username, string password)
		{
			Username = username;
			Password = password;
		}

		public string Username { get; }

		public string Password { get; }
	}

	public class EmailLoginInformation : IEmailLoginInformation
	{
		public EmailLoginInformation(string email, string password)
		{
			Email = email;
			Password = password;
		}

		public string Email { get; }

		public string Password { get; }
	}
}
