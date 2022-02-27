using System.Security.Claims;
using VseVerification.Data;
using VseVerification.Services.Contract;

namespace VseVerification.Services;

public class MemberVerificationsService : IMemberVerificationsService
{
    private readonly VseVerificationDbContext _context;

    public MemberVerificationsService(VseVerificationDbContext context)
    {
        _context = context;
    }

    public async Task VerifyMemberAsync(Guid id, IEnumerable<ClaimsIdentity> identities)
    {
        var verification = await _context.Verifications.FindAsync(id) 
            ?? throw new ApplicationException("Member verification not found");
    }
}