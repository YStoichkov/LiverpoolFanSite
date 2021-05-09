namespace LiverpoolFanSite.Web.Controllers
{
    using System.Threading.Tasks;

    using LiverpoolFanSite.Data.Common.Repositories;
    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Web.ViewModels.Contacts;
    using Microsoft.AspNetCore.Mvc;

    public class ContactsController : Controller
    {
        private readonly IRepository<ContactForm> contactsRepository;

        public ContactsController(IRepository<ContactForm> contactsRepository)
        {
            this.contactsRepository = contactsRepository;
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

            return this.RedirectToAction("ThankYou");
        }
    }
}
