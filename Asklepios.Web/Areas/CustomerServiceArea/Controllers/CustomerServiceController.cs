using Asklepios.Core.Models;
using Asklepios.Data.Interfaces;
using Asklepios.Web.Areas.CustomerServiceArea.Models;
using Asklepios.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Asklepios.Web.Areas.CustomerServiceArea.Controllers
{
    [Area("CustomerServiceArea")]
    public class CustomerServiceController : Controller
    {
       // private readonly ILogger<CustomerServiceController> _logger;

        //public CustomerServiceController(ILogger<CustomerServiceController> logger)
        //{
        //    _logger = logger;
        //}

        //public IActionResult Index()
        //{
        //    return View();
        //}
        ICustomerServiceModuleRepository _context;
        private static User _loggedUser { get; set; }
        private static Person _person { get; set; }
        private static Patient _selectedPatient { get; set; }
        public CustomerServiceController(ICustomerServiceModuleRepository context)
        {
            _context = context;

        }
        //public IActionResult Index()
        //{
        //    string id= _context.GetPatientData().Id.ToString();
        //    return View(id);
        //}
        [HttpPost]
        public IActionResult Index(SelectPatientViewModel model)
        {
            if (_loggedUser != null)
            {
                model.AllPatients = _context.GetAllPatients().ToList();
                model.SelectedPatient = _selectedPatient;
                return View(model);
            }
            return NotFound();
        }
        [HttpGet]
        public IActionResult Index(string id)
        {
            if (int.TryParse(id, out int parsedId))
            {
                _loggedUser = _context.GetUser(parsedId);
                _person = _context.GetPerson(_loggedUser.PersonId);

                SelectPatientViewModel model = new SelectPatientViewModel(_selectedPatient, _context.GetAllPatients().ToList());
                //model.AllPatients = _context.GetAllPatients().ToList();
                //model.SelectedPatient = _selectedPatient;
                return View(model);
            }
            else
            {
                if (_loggedUser != null)
                {
                    SelectPatientViewModel model = new SelectPatientViewModel(_selectedPatient, _context.GetAllPatients().ToList());
                    return View(model);
                }
                else
                {
                    return NotFound();
                }
            }
        }
        public IActionResult SelectPatient(string id)
        {
            if (_loggedUser != null)
            {
                if (id != null)
                {
                    if (long.TryParse(id, out long lid))
                    {
                        List<Patient> patients = _context.GetAllPatients().ToList();
                        Patient patient = patients.Where(c => c.Id == lid).FirstOrDefault();

                        if (patient != null)
                        {
                            _selectedPatient = patient;
                        }
                    }
                }
            }

            return RedirectToAction("Index", "CustomerService", new { area = "CustomerServiceArea" });
        }

        public IActionResult DeselectPatient()
        {
            if (_loggedUser != null)
            {
                _selectedPatient = null;
                return RedirectToAction("Index", "CustomerService", new { area = "CustomerServiceArea" });
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult Locations()
        {
            if (_loggedUser != null)
            {

                List<Location> locations = _context.GetAllLocations().ToList();
                LocationsViewModel model = new LocationsViewModel(_selectedPatient, locations);

                return View(model);
            }
            return NotFound();

        }

        public IActionResult NoReferral(string id)
        {
            if (_loggedUser != null)
            {

                if (long.TryParse(id, out long lid))
                {
                    Visit visit = _context.GetAvailableVisitById(lid);

                    return View(visit);
                }
                else
                {
                    return NotFound();
                }
            }
            return NotFound();

        }

        public IActionResult BookSpecifiedVisit(string id)
        {
            if (_loggedUser != null)
            {

                if (long.TryParse(id, out long lid))
                {
                    Patient patient = _context.CurrentPatient;

                    Visit visit = _context.GetAvailableVisitById(lid);

                    if (visit.PrimaryService.RequireRefferal)
                    {
                        if (patient.MedicalReferrals.Count > 0)
                        {
                            MedicalReferral referral = patient.MedicalReferrals.Where(c => c.MedicalService == visit.PrimaryService && c.IsActive).FirstOrDefault();
                            if (referral != null)
                            {
                                referral.HasBeenUsed = true;
                                referral.Visit = visit;
                                patient.BookVisit(visit);

                                _context.UpdateReferral(referral);
                                _context.UpdateVisit(visit);
                                return RedirectToAction("PlannedVisits");
                            }
                            else
                            {
                                return RedirectToAction("NoReferral", "CustomerService", new { area = "CustomerServiceArea", id = visit.Id });
                            }
                        }
                        else
                        {
                            return RedirectToAction("NoReferral", "CustomerService", new { area = "CustomerServiceArea", id = visit.Id });
                        }
                    }
                    else
                    {
                        patient.BookVisit(visit);
                        _context.UpdateVisit(visit);
                        return RedirectToAction("PlannedVisits");
                    }

                }
                else
                {
                    return RedirectToAction("BookVisit", "CustomerService", new { area = "CustomerServiceArea" });
                }
            }
            return NotFound();

        }

        [HttpPost]
        public IActionResult BookVisit(BookVisitViewModel model)
        {
            if (_loggedUser != null)
            {
                model.AllVisitsList = _context.GetAvailableVisits().ToList();
                model.SelectedPatient = _selectedPatient;
                return View(model);
            }
            return NotFound();
            //BookVisitViewModel bookVisitViewModel = new BookVisitViewModel(_context.GetAvailableVisits().ToList(), model);
        }
        [HttpGet]
        public IActionResult BookVisit()
        {
            if (_loggedUser != null)
            {
                BookVisitViewModel model = new BookVisitViewModel(_selectedPatient) { AllVisitsList = _context.GetAvailableVisits().ToList() }; //(_context.GetAvailableVisits().ToList(),new VisitSearchOptions());
                return View(model);
            }
            return NotFound();

        }

        [HttpGet]
        public IActionResult Contact()
        {
            if (_loggedUser != null)
            {
                ContactMessageViewModel model = new ContactMessageViewModel(_selectedPatient);
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Contact(ContactMessageViewModel model)
        {
            if (_loggedUser != null)
            {
                ContactMessageViewModel modelP = new ContactMessageViewModel(_selectedPatient);
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
            return NotFound();

        }
        //public IActionResult MedicalAdvice()
        //{
        //    return View();
        //}
        public IActionResult MedicalWorkersList()
        {
            if (_loggedUser != null)
            {

                List<MedicalWorker> medicalWorkers = _context.GetMedicalWorkers().ToList();
                MedicalWorkersViewModel model = new MedicalWorkersViewModel(_selectedPatient, medicalWorkers);
                return View(model);
            }
            return NotFound();

        }
        public IActionResult MedicalWorkerDetails(string id)
        {
            if (_loggedUser != null)
            {

                MedicalWorker worker = _context.GetMedicalWorkers().Where(c => c.Id.ToString() == id).FirstOrDefault();
                MedicalWorkerViewModel model = new MedicalWorkerViewModel(_selectedPatient, worker);

                return View(model);
            }
            return NotFound();
        }

        public IActionResult VisitDetails(string id)
        {
            if (_loggedUser != null)
            {

                if (long.TryParse(id, out long lid))
                {
                    Visit visit = _context.GetHistoricalVisitById(lid);
                    VisitViewModel model = new VisitViewModel(_selectedPatient, visit);
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "CustomerService", new { area = "CustomerServiceArea", id = _context.CurrentPatient.Id });
                }
            }
            return NotFound();

        }

        public IActionResult HistoricalVisits()
        {
            if (_loggedUser != null)
            {

                Patient patient = _context.CurrentPatient;
                CustomerServiceArea.Models.PatientViewModel viewModel = new PatientViewModel(patient);
                return View(viewModel);
            }
            return NotFound();

        }
        public IActionResult PlannedVisits()
        {
            if (_loggedUser != null)
            {

                Patient patient = _context.CurrentPatient;
                CustomerServiceArea.Models.PatientViewModel viewModel = new PatientViewModel(patient);
                return View(viewModel);
            }
            return NotFound();

        }
        public IActionResult Prescriptions()
        {
            if (_loggedUser != null)
            {

                Patient patient = _context.CurrentPatient;
                CustomerServiceArea.Models.PatientViewModel viewModel = new PatientViewModel(patient);
                return View(viewModel);
            }
            return NotFound();

        }
        public IActionResult Referrals()
        {
            if (_loggedUser != null)
            {
                Patient patient = _context.CurrentPatient;
                CustomerServiceArea.Models.PatientViewModel viewModel = new PatientViewModel(patient);
                return View(viewModel);
            }
            return NotFound();

        }
        public IActionResult TestResults()
        {
            if (_loggedUser != null)
            {
                Patient patient = _context.CurrentPatient;
                CustomerServiceArea.Models.PatientViewModel viewModel = new PatientViewModel(patient);
                return View(viewModel);
            }
            return NotFound();

        }

        public IActionResult DownloadFile(string id)
        {
            if (_loggedUser != null)
            {

                //Build the File Path.
                if (long.TryParse(id, out long idL))
                {
                    byte[] bytes = _context.CurrentPatient.TestsResults.Where(c => c.Id == idL).FirstOrDefault()?.PdfDocument;//pdf.  System.IO.File.ReadAllBytes(pdf);

                    //Send the File to Download.
                    return File(bytes, "application/octet-stream", "results.pdf");
                }
                else
                {
                    return NotFound();
                }
            }
            return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
