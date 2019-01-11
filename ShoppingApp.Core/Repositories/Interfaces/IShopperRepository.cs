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

	// TODO: remove these interfaces if i become 100% sure i don't need them
	public interface IUsername
	{
		string Username { get; }
	}

	public interface IEmail
	{
		string Email { get; }
	}

	public interface IPassword
	{
		string Password { get; }
	}

	public interface IRegistrationInformation : IUsername, IEmail, IPassword
	{
		bool SendEmails { get; }
	}

	public interface IEmailLoginInformation : IEmail, IPassword
	{
	}

	public interface IUsernameLoginInformation : IUsername, IPassword
	{
	}
}