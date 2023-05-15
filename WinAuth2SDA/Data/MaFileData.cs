using System.Text.Json.Serialization;

namespace WinAuth2SDA.Data
{
    public sealed record MaFileData
    {
        [JsonPropertyName("shared_secret")]
        public string? SharedSecret { get; set; }

        [JsonPropertyName("serial_number")]
        public string? SerialNumber { get; set; }

        [JsonPropertyName("revocation_code")]
        public string? RevocationCode { get; set; }

        [JsonPropertyName("uri")]
        public string? Uri { get; set; }

        [JsonPropertyName("server_time")]
        public long ServerTime { get; set; }

        [JsonPropertyName("account_name")]
        public string? AccountName { get; set; }

        [JsonPropertyName("token_gid")]
        public string? TokenGid { get; set; }

        [JsonPropertyName("identity_secret")]
        public string? IdentitySecret { get; set; }

        [JsonPropertyName("secret_1")]
        public string? Secret1 { get; set; }

        [JsonPropertyName("status")]
        public int status { get; set; }

        [JsonPropertyName("device_id")]
        public string? DeviceId { get; set; }

        [JsonPropertyName("fully_enrolled")]
        public bool FullyEnrolled { get; set; } = true;

        [JsonPropertyName("Session")]
        public SessionData? Session { get; set; }
    }
}
