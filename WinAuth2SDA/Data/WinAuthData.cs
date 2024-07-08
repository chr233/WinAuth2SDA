using Newtonsoft.Json;

namespace WinAuth2SDA.Data
{
    public sealed record WinAuthData
    {
        [JsonProperty(PropertyName = "shared_secret")]
        public string? SharedSecret { get; set; }

        [JsonProperty(PropertyName = "serial_number")]
        public string? SerialNumber { get; set; }

        [JsonProperty(PropertyName = "revocation_code")]
        public string? RevocationCode { get; set; }

        [JsonProperty(PropertyName = "uri")]
        public string? Uri { get; set; }

        [JsonProperty(PropertyName = "server_time")]
        public string? ServerTime { get; set; }

        [JsonProperty(PropertyName = "account_name")]
        public string? AccountName { get; set; }

        [JsonProperty(PropertyName = "token_gid")]
        public string? TokenGid { get; set; }

        [JsonProperty(PropertyName = "identity_secret")]
        public string? IdentitySecret { get; set; }

        [JsonProperty(PropertyName = "secret_1")]
        public string? Secret1 { get; set; }

        [JsonProperty(PropertyName = "status")]
        public int? status { get; set; }

        [JsonProperty(PropertyName = "device_id")]
        public string? DeviceId { get; set; }

        [JsonProperty(PropertyName = "fully_enrolled")]
        public bool FullyEnrolled { get; set; } = true;

        [JsonProperty(PropertyName = "Session")]
        public SessionData? Session { get; set; }
    }
}
