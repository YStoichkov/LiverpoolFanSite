namespace LiverpoolFanSite.Web.ViewModels.StadiumTours
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Data.Models.Enums;
    using LiverpoolFanSite.Services.Mapping;

    public class StadiumTourInputModel : IMapFrom<StadiumTour>
    {
        public int UserId { get; set; }

        public ApplicationUser User { get; set; }

        [DataType(DataType.Date)]
        public DateTime TourDate { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Range(1, 20)]
        public int Tickets { get; set; }

        public double TicketPrice { get; set; }

        public double TotalPriceForTour { get; set; }

        public StadiumTourType TourType { get; set; }
    }
}
