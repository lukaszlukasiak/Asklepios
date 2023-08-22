using Asklepios.Core.Models;
using Asklepios.Data.DBContexts;
using Asklepios.Data.InMemoryContexts;
using Asklepios.Data.Interfaces;
using Asklepios.Web.Areas.HomeArea.Models;
using Asklepios.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Asklepios.Web.Areas.HomeArea.Controllers
{
    [Area("HomeArea")]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        IHomeModuleRepository _context { get; set; }
        SignInManager<User> _signManager { get; set; }
        IPasswordHasher<User> _passwordHasher { get; set; }
        UserManager<User> _userManager { get; set; }
        RoleManager<IdentityRole<long>> _roleManager { get; set; }

        public HomeController(IHomeModuleRepository context, SignInManager<User> signManager, UserManager<User> usrMgr, IPasswordHasher<User> passwordHash, RoleManager<IdentityRole<long>> roleMgr)
        {

            //SeedIdentity();
            //PatientMockDB.SetData();
            _userManager = usrMgr;
            _passwordHasher = passwordHash;
            _signManager = signManager;
            _roleManager = roleMgr;
            _context = context;
        }
        public async Task<IActionResult> LogOutAsync()
        {           
            await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "HomeArea" });
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public async Task<IActionResult> Index()
        {

            //User appUser = new User
            //{
            //    UserName = "uzyszkodnik",
            //    Email = "uzyszkodnik@gmail.com"
            //};

            //IdentityResult result = await _userManager.CreateAsync(appUser, "Haselko2020!");


            //await ServiceClasses.IdentitySeeder.Seed(_signManager, _userManager, _context);
            //await ServiceClasses.IdentitySeeder.SeedRoles(_roleManager, _context);
            //await ServiceClasses.IdentitySeeder.SeedRoles(_roleManager, _context);
            //await ServiceClasses.IdentitySeeder.AddRolesToUsers(_userManager,_roleManager, _context);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Locations()
        {
            LocationsViewModel model = new LocationsViewModel
            {
                //  HttpContext.SignInAsync();
                Locations = _context.GetAllLocations().ToList()
            };
            //foreach (Location item in model.Locations)
            //{
            //    item.Services= _context.GetLocationServices(item.Id);
            //}
            return View(model);
        }
        public IActionResult LogIn()
        {
            LogInViewModel model = new LogInViewModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogInPatient(LogInViewModel model)
        {
            User user = _context.CheckUserNameAndRole(model.UserName, Core.Enums.WorkerModuleType.CustomerServiceModule, Core.Enums.UserType.Patient);
            model.UserType = Core.Enums.UserType.Patient;

            if (user != null)
            {
               // Patient patient = _context.Get(userId);

               // TempData["User"] = JsonConvert.SerializeObject(user);
                //SignIn();
                //check if user is existing 
                //var user2 = await _userManager.FindByEmailAsync(user.Email); //or .FindByNameAsync(username_here)


                //check if pwd is valid
               // var pwd_is_valid = _userManager.CheckPasswordAsync(user2, model.Password);
               
                //_signManager.SignIn(user, this);
                var result1 = await _signManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                
                //var result2 = await _signManager.PasswordSignInAsync(user.UserName, model.User.PasswordHash, false, false);
                //var result3 = await _signManager.PasswordSignInAsync(user.Email, user.PasswordHash, false, false);
                //var result4 = await _signManager.PasswordSignInAsync(user.Email, model.User.PasswordHash, false, false);

               // var result5  = await _signManager.PasswordSignInAsync("uzyszkodnik", "Haselko2020!", false, false);
              //  await _signManager.SignInAsync(user, false);

                if (result1.Succeeded)
                {
                    return RedirectToAction("Index", "Patient", new { area = "PatientArea" });
                }
                else
                {
                    model.LogInFailed = true;
                    return View("LogIn", model);
                }

            }
            else
            {
                
            }
            model.LogInFailed = true;
            return View("LogIn", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogInEmployee(LogInViewModel model)
        {
            if (model==null)
            {
                return View("LogIn", model);
            }
            //long userId = model.User.Id;
            model.UserType = Core.Enums.UserType.Employee;
            User user = _context.CheckUserNameAndRole(model.UserName, model.WorkerModuleType,Core.Enums.UserType.Employee);

            if (user != null)
            {
                user = await _userManager.FindByEmailAsync(user.Email);
                await _signManager.SignOutAsync();

                var result = await _signManager.PasswordSignInAsync(user.UserName, model.Password,false,false);
                //result = await _signManager.PasswordSignInAsync(user, model.User.PasswordHash, false, false);
                //SignInResult signInResult = await _signManager.CheckPasswordSignInAsync(user, model.User.PasswordHash, false);
                //signInResult = await _signManager.CheckPasswordSignInAsync(user, user.PasswordHash, false);
                //bool lol=await _signManager.CanSignInAsync(user);
                //_signManager.
                //await _signManager.SignInAsync(user, false);
                //_signManager.UserManager.
                //long userId = User.Identity.GetUserId<long>();
           //     var id=HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;//
                

                if (result.Succeeded)
                {
                    TempData["User"] = JsonConvert.SerializeObject(user);

                    switch (model.WorkerModuleType)
                    {
                        case Core.Enums.WorkerModuleType.CustomerServiceModule:
                            return RedirectToAction("Index", "CustomerService", new { area = "CustomerServiceArea" });

                        // return RedirectToAction("Index", "CustomerService", new { area = "CustomerServiceArea", id = user.UserId.ToString() });
                        case Core.Enums.WorkerModuleType.AdministrativeWorkerModule:
                            return RedirectToAction("Index", "Administrative", new { area = "AdministrativeArea" });
                        //return RedirectToAction("Index", "Administrative", new { area = "AdministrativeArea", id = user.UserId.ToString() });
                        case Core.Enums.WorkerModuleType.MedicalWorkerModule:
                            return RedirectToAction("Index", "MedicalWorker", new { area = "MedicalWorkerArea" });
                        // return RedirectToAction("Index", "MedicalWorker", new { area = "MedicalWorkerArea", id = user.UserId.ToString() });
                        default:
                            break;
                    }
                }
                else
                {
                    model.LogInFailed = true;
                    return View("LogIn", model);
                }
            }               
            else
            {
                
            }
            model.LogInFailed = true;
            return View("LogIn", model);
        }


        [HttpGet]
        public IActionResult Contact()
        {
            ContactMessageViewModel model = new ContactMessageViewModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Contact(ContactMessageViewModel model)
        {
            //ContactMessageViewModel model = new ContactMessageViewModel();
            bool isSent = ServiceClasses.MailServices.CreateAndSendMail(model);
            if (isSent)
            {
                model = new ContactMessageViewModel();
                model.AlertMessage = "Wiadomość została wysłana!";
                model.AlertMessageType = Enums.AlertMessageType.InfoMessage;

                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Only alert Message');", true);

            }
            else
            {
                model.AlertMessageType = Enums.AlertMessageType.ErrorMessage;
                model.AlertMessage = "Wystąpił błąd podczas próby wysłania wiadomości! Spróbuj jeszcze raz!";
                ViewBag.Message = "Wystąpił błąd podczas próby wysłania wiadomości! Spróbuj jeszcze raz!";
            }
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
