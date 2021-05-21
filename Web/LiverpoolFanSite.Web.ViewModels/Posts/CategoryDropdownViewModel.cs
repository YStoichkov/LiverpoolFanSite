namespace LiverpoolFanSite.Web.ViewModels.Posts
{
    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Services.Mapping;

    public class CategoryDropdownViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
