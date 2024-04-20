namespace TapTap.Models
{
    public class MintAsset
    {
        public string? asset_version { get; set; }
        public string? asset_type { get; set; }
        public string? name { get; set; }
        public string? amount { get; set; }
        public string? group_key { get; set; }
        public string? group_anchor { get; set; }
        public AssetMeta? asset_meta { get; set; }
        public bool? new_grouped_asset { get; set; }
        public bool? grouped_asset { get; set; }
    }
}
