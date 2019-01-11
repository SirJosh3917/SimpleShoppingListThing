using Microsoft.EntityFrameworkCore;
using ShoppingApp.Core;
using System;

namespace ShoppingApp.Data
{
	public class ShoppingContext : DbContext
	{
		public ShoppingContext()
		{
			Database.EnsureCreated();
		}

		public DbSet<Shopper> Shoppers { get; set; }
		public DbSet<ShoppingList> ShoppingLists { get; set; }
		public DbSet<ShoppingItem> ShoppingItems { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Filename=blogging.db");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Shopper>
			(
				entity =>
				{
					entity.HasKey(e => e.Id);
					entity.Property(e => e.Id)
						.ValueGeneratedOnAdd();
				}
			);

			modelBuilder.Entity<ShoppingItem>
			(
				entity =>
				{
					entity.HasKey(e => e.Id);
					entity.Property(e => e.Id)
						.ValueGeneratedOnAdd();
				}
			);

			modelBuilder.Entity<ShoppingList>
			(
				entity =>
				{
					entity.HasKey(e => e.Id);
					entity.Property(e => e.Id)
						.ValueGeneratedOnAdd();
				}
			);
		}
	}
}
