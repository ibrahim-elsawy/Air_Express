using System.ComponentModel.DataAnnotations;

namespace Dotnet_webapi.Models.DTO
{
	public class UserLoginRequest
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
	}
}