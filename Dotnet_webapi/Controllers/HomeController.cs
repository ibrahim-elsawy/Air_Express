using Dotnet_webapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{

	private readonly ILogger<HomeController> _logger;
	private readonly IScopedOperation _scopeOps;

	public HomeController(ILogger<HomeController> logger, IScopedOperation scopeOps)
	{
		_logger = logger;
		_scopeOps = scopeOps;
	}

	// [HttpGet(Name = "GetWeatherForecast")]
	public IActionResult Get()
	{
		_logger.LogInformation("Get request is processed........");
		_logger.LogInformation($"Scoped:  {_scopeOps.OperationId}");

		return Ok();
	}
}
