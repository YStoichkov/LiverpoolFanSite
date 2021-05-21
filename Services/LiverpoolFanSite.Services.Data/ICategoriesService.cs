namespace LiverpoolFanSite.Services.Data
{
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name, int skip = 0, int? take = null);
    }
}
