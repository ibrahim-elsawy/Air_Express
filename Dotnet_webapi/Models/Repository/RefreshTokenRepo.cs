using Dotnet_webapi.Models.DAO;
using Dotnet_webapi.Models.Entity;

namespace Dotnet_webapi.Models.Repository
{
	public class RefreshTokenRepo : IRefreshTokenRepo
	{
		private IRefreshTokenDAO _dao;

		public RefreshTokenRepo(IRefreshTokenDAO dao)
		{
			_dao = dao;
		}

		public async Task<bool> CreateRefreshToken(RefreshToken token)
		{
			await _dao.CreateToken(token);
			return true;
		}

		public async Task<RefreshToken> GetRefreshToken(string token)
		{
			return await _dao.GetRefreshToken(token);
		}

		public async Task<bool> UpdateRefreshToken(RefreshToken token)
		{
			return await _dao.UpdateToken(token);
		}
	}
}