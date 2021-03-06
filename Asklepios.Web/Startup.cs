using Asklepios.Data.InMemoryContexts;
using Asklepios.Data.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            //services.
            services.AddControllersWithViews(options =>
            {
                options.CacheProfiles.Add("Caching", new CacheProfile()
                {
                    Duration = 120,
                    Location = ResponseCacheLocation.Any,
                    VaryByHeader = "cookie"
                });
                options.CacheProfiles.Add("NoCaching", new CacheProfile()
                {
                    NoStore = true,
                    Location = ResponseCacheLocation.None
                });

            });
            services.AddScoped<IHomeModuleRepository, HomeInMemoryContext>();
            services.AddScoped<IPatientModuleRepository, PatientInMemoryContext>();
            services.AddScoped<IMedicalWorkerModuleRepository, MedicalWorkerInMemoryContext>();
            services.AddScoped<ICustomerServiceModuleRepository, CustomerServiceInMemoryContext>();
            services.AddScoped<IAdministrationModuleRepository, AdministrationInMemoryContext>();


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

            app.UseAuthorization();

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
