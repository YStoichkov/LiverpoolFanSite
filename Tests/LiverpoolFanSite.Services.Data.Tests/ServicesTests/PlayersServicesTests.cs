namespace LiverpoolFanSite.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using LiverpoolFanSite.Data;
    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Data.Repositories;
    using LiverpoolFanSite.Services.Mapping;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class PlayersServicesTests
    {
        [Fact]
        public void TestGetRepositoryCountWithZeroPlayersAdded()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Player>(new ApplicationDbContext(options.Options));

            var playersService = new PlayersService(repository);

            Assert.Equal(0, playersService.GetCount());
        }

        [Fact]
        public void TestCreatePlayerAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Player>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Player { FirstName = "Mohamed", LastName = "Salah" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();
            var playerService = new PlayersService(repository);

            Assert.Equal(1, playerService.GetCount());
        }

        [Fact]
        public void TestGetAllPlayersTestWithOnePlayer()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                    .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Player>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Player { FirstName = "Mohamed", LastName = "Salah" }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var playerService = new PlayersService(repository);
            var result = playerService.GetAll<PlayersServicesTests.MyTest>(1);
            Assert.Single(result);
        }

        [Fact]
        public void TestGetAllPlayersTestWithZeroPlayers()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                    .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Player>(new ApplicationDbContext(options.Options));

            var playerService = new PlayersService(repository);
            AutoMapperConfig.RegisterMappings(typeof(PlayersServicesTests.MyTest).Assembly);

            Assert.Empty(playerService.GetAll<PlayersServicesTests.MyTest>(1));
        }

        [Fact]
        public void TestGetPlayerById()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                    .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Player>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Player { FirstName = "Mohamed", LastName = "Salah", Id = 2 }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var playerService = new PlayersService(repository);
            AutoMapperConfig.RegisterMappings(typeof(PlayersServicesTests.MyTest).Assembly);

            var player = playerService.GetById<PlayersServicesTests.MyTest>(2);

            Assert.Equal("Mohamed", player.FirstName);
        }

        [Fact]
        public void TestGetPlayerByIdWithNotExistingId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                    .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Player>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Player { FirstName = "Mohamed", LastName = "Salah", Id = 2 }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var playerService = new PlayersService(repository);
            AutoMapperConfig.RegisterMappings(typeof(PlayersServicesTests.MyTest).Assembly);

            Assert.Null(playerService.GetById<MyTest>(3));
        }

        [Fact]
        public void TestSearchWithIncorrectInput()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                    .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Player>(new ApplicationDbContext(options.Options));
            repository.AddAsync(new Player { FirstName = "Mohamed", LastName = "Salah", Id = 2 }).GetAwaiter().GetResult();
            repository.SaveChangesAsync().GetAwaiter().GetResult();

            var playerService = new PlayersService(repository);
            AutoMapperConfig.RegisterMappings(typeof(PlayersServicesTests.MyTest).Assembly);

            Assert.Null(playerService.Search<MyTest>("Sadio Mane"));
        }

        public class MyTest : IMapFrom<Player>
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }
        }
    }
}
