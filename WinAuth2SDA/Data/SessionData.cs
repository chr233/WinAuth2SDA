using Newtonsoft.Json;

namespace WinAuth2SDA.Data
{
    public sealed record SessionData
    {
        [JsonProperty(PropertyName = "SessionID")]
        public string? SessionID { get; set; }

        [JsonProperty(PropertyName = "SteamLogin")]
        public string? SteamLogin { get; set; }

        [JsonProperty(PropertyName = "SteamLoginSecure")]
        public string? SteamLoginSecure { get; set; }

        [JsonProperty(PropertyName = "WebCookie")]
        public string? WebCookie { get; set; }

        [JsonProperty(PropertyName = "OAuthToken")]
        public string? OAuthToken { get; set; }

        [JsonProperty(PropertyName = "SteamID")]
        public ulong SteamID { get; set; }
    }
}
