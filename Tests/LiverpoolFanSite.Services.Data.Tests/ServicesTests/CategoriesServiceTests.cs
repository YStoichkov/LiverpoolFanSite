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

    public class CategoriesServiceTests
    {
        [Fact]
        public void TestGetAllCategoriesWithNoCaterogies()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                    .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));

            var categoriesService = new CategoriesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(CategoriesServiceTests.MyTest).Assembly);

            Assert.Empty(categoriesService.GetAll<CategoriesServiceTests.MyTest>(1));
        }

        [Fact]
        public void TestGetAllCategoriesWithSingleCategory()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                   .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));

            repository.AddAsync(new Category { Name = "TestCategory" });
            repository.SaveChangesAsync();
            var categoriesService = new CategoriesService(repository);
            AutoMapperConfig.RegisterMappings(typeof(CategoriesServiceTests.MyTest).Assembly);
            var result = categoriesService.GetAll<CategoriesServiceTests.MyTest>();
            Assert.Single(result);
        }

        [Fact]
        public void TestGetByName()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                  .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Category>(new ApplicationDbContext(options.Options));

            repository.AddAsync(new Category { Name = "TestCategory" });
            repository.SaveChangesAsync();
            var categoriesService = new CategoriesService(repository);
            var result = categoriesService.GetByName<CategoriesServiceTests.MyTest>("TestCategory");
            Assert.Equal("TestCategory", result.Name);
        }

        public class MyTest : IMapFrom<Category>
        {
            public string Name { get; set; }
        }
    }
}
