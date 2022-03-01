using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VseVerification.Exceptions;
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

    [Authorize("Student")]
    [HttpGet("/verification/{id:guid}")]
    public async Task<IActionResult> ProcessVerification(Guid id)
    {
        try
        {
            await _service.VerifyMemberAsync(id, User.Identities);
            return Redirect("/verification/success");
        }
        catch (MemberVerificationException exception)
        {
            _logger.LogCritical("Failed to process verification [{id}] for the following reason: {reason}", id, exception.Message);
            return Redirect("/verification/failure");
        }
        catch (Exception exception)
        {
            return Redirect("/error");
        }
    }

    [AllowAnonymous]
    [HttpGet("/verification/success")]
    public IActionResult Success() => View("Success");

    [AllowAnonymous]
    [HttpGet("/verification/failure")]
    public IActionResult Failure() => View("Failure");
}