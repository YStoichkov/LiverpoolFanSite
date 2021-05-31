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
    using LiverpoolFanSite.Web.ViewModels.Players;

    public class PlayersService : IPlayersService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Player> playerRepository;

        public PlayersService(IDeletableEntityRepository<Player> playerRepository)
        {
            this.playerRepository = playerRepository;
        }

        public async Task CreateAsync(CreatePlayerInputModel input, string userId, string imagePath)
        {
            var player = new Player
            {
                Appearances = input.Appearances,
                DateOfBirth = input.DateOfBirth,
                Description = input.Description,
                FirstName = input.FirstName,
                LastName = input.LastName,
                Goals = input.Goals,
                PlaceOfBirth = input.PlaceOfBirth,
                Position = input.Position,
                ShirtNumber = input.ShirtNumber,
                Signed = input.Signed,
            };

            // /wwwroot/images/recipes/jhdsi-343g3h453-=g34g.jpg
            Directory.CreateDirectory($"{imagePath}/players/");
            foreach (var image in input.Images)
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
                player.Images.Add(dbImage);

                var physicalPath = $"{imagePath}/players/{dbImage.Id}.{extension}";
                using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }

            await this.playerRepository.AddAsync(player);
            await this.playerRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12)
        {
            var players = this.playerRepository.AllAsNoTracking()
                .OrderBy(x => x.FirstName)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
            return players;
        }

        public T GetById<T>(int id)
        {
            var player = this.playerRepository.AllAsNoTracking().Where(x => x.Id == id)
                  .To<T>().FirstOrDefault();
            return player;
        }

        public int GetCount()
        {
            return this.playerRepository.All().Count();
        }

        public T Search<T>(string search)
        {
            var splittedSearch = search.Split(" ");

            // if (splittedSearch.Length == 1)
            // {
            //    var player = this.playerRepository.AllAsNoTracking().Where(x => x.FirstName == splittedSearch[0] || x.LastName == splittedSearch[0]).To<T>().FirstOrDefault();
            //    return player;
            // }
            // else
            // {
            //    var player = this.playerRepository.AllAsNoTracking().Where(x => x.FirstName == splittedSearch[0] && x.LastName == splittedSearch[1]).To<T>().FirstOrDefault();
            //    return player;
            // }
            var player = this.playerRepository.AllAsNoTracking().Where(x => x.FirstName.Contains(search) || x.LastName.Contains(search)).To<T>().FirstOrDefault();
            return player;
        }
    }
}
