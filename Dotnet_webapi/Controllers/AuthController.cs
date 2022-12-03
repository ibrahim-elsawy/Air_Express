using Dotnet_webapi.Config;
using Dotnet_webapi.Models.DTO;
using Dotnet_webapi.Models.Entity;
using Dotnet_webapi.Models.Repository;
using Dotnet_webapi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Dotnet_webapi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AuthController : ControllerBase
	{

		private readonly ILogger<AuthController> _logger;
		private readonly IAuthService _auth;

		public AuthController(
		ILogger<AuthController> logger,
		IAuthService auth
		)
		{
			_logger = logger;
			_auth = auth;
		}


		[HttpPost]
		[Route("Register")]
		public async Task<IActionResult> Register(UserRegistrationDto user)
		{
			if (ModelState.IsValid)
			{
				var jwtToken = await _auth.RegisterNewUser(user);
				if (jwtToken.Success)
				{
					return Ok(jwtToken);
				}

				_logger.LogInformation("Error in Registring New User ..... %o", jwtToken.Errors[0]);
				return BadRequest(new RegistrationResponse()
				{
					Errors = new List<string>(){
						"Invalid payload"
						},
					Success = false
				});

			}

			return BadRequest(new RegistrationResponse()
			{
				Errors = new List<string>() {
					"Invalid payload"
					},
				Success = false
			});
		}

		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
		{
			if (ModelState.IsValid)
			{
				var jwtToken = await _auth.Login(user);
				if (jwtToken.Success)
				{
					return Ok(jwtToken);
				} 
				_logger.LogInformation("Error in Token Login ....... %o", jwtToken.Errors[0]);
				return BadRequest(new RegistrationResponse()
				{
					Errors = new List<string>(){
						"Invalid payload"
						},
					Success = false
				});

			}

			return BadRequest(new RegistrationResponse()
			{
				Errors = new List<string>(){
					"Invalid payload"
				},
				Success = false
			});
		}

		[HttpPost]
		[Route("RefreshToken")]
		public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
		{
			if (ModelState.IsValid)
			{
				var jwtToken = await _auth.ValidateAndRegenerateToken(tokenRequest);
				if (jwtToken.Success)
				{
					return Ok(jwtToken);
				}
				_logger.LogInformation("Error in Refresh Token Validation ....... %o", jwtToken.Errors[0]);
				return BadRequest(new RegistrationResponse()
				{
					Errors = new List<string>(){
						"Invalid payload"
						},
					Success = false
				});

			}
			return BadRequest(new RegistrationResponse()
			{
				Errors = new List<string>(){
					"Invalid payload"
				},
				Success = false
			});
		}


	}
}