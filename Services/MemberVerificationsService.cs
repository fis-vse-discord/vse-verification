using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using VseVerification.Data;
using VseVerification.Models;
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

    public async Task<MemberVerification> GetMemberVerificationAsync(ulong discordId)
    {
        var verification = await _context.Verifications.FirstOrDefaultAsync(v => v.DiscordId == discordId);

        if (verification != null)
        {
            return verification;
        }

        var instance = new MemberVerification { DiscordId = discordId };

        await _context.AddAsync(instance);
        await _context.SaveChangesAsync();

        return instance;
    }
}