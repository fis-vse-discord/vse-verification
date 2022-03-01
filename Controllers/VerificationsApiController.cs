using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VseVerification.Dto;
using VseVerification.Models;
using VseVerification.Services.Contract;

namespace VseVerification.Controllers;

[ApiController]
[Authorize("ApiKey")]
public class VerificationsApiController : Controller
{
    private readonly ILogger<VerificationsApiController> _logger;

    private readonly IMemberVerificationsService _service;

    public VerificationsApiController(ILogger<VerificationsApiController> logger, IMemberVerificationsService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet("/api/verification/{discordId:long}")]
    public async Task<MemberVerificationDto> GetVerificationAsync(ulong discordId)
    {
        var verification = await _service.GetMemberVerificationAsync(discordId);

        return new MemberVerificationDto(verification);
    }
}