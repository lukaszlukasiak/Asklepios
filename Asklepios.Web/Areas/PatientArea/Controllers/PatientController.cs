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
        public IActionResult Contact()
        {
            return View();
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
