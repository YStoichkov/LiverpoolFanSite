namespace LiverpoolFanSite.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Services.Data;
    using LiverpoolFanSite.Web.ViewModels.Teams;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class TablesController : Controller
    {
        private readonly ITablesService tablesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public TablesController(
            ITablesService tablesService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.tablesService = tablesService;
            this.userManager = userManager;
            this.environment = environment;
        }

        public IActionResult PremierLeague()
        {
            var viewModel = new TeamsListViewModel
            {
                Teams = this.tablesService.GetAll<TeamInListViewModel>(),
            };
            return this.View(viewModel);
        }

        public IActionResult ChampionsLeague()
        {
            return this.View();
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateTeamInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            try
            {
                await this.tablesService.CreateTeamAsync(input, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            return this.RedirectToAction("PremierLeague");
        }
    }
}
