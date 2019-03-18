﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Mvc_Basics_Assignment.Controllers;
using Mvc_Basics_Assignment.Models;

namespace Mvc_Basics_Assignment
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(60);
                options.Cookie.HttpOnly = true;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
            });

            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            // This creates custom routing. The 2nd "routes.MapRoute is the standard in this case, while the first is a custom
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute("FeverCheck", "FeverCheck",
                    defaults: new { controller = "FeverCheck", action = "FeverCheck" });
                routes.MapRoute("GuessingGame", "GuessingGame",
                    defaults: new { controller = "GuessingGame", action = "GuessingGame" });
                routes.MapRoute("Home", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
