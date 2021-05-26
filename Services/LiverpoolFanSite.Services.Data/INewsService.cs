namespace LiverpoolFanSite.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LiverpoolFanSite.Web.ViewModels.News;

    public interface INewsService
    {
        Task CreateAsync(CreateNewsInputModel input, string userId, string imagePath);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        int GetCount();

        T GetById<T>(int id);
    }
}
