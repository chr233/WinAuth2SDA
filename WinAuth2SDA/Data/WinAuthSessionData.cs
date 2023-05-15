using System.Text.Json.Serialization;

namespace WinAuth2SDA.Data
{
    public sealed record WinAuthSessionData
    {
        [JsonPropertyName("steamid")]
        public string? SteamId { get; set; }

        [JsonPropertyName("cookies")]
        public string? Cookies { get; set; }

        [JsonPropertyName("oauthtoken")]
        public string? OAuthToken { get; set; }

        [JsonPropertyName("confs")]
        public string? Confs { get; set; }
    }
}
