using Dotnet_webapi.Models.DAO;
using Dotnet_webapi.Models.DTO;
using Dotnet_webapi.Models.Entity;
using Microsoft.AspNetCore.Identity;

namespace Dotnet_webapi.Models.Repository
{
	public class AccountRepo : IAccountRepo
	{
		private readonly PostgresContext _context;
		private readonly IAccountDAO dao;
		private readonly UserManager<IdentityUser> _userManager;
		public AccountRepo(PostgresContext context, UserManager<IdentityUser> userManager)
		{
			_context = context;
			_userManager = userManager;
			dao = new AccountDAO(context);
		}
		// TODO: check if there's account already in Account Table air_postgres
		public async Task<IdentityUser> CreateAccount(UserRegistrationDto user)
		{
			var existingUser = await _userManager.FindByEmailAsync(user.Email);
			if (existingUser != null)
			{
				return null;
			}

			var newUser = new IdentityUser() { Email = user.Email, UserName = user.Username };
			var isCreated = await _userManager.CreateAsync(newUser, user.Password);
			if (isCreated.Succeeded)
			{
				await dao.CreateAccount(user);

				return newUser;
			}
			return null;
		}

		public Task<bool> DeleteAccountById(int accountId)
		{
			throw new NotImplementedException();
		}

		public async Task<Account> GetAccountById(int accountId)
		{
			return await dao.getAccountById(accountId);
		}

		public async Task<IdentityUser> GetUserById(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);
			return user;
		}

		public async Task<IdentityUser> Login(UserLoginRequest user)
		{
			var existingUser = await _userManager.FindByEmailAsync(user.Email); 
			if (existingUser == null)
			{
				return null;
			}

			var isCorrectPassword = await _userManager.CheckPasswordAsync(existingUser, user.Password);
			if (!isCorrectPassword)
			{
				return null;
			}
			return existingUser;

		}

		public Task<bool> UpdateAccountById(int accountId)
		{
			throw new NotImplementedException();
		}
	}
}