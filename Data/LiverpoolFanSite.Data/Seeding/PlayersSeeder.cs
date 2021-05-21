namespace LiverpoolFanSite.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using LiverpoolFanSite.Data.Models;

    public class PlayersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Players.Any())
            {
                return;
            }

            await dbContext.Players.AddAsync(new Player { FirstName = "Alisson", LastName = "Becker", Position = "Goalkeeper", PlaceOfBirth = "Novo Hamburgo", ShirtNumber = "1" });
            await dbContext.Players.AddAsync(new Player { FirstName = "Virgil", LastName = "Van Dijk", Position = "Defender", PlaceOfBirth = "Breda", ShirtNumber = "4" });
            await dbContext.Players.AddAsync(new Player { FirstName = "Mohamed", LastName = "Salah", Position = "Forward", PlaceOfBirth = "Basyoun" });
            await dbContext.Players.AddAsync(new Player { FirstName = "Thiago", LastName = "Alcantara", Position = "Midfielder", PlaceOfBirth = "Italy" });
        }
    }
}
