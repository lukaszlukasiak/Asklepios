using Asklepios.Core.Models;
using Asklepios.Data.Interfaces;
using Asklepios.Web.Areas.HomeArea.Models;
using Asklepios.Web.Areas.PatientArea.Models;
using Asklepios.Web.Extensions;
using Asklepios.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.PatientArea.Controllers
{
    [Area("PatientArea")]
    [Authorize(Roles = "Patient")]
    public class PatientController : Controller
    {

        IPatientModuleRepository _context;
        //private User _loggedUser { get; set; }
        //private static Person _person { get; set; }
        //private static Patient _selectedPatient { get; set; }
        SignInManager<User> _signManager { get; set; }
        IWebHostEnvironment _hostEnvironment { get; set; }
        long UserId { get; set; }
        private User _loggedUser;
        private Patient _patient;

        //User loggedUser
        //{
        //    get
        //    {
        //        if (_loggedUser==null)
        //        {
        //            _loggedUser = _context.GetUserById(UserId);
        //        }
        //        else
        //        {
        //            return _loggedUser;
        //        }
        //    }
        //    set
        //    {
        //        loggedUser = value;
        //    }
        //}

        public PatientController(IPatientModuleRepository context, IWebHostEnvironment hostEnvironment, SignInManager<User> signManager)
        {
            _context = context;
            _signManager = signManager;
            _hostEnvironment = hostEnvironment;
            //HttpContext.Session.Set()

        }

        public async Task<IActionResult> LogOutAsync()
        {
            await _signManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home", new { area = "HomeArea" });
        }

        public IActionResult Index()
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser == null)
            {
            }
            else
            {
            }
            return RedirectToAction("Dashboard");
        }
        public IActionResult Dashboard()
        {

            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);
            if (_loggedUser == null)
            {
                return NotFound();
            }
            else
            {
                _patient = _context.GetPatientByUserId(_loggedUser.Id);

                Patient patient = _context.GetPatientById(_patient.Id);
                PatientArea.Models.PatientViewModel viewModel = new Models.PatientViewModel(patient)
                {
                    Visits = _context.GetAllVisitsByPatientIdQuery(patient.Id),
                    UserName = _loggedUser.Person.FullName,
                    Notifications = _context.GetNotificationsByPatientId(_patient.Id)
                };

                //viewModel.
                return View(viewModel);
            }
        }
        public IActionResult UserProfile()
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                _patient = _context.GetPatientByUserId(_loggedUser.Id);

                //MedicalWorker medicalWorker = _context.GetMedicalWorkerByUserId(User.);
                //Patient patient =_context.GetPatientById(_patient.Id);

                PatientViewModel model = new PatientViewModel(_patient);
                model.UserName = _loggedUser.Person.FullName;
                model.Notifications = _context.GetNotificationsByPatientId(_patient.Id);


                return View(model);
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult Locations()
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                _patient = _context.GetPatientByUserId(_loggedUser.Id);

                LocationsViewModel model = new LocationsViewModel();
                model.Locations = _context.GetAllLocations().ToList();
                model.UserName = _loggedUser.Person.FullName;
                model.Notifications = _context.GetNotificationsByPatientId(_patient.Id);

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }


        public IActionResult NoReferral(string id)
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {

                if (long.TryParse(id, out long lid))
                {
                    _patient = _context.GetPatientByUserId(_loggedUser.Id);

                    VisitViewModel model = new VisitViewModel();
                    Visit visit = _context.GetFutureVisitById(lid);
                    model.Visit = visit;
                    model.UserName = _loggedUser.Person.FullName;
                    model.Notifications = _context.GetNotificationsByPatientId(_patient.Id);

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
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {

                if (long.TryParse(id, out long lid))
                {

                    Visit visit = _context.GetFutureVisitById(lid);
                    _patient = _context.GetPatientByUserId(_loggedUser.Id);

                    if (visit.PrimaryService.RequireRefferal)
                    {
                        if (_patient.MedicalReferrals != null)
                        {
                            if (_patient.MedicalReferrals.Count > 0)
                            {
                                MedicalReferral referral = _patient.MedicalReferrals.Where(c => c.PrimaryMedicalService == visit.PrimaryService && c.IsActive).FirstOrDefault();
                                if (referral != null)
                                {
                                    referral.HasBeenUsed = true;
                                    referral.VisitWhenUsed = visit;
                                    //_selectedPatient.BookVisit(visit);

                                    _context.UpdateReferral(referral);
                                    _context.BookVisit(_patient.Id, visit.Id);
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
                            return RedirectToAction("NoReferral", "Patient", new { area = "PatientArea", id = visit.Id });
                        }
                    }
                    else
                    {
                        _context.BookVisit(_patient.Id, visit.Id);
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
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                _patient = _context.GetPatientByUserId(_loggedUser.Id);

                BookVisitViewModel viewModel = new BookVisitViewModel(model);

                IQueryable<Visit> visits = _context.GetAvailableVisitsQuery();

                if (viewModel.HasPredefinedMedicalWorker)
                {
                    if (long.TryParse(viewModel.SelectedMedicalWorkerId, out long mwid))
                    {
                        MedicalWorker worker = _context.GetMedicalWorkerById(mwid);
                        visits = visits.Where(c => c.MedicalWorkerId == mwid).AsQueryable();
                        viewModel.SelectedMedicalWorkerName = worker.FullProffesionalName;
                    }
                }
                if (viewModel.HasPredefinedService)
                {
                    if (long.TryParse(viewModel.SelectedServiceId, out long msid))
                    {
                        visits = visits.Where(c => c.PrimaryServiceId == msid).AsQueryable();
                        MedicalService medicalService = _context.GetMedicalServiceById(msid);
                        viewModel.SelectedMedicalServiceName = medicalService.Name;
                    }
                }
                if (viewModel.HasPredefinedCategory)
                {
                    if (long.TryParse(viewModel.SelectedCategoryId, out long cid))
                    {
                        visits = visits.Where(c => c.VisitCategoryId == cid).AsQueryable();
                    }
                }
                if (viewModel.HasPredefinedLocation)
                {
                    if (long.TryParse(viewModel.SelectedLocationId, out long lid))
                    {
                        visits = visits.Where(c => c.LocationId == lid).AsQueryable();
                    }
                }


                //if (!viewModel.HasAnythingPredefined)
                //{
                viewModel.AllCategories = _context.GetVisitCategories().ToList();
                viewModel.AllLocations = _context.GetAllLocations().ToList();
                viewModel.AllMedicalServices = _context.GetMedicalServices().ToList();
                viewModel.AllMedicalWorkers = _context.GetMedicalWorkers().ToList();
                //}
                viewModel.PreFilteredVisitsList = visits;
                viewModel.UserName = _loggedUser.Person.FullName;
                viewModel.Notifications = _context.GetNotificationsByPatientId(_patient.Id);
                viewModel.FilterVisits(_context.GetFutureVisitsQueryPatient());
                CreatePageSelectList(viewModel);

                return View(viewModel);
            }
            else
            {
                return NotFound();
            }
        }
        private void CreatePageSelectList(BookVisitViewModel model)
        {
            List<PageSelect> items = new List<PageSelect>();
            for (int i = 1; i <= model.NumberOfPages; i++)
            {
                PageSelect page = new PageSelect();
                page.Value = i.ToString();
                page.Id = i;
                items.Add(page);
            }
            SelectList pagesList = new SelectList(items, "Id", "Value", model.CurrentPageNum);
            ViewData["PagesList"] = pagesList;
        }

        [HttpPost]
        public IActionResult BookVisit(BookVisitViewModel model)
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                _patient = _context.GetPatientByUserId(_loggedUser.Id);

                model.PreFilteredVisitsList = _context.GetAvailableVisitsQuery();
                model.UserName = _loggedUser.Person.FullName;
                model.Notifications = _context.GetNotificationsByPatientId(_patient.Id);
                model.FilterVisits(_context.GetFutureVisitsQueryPatient());
                CreatePageSelectList(model);

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
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                _patient = _context.GetPatientByUserId(_loggedUser.Id);

                BookVisitViewModel model = new BookVisitViewModel()
                {
                    PreFilteredVisitsList = _context.GetAvailableVisitsQuery()
                }; //(_context.GetAvailableVisits().ToList(),new VisitSearchOptions());
                model.UserName = _loggedUser.Person.FullName;
                model.Notifications = _context.GetNotificationsByPatientId(_patient.Id);
                model.FilterVisits(_context.GetFutureVisitsQueryPatient());
                CreatePageSelectList(model);

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
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                _patient = _context.GetPatientByUserId(_loggedUser.Id);

                ContactMessageViewModel model = new ContactMessageViewModel(_patient, _loggedUser.Person.FullName);
                model.UserName = _loggedUser.Person.FullName;
                model.Notifications = _context.GetNotificationsByPatientId(_patient.Id);

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
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                _patient = _context.GetPatientByUserId(_loggedUser.Id);

                ContactMessageViewModel modelP = new ContactMessageViewModel(_patient, _loggedUser.Person.FullName);
                modelP.Message = model.Message;
                modelP.Subject = model.Subject;
                //ContactMessageViewModel model = new ContactMessageViewModel();
                bool isSent = ServiceClasses.MailServices.CreateAndSendMail(modelP);
                if (isSent)
                {
                    model.AlertMessage = "Wiadomość została wysłana!";
                    model.AlertMessageType = Enums.AlertMessageType.InfoMessage;
                }
                else
                {
                    model.AlertMessage = "Wystąpił błąd podczas próby wysłania wiadomości! Spróbuj jeszcze raz!";
                    model.AlertMessageType = Enums.AlertMessageType.ErrorMessage;
                    ViewBag.Message = "Wystąpił błąd podczas próby wysłania wiadomości! Spróbuj jeszcze raz!";

                }
                model.UserName = _loggedUser.Person.FullName;
                model.Notifications = _context.GetNotificationsByPatientId(_patient.Id);

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
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                _patient = _context.GetPatientByUserId(_loggedUser.Id);

                VisitViewModel model = new VisitViewModel();
                //List<MedicalWorker> medicalWorkers = _context.GetMedicalWorkers().ToList();
                model.UserName = _loggedUser.Person.FullName;
                model.Notifications = _context.GetNotificationsByPatientId(_patient.Id);
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
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                _patient = _context.GetPatientByUserId(_loggedUser.Id);

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
                model.Notifications = _context.GetNotificationsByPatientId(_patient.Id);

                return RedirectToAction("NotificationsList");
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult SetNotificationAsReadDetails(long id)
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                _patient = _context.GetPatientByUserId(_loggedUser.Id);

                VisitViewModel model = new VisitViewModel();
                Notification notification = _context.GetNotificationById(id);
                //Visit visit = _context.GetHistoricalVisitById(notification.VisitId);
                if (notification == null)
                {
                    return NotFound();
                }
                notification.IsRead = true;
                _context.UpdateNotification(notification);

                //List<MedicalWorker> medicalWorkers = _context.GetMedicalWorkers().ToList();
                model.UserName = _loggedUser.Person.FullName;
                model.Notifications = _context.GetNotificationsByPatientId(_patient.Id);

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
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                _patient = _context.GetPatientByUserId(_loggedUser.Id);

                MedicalWorkersListViewModel model = new MedicalWorkersListViewModel();
                //List<MedicalWorker> medicalWorkers = _context.GetMedicalWorkers().ToList();
                model.MedicalWorkers = _context.GetMedicalWorkers().ToList();
                model.UserName = _loggedUser.Person.FullName;
                model.Notifications = _context.GetNotificationsByPatientId(_patient.Id);
                return View(model);
            }
            else
            {
                return NotFound();
            }

        }
        public IActionResult MedicalWorkerDetails(string id)
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                _patient = _context.GetPatientByUserId(_loggedUser.Id);

                MedicalWorker worker = _context.GetMedicalWorkers().Where(c => c.Id.ToString() == id).FirstOrDefault();
                MedicalWorkerViewModel model = new MedicalWorkerViewModel();
                model.MedicalWorker = worker;
                model.UserName = _loggedUser.Person.FullName;
                model.Notifications = _context.GetNotificationsByPatientId(_patient.Id);

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
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                _patient = _context.GetPatientByUserId(_loggedUser.Id);

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
                    model.Notifications = _context.GetNotificationsByPatientId(_patient.Id);

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
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                _patient = _context.GetPatientByUserId(_loggedUser.Id);

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
                        model.Notifications = _context.GetNotificationsByPatientId(_patient.Id);

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
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                _patient = _context.GetPatientByUserId(_loggedUser.Id);

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
                    model.Notifications = _context.GetNotificationsByPatientId(_patient.Id);

                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Patient", new { area = "PatientArea", id = _patient.Id });
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
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                _patient = _context.GetPatientByUserId(_loggedUser.Id);

                //Patient patient = _context.CurrentPatient;
                PatientArea.Models.PatientViewModel model = new Models.PatientViewModel(_patient);
                model.Visits = _context.GetHistoricalVisitsByPatientIdQuery(_patient.Id);
                model.UserName = _loggedUser.Person.FullName;
                model.Notifications = _context.GetNotificationsByPatientId(_patient.Id);

                return View(model);
            }
            else
            {
                return NotFound();
            }

        }
        public IActionResult PlannedVisits()
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                _patient = _context.GetPatientByUserId(_loggedUser.Id);

                //Patient patient = _context.CurrentPatient;
                //_selectedPatient.AllVisits.AddRange(_context.GetBookedVisitsByPatientId(_selectedPatient.Id));
                //_selectedPatient.AllVisits = _context.GetBookedVisitsByPatientId(_selectedPatient.Id);
                //_selectedPatient.BookedVisits = _context.GetBookedVisitsByPatientId(_selectedPatient.Id);
                PatientArea.Models.PatientViewModel model = new PatientViewModel(_patient)
                {
                    Visits = _context.GetBookedVisitsByPatientIdQuery(_patient.Id),
                    UserName = _loggedUser.Person.FullName,
                    Notifications = _context.GetNotificationsByPatientId(_patient.Id)
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
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                if (long.TryParse(id, out long lid))
                {
                    _patient = _context.GetPatientByUserId(_loggedUser.Id);

                    //   Visit plannedVisit = _selectedPatient.BookedVisits.Where(c => c.Id == lid).FirstOrDefault();
                    Visit plannedVisit = _context.GetBookedVisitsByPatientId(_patient.Id).FirstOrDefault(c => c.Id == lid);//_selectedPatient.BookedVisits.Where(c => c.Id == lid).FirstOrDefault();

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
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                if (long.TryParse(id, out long lid))
                {
                    _patient = _context.GetPatientByUserId(_loggedUser.Id);

                    Visit plannedVisit = _context.GetBookedVisitsByPatientId(_patient.Id).FirstOrDefault(c => c.Id == lid);//_selectedPatient.BookedVisits.Where(c => c.Id == lid).FirstOrDefault();
                    if (plannedVisit != null)
                    {
                        if (plannedVisit.DateTimeSince > DateTimeOffset.Now)
                        {
                            RescheduleVisitViewModel model = new RescheduleVisitViewModel();
                            model.AllVisitsList = _context.GetAvailableVisitsQuery();
                            model.MedicalServices = _context.GetMedicalServices();
                            model.Notifications = _context.GetNotificationsByPatientId(_patient.Id);

                            model.RescheduledVisitId = lid;
                            model.RescheduledVisit = plannedVisit;

                            model.SelectedPrimaryServiceId = plannedVisit.PrimaryService.Id.ToString();
                            model.UserName = _loggedUser.Person.FullName;
                            model.Notifications = _context.GetNotificationsByPatientId(_patient.Id);
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
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                _patient = _context.GetPatientByUserId(_loggedUser.Id);

                Visit plannedVisit = _context.GetBookedVisitsByPatientId(_patient.Id).FirstOrDefault(c => c.Id == model.RescheduledVisitId);// _selectedPatient.BookedVisits.Where(c => c.Id == model.RescheduledVisitId).FirstOrDefault();
                if (plannedVisit != null)
                {

                    if (plannedVisit.DateTimeSince > DateTimeOffset.Now)
                    {
                        model.AllVisitsList = _context.GetAvailableVisitsQuery();

                        model.MedicalServices = _context.GetMedicalServices();
                        model.Notifications = _context.GetNotificationsByPatientId(_patient.Id);

                        //model.RescheduledVisitId = lid;
                        model.RescheduledVisit = plannedVisit;

                        model.SelectedPrimaryServiceId = plannedVisit.PrimaryService.Id.ToString();

                        //RescheduleVisitViewModel model = new RescheduleVisitViewModel();
                        //model.SelectedPrimaryServiceId = plannedVisit.PrimaryService.Id.ToString();
                        model.UserName = _loggedUser.Person.FullName;
                        model.Notifications = _context.GetNotificationsByPatientId(_patient.Id);

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
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                _patient = _context.GetPatientByUserId(_loggedUser.Id);

                //Visit visitToReschedule = _selectedPatient.BookedVisits.Where(c => c.Id == model.RescheduledVisitId).FirstOrDefault();
                Visit visitToReschedule = _context.GetBookedVisitsByPatientId(_patient.Id).FirstOrDefault(c => c.Id == model.RescheduledVisitId);// _selectedPatient.BookedVisits.Where(c => c.Id == model.RescheduledVisitId).FirstOrDefault();

                if (visitToReschedule != null)
                {
                    Visit newVisit = _context.GetFutureVisitById(model.SelectedNewVisitId);
                    if (newVisit != null)
                    {

                        newVisit.Patient = _patient;
                        if (visitToReschedule.UsedExaminationReferral != null)
                        {
                            newVisit.UsedExaminationReferral = visitToReschedule.UsedExaminationReferral;
                        }
                        _context.BookVisit(_patient.Id, newVisit.Id);
                        _context.ResignFromVisit(visitToReschedule.Id);//, _selectedPatient);
                        model.Notifications = _context.GetNotificationsByPatientId(_patient.Id);

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
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                _patient = _context.GetPatientByUserId(_loggedUser.Id);

                //Patient patient = _context.CurrentPatient;
                PatientArea.Models.PatientViewModel model = new Models.PatientViewModel(_patient)
                {
                    Visits = _context.GetAllVisitsByPatientIdQuery(_patient.Id),
                    Prescriptions = _context.GetPrescriptionsByPatientIdQuery(_patient.Id),
                    UserName = _loggedUser.Person.FullName,
                    Notifications = _context.GetNotificationsByPatientId(_patient.Id)
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
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                _patient = _context.GetPatientByUserId(_loggedUser.Id);

                PatientArea.Models.PatientViewModel model = new Models.PatientViewModel(_patient)
                {
                    Visits = _context.GetAllVisitsByPatientIdQuery(_patient.Id),
                    MedicalReferrals = _context.GetMedicalReferralsByPatientIdQuery(_patient.Id),
                    UserName = _loggedUser.Person.FullName,
                    Notifications = _context.GetNotificationsByPatientId(_patient.Id)
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
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                _patient = _context.GetPatientByUserId(_loggedUser.Id);

                PatientArea.Models.PatientViewModel model = new Models.PatientViewModel(_patient)
                {
                    Visits = _context.GetAllVisitsByPatientIdQuery(_patient.Id),
                    MedicalTestResults = _context.GetMedicalTestResultsByPatientIdQuery(_patient.Id),
                    UserName = _loggedUser.Person.FullName,
                    Notifications = _context.GetNotificationsByPatientId(_patient.Id)
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
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                if (long.TryParse(id, out long lid))
                {
                    _patient = _context.GetPatientByUserId(_loggedUser.Id);


                    //Patient patient = _context.GetPatientById(lid);
                    PatientViewModel model = new PatientViewModel(_patient);
                    model.UserName = _loggedUser.Person.FullName;
                    model.Notifications = _context.GetNotificationsByPatientId(_patient.Id);
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
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                _patient = _context.GetPatientByUserId(_loggedUser.Id);

                //Build the File Path.
                if (long.TryParse(id, out long idL))
                {
                    MedicalTestResult medicalTestResult = _context.GetMedicalTestResultById(idL);
                    if (medicalTestResult.PatientId != _patient.Id)
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
