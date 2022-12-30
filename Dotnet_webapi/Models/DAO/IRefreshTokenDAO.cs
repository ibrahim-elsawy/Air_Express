using Dotnet_webapi.Models.Entity;

namespace Dotnet_webapi.Models.DAO
{
    public interface IRefreshTokenDAO
    {
		Task<bool> CreateToken(RefreshToken token);
		Task<bool> UpdateToken(RefreshToken token);
		Task<RefreshToken> GetRefreshToken(string token);
	}
}