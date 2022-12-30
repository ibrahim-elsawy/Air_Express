using Microsoft.AspNetCore.Mvc;

namespace Dotnet_webapi.Controllers
{
	[ApiController]
	[Route("[controller]")]
    public class FlightController : ControllerBase
    { 
        private readonly ILogger<AccountController> _logger; 
        
        [HttpGet("delayed")]
        public async Task<IActionResult> GetAsync()
        {
            _logger.LogInformation("Get /Delayed Flight request is processed........");

            // return new JsonResult();
            return Ok();
        }
    }
}