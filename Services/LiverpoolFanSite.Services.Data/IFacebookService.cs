namespace LiverpoolFanSite.Services.Data
{
    using System;

    using Microsoft.Extensions.Configuration;

    public interface IFacebookService
    {
        public IConfiguration GetConfiguration();

        public Uri RedirectUri();
    }
}
