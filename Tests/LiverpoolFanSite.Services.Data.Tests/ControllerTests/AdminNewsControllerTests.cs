namespace LiverpoolFanSite.Services.Data.Tests.ControllerTests
{
    using System;

    using LiverpoolFanSite.Data;
    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Web.Areas.Administration;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class AdminNewsControllerTests
    {
        [Fact]
        public async void IndexShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);

            var controller = new NewsController(dbContext);

            var result = await controller.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void DetailsWithNullAsParameterShouldReturnNotFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);

            var controller = new NewsController(dbContext);

            var result = await controller.Details(null);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void DetailsWithNotExistingNewsShouldReturnNotFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);

            var controller = new NewsController(dbContext);

            var result = await controller.Details(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void DetailsShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);

            var controller = new NewsController(dbContext);

            await dbContext.News.AddAsync(new News { Id = 1, Title = "Test", Content = "Test", AddedByUserId = "1" });
            await dbContext.SaveChangesAsync();

            var result = await controller.Details(1);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void CreateShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);

            var controller = new NewsController(dbContext);

            var result = controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void CreateWithCorrectDataShouldReturnRedirectToActionResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);

            var controller = new NewsController(dbContext);

            var result = await controller.Create(new News { Title = "Test", Id = 2, Content = "Test" });

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async void CreateWithInvalidModelShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);

            var controller = new NewsController(dbContext);
            controller.ModelState.AddModelError("test", "test");
            var result = await controller.Create(new News { Title = "Test", Id = 2, Content = "Test" });

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void EditWithNullForIdShouldReturnNotFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);

            var controller = new NewsController(dbContext);

            var result = await controller.Edit(null);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void EditWithIncorrectIdShouldReturnNotFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);

            var controller = new NewsController(dbContext);

            var result = await controller.Edit(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void EditWithCorrectDataShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);

            var controller = new NewsController(dbContext);

            await dbContext.News.AddAsync(new News { Title = "Test", Id = 1, Content = "Test" });
            await dbContext.SaveChangesAsync();
            var result = await controller.Edit(1);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void EditWithNoNewsShouldReturnNotFoundResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);
            var controller = new NewsController(dbContext);

            var result = await controller.Edit(2, new News
            {
                Title = "Test",
                Id = 1,
                Content = "Test",
            });
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void EditWithIncorrectModelShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);
            var controller = new NewsController(dbContext);
            controller.ModelState.AddModelError("test", "test");
            var result = await controller.Edit(1, new News
            {
                Title = "Test",
                Id = 1,
                Content = "Test",
            });
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void EditWithNoTeamShouldReturnNotFoundResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);
            var controller = new NewsController(dbContext);

            var result = await controller.Edit(1, new News
            {
                Title = "Test",
                Id = 1,
            });
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void EditWithCorrectDataShouldRedirectToAction()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);
            var controller = new NewsController(dbContext);

            var news = new News { Title = "Test", Id = 1 };
            await dbContext.News.AddAsync(news);
            await dbContext.SaveChangesAsync();

            var result = await controller.Edit(1, news);
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async void DeleteWithNullShouldReturnNotFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);
            var controller = new NewsController(dbContext);

            var result = await controller.Delete(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void DeleteWithCorrectInputShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);
            var controller = new NewsController(dbContext);

            await dbContext.News.AddAsync(new News { Title = "Test title", Id = 1 });
            await dbContext.SaveChangesAsync();
            var result = await controller.Delete(1);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void DeleteNotExistingMovieShouldReturnNotFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);
            var controller = new NewsController(dbContext);

            var result = await controller.Delete(1);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void DeleteConfirmedShouldRedirectToAction()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);
            var controller = new NewsController(dbContext);

            var news = new News { Title = "Test", Id = 1 };
            await dbContext.News.AddAsync(news);
            await dbContext.SaveChangesAsync();

            var result = await controller.DeleteConfirmed(1);
            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
