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
               viewModel.Email,
               viewModel.Name,
               GlobalConstants.SystemEmail,
               viewModel.Title,
               viewModel.Message);
            return this.RedirectToAction("ThankYou");
        }

        public IActionResult ThankYou()
        {
            return this.View();
        }

        // [HttpPost]
        // public async Task<IActionResult> SendToEmail()
        // {
        //    var html = new StringBuilder();
        //    html.AppendLine("<h1>Title</h1>");
        //    html.AppendLine("<h3>Category</h1>");
        //    html.AppendLine("<h1>Description</h1>");
        //    html.AppendLine("<p>Testing Sendgrid</p>");
        //    await this.emailSender.SendEmailAsync("testing@abv.bg", "testName", "hijoha8280@geekale.com", "testing", html.ToString());
        //    return this.Redirect("/");
        // }
    }
}
