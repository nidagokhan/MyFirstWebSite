using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyFirstWebSite.Sample.Data.Context;
using MyFirstWebSite.Sample.Data.Interfaces;
using MyFirstWebSite.Sample.Data.Mocks;
using MyFirstWebSite.Sample.Data.Models;
using MyFirstWebSite.Sample.Data.Repositories;
using MyFirstWebSite.Sample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWebSite.Sample
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

            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("conn")));

            services.AddSingleton<DbInitializer>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<ICategoryRepo, CategoryRepository>();
            services.AddTransient<IDrinkRepo, DrinkRepository>();

            services.AddScoped(sp => ShoppingCart.GetCart(sp));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DbInitializer dbInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();//css gibi statik dosyalarý kullanabilmemizi saðlar
            app.UseStatusCodePages();
            app.UseRouting();
            dbInitializer.Seed();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "categoryfilter",
                    pattern: "Drink/{action}/{category?}",
                    defaults: new { Controller = "Drink", action = "List" });
            });
        }
    }
}
