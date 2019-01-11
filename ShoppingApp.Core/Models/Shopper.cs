using System;
using System.Collections.Generic;

namespace ShoppingApp.Core
{
	public class Shopper : DataItem
	{
		public string Username { get; set; }

		public long Balance { get; set; }

		public string Email { get; set; }

		public bool SendEmails { get; set; }

		public byte[] PasswordHash { get; set; }

		public DateTime RegisterDate { get; set; }

		public virtual ICollection<ShoppingList> ShoppingLists { get; set; }
	}
}