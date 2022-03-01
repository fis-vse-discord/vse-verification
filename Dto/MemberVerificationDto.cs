using System.Text.Json.Serialization;
using VseVerification.Models;

namespace VseVerification.Dto;

public class MemberVerificationDto
{
    public MemberVerificationDto(MemberVerification verification)
    {
        IsVerified = verification.AzureId != null && !verification.IsRevoked;
        VerificationId = verification.Id.ToString();
        DiscordMemberId = verification.DiscordId.ToString();
        AzureId = verification.AzureId;
    }

    [JsonPropertyName("is_verified")]
    public bool IsVerified;

    [JsonPropertyName("verification_id")]
    public string VerificationId;

    [JsonPropertyName("discord_member_id")]
    public string DiscordMemberId;

    [JsonPropertyName("azure_id")]
    public string? AzureId;
}