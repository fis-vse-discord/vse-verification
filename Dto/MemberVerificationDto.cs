using System.Text.Json.Serialization;
using VseVerification.Models;

namespace VseVerification.Dto;

public class MemberVerificationDto
{
    public MemberVerificationDto(MemberVerification verification)
    {
        VerificationId = verification.Id.ToString();
        DiscordMemberId = verification.DiscordId.ToString();
        AzureId = verification.AzureId;
    }
    
    [JsonPropertyName("verification_id")]
    public string VerificationId { get; }
    
    [JsonPropertyName("discord_member_id")]
    public string DiscordMemberId { get; }
    
    [JsonPropertyName("azure_id")]
    public string? AzureId { get; }
}