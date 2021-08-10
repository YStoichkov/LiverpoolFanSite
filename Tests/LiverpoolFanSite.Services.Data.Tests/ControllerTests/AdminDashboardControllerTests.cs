namespace LiverpoolFanSite.Services.Data.Tests.ControllerTests
{
    using LiverpoolFanSite.Web.Areas.Administration.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class AdminDashboardControllerTests
    {
        [Fact]
        public void IndexShouldReturnView()
        {
            var mockService = new Mock<ISettingsService>();

            var controller = new DashboardController(mockService.Object);

            var result = controller.Index();

            Assert.IsType<ViewResult>(result);
        }
    }
}
