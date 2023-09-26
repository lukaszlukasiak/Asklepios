using Asklepios.Core.Models;
using Asklepios.Data.Interfaces;
using Asklepios.Web.Areas.CustomerServiceArea.Models;
using Asklepios.Web.Extensions;
using Asklepios.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System;
using Asklepios.Core.Enums;
using Asklepios.Core.Extensions;
using Newtonsoft.Json;

namespace Asklepios.Web.Areas.CustomerServiceArea.Controllers
{
    [Area("CustomerServiceArea")]
    [Authorize(Roles = "CustomerService")]
    public class CustomerServiceController : Controller
    {
        const string USER_ID = "USER_ID";
        const string PERSON_ID = "PERSON_ID";

        const string PATIENT_ID = "PATIENT_ID";
        readonly ICustomerServiceModuleRepository _context;
        private User _loggedUser { get; set; }
        IWebHostEnvironment _hostEnvironment { get; set; }
        private SignInManager<User> _signManager { get; set; }
        public long UserId { get; }



        public CustomerServiceController(ICustomerServiceModuleRepository context, IWebHostEnvironment hostEnvironment, SignInManager<User> signManager)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _signManager = signManager;
        }

        private long? GetSelectedPatientId()
        {
            if (!string.IsNullOrWhiteSpace(HttpContext.Session.GetString(PATIENT_ID)))
            {
                if (long.TryParse(HttpContext.Session.GetString(PATIENT_ID), out long id))
                {
                    return id;
                }
            }
            return null;
        }
        private long? GetUserId()
        {
            if (!string.IsNullOrWhiteSpace(HttpContext.Session.GetString(USER_ID)))
            {
                if (long.TryParse(HttpContext.Session.GetString(USER_ID), out long id))
                {
                    return id;
                }
            }
            return null;
        }
        private long? GetCSPersonId()
        {
            if (!string.IsNullOrWhiteSpace(HttpContext.Session.GetString(PERSON_ID)))
            {
                if (long.TryParse(HttpContext.Session.GetString(PERSON_ID), out long id))
                {
                    return id;
                }
            }
            return null;
        }


        public async Task<ActionResult> LogOutAsync()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult UserProfile()
        {
            _loggedUser = _context.GetUserById(GetUserId().Value);

            if (_loggedUser != null)
            {
                UserViewModel model = new UserViewModel();
                model.User = _loggedUser;
                model.UserName = _loggedUser.Person.FullName;
                model.SelectedPatient = GetSelectedPatient();//_context.GetPatientById(getse);

                return View(model);

            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public IActionResult Index()
        {
            long id = HttpContext.User.GetUserId().Value;
            User user = _context.GetUserById(id);
            HttpContext.Session.SetString(USER_ID, id.ToString());
            HttpContext.Session.SetString(PERSON_ID, user.PersonId.Value.ToString());


            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            _loggedUser = _context.GetUserById(GetUserId().Value);

            if (_loggedUser != null)
            {
                SelectPatientViewModel model = new SelectPatientViewModel();
                model.AllPatients = _context.GetAllPatients();
                model.UserName = _loggedUser.Person.FullName;

                long? selectedPatientId = GetSelectedPatientId();

                if (selectedPatientId.HasValue)
                {
                    model.SelectedPatient = _context.GetPatientById(selectedPatientId.Value);
                }

                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Dashboard(SelectPatientViewModel model)
        {
            _loggedUser = _context.GetUserById(GetUserId().Value);

            if (_loggedUser != null)
            {
                model.AllPatients = _context.GetAllPatients();
                model.UserName = _loggedUser.Person.FullName;

                long? selectedPatientId = GetSelectedPatientId();

                if (selectedPatientId.HasValue)
                {
                    model.SelectedPatient = _context.GetPatientById(selectedPatientId.Value);
                }

                return View(model);
            }
            return NotFound();
        }


        public IActionResult SelectPatient(string id)
        {
            _loggedUser = _context.GetUserById(GetUserId().Value);

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
                            HttpContext.Session.SetString(PATIENT_ID, lid.ToString());
                        }
                    }
                }
            }

            return RedirectToAction("Dashboard", "CustomerService", new { area = "CustomerServiceArea" });
        }

