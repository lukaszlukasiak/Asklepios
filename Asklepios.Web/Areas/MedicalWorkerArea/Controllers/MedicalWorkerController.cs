using Asklepios.Core.Models;
using Asklepios.Data.Interfaces;
using Asklepios.Web.Areas.CustomerServiceArea.Models;
using Asklepios.Web.Areas.MedicalWorkerArea.Models;
using Asklepios.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.MedicalWorkerArea.Controllers
{
    [Area("MedicalWorkerArea")]

    public class MedicalWorkerController : Controller
    {
        private readonly ILogger<MedicalWorkerController> _logger;

        //public MedicalWorkerController(ILogger<MedicalWorkerController> logger)
        //{
        //    _logger = logger;
        //}
        IMedicalWorkerModuleRepository _context;
        public MedicalWorkerController(IMedicalWorkerModuleRepository context)
        {
            _context = context;

        }

        private static User _loggedUser { get; set; }
        private static Person _person { get; set; }
        private static MedicalWorker _medicalWorker { get; set; }
        private static long CurrentVisitId { get; set; }

        internal static void LogOut()
        {
            _loggedUser = null;
            _person = null;
            _medicalWorker = null;

        }
        //public IActionResult LogOut()
        //{
        //    return RedirectToAction("Index", "Home", new { area = "HomeArea" });
        //}
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public IActionResult Index()
        {
            if (_loggedUser == null)
            {
                if (TempData.ContainsKey("User") == true)
                {
                    User user = JsonConvert.DeserializeObject<User>((string)TempData["User"]);
                    _loggedUser = user;
                    _medicalWorker = _context.GetMedicalWorkerByUserId(_loggedUser.PersonId);

                    // User user = TempData["User"] as User;
                }
            }
            else
            {
                return NotFound();
            }
            return RedirectToAction("Dashboard");
        }
        [HttpGet]
        public IActionResult Dashboard()
        {
            if (_loggedUser != null)
            {
                DashboardViewModel model = new DashboardViewModel(_medicalWorker);
                return View(model);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public IActionResult CurrentVisit()
        {
            if (_loggedUser != null)
            {
                Visit visit = _context.GetVisitById(CurrentVisitId);
                CurrentVisitViewModel model = new CurrentVisitViewModel(visit);
                return View(model);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpGet]
        public IActionResult Schedule()
        {
            if (_loggedUser != null)
            {
                Visit visit = _context.GetVisitById(CurrentVisitId);
                CurrentVisitViewModel model = new CurrentVisitViewModel(visit);
                return View(model);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpGet]
        public IActionResult History()
        {
            if (_loggedUser != null)
            {
                Visit visit = _context.GetVisitById(CurrentVisitId);
                CurrentVisitViewModel model = new CurrentVisitViewModel(visit);
                return View(model);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        public IActionResult Contact()
        {
            if (_loggedUser != null)
            {

                ContactMessageViewModel model = new ContactMessageViewModel(_medicalWorker);
                return View(model);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpPost]
        public IActionResult Contact(ContactMessageViewModel model)
        {
            if (_loggedUser != null)
            {

                ContactMessageViewModel modelP = new ContactMessageViewModel(_medicalWorker);
                modelP.Message = model.Message;
                modelP.Subject = model.Subject;
                //ContactMessageViewModel model = new ContactMessageViewModel();
                bool isSent = ServiceClasses.MailServices.CreateAndSendMail(modelP);
                if (isSent)
                {
                    model.AlertMessage = "Wiadomość została wysłana!";
                    model.AlertMessageType = Enums.AlertMessageType.Info;
                }
                else
                {
                    model.AlertMessage = "Wystąpił błąd podczas próby wysłania wiadomości! Spróbuj jeszcze raz!";
                    model.AlertMessageType = Enums.AlertMessageType.Error;
                    ViewBag.Message = "Wystąpił błąd podczas próby wysłania wiadomości! Spróbuj jeszcze raz!";

                }
                return View(model);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public IActionResult Reviews()
        {
            if (_loggedUser != null)
            {
                Visit visit = _context.GetVisitById(CurrentVisitId);
                CurrentVisitViewModel model = new CurrentVisitViewModel(visit);
                return View(model);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpGet]
        public IActionResult Locations()
        {
            if (_loggedUser != null)
            {
                Visit visit = _context.GetVisitById(CurrentVisitId);
                CurrentVisitViewModel model = new CurrentVisitViewModel(visit);
                return View(model);
            }
            else
            {
                return NotFound();
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
