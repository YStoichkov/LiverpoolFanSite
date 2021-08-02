namespace LiverpoolFanSite.Services.Data.Tests
{
    using System;
    using System.Linq;

    using LiverpoolFanSite.Data;
    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Data.Repositories;
    using LiverpoolFanSite.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class PostsServiceTests
    {
        [Fact]
        public async void TestCreateAsyncPost()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));

            var postsService = new PostsService(repository);

            await postsService.CreateAsync("TestPost", "TestContent", 1, "TestUserId");
            AutoMapperConfig.RegisterMappings(typeof(MyTest).Assembly);
            Assert.Equal(1, repository.All().Count());
        }

        [Fact]
        public void TestGetById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                         .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));

            var postsService = new PostsService(repository);
            postsService.CreateAsync("TestPost", "TestContent", 1, "TestUserId").GetAwaiter().GetResult();
            var post = postsService.GetById<PostsServiceTests.MyTest>(1);
            AutoMapperConfig.RegisterMappings(typeof(MyTest).Assembly);
            Assert.Equal("TestPost", post.Title);
        }

        [Fact]
        public void TestGetByCategoryId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));

            var postsService = new PostsService(repository);

            postsService.CreateAsync("TestPost", "TestContent", 1, "TestUserId").GetAwaiter().GetResult();
            postsService.CreateAsync("TestPost2", "TestContent2", 1, "TestUserId2").GetAwaiter().GetResult();

            var result = postsService.GetByCategoryId<PostsServiceTests.MyTest>(1);
            var resultTwo = postsService.GetByCategoryId<PostsServiceTests.MyTest>(2);

            Assert.Equal(2, result.Count());
            Assert.Empty(resultTwo);
        }

        [Fact]
        public void TestGetCountByCategoryId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Post>(new ApplicationDbContext(options.Options));

            var postsService = new PostsService(repository);

            postsService.CreateAsync("TestPost", "TestContent", 1, "TestUserId").GetAwaiter().GetResult();
            postsService.CreateAsync("TestPost2", "TestContent2", 1, "TestUserId2").GetAwaiter().GetResult();

            var result = postsService.GetCountByCategoryId(1);
            var resultTwo = postsService.GetCountByCategoryId(9);

            Assert.Equal(2, result);
            Assert.Equal(0, resultTwo);
        }

        public class MyTest : IMapFrom<Post>
        {
            public string Title { get; set; }

            public string Content { get; set; }

            public string UserId { get; set; }

            public int CategoryId { get; set; }
        }
    }
}
