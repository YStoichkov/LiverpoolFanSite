namespace LiverpoolFanSite.Web.ViewModels.Players
{
    using System.Linq;

    using AutoMapper;
    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Services.Mapping;

    public class PlayersInListViewModel : IMapFrom<Player>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Position { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Player, PlayersInListViewModel>()
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(x => x.Images.FirstOrDefault().RemoteImageUrl != null ?
                      x.Images.FirstOrDefault().RemoteImageUrl :
                      "/images/players/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}
