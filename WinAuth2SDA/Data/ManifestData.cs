using System.Text.Json.Serialization;

namespace WinAuth2SDA.Data
{
    public sealed record ManifestData
    {
        [JsonPropertyName("encrypted")]
        public bool Encrypted { get; set; }

        [JsonPropertyName("first_run")]
        public bool FirstRun { get; set; }

        [JsonPropertyName("entries")]
        public List<ManifestEntryData>? Entries { get; set; }

        [JsonPropertyName("periodic_checking")]
        public bool PeriodicChecking { get; set; }

        [JsonPropertyName("periodic_checking_interval")]
        public int PeriodicCheckingInterval { get; set; }

        [JsonPropertyName("periodic_checking_checkall")]
        public bool PeriodicCheckingCheckall { get; set; }

        [JsonPropertyName("auto_confirm_market_transactions")]
        public bool AutoConfirmMarketTransactions { get; set; }

        [JsonPropertyName("auto_confirm_trades")]
        public bool AutoConfirmTrades { get; set; }
    }
}
