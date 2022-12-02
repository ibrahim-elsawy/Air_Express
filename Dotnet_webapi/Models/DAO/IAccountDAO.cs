using Dotnet_webapi.Models.Entity;

namespace Dotnet_webapi.Models.DAO
{
    public interface IAccountDAO
    {
		Task<Account> getAccountById(int accountId);
		Task<IEnumerable<Account>> getAccounts();
		void deleteAccountById(int accountId);
		Task<Boolean> updateAccountById(Account newAccount);

	}
}