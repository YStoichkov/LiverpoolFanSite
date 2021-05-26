namespace LiverpoolFanSite.Web.ViewModels.News
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateNewsInputModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        // [Required]
        // public string ImageUrl { get; set; }
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
