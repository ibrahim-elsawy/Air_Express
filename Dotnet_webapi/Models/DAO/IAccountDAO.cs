using Dotnet_webapi.Models.DTO;
using Dotnet_webapi.Models.Entity;

namespace Dotnet_webapi.Models.DAO
{
	public interface IAccountDAO
	{
		Task<bool> CreateAccount(UserRegistrationDto user);
		Task<Account> getAccountById(int accountId);
		Task<IEnumerable<Account>> getAccounts();
		void deleteAccountById(int accountId);
		Task<Boolean> updateAccountById(Account newAccount);

	}
}