namespace LiverpoolFanSite.Data.Models
{
    using System;
    using System.Collections.Generic;

    using LiverpoolFanSite.Data.Common.Models;

    public class News : BaseDeletableModel<int>
    {
        public News()
        {
            this.Tags = new HashSet<NewsTag>();
            this.Images = new HashSet<Image>();
        }

        public string Title { get; set; }

        public string Content { get; set; }

        // public string ImageUrl { get; set; }
        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }

        public virtual ICollection<NewsTag> Tags { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
