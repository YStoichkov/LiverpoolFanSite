namespace LiverpoolFanSite.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using LiverpoolFanSite.Data.Common.Repositories;
    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Services.Mapping;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesService;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Category> query =
            this.categoriesService.All().OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetByName<T>(string name, int skip = 0, int? take = null)
        {
            var category = this.categoriesService.All().Where(x => x.Name == name)
                .To<T>().FirstOrDefault();
            return category;
        }
    }
}
