namespace LiverpoolFanSite.Data.Models
{
    using System;

    using LiverpoolFanSite.Data.Common.Models;

    public class Player : BaseDeletableModel<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Position { get; set; }

        public string ShirtNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PlaceOfBirth { get; set; }

        public string Description { get; set; }
    }
}
