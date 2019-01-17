using ShoppingApp.Core;

using System;

namespace ShoppingApp.Demo
{
	internal class Program
	{
		private static void Main(string[] args)
		{/*
			IShopperRepository repo = new ShopperRepository();

			var johan = new RegistrationInformation(false, "Johan Kunt", "johankunt@gmail.com", "password1");
			var millene = new RegistrationInformation(true, "Millene Want", "millenewantagagon@yahoo.com", "password1");

			repo.Register(johan);
			repo.Register(millene);

			var list = new ShoppingList
			{
				Name = "Untitled-1",
				Items = new ShoppingItem[]
				{
					new ShoppingItem
					{
						Name = "chair",
						Price = 399L
					},
					new ShoppingItem
					{
						Name = "paper",
						Price = 9L
					},
					new ShoppingItem
					{
						Name = "i should use decimals instead of longs",
						Price = 1L
					}
				}
			};

			Console.WriteLine(repo.PasswordMatches(millene as IEmailLoginInformation));
			Console.WriteLine(repo.PasswordMatches(johan as IUsernameLoginInformation));

			var shopper = repo.FindByEmail("johankunt@gmail.com");

			repo.AddListTo(list, shopper);

			Console.WriteLine(shopper.RegisterDate);

			foreach (var lists in repo.FindByUsername(johan.Username).ShoppingLists)
			{
				foreach (var item in lists.Items)
				{
					Console.WriteLine($"{item.Name} - {item.Price}");
				}
			}
			*/
			Console.WriteLine("Done");
			Console.ReadLine();
		}
	}
}