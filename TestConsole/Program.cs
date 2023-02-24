using Asklepios.Core.Models;
using Asklepios.Data.InMemoryContexts;
using Asklepios.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TestConsole
{
    public  class Progam
    {
        public static void Main(string[] args)
        {
            PatientMockDB.SetData();
            List<User> users = PatientMockDB.Users;



            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<Asklepios.Data.DBContexts.AsklepiosDbContext>();
                    //context.Database.Migrate();

                    var config = host.Services.GetRequiredService<IConfiguration>();
                    var userList = config.GetSection("userList").Get<List<string>>();
                    //context.entity
                    //SeedData.Initialize(services, userList).Wait();
                }
                catch (Exception ex)
                {
                    //var logger = services.GetRequiredService<ILogger<Program>>();
                    //logger.LogError(ex, "An error occurred adding users.");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static async Task Initialize(IServiceProvider serviceProvider,
                                    List<User> userList)
        {
            var userManager = serviceProvider.GetService<UserManager<User>>();

            foreach (var user in userList)
            {
                var userPassword = user.PasswordHash;//GenerateSecurePassword();
                var userId = await EnsureUser(userManager, user);

                //NotifyUser(userName, userPassword);
            }
        }

        private static async Task<long> EnsureUser(UserManager<User> userManager, User user)
        {
            var foundUser = await userManager.FindByNameAsync(user.UserName);

            if (foundUser == null)
            {
                foundUser = new User()
                {
                    UserName=user.UserName,

                    EmailConfirmed = true
                };
                await userManager.CreateAsync(foundUser, user.PasswordHash);
            }

            return foundUser.Id;
        }


    }
}

