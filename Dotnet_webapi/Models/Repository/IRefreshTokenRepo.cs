using Dotnet_webapi.Models.Entity;

namespace Dotnet_webapi.Models.Repository
{
    public interface IRefreshTokenRepo
    { 
        Task<bool> CreateRefreshToken(RefreshToken token);
        Task<bool> UpdateRefreshToken(RefreshToken token);
		Task<RefreshToken> GetRefreshToken(string token);

	}
}