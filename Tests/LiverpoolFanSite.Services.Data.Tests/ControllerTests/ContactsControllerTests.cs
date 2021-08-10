namespace LiverpoolFanSite.Services.Data.Tests.ControllerTests
{
    using System;

    using LiverpoolFanSite.Data;
    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Data.Repositories;
    using LiverpoolFanSite.Services.Messaging;
    using LiverpoolFanSite.Web.Controllers;
    using LiverpoolFanSite.Web.ViewModels.Contacts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class ContactsControllerTests
    {
        [Fact]
        public void IndexShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var mockEmailSender = new Mock<IEmailSender>();
            var repository = new EfRepository<ContactForm>(new ApplicationDbContext(options.Options));
            var controller = new ContactsController(repository, mockEmailSender.Object);

            var result = controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ThankYouShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var mockEmailSender = new Mock<IEmailSender>();
            var repository = new EfRepository<ContactForm>(new ApplicationDbContext(options.Options));
            var controller = new ContactsController(repository, mockEmailSender.Object);

            var result = controller.ThankYou();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void IndexShouldRedirectToAction()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var mockEmailSender = new Mock<IEmailSender>();
            var repository = new EfRepository<ContactForm>(new ApplicationDbContext(options.Options));
            var controller = new ContactsController(repository, mockEmailSender.Object);

            var model = new ContactFormViewModel
            {
                Email = "Test Email",
                Message = "Test message",
                Name = "Test name",
                Title = "Test title",
                RecaptchaValue = "testValue",
            };
            var result = await controller.Index(model);
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async void IndexWithInvalidModelShouldReturnView()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var mockEmailSender = new Mock<IEmailSender>();
            var repository = new EfRepository<ContactForm>(new ApplicationDbContext(options.Options));
            var controller = new ContactsController(repository, mockEmailSender.Object);
            controller.ModelState.AddModelError("test", "test");
            var model = new ContactFormViewModel
            {
                Email = "Test Email",
                Message = "Test message",
                Name = "Test name",
                Title = "Test title",
            };
            var result = await controller.Index(model);
            Assert.IsType<ViewResult>(result);
        }
    }
}
