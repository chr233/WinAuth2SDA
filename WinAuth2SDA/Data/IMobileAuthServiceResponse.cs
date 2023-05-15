using System.Text.Json.Serialization;

namespace WinAuth2SDA.Data
{
    public sealed record IMobileAuthServiceResponse
    {
        [JsonPropertyName("response")]
        public ResponseTokenData? Response { get; set; }
    }

    public sealed record ResponseTokenData
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("token_secure")]
        public string? TokenSecure { get; set; }
    }
}
