using Newtonsoft.Json;

namespace WinAuth2SDA.Data
{
    public sealed record IMobileAuthServiceResponse
    {
        [JsonProperty(PropertyName = "response")]
        public ResponseTokenData? Response { get; set; }
    }

    public sealed record ResponseTokenData
    {
        [JsonProperty(PropertyName = "token")]
        public string? Token { get; set; }

        [JsonProperty(PropertyName = "token_secure")]
        public string? TokenSecure { get; set; }
    }
}
