namespace LiverpoolFanSite.Data.Models
{
    using LiverpoolFanSite.Data.Common.Models;

    public class ContactFormEntry : BaseModel<int>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Message { get; set; }
    }
}
