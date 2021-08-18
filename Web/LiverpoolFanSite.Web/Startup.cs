namespace LiverpoolFanSite.Web
{
    using System.Reflection;

    using CloudinaryDotNet;
    using LiverpoolFanSite.Data;
    using LiverpoolFanSite.Data.Common;
    using LiverpoolFanSite.Data.Common.Repositories;
    using LiverpoolFanSite.Data.Models;
    using LiverpoolFanSite.Data.Repositories;
    using LiverpoolFanSite.Data.Seeding;
    using LiverpoolFanSite.Services.Data;
    using LiverpoolFanSite.Services.Mapping;
    using LiverpoolFanSite.Services.Messaging;
    using LiverpoolFanSite.Web.Hubs;
    using LiverpoolFanSite.Web.ViewModels;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    }).AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

            Account account = new Account(
                this.configuration["Cloudinary:Name"],
                this.configuration["Cloudinary:ApiKey"],
                this.configuration["Cloudinary:ApiSecret"]);

            Cloudinary cloudinary = new Cloudinary(account);
            services.AddSingleton(cloudinary);

            services.AddSingleton(this.configuration);
            services.AddSignalR();

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Add SignalR
            services.AddSignalR();

            // Application services
            services.AddTransient<IEmailSender>(
                serviceProvider => new SendGridEmailSender(this.configuration["SendGrid:ApiKey"]));
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IPlayersService, PlayersService>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<IPostsService, PostsService>();

            services.AddTransient<ICommentsService, CommentsService>();
            services.AddTransient<INewsService, NewsService>();
            services.AddTransient<ITablesService, TablesService>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddAuthentication();

            services.AddAuthentication()
                .AddFacebook(options =>
                {
                    options.AppId = this.configuration["FacebookLogin:AppId"];
                    options.AppSecret = this.configuration["FacebookLogin:AppSecret"];
                    options.Fields.Add("picture");
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseStatusCodePagesWithRedirects("/Home/SiteError?errorCode={0}");
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/Home/SiteError?errorCode={0}");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapHub<ChatHub>("forum/chathub");
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("forumCategory", "lfc/{name:minlength(3)}", new { controller = "Forum", action = "ByName" });
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
