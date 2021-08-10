namespace LiverpoolFanSite.Services.Data.Tests.ControllerTests
{
    using System;
    using System.Security.Claims;

    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Web.Controllers;
    using LiverpoolFanSite.Web.ViewModels.Posts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Moq;
    using Xunit;

    public class PostsControllerTests
    {
        [Fact]
        public void CreateShouldReturnView()
        {
            var user = new ApplicationUser { UserName = "Test", Id = "1" };
            var mockPostsService = new Mock<IPostsService>();
            var mockCategoriesService = new Mock<ICategoriesService>();
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

            var controller = new PostsController(mockPostsService.Object, mockUserManager.Object, mockCategoriesService.Object);

            var result = controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void CreateWithParametersShouldReturnView()
        {
            var user = new ApplicationUser { UserName = "Test", Id = "1" };
            var mockPostsService = new Mock<IPostsService>();
            var mockCategoriesService = new Mock<ICategoriesService>();
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
            mockPostsService.Setup(x => x.CreateAsync("Test title", "Test Content", 1, "1"));
            mockUserManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);

            var controller = new PostsController(mockPostsService.Object, mockUserManager.Object, mockCategoriesService.Object);

            var post = new CreatePostInputModel
            {
                CategoryId = 1,
                Content = "Test content",
                Title = "Test title",
            };

            var result = await controller.Create(post);

            Assert.IsType<RedirectResult>(result);
        }

        [Fact]
        public async void CreateWithInvalidModelStateShouldReturnView()
        {
            var user = new ApplicationUser { UserName = "Test", Id = "1" };
            var mockPostsService = new Mock<IPostsService>();
            var mockCategoriesService = new Mock<ICategoriesService>();
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
            mockPostsService.Setup(x => x.CreateAsync("Test title", "Test Content", 1, "1"));
            mockUserManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);

            var controller = new PostsController(mockPostsService.Object, mockUserManager.Object, mockCategoriesService.Object);
            controller.ModelState.AddModelError("test", "test");
            var post = new CreatePostInputModel
            {
                CategoryId = 1,
                Content = "Test content",
                Title = "Test title",
            };

            var result = await controller.Create(post);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ByIdWithWrongInputShouldReturnNotFound()
        {
            var user = new ApplicationUser { UserName = "Test", Id = "1" };
            var mockPostsService = new Mock<IPostsService>();
            var mockCategoriesService = new Mock<ICategoriesService>();
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
            mockPostsService.Setup(x => x.GetById<PostViewModel>(1));
            var controller = new PostsController(mockPostsService.Object, mockUserManager.Object, mockCategoriesService.Object);

            var result = controller.ById(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void ByIdShouldReturnView()
        {
            var mockPostsService = new Mock<IPostsService>();
            var mockCategoriesService = new Mock<ICategoriesService>();
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

            var model = new PostViewModel
            {
                Id = 1,
                Title = "Test title",
                Content = "Test content",
            };
            mockPostsService.Setup(x => x.GetById<PostViewModel>(1)).Returns(model);
            var controller = new PostsController(mockPostsService.Object, mockUserManager.Object, mockCategoriesService.Object);
            var result = controller.ById(model.Id);

            Assert.IsType<ViewResult>(result);
        }
    }
}
