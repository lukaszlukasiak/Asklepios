using Asklepios.Core.Models;
using Asklepios.Data.Interfaces;
using Asklepios.Web.Areas.AdministrativeArea.Models;
using Asklepios.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Asklepios.Web.Areas.AdministrativeArea.Controllers
{
    [Area("AdministrativeArea")]
    public class AdministrativeController : Controller
    {
        //private readonly ILogger<AdministrativeWorkerController> _logger;
        IAdministrationModuleRepository _context;

        //public AdministrativeWorkerController(ILogger<AdministrativeWorkerController> logger)
        //{
        //    _logger = logger;
        //}
        public AdministrativeController(IAdministrationModuleRepository context)
        {
            _context = context;

        }

        private static User _loggedUser { get; set; }
        //private static Person _person { get; set; }
        private static Patient _selectedPatient { get; set; }

        internal static void LogOut()
        {
            _loggedUser = null;
            //_person = null;
            _selectedPatient = null;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
        public IActionResult Index( string id)
        {
            if (int.TryParse(id, out int parsedId))
            {
                _loggedUser = _context.GetUser(parsedId);
                //_person = _context.GetPerson(_loggedUser.PersonId);
                return View();
            }
            else
            {
                if (_loggedUser != null)
                {
                    return View();
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpGet]
        public IActionResult Contact()
        {
            if (_loggedUser != null)
            {
                ContactMessageViewModel model = new ContactMessageViewModel(_loggedUser);
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
        [HttpGet]
        public IActionResult ScheduleItemsManage()
        {
            if (_loggedUser != null)
            {
                ScheduleManageViewModel model = null;// new ScheduleManageViewModel();

                if (TempData["ScheduleSearch"] != null)
                {
                    model = TempData["ScheduleSearch"] as ScheduleManageViewModel;
                }
                else
                {
                    model = new ScheduleManageViewModel();
                }
                List<Visit> visits = _context.GetAvailableVisits();
                model.Schedule = visits;
                model.Locations = _context.GetAllLocations();
                //model.MedicalRooms=_context
                model.MedicalWorkers = _context.GetMedicalWorkers();
                model.PrimaryMedicalServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService == true).ToList();
                model.VisitCategories = _context.GetVisitCategories();
                return View(model);
            }
            return NotFound();
        }
        //[HttpGet]
        //public IActionResult ScheduleItemsManage(ISearchVisit searchVisit)
        //{
        //    if (_loggedUser != null)
        //    {
        //        ScheduleManageViewModel model = null;// new ScheduleManageViewModel();

        //        if (TempData["ScheduleSearch"] != null)
        //        {
        //            model = TempData["ScheduleSearch"] as ScheduleManageViewModel;
        //        }
        //        else
        //        {
        //            model = new ScheduleManageViewModel();
        //        }
        //        List<Visit> visits = _context.GetAvailableVisits();
        //        model.Schedule = visits;
        //        model.Locations = _context.GetAllLocations();
        //        //model.MedicalRooms=_context
        //        model.MedicalWorkers = _context.GetMedicalWorkers();
        //        model.PrimaryMedicalServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService == true).ToList();
        //        model.VisitCategories = _context.GetVisitCategories();
        //        return View(model);
        //    }
        //    return NotFound();
        //}

        [HttpPost]
        public IActionResult ScheduleItemsManage(ScheduleManageViewModel model)
        {
            if (_loggedUser != null)
            {
                List<Visit> visits = _context.GetAvailableVisits();
                model.Schedule = visits;
                model.Locations = _context.GetAllLocations();
                //model.MedicalRooms=_context
                model.MedicalWorkers = _context.GetMedicalWorkers();
                model.VisitCategories = _context.GetVisitCategories();
                if (long.TryParse(model.SelectedLocationId, out long lid))
                {
                    Location location = model.Locations.Where(c => c.Id == lid).FirstOrDefault();
                    model.MedicalRooms = location.MedicalRooms.ToList();
                }
                if (long.TryParse(model.SelectedVisitCategoryId, out long lid2))
                {
                    VisitCategory category = model.VisitCategories.Where(c => c.Id == lid2).FirstOrDefault();
                    model.PrimaryMedicalServices = category.PrimaryMedicalServices.ToList();
                }
                else
                {
                    model.PrimaryMedicalServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService == true).ToList();
                }
                if (long.TryParse(model.SelectedPrimaryServiceId, out long lid3))
                {
                    //MedicalService medicalService = model.PrimaryMedicalServices.Where(c => c.Id == lid3).FirstOrDefault();
                    model.MedicalWorkers = model.MedicalWorkers.Where(c=>c.MedicalServices.Find(d=>d.Id==lid3)!=null).ToList();
                }
                else if (long.TryParse(model.SelectedVisitCategoryId, out long lid4))
                {
                    List<MedicalService> categoryServices= model.VisitCategories.Where(c => c.Id == lid2).FirstOrDefault().PrimaryMedicalServices;
                    model.MedicalWorkers = model.MedicalWorkers.Where(c => c.MedicalServices.Intersect(categoryServices).Count()>1 ).ToList();

                }

                return View(model);
            }
            return NotFound();
        }


        [HttpGet]
        public IActionResult ScheduleItemsAdd()
        {
            if (_loggedUser != null)
            {
                List<Visit> visits = _context.GetAvailableVisits();
                ScheduleItemsAddViewModel model = new ScheduleItemsAddViewModel();
                model.Schedule = visits;
                model.Locations = _context.GetAllLocations();
                //model.MedicalRooms=_context
                model.MedicalWorkers = _context.GetMedicalWorkers();
                model.PrimaryMedicalServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService == true).ToList();
                model.VisitCategories = _context.GetVisitCategories();
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult ScheduleItemsAdd(ScheduleItemsAddViewModel model)
        {
            if (_loggedUser != null)
            {
                if (model.IsValid() && !model.IsDuplicated(_context.GetAvailableVisits()) &&  string.IsNullOrWhiteSpace(model.Guard))
                {
                    List<Visit> visits = CreateNewVisits(model);
                    if (visits!=null)
                    {
                        _context.AddVisitsToSchedule(visits);
                        
                        model.SuccessMessage = "Wizyty zostały dodane";
                        model.ErrorMessage = null;

                        model.VisitCategories = _context.GetVisitCategories();
                        model.PrimaryMedicalServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService == true).ToList();
                        model.MedicalWorkers = _context.GetMedicalWorkers();
                        model.Locations = _context.GetAllLocations();

                        ScheduleItemsAddUpdateLists(model);

                        return View(model);
                    }
                    else
                    {
                        return NotFound();
                    }

                }
                else
                {
                    model.VisitCategories = _context.GetVisitCategories();
                    model.PrimaryMedicalServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService == true).ToList();
                    model.MedicalWorkers = _context.GetMedicalWorkers();
                    model.Locations = _context.GetAllLocations();
                    model.Guard = null;
                    ScheduleItemsAddUpdateLists(model);

                    return View(model);
                }


            }
            return NotFound();
        }

        private List<Visit> CreateNewVisits(ScheduleItemsAddViewModel model)
        {
            Location location = _context.GetLocationById(long.Parse(model.SelectedLocationId));
            MedicalRoom medicalRoom = location.MedicalRooms.Where(c => c.Id == long.Parse(model.SelectedRoomId)).FirstOrDefault();
            MedicalWorker medicalWorker = _context.GetMedicalWorkerById(long.Parse(model.SelectedMedicalWorkerId));
            VisitCategory visitCategory = _context.GetVisitCategoryById(long.Parse(model.SelectedVisitCategoryId));
            MedicalService medicalService = _context.GetMedicalServiceById(long.Parse(model.SelectedMedicalServiceId));

            List<Visit> visitsToAdd = new List<Visit>();

            for (int i = 0; i < model.NumberOfVisitsToAdd; i++)
            {
                DateTimeOffset start = new DateTimeOffset(model.VisitsDate.DateTime, model.FirstVisitTime.Add(new TimeSpan(0, model.DurationOfVisit * i, 0)));
                DateTimeOffset end = new DateTimeOffset(model.VisitsDate.DateTime, model.FirstVisitTime.Add(new TimeSpan(0, model.DurationOfVisit * (i + 1), 0)));

                Visit visit= new Visit(location, medicalRoom, medicalWorker, visitCategory, medicalService, start, end);
                visitsToAdd.Add(visit);
            }
            return visitsToAdd;
        }

        //private static void NewMethod(Location location, MedicalRoom medicalRoom, MedicalWorker medicalWorker, VisitCategory visitCategory, MedicalService medicalService, DateTimeOffset start, DateTimeOffset end)
        //{
        //    Visit visit = new Visit();

        //    visit.DateTimeSince = start;
        //    visit.DateTimeTill = end;
        //    visit.Location = location;
        //    visit.MedicalRoom = medicalRoom;
        //    visit.MedicalWorker = medicalWorker;
        //    visit.VisitCategory = visitCategory;
        //    visit.PrimaryService = medicalService;
        //}

        private void ScheduleItemsAddUpdateLists(ScheduleItemsAddViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.SelectedLocationId))
            {
                if (long.TryParse(model.SelectedLocationId, out long lid))
                {
                    if (lid > 0)
                    {
                        Location location = model.Locations.Where(c => c.Id == lid).FirstOrDefault();
                        model.MedicalRooms = location.MedicalRooms.ToList();
                        model.MedicalWorkers = model.MedicalWorkers.Where(c => c.Person.DefaultAglomeration == location.Aglomeration).ToList();
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(model.SelectedVisitCategoryId))
            {
                if (long.TryParse(model.SelectedVisitCategoryId, out long lid))
                {
                    if (lid > 0)
                    {
                        VisitCategory visitCategory = _context.GetVisitCategoryById(lid);
                        List<MedicalService> services = visitCategory.PrimaryMedicalServices;
                        List<MedicalWorker> workers = model.MedicalWorkers.Where(c => c.MedicalServices.Intersect(visitCategory.PrimaryMedicalServices).Count() > 0).ToList();
                        model.MedicalWorkers = workers;
                        model.PrimaryMedicalServices = services;
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(model.SelectedMedicalServiceId))
            {
                if (long.TryParse(model.SelectedMedicalServiceId, out long lid))
                {
                    if (lid > 0)
                    {
                        MedicalService service = _context.GetMedicalServices().Where(c => c.Id == lid).FirstOrDefault();
                        List<MedicalWorker> workers = model.MedicalWorkers.Where(c => c.MedicalServices.Contains(service)).ToList();
                        model.MedicalWorkers = workers;
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(model.SelectedMedicalWorkerId))
            {
                if (long.TryParse(model.SelectedMedicalWorkerId, out long lid))
                {
                    if (lid > 0)
                    {
                        model.SelectedMedicalWorker = model.MedicalWorkers?.Where(c => c.Id == lid).FirstOrDefault();
                    }
                }
            }
        }
        [HttpPost]
        public IActionResult RemoveSpecifiedVisit(VisitViewModel model)
        {
            if (_loggedUser!=null)
            {
                if (model.Visit != null)
                {
                    Visit visit1 = _context.GetAvailableVisitById(model.Visit.Id);
                    if (visit1!=null)
                    {
                        _context.RemoveVisitById(visit1.Id);
                        ISearchVisit searchOptions = model;
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
                return View(worker);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult PatientItemsAdd(PatientAddViewModel model)
        {
            if (_loggedUser != null)
            {
                model.User.Person = model.Person;
                model.User.UserType = Core.Enums.UserType.Patient;
                model.User.WorkerModuleType = null;
                model.Person.EmailAddress = model.User.UserName;
                model.Patient.Person = model.Person;
                model.Patient.User = model.User;
                

                if (model.IsValid)
                {
                    model.MedicalPackages = _context.GetMedicalPackages();
                    model.NFZUnits = _context.GetNFZUnits();

                    return View(model);
                }
                else
                {
                    model.MedicalPackages = _context.GetMedicalPackages();
                    model.NFZUnits = _context.GetNFZUnits();

                    return View(model);
                }
                //PatientAddViewModel model = new PatientAddViewModel();
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult PatientItemsAdd()
        {
            if (_loggedUser != null)
            {
                PatientAddViewModel model = new PatientAddViewModel();
                model.MedicalPackages = _context.GetMedicalPackages();
                model.NFZUnits = _context.GetNFZUnits();

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult PatientItemsManage()
        {
            if (_loggedUser != null)
            {
                return View();
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult MedicalWorkerItemsAdd()
        {
            if (_loggedUser != null)
            {
                return View();
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult MedicalWorkerItemsManage()
        {
            if (_loggedUser != null)
            {
                return View();
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult NonMedicalWorkerItemsAdd()
        {
            if (_loggedUser != null)
            {
                return View();
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult NonMedicalWorkerItemsManage()
        {
            if (_loggedUser != null)
            {
                return View();
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult LocationItemsAdd()
        {
            if (_loggedUser != null)
            {
                return View();
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult LocationItemsManage()
        {
            if (_loggedUser != null)
            {
                return View();
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult RoomItemsAdd()
        {
            if (_loggedUser != null)
            {
                return View();
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult RoomItemsManage()
        {
            if (_loggedUser != null)
            {
                return View();
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult MedicalPackageItemsAdd()
        {
            if (_loggedUser != null)
            {
                
                return View();
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult MedicalPackageItemsManage()
        {
            if (_loggedUser != null)
            {
                return View();
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult MedicalServiceItemsAdd()
        {
            if (_loggedUser != null)
            {

                return View();
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult MedicalServiceItemsManage()
        {
            if (_loggedUser != null)
            {

                return View();
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
