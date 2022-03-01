using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using VseVerification.Configuration;
using VseVerification.Data;
using VseVerification.Exceptions;
using VseVerification.Models;
using VseVerification.Services.Contract;

namespace VseVerification.Services;

public class MemberVerificationsService : IMemberVerificationsService
{
    private readonly VseVerificationDbContext _context;

    private readonly VerificationConfiguration _configuration;

    private const string ObjectIdClaimType = "http://schemas.microsoft.com/identity/claims/objectidentifier";

    private const string GroupClaimType = "groups";

    public MemberVerificationsService(VseVerificationDbContext context,
        IOptions<VerificationConfiguration> configuration)
    {
        _context = context;
        _configuration = configuration.Value;
    }

    public async Task VerifyMemberAsync(Guid id, IEnumerable<ClaimsIdentity> identities)
    {
        var verification = await _context.Verifications.FindAsync(id)
                           ?? throw new MemberVerificationException("Member verification not found");

        // If the member was already verified and the verification is not revoked, early return to indicate success
        if (verification.IsVerified) return;

        if (verification.IsRevoked)
        {
            throw new MemberVerificationException("Member verification was revoked by the administrators.");
        }

        var claims = identities.SelectMany(i => i.Claims).ToList();
        var azureId = claims.First(c => c.Type == ObjectIdClaimType).Value;

        // Check if this azure Id was already used to complete another verification
        if (await _context.Verifications.AnyAsync(v => v.Id != id && v.AzureId == azureId))
        {
            throw new MemberVerificationException("This azure account was already used to complete another verification!");
        }

        var groups = claims.Where(c => c.Type == GroupClaimType).Select(c => c.Value);

        if (groups.Any(group => _configuration.BlockedRoles.Contains(group)))
        {
            throw new MemberVerificationException("User is member of one or more of the blocked groups!");
        }

        // Complete the verification process
        verification.AzureId = azureId;
        verification.IsRevoked = false;

        _context.Verifications.Update(verification);
        
        await _context.SaveChangesAsync();
    }

    public async Task<MemberVerification> GetMemberVerificationAsync(ulong discordId)
    {
        var verification = await _context.Verifications.FirstOrDefaultAsync(v => v.DiscordId == discordId);

        if (verification != null)
        {
            return verification;
        }

        var instance = new MemberVerification {DiscordId = discordId};

        await _context.AddAsync(instance);
        await _context.SaveChangesAsync();

        return instance;
    }
}