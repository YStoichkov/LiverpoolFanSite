namespace LiverpoolFanSite.Web.ViewModels.Teams
{
    using System.Linq;

    using AutoMapper;
    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Services.Mapping;

    public class TeamInListViewModel : IMapFrom<Team>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Wins { get; set; }

        public int Draws { get; set; }

        public int Loses { get; set; }

        public int GoalsScored { get; set; }

        public int GoalsAgainst { get; set; }

        public int Difference => this.GoalsScored - this.GoalsAgainst;

        public int PlayedGames => this.Wins + this.Draws + this.Loses;

        public int Points { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Team, TeamInListViewModel>()
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(x => x.ClubBadge.FirstOrDefault().RemoteImageUrl != null ?
                      x.ClubBadge.FirstOrDefault().RemoteImageUrl :
                      "/images/teams/" + x.ClubBadge.FirstOrDefault().Id + "." + x.ClubBadge.FirstOrDefault().Extension));
        }
    }
}
