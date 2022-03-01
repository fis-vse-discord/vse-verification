using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VseVerification.Models;

public class MemberVerification
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public ulong DiscordId { get; set; }

    public string? AzureId { get; set; } = null;

    [Required]
    public bool IsRevoked { get; set; } = false;

    [NotMapped]
    public bool IsVerified => AzureId != null && !IsRevoked;
}