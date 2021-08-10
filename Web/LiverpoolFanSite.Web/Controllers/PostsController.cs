namespace LiverpoolFanSite.Web.Controllers
{
    using System.Threading.Tasks;

    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Services.Data;
    using LiverpoolFanSite.Web.ViewModels.Posts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class PostsController : Controller
    {
        private readonly IPostsService postsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICategoriesService categoriesService;

        public PostsController(
            IPostsService postsService,
            UserManager<ApplicationUser> userManager,
            ICategoriesService categoriesService)
        {
            this.postsService = postsService;
            this.userManager = userManager;
            this.categoriesService = categoriesService;
        }

        [Authorize]
        public IActionResult Create()
        {
            var categories = this.categoriesService.GetAll<CategoryDropdownViewModel>();
            var viewModel = new CreatePostInputModel
            {
                Categories = categories,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreatePostInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var postId = await this.postsService.CreateAsync(inputModel.Title, inputModel.Content, inputModel.CategoryId, user.Id);
            return this.Redirect("/Forum");
        }

        public IActionResult ById(int id)
        {
            var post = this.postsService.GetById<PostViewModel>(id);
            if (post == null)
            {
                return this.NotFound();
            }

            return this.View(post);
        }
    }
}
