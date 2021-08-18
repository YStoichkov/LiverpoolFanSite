namespace LiverpoolFanSite.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult SiteError(int errorCode)
        {
            return this.View();
        }

        public IActionResult Anfield()
        {
            return this.View();
        }

        public IActionResult Shop()
        {
            return this.Redirect("https://store.liverpoolfc.com/");
        }

        public IActionResult Gallery()
        {
            return this.View();
        }

        public IActionResult History()
        {
            return this.View();
        }
    }
}
