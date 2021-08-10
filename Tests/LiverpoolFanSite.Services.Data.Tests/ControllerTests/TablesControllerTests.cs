namespace LiverpoolFanSite.Services.Data.Tests.ControllerTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Claims;
    using System.Text;

    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Web.Controllers;
    using LiverpoolFanSite.Web.ViewModels.Teams;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Moq;
    using Xunit;

    public class TablesControllerTests
    {
        [Fact]
        public void PremierLeagueShouldReturnView()
        {
            var mockService = new Mock<ITablesService>();
            mockService.Setup(x => x.GetAll<TeamInListViewModel>());

            var mockEnvironment = new Mock<IWebHostEnvironment>();

            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                   new Mock<IUserStore<ApplicationUser>>().Object,
                   new Mock<IOptions<IdentityOptions>>().Object,
                   new Mock<IPasswordHasher<ApplicationUser>>().Object,
                   new IUserValidator<ApplicationUser>[0],
                   new IPasswordValidator<ApplicationUser>[0],
                   new Mock<ILookupNormalizer>().Object,
                   new Mock<IdentityErrorDescriber>().Object,
                   new Mock<IServiceProvider>().Object,
                   new Mock<ILogger<UserManager<ApplicationUser>>>().Object);
            var controller = new TablesController(mockService.Object, mockUserManager.Object, mockEnvironment.Object);

            var result = controller.PremierLeague();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ChampionsLeagueShouldReturnView()
        {
            var mockService = new Mock<ITablesService>();
            mockService.Setup(x => x.GetAll<TeamInListViewModel>());

            var mockEnvironment = new Mock<IWebHostEnvironment>();

            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                   new Mock<IUserStore<ApplicationUser>>().Object,
                   new Mock<IOptions<IdentityOptions>>().Object,
                   new Mock<IPasswordHasher<ApplicationUser>>().Object,
                   new IUserValidator<ApplicationUser>[0],
                   new IPasswordValidator<ApplicationUser>[0],
                   new Mock<ILookupNormalizer>().Object,
                   new Mock<IdentityErrorDescriber>().Object,
                   new Mock<IServiceProvider>().Object,
                   new Mock<ILogger<UserManager<ApplicationUser>>>().Object);
            var controller = new TablesController(mockService.Object, mockUserManager.Object, mockEnvironment.Object);

            var result = controller.ChampionsLeague();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void CreateShouldReturnView()
        {
            var mockService = new Mock<ITablesService>();
            mockService.Setup(x => x.GetAll<TeamInListViewModel>());

            var mockEnvironment = new Mock<IWebHostEnvironment>();

            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                   new Mock<IUserStore<ApplicationUser>>().Object,
                   new Mock<IOptions<IdentityOptions>>().Object,
                   new Mock<IPasswordHasher<ApplicationUser>>().Object,
                   new IUserValidator<ApplicationUser>[0],
                   new IPasswordValidator<ApplicationUser>[0],
                   new Mock<ILookupNormalizer>().Object,
                   new Mock<IdentityErrorDescriber>().Object,
                   new Mock<IServiceProvider>().Object,
                   new Mock<ILogger<UserManager<ApplicationUser>>>().Object);
            var controller = new TablesController(mockService.Object, mockUserManager.Object, mockEnvironment.Object);

            var result = controller.Create();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void CreateShouldRedirectToAction()
        {
            var user = new ApplicationUser { UserName = "Test", Id = "1" };
            var mockService = new Mock<ITablesService>();
            mockService.Setup(x => x.GetAll<TeamInListViewModel>());

            var mockEnvironment = new Mock<IWebHostEnvironment>();

            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                   new Mock<IUserStore<ApplicationUser>>().Object,
                   new Mock<IOptions<IdentityOptions>>().Object,
                   new Mock<IPasswordHasher<ApplicationUser>>().Object,
                   new IUserValidator<ApplicationUser>[0],
                   new IPasswordValidator<ApplicationUser>[0],
                   new Mock<ILookupNormalizer>().Object,
                   new Mock<IdentityErrorDescriber>().Object,
                   new Mock<IServiceProvider>().Object,
                   new Mock<ILogger<UserManager<ApplicationUser>>>().Object);

            mockUserManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
            var controller = new TablesController(mockService.Object, mockUserManager.Object, mockEnvironment.Object);

            var image = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.jpg");
            var images = new List<IFormFile>();
            images.Add(image);
            var team = new CreateTeamInputModel
            {
                Name = "Test Team",
                ClubBadge = images,
            };
            var result = await controller.Create(team);

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async void CreateShouldReturnViewWhenExceptionIsTrhrown()
        {
            var mockService = new Mock<ITablesService>();
            mockService.Setup(x => x.GetAll<TeamInListViewModel>());

            var mockEnvironment = new Mock<IWebHostEnvironment>();

            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                   new Mock<IUserStore<ApplicationUser>>().Object,
                   new Mock<IOptions<IdentityOptions>>().Object,
                   new Mock<IPasswordHasher<ApplicationUser>>().Object,
                   new IUserValidator<ApplicationUser>[0],
                   new IPasswordValidator<ApplicationUser>[0],
                   new Mock<ILookupNormalizer>().Object,
                   new Mock<IdentityErrorDescriber>().Object,
                   new Mock<IServiceProvider>().Object,
                   new Mock<ILogger<UserManager<ApplicationUser>>>().Object);

            var controller = new TablesController(mockService.Object, mockUserManager.Object, mockEnvironment.Object);

            var image = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.jpg");
            var images = new List<IFormFile>();
            images.Add(image);
            var team = new CreateTeamInputModel
            {
                Name = "Test Team",
                ClubBadge = images,
            };
            var result = await controller.Create(null);

            Assert.IsType<ViewResult>(result);
        }
    }
}
