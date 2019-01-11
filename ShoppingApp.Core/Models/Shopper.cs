using System;
using System.Collections.Generic;

namespace ShoppingApp.Core
{
	public class Shopper : DataItem
	{
		public string Username { get; set; }

		public long Balance { get; set; } = 0L;

		public string Email { get; set; }

		public bool SendEmails { get; set; }

		public byte[] PasswordHash { get; set; }

		public DateTime RegisterDate { get; set; } = DateTime.Now;

		public virtual ICollection<ShoppingList> ShoppingLists { get; set; }
	}
}