using Dotnet_webapi.Models.Entity;
using Dotnet_webapi.Models.Repository;
using Dotnet_webapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{

	private readonly ILogger<HomeController> _logger;
	private readonly IScopedOperation _scopeOps;
    private readonly IAccountRepo _accountRepo;

	public AccountController(
        ILogger<HomeController> logger, 
        IScopedOperation scopeOps,
        IAccountRepo accountRepo
        )
	{
		_logger = logger;
		_scopeOps = scopeOps;
        _accountRepo = accountRepo;
	}

    [HttpGet("{id}")]
	public async Task<IActionResult> GetAsync(int id)
	{
		_logger.LogInformation("Get request is processed........");
		_logger.LogInformation($"Scoped:  {_scopeOps.OperationId}");

		Account acc = await _accountRepo.GetAccountById(id);

		return new JsonResult(acc);
	}
}
