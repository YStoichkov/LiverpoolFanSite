namespace LiverpoolFanSite.Web.ViewModels.Players
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;
    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Services.Mapping;

    public class SinglePlayerViewModel : IMapFrom<Player>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Position { get; set; }

        public string ShirtNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public string PlaceOfBirth { get; set; }

        public string Description { get; set; }

        public string Signed { get; set; }

        public int Appearances { get; set; }

        public int Goals { get; set; }

        public string ImageUrl { get; set; }

        public string OriginalUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Player, SinglePlayerViewModel>()
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(x => x.Images.FirstOrDefault().RemoteImageUrl != null ?
                x.Images.FirstOrDefault().RemoteImageUrl :
                "/images/players/" + x.Images.FirstOrDefault().Id + "." + x.Images.FirstOrDefault().Extension));
        }
    }
}
