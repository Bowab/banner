using BannerAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BannerAPI
{
    public class Startup
    {
        // L�sning f�r localhost.
        public static IWebHostEnvironment _webHostingEnvironment;
        
        // F�r att f�renkla f�r mig/er att anv�nda n�r jag l�ser och skriver till den fejkade databasen som
        // bara best�r av json filer.
        public static string GetPath(string path) => System.IO.Path.Combine(_webHostingEnvironment.ContentRootPath, path);

        // H�r injectar vi IWebHostEnvironment f�r att f� tag i root path f�r projektet.
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _webHostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(); // .net core 3.1 vill ha denna om vi vill ha controller med views.

            // Scoped duger d� vi vill ha en ny IBannerService per request.
            services.AddScoped<IBannerService, BannerService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseStaticFiles(); // f�r wwwroot.

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
