namespace LiverpoolFanSite.Services.Data.Tests.ControllerTests
{
    using System;

    using LiverpoolFanSite.Data;
    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Web.Areas.Administration.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class AdminTeamsControllerTests
    {
        [Fact]
        public async void IndexShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);
            var controller = new TeamsController(dbContext);

            var result = await controller.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void CreateShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);
            var controller = new TeamsController(dbContext);

            var result = controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void DetailsWithNullShouldReturnNotFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);
            var controller = new TeamsController(dbContext);

            var result = await controller.Details(null);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void DetailsShouldReturnNotFoundResultIfTeamIsNotFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);
            var controller = new TeamsController(dbContext);

            var result = await controller.Details(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void DetailsShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);
            var controller = new TeamsController(dbContext);

            await dbContext.Teams.AddAsync(new Team { Name = "Test Team", Id = 1, Draws = 2, Loses = 1, Wins = 3, Points = 20 });
            await dbContext.SaveChangesAsync();
            var result = await controller.Details(1);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void CreateViewWithParameterShouldReturnRedirectToAction()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);
            var controller = new TeamsController(dbContext);

            var result = await controller.Create(new Team
            {
                Name = "Test",
                Wins = 1,
                Draws = 1,
                Loses = 1,
                GoalsScored = 1,
                GoalsAgainst = 1,
            });
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async void CreateWithInvalidModelShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);
            var controller = new TeamsController(dbContext);
            controller.ModelState.AddModelError("test", "test");
            var result = await controller.Create(new Team
            {
                Name = "Test",
                Wins = 1,
                Draws = 1,
                Loses = 1,
                GoalsScored = 1,
                GoalsAgainst = 1,
            });
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void EditWithNullShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);
            var controller = new TeamsController(dbContext);

            var result = await controller.Edit(null);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void EditWithNoTeamShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);
            var controller = new TeamsController(dbContext);

            var result = await controller.Edit(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void EditShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);
            var controller = new TeamsController(dbContext);

            await dbContext.Teams.AddAsync(new Team
            {
                Name = "Test",
                Wins = 1,
                Draws = 1,
                Loses = 1,
                GoalsScored = 1,
                GoalsAgainst = 1,
                Id = 1,
            });
            await dbContext.SaveChangesAsync();
            var result = await controller.Edit(1);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void EditWithNoTeamShouldReturnNotFoundResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);
            var controller = new TeamsController(dbContext);

            var result = await controller.Edit(1, new Team
            {
                Name = "Test",
                Wins = 1,
                Draws = 1,
                Loses = 1,
                GoalsScored = 1,
                GoalsAgainst = 1,
                Id = 1,
            });
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void EditWithTeamShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);
            var controller = new TeamsController(dbContext);
            var team = new Team { Name = "Test team", Id = 1 };
            await dbContext.Teams.AddAsync(team);
            await dbContext.SaveChangesAsync();
            var result = await controller.Edit(1, team);
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async void EditWithInvalidModelShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);
            var controller = new TeamsController(dbContext);
            controller.ModelState.AddModelError("test", "test");
            var team = new Team { Name = "Test team", Id = 1 };
            await dbContext.Teams.AddAsync(team);
            await dbContext.SaveChangesAsync();
            var result = await controller.Edit(1, team);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void EditWithDifferentIdShouldReturnNotFound()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);
            var controller = new TeamsController(dbContext);
            var team = new Team { Name = "Test team", Id = 1 };
            await dbContext.Teams.AddAsync(team);
            await dbContext.SaveChangesAsync();
            var result = await controller.Edit(2, team);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void DeleteWithNullAsParameterShouldReturnNotFoundResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);
            var controller = new TeamsController(dbContext);
            var result = await controller.Delete(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void DeleteWithNoTeamShouldReturnNotFoundResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);
            var controller = new TeamsController(dbContext);
            var result = await controller.Delete(1);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void DeleteWithCorrectInputShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);
            var team = new Team { Name = "Test team", Id = 1 };
            await dbContext.Teams.AddAsync(team);
            await dbContext.SaveChangesAsync();
            var controller = new TeamsController(dbContext);
            var result = await controller.Delete(1);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void DeleteConfirmShouldReturnRedirectToAction()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                       .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var dbContext = new ApplicationDbContext(options);
            var team = new Team { Name = "Test team", Id = 1 };
            await dbContext.Teams.AddAsync(team);
            await dbContext.SaveChangesAsync();
            var controller = new TeamsController(dbContext);
            var result = await controller.DeleteConfirmed(1);
            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
