namespace LiverpoolFanSite.Web.Areas.Administration.Controllers
{
    using LiverpoolFanSite.Common;
    using LiverpoolFanSite.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
