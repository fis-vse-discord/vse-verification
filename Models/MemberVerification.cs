using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VseVerification.Models;

public class MemberVerification
{
    [Key]
    [JsonPropertyName("verification_id")]
    public Guid Id { get; set; }

    [Required]
    [JsonPropertyName("discord_user_id")]
    public ulong DiscordId { get; set; }

    [Required]
    [JsonPropertyName("azure_user_id")]
    public string? AzureId { get; set; } = null;
}