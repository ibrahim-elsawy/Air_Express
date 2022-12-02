using Dotnet_webapi.Models.DAO;
using Dotnet_webapi.Models.Entity;

namespace Dotnet_webapi.Models.Repository
{
	public class AccountRepo : IAccountRepo
	{
        private readonly PostgresContext _context;
		private readonly IAccountDAO dao;
		public AccountRepo(PostgresContext context)
		{
            _context = context;
			dao = new AccountDAO(context);
		}

		public async Task<Account> GetAccountById(int accountId)
		{
			return await dao.getAccountById(accountId);
		}
	}
}