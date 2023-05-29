using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WinAuth2SDA.Data
{
    public sealed record ManifestEntryData
    {
        [JsonProperty(PropertyName = "encryption_iv")]
        public string? EncryptionIv { get; set; }

        [JsonProperty(PropertyName = "encryption_salt")]
        public string? EncryptionSalt { get; set; }

        [JsonProperty(PropertyName = "filename")]
        public string? FileName { get; set; }

        [JsonProperty(PropertyName = "steamid")]
        public ulong SteamId { get; set; }
    }
}
