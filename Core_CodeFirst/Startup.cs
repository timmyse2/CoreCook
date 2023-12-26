using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore; //sql


namespace Core_CodeFirst
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

            //::@Timmy
            //string connectString
            services.AddDbContext<BlogDbContext>(
                options =>
                    options.UseSqlServer(
                    Configuration.GetConnectionString("BlogDbContext")
                    ));

            services.AddDbContext<ComicDbContext>(
                options =>
                    options.UseSqlServer(
                    //Configuration.GetConnectionString("ComicDbContext")
                    Configuration.GetConnectionString("BlogDbContext")
                    ));
            
            //<Timmy><2023.12.8><add session>
            services.AddDistributedMemoryCache();            
            services.AddSession(
                options =>
                {
                    options.Cookie.Name = ".AdventureWorks.Session";
                    //options.IdleTimeout = TimeSpan.FromSeconds(10);
                    options.IdleTimeout = TimeSpan.FromMinutes(15); //15 min
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                });
            //<><><>

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            //<Timmy><2023.12.8><add session>
            app.UseSession();
            app.Use(async(context, next) =>
            {
                context.Session.SetString("SessionKey", "SessoinValue");
                await next.Invoke();
            });
            //<><><>

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
