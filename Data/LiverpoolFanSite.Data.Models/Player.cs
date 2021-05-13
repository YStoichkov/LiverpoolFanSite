namespace LiverpoolFanSite.Data.Models
{
    using System;
    using System.Collections.Generic;

    using LiverpoolFanSite.Data.Common.Models;

    public class Player : BaseDeletableModel<int>
    {
        public Player()
        {
            this.Images = new HashSet<Image>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Position { get; set; }

        public string ShirtNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PlaceOfBirth { get; set; }

        public string Description { get; set; }

        public string Signed { get; set; }

        public int Appearances { get; set; }

        public int Goals { get; set; }

        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
