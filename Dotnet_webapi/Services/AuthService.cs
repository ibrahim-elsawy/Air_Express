using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dotnet_webapi.Config;
using Dotnet_webapi.Models.DTO;
using Dotnet_webapi.Models.Entity;
using Dotnet_webapi.Models.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Dotnet_webapi.Services
{
	public class AuthService : IAuthService
	{
		private readonly ILogger<AuthService> _logger;
		private readonly JwtConfig _jwtConfig;
		private readonly TokenValidationParameters _tokenValidationParams;
		private readonly IRefreshTokenRepo _refreshRepo;
		private readonly IAccountRepo _accountRepo;
		public AuthService(
			ILogger<AuthService> logger,
			IOptionsMonitor<JwtConfig> optionsMonitor,
			TokenValidationParameters tokenValidationParams,
			IRefreshTokenRepo refreshRepo,
			IAccountRepo accountRepo)
		{
			_logger = logger;
			_jwtConfig = optionsMonitor.CurrentValue;
			_tokenValidationParams = tokenValidationParams;
			_refreshRepo = refreshRepo;
			_accountRepo = accountRepo;
		}

		public async Task<AuthResult> RegisterNewUser(UserRegistrationDto user)
		{
			var newUser = await _accountRepo.CreateAccount(user);
			if (newUser != null)
			{
				var jwtToken = await GenerateJwtToken(newUser);
				return jwtToken;
			}
			return null;
		}

		public async Task<AuthResult> Login(UserLoginRequest user)
		{
			IdentityUser existingUser = await _accountRepo.Login(user);
			if (existingUser == null)
			{
				return null;
			}
			return await GenerateJwtToken(existingUser);
		}

	
		public async Task<AuthResult> ValidateAndRegenerateToken(TokenRequest tokenRequest)
		{
			return await VerifyAndGenerateToken(tokenRequest);
		}
		private async Task<AuthResult> GenerateJwtToken(IdentityUser user)
		{
			var jwtTokenHandler = new JwtSecurityTokenHandler();

			var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim("Id", user.Id),
					new Claim(JwtRegisteredClaimNames.Email, user.Email),
					new Claim(JwtRegisteredClaimNames.Sub, user.Email),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
				}),
				// Expires = DateTime.UtcNow.AddSeconds(30), // 5-10 
				Expires = DateTime.UtcNow.AddDays(10), // 5-10 
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = jwtTokenHandler.CreateToken(tokenDescriptor);
			var jwtToken = jwtTokenHandler.WriteToken(token);

			var refreshToken = new RefreshToken()
			{
				JwtId = token.Id,
				IsUsed = false,
				IsRevorked = false,
				UserId = user.Id,
				AddedDate = DateTime.UtcNow,
				ExpiryDate = DateTime.UtcNow.AddMonths(6),
				Token = RandomString(35) + Guid.NewGuid()
			};
			await _refreshRepo.CreateRefreshToken(refreshToken);
			var x = new AuthResult()
			{
				Token = jwtToken,
				Success = true,
				RefreshToken = refreshToken.Token
			};
			Console.WriteLine($"this generate funvtion line 210   {x}");

			return x;
		}

		private async Task<AuthResult> VerifyAndGenerateToken(TokenRequest tokenRequest)
		{
			var jwtTokenHandler = new JwtSecurityTokenHandler();

			try
			{
				// Validation 1 - Validation JWT token format
				var tokenInVerification = jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParams, out var validatedToken);

				// Validation 2 - Validate encryption alg
				if (validatedToken is JwtSecurityToken jwtSecurityToken)
				{
					var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

					if (result == false)
					{
						return null;
					}
				}

				// Validation 3 - validate expiry date
				var utcExpiryDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

				var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);

				if (expiryDate > DateTime.UtcNow)
				{
					return new AuthResult()
					{
						Success = false,
						Errors = new List<string>() {
			    "Token has not yet expired"
			}
					};
				}

				// validation 4 - validate existence of the token
				var storedToken = await _refreshRepo.GetRefreshToken(tokenRequest.RefreshToken);

				if (storedToken == null)
				{
					return new AuthResult()
					{
						Success = false,
						Errors = new List<string>() {
			    "Token does not exist"
			}
					};
				}

				// Validation 5 - validate if used
				if (storedToken.IsUsed)
				{
					return new AuthResult()
					{
						Success = false,
						Errors = new List<string>() {
			    "Token has been used"
			}
					};
				}

				// Validation 6 - validate if revoked
				if (storedToken.IsRevorked)
				{
					return new AuthResult()
					{
						Success = false,
						Errors = new List<string>() {
			    "Token has been revoked"
			}
					};
				}

				// Validation 7 - validate the id
				var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

				if (storedToken.JwtId != jti)
				{
					return new AuthResult()
					{
						Success = false,
						Errors = new List<string>() {
							"Token doesn't match"
							}
					};
				}

				// update current token 

				storedToken.IsUsed = true;
				await _refreshRepo.UpdateRefreshToken(storedToken);

				// Generate a new token
				var dbUser = await _accountRepo.GetUserById(storedToken.UserId);
				return await GenerateJwtToken(dbUser);
			}
			catch (Exception ex)
			{
				if (ex.Message.Contains("Lifetime validation failed. The token is expired."))
				{

					return new AuthResult()
					{
						Success = false,
						Errors = new List<string>() {
							"Token has expired please re-login"
							}
					};

				}
				else
				{
					return new AuthResult()
					{
						Success = false,
						Errors = new List<string>() {
							"Something went wrong."
							}
					};
				}
			}
		}

		private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
		{
			var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();

			return dateTimeVal;
		}

		private string RandomString(int length)
		{
			var random = new Random();
			var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(Enumerable.Repeat(chars, length)
				.Select(x => x[random.Next(x.Length)]).ToArray());
		}


	}
}