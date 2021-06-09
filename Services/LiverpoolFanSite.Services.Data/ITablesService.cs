namespace LiverpoolFanSite.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LiverpoolFanSite.Web.ViewModels.Teams;

    public interface ITablesService
    {
        Task CreateTeamAsync(CreateTeamInputModel input, string userId, string imagePath);

        IEnumerable<T> GetAll<T>();
    }
}
