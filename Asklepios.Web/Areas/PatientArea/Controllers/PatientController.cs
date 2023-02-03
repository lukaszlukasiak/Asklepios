using Asklepios.Core.Models;
using Asklepios.Data.Interfaces;
using Asklepios.Web.Areas.HomeArea.Models;
using Asklepios.Web.Areas.PatientArea.Models;
using Asklepios.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        private static User _loggedUser { get; set; }
        private static Person _person { get; set; }
        private static Patient _selectedPatient { get; set; }

        IWebHostEnvironment _hostEnvironment { get; set; }

        public PatientController(IPatientModuleRepository context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        internal static void LogOut()
        {
            _loggedUser = null;
            _person = null;
            _selectedPatient = null;
        }

        public IActionResult Index()
        {
            if (_loggedUser == null)
            {
                if (TempData.ContainsKey("User") == true)
                {
                    User user = JsonConvert.DeserializeObject<User>((string)TempData["User"]);
                    _loggedUser = user;
                    _selectedPatient = _context.GetPatientByUserId(_loggedUser.Id);
                    _loggedUser.Person = _selectedPatient.Person;

                }

            }
            else
            {
            }
            return RedirectToAction("Dashboard");

        }
        public IActionResult Dashboard()
        {
            if (_loggedUser == null)
            {
                return NotFound();
            }
            else
            {

                //_selectedPatient.AllVisits.AddRange(_context.GetHistoricalVisitsByPatientId(_selectedPatient.Id));
                //_selectedPatient.AllVisits.AddRange(_context.GetBookedVisitsByPatientId(_selectedPatient.Id));
                //_selectedPatient.HistoricalVisits = _context.GetHistoricalVisitsByPatientId(_selectedPatient.Id);
                //_selectedPatient.BookedVisits = _context.GetBookedVisitsByPatientId(_selectedPatient.Id);
                Patient patient = _context.GetPatientById(_selectedPatient.Id);
                PatientArea.Models.PatientViewModel viewModel = new Models.PatientViewModel(patient)
                {
                    Visits = _context.GetAllVisitsByPatientIdQuery(patient.Id),
                    UserName = _loggedUser.Person.FullName,
                    Notifications = _context.GetNotificationsByPatientId(_selectedPatient.Id)
                };
                //viewModel.
                return View(viewModel);
            }
            //if (id == null)
            //{
            //    //id = _context.GetPatientData().Id.ToString();
            //    if (_selectedPatient?.Id!=null)
            //    {
            //        id = _selectedPatient.Id.ToString();
            //    }
            //    else
            //    {
            //        return NotFound();
            //    }
            //}
            //if (int.TryParse(id, out int parsedId))
            //{
            //    _loggedUser = _context.GetUser(parsedId);
            //    //_person = _context.GetPerson(_loggedUser.PersonId);
            //    _selectedPatient = _context.GetPatientById(_loggedUser.PersonId);

            //    PatientArea.Models.PatientViewModel viewModel = new Models.PatientViewModel(_selectedPatient);
            //    return View(viewModel);
            //}
            //else
            //{
            //    return null;
            //}
        }
        public IActionResult UserProfile()
        {
            if (_loggedUser != null)
            {
                //MedicalWorker medicalWorker = _context.GetMedicalWorkerByUserId(User.);
                Patient patient=_context.GetPatientById(_selectedPatient.Id);

                PatientViewModel model = new PatientViewModel(patient);
                model.UserName = _loggedUser.Person.FullName;
                model.Notifications = _context.GetNotificationsByPatientId(_selectedPatient.Id);
                

                return View(model);
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

                LocationsViewModel model = new LocationsViewModel();
                model.Locations = _context.GetAllLocations().ToList();
                model.UserName = _loggedUser.Person.FullName;
                model.Notifications = _context.GetNotificationsByPatientId(_selectedPatient.Id);

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }


        public IActionResult NoReferral(string id)
        {
            if (_loggedUser != null)
            {

                if (long.TryParse(id, out long lid))
                {
                    VisitViewModel model = new VisitViewModel();
                    Visit visit = _context.GetFutureVisitById(lid);
                    model.Visit = visit;
                    model.UserName = _loggedUser.Person.FullName;
                    model.Notifications = _context.GetNotificationsByPatientId(_selectedPatient.Id);

                    return View(model);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult BookSpecifiedVisit(string id)
        {
            if (_loggedUser != null)
            {

                if (long.TryParse(id, out long lid))
                {
                    //Patient patient = _context.CurrentPatient;

                    Visit visit = _context.GetFutureVisitById(lid);

                    if (visit.PrimaryService.RequireRefferal)
                    {
                        if (_selectedPatient.MedicalReferrals.Count > 0)
                        {
                            MedicalReferral referral = _selectedPatient.MedicalReferrals.Where(c => c.PrimaryMedicalService == visit.PrimaryService && c.IsActive).FirstOrDefault();
                            if (referral != null)
                            {
                                referral.HasBeenUsed = true;
                                referral.VisitWhenUsed = visit;
                                //_selectedPatient.BookVisit(visit);

                                _context.UpdateReferral(referral);
                                _context.BookVisit(_selectedPatient.Id, visit.Id);
                                return RedirectToAction("PlannedVisits");
                            }
                            else
                            {
                                return RedirectToAction("NoReferral", "Patient", new { area = "PatientArea", id = visit.Id });
                            }
                        }
                        else
                        {
                            return RedirectToAction("NoReferral", "Patient", new { area = "PatientArea", id = visit.Id });
                        }
                    }
                    else
                    {
                        //_selectedPatient.BookVisit(visit);
                        _context.BookVisit(_selectedPatient.Id, visit.Id);
                        //_context.UpdateVisit(visit);
                        return RedirectToAction("PlannedVisits");
                    }

                }
                else
                {
                    return RedirectToAction("BookVisit", "Patient", new { area = "PatientArea" });
                }
            }
            else
            {
                return NotFound();
            }

        }
        [HttpPost]
        public IActionResult BookVisitConditions(SearchViewModel model)
        {
            if (_loggedUser != null)
            {
                BookVisitViewModel model2 = new BookVisitViewModel()
                {
                    AllVisitsList = _context.GetAvailableVisitsQuery()
                }; //(_context.GetAvailableVisits().ToList(),new VisitSearchOptions());
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
                model2.Notifications = _context.GetNotificationsByPatientId(_selectedPatient.Id);

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

                //BookVisitViewModel bookVisitViewModel = new BookVisitViewModel(_context.GetAvailableVisits().ToList(), model);
                model.AllVisitsList = _context.GetAvailableVisitsQuery();
                model.UserName = _loggedUser.Person.FullName;
                model.Notifications = _context.GetNotificationsByPatientId(_selectedPatient.Id);

                return View(model);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpGet]
        public IActionResult BookVisit()
        {
            if (_loggedUser != null)
            {
                BookVisitViewModel model = new BookVisitViewModel()
                {
                    AllVisitsList = _context.GetAvailableVisitsQuery()
                }; //(_context.GetAvailableVisits().ToList(),new VisitSearchOptions());
                model.UserName = _loggedUser.Person.FullName;
                model.Notifications = _context.GetNotificationsByPatientId(_selectedPatient.Id);

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

                ContactMessageViewModel model = new ContactMessageViewModel(_selectedPatient, _loggedUser.Person.FullName);
                model.UserName = _loggedUser.Person.FullName;
                model.Notifications = _context.GetNotificationsByPatientId(_selectedPatient.Id);

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

                ContactMessageViewModel modelP = new ContactMessageViewModel(_selectedPatient, _loggedUser.Person.FullName);
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
                model.UserName = _loggedUser.Person.FullName;
                model.Notifications = _context.GetNotificationsByPatientId(_selectedPatient.Id);

                return View(model);
            }
            else
            {
                return NotFound();
            }

        }
        //public IActionResult MedicalAdvice()
        //{
        //    return View();
        //}
        public IActionResult NotificationsList()
        {
            if (_loggedUser != null)
            {
                VisitViewModel model = new VisitViewModel();
                //List<MedicalWorker> medicalWorkers = _context.GetMedicalWorkers().ToList();
                model.UserName = _loggedUser.Person.FullName;
                model.Notifications = _context.GetNotificationsByPatientId(_selectedPatient.Id);
                foreach (Notification item in model.Notifications)
                {
                    item.Visit = _context.GetHistoricalVisitById(item.VisitId);
                }

                return View(model);
            }
            else
            {
                return NotFound();
            }

        }
        public IActionResult SetNotificationAsReadList(long id)
        {
            if (_loggedUser != null)
            {
                VisitViewModel model = new VisitViewModel();
                Notification notification = _context.GetNotificationById(id);
                if (notification == null)
                {
                    return NotFound();
                }
                notification.IsRead = true;
                _context.UpdateNotification(notification);
                //List<MedicalWorker> medicalWorkers = _context.GetMedicalWorkers().ToList();
                model.UserName = _loggedUser.Person.FullName;
                model.Notifications = _context.GetNotificationsByPatientId(_selectedPatient.Id);

                return RedirectToAction("NotificationsList");
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult SetNotificationAsReadDetails(long id)
        {
            if (_loggedUser != null)
            {
                VisitViewModel model = new VisitViewModel();
                Notification notification = _context.GetNotificationById(id);
                //Visit visit = _context.GetHistoricalVisitById(notification.VisitId);
                if (notification == null)
                {
                    return NotFound();
                }
                notification.IsRead = true;

                //List<MedicalWorker> medicalWorkers = _context.GetMedicalWorkers().ToList();
                model.UserName = _loggedUser.Person.FullName;
                model.Notifications = _context.GetNotificationsByPatientId(_selectedPatient.Id);

                return RedirectToAction("VisitDetails", "Patient", new { area = "PatientArea", id = notification.VisitId });

                // return RedirectToAction("VisitDetails");
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult MedicalWorkersList()
        {
            if (_loggedUser != null)
            {
                MedicalWorkersListViewModel model = new MedicalWorkersListViewModel();
                //List<MedicalWorker> medicalWorkers = _context.GetMedicalWorkers().ToList();
                model.MedicalWorkers = _context.GetMedicalWorkers().ToList();
                model.UserName = _loggedUser.Person.FullName;
                model.Notifications = _context.GetNotificationsByPatientId(_selectedPatient.Id);

                return View(model);
            }
            else
            {
                return NotFound();
            }

        }
        public IActionResult MedicalWorkerDetails(string id)
        {
            if (_loggedUser != null)
            {

                MedicalWorker worker = _context.GetMedicalWorkers().Where(c => c.Id.ToString() == id).FirstOrDefault();
                MedicalWorkerViewModel model = new MedicalWorkerViewModel();
                model.MedicalWorker = worker;
                model.UserName = _loggedUser.Person.FullName;
                model.Notifications = _context.GetNotificationsByPatientId(_selectedPatient.Id);

                return View(model);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpPost]
        public IActionResult RateVisit(RateVisitViewModel model)
        {
            if (_loggedUser != null)
            {

                Visit visit = _context.GetHistoricalVisitById(model.VisitId);
                if (visit == null)
                {
                    return NotFound();
                }
                //RateVisitViewModel model = new RateVisitViewModel() { VisitId = m.VisitId };
                //model.MedicalWorker = _context.GetHistoricalVisitById(model.VisitId).MedicalWorker;
                if (model.IsDataProper)
                {
                    visit.VisitReview = model.GetVisitReview();
                    _context.UpdateVisit(visit);
                    return RedirectToAction("VisitDetails", "Patient", new { area = "PatientArea", id = visit.Id });
                }
                else
                {
                    model.UserName = _loggedUser.Person.FullName;
                    model.Notifications = _context.GetNotificationsByPatientId(_selectedPatient.Id);

                    return View(model);
                }
            }
            else
            {
                return NotFound();
            }

            //visit.VisitReview = model.GetVisitReview();
            //return View(model);

        }
        [HttpGet]
        public IActionResult RateVisit(string id)
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
                    else
                    {
                        RateVisitViewModel model = new RateVisitViewModel() { VisitId = lid };
                        model.MedicalWorker = _context.GetHistoricalVisitById(lid).MedicalWorker;
                        model.UserName = _loggedUser.Person.FullName;
                        model.Notifications = _context.GetNotificationsByPatientId(_selectedPatient.Id);

                        //visit.VisitReview = model.GetVisitReview();
                        return View(model);
                    }
                }
                else
                {
                    return NotFound();

                }
            }
            else
            {
                return NotFound();
            }

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
                    VisitViewModel model = new VisitViewModel();
                    model.Visit = visit;
                    model.UserName = _loggedUser.Person.FullName;
                    model.Notifications = _context.GetNotificationsByPatientId(_selectedPatient.Id);

                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Patient", new { area = "PatientArea", id = _selectedPatient.Id });
                    //return RedirectToAction("Index", "Patient", new { area = "PatientArea", id = _context.CurrentPatient.Id });
                }
            }
            else
            {
                return NotFound();
            }

        }

        public IActionResult HistoricalVisits()
        {
            if (_loggedUser != null)
            {

                //Patient patient = _context.CurrentPatient;
                PatientArea.Models.PatientViewModel model = new Models.PatientViewModel(_selectedPatient);
                model.Visits = _context.GetHistoricalVisitsByPatientIdQuery(_selectedPatient.Id);
                model.UserName = _loggedUser.Person.FullName;
                model.Notifications = _context.GetNotificationsByPatientId(_selectedPatient.Id);

                return View(model);
            }
            else
            {
                return NotFound();
            }

        }
        public IActionResult PlannedVisits()
        {
            if (_loggedUser != null)
            {

                //Patient patient = _context.CurrentPatient;
                //_selectedPatient.AllVisits.AddRange(_context.GetBookedVisitsByPatientId(_selectedPatient.Id));
                //_selectedPatient.AllVisits = _context.GetBookedVisitsByPatientId(_selectedPatient.Id);
                //_selectedPatient.BookedVisits = _context.GetBookedVisitsByPatientId(_selectedPatient.Id);
                PatientArea.Models.PatientViewModel model = new PatientViewModel(_selectedPatient)
                {
                    Visits = _context.GetAllVisitsByPatientIdQuery(_selectedPatient.Id),
                    UserName = _loggedUser.Person.FullName,
                    Notifications = _context.GetNotificationsByPatientId(_selectedPatient.Id)
                };

                return View(model);
            }
            else
            {
                return NotFound();
            }

        }
        public IActionResult ResignFromVisit(string id)
        {
            if (_loggedUser != null)
            {
                if (long.TryParse(id, out long lid))
                {
                 //   Visit plannedVisit = _selectedPatient.BookedVisits.Where(c => c.Id == lid).FirstOrDefault();
                    Visit plannedVisit = _context.GetBookedVisitsByPatientId(_selectedPatient.Id).FirstOrDefault(c => c.Id == lid);//_selectedPatient.BookedVisits.Where(c => c.Id == lid).FirstOrDefault();

                    if (plannedVisit != null)
                    {
                        if (plannedVisit.DateTimeSince > DateTimeOffset.Now)
                        {
                            _context.ResignFromVisit(plannedVisit.Id);//, _selectedPatient);
                            return RedirectToAction("PlannedVisits", "Patient", new { area = "PatientArea" });
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
                    Visit plannedVisit = _context.GetBookedVisitsByPatientId(_selectedPatient.Id).FirstOrDefault(c => c.Id == lid);//_selectedPatient.BookedVisits.Where(c => c.Id == lid).FirstOrDefault();
                    if (plannedVisit != null)
                    {
                        if (plannedVisit.DateTimeSince > DateTimeOffset.Now)
                        {
                            RescheduleVisitViewModel model = new RescheduleVisitViewModel();
                            model.AllVisitsList = _context.GetAvailableVisitsQuery();
                            model.MedicalServices = _context.GetMedicalServices();
                            model.Notifications = _context.GetNotificationsByPatientId(_selectedPatient.Id);

                            model.RescheduledVisitId = lid;
                            model.RescheduledVisit = plannedVisit;

                            model.SelectedPrimaryServiceId = plannedVisit.PrimaryService.Id.ToString();
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
            if (_loggedUser != null)
            {
                Visit plannedVisit = _context.GetBookedVisitsByPatientId(_selectedPatient.Id).FirstOrDefault(c => c.Id == model.RescheduledVisitId);// _selectedPatient.BookedVisits.Where(c => c.Id == model.RescheduledVisitId).FirstOrDefault();
                if (plannedVisit != null)
                {
                    if (plannedVisit.DateTimeSince > DateTimeOffset.Now)
                    {
                        model.AllVisitsList = _context.GetAvailableVisitsQuery();

                        model.MedicalServices = _context.GetMedicalServices();
                        model.Notifications = _context.GetNotificationsByPatientId(_selectedPatient.Id);

                        //model.RescheduledVisitId = lid;
                        model.RescheduledVisit = plannedVisit;

                        model.SelectedPrimaryServiceId = plannedVisit.PrimaryService.Id.ToString();

                        //RescheduleVisitViewModel model = new RescheduleVisitViewModel();
                        //model.SelectedPrimaryServiceId = plannedVisit.PrimaryService.Id.ToString();
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
                //Visit visitToReschedule = _selectedPatient.BookedVisits.Where(c => c.Id == model.RescheduledVisitId).FirstOrDefault();
                Visit visitToReschedule = _context.GetBookedVisitsByPatientId(_selectedPatient.Id).FirstOrDefault(c => c.Id == model.RescheduledVisitId);// _selectedPatient.BookedVisits.Where(c => c.Id == model.RescheduledVisitId).FirstOrDefault();

                if (visitToReschedule != null)
                {
                    Visit newVisit = _context.GetFutureVisitById(model.SelectedNewVisitId);
                    if (newVisit != null)
                    {

                        newVisit.Patient = _selectedPatient;
                        if (visitToReschedule.UsedExaminationReferral != null)
                        {
                            newVisit.UsedExaminationReferral = visitToReschedule.UsedExaminationReferral;
                        }
                        _context.BookVisit(_selectedPatient.Id, newVisit.Id);
                        _context.ResignFromVisit(visitToReschedule.Id);//, _selectedPatient);
                        return RedirectToAction("PlannedVisits", "Patient", new { area = "PatientArea" });
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
                //Patient patient = _context.CurrentPatient;
                PatientArea.Models.PatientViewModel model = new Models.PatientViewModel(_selectedPatient)
                {
                    Visits = _context.GetAllVisitsByPatientIdQuery(_selectedPatient.Id),
                    Prescriptions = _context.GetPrescriptionsByPatientIdQuery(_selectedPatient.Id),
                    UserName = _loggedUser.Person.FullName,
                    Notifications = _context.GetNotificationsByPatientId(_selectedPatient.Id)
                };


                return View(model);
            }
            else
            {
                return NotFound();
            }

        }
        public IActionResult Referrals()
        {
            if (_loggedUser != null)
            {

                PatientArea.Models.PatientViewModel model = new Models.PatientViewModel(_selectedPatient)
                {
                    Visits = _context.GetAllVisitsByPatientIdQuery(_selectedPatient.Id),
                    MedicalReferrals = _context.GetMedicalReferralsByPatientIdQuery(_selectedPatient.Id),
                    UserName = _loggedUser.Person.FullName,
                    Notifications = _context.GetNotificationsByPatientId(_selectedPatient.Id)
                };

                return View(model);
            }
            else
            {
                return NotFound();
            }

        }
        public IActionResult TestResults()
        {
            if (_loggedUser != null)
            {

                PatientArea.Models.PatientViewModel model = new Models.PatientViewModel(_selectedPatient)
                {
                    Visits = _context.GetAllVisitsByPatientIdQuery(_selectedPatient.Id),
                    MedicalTestResults = _context.GetMedicalTestResultsByPatientIdQuery(_selectedPatient.Id),
                    UserName = _loggedUser.Person.FullName,
                    Notifications = _context.GetNotificationsByPatientId(_selectedPatient.Id)
                };

                return View(model);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpGet]
        public IActionResult Notifications(string id)
        {
            if (_loggedUser != null)
            {
                if (long.TryParse(id, out long lid))
                {

                    //Patient patient = _context.GetPatientById(lid);
                    PatientViewModel model = new PatientViewModel(_selectedPatient);
                    model.UserName = _loggedUser.Person.FullName;
                    model.Notifications = _context.GetNotificationsByPatientId(_selectedPatient.Id);
                    foreach (Notification item in model.Notifications)
                    {
                        item.Visit = _context.GetHistoricalVisitById(item.VisitId);
                    }

                    //_selectedPatient.

                    return View(model);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult DownloadFile(string id)
        {
            if (_loggedUser != null)
            {

                //Build the File Path.
                if (long.TryParse(id, out long idL))
                {
                    MedicalTestResult medicalTestResult = _context.GetMedicalTestResultById(idL);
                    if (medicalTestResult.PatientId != _selectedPatient.Id)
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
            else
            {
                return NotFound();
            }
            //string path = Path.Combine(this.Environment.WebRootPath, "Files/") + fileName;
        }
    }
}
