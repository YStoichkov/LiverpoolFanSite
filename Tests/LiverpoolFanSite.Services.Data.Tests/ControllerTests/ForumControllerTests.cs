namespace LiverpoolFanSite.Services.Data.Tests.ControllerTests
{
    using System;
    using System.Collections.Generic;

    using LiverpoolFanSite.Data;
    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Data.Repositories;
    using LiverpoolFanSite.Web.Controllers;
    using LiverpoolFanSite.Web.ViewModels.Categories;
    using LiverpoolFanSite.Web.ViewModels.Forum;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class ForumControllerTests
    {
        [Fact]
        public void ChatShouldReturnView()
        {
            var mockCategoriesService = new Mock<ICategoriesService>();
            var mockPostsService = new Mock<IPostsService>();

            var controller = new ForumController(mockCategoriesService.Object, mockPostsService.Object);

            var result = controller.Chat();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void IndexShouldReturnView()
        {
            var mockCategoriesService = new Mock<ICategoriesService>();
            var mockPostsService = new Mock<IPostsService>();

            var categories = mockCategoriesService.Setup(x => x.GetAll<IndexCategoryViewModel>(2));

            var controller = new ForumController(mockCategoriesService.Object, mockPostsService.Object);
            var result = controller.Index();
            Assert.IsType<ViewResult>(result);
        }
    }
}
