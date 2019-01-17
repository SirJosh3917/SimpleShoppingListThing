namespace ShoppingApp.Core.Middlemen
{
	public interface IUserValidator
	{
		bool IsValid(string username, string password);

		Shopper FetchUser(string username, string password);
	}
}