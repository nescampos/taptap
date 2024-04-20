namespace TapTap.Models
{
    public class MintAssetViewModel
    {
        public MintAssetFormModel Form { get; set; }
        public string? Error { get; set; }

        public MintAssetViewModel()
        {
            Form = new MintAssetFormModel();
        }
    }
}
