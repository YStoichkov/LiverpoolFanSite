namespace LiverpoolFanSite.Data.Models
{
    using System;

    using LiverpoolFanSite.Data.Common.Models;
    using LiverpoolFanSite.Data.Models.Enums;

    public class StadiumTour : BaseDeletableModel<int>
    {
        public StadiumTour()
        {
            this.TicketPrice = 20;
        }

        public DateTime TourDate { get; set; }

        public string Email { get; set; }

        public int Tickets { get; set; }

        public double TicketPrice { get; set; }

        public double TotalPriceForTour { get; set; }

        public StadiumTourType TourType { get; set; }
    }
}
