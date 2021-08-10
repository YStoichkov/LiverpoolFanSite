namespace LiverpoolFanSite.Services.Data.Tests.ControllerTests
{
    using LiverpoolFanSite.Web.Controllers;
    using LiverpoolFanSite.Web.ViewModels.Images;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class GalleryControllerTests
    {
        [Fact]
        public void UploadImageShouldReturnView()
        {
            var mockService = new Mock<ICloudinaryService>();

            var controller = new GalleryController(mockService.Object);

            var result = controller.UploadImage();

            Assert.IsType<ViewResult>(result);
        }
    }
}
