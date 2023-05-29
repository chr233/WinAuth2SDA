using Newtonsoft.Json;

namespace WinAuth2SDA.Data
{
    public sealed record WinAuthSessionData
    {
        [JsonProperty(PropertyName = "steamid")]
        public string? SteamId { get; set; }

        [JsonProperty(PropertyName = "cookies")]
        public string? Cookies { get; set; }

        [JsonProperty(PropertyName = "oauthtoken")]
        public string? OAuthToken { get; set; }
    }
}
