namespace LiverpoolFanSite.Services.Data.Tests.ControllerTests
{
    using System;

    using LiverpoolFanSite.Data;
    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Data.Repositories;
    using LiverpoolFanSite.Web.Controllers;
    using LiverpoolFanSite.Web.ViewModels.StadiumTours;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class StadiumToursControllerTests
    {
        [Fact]
        public void BookTourShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<StadiumTour>(new ApplicationDbContext(options.Options));
            var controller = new StadiumToursController(repository);

            var result = controller.BookTour();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void TourConfirmationShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
              .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<StadiumTour>(new ApplicationDbContext(options.Options));
            var controller = new StadiumToursController(repository);

            var result = controller.TourConfirmation();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void BookTourShouldRedirectToAction()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<StadiumTour>(new ApplicationDbContext(options.Options));
            var controller = new StadiumToursController(repository);

            var stadiumTour = new StadiumTourInputModel
            {
                Email = "test@test.bg",
                Tickets = 5,
            };

            var result = await controller.BookTour(stadiumTour);
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async void BookTourWithIncorrectInputShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repository = new EfDeletableEntityRepository<StadiumTour>(new ApplicationDbContext(options.Options));
            var controller = new StadiumToursController(repository);
            controller.ModelState.AddModelError("test", "test");

            var result = await controller.BookTour(new StadiumTourInputModel { });
            Assert.IsType<ViewResult>(result);
        }
    }
}
