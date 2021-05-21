namespace LiverpoolFanSite.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LiverpoolFanSite.Web.ViewModels.Players;

    public interface IPlayersService
    {
        Task CreateAsync(CreatePlayerInputModel input, string userId, string imagePath);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        int GetCount();

        T GetById<T>(int id);
    }
}
