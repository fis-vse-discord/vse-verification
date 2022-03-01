using System.Security.Claims;
using VseVerification.Models;

namespace VseVerification.Services.Contract;

public interface IMemberVerificationsService
{
    Task VerifyMemberAsync(Guid id, IEnumerable<ClaimsIdentity> identities);

    Task<MemberVerification> GetMemberVerificationAsync(ulong discordId);
}