        public IActionResult DeselectPatient()
        {
            _loggedUser = _context.GetUserById(GetUserId().Value);

            if (_loggedUser != null)
            {
                HttpContext.Session.SetString(PATIENT_ID, "");
                return RedirectToAction("Dashboard", "CustomerService", new { area = "CustomerServiceArea" });
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult Locations()
        {
            _loggedUser = _context.GetUserById(GetUserId().Value);

            if (_loggedUser != null)
            {
                long? selectedPatientId = GetSelectedPatientId();
                Patient patient = null;

                if (selectedPatientId.HasValue)
                {
                    patient = _context.GetPatientById(selectedPatientId.Value);
                }


                List<Location> locations = _context.GetAllLocations().OrderBy(c => c.Name).ToList();
                LocationsViewModel model = new LocationsViewModel(patient, locations);
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            return NotFound();

        }

        public IActionResult NoReferral(string id)
        {
            _loggedUser = _context.GetUserById(GetUserId().Value);

            if (_loggedUser != null)
            {

                if (long.TryParse(id, out long lid))
                {
                    Patient patient = GetSelectedPatient();

                    Visit visit = _context.GetFutureVisitById(lid);
                    VisitViewModel model = new VisitViewModel(patient, visit);
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

        private Patient GetSelectedPatient()
        {
            long? selectedPatientId = GetSelectedPatientId();
            Patient patient = null;

            if (selectedPatientId.HasValue)
            {
                patient = _context.GetPatientById(selectedPatientId.Value);
            }
            return patient;
        }

        public IActionResult BookSpecifiedVisit(string id)
        {
            _loggedUser = _context.GetUserById(GetUserId().Value);

            if (_loggedUser != null)
            {
                Patient patient = GetSelectedPatient();
                if (patient == null)
                {
                    return RedirectToAction("Dashboard");
                }

                if (long.TryParse(id, out long lid))
                {
                    Visit visit = _context.GetFutureVisitById(lid);

                    if (visit.VisitStatus != Core.Enums.VisitStatus.AvailableNotBooked)
                    {
                        ViewMessage viewMessage = new ViewMessage()
                        {
                            Message = "Ta wizyta nie jest już dostępna!",
                            MessageType = Enums.AlertMessageType.ErrorMessage
                        };
                        TempData[ViewMessage.MESSAGE_KEY] = JsonConvert.SerializeObject(viewMessage);
                        return RedirectToAction("BookVisit");
                    }


                    if (visit.PrimaryService.RequireRefferal)
                    {
                        IQueryable<MedicalReferral> medicalReferrals = _context.GetMedicalReferralsByPatientIdQuery(patient.Id);

                        if (medicalReferrals.Count() > 0)
                        {
                            MedicalReferral referral = medicalReferrals.Where(c => c.PrimaryMedicalServiceId == visit.PrimaryServiceId && !c.HasBeenUsed).FirstOrDefault();
                            if (referral != null)
                            {

                                bool hasVivistAtThSameTime = HasVivistAtThSameTime(visit, patient.Id);
                                if (hasVivistAtThSameTime)
                                {
                                    ViewMessage viewMessage = new ViewMessage()
                                    {
                                        Message = "Już posiadasz zarezerwowaną wizytę w tym terminie!",
                                        MessageType = Enums.AlertMessageType.ErrorMessage
                                    };
                                    TempData[ViewMessage.MESSAGE_KEY] = JsonConvert.SerializeObject(viewMessage);
                                    return RedirectToAction("BookVisit");
                                }

                                referral.HasBeenUsed = true;
                                referral.VisitWhenUsedId = visit.Id;
                                visit.UsedExaminationReferralId = referral.Id;
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
                        bool hasVivistAtThSameTime = HasVivistAtThSameTime(visit, patient.Id);
                        if (hasVivistAtThSameTime)
                        {
                            ViewMessage viewMessage = new ViewMessage()
                            {
                                Message = "Już posiadasz zarezerwowaną wizytę w tym terminie!",
                                MessageType = Enums.AlertMessageType.ErrorMessage
                            };
                            TempData[ViewMessage.MESSAGE_KEY] = JsonConvert.SerializeObject(viewMessage);
                            return RedirectToAction("BookVisit");
                        }

                        _context.BookVisit(patient.Id, visit.Id);
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
        private bool HasVivistAtThSameTime(Visit visit, long patientId)
        {

            List<Visit> sameDayVisits = _context.GetBookedVisitsByPatientIdQuery(patientId).Where(c => c.DateTimeSince.Date == visit.DateTimeSince.Date).ToList();

            foreach (var item in sameDayVisits)
            {
                if (item.DateTimeSince <= visit.DateTimeSince && item.DateTimeTill >= visit.DateTimeSince)
                {
                    return true;
                }
                if (item.DateTimeSince <= visit.DateTimeTill && item.DateTimeTill >= visit.DateTimeTill)
                {
                    return true;
                }
            }
            return false;
        }
        [HttpPost]
        public IActionResult BookVisitConditions(SearchViewModel model)
        {
            _loggedUser = _context.GetUserById(GetUserId().Value);

            if (_loggedUser != null)
            {
                Patient patient = GetSelectedPatient();

                if (patient == null)
                {
                    return RedirectToAction("Dashboard");
                }

                BookVisitViewModel model2 = new BookVisitViewModel() 
                { 
                    PreFilteredVisitsList = _context.GetAvailableVisitsQuery() 
                };
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

                //model2.AllCategories = _context.GetVisitCategories().OrderBy(c => c.CategoryName).ToList();
                //model2.AllLocations = _context.GetAllLocations().OrderBy(c => c.Name).ToList();
                //model2.AllMedicalServices = _context.GetMedicalServices().OrderBy(c => c.Name).ToList();
                //model2.AllMedicalWorkers = _context.GetMedicalWorkers().OrderBy(c => c.FullProffesionalName).ToList();
                model2.UserName = _loggedUser.Person.FullName;
                model2.FilterVisits(_context.GetAvailableVisitsQuery());

                model2.SelectedPatient = patient;
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
            _loggedUser = _context.GetUserById(GetUserId().Value);

            if (_loggedUser != null)
            {
                Patient patient = GetSelectedPatient();

                if (patient == null)
                {
                    return RedirectToAction("Dashboard");
                }
                model.PreFilteredVisitsList = _context.GetAvailableVisitsQuery();
                model.SelectedPatient = patient;
                model.UserName = _loggedUser.Person.FullName;
                //model.AllCategories = _context.GetVisitCategories().OrderBy(c => c.CategoryName).ToList();
                //model.AllLocations = _context.GetAllLocations().OrderBy(c => c.Name).ToList();
                //model.AllMedicalServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService).OrderBy(c => c.Name).ToList();
                //model.AllMedicalWorkers = _context.GetMedicalWorkers().OrderBy(c => c.FullProffesionalName).ToList();

                model.FilterVisits(_context.GetAvailableVisitsQuery());

                return View(model);
            }
            return NotFound();
        }
        [HttpGet]
        public IActionResult BookVisit()
        {
            _loggedUser = _context.GetUserById(GetUserId().Value);

            if (_loggedUser != null)
            {
                Patient patient = GetSelectedPatient();

                if (patient == null)
                {
                    return RedirectToAction("Dashboard");
                }
                BookVisitViewModel model = new BookVisitViewModel(patient) 
                { 
                    PreFilteredVisitsList = _context.GetAvailableVisitsQuery() 
                };
                
                if (TempData.ContainsKey(ViewMessage.MESSAGE_KEY))
                {
                    ViewMessage message = JsonConvert.DeserializeObject<ViewMessage>((string)TempData[ViewMessage.MESSAGE_KEY]);
                    model.ViewMessage = message;
                    TempData.Remove(ViewMessage.MESSAGE_KEY);
                }
                model.FilterVisits(_context.GetAvailableVisitsQuery());

                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            _loggedUser = _context.GetUserById(GetUserId().Value);

            if (_loggedUser != null)
            {
                Patient patient = GetSelectedPatient();

                ContactMessageViewModel model = new ContactMessageViewModel(_loggedUser);
                model.UserName = _loggedUser.Person.FullName;
                model.SelectedPatient = patient;
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Contact(ContactMessageViewModel model)
        {
            _loggedUser = _context.GetUserById(GetUserId().Value);

            if (_loggedUser != null)
            {
                Patient patient = GetSelectedPatient();

                ContactMessageViewModel modelP = new ContactMessageViewModel(_loggedUser);
                modelP.Message = model.Message;
                modelP.Subject = model.Subject;
                bool isSent = ServiceClasses.MailServices.CreateAndSendMail(modelP);
                if (isSent)
                {
                    model.AlertMessage = "Wiadomość została wysłana!";
                    model.AlertMessageType = Enums.AlertMessageType.InfoMessage;
                    ModelState.Clear();
                }
                else
                {
                    model.AlertMessage = "Wystąpił błąd podczas próby wysłania wiadomości! Spróbuj jeszcze raz!";
                    model.AlertMessageType = Enums.AlertMessageType.ErrorMessage;
                    ViewBag.Message = "Wystąpił błąd podczas próby wysłania wiadomości! Spróbuj jeszcze raz!";

                }
                model.UserName = _loggedUser.Person.FullName;
                model.SelectedPatient = patient;
                return View(model);
            }
            return NotFound();

        }
        public IActionResult MedicalWorkersList()
        {
            _loggedUser = _context.GetUserById(GetUserId().Value);

            if (_loggedUser != null)
            {
                Patient patient = GetSelectedPatient();

                List<MedicalWorker> medicalWorkers = _context.GetMedicalWorkers().OrderBy(c => c.FullProffesionalName).ToList();
                MedicalWorkersViewModel model = new MedicalWorkersViewModel(patient, medicalWorkers);
                model.UserName = _loggedUser.Person.FullName;
                model.SelectedPatient = patient;

                return View(model);
            }
            return NotFound();

        }
        public IActionResult MedicalWorkerDetails(string id)
        {
            _loggedUser = _context.GetUserById(GetUserId().Value);

            if (_loggedUser != null)
            {
                Patient patient = GetSelectedPatient();

                if (long.TryParse(id, out long lId))
                {
                    MedicalWorker worker = _context.GetMedicalWorkerDetailsById(lId);//_context.GetMedicalWorkers().Where(c => c.Id.ToString() == id).FirstOrDefault();
                    MedicalWorkerViewModel model = new MedicalWorkerViewModel(patient, worker);
                    model.UserName = _loggedUser.Person.FullName;
                    model.SelectedPatient = patient;
                    return View(model);
                }
            }
            return NotFound();
        }

        public IActionResult VisitDetails(string id)
        {
            _loggedUser = _context.GetUserById(GetUserId().Value);

            if (_loggedUser != null)
            {
                Patient patient = GetSelectedPatient();

                if (long.TryParse(id, out long lid))
                {
                    Visit visit = _context.GetHistoricalVisitById(lid);
                    if (visit == null)
                    {
                        return NotFound();
                    }
                    VisitViewModel model = new VisitViewModel(visit.Patient, visit);
                    model.UserName = _loggedUser.Person.FullName;
                    model.SelectedPatient = patient;

                    return View(model);
                }
                else
                {
                    return RedirectToAction("Dashboard", "CustomerService", new { area = "CustomerServiceArea", id = patient.Id });
                }
            }
            return NotFound();

        }

        public IActionResult HistoricalVisits()
        {
            _loggedUser = _context.GetUserById(GetUserId().Value);

            if (_loggedUser != null)
            {
                Patient patient = GetSelectedPatient();

                if (patient == null)
                {
                    return RedirectToAction("Dashboard");
                }
                CustomerServiceArea.Models.PatientViewModel model = new PatientViewModel(patient);
                model.AllVisits = _context.GetHistoricalVisitsByPatientIdQuery(patient.Id);
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            return NotFound();

        }
        public IActionResult PlannedVisits()
        {
            _loggedUser = _context.GetUserById(GetUserId().Value);

            if (_loggedUser != null)
            {
                Patient patient = GetSelectedPatient();

                if (patient == null)
                {
                    return RedirectToAction("Dashboard");
                }

                CustomerServiceArea.Models.PatientViewModel model = new PatientViewModel(patient);
                model.AllVisits = _context.GetBookedVisitsByPatientIdQuery(patient.Id);
                model.UserName = _loggedUser.Person.FullName;
                model.SelectedPatient = patient;

                return View(model);
            }
            return NotFound();

        }
        public IActionResult ResignFromVisit(string id)
        {
            _loggedUser = _context.GetUserById(GetUserId().Value);

            if (_loggedUser != null)
            {
                Patient patient = GetSelectedPatient();

                if (patient == null)
                {
                    return RedirectToAction("Dashboard");
                }

                if (long.TryParse(id, out long lid))
                {
                    Visit plannedVisit = _context.GetBookedVisitsByPatientId(patient.Id).Where(c => c.Id == lid).FirstOrDefault(); //selectedPatient.BookedVisits.Where(c => c.Id == lid).FirstOrDefault();
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
            _loggedUser = _context.GetUserById(GetUserId().Value);

            if (_loggedUser != null)
            {
                Patient patient = GetSelectedPatient();

                if (patient == null)
                {
                    return RedirectToAction("Dashboard");
                }

                if (long.TryParse(id, out long lid))
                {
                    Visit plannedVisit = _context.GetBookedVisitsByPatientId(patient.Id).Where(c => c.Id == lid).FirstOrDefault(); //selectedPatient.BookedVisits.Where(c => c.Id == lid).FirstOrDefault();
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
                            model.SelectedPatient = patient;//_context.CurrentPatient;
                            model.UserName = _loggedUser.Person.FullName;

                            return View(model);
                        }
                        else
                        {
                            return NotFound();
                        }
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
            _loggedUser = _context.GetUserById(GetUserId().Value);

            if (_loggedUser != null)
            {
                Patient patient = GetSelectedPatient();

                if (patient == null)
                {
                    return RedirectToAction("Dashboard");
                }

                Visit plannedVisit = _context.GetBookedVisitsByPatientId(patient.Id).Where(c => c.Id == model.RescheduledVisitId).FirstOrDefault();// selectedPatient.BookedVisits.Where(c => c.Id == model.RescheduledVisitId).FirstOrDefault();
                if (plannedVisit != null)
                {
                    if (plannedVisit.DateTimeSince > DateTimeOffset.Now)
                    {
                        model.SelectedPrimaryServiceId = plannedVisit.PrimaryService.Id.ToString();
                        model.RescheduledVisitId = model.RescheduledVisitId;
                        model.RescheduledVisit = plannedVisit;
                        model.MedicalServices = _context.GetMedicalServices();
                        model.AllVisitsList = _context.GetAvailableVisitsQuery();
                        model.SelectedPatient = patient;//_context.CurrentPatient;
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
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult RescheduleVisitFinal(RescheduleVisitSelectNewViewModel model)
        {
            _loggedUser = _context.GetUserById(GetUserId().Value);

            if (_loggedUser != null)
            {
                Patient patient = GetSelectedPatient();

                if (patient == null)
                {
                    return RedirectToAction("Dashboard");
                }

                Visit visitToReschedule = _context.GetBookedVisitsByPatientId(patient.Id).Where(c => c.Id == model.RescheduledVisitId).FirstOrDefault();// selectedPatient.BookedVisits.Where(c => c.Id == model.RescheduledVisitId).FirstOrDefault();
                if (visitToReschedule != null)
                {
                    Visit newVisit = _context.GetFutureVisitById(model.SelectedNewVisitId);
                    if (newVisit != null)
                    {
                        _context.BookVisit(patient.Id, newVisit.Id);
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
            _loggedUser = _context.GetUserById(GetUserId().Value);

            if (_loggedUser != null)
            {
                Patient patient = GetSelectedPatient();

                if (patient == null)
                {
                    return RedirectToAction("Dashboard");
                }

                CustomerServiceArea.Models.PatientViewModel model = new PatientViewModel(patient);
                model.UserName = _loggedUser.Person.FullName;
                model.Prescriptions = _context.GetPrescriptionsByPatientId(patient.Id);
                return View(model);
            }
            return NotFound();

        }
        public IActionResult Referrals()
        {
            _loggedUser = _context.GetUserById(GetUserId().Value);

            if (_loggedUser != null)
            {
                Patient patient = GetSelectedPatient();

                if (patient == null)
                {
                    return RedirectToAction("Dashboard");
                }

                CustomerServiceArea.Models.PatientViewModel model = new PatientViewModel(patient);
                model.MedicalReferrals = _context.GetMedicalReferralsByPatientIdQuery(patient.Id).ToList();
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            return NotFound();
        }
        public IActionResult TestResults()
        {
            _loggedUser = _context.GetUserById(GetUserId().Value);

            if (_loggedUser != null)
            {
                Patient patient = GetSelectedPatient();

                CustomerServiceArea.Models.PatientViewModel model = new PatientViewModel(patient);
                model.UserName = _loggedUser.Person.FullName;
                model.TestResults = _context.GetTestResultsByPatientId(patient.Id);
                return View(model);
            }
            return NotFound();

        }

        public IActionResult DownloadFile(string id)
        {
            _loggedUser = _context.GetUserById(GetUserId().Value);

            if (_loggedUser != null)
            {
                Patient patient = GetSelectedPatient();

                if (patient == null)
                {
                    return RedirectToAction("Dashboard");
                }

                if (long.TryParse(id, out long idL))
                {
                    MedicalTestResult medicalTestResult = _context.GetMedicalTestResultById(idL);

                    if (medicalTestResult.PatientId != patient.Id)
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
