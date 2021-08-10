namespace LiverpoolFanSite.Services.Data.Tests.ControllerTests
{
    using System;

    using LiverpoolFanSite.Data;
    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Data.Repositories;
    using LiverpoolFanSite.Web.Areas.Administration.Controllers;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Moq;
    using Xunit;

    public class AdminPlayersControllerTests
    {
        [Fact]
        public async void IndexShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                     .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Player>(new ApplicationDbContext(options.Options));

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

            var controller = new PlayersController(repository, mockUserManager.Object);

            var result = await controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void DetailsWithNullAsParameterShouldReturnNotFoundResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                     .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Player>(new ApplicationDbContext(options.Options));

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

            var controller = new PlayersController(repository, mockUserManager.Object);

            var result = await controller.Details(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void DetailsWithNoPlayerShouldReturnNotFoundResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                     .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Player>(new ApplicationDbContext(options.Options));

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

            var controller = new PlayersController(repository, mockUserManager.Object);

            var result = await controller.Details(1);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void DetailsShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                     .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Player>(new ApplicationDbContext(options.Options));

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

            var controller = new PlayersController(repository, mockUserManager.Object);
            var player = new Player
            {
                FirstName = "Test",
                LastName = "Test",
                Appearances = 2,
                Goals = 2,
                ShirtNumber = "2",
                Position = "GK",
                DateOfBirth = DateTime.UtcNow,
                PlaceOfBirth = "Test",
                Id = 1,
            };
            await repository.AddAsync(player);
            await repository.SaveChangesAsync();
            var result = await controller.Details(1);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void CreateShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                     .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Player>(new ApplicationDbContext(options.Options));

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

            var controller = new PlayersController(repository, mockUserManager.Object);
            var result = controller.Create();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void CreateWIthParameterSouldRedirectToAction()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                     .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Player>(new ApplicationDbContext(options.Options));

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

            var controller = new PlayersController(repository, mockUserManager.Object);
            var result = await controller.Create(new Player { FirstName = "Test" });
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async void CreateWithInvalidInputShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                     .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Player>(new ApplicationDbContext(options.Options));

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

            var controller = new PlayersController(repository, mockUserManager.Object);
            controller.ModelState.AddModelError("test", "test");
            var result = await controller.Create(new Player { FirstName = "Test" });
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void EditWithNullAsIdShouldReturnNotFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                     .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Player>(new ApplicationDbContext(options.Options));

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

            var controller = new PlayersController(repository, mockUserManager.Object);
            var result = await controller.Edit(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void EditWithInvalidModelShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            // options.Option
            var repository = new EfDeletableEntityRepository<Player>(new ApplicationDbContext(options.Options));

            // repository
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

            await repository.AddAsync(new Player { FirstName = "Test", Id = 1 });
            await repository.SaveChangesAsync();
            var controller = new PlayersController(repository, mockUserManager.Object);
            controller.ModelState.AddModelError("test", "test");
            var result = await controller.Edit(1, new Player { FirstName = "test", Id = 1 });
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void EditWithNoPlayerShouldReturnNotFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                     .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Player>(new ApplicationDbContext(options.Options));

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

            var controller = new PlayersController(repository, mockUserManager.Object);
            var result = await controller.Edit(1);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void EditPlayerWithNotExistingPlayerShouldReturnNotFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                     .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Player>(new ApplicationDbContext(options.Options));

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

            var controller = new PlayersController(repository, mockUserManager.Object);
            var result = await controller.Edit(1, new Player { FirstName = "Test", Id = 1 });
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void EditShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                     .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Player>(new ApplicationDbContext(options.Options));

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

            await repository.AddAsync(new Player { FirstName = "Test", Id = 1 });
            await repository.SaveChangesAsync();
            var controller = new PlayersController(repository, mockUserManager.Object);
            var result = await controller.Edit(1);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void EditWithIncorrectPlayerIdShouldReturnNotFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                     .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Player>(new ApplicationDbContext(options.Options));

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

            var player = new Player
            {
                FirstName = "Test",
                Id = 1,
            };

            await repository.AddAsync(player);
            await repository.SaveChangesAsync();
            var controller = new PlayersController(repository, mockUserManager.Object);
            var result = await controller.Edit(2, player);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void EditWithIncorrectModelShouldReturnNotFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                     .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Player>(new ApplicationDbContext(options.Options));

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

            var player = new Player
            {
                FirstName = "Test",
                Id = 1,
            };

            await repository.AddAsync(player);
            await repository.SaveChangesAsync();
            var controller = new PlayersController(repository, mockUserManager.Object);
            var result = await controller.Edit(2, new Player { });
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void EditWithCorrectDataShouldRedirectToAction()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                     .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Player>(new ApplicationDbContext(options.Options));

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

            var player = new Player
            {
                FirstName = "Test",
                Id = 1,
            };

            await repository.AddAsync(player);
            await repository.SaveChangesAsync();
            var controller = new PlayersController(repository, mockUserManager.Object);
            var result = await controller.Edit(1, player);
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async void EditNotExistingPlayerShourReturnNotFoundResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                     .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Player>(new ApplicationDbContext(options.Options));

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

            var player = new Player
            {
                FirstName = "Test",
                Id = 1,
            };

            await repository.AddAsync(player);
            await repository.SaveChangesAsync();
            var controller = new PlayersController(repository, mockUserManager.Object);
            var result = await controller.Edit(2, player);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void DeleteWithIncorrectIdShouldReturnNotFoundResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                     .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Player>(new ApplicationDbContext(options.Options));

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
            var controller = new PlayersController(repository, mockUserManager.Object);
            var result = await controller.Delete(2);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void DeleteWIthNullAsParameterShouldReturnNotFoundResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                     .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Player>(new ApplicationDbContext(options.Options));

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
            var controller = new PlayersController(repository, mockUserManager.Object);
            var result = await controller.Delete(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void DeleteWithCorrectDataShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                     .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Player>(new ApplicationDbContext(options.Options));

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
            var controller = new PlayersController(repository, mockUserManager.Object);
            await repository.AddAsync(new Player { FirstName = "Test", Id = 1 });
            await repository.SaveChangesAsync();
            var result = await controller.Delete(1);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void DeleteConfirmedShouldRedirectToAction()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                     .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<Player>(new ApplicationDbContext(options.Options));

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
            var controller = new PlayersController(repository, mockUserManager.Object);
            await repository.AddAsync(new Player { FirstName = "Test", Id = 1 });
            await repository.SaveChangesAsync();
            var result = await controller.DeleteConfirmed(1);
            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
