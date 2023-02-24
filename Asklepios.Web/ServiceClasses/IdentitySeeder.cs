using Asklepios.Core.Models;
using Asklepios.Data.InMemoryContexts;
using Asklepios.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.ServiceClasses
{
    public static class IdentitySeeder
    {
        internal async static Task Seed(SignInManager<User> signManager, UserManager<User> userManager, IHomeModuleRepository context)
        {
            PatientMockDB.SetData();
            List<User> users = PatientMockDB.Users;

            foreach (User user in users)
            {
                long id = await EnsureUser(userManager,user);
            }
            context.Save();
        }
        internal async static Task Seed1User(SignInManager<User> signManager, UserManager<User> userManager, IHomeModuleRepository context)
        {
            //await userManager.CreateAsync(new User() { }, "haslo11111");

            //long id = await EnsureUser(userManager, "uzyszkodnik");
            //context.Save();
        }

        private static async Task<long> EnsureUser(UserManager<User> userManager, User user)
        {
            User foundUser = null;// await userManager.FindByNameAsync(user.UserName);

            if (foundUser == null)
            {
                foundUser = new User()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    MedicalWorkerId = user.MedicalWorkerId,
                    PatientId=user.PatientId,
                    PersonId=user.PersonId,
                    UserType=user.UserType,
                    WorkerModuleType=user.WorkerModuleType, 
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(foundUser, user.PasswordHash);
            }

            return foundUser.Id;
        }

        internal static async Task SeedRoles(RoleManager<IdentityRole<long>> roleManager, IHomeModuleRepository context)
        {
            string[] roleNames = new string[] { "AdministrativeWorker", "ServiceWorker", "MedicalWorker", "Patient" };

            foreach (string item in roleNames)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole<long>(item));
            }
            
            context.Save();
        }
        internal static async Task AddRolesToUsers(UserManager<User> userManager, RoleManager<IdentityRole<long>> roleManager, IHomeModuleRepository context)
        {
            List<IdentityRole<long>> roles = roleManager.Roles.ToList();
            List<User> users = userManager.Users.Where(c=>c.WorkerModuleType.Value==Core.Enums.WorkerModuleType.AdministrativeWorkerModule).ToList();
            

            foreach (var item in users)
            {
                IdentityResult result  = await userManager.AddToRoleAsync(item, roles[0].Name);
            }

            users = userManager.Users.Where(c => c.WorkerModuleType.Value == Core.Enums.WorkerModuleType.CustomerServiceModule).ToList();
            foreach (var item in users)
            {
                IdentityResult result = await userManager.AddToRoleAsync(item, roles[1].Name);
            }

            users = userManager.Users.Where(c => c.WorkerModuleType.Value == Core.Enums.WorkerModuleType.MedicalWorkerModule).ToList();
            foreach (var item in users)
            {
                IdentityResult result = await userManager.AddToRoleAsync(item, roles[2].Name);
            }

            users = userManager.Users.Where(c => c.UserType.Value == Core.Enums.UserType.Patient).ToList();
            foreach (var item in users)
            {
                IdentityResult result = await userManager.AddToRoleAsync(item, roles[3].Name);
            }
            context.Save();

        }

    }
}
