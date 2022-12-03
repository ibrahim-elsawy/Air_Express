using Dotnet_webapi.Models.DTO;
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

		public async Task<bool> CreateAccount(UserRegistrationDto user)
		{
			var acc = new Account() { 
				Login=user.Email,
				FirstName=user.FirstName,
				LastName=user.LastName
			};

			await _context.AddAsync<Account>(acc);
			await _context.SaveChangesAsync();
			return true;
		}

		public void deleteAccountById(int accountId)
		{
			throw new NotImplementedException();
		}

		public async Task<Account> getAccountById(int accountId)
		{
			Account acc = await _context.Accounts.FindAsync(accountId);
			if (acc != null)
			{
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