using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Localization;
using WebVueTest.Models;
using WebVueTest.MiddleWare;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;
using System.IO;
using DB.Repositories;
using DB.DBModels;
using DB.Repositories.Task;
using RedisBrowser;

namespace WebVueTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddSession(options =>
            {
                options.Cookie.Name = ".AppCore.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(3600);
                options.Cookie.IsEssential = true;
            });
            services.AddLocalization(option => option.ResourcesPath = "Resources");


            // injection common rep "Task" for db
            services.AddTransient<CommonRepository<DBTask, TaskFilter>, TaskRepository>();

            // injection common rep "Task" for redis 
            //services.AddTransient<CommonRepository<DBTask, TaskFilter>, TaskRedisRepository>();

            /*services.AddIdentity<appUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationContext>();*/
            services.AddSignalR();
            //services.AddMvc()
            //    //.SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
            //    .AddDataAnnotationsLocalization()
            //    .AddViewLocalization();

            services.AddControllersWithViews(mvcOtions =>
            {
                mvcOtions.EnableEndpointRouting = false;
            })
            .AddDataAnnotationsLocalization()
            .AddViewLocalization();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("ru")
                };
                options.DefaultRequestCulture = new RequestCulture("ru");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseMiddleware<LoggerRequest>();
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseRequestLocalization();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //app.UseCookiePolicy();
            app.UseSession();

            app.Use(async (context, next) =>
            {
                if (context.Session.Keys.Contains(appUser.sessionKey))
                {
                }
                var usr = context.Request.Cookies[appUser.sessionKey];
                var rr = context.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];

                //context.Response.Cookies.Append(
                //    CookieRequestCultureProvider.DefaultCookieName,
                //    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(defCultureInfo)),
                //    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                //);
                await next.Invoke();
            });

            app.UseAuthentication();    // подключение аутентификации
            //app.UseAuthorization();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});
            app.UseMvcWithDefaultRoute();

            //добавляем поддержку каталога node_modules
            app.UseFileServer(new FileServerOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, "node_modules")
                ),
                RequestPath = "/node_modules",
                EnableDirectoryBrowsing = false
            });

            app.UseSignalR(routes =>
            {
                routes.MapHub<CommonHub>("/common");
            });

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chat");
            });
        }
    }
}
