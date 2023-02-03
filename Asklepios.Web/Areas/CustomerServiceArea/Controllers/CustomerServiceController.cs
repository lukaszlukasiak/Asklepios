using Asklepios.Core.Models;
using Asklepios.Data.Interfaces;
using Asklepios.Web.Areas.CustomerServiceArea.Models;
using Asklepios.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
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

        readonly ICustomerServiceModuleRepository _context;
        private static User loggedUser { get; set; }
        private static Person person { get; set; }
        private static Patient selectedPatient { get; set; }
        IWebHostEnvironment _hostEnvironment { get; set; }

        internal static void LogOut()
        {
            loggedUser = null;
            person = null;
            selectedPatient = null;
        }


        public CustomerServiceController(ICustomerServiceModuleRepository context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment= hostEnvironment;
        }
        public IActionResult UserProfile()
        {
            if (loggedUser != null)
            {
                UserViewModel model = new UserViewModel();
                model.User = loggedUser;
                model.UserName = loggedUser.Person.FullName;
                model.SelectedPatient = selectedPatient;

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
                loggedUser = _context.GetUserById(parsedId);
                person = _context.GetPersonById(loggedUser.PersonId.Value);
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
            if (loggedUser != null)
            {
                model.AllPatients = _context.GetAllPatients();
                model.SelectedPatient = selectedPatient;
                model.UserName = loggedUser.Person.FullName;

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
                if (loggedUser != null)
                {
                    SelectPatientViewModel model = new SelectPatientViewModel(selectedPatient, _context.GetAllPatients());
                    model.UserName = loggedUser.Person.FullName;

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
            if (loggedUser != null)
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
                            selectedPatient = patient;
                        }
                    }
                }
            }

            return RedirectToAction("Dashboard", "CustomerService", new { area = "CustomerServiceArea" });
        }

        public IActionResult DeselectPatient()
        {
            if (loggedUser != null)
            {
                selectedPatient = null;
                return RedirectToAction("Dashboard", "CustomerService", new { area = "CustomerServiceArea" });
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult Locations()
        {
            if (loggedUser != null)
            {

                List<Location> locations = _context.GetAllLocations().OrderBy(c=>c.Name).ToList();
                LocationsViewModel model = new LocationsViewModel(selectedPatient, locations);
                model.UserName = loggedUser.Person.FullName;

                return View(model);
            }
            return NotFound();

        }

        public IActionResult NoReferral(string id)
        {
            if (loggedUser != null)
            {

                if (long.TryParse(id, out long lid))
                {
                    Visit visit = _context.GetFutureVisitById(lid);
                    VisitViewModel model = new VisitViewModel(selectedPatient, visit);
                    model.UserName = loggedUser.Person.FullName;

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
            if (loggedUser != null)
            {
                if (selectedPatient == null)
                {
                    return RedirectToAction("Dashboard");
                }

                if (long.TryParse(id, out long lid))
                {
                    //Patient patient = _context.CurrentPatient;
                    Patient patient = selectedPatient;
                    Visit visit = _context.GetFutureVisitById(lid);

                    if (visit.PrimaryService.RequireRefferal)
                    {
                        //patient.HistoricalVisits = _context.GetHistoricalVisitsByPatientId(selectedPatient.Id);
                        patient.AllVisits.AddRange(_context.GetHistoricalVisitsByPatientIdQuery(selectedPatient.Id));
                        
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
                        //patient.BookVisit(visit);
                        _context.BookVisit(selectedPatient.Id,visit.Id);
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
            if (loggedUser != null)
            {
                if (selectedPatient == null)
                {
                    return RedirectToAction("Dashboard");
                }

                BookVisitViewModel model2 = new BookVisitViewModel() { AllVisitsList = _context.GetAvailableVisitsQuery() }; //(_context.GetAvailableVisits().ToList(),new VisitSearchOptions());
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
                model2.AllCategories = _context.GetVisitCategories().OrderBy(c=>c.CategoryName).ToList();
                model2.AllLocations = _context.GetAllLocations().OrderBy(c=>c.Name).ToList();
                model2.AllMedicalServices = _context.GetMedicalServices().OrderBy(c=>c.Name).ToList();
                model2.AllMedicalWorkers = _context.GetMedicalWorkers().OrderBy(c=>c.FullProffesionalName).ToList();
                model2.UserName = loggedUser.Person.FullName;

                model2.SelectedPatient = selectedPatient;
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
            if (loggedUser != null)
            {
                if (selectedPatient==null)
                {
                    return RedirectToAction("Dashboard");
                }
                model.AllVisitsList = _context.GetAvailableVisitsQuery();
                model.SelectedPatient = selectedPatient;
                model.UserName = loggedUser.Person.FullName;
                model.AllCategories = _context.GetVisitCategories().OrderBy(c => c.CategoryName).ToList();
                model.AllLocations = _context.GetAllLocations().OrderBy(c => c.Name).ToList();
                model.AllMedicalServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService).OrderBy(c => c.Name).ToList();
                model.AllMedicalWorkers = _context.GetMedicalWorkers().OrderBy(c => c.FullProffesionalName).ToList();

                return View(model);
            }
            return NotFound();
            //BookVisitViewModel bookVisitViewModel = new BookVisitViewModel(_context.GetAvailableVisits().ToList(), model);
        }
        [HttpGet]
        public IActionResult BookVisit()
        {
            if (loggedUser != null)
            {
                if (selectedPatient==null)
                {
                    return RedirectToAction("Dashboard");
                }
                BookVisitViewModel model = new BookVisitViewModel(selectedPatient) { AllVisitsList = _context.GetAvailableVisitsQuery() }; //(_context.GetAvailableVisits().ToList(),new VisitSearchOptions());
                model.UserName = loggedUser.Person.FullName;
                model.AllCategories = _context.GetVisitCategories().OrderBy(c=>c.CategoryName).ToList();
                model.AllLocations = _context.GetAllLocations().OrderBy(c=>c.Name).ToList();
                model.AllMedicalServices = _context.GetMedicalServices().Where(c=>c.IsPrimaryService).OrderBy(c=>c.Name).ToList();
                model.AllMedicalWorkers = _context.GetMedicalWorkers().OrderBy(c=>c.FullProffesionalName).ToList();

                //model.SelectedPatient = selectedPatient;

                return View(model);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            if (loggedUser != null)
            {
                ContactMessageViewModel model = new ContactMessageViewModel(loggedUser);
                model.UserName = loggedUser.Person.FullName;
                model.SelectedPatient = selectedPatient;
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Contact(ContactMessageViewModel model)
        {
            if (loggedUser != null)
            {
                ContactMessageViewModel modelP = new ContactMessageViewModel(loggedUser);
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
                model.UserName = loggedUser.Person.FullName;
                model.SelectedPatient = selectedPatient;
                return View(model);
            }
            return NotFound();

        }
        public IActionResult MedicalWorkersList()
        {
            if (loggedUser != null)
            {

                List<MedicalWorker> medicalWorkers = _context.GetMedicalWorkers().OrderBy(c=>c.FullProffesionalName).ToList();
                MedicalWorkersViewModel model = new MedicalWorkersViewModel(selectedPatient, medicalWorkers);
                model.UserName = loggedUser.Person.FullName;
                model.SelectedPatient = selectedPatient;

                return View(model);
            }
            return NotFound();

        }
        public IActionResult MedicalWorkerDetails(string id)
        {
            if (loggedUser != null)
            {
                if (long.TryParse(id, out long lId))
                {
                    MedicalWorker worker = _context.GetMedicalWorkerDetailsById(lId);//_context.GetMedicalWorkers().Where(c => c.Id.ToString() == id).FirstOrDefault();
                    MedicalWorkerViewModel model = new MedicalWorkerViewModel(selectedPatient, worker);
                    model.UserName = loggedUser.Person.FullName;
                    model.SelectedPatient = selectedPatient;
                    return View(model);
                }
            }
            return NotFound();
        }

        public IActionResult VisitDetails(string id)
        {
            if (loggedUser != null)
            {

                if (long.TryParse(id, out long lid))
                {
                    Visit visit = _context.GetHistoricalVisitById(lid);
                    if (visit == null)
                    {
                        return NotFound();
                    }
                    VisitViewModel model = new VisitViewModel(visit.Patient, visit);
                    model.UserName = loggedUser.Person.FullName;
                    model.SelectedPatient = selectedPatient;

                    //VisitViewModel model = new VisitViewModel(_selectedPatient, visit);
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Dashboard", "CustomerService", new { area = "CustomerServiceArea", id = selectedPatient.Id });
                }
            }
            return NotFound();

        }

        public IActionResult HistoricalVisits()
        {
            if (loggedUser != null)
            {
                if (selectedPatient == null)
                {
                    return RedirectToAction("Dashboard");
                }

                //Patient patient = _context.CurrentPatient;
                //selectedPatient.HistoricalVisits = _context.GetHistoricalVisitsByPatientId(selectedPatient.Id);
                //selectedPatient.AllVisits.AddRange(_context.GetHistoricalVisitsByPatientId(selectedPatient.Id));

                CustomerServiceArea.Models.PatientViewModel model = new PatientViewModel(selectedPatient);
                model.AllVisits = _context.GetHistoricalVisitsByPatientIdQuery(selectedPatient.Id);
                //model.SelectedPatient = selectedPatient;
                model.UserName = loggedUser.Person.FullName;

                return View(model);
            }
            return NotFound();

        }
        public IActionResult PlannedVisits()
        {
            if (loggedUser != null)
            {
                if (selectedPatient == null)
                {
                    return RedirectToAction("Dashboard");
                }

                // Patient patient = _context.CurrentPatient;
                //selectedPatient.AllVisits.AddRange(_context.GetBookedVisitsByPatientId(selectedPatient.Id));
                //selectedPatient.BookedVisits = _context.GetBookedVisitsByPatientId(selectedPatient.Id);
                CustomerServiceArea.Models.PatientViewModel model = new PatientViewModel(selectedPatient);
                model.AllVisits = _context.GetBookedVisitsByPatientId(selectedPatient.Id).AsQueryable();
                model.UserName = loggedUser.Person.FullName;
                model.SelectedPatient = selectedPatient;

                return View(model);
            }
            return NotFound();

        }
        public IActionResult ResignFromVisit(string id)
        {
            if (loggedUser != null)
            {
                if (selectedPatient == null)
                {
                    return RedirectToAction("Dashboard");
                }

                if (long.TryParse(id, out long lid))
                {
                    Visit plannedVisit = _context.GetBookedVisitsByPatientId(selectedPatient.Id).Where(c => c.Id == lid).FirstOrDefault(); //selectedPatient.BookedVisits.Where(c => c.Id == lid).FirstOrDefault();
                    if (plannedVisit != null)
                    {
                        if (plannedVisit.DateTimeSince > DateTimeOffset.Now)
                        {
                            _context.ResignFromVisit(plannedVisit.Id);
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
            if (loggedUser != null)
            {
                if (selectedPatient == null)
                {
                    return RedirectToAction("Dashboard");
                }

                if (long.TryParse(id, out long lid))
                {
                    Visit plannedVisit = _context.GetBookedVisitsByPatientId(selectedPatient.Id).Where(c => c.Id == lid).FirstOrDefault(); //selectedPatient.BookedVisits.Where(c => c.Id == lid).FirstOrDefault();
                    if (plannedVisit != null)
                    {
                        if (plannedVisit.DateTimeSince > DateTimeOffset.Now)
                        {
                            RescheduleVisitViewModel model = new RescheduleVisitViewModel();
                            model.SelectedPrimaryServiceId = plannedVisit.PrimaryService.Id.ToString();
                            model.RescheduledVisitId = lid;
                            model.RescheduledVisit = plannedVisit;
                            model.MedicalServices = _context.GetMedicalServices();
                            model.AllVisitsList = _context.GetAvailableVisitsQuery();
                            //model.GetVisitCategories
                            model.SelectedPatient = selectedPatient;//_context.CurrentPatient;
                            model.UserName = loggedUser.Person.FullName;

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
            if (loggedUser != null)
            {
                if (selectedPatient == null)
                {
                    return RedirectToAction("Dashboard");
                }

                Visit plannedVisit = _context.GetBookedVisitsByPatientId(selectedPatient.Id).Where(c => c.Id == model.RescheduledVisitId).FirstOrDefault();// selectedPatient.BookedVisits.Where(c => c.Id == model.RescheduledVisitId).FirstOrDefault();
                if (plannedVisit != null)
                {
                    if (plannedVisit.DateTimeSince > DateTimeOffset.Now)
                    {
                        model.SelectedPrimaryServiceId = plannedVisit.PrimaryService.Id.ToString();
                        model.RescheduledVisitId = model.RescheduledVisitId;
                        model.RescheduledVisit = plannedVisit;
                        model.MedicalServices = _context.GetMedicalServices();
                        model.AllVisitsList = _context.GetAvailableVisitsQuery();
                        model.SelectedPatient = selectedPatient;//_context.CurrentPatient;
                        model.UserName = loggedUser.Person.FullName;

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
            if (loggedUser != null)
            {
                if (selectedPatient == null)
                {
                    return RedirectToAction("Dashboard");
                }

                Visit visitToReschedule = _context.GetBookedVisitsByPatientId(selectedPatient.Id).Where(c => c.Id == model.RescheduledVisitId).FirstOrDefault();// selectedPatient.BookedVisits.Where(c => c.Id == model.RescheduledVisitId).FirstOrDefault();
                if (visitToReschedule != null)
                {
                    Visit newVisit = _context.GetFutureVisitById(model.SelectedNewVisitId);
                    if (newVisit != null)
                    {

                        //newVisit.Patient = selectedPatient;
                        _context.BookVisit(selectedPatient.Id, newVisit.Id);
                        _context.ResignFromVisit(visitToReschedule.Id);
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
            if (loggedUser != null)
            {
                if (selectedPatient == null)
                {
                    return RedirectToAction("Dashboard");
                }

                Patient patient = selectedPatient;//_context.CurrentPatient;
                CustomerServiceArea.Models.PatientViewModel model = new PatientViewModel(patient);
                model.UserName = loggedUser.Person.FullName;
                model.Prescriptions = _context.GetPrescriptionsByPatientId(patient.Id);
                return View(model);
            }
            return NotFound();

        }
        public IActionResult Referrals()
        {
            if (loggedUser != null)
            {
                if (selectedPatient == null)
                {
                    return RedirectToAction("Dashboard");
                }

                Patient patient = selectedPatient;// _context.CurrentPatient;
                CustomerServiceArea.Models.PatientViewModel model = new PatientViewModel(patient);
                //model.AllVisits = _context.GetHistoricalVisitsByPatientIdQuery(selectedPatient.Id);
                model.MedicalReferrals = _context.GetMedicalReferralsByPatientIdQuery(selectedPatient.Id).ToList();
                model.UserName = loggedUser.Person.FullName;

                return View(model);
            }
            return NotFound();
        }
        public IActionResult TestResults()
        {
            if (loggedUser != null)
            {
                Patient patient = selectedPatient;// _context.CurrentPatient;
                CustomerServiceArea.Models.PatientViewModel model = new PatientViewModel(patient);
                model.UserName = loggedUser.Person.FullName;
                model.TestResults = _context.GetTestResultsByPatientId(patient.Id);
                return View(model);
            }
            return NotFound();

        }

        public IActionResult DownloadFile(string id)
        {
            if (loggedUser != null)
            {
                if (selectedPatient == null)
                {
                    return RedirectToAction("Dashboard");
                }

                //Build the File Path.
                if (long.TryParse(id, out long idL))
                {
                    MedicalTestResult medicalTestResult = _context.GetMedicalTestResultById(idL);

                    if (medicalTestResult.PatientId!=selectedPatient.Id)
                    {
                        return NotFound();
                    }
                    byte[] bytes = _context.GetDocument(medicalTestResult.DocumentPath, _hostEnvironment.WebRootPath);// _context.//_selectedPatient.TestsResults.Where(c => c.Id == idL).FirstOrDefault()?.Document;//pdf.  System.IO.File.ReadAllBytes(pdf);

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
