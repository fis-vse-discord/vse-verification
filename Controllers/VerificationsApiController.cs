using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VseVerification.Dto;
using VseVerification.Services.Contract;

namespace VseVerification.Controllers;

[ApiController]
[Authorize("ApiKey")]
public class VerificationsApiController : Controller
{
    private readonly IMemberVerificationsService _service;

    public VerificationsApiController(IMemberVerificationsService service)
    {
        _service = service;
    }

    [HttpGet("/api/verification/{discordId:long}")]
    public async Task<MemberVerificationDto> GetVerificationAsync(ulong discordId)
    {
        var verification = await _service.GetMemberVerificationAsync(discordId);

        return new MemberVerificationDto(verification);
    }
}