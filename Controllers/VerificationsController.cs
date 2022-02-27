using Microsoft.AspNetCore.Mvc;

namespace VseVerification.Controllers;

public class VerificationsController : Controller
{
    private readonly ILogger<VerificationsController> _logger;

    public VerificationsController(ILogger<VerificationsController> logger)
    {
        _logger = logger;
    }

    [HttpGet("/verification/{discordMemberId:long}")]
    public async Task<IActionResult> ProcessVerification(ulong discordMemberId)
    {
        try
        {
            return View("Success");
        }
        catch (Exception exception)
        {
            _logger.LogCritical("Failed to authenticate user [discord_member_id={id}] for the following reason: {reason}", discordMemberId, exception.Message);
            return View("Failure");
        }
    }
}