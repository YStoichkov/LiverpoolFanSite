// namespace LiverpoolFanSite.Services.Data
// {
//    using System;

// using Microsoft.Extensions.Configuration;
//    using Microsoft.Extensions.Logging;

// public class FacebookService : IFacebookService
//    {
//        string appId = string.Empty;
//        string appSecret = string.Empty;

// private readonly ILogger<FacebookService> _logger;

// public FacebookService(ILogger<FacebookService> logger)
//        {
//            this._logger = logger;
//            //var configuration = this.GetConfiguration();
//            this.appId = configuration.GetSection("FacebookLogin:AppId").Value;
//            this.appSecret = configuration.GetSection("FacebookLogin:AppSecret").Value;
//        }

// //public IConfiguration GetConfiguration()
//        //{
//        //    var builder = new ConfigurationBuilder().Set
//        //}

// public Uri RedirectUri()
//        {
//            throw new NotImplementedException();
//        }
//    }
// }
