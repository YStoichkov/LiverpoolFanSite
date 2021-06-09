namespace LiverpoolFanSite.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using LiverpoolFanSite.Data.Common.Repositories;
    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Services.Mapping;
    using LiverpoolFanSite.Web.ViewModels.Teams;

    public class TablesService : ITablesService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Team> teamsRepository;

        public TablesService(IDeletableEntityRepository<Team> teamsRepository)
        {
            this.teamsRepository = teamsRepository;
        }

        public async Task CreateTeamAsync(CreateTeamInputModel input, string userId, string imagePath)
        {
            var team = new Team
            {
               Name = input.Name,
            };

            Directory.CreateDirectory($"{imagePath}/teams/");
            foreach (var image in input.ClubBadge)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');
                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                var dbImage = new Image
                {
                    AddedByUserId = userId,
                    Extension = extension,
                };
                team.ClubBadge.Add(dbImage);

                var physicalPath = $"{imagePath}/teams/{dbImage.Id}.{extension}";
                using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }

            await this.teamsRepository.AddAsync(team);
            await this.teamsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<Team> query =
               this.teamsRepository.All().OrderByDescending(x => x.Points);

            return query.To<T>().ToList();
        }
    }
}
