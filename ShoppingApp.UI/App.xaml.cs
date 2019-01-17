using ShoppingApp.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ShoppingApp.UI
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			// seed db
			using (var shopCtx = new ShoppingContext())
			{
				foreach(var shp in shopCtx.Shoppers)
				{
					string a = $"{shp.Email}, {shp.Username}, {shp.PasswordHash}";
					bool b = shp.Username == "sj";
					bool c = shp.PasswordHash == "1234".Hash("sj");
					bool[] cmp = new bool[shp.PasswordHash.Length];

					for (int psH = 0; psH < shp.PasswordHash.Length; psH++)
					{
						cmp[psH] = shp.PasswordHash[psH] == "1234".Hash("sj")[psH];
					}

					Console.WriteLine();
				}

				if (!shopCtx.Shoppers.Any(shopper => shopper.Username == "sj"))
				{
					shopCtx.Shoppers.Add
					(
						new Core.Shopper
						{
							Balance = 1337L,
							Email = "jg@g.c",
							PasswordHash = "1234".Hash("sj"),
							Username = "sj",
							SendEmails = true,
							ShoppingLists = new List<ShoppingList>
							{
							new ShoppingList
							{
								Name = "shop l ist UNO",
								Items = new List<ShoppingItem>
								{
									new ShoppingItem
									{
										AmountBought = 1,
										Name = "drUgs",
										Price = 1337,
										Quantity = 72
									}
								}
							}
							}
						}
					);

					shopCtx.SaveChanges();
				}
			}
		}
	}
}
