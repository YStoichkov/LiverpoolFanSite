namespace LiverpoolFanSite.Web.ViewModels.Players
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreatePlayerInputModel
    {
        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string ShirtNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MinLength(3)]
        public string PlaceOfBirth { get; set; }

        [Required]
        [MinLength(20)]
        public string Description { get; set; }

        [Required]
        public string Signed { get; set; }

        [Required]
        public int Appearances { get; set; }

        [Required]
        public int Goals { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }
    }
}
