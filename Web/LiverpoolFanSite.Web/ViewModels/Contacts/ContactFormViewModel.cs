namespace LiverpoolFanSite.Web.ViewModels.Contacts
{
    using System.ComponentModel.DataAnnotations;

    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Services.Mapping;

    public class ContactFormViewModel : IMapFrom<ContactForm>
    {
        [Required(ErrorMessage = "Please enter your names")]
        [Display(Name = "Your Names")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter title")]
        [Display(Name = "Message title")]
        public string Title { get; set; }

        [Required(ErrorMessage ="Please enter the content of your message")]
        [Display(Name ="Content")]
        public string Message { get; set; }
    }
}
