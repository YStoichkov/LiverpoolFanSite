namespace LiverpoolFanSite.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class TablesController : Controller
    {
        public IActionResult PremierLeague()
        {
            return this.View();
        }

        public IActionResult ChampionsLeague()
        {
            return this.View();
        }
    }
}
