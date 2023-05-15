using System.Text.Json.Serialization;

namespace WinAuth2SDA.Data
{
    public sealed record ManifestEntryData
    {
        [JsonPropertyName("encryption_iv")]
        public string? EncryptionIv { get; set; }

        [JsonPropertyName("encryption_salt")]
        public string? EncryptionSalt { get; set; }

        [JsonPropertyName("filename")]
        public string? FileName { get; set; }

        [JsonPropertyName("steamid")]
        public long SteamId { get; set; }
    }
}
