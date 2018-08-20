using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Hubs;
using Web.Services;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();

            services.AddDbContext<ApplicationDbContext>(c =>
              c.UseSqlServer(Configuration.GetConnectionString("CatalogConnection")));

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<IRepository<ChartItem>, EfRepository<ChartItem>>();
            services.AddScoped<IAsyncRepository<Employee>, EfRepository<Employee>>();
            services.AddScoped<NewsDataService>();
            services.AddScoped<EmployeeDataService>();
            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ContextsSeed.SeedUsers(userManager, roleManager).Wait();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSignalR(routes => {
                routes.MapHub<ChatHub>("/chatHub");
            });
          

            app.UseMvc(routes =>
             {
                 routes.MapRoute(
                   name: null,
                   template: "/Chat",
                   defaults: new { Controller = "Chat", action = "Index" }
                   );
                 routes.MapRoute(
                    name: null,
                    template: "News/Page/{page:int}",
                    defaults: new { Controller = "News", action = "Index" }
                    );
                 routes.MapRoute(
                   name: null,
                   template: "News/Info/{id}",
                   defaults: new { Controller = "News", action = "Info" }
                   );

               

                 routes.MapRoute(
                    name: null,
                    template: "Employers/{position}/Page/{page:int}",
                    defaults: new { Controller = "Account", action = "Index" }
                    );
               

                 routes.MapRoute(
                    name: null,
                    template: "Employers/Page/{page:int}",
                    defaults: new { Controller = "Account", action = "Index", page = 1 }
                    );

                 routes.MapRoute(
                     name: null,
                     template: "Employers/{position}",
                     defaults: new { Controller = "Account", action = "Index", page = 1 }
                     );

                 routes.MapRoute(
                    name: null,
                    template: null,
                    defaults: new { Controller = "Account", action = "Index", page = 1 }
                    );
                 routes.MapRoute(
                    name: null,
                    template: "{controller}/{action}/{id?}");


             });

        }
    }
}
