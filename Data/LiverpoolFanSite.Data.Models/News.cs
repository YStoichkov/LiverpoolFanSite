namespace LiverpoolFanSite.Data.Models
{
    using System.Collections.Generic;

    using LiverpoolFanSite.Data.Common.Models;

    public class News : BaseDeletableModel<int>
    {
        public News()
        {
            this.Tags = new HashSet<NewsTag>();
        }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<NewsTag> Tags { get; set; }
    }
}
