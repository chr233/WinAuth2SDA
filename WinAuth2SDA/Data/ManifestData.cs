using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WinAuth2SDA.Data
{
    public sealed record ManifestData
    {
        [JsonProperty(PropertyName = "encrypted")]
        public bool Encrypted { get; set; }

        [JsonProperty(PropertyName = "first_run")]
        public bool FirstRun { get; set; }

        [JsonProperty(PropertyName = "entries")]
        public List<ManifestEntryData>? Entries { get; set; }

        [JsonProperty(PropertyName = "periodic_checking")]
        public bool PeriodicChecking { get; set; }

        [JsonProperty(PropertyName = "periodic_checking_interval")]
        public int PeriodicCheckingInterval { get; set; } = 3000;

        [JsonProperty(PropertyName = "periodic_checking_checkall")]
        public bool PeriodicCheckingCheckall { get; set; }

        [JsonProperty(PropertyName = "auto_confirm_market_transactions")]
        public bool AutoConfirmMarketTransactions { get; set; }

        [JsonProperty(PropertyName = "auto_confirm_trades")]
        public bool AutoConfirmTrades { get; set; }
    }
}
