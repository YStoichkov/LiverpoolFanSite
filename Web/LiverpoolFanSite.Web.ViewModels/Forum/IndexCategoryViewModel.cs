namespace LiverpoolFanSite.Web.ViewModels.Forum
{
    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Services.Mapping;

    public class IndexCategoryViewModel : IMapFrom<Category>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int PostsCount { get; set; }

        public string Url => $"/lfc/{this.Name.Replace(' ', '-')}";
    }
}
