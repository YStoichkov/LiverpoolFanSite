namespace LiverpoolFanSite.Web.ViewModels.Images
{
    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class UploadImageViewModel
    {
        public string RemoteImageUrl { get; set; }

        public IFormFile Image { get; set; }
    }
}
