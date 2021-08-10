namespace LiverpoolFanSite.Services.Data.Tests.ControllerTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Claims;
    using System.Text;

    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Web.Controllers;
    using LiverpoolFanSite.Web.ViewModels.News;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Moq;
    using Xunit;

    public class NewsControllerTests
    {
        [Fact]
        public void IndexShouldReturnView()
        {
            var mockService = new Mock<INewsService>();
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

            var controller = new NewsController(mockService.Object, mockUserManager.Object, mockEnvironment.Object);

            var result = controller.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void CreateShouldReturnView()
        {
            var mockService = new Mock<INewsService>();
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

            var controller = new NewsController(mockService.Object, mockUserManager.Object, mockEnvironment.Object);

            var result = controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void AllShouldReturnView()
        {
            var mockService = new Mock<INewsService>();
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

            var controller = new NewsController(mockService.Object, mockUserManager.Object, mockEnvironment.Object);

            var result = controller.All();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ByIdShouldReturnView()
        {
            var mockService = new Mock<INewsService>();
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
            mockService.Setup(x => x.GetById<SingleNewsViewModel>(1));
            var controller = new NewsController(mockService.Object, mockUserManager.Object, mockEnvironment.Object);

            var result = controller.ById(1);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void CreateShouldRedirectToAction()
        {
            var user = new ApplicationUser { UserName = "Test", Id = "1" };
            var mockService = new Mock<INewsService>();
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
            mockService.Setup(x => x.GetById<SingleNewsViewModel>(1));
            var controller = new NewsController(mockService.Object, mockUserManager.Object, mockEnvironment.Object);
            var image = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.jpg");
            var images = new List<IFormFile>();
            images.Add(image);
            var model = new CreateNewsInputModel
            {
                Content = "Test Content",
                Title = "Test Title",
                Images = images,
            };
            mockService.Setup(x => x.CreateAsync(
                new CreateNewsInputModel
                {
                    Content = "Test Content",
                    Title = "Test Title",
                    Images = images,
                },
                "1",
                "images/all"));

            var result = controller.Create(model);
            Assert.NotNull(result);
        }
    }
}
