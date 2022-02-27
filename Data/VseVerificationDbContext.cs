using Microsoft.EntityFrameworkCore;
using VseVerification.Models;

namespace VseVerification.Data;

public class VseVerificationDbContext : DbContext
{
    public VseVerificationDbContext(DbContextOptions<VseVerificationDbContext> options) : base(options)
    {
    }

    public virtual DbSet<MemberVerification> Verifications { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder model)
    {
        model.Entity<MemberVerification>()
            .HasIndex(m => m.DiscordId)
            .IsUnique();

        model.Entity<MemberVerification>()
            .HasIndex(m => m.AzureId)
            .IsUnique();
    }
}