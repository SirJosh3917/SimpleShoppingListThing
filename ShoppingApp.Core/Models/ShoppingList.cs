using System;
using System.Collections.Generic;

namespace ShoppingApp.Core
{
	public class ShoppingList : DataItem
	{
		public string Name { get; set; }

		public DateTime CreationDate { get; set; } = DateTime.UtcNow;

		public ICollection<ShoppingItem> Items { get; set; }
	}
}