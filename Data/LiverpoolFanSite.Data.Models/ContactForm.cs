namespace LiverpoolFanSite.Data.Models
{
    using LiverpoolFanSite.Data.Common.Models;

    public class ContactForm : BaseModel<int>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }
    }
}
