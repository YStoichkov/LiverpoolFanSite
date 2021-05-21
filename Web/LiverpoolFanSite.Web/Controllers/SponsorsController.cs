namespace LiverpoolFanSite.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class SponsorsController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
