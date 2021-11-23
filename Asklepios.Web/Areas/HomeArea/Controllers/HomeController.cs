using Asklepios.Data.Interfaces;
using Asklepios.Web.Areas.HomeArea.Models;
using Asklepios.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Asklepios.Web.Areas.HomeArea.Controllers
{
    [Area("HomeArea")]

    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        IHomeModuleRepository _context;
        public HomeController(IHomeModuleRepository context)
        {
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
        public IActionResult Index()
        {
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
            LocationsViewModel model = new LocationsViewModel();
            model.Locations = _context.GetAllLocations().ToList();
            return View(model);
        }
        public IActionResult LogIn()
        {
            LogInViewModel model = new LogInViewModel();

            return View(model);
        }
        [HttpPost]
        public IActionResult LogInPatient(LogInViewModel model)
        {
            //RedirectToRoute()
            //return RedirectToAction("Index", "~/PatientArea/PatientController");
            return RedirectToAction("Index", "Patient", new { area = "PatientArea" });
            //return  Redirect("/PatientArea/PatientController/Index");


            //return View("LogIn", , model);
        }
        [HttpPost]
        public IActionResult LogInEmployee(LogInViewModel model)
        {
            switch (model.WorkerModuleType)
            {
                case Core.Enums.WorkerModuleType.CustomerServiceModule:
                    return RedirectToAction("Index", "CustomerServiceWorker", new { area = "CustomerServiceWorkerArea" });
                case Core.Enums.WorkerModuleType.AdministrativeWorkerModule:
                    return RedirectToAction("Index", "AdministrativeWorker", new { area = "AdministrativeWorkerArea" });
                case Core.Enums.WorkerModuleType.MedicalWorkerModule:
                    return RedirectToAction("Index", "MedicalWorker", new { area = "MedicalWorkerArea" });
                default:
                    break;
            }
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
                //ViewBag.Message = "Wiadomość została wysłana!";
                //return RedirectToAction("Index");
                model = new ContactMessageViewModel();
                model.AlertMessage = "Wiadomość została wysłana!";
                model.AlertMessageType = Enums.AlertMessageType.Info;

                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Only alert Message');", true);

            }
            else
            {
                model.AlertMessageType = Enums.AlertMessageType.Error;
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
