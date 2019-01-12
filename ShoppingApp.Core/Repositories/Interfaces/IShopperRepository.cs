namespace ShoppingApp.Core.Repositories.Interfaces
{
	public interface IShopperRepository
	{
		void Register(IRegistrationInformation registrationInformation);

		bool PasswordMatches(IEmailLoginInformation emailLogin);

		bool PasswordMatches(IUsernameLoginInformation usernameLogin);

		Shopper FindByUsername(string username);

		Shopper FindByEmail(string email);

		void AddListTo(ShoppingList shoppingList, Shopper onShopper);
	}

	public interface IRegistrationInformation
	{
		string Username { get; }
		string Email { get; }
		string Password { get; }
		bool SendEmails { get; }
	}

	public interface IEmailLoginInformation
	{
		string Email { get; }
		string Password { get; }
	}

	public interface IUsernameLoginInformation
	{
		string Username { get; }
		string Password { get; }
	}
}