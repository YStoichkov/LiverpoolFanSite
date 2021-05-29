namespace LiverpoolFanSite.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Services.Data;
    using LiverpoolFanSite.Web.ViewModels.Players;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class TeamsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPlayersService playersService;
        private readonly IWebHostEnvironment environment;

        public TeamsController(
            UserManager<ApplicationUser> userManager,
            IPlayersService playersService,
            IWebHostEnvironment environment)
        {
            this.userManager = userManager;
            this.playersService = playersService;
            this.environment = environment;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePlayerInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            try
            {
                await this.playersService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            return this.RedirectToAction("Create");
        }

        public IActionResult All(int id = 1)
        {
            const int ItemsPerPage = 12;
            var viewModel = new PlayersListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                PlayersCount = this.playersService.GetCount(),
                Players = this.playersService.GetAll<PlayersInListViewModel>(id, ItemsPerPage),
            };
            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var player = this.playersService.GetById<SinglePlayerViewModel>(id);
            return this.View(player);
        }

        public IActionResult Search(string searchTerm)
        {
            var player = this.playersService.Search<SinglePlayerViewModel>(searchTerm);
            if (player==null)
            {
                return this.RedirectToAction("All");
            }

            return this.View(player);
        }
    }
}
