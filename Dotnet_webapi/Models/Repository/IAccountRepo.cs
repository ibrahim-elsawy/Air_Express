using Dotnet_webapi.Models.DTO;
using Dotnet_webapi.Models.Entity;
using Microsoft.AspNetCore.Identity;

namespace Dotnet_webapi.Models.Repository
{
	public interface IAccountRepo
	{
		Task<Account> GetAccountById(int accountId);
		Task<IdentityUser> GetUserById(string userId);
		Task<IdentityUser> CreateAccount(UserRegistrationDto user);
		Task<IdentityUser> Login(UserLoginRequest user);
		Task<bool> DeleteAccountById(int accountId);
		Task<bool> UpdateAccountById(int accountId);
	}
}