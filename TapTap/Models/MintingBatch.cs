namespace TapTap.Models
{
    public class MintingBatch
    {
        public string? batch_key { get; set; }
        public string? batch_txid { get; set; }
        public string? state { get; set; }
        public string? created_at { get; set; }
        public string? batch_psbt { get; set; }

        public PendingAsset[] assets { get; set; }
        public int? height_hint { get; set; }
    }
}
