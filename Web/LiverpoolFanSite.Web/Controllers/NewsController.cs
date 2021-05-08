namespace LiverpoolFanSite.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class NewsController : Controller
    {
       public IActionResult Index()
        {
            return this.View();
        }
    }
}
