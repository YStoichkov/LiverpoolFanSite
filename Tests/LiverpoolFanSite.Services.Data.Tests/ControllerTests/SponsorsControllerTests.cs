namespace LiverpoolFanSite.Services.Data.Tests.ControllerTests
{
    using LiverpoolFanSite.Web.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Xunit;

    public class SponsorsControllerTests
    {
        [Fact]
        public void IndexShouldReturnView()
        {
            var controller = new SponsorsController();

            var result = controller.Index();

            Assert.IsType<ViewResult>(result);
        }
    }
}
