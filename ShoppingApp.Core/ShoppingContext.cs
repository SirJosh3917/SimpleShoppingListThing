using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query;

using System.Collections.Generic;
using System.Linq;

namespace ShoppingApp.Core
{
	public class ShoppingContext : DbContext
	{
		public ShoppingContext() => Database.EnsureCreated();

		public DbSet<Shopper> Shoppers { get; set; }
		public DbSet<ShoppingList> ShoppingLists { get; set; }
		public DbSet<ShoppingItem> ShoppingItems { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			=> optionsBuilder.UseSqlite("Filename=shop.db");

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Shopper>(entity =>
			{
				entity.MapDataItem();
			});

			modelBuilder.Entity<ShoppingItem>(entity =>
			{
				entity.MapDataItem();
			});

			modelBuilder.Entity<ShoppingList>(entity =>
			{
				entity.MapDataItem();
			});
		}
	}

	public static class Helpers
	{
		public static void MapDataItem<T>(this EntityTypeBuilder<T> dataItem)
			where T : DataItem
		{
			dataItem.HasKey(e => e.Id);
			dataItem.Property(e => e.Id);
			// .ValueGeneratedOnAdd();
		}

		public static IIncludableQueryable<Shopper, ICollection<ShoppingItem>> IncludeShopperItems(this IQueryable<Shopper> shopperQuery)
			=> shopperQuery.Include(x => x.ShoppingLists)
					.ThenInclude(x => x.Items);
	}
}