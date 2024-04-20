using System.ComponentModel.DataAnnotations;

namespace TapTap.Models
{
    public class MintAssetFormModel
    {
        [Required]
        public string? Version { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Type { get; set; }

        [Required]
        public long? Amount { get; set; }
    }
}
