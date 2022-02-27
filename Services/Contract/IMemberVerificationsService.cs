using System.Security.Claims;

namespace VseVerification.Services.Contract;

public interface IMemberVerificationsService
{
    public Task VerifyMemberAsync(Guid id, IEnumerable<ClaimsIdentity> identities);

}