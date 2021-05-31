namespace LiverpoolFanSite.Web.Controllers
{
    using System;

    using LiverpoolFanSite.Services.Data;
    using LiverpoolFanSite.Web.ViewModels.Categories;
    using LiverpoolFanSite.Web.ViewModels.Forum;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ForumController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IPostsService postsService;

        public ForumController(
            ICategoriesService categoriesService,
            IPostsService postsService)
        {
            this.categoriesService = categoriesService;
            this.postsService = postsService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();
            viewModel.Categories = this.categoriesService.GetAll<IndexCategoryViewModel>();
            return this.View(viewModel);
        }

        public IActionResult ByName(string name, int page = 1)
        {
            const int ItemsPerPage = 5;

            var viewModel = this.categoriesService.GetByName<CategoryViewModel>(name);
            viewModel.ForumPosts = this.postsService.GetByCategoryId<PostInCategoryViewModel>(viewModel.Id, ItemsPerPage, (page - 1) * ItemsPerPage);

            var count = this.postsService.GetCountByCategoryId(viewModel.Id);
            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
            viewModel.CurrentPage = page;
            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Chat()
        {
            return this.View();
        }
    }
}
