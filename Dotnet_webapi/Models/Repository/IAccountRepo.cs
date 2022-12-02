using Dotnet_webapi.Models.Entity;

namespace Dotnet_webapi.Models.Repository
{
    public interface IAccountRepo
    {
		Task<Account> GetAccountById(int accountId);
	}
}