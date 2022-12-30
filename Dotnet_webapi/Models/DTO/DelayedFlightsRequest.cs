using System.ComponentModel.DataAnnotations;

namespace Dotnet_webapi.Models.DTO
{
    public class DelayedFlightsRequest
    {
        [Required]
        public string StartDate {get; set;}
        [Required]
        public string EndDate { get; set; }
	}
}