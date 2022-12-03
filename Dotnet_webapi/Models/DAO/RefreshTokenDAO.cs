using Dotnet_webapi.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_webapi.Models.DAO
{
	public class RefreshTokenDAO : IRefreshTokenDAO
	{
		private readonly PostgresContext _apiDbContext;

		public RefreshTokenDAO(PostgresContext apiDbContext)
		{
			_apiDbContext = apiDbContext;
		}

		public async Task<bool> CreateToken(RefreshToken token)
		{

			await _apiDbContext.RefreshToken.AddAsync(token);
			await _apiDbContext.SaveChangesAsync();
			return true;
		}

		public async Task<RefreshToken> GetRefreshToken(string token)
		{
			RefreshToken retToken = await _apiDbContext.RefreshToken.FirstOrDefaultAsync(x => x.Token == token);
            return retToken;
		}

		public async Task<bool> UpdateToken(RefreshToken token)
		{
			_apiDbContext.RefreshToken.Update(token);
            await _apiDbContext.SaveChangesAsync();
			return true;
		}
	}
}