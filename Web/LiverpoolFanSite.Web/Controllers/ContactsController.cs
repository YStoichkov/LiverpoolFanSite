namespace LiverpoolFanSite.Web.Controllers
{
    using System.Text;
    using System.Threading.Tasks;

    using LiverpoolFanSite.Common;
    using LiverpoolFanSite.Data.Common.Repositories;
    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Services.Messaging;
    using LiverpoolFanSite.Web.ViewModels.Contacts;
    using Microsoft.AspNetCore.Mvc;

    public class ContactsController : Controller
    {
        private readonly IRepository<ContactForm> contactsRepository;
        private readonly IEmailSender emailSender;

        public ContactsController(
            IRepository<ContactForm> contactsRepository,
            IEmailSender emailSender)
        {
            this.contactsRepository = contactsRepository;
            this.emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactFormViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            var contactForm = new ContactForm
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Title = viewModel.Title,
                Message = viewModel.Message,
            };
            await this.contactsRepository.AddAsync(contactForm);
            await this.contactsRepository.SaveChangesAsync();

            await this.emailSender.SendEmailAsync(
               GlobalConstants.SystemEmail,
               GlobalConstants.SystemName,
               viewModel.Email,
               viewModel.Title,
               viewModel.Message);
            return this.RedirectToAction("ThankYou");
        }

        public IActionResult ThankYou()
        {
            return this.View();
        }
    }
}
