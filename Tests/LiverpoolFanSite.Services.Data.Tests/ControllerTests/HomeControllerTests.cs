namespace LiverpoolFanSite.Services.Data.Tests.ControllerTests
{
    using LiverpoolFanSite.Web.Controllers;
    using LiverpoolFanSite.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Xunit;

    public class HomeControllerTests
    {
        [Fact]
        public void IndexShouldReturnView()
        {
            var controller = new HomeController();

            var result = controller.Index();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void PrivacyShouldReturnView()
        {
            var controller = new HomeController();

            var result = controller.Privacy();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void AnfiledShouldReturnView()
        {
            var controller = new HomeController();

            var result = controller.Anfield();

            Assert.IsType<ViewResult>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public void ShopShouldRedirect()
        {
            var controller = new HomeController();

            var result = controller.Shop();

            Assert.IsType<RedirectResult>(result);
        }

        [Fact]
        public void GalleryShouldReturnView()
        {
            var controller = new HomeController();

            var result = controller.Gallery();

            Assert.IsType<ViewResult>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public void HistoryShouldReturnView()
        {
            var controller = new HomeController();

            var result = controller.History();

            Assert.IsType<ViewResult>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public void SiteErrorShouldreturnError()
        {
            var controller = new HomeController();

            var result = controller.SiteError(404);

            Assert.IsType<ViewResult>(result);
        }
    }
}
