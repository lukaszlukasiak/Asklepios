﻿using Asklepios.Core.Models;
using Asklepios.Data.Interfaces;
using Asklepios.Web.Areas.AdministrativeArea.Interfaces;
using Asklepios.Web.Areas.AdministrativeArea.Models;
using Asklepios.Web.Enums;
using Asklepios.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        IWebHostEnvironment _hostEnvironment { get; set; }
        public AdministrativeController(IAdministrationModuleRepository context, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
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

        public IActionResult Index(string id)
        {
            if (int.TryParse(id, out int parsedId))
            {
                _loggedUser = _context.GetUser(parsedId);
                //_person = _context.GetPerson(_loggedUser.PersonId);
                //return View();
                return RedirectToAction("ScheduleItemsManage");

            }
            else
            {
                if (_loggedUser != null)
                {
                    return RedirectToAction("ScheduleItemsManage");
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
                    model.MedicalWorkers = model.MedicalWorkers.Where(c => c.MedicalServices.Find(d => d.Id == lid3) != null).ToList();
                }
                else if (long.TryParse(model.SelectedVisitCategoryId, out long lid4))
                {
                    List<MedicalService> categoryServices = model.VisitCategories.Where(c => c.Id == lid2).FirstOrDefault().PrimaryMedicalServices;
                    model.MedicalWorkers = model.MedicalWorkers.Where(c => c.MedicalServices.Intersect(categoryServices).Count() > 1).ToList();
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
                if (model.IsValid() && !model.IsDuplicated(_context.GetAvailableVisits()) && string.IsNullOrWhiteSpace(model.Guard))
                {
                    List<Visit> visits = CreateNewVisits(model);
                    if (visits != null)
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

                Visit visit = new Visit(location, medicalRoom, medicalWorker, visitCategory, medicalService, start, end);
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
            if (_loggedUser != null)
            {
                if (model.Visit != null)
                {
                    Visit visit1 = _context.GetAvailableVisitById(model.Visit.Id);
                    if (visit1 != null)
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
        public IActionResult MedicalWorkerItemEdit(MedicalWorkersManageViewModel model)
        {
            if (_loggedUser != null)
            {
                model.PrimaryServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService == true).ToList();

                //MedicalWorker medicalWorker = _context.GetMedicalWorkerById(model.SelectedWorkerId);
                if (model.ViewMode == ViewMode.Read)
                {
                    if (model.SelectedWorkerId > 0)
                    {
                        MedicalWorker medicalWorker = _context.GetMedicalWorkerById(model.SelectedWorkerId);
                        model.SelectedWorker = medicalWorker;
                        model.SelectedWorkerId = model.SelectedWorker.Id;
                        model.User = model.SelectedWorker.User;
                        model.Person = model.SelectedWorker.Person;
                        model.MedicalWorkertData = new MedicalWorkertData(model.SelectedWorker);
                        return View(model);
                    }
                }
                else if (model.ViewMode == ViewMode.Edit)
                {
                    if (model.SelectedWorkerId > 0)
                    {
                        MedicalWorker medicalWorker = _context.GetMedicalWorkerById(model.SelectedWorkerId);
                        model.SelectedWorker = medicalWorker;
                        model.MedicalWorkertData.UpdateWorkerWithData(model.SelectedWorker, model.PrimaryServices);
                        model.SelectedWorker.User.UpdateWith(model.User);
                        model.SelectedWorker.Person.UpdateWith(model.Person);
                        model.SelectedWorker.User.Person = model.SelectedWorker.Person;
                        if (model.Person.IsValid)
                        {
                            if (model.SelectedWorker.User.IsValid)
                            {
                                if (model.SelectedWorker.IsValid)
                                {
                                    //model.SelectedWorker.Person = model.Person;
                                    //model.SelectedWorker.User = model.User;
                                    if (model.Person.ImageFile != null)
                                    {
                                        _context.UpdatePersonImage(model.Person.ImageFile, model.SelectedWorker.Person, _hostEnvironment.WebRootPath);
                                    }
                                    _context.UpdateMedicalWorker(model.SelectedWorker, model.SelectedWorkerId);

                                }

                            }
                        }

                    }

                    return View(model);
                }

                return NotFound();

            }
            else
            {
                return NotFound();
            }

        }
        [HttpPost]
        public IActionResult MedicalWorkerItemRemove(MedicalWorkersManageViewModel model)
        {
            if (_loggedUser != null)
            {
                if (model.SelectedWorkerId > 0)
                {
                    MedicalWorker medicalWorker = _context.GetMedicalWorkerById(model.SelectedWorkerId);
                    if (medicalWorker != null)
                    {
                        _context.RemoveMedicalWorkerById(model.SelectedWorkerId);
                        model.SuccessMessage = "Pracownik medyczny został usunięty!";
                        return View(model);
                    }
                }
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult MedicalWorkerItemAdd(MedicalWorkerAddViewModel model)
        {
            if (_loggedUser != null)
            {
                model.PrimaryServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService == true).ToList();

                if (model.MedicalWorkertData.IsValid)
                {
                    if (model.Person.IsValid)
                    {
                        model.User.UserType = Core.Enums.UserType.Employee;
                        model.User.WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule;
                        model.User.Person = model.Person;

                        if (model.User.IsValid)
                        {
                            model.CreateMedicalWorker();
                            if (model.IsValid)
                            {
                                if (model.Person.ImageFile != null)
                                {
                                    _context.UpdatePersonImage(model.Person.ImageFile, model.Person, _hostEnvironment.WebRootPath);
                                }

                                _context.AddMedicalWorkerObjects(model.User, model.Person, model.MedicalWorker);

                                model.SuccessMessage = "Pracownik medyczny został dodany!";

                                return View(model);

                            }

                        }
                    }

                }
                //List<>
                //MedicalWorkerAddViewModel model = new MedicalWorkerAddViewModel();

                return View(model);
            }
            else
            {
                return NotFound();
            }

        }


        public IActionResult MedicalWorkerItemAdd()
        {
            if (_loggedUser != null)
            {
                //List<>
                MedicalWorkerAddViewModel model = new MedicalWorkerAddViewModel();
                model.PrimaryServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService == true).ToList();

                return View(model);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        public IActionResult PatientItemsAdd(PatientAddEditViewModel model)
        {
            if (_loggedUser != null)
            {
                model.User.Person = model.Person;
                model.User.UserType = Core.Enums.UserType.Patient;
                model.User.WorkerModuleType = null;
                //model.Person.EmailAddress = model.User.UserName;
                model.Patient.Person = model.Person;
                model.Patient.User = model.User;
                //model.User.  

                if (model.IsValid)
                {
                    model.MedicalPackages = _context.GetMedicalPackages();
                    model.NFZUnits = _context.GetNFZUnits();
                    if (model.Person.ImageFile != null)
                    {
                        _context.UpdatePersonImage(model.Person.ImageFile, model.Person, _hostEnvironment.WebRootPath);
                        //string imagePath = SaveImage(model.Person.ImageFile, ImageFolderType.Persons, _hostEnvironment.WebRootPath);
                        //model.Person.ImageFilePath = imagePath;
                    }

                    _context.AddPatientObjects(model.User, model.Person, model.Patient);

                    model.SuccessMessage = "Pacjent został dodany!";

                    return View(model);
                }
                else
                {
                    model.MedicalPackages = _context.GetMedicalPackages();
                    model.NFZUnits = _context.GetNFZUnits();
                    model.ErrorMessage = "Nie udało się dodać pacjenta!";

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
                PatientAddEditViewModel model = new PatientAddEditViewModel();
                model.MedicalPackages = _context.GetMedicalPackages();
                model.NFZUnits = _context.GetNFZUnits();

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult PatientItemEdit(PatientDetailsViewModel model)
        {
            if (_loggedUser != null)
            {
                if (model.CurrentPatientId > 0)
                {
                    Patient patient = _context.GetPatientById(model.CurrentPatientId);
                    if (model.ViewMode == ViewMode.Read)
                    {
                        model.CurrentPatient = patient;
                        model.NFZUnits = _context.GetNFZUnits();
                        model.MedicalPackages = _context.GetMedicalPackages();

                    }
                    else if (model.ViewMode == ViewMode.Edit)
                    {
                        UpdatePatientData(model, patient);

                    }
                    else if (model.ViewMode == ViewMode.Remove)
                    {
                    }

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

        private void UpdatePatientData(PatientDetailsViewModel model, Patient patient)
        {
            if (string.IsNullOrWhiteSpace(model.CurrentPatient.User.Password))
            {

                //model.CurrentPatient.User.Password = patient.User.Password;
            }
            else
            {
                patient.User.Password = model.CurrentPatient.User.Password;
            }
            if (string.IsNullOrWhiteSpace(model.CurrentPatient.User.EmailAddress))
            {
            }
            else
            {
                patient.User.Password = model.CurrentPatient.User.EmailAddress;
            }
            model.CurrentPatient.User = patient.User;
            model.CurrentPatient.User.Person = model.CurrentPatient.Person;

            if (model.CurrentPatient.Person.ImageFile != null)
            {
                _context.UpdatePersonImage(model.CurrentPatient.Person.ImageFile, model.CurrentPatient.Person, _hostEnvironment.WebRootPath);

                //string imagePath = SaveImage(model.CurrentPatient.Person.ImageFile, ImageFolderType.Persons, _hostEnvironment.WebRootPath);
                //model.CurrentPatient.Person.ImageFilePath = imagePath;
            }

            if (model.IsValid)
            {
                _context.UpdatePatient(model.CurrentPatient);

                model.SuccessMessage = "Dane pacjenta zostały zaktualizowane!";
            }
            else
            {
                model.ErrorMessage = "Nie udało się zaktualizować danych pacjenta!";

            }
            //model.CurrentPatient = patient;
            model.NFZUnits = _context.GetNFZUnits();
            model.MedicalPackages = _context.GetMedicalPackages();
        }

        public IActionResult PatientItemEdit(string id)
        {
            if (_loggedUser != null)
            {
                if (long.TryParse(id, out long lid))
                {
                    Patient patient = _context.GetPatientById(lid);
                    return View(patient);
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
        [HttpPost]
        public IActionResult PatientItemRemove(PatientDetailsViewModel model)
        {
            if (_loggedUser != null)
            {
                if (model.CurrentPatientId > 0)
                {
                    Patient patient = _context.GetPatientById(model.CurrentPatientId);
                    if (patient != null)
                    {
                        _context.RemovePatientById(model.CurrentPatientId);
                        model.SuccessMessage = "Pacjent został pomyślnie usunięty!";
                        return View(model);
                    }
                    model.SuccessMessage = "Podczas próby usunięcia pacjenta nastąpił nieoczekiwany błąd!";
                }
                //return RedirectToAction("VisitDetails", "Patient", new { area = "PatientArea", id = visit.Id });

                return NotFound();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult PatientItemsManage(PatientsManageViewModel model)//(PatientsManageViewModel model)
        {

            if (_loggedUser != null)
            {
                //PatientsManageViewModel model = new PatientsManageViewModel();
                //model.
                //IPatientSearch newSearch = model;
                //newSearch.SetSearchOptions(searchPatient);
                //model.sets= searchPatient.;

                model.NFZUnits = _context.GetNFZUnits();
                model.MedicalPackages = _context.GetMedicalPackages();
                model.AllPatients = _context.GetAllPatients();
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
                PatientsManageViewModel model = new PatientsManageViewModel();
                model.NFZUnits = _context.GetNFZUnits();
                model.MedicalPackages = _context.GetMedicalPackages();
                model.AllPatients = _context.GetAllPatients();
                return View(model);
            }
            else
            {
                return NotFound();
            }
        }

        //public IActionResult MedicalWorkerItemsAdd()
        //{
        //    if (_loggedUser != null)
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}
        [HttpPost]
        public IActionResult MedicalWorkerItemsManage(MedicalWorkersManageViewModel model)
        {
            if (_loggedUser != null)
            {
                model.AllMedicalWorkers = _context.GetMedicalWorkers();
                model.PrimaryServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService == true).ToList();
                return View(model);
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
                MedicalWorkersManageViewModel model = new MedicalWorkersManageViewModel();
                model.AllMedicalWorkers = _context.GetMedicalWorkers();
                model.PrimaryServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService == true).ToList();
                return View(model);
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult Temp(long id)
        {
            if (_loggedUser != null)
            {
                LocationsManageViewModel model = new LocationsManageViewModel();
                model.SelectedLocationId = id;
                return View(model);
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult LocationItemEdit(long id)
        {
            if (_loggedUser != null)
            {
                LocationsManageViewModel model = new LocationsManageViewModel();
                Location loc = _context.GetLocationById(model.SelectedLocationId);
                model.SelectedLocation = loc;// _context.GetLocationById(model.SelectedLocationId);
                model.UnasignedRooms = _context.GetUnasignedRooms();
                model.PrimaryServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService == true).ToList();

                return View(model);

            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [ResponseCache(CacheProfileName = "NoCaching")]
        public IActionResult LocationItemEdit(LocationsManageViewModel model)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (_loggedUser != null)
            {
                model.PrimaryServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService == true).ToList();
                //model.SelectedLocation = _context.GetLocationById(model.SelectedLocationId);
                Location loc = _context.GetLocationById(model.SelectedLocationId);

                if (model.ViewMode == ViewMode.Read)
                {
                    if (model.SelectedLocationId > 0)
                    {

                        model.SelectedLocation = loc;// _context.GetLocationById(model.SelectedLocationId);
                        model.UnasignedRooms = _context.GetUnasignedRooms();
                        return View(model);
                    }
                }
                else if (model.ViewMode == ViewMode.Edit)
                {
                    model.SelectedLocation.MedicalRooms = loc.MedicalRooms;
                    model.SelectedLocation.Id = loc.Id;
                    model.UnasignedRooms = _context.GetUnasignedRooms();

                    model.UpdateSelectionOfRooms();
                    model.UpdateSelectionOfServices();

                    if (model.SelectedLocation.IsValid)
                    {
                        if (model.SelectedLocation.ImageFile != null)
                        {
                            _context.UpdateLocationImage(model.SelectedLocation.ImageFile, model.SelectedLocation, _hostEnvironment.WebRootPath);
                            //_context.UpdatePersonImage()
                        }
                        else
                        {
                            model.SelectedLocation.ImageFile = loc.ImageFile;
                            model.SelectedLocation.ImagePath = loc.ImagePath;
                        }
                        //_context.AddLocation(model.SelectedLocation);
                        _context.UpdateLocation(model.SelectedLocation, model.SelectedLocationId);
                        model.UnasignedRooms = _context.GetUnasignedRooms();
                        //model.SuccessMessage = "Dane placówki zostały zaktualizowane";
                        TempData["successMessage"]= "Dane placówki zostały zaktualizowane";
                        return RedirectToAction("Temp", new { id = model.SelectedLocationId });
                        //return View(model);

                    }
                    else
                    {
                        return View(model);
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
        public IActionResult LocationItemRemove(LocationsManageViewModel model)
        {
            if (_loggedUser != null)
            {
                if (model.SelectedLocationId > 0)
                {

                    Location location = _context.GetLocationById(model.SelectedLocationId);
                    model.SuccessMessage = "Placówka została pomyślnie usunięta!";
                    TempData["successMessage"]= "Placówka została pomyślnie usunięta!";
                    _context.RemoveLocationById(model.SelectedLocationId);
                    //return RedirectToAction("LocationItemsManage", model);
                    return View();
                }

                return NotFound();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult LocationItemAdd(LocationsManageViewModel model)
        {
            if (_loggedUser != null)
            {

                model.GetRooms(_context, model.SelectedLocation);
                model.GetServices(_context, model.SelectedLocation);

                if (model.SelectedLocation.IsValid)
                {

                    if (model.SelectedLocation.ImageFile != null)
                    {
                        _context.UpdateLocationImage(model.SelectedLocation.ImageFile, model.SelectedLocation, _hostEnvironment.WebRootPath);
                    }
                    //_context.UpdateLocation(model.SelectedLocation, model.SelectedLocationId);
                    //model.SuccessMessage = "Placówka została dodana";
                    TempData["successMessage"] = "Placówka została dodana";
                    _context.AddLocation(model.SelectedLocation);
                    //model.UnasignedRooms = _context.GetUnasignedRooms();
                    //model.PrimaryServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService == true).ToList();
                    return RedirectToAction("LocationItemAdd");
                    //return View(model);

                }
                model.UnasignedRooms = _context.GetUnasignedRooms();
                model.PrimaryServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService == true).ToList();

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult LocationItemAdd()
        {
            if (_loggedUser != null)
            {
                LocationsManageViewModel model = new LocationsManageViewModel();
                model.SelectedLocation = new Location();
                model.UnasignedRooms = _context.GetUnasignedRooms();
                model.PrimaryServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService == true).ToList();
                return View(model);
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
                LocationsManageViewModel model = new LocationsManageViewModel();
                model.AllLocations = _context.GetAllLocations();
                return View(model);
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
        //public IActionResult NonMedicalWorkerItemsAdd()
        //{
        //    if (_loggedUser != null)
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}
        //[HttpPost]
        //public IActionResult NonMedicalWorkerItemsAdd(MedicalWorkersManageViewModel model )
        //{
        //    if (_loggedUser != null)
        //    {
        //        if (model.ViewMode==ViewMode.Read)
        //        {

        //        }
        //        else if(model.ViewMode==ViewMode.Edit)
        //        {

        //        }
        //        return View();
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}
        //[HttpPost]
        //public IActionResult NonMedicalWorkerItemsRemove(MedicalWorkersManageViewModel model)
        //{
        //    if (_loggedUser != null)
        //    {
        //        if (model.ViewMode == ViewMode.Remove)
        //        {
        //            MedicalWorker worker = _context.GetMedicalWorkerById(model.SelectedWorkerId);
        //            if (worker!=null)
        //            {
        //                _context.RemoveMedicalWorkerById(model.SelectedWorkerId);
        //            }
        //            else
        //            {
        //                return NotFound();
        //            }
        //        }
        //        return View();
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}

        //public IActionResult NonMedicalWorkerItemsManage()
        //{
        //    if (_loggedUser != null)
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}
    }
}