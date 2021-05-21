namespace LiverpoolFanSite.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LiverpoolFanSite.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Category.Any())
            {
                return;
            }

            var categories = new List<string> { "News", "Players", "Music", "COVID-19", "Games" };
            foreach (var category in categories)
            {
                await dbContext.Category.AddAsync(new Category
                {
                    Name = category,
                    Description = category,
                    Title = category,
                });
            }
        }
    }
}
