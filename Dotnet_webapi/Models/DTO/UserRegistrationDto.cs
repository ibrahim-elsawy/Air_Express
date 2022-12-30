using System.ComponentModel.DataAnnotations;

namespace Dotnet_webapi.Models.DTO
{
	public class UserRegistrationDto
	{
		[Required]
		public string Username { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public string Password { get; set; }
	}
}