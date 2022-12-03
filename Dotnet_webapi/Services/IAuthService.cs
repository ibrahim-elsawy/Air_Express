using Dotnet_webapi.Models.DTO;

namespace Dotnet_webapi.Services
{
	public interface IAuthService
	{
		Task<AuthResult> RegisterNewUser(UserRegistrationDto user);
		Task<AuthResult> Login(UserLoginRequest user);
		Task<AuthResult> ValidateAndRegenerateToken(TokenRequest tokenRequest);

	}
}