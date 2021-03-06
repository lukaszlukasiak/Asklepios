using Asklepios.Core.Models;
using Asklepios.Data.Interfaces;
using Asklepios.Web.Areas.CustomerServiceArea.Models;
using Asklepios.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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

        ICustomerServiceModuleRepository _context;
        private static User _loggedUser { get; set; }
        private static Person _person { get; set; }
        private static Patient _selectedPatient { get; set; }

        internal static void LogOut()
        {
            _loggedUser = null;
            _person = null;
            _selectedPatient = null;
        }

        public CustomerServiceController(ICustomerServiceModuleRepository context)
        {
            _context = context;
        }
        public IActionResult UserProfile()
        {
            if (_loggedUser != null)
            {
                UserViewModel model = new UserViewModel();
                model.User = _loggedUser;
                model.UserName = _loggedUser.Person.FullName;
                model.SelectedPatient = _selectedPatient;

                return View(model);

            }
            else
            {
                return NotFound();
            }
        }
        //[HttpPost]
        //public IActionResult Index(SelectPatientViewModel model)
        //{
        //    if (_loggedUser != null)
        //    {
        //        model.AllPatients = _context.GetAllPatients().ToList();
        //        model.SelectedPatient = _selectedPatient;
        //        model.UserName = _loggedUser.Person.FullName;

        //        return View(model);
        //    }
        //    return NotFound();
        //}
        [HttpGet]
        public IActionResult Index(string id)
        {
            if (int.TryParse(id, out int parsedId))
            {
                _loggedUser = _context.GetUser(parsedId);
                _person = _context.GetPerson(_loggedUser.PersonId);
                //_selectedPatient = _context.GetCurrentPatientData();
                //SelectPatientViewModel model = new SelectPatientViewModel(_selectedPatient, _context.GetAllPatients().ToList());
                ////model.AllPatients = _context.GetAllPatients().ToList();
                ////model.SelectedPatient = _selectedPatient;
                //model.UserName = _loggedUser.Person.FullName;
                return RedirectToAction("Dashboard");
            }
            //return View(model);
            //}
            else
            {
                //if (_loggedUser != null)
                //{
                ////SelectPatientViewModel model = new SelectPatientViewModel(_selectedPatient, _context.GetAllPatients().ToList());
                ////model.UserName = _loggedUser.Person.FullName;

                ////return View(model);
                //return RedirectToAction("Dashboard");
                //}
                //else
                //{
                return NotFound();
                //}
            }
        }
        [HttpPost]
        public IActionResult Dashboard(SelectPatientViewModel model)
        {
            if (_loggedUser != null)
            {
                model.AllPatients = _context.GetAllPatients().ToList();
                model.SelectedPatient = _selectedPatient;
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            return NotFound();
        }
        [HttpGet]
        public IActionResult Dashboard(string id)
        {
            //if (int.TryParse(id, out int parsedId))
            //{
            //    _loggedUser = _context.GetUser(parsedId);
            //    _person = _context.GetPerson(_loggedUser.PersonId);
            //    _selectedPatient = _context.GetCurrentPatientData();
            //    SelectPatientViewModel model = new SelectPatientViewModel(_selectedPatient, _context.GetAllPatients().ToList());
            //    model.AllPatients = _context.GetAllPatients().ToList();
            //    model.SelectedPatient = _selectedPatient;
            //    model.UserName = _loggedUser.Person.FullName;

            //    return View(model);
            //}
            //else
            //{
                if (_loggedUser != null)
                {
                    SelectPatientViewModel model = new SelectPatientViewModel(_selectedPatient, _context.GetAllPatients().ToList());
                    model.UserName = _loggedUser.Person.FullName;

                    return View(model);
                }
                else
                {
                    return NotFound();
                }
            //}
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
                        //Patient patientData = _context.GetCurrentPatientData();

                        if (patient != null)
                        {
                            //patientData.Person = patient.Person;
                            //_selectedPatient = patientData;
                            //patient.Person = _context.GetPerson(patient.PersonId);
                            _selectedPatient = patient;
                        }
                    }
                }
            }

            return RedirectToAction("Dashboard", "CustomerService", new { area = "CustomerServiceArea" });
        }

        public IActionResult DeselectPatient()
        {
            if (_loggedUser != null)
            {
                _selectedPatient = null;
                return RedirectToAction("Dashboard", "CustomerService", new { area = "CustomerServiceArea" });
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
                model.UserName = _loggedUser.Person.FullName;

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
                    VisitViewModel model = new VisitViewModel(_selectedPatient, visit);
                    model.UserName = _loggedUser.Person.FullName;

                    return View(model);
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
                    //Patient patient = _context.CurrentPatient;
                    Patient patient = _selectedPatient;
                    Visit visit = _context.GetAvailableVisitById(lid);

                    if (visit.PrimaryService.RequireRefferal)
                    {
                        patient.HistoricalVisits = _context.GetHistoricalVisitsByPatientId(_selectedPatient.Id);
                        //patient.MedicalReferrals = _context.GetMedicalReferralsByPatientId(_selectedPatient.Id);
                        if (patient.MedicalReferrals?.Count > 0)
                        {
                            MedicalReferral referral = patient.MedicalReferrals.Where(c => c.PrimaryMedicalService == visit.PrimaryService && c.IsActive).FirstOrDefault();
                            if (referral != null)
                            {
                                referral.HasBeenUsed = true;
                                referral.VisitWhenIssued = visit;
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
        public IActionResult BookVisitConditions(SearchViewModel model)
        {
            if (_loggedUser != null)
            {
                BookVisitViewModel model2 = new BookVisitViewModel() { AllVisitsList = _context.GetAvailableVisits().ToList() }; //(_context.GetAvailableVisits().ToList(),new VisitSearchOptions());
                if (!string.IsNullOrWhiteSpace(model.SelectedCategoryId))
                {
                    model2.SelectedCategoryId = model.SelectedCategoryId.ToString();
                }
                if (!string.IsNullOrWhiteSpace(model.SelectedLocationId))
                {
                    model2.SelectedLocationId = model.SelectedLocationId.ToString();
                }
                if (!string.IsNullOrWhiteSpace(model.SelectedMedicalWorkerId))
                {
                    model2.SelectedMedicalWorkerId = model.SelectedMedicalWorkerId.ToString();
                }
                if (!string.IsNullOrWhiteSpace(model.SelectedServiceId))
                {
                    model2.SelectedServiceId = model.SelectedServiceId.ToString();
                }
                //model2. = model;
                model2.AllCategories = _context.GetVisitCategories().ToList();
                model2.AllLocations = _context.GetAllLocations().ToList();
                model2.AllMedicalServices = _context.GetMedicalServices().ToList();
                model2.AllMedicalWorkers = _context.GetMedicalWorkers().ToList();
                model2.UserName = _loggedUser.Person.FullName;

                model2.SelectedPatient = _selectedPatient;
                return View(model2);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult BookVisit(BookVisitViewModel model)
        {
            if (_loggedUser != null)
            {
                model.AllVisitsList = _context.GetAvailableVisits().ToList();
                model.SelectedPatient = _selectedPatient;
                model.UserName = _loggedUser.Person.FullName;

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
                model.UserName = _loggedUser.Person.FullName;
                model.SelectedPatient = _selectedPatient;

                return View(model);
            }
            return NotFound();

        }

        [HttpGet]
        public IActionResult Contact()
        {
            if (_loggedUser != null)
            {
                ContactMessageViewModel model = new ContactMessageViewModel(_loggedUser);
                model.UserName = _loggedUser.Person.FullName;
                model.SelectedPatient = _selectedPatient;
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Contact(ContactMessageViewModel model)
        {
            if (_loggedUser != null)
            {
                ContactMessageViewModel modelP = new ContactMessageViewModel(_loggedUser);
                modelP.Message = model.Message;
                modelP.Subject = model.Subject;
                //ContactMessageViewModel model = new ContactMessageViewModel();
                bool isSent = ServiceClasses.MailServices.CreateAndSendMail(modelP);
                if (isSent)
                {
                    model.AlertMessage = "Wiadomość została wysłana!";
                    model.AlertMessageType = Enums.AlertMessageType.Info;
                    ModelState.Clear();
                }
                else
                {
                    model.AlertMessage = "Wystąpił błąd podczas próby wysłania wiadomości! Spróbuj jeszcze raz!";
                    model.AlertMessageType = Enums.AlertMessageType.Error;
                    ViewBag.Message = "Wystąpił błąd podczas próby wysłania wiadomości! Spróbuj jeszcze raz!";

                }
                model.UserName = _loggedUser.Person.FullName;
                model.SelectedPatient = _selectedPatient;
                return View(model);
            }
            return NotFound();

        }
        public IActionResult MedicalWorkersList()
        {
            if (_loggedUser != null)
            {

                List<MedicalWorker> medicalWorkers = _context.GetMedicalWorkers().ToList();
                MedicalWorkersViewModel model = new MedicalWorkersViewModel(_selectedPatient, medicalWorkers);
                model.UserName = _loggedUser.Person.FullName;
                model.SelectedPatient = _selectedPatient;

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
                model.UserName = _loggedUser.Person.FullName;
                model.SelectedPatient = _selectedPatient;

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
                    if (visit == null)
                    {
                        return NotFound();
                    }
                    VisitViewModel model = new VisitViewModel(visit.Patient, visit);
                    model.UserName = _loggedUser.Person.FullName;
                    model.SelectedPatient = _selectedPatient;

                    //VisitViewModel model = new VisitViewModel(_selectedPatient, visit);
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Dashboard", "CustomerService", new { area = "CustomerServiceArea", id = _context.CurrentPatient.Id });
                }
            }
            return NotFound();

        }

        public IActionResult HistoricalVisits()
        {
            if (_loggedUser != null)
            {

                //Patient patient = _context.CurrentPatient;
                _selectedPatient.HistoricalVisits = _context.GetHistoricalVisitsByPatientId(_selectedPatient.Id);
                CustomerServiceArea.Models.PatientViewModel model = new PatientViewModel(_selectedPatient);
                model.SelectedPatient = _selectedPatient;
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            return NotFound();

        }
        public IActionResult PlannedVisits()
        {
            if (_loggedUser != null)
            {

                // Patient patient = _context.CurrentPatient;
                _selectedPatient.BookedVisits = _context.GetBookedVisitsByPatientId(_selectedPatient.Id);
                CustomerServiceArea.Models.PatientViewModel model = new PatientViewModel(_selectedPatient);
                model.UserName = _loggedUser.Person.FullName;
                model.SelectedPatient = _selectedPatient;

                return View(model);
            }
            return NotFound();

        }
        public IActionResult ResignFromVisit(string id)
        {
            if (_loggedUser != null)
            {
                if (long.TryParse(id, out long lid))
                {
                    Visit plannedVisit = _selectedPatient.BookedVisits.Where(c => c.Id == lid).FirstOrDefault();
                    if (plannedVisit != null)
                    {
                        if (plannedVisit.DateTimeSince > DateTimeOffset.Now)
                        {
                            _context.ResignFromVisit(plannedVisit, _selectedPatient);
                            return RedirectToAction("PlannedVisits", "CustomerService", new { area = "CustomerServiceArea" });
                        }
                        else
                        {
                            return NotFound();
                        }
                        //plannedVisit.Patient = null;
                    }
                }
                return NotFound();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public IActionResult RescheduleVisit(string id)
        {
            if (_loggedUser != null)
            {
                if (long.TryParse(id, out long lid))
                {
                    Visit plannedVisit = _selectedPatient.BookedVisits.Where(c => c.Id == lid).FirstOrDefault();
                    if (plannedVisit != null)
                    {
                        if (plannedVisit.DateTimeSince > DateTimeOffset.Now)
                        {
                            RescheduleVisitViewModel model = new RescheduleVisitViewModel();
                            model.SelectedPrimaryServiceId = plannedVisit.PrimaryService.Id.ToString();
                            model.RescheduledVisitId = lid;
                            model.MedicalServices = _context.GetMedicalServices();
                            model.AllVisitsList = _context.GetAvailableVisits().ToList();
                            model.SelectedPatient = _selectedPatient;//_context.CurrentPatient;
                            model.UserName = _loggedUser.Person.FullName;

                            return View(model);
                        }
                        else
                        {
                            return NotFound();
                        }
                        //plannedVisit.Patient = null;
                    }
                }
                return NotFound();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult RescheduleVisit(RescheduleVisitViewModel model)
        {
            if (_loggedUser != null)
            {

                Visit plannedVisit = _selectedPatient.BookedVisits.Where(c => c.Id == model.RescheduledVisitId).FirstOrDefault();
                if (plannedVisit != null)
                {
                    if (plannedVisit.DateTimeSince > DateTimeOffset.Now)
                    {
                        model.UserName = _loggedUser.Person.FullName;
                        return View(model);
                    }
                    else
                    {
                        return NotFound();
                    }
                    //plannedVisit.Patient = null;
                }
                return NotFound();

            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult RescheduleVisitFinal(RescheduleVisitSelectNewViewModel model)
        {
            if (_loggedUser != null)
            {
                Visit visitToReschedule = _selectedPatient.BookedVisits.Where(c => c.Id == model.RescheduledVisitId).FirstOrDefault();
                if (visitToReschedule != null)
                {
                    Visit newVisit = _context.GetAvailableVisitById(model.SelectedNewVisitId);
                    if (newVisit != null)
                    {

                        newVisit.Patient = _selectedPatient;
                        _context.BookVisit(_selectedPatient, newVisit);
                        _context.ResignFromVisit(visitToReschedule, _selectedPatient);
                        return RedirectToAction("PlannedVisits", "CustomerService", new { area = "CustomerServiceArea" });
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                return NotFound();
            }
            else
            {
                return NotFound();
            }

        }

        public IActionResult Prescriptions()
        {
            if (_loggedUser != null)
            {
                Patient patient = _selectedPatient;//_context.CurrentPatient;
                CustomerServiceArea.Models.PatientViewModel model = new PatientViewModel(patient);
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            return NotFound();

        }
        public IActionResult Referrals()
        {
            if (_loggedUser != null)
            {
                Patient patient = _selectedPatient;// _context.CurrentPatient;
                CustomerServiceArea.Models.PatientViewModel model = new PatientViewModel(patient);
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            return NotFound();
        }
        public IActionResult TestResults()
        {
            if (_loggedUser != null)
            {
                Patient patient = _selectedPatient;// _context.CurrentPatient;
                CustomerServiceArea.Models.PatientViewModel model = new PatientViewModel(patient);
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
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
                    byte[] bytes = _selectedPatient.TestsResults.Where(c => c.Id == idL).FirstOrDefault()?.Document;//pdf.  System.IO.File.ReadAllBytes(pdf);

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
