using InAndOut.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut
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
            // This is the dbConetextconfiguration
            services.AddDbContext<ApplicationDBContext>(options =>
            // this is the default server, or connection that is used
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            // This is basically the dependency injection container, 
            // where the services that come in. 
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // The hsts settings are highly cacheable / not great for development
                app.UseHsts();
            }
            // These are are middleware, the pipeline uses:
            // this influences how the whole response works. 
            app.UseHttpsRedirection();
            // This refers to anything inside the wwwroot folder. 
            // This is more or less like a public assets folder where you put your js / css / images
            app.UseStaticFiles();

            // Routing this is what executes the controller & actions. 
            // 
            app.UseRouting();

            app.UseAuthorization();
            // Additional middleware would go here. 
           
            // This is the endpoint or final response. 
            // The Id on the end is optional. 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
