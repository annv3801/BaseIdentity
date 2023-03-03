using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Application.DTO.Account.Responses
{
    [ExcludeFromCodeCoverage]
    // Var name is set to mapping with fields Json string
    public class FacebookAccountData
    {
        [JsonPropertyName("id")] public string Id { get; set; } = "";
        [JsonPropertyName("email")] public string Email { get; set; } = "";
        [JsonPropertyName("first_name")] public string FirstName { get; set; } = "";
        [JsonPropertyName("last_name")] public string LastName { get; set; } = "";
        [JsonPropertyName("gender")] public string Gender { get; set; } = "";
        [JsonPropertyName("picture")] public Picture Picture { get; set; } = new Picture();
    }

    [ExcludeFromCodeCoverage]
    public class Data
    {
        [JsonPropertyName("height")] public int Height { get; set; } = 0;
        [JsonPropertyName("is_silhouette")] public bool IsSilhouette { get; set; } = true;
        [JsonPropertyName("url")] public string Url { get; set; } = "";
        [JsonPropertyName("width")] public int Width { get; set; } = 0;
    }

    [ExcludeFromCodeCoverage]
    public class Picture
    {
        [JsonPropertyName("data")] public Data Data { get; set; } = new Data();
    }
}