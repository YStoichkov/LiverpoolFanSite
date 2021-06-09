namespace LiverpoolFanSite.Data.Models
{
    using System.Collections.Generic;

    using LiverpoolFanSite.Data.Common.Models;

    public class Team : BaseDeletableModel<int>
    {
        public Team()
        {
            this.ClubBadge = new HashSet<Image>();
        }

        public string Name { get; set; }

        public int Wins { get; set; }

        public int Draws { get; set; }

        public int Loses { get; set; }

        public int GoalsScored { get; set; }

        public int GoalsAgainst { get; set; }

        public int GoalDifference => this.GoalsScored - this.GoalsAgainst;

        public int Points { get; set; }

        public virtual ICollection<Image> ClubBadge { get; set; }
    }
}
