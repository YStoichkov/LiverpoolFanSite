namespace LiverpoolFanSite.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class StadiumToursController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
