namespace LiverpoolFanSite.Data.Models
{
    using System.Collections.Generic;

    using LiverpoolFanSite.Data.Common.Models;

    public class Table : BaseModel<int>
    {
        public Table()
        {
            this.Teams = new HashSet<Team>();
        }

        public virtual ICollection<Team> Teams { get; set; }
    }
}
