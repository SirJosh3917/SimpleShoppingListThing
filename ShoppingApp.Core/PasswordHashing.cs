using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ShoppingApp.Core
{
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

		public static bool Equivalent(this byte[] byteArray, byte[] otherByteArray)
		{
			if (byteArray.Length != otherByteArray.Length)
			{
				return false;
			}

			for(var index = 0; index < byteArray.Length; index++)
			{
				if (byteArray[index] != otherByteArray[index])
				{
					return false;
				}
			}

			return true;
		}
	}
}
