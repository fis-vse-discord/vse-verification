using System.Text.Json.Serialization;
using VseVerification.Models;

namespace VseVerification.Dto;

public class MemberVerificationDto
{
    public MemberVerificationDto(MemberVerification verification)
    {
        IsVerified = verification.AzureId != null && !verification.IsRevoked;
        DiscordMemberId = verification.DiscordId;
        VerificationId = verification.Id.ToString();
        AzureId = verification.AzureId;
    }

    [JsonPropertyName("is_verified")]
    public bool IsVerified { get; }

    [JsonPropertyName("discord_member_id")]
    public ulong DiscordMemberId { get; }

    [JsonPropertyName("verification_id")]
    public string VerificationId { get; }

    [JsonPropertyName("azure_id")]
    public string? AzureId { get; }
}