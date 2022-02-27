using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VseVerification.Migrations;
using VseVerification.Services.Contract;

namespace VseVerification.Controllers;

public class VerificationsController : Controller
{
    private readonly ILogger<VerificationsController> _logger;

    private readonly IMemberVerificationsService _service;

    public VerificationsController(ILogger<VerificationsController> logger, IMemberVerificationsService service)
    {
        _logger = logger;
        _service = service;
    }

    [Authorize]
    [HttpGet("/verification/{id:guid}")]
    public async Task<IActionResult> ProcessVerification(Guid id)
    {
        try
        {
            await _service.VerifyMemberAsync(id, User.Identities);
            return View("Success");
        }
        catch (Exception exception)
        {
            _logger.LogCritical("Failed to process verification [{id}] for the following reason: {reason}", id, exception.Message);
            return View("Failure");
        }
    }
}