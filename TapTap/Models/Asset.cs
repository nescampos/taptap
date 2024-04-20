namespace TapTap.Models
{
    public class Asset
    {
        public string? version { get; set; }
        public string? amount { get; set; }
        public string? script_key { get; set; }
        public string? lease_owner { get; set; }
        public string? lease_expiry { get; set; }
        public GenesisInfo? asset_genesis { get; set; }
        public int? lock_time { get; set; }
        public int? relative_lock_time { get; set; }
        public int? script_version { get; set; }
        public bool? script_key_is_local { get; set; }
        public bool? is_spent { get; set; }
        public bool? is_burn { get; set; }
    }
}
