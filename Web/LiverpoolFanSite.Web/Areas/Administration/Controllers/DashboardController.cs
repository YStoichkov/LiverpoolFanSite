﻿namespace LiverpoolFanSite.Web.Areas.Administration.Controllers
{
    using LiverpoolFanSite.Services.Data;
    using LiverpoolFanSite.Web.ViewModels.Administration.Dashboard;

    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;

        public DashboardController(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount(), };
            return this.View(viewModel);
        }
    }
}
