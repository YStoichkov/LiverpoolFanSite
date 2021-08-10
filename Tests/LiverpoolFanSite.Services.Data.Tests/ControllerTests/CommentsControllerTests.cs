namespace LiverpoolFanSite.Services.Data.Tests.ControllerTests
{
    using System;
    using System.Security.Claims;

    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Web.Controllers;
    using LiverpoolFanSite.Web.ViewModels.Comments;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Moq;
    using Xunit;

    public class CommentsControllerTests
    {
        [Fact]
        public async void CreateShouldReturnRedirect()
        {
            var user = new ApplicationUser { UserName = "Test", Id = "1" };
            var mockService = new Mock<ICommentsService>();
            mockService.Setup(x => x.IsInSamePost(1, 2));
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
            var controller = new CommentsController(mockService.Object, mockUserManager.Object);

            var inputModel = new CreateCommentInputModel
            {
                PostId = 1,
                Content = "Test",
            };
            mockService.Setup(x => x.Create(inputModel.PostId, user.Id, inputModel.Content, inputModel.ParentId));
            var result = await controller.Create(inputModel);
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async void CreateWithIncorrectInputShouldReturnBadRequest()
        {
            var user = new ApplicationUser { UserName = "Test", Id = "1", Email = "test@test.bg", PhoneNumber = "111111111" };
            var mockService = new Mock<ICommentsService>();
            mockService.Setup(x => x.IsInSamePost(1, 2));
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
            var controller = new CommentsController(mockService.Object, mockUserManager.Object);

            var inputModel = new CreateCommentInputModel
            {
                PostId = 1,
                Content = "Test",
                ParentId = 2,
            };
            mockService.Setup(x => x.Create(inputModel.PostId, user.Id, inputModel.Content, inputModel.ParentId));
            var result = await controller.Create(inputModel);
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
