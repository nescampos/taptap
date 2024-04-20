using Microsoft.AspNetCore.Mvc.Rendering;

namespace TapTap.Models
{
    public class MintAssetViewModel
    {
        public MintAssetFormModel Form { get; set; }
        public string? Error { get; set; }
        public List<SelectListItem> Versions { get; set; }
        public List<SelectListItem> Types { get; set; }

        public MintAssetViewModel()
        {
            Form = new MintAssetFormModel();
            CreateVersions();
            CreateTypes();
        }

        private void CreateVersions()
        {
            Versions = new List<SelectListItem>();
            Versions.Add(new SelectListItem { Value = "0", Text="Version 0" });
            Versions.Add(new SelectListItem { Value = "1", Text = "Version 1" });
        }

        private void CreateTypes()
        {
            Types = new List<SelectListItem>();
            Types.Add(new SelectListItem { Value = "0", Text = "Normal" });
            Types.Add(new SelectListItem { Value = "1", Text = "Collectible" });
        }
    }
}
