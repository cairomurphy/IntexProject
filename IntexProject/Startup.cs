using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntexProject.Models;
using IntexProject.Models.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace IntexProject
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
            services.AddControllersWithViews();

            //services.AddIdentity<IdentityUser, IdentityRole>()
            //     .AddEntityFrameworkStores<AppIdentityDBContext>();

            // Sets the display of the Cookie Consent banner (/Pages/Shared/_CookieConsentPartial.cshtml).
            // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            services.Configure<CookiePolicyOptions>(options => {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });

            services.AddDbContext<CrashDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("CrashDbConnection"));

            });

            services.AddScoped<ICrashRepository, EFCrashRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseCookiePolicy();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "Paging",
                    pattern: "Page-{pageNum}",
                    defaults: new { Controller = "Home", action = "Index", pageNum = 1 });
            });
        }
    }
}

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//        "typepage",
//        "{projectType}/Page-{pageNum}",
//        new { Controller = "Home", action = "Index" });

//    endpoints.MapControllerRoute(
//        name: "Paging",
//        pattern: "Page-{pageNum}",
//        defaults: new { Controller = "Home", action = "Index", pageNum = 1 });

//    endpoints.MapControllerRoute(
//        "type",
//        "{projectType}",
//        new { Controller = "Home", action = "Index", pageNum = 1 });

//    endpoints.MapDefaultControllerRoute();

//    endpoints.MapRazorPages();
//});