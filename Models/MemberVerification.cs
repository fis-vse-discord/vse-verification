using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VseVerification.Models;

public class MemberVerification
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public ulong DiscordId { get; set; }

    [Required]
    public string? AzureId { get; set; } = null;

    [Required]
    public bool IsRevoked { get; set; } = false;
}