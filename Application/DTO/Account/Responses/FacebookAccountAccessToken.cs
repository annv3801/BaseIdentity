using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Application.DTO.Account.Responses
{
    [ExcludeFromCodeCoverage]
    // Var name is set to mapping with fields Json string
    public class FacebookAccountAccessToken
    {
        [JsonPropertyName("data")] public DataAccessToken Data { get; set; } = new DataAccessToken();
    }

    [ExcludeFromCodeCoverage]
    public class Error
    {
        [JsonPropertyName("code")] public int Code { get; set; } = 0;

        [JsonPropertyName("message")] public string Message { get; set; } = "";

        [JsonPropertyName("subcode")] public int SubCode { get; set; } = 0;
    }

    [ExcludeFromCodeCoverage]
    public class Metadata
    {
        [JsonPropertyName("auth_type")] public string AuthType { get; set; } = "";
    }

    [ExcludeFromCodeCoverage]
    public class DataAccessToken
    {
        [JsonPropertyName("app_id")] public string AppId { get; set; } = "";

        [JsonPropertyName("type")] public string Type { get; set; } = "";

        [JsonPropertyName("application")] public string Application { get; set; } = "";

        [JsonPropertyName("data_access_expires_at")]
        public int DataAccessExpiresAt { get; set; } = 0;

        [JsonPropertyName("error")] public Error? Error { get; set; } = new Error();

        [JsonPropertyName("expires_at")] public int ExpiresAt { get; set; } = 0;

        [JsonPropertyName("is_valid")] public bool IsValid { get; set; } = true;

        [JsonPropertyName("metadata")] public Metadata Metadata { get; set; } = new Metadata();
        [JsonPropertyName("scopes")] public List<string> Scopes { get; set; } = new List<string>();

        [JsonPropertyName("user_id")] public string UserId { get; set; } = "";
    }
}