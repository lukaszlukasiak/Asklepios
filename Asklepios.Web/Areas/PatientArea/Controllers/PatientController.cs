using Asklepios.Data.Interfaces;
using Asklepios.Web.Areas.HomeArea.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.PatientArea.Controllers
{
    [Area("PatientArea")]

    public class PatientController : Controller
    {
        IPatientModuleRepository _context;
        public PatientController(IPatientModuleRepository context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Locations()
        {
            LocationsViewModel model = new LocationsViewModel();
            model.Locations = _context.GetAllLocations().ToList();
            return View(model);
        }
        public IActionResult BookVisit()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Contact()
        {
            ContactMessageViewModel model = new ContactMessageViewModel(_context.GetPatientData());
            return View(model);
        }
        [HttpPost]
        public IActionResult Contact(ContactMessageViewModel model)
        {

            //ContactMessageViewModel model = new ContactMessageViewModel();
            bool isSent = ServiceClasses.MailServices.CreateAndSendMail(model);
            if (isSent)
            {
                model.AlertMessage = "Wiadomość została wysłana!";
                ViewBag.Message = "Wiadomość została wysłana!";
                //return RedirectToAction("Index");
                model = new ContactMessageViewModel();
                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Only alert Message');", true);

            }
            else
            {
                model.AlertMessage = "Wystąpił błąd podczas próby wysłania wiadomości! Spróbuj jeszcze raz!";
                ViewBag.Message = "Wystąpił błąd podczas próby wysłania wiadomości! Spróbuj jeszcze raz!";

            }
            return View(model);
        }
        public IActionResult MedicalAdvice()
        {
            return View();
        }
        public IActionResult MedicalWorkersRanking()
        {
            return View();
        }
        public IActionResult PastVisits()
        {
            return View();
        }
        public IActionResult PlannedVisits()
        {
            return View();
        }
        public IActionResult Prescriptions()
        {
            return View();
        }
        public IActionResult Referrals()
        {
            return View();
        }
        public IActionResult TestResults()
        {
            return View();
        }





    }
}
