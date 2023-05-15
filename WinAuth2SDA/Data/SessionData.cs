using System.Text.Json.Serialization;

namespace WinAuth2SDA.Data
{
    public sealed record SessionData
    {
        [JsonPropertyName("SessionID")]
        public string? SessionID { get; set; }

        [JsonPropertyName("SteamLogin")]
        public string? SteamLogin { get; set; }

        [JsonPropertyName("SteamLoginSecure")]
        public string? SteamLoginSecure { get; set; }

        [JsonPropertyName("WebCookie")]
        public string? WebCookie { get; set; }

        [JsonPropertyName("OAuthToken")]
        public string? OAuthToken { get; set; }

        [JsonPropertyName("SteamID")]
        public long SteamID { get; set; }
    }
}
