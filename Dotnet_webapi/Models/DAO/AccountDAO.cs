using Dotnet_webapi.Models.Entity;

namespace Dotnet_webapi.Models.DAO
{
	public class AccountDAO : IAccountDAO
	{
        private readonly PostgresContext _context;
		public AccountDAO(PostgresContext context)
		{
            _context = context;
		}

		public void deleteAccountById(int accountId)
		{
			throw new NotImplementedException();
		}

		public async Task<Account> getAccountById(int accountId)
		{
            Account acc = await _context.Accounts.FindAsync(accountId);
            if (acc != null){
				return acc;
			}
			return null;
		}

		public Task<IEnumerable<Account>> getAccounts()
		{
			throw new NotImplementedException();
		}

		public Task<bool> updateAccountById(Account newAccount)
		{
			throw new NotImplementedException();
		}
	}
}