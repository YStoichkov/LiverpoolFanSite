namespace LiverpoolFanSite.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Services.Data;
    using LiverpoolFanSite.Web.ViewModels.News;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class NewsController : Controller
    {
        private readonly INewsService newsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public NewsController(
            INewsService newsService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.newsService = newsService;
            this.userManager = userManager;
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
        public async Task<IActionResult> Create(CreateNewsInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            try
            {
                await this.newsService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}/images");
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
            var viewModel = new NewsListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                PlayersCount = this.newsService.GetCount(),
                News = this.newsService.GetAll<NewsInListViewModel>(id, ItemsPerPage),
            };
            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var recipe = this.newsService.GetById<SingleNewsViewModel>(id);
            return this.View(recipe);
        }
    }
}
