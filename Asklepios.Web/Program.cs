using Asklepios.Core.Models;
using Asklepios.Data.DBContexts;
using Asklepios.Data.InMemoryContexts;
using Asklepios.Data.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
string connString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddDbContext<AsklepiosDbContext>(options =>
{
    options.UseSqlServer(connString);
});



builder.Services.AddIdentity<User, IdentityRole<long>>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedEmail = false;

})  
    .AddRoles<IdentityRole<long>>()
    .AddEntityFrameworkStores<AsklepiosDbContext>();

builder.Services.AddRazorPages();

builder.Services.AddScoped<IHomeModuleRepository, AsklepiosDbContext>();
builder.Services.AddScoped<IPatientModuleRepository, AsklepiosDbContext>();
builder.Services.AddScoped<IMedicalWorkerModuleRepository, AsklepiosDbContext>();
builder.Services.AddScoped<ICustomerServiceModuleRepository, AsklepiosDbContext>();
builder.Services.AddScoped<IAdministrationModuleRepository, AsklepiosDbContext>();

//builder.Services.Configure<CookiePolicyOptions>(options =>
//{
//    options.ConsentCookie.IsEssential = true;
//    options.CheckConsentNeeded = context => false;
//    options.MinimumSameSitePolicy = SameSiteMode.None;
//});


builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/HomeArea/Home/Login";
    options.Cookie.Name = "CiasteczkoAsklepios";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    options.LoginPath = "/HomeArea/Home/Login";
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.SlidingExpiration = true;
});

builder.Services.AddAuthentication();
//    CookieAuthenticationDefaults.AuthenticationScheme)
//.AddCookie(options =>
//{
//    options.Cookie.IsEssential = true;//<--NOTE THIS
//    options.Cookie.HttpOnly = true;
//    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
//    options.Cookie.SameSite = SameSiteMode.None;
//});

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(20));

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
        


        var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/HomeArea/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();


app.UseRouting();

app.UseCookiePolicy();

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute("areas", "{area:exists}/{controller:exists}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{area=HomeArea}/{controller=Home}/{action=Index}/{id?}");
});


app.Run();
