using Asklepios.Data.DBContexts;
using Asklepios.Data.InMemoryContexts;
using Asklepios.Data.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Authentication.Cookies;

using System;
using Asklepios.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Asklepios.Web
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
            services.AddDbContext<AsklepiosDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("Webio"), options => options.EnableRetryOnFailure()), ServiceLifetime.Scoped);

            
            services.AddIdentity<User,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddRoles<IdentityRole<long>>()
                    .AddSignInManager()
                    .AddEntityFrameworkStores<AsklepiosDbContext>();

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/HomeArea/Home/Login";
                options.Cookie.Name = "CiasteczkoAsklepios";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.LoginPath = "/HomeArea/Home/Login";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });
            services.AddDistributedMemoryCache();
            services.AddSession(options=>options.IdleTimeout=TimeSpan.FromMinutes(20));

            services.AddControllersWithViews(options =>
            {
                options.CacheProfiles.Add("Caching", new CacheProfile()
                {
                    Duration = 120,
                    Location = ResponseCacheLocation.Any,
                    VaryByHeader = "cookie"
                });
            });
            services.AddScoped<IHomeModuleRepository, AsklepiosDbContext>();
            services.AddScoped<IPatientModuleRepository, AsklepiosDbContext>();
            services.AddScoped<IMedicalWorkerModuleRepository, AsklepiosDbContext>();
            services.AddScoped<ICustomerServiceModuleRepository, AsklepiosDbContext>();
            services.AddScoped<IAdministrationModuleRepository, AsklepiosDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("Start.html");
            app.UseDefaultFiles(options);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("areas", "{area:exists}/{controller:exists}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area=HomeArea}/{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{area=HomeArea}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
