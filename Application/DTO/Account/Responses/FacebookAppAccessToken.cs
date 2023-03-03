using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Application.DTO.Account.Responses
{
    [ExcludeFromCodeCoverage]
    public class FacebookAppAccessToken
    {
        [JsonPropertyName("access_token")] public string AccessToken { get; set; } = "";
        [JsonPropertyName("token_type")] public string TokenType { get; set; } = "";
    }
}