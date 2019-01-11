using Microsoft.EntityFrameworkCore;
using ShoppingApp.Core;
using ShoppingApp.Core.Repositories.Implementations;
using ShoppingApp.Core.Repositories.Interfaces;
using System;
using System.Linq;

namespace ShoppingApp.Demo
{
	class Program
	{
		static void Main(string[] args)
		{
			IShopperRepository repo = new ShopperRepository();

			var johan = new RegistrationInformation(false, "Johan Kunt", "johankunt@gmail.com", "password1");
			var millene = new RegistrationInformation(true, "Millene Want", "millenewantagagon@yahoo.com", "password1");

			repo.Register(johan);
			repo.Register(millene);

			IShoppingListRepository list = new ShoppingListRepository("Untitled-1");
			list
				.Add(new ShoppingItem
				{
					Name = "chair",
					Price = 399L
				})
				.Add(new ShoppingItem
				{
					Name = "paper",
					Price = 9L
				})
				.Add(new ShoppingItem
				{
					Name = "i should use decimals instead of longs",
					Price = 1L
				});
			
			Console.WriteLine(repo.PasswordMatches(millene as IEmailLoginInformation));
			Console.WriteLine(repo.PasswordMatches(johan as IUsernameLoginInformation));

			var shopper = repo.FindByEmail("johankunt@gmail.com");

			repo.AddListTo(list.ToList(), shopper);

			Console.WriteLine(shopper.RegisterDate);

			foreach(var lists in repo.FindByUsername(johan.Username).ShoppingLists)
			{
				foreach(var item in lists.Items)
				{
					Console.WriteLine($"{item.Name} - {item.Price}");
				}
			}

			Console.WriteLine("Done");
			Console.ReadLine();
		}
	}
}
