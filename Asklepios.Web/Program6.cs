using Asklepios.Data.InMemoryContexts;
using Asklepios.Data.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddControllersWithViews(options =>
//{
//    options.CacheProfiles.Add("Caching", new CacheProfile()
//    {
//        Duration = 120,
//        Location = ResponseCacheLocation.Any,
//        VaryByHeader = "cookie"
//    });
//    options.CacheProfiles.Add("NoCaching", new CacheProfile()
//    {
//        NoStore = true,
//        Location = ResponseCacheLocation.None
//    });

//});
builder.Services.AddScoped<IHomeModuleRepository, HomeInMemoryContext>();
builder.Services.AddScoped<IPatientModuleRepository, PatientInMemoryContext>();
builder.Services.AddScoped<IMedicalWorkerModuleRepository, MedicalWorkerInMemoryContext>();
builder.Services.AddScoped<ICustomerServiceModuleRepository, CustomerServiceInMemoryContext>();
builder.Services.AddScoped<IAdministrationModuleRepository, AdministrationInMemoryContext>();

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapBlazorHub();

app.MapFallbackToPage("/app/{*catchall}", "/App/Index");

app.Run();
