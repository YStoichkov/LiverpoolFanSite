namespace LiverpoolFanSite.Web.Controllers
{
    using System.Threading.Tasks;

    using LiverpoolFanSite.Services.Data;
    using LiverpoolFanSite.Web.ViewModels.Images;
    using Microsoft.AspNetCore.Mvc;

    public class GalleryController : Controller
    {
        private readonly ICloudinaryService cloudinaryService;

        public GalleryController(ICloudinaryService cloudinaryService)
        {
            this.cloudinaryService = cloudinaryService;
        }

        //public IActionResult All()
        //{
        //    return this.View();
        //}

        public IActionResult UploadImage()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(UploadImageViewModel model)
        {
            var imageUrl = await this.cloudinaryService.UploadFormFileAsync(model.Image);
            return this.RedirectToAction("All");
        }

        public async Task<IActionResult> All(AllImagesViewModel viewModel)
        {
            var images = await this.cloudinaryService.GetListAsync();
            var model = new AllImagesViewModel
            {
                RemoteImageUrl = images,
            };
            return this.View(model);
        }
    }
}
