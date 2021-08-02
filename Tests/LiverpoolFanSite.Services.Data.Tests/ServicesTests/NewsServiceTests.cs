namespace LiverpoolFanSite.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using LiverpoolFanSite.Data;
    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Data.Repositories;
    using LiverpoolFanSite.Services.Mapping;
    using LiverpoolFanSite.Web.ViewModels.News;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class NewsServiceTests
    {
        [Fact]
        public void GetCountShouldReturnZero()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<News>(new ApplicationDbContext(options.Options));

            var newsService = new NewsService(repository);

            var result = newsService.GetCount();
            Assert.Equal(0, result);
        }

        [Fact]
        public async void TestCreateAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<News>(new ApplicationDbContext(options.Options));

            var newsService = new NewsService(repository);
            var image = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.jpg");
            var images = new List<IFormFile>();
            images.Add(image);
            var news = new CreateNewsInputModel
            {
                Title = "TestTitle",
                Content = "TestContent",
                Images = images,
            };
            await newsService.CreateAsync(news, "TestUserId", "TestImagePath");
            Assert.Equal(1, repository.All().Count());
        }

        [Fact]
        public async void TestGetAll()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                          .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<News>(new ApplicationDbContext(options.Options));

            var newsService = new NewsService(repository);
            var image = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.jpg");
            var images = new List<IFormFile>();
            images.Add(image);
            var news = new CreateNewsInputModel
            {
                Title = "TestTitle",
                Content = "TestContent",
                Images = images,
            };
            await newsService.CreateAsync(news, "TestUserId", "TestImagePath");
            var result = newsService.GetAll<NewsServiceTests.MyTest>(1);
            Assert.Single(result);
        }

        public class MyTest : IMapFrom<News>
        {
            public int Id { get; set; }

            public string Title { get; set; }

            public string Content { get; set; }
        }
    }
}
