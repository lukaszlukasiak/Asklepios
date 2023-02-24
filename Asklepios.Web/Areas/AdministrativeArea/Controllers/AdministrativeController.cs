using Asklepios.Core.Models;
using Asklepios.Data.Interfaces;
using Asklepios.Web.Areas.AdministrativeArea.Interfaces;
using Asklepios.Web.Areas.AdministrativeArea.Models;
using Asklepios.Web.Enums;
using Asklepios.Web.Models;
using Asklepios.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Asklepios.Core.Enums;
using Asklepios.Core.Extensions;

namespace Asklepios.Web.Areas.AdministrativeArea.Controllers
{
    [Area("AdministrativeArea")]
    [Authorize(Roles = "AdministrativeWorker")]
    public class AdministrativeController : Controller
    {
        //private readonly ILogger<AdministrativeWorkerController> _logger;
        IAdministrationModuleRepository _context;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole<long>> _roleManager;
        //private PasswordHasher<User> _passwordHasher;
        //public AdministrativeWorkerController(ILogger<AdministrativeWorkerController> logger)
        //{
        //    _logger = logger;
        //}
        SignInManager<User> _signManager { get; set; }
        IWebHostEnvironment _hostEnvironment { get; set; }

        long UserId { get; set; }
        //public const string ERROR_MESSAGE = "errorMessage";
        //public const string SUCCESS_MESSAGE = "successMessage";
        public const string MESSAGE = "MESSAGE";
        private User _loggedUser { get; set; }


        public AdministrativeController(IAdministrationModuleRepository context, 
                                                IWebHostEnvironment hostEnvironment, 
                                                    SignInManager<User> signManager, 
                                                        UserManager<User> userManager, 
                                                            RoleManager<IdentityRole<long>> roleManager
                                                                )//PasswordHasher<User> passwordHasher
        {
            _hostEnvironment = hostEnvironment;
            _signManager= signManager;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            //_passwordHasher = passwordHasher;
            //UserId = HttpContext.User.GetUserId().Value;
            //_loggedUser=_context.GetUserById(UserId);
        }
        //private static Person _person { get; set; }
        //private static Patient _selectedPatient { get; set; }

        public async Task<IActionResult> LogOutAsync()
        {
            //_loggedUser = null;
            //_person = null;
            //_selectedPatient = null;

            await _signManager.SignOutAsync();
            return RedirectToAction("Index","Home");

        }

        public IActionResult Index()
        {
            //_signManager.UserManager.
            //if (int.TryParse(id, out int parsedId))
            //{
                //_loggedUser = _context.GetUserById(parsedId);
                //_person = _context.GetPerson(_loggedUser.PersonId);
                //return View();
                return RedirectToAction("ScheduleItemsManage");

            //}
            //else
            //{
            //    if (_loggedUser != null)
            //    {
            //        return RedirectToAction("ScheduleItemsManage");
            //    }
            //    else
            //    {
            //        return NotFound();
            //    }
            //}
        }

        [HttpGet]
        public IActionResult Contact()
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                ContactMessageViewModel model = new ContactMessageViewModel(_loggedUser);
                model.UserName = _loggedUser.Person.FullName;
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Contact(ContactMessageViewModel model)
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

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
                    model.AlertMessageType = Enums.AlertMessageType.InfoMessage;
                }
                else
                {
                    model.AlertMessage = "Wystąpił błąd podczas próby wysłania wiadomości! Spróbuj jeszcze raz!";
                    model.AlertMessageType = Enums.AlertMessageType.ErrorMessage;
                    ViewBag.Message = "Wystąpił błąd podczas próby wysłania wiadomości! Spróbuj jeszcze raz!";
                }
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            return NotFound();
        }
        [HttpGet]
        public IActionResult ScheduleItemsManage()
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

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
                model.CurrentPageNum = 1;
                //List<Visit> visits = _context.GetFutureVisitsChunk(model.CurrentPageNumId, 100);//_context.GetAvailableVisits();
                //model.Schedule = visits;
                model.FilterSchedule(_context.GetFutureVisitsQuery());

                model.Locations = _context.GetAllLocations();
                //model.MedicalRooms=_context
                model.MedicalWorkers = _context.GetMedicalWorkers();
                model.PrimaryMedicalServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService == true).ToList();
                model.VisitCategories = _context.GetVisitCategories();
                model.UserName = _loggedUser.Person.FullName;

                CreatePageSelectList(model);
                return View(model);
            }
            return NotFound();
        }

        private void CreatePageSelectList(ScheduleManageViewModel model)
        {
            List<PageSelect> items = new List<PageSelect>();
            for (int i = 1; i <= model.NumberOfPages; i++)
            {
                PageSelect page = new PageSelect();
                page.Value = i.ToString();
                page.Id = i;
                //SelectListItem item = new SelectListItem(i.ToString(), i.ToString());
                items.Add(page);
            }
            SelectList pagesList = new SelectList(items, "Id", "Value", model.CurrentPageNum);
            ViewData["PagesList"] = pagesList;
        }

        [HttpPost]
        public IActionResult ScheduleItemsManage(ScheduleManageViewModel model)
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                //List<Visit> visits = _context.GetFutureVisitsChunk(model.CurrentPageNumId,model.ItemsPerPage);
                IQueryable<Visit> queryVisits = _context.GetFutureVisitsQuery();
                //do stworzenia
                if (model.CurrentPageNum == 0)
                {
                    model.CurrentPageNum = 1;
                }
                model.FilterSchedule(queryVisits);

                //model.Schedule = visits;
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
                    model.PrimaryMedicalServices = category.MedicalServices.ToList();
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
                    List<MedicalService> categoryServices = model.VisitCategories.Where(c => c.Id == lid2).FirstOrDefault().MedicalServices;
                    model.MedicalWorkers = model.MedicalWorkers.Where(c => c.MedicalServices.Intersect(categoryServices).Count() > 1).ToList();
                }
                model.UserName = _loggedUser.Person.FullName;

                //List<PageSelect> items = new List<PageSelect>();
                //for (int i = 1; i <= model.NumberOfPages; i++)
                //{
                //    PageSelect page = new PageSelect();
                //    page.Value = i.ToString();
                //    page.Id = i;
                //    //SelectListItem item = new SelectListItem(i.ToString(), i.ToString());
                //    items.Add(page);
                //}
                List<long> items = new List<long>();
                for (int i = 1; i <= model.NumberOfPages; i++)
                {
                    items.Add(i);
                }

                SelectList pagesList = new SelectList(items, model.CurrentPageNum - 1);
                ViewData["PagesList"] = pagesList;

                return View(model);
            }
            return NotFound();
        }


        [HttpGet]
        public IActionResult ScheduleItemsAdd()
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                IQueryable<Visit> visits = _context.GetAvailableVisitsQuery();
                ScheduleItemsAddViewModel model = new ScheduleItemsAddViewModel();
                //model.Schedule = visits;
                model.Locations = _context.GetAllLocations();
                //model.MedicalRooms=_context
                model.MedicalWorkers = _context.GetMedicalWorkers();
                model.PrimaryMedicalServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService == true).ToList();
                model.VisitCategories = _context.GetVisitCategories();
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult ScheduleItemsAdd(ScheduleItemsAddViewModel model)
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                if (string.IsNullOrWhiteSpace(model.Guard))
                {
                    if (model.IsValid())
                    {
                        if (!model.IsDuplicated(_context.GetFutureVisitsQuery()))
                        {
                            List<Visit> visits = CreateNewVisits(model);
                            if (visits != null)
                            {
                                _context.AddVisitsToSchedule(visits);

                                model.Message = "Wizyty zostały dodane";
                                model.AlertMessageType=AlertMessageType.SuccessMessage;

                                model.VisitCategories = _context.GetVisitCategories();
                                model.PrimaryMedicalServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService == true).ToList();
                                model.MedicalWorkers = _context.GetMedicalWorkers();
                                model.Locations = _context.GetAllLocations();

                                ScheduleItemsAddUpdateLists(model);
                                model.UserName = _loggedUser.Person.FullName;

                                return View(model);
                            }
                            else
                            {
                                return NotFound();
                            }

                        }
                        else
                        {
                            model.Message = "Dodawane wizyty duplikują się z już istniejącymi dla wybranego pracownika medycznego";
                            model.AlertMessageType = AlertMessageType.ErrorMessage;
                        }
                    }
                    else
                    {
                        model.Message = "Nie wszystkie dane zostały poprawnie uzupełnione";
                        model.AlertMessageType = AlertMessageType.ErrorMessage;
                    }

                }
                model.VisitCategories = _context.GetVisitCategories();
                model.PrimaryMedicalServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService == true).ToList();
                model.MedicalWorkers = _context.GetMedicalWorkers();
                model.Locations = _context.GetAllLocations();
                model.Guard = null;
                ScheduleItemsAddUpdateLists(model);
                model.UserName = _loggedUser.Person.FullName;
                ModelState.Clear();

                return View(model);

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
            model.VisitsDate = model.VisitsDate.Add(model.FirstVisitTime);

            for (int i = 0; i < model.NumberOfVisitsToAdd; i++)
            {

                DateTime start = model.VisitsDate.Add(new TimeSpan(0, model.DurationOfVisit * i, 0)); //new DateTimeOffset(model.VisitsDate.DateTime, new TimeSpan(0, model.DurationOfVisit * i, 0)); //new DateTimeOffset(model.VisitsDate.DateTime, model.FirstVisitTime.Add(new TimeSpan(0, model.DurationOfVisit * i, 0)));
                DateTime end = model.VisitsDate.Add(new TimeSpan(0, model.DurationOfVisit * (i+1), 0));//new DateTimeOffset(model.VisitsDate.DateTime, new TimeSpan(0, model.DurationOfVisit * (i+1), 0));  //new DateTimeOffset(model.VisitsDate.DateTime, model.FirstVisitTime.Add(new TimeSpan(0, model.DurationOfVisit * (i + 1), 0)));

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
                        List<MedicalService> services = visitCategory.MedicalServices;
                        List<MedicalWorker> workers = model.MedicalWorkers.Where(c => c.MedicalServices.Intersect(visitCategory.MedicalServices).Count() > 0).ToList();
                        model.MedicalWorkers = workers;
                        model.PrimaryMedicalServices = services.Where(c=>c.IsPrimaryService).ToList();
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
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                if (model.Visit != null)
                {
                    Visit visit1 = _context.FutureVisitById(model.Visit.Id);
                    if (visit1 != null)
                    {
                        ScheduleManageViewModel model2 = new ScheduleManageViewModel();
                        (model2 as ISearchVisit).SetSearchOptions(model);

                        _context.RemoveVisitById(visit1.Id);
                        model2.Message = "Wizyta została usunięta!";
                        model2.AlertMessageType = AlertMessageType.InfoMessage;
                        ISearchVisit searchOptions = model;
                        model.UserName = _loggedUser.Person.FullName;

                        return View(model2);
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
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                MedicalWorker worker = _context.GetMedicalWorkerDetailsById(long.Parse(id));
                worker.VisitReviews.ForEach(c => c.Reviewer = _context.GetPatientById(c.ReviewerId));
                MedicalWorkerViewModel model = new MedicalWorkerViewModel(worker);
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> MedicalWorkerItemEdit(MedicalWorkersManageViewModel model)
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                model.PrimaryServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService == true).ToList();

                if (model.ViewMode == ViewMode.Read)
                {
                    if (model.SelectedWorkerId > 0)
                    {
                        MedicalWorker medicalWorker = _context.GetMedicalWorkerDetailsById(model.SelectedWorkerId);
                        model.SelectedWorker = medicalWorker;
                        model.SelectedWorkerId = model.SelectedWorker.Id;
                        model.User = model.SelectedWorker.User;
                        model.Person = model.SelectedWorker.Person;
                        model.MedicalWorkertData = new MedicalWorkertData(model.SelectedWorker);
                        model.UserName = _loggedUser.Person.FullName;

                        return View(model);
                    }
                }
                else if (model.ViewMode == ViewMode.Edit)
                {
                    if (model.SelectedWorkerId > 0)
                    {
                        MedicalWorker medicalWorker = _context.GetMedicalWorkerDetailsById(model.SelectedWorkerId);
                        model.SelectedWorker = medicalWorker;
                        model.MedicalWorkertData.UpdateWorkerWithData(model.SelectedWorker, model.PrimaryServices);
                        model.SelectedWorker.User.UpdateWith(model.User);
                        model.SelectedWorker.Person.UpdateWith(model.Person);
                        model.SelectedWorker.User.Person = model.SelectedWorker.Person;
                        model.SelectedWorker.Id = model.SelectedWorkerId;
                        if (string.IsNullOrWhiteSpace(model.User.PasswordHash))
                        {
                            if (ModelState.ContainsKey("User.PasswordHash"))
                            {
                                ModelState.Remove("User.PasswordHash");
                            }
                        }
                        else
                        {
                            var passwordValidator = new PasswordValidator<User>();
                            IdentityResult result = await passwordValidator.ValidateAsync(_userManager, model.User, model.User.PasswordHash);

                            if (result.Succeeded)
                            {
                                PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

                                model.SelectedWorker.User.PasswordHash = passwordHasher.HashPassword(model.User, model.User.PasswordHash);

                            }
                        }
                        if (model.Person.IsValid)
                        {
                            if (model.SelectedWorker.User.IsValid)
                            {
                                if (model.SelectedWorker.IsValid)
                                {
                                    //model.SelectedWorker.Person = model.Person;
                                    //model.SelectedWorker.User = model.User;
                                    //if (model.Person.ImageFile != null)
                                    //{
                                    //    _context.UpdatePersonImage(model.Person.ImageFile, model.SelectedWorker.Person, _hostEnvironment.WebRootPath);
                                    //}
                                    _context.UpdateMedicalWorker(model.SelectedWorker, _hostEnvironment.WebRootPath);
                                    //TempData[SUCCESS_MESSAGE] = "Dane pracownika medycznego zostały zaktualizowane!";
                                    model.Message = "Dane pracownika medycznego zostały zaktualizowane!";
                                    model.AlertMessageType = AlertMessageType.InfoMessage;

                                }
                            }
                        }
                        else
                        {
                            //TempData[ERROR_MESSAGE] = "Dane pracownika medycznego nie zostały zaktualizowane. Uzupełnij wszystko poprawnie!";
                            model.Message = "Dane pracownika medycznego nie zostały zaktualizowane. Uzupełnij wszystko poprawnie!";
                            model.AlertMessageType = AlertMessageType.InfoMessage;

                        }
                    }
                    model.UserName = _loggedUser.Person.FullName;

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
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                if (model.SelectedWorkerId > 0)
                {
                    MedicalWorker medicalWorker = _context.GetMedicalWorkerById(model.SelectedWorkerId);
                    if (medicalWorker != null)
                    {

                        if (_context.HasMedicalWorkerVisits(medicalWorker.Id))
                        {
                            TempData[MESSAGE] = new ViewMessage()
                            {
                                Message = "Nie można usunąć pracownika, do którego są przypisane jakiekolwiek wizyty!",
                                MessageType = AlertMessageType.ErrorMessage
                            };
                            //model.ErrorMessage = "Nie można usunąć pracownika, do którego są przypisane jakiekolwiek wizyty!";
                            model.UserName = _loggedUser.Person.FullName;
                        }
                        else
                        {
                            long personId = model.Person.Id;
                            long userId = model.User.Id;
                            _context.RemoveMedicalWorkerById(model.SelectedWorkerId);
                            _context.RemovePersonById(personId);
                            _context.RemoveUserById(userId);

                            TempData[MESSAGE]  = new ViewMessage()
                            {
                                Message = "Pracownik medyczny został usunięty!",
                                MessageType = AlertMessageType.InfoMessage
                            };
                            //TempData[MESSAGE] = viewMessage;//"Pracownik medyczny został usunięty!";
                            //model.SuccessMessage = "Pracownik medyczny został usunięty!";
                            model.UserName = _loggedUser.Person.FullName;

                        }
                        return RedirectToAction("MedicalWorkerItemsManage");

                        //return View(model);
                    }
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> MedicalWorkerItemAdd(MedicalWorkerAddViewModel model)
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                model.UserName = _loggedUser.Person.FullName;

                model.PrimaryServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService == true).ToList();

                if (model.MedicalWorkertData.IsValid)
                {
                    if (model.Person.IsValid)
                    {
                        model.User.UserType = Core.Enums.UserType.Employee;
                        model.User.WorkerModuleType = Core.Enums.WorkerModuleType.MedicalWorkerModule;
                        //model.User.Person = model.Person;

                        if (model.User.IsValid)
                        {
                            model.CreateMedicalWorker();
                            model.MedicalWorker.Person = model.Person;
                            User userInDb = null;
                            IdentityResult result = null;

                            if (!string.IsNullOrWhiteSpace(model.User.PasswordHash))
                            {
                                var passwordValidator = new PasswordValidator<User>();
                                result = await passwordValidator.ValidateAsync(_userManager, null, model.User.PasswordHash);

                                userInDb = await _userManager.FindByNameAsync(model.User.UserName);
                            }


                            if (model.IsValid && userInDb == null && result.Succeeded)
                            {
                                _context.AddPerson(model.Person, _hostEnvironment.WebRootPath);

                                model.User.Person = null;
                                model.User.PersonId = model.Person.Id;

                                //IdentityResult identityResult = await _userManager.CreateAsync(model.User, model.User.PasswordHash);
                                bool isSuccessfull = await _context.AddIdenitytUserWithRole(model.User, _userManager, _roleManager);

                                model.MedicalWorker.UserId = model.MedicalWorker.User.Id;
                                model.MedicalWorker.PersonId = model.MedicalWorker.Person.Id;
                                model.MedicalWorker.Person = null;
                                model.User = null;
                                model.MedicalWorker.User = null;

                                _context.AddMedicalWorkerObjects(model.MedicalWorker, _hostEnvironment.WebRootPath);

                                model.Message = "Pracownik medyczny został dodany!";
                                model.AlertMessageType = AlertMessageType.InfoMessage;
                                model.UserName = _loggedUser.Person.FullName;

                                return View(model);
                            }

                            //if (model.IsValid)
                            //{
                            //    //if (model.Person.ImageFile != null)
                            //    //{
                            //    //    _context.UpdatePersonImage(model.Person.ImageFile, model.Person, _hostEnvironment.WebRootPath);
                            //    //}
                            //    var passwordValidator = new PasswordValidator<User>();

                            //    var result = await passwordValidator.ValidateAsync(_userManager, null, model.User.PasswordHash);
                            //    User userSearch= await _userManager.FindByNameAsync(model.User.UserName);


                            //    if (userSearch != null)
                            //    {
                            //        model.Message = "Już istnieje użytkownikiem z taką nazwą! Zmień ją!";
                            //        model.AlertMessageType = AlertMessageType.WarningMessage;
                            //        return View(model);
                            //    }
                            //    if (result.Succeeded==false)
                            //    {
                            //        model.Message = "Hasło nie spełnia wymogów. Popraw je!";
                            //        model.AlertMessageType = AlertMessageType.WarningMessage;
                            //        return View(model);

                            //    }


                            //    _context.AddMedicalWorkerObjects( model.MedicalWorker, _hostEnvironment.WebRootPath);
                            //    model.Message= "Pracownik medyczny został dodany!";
                            //    model.AlertMessageType = AlertMessageType.InfoMessage;
                            //    //TempData[SUCCESS_MESSAGE]= "Pracownik medyczny został dodany!";
                            //    //model.SuccessMessage = "Pracownik medyczny został dodany!";

                            //    return View(model);

                            //}
                            else
                            {
                                if (string.IsNullOrWhiteSpace(model.User.PasswordHash))
                                {
                                    model.Message = "Uzupełnij hasło";
                                }
                                else if (!result.Succeeded)
                                {
                                    model.Message = "Hasło nie spełnia wszystkich wymagań! Popraw je!";
                                }
                                if (userInDb != null)
                                {
                                    model.Message = "Już istnieje użytkownik z podaną nazwą!";
                                }
                                model.UserName = _loggedUser.Person.FullName;
                                model.AlertMessageType = AlertMessageType.WarningMessage;
                                if (string.IsNullOrWhiteSpace( model.Message))
                                {
                                    model.Message = "Pracownik medyczny nie został dodany. Wypełnij poprawnie wszystkie dane!";
                                }
                                return View(model);

                                //TempData[ERROR_MESSAGE] = "Pracownik medyczny nie został dodany. Wypełnij poprawnie wszystko!";

                            }

                        }
                    }

                }

                return View(model);
            }
            else
            {
                return NotFound();
            }

        }


        public IActionResult MedicalWorkerItemAdd()
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                //List<>
                MedicalWorkerAddViewModel model = new MedicalWorkerAddViewModel();
                model.PrimaryServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService == true).ToList();
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        public async Task<IActionResult> PatientItemsAdd(PatientAddEditViewModel model)
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                model.User.Person = model.Person;
                model.User.UserType = Core.Enums.UserType.Patient;
                model.User.WorkerModuleType = null;
                //model.Person.EmailAddress = model.User.UserName;
                model.Patient.Person = model.Person;
                model.Patient.User = model.User;

                User userInDb = null;
                IdentityResult result = null;

                if (!string.IsNullOrWhiteSpace( model.User.PasswordHash))
                {
                    var passwordValidator = new PasswordValidator<User>();
                    result = await passwordValidator.ValidateAsync(_userManager, null, model.User.PasswordHash);

                    userInDb = await _userManager.FindByNameAsync(model.User.UserName);
                }


                if (model.IsValid && userInDb==null && result.Succeeded)
                {
                    model.MedicalPackages = _context.GetMedicalPackages();
                    model.NFZUnits = _context.GetNFZUnits();

                    _context.AddPerson(model.Person, _hostEnvironment.WebRootPath);

                    model.User.Person = null;
                    model.User.PersonId=model.Person.Id;

                    bool isSuccessfull= await _context.AddIdenitytUserWithRole(model.User, _userManager, _roleManager);
                    //IdentityResult identityResult= await _userManager.CreateAsync(model.User, model.User.PasswordHash);
                    //var role = await _roleManager.FindByNameAsync(IdentityRoleTypes.Patient.GetDescription());
                    model.Patient.UserId = model.Patient.User.Id;
                    model.Patient.PersonId = model.Patient.Person.Id;
                    model.Patient.Person = null;
                    model.User = null;
                    model.Patient.User = null;

                    _context.AddPatientObjects(  model.Patient, _hostEnvironment.WebRootPath);

                    model.Message = "Pacjent został dodany!";
                    model.AlertMessageType = AlertMessageType.InfoMessage;
                    model.UserName = _loggedUser.Person.FullName;

                    return View(model);
                }
                else
                {
                    if (string.IsNullOrWhiteSpace( model.User.PasswordHash))
                    {
                        model.Message= "Uzupełnij hasło";
                    }
                    else if (!result.Succeeded)
                    {
                        model.Message = "Hasło nie spełnia wszystkich wymagań! Popraw je!";
                    }
                    if (userInDb!=null)
                    {
                        model.Message = "Już istnieje użytkownik z podaną nazwą!";
                    }
                    model.MedicalPackages = _context.GetMedicalPackages();
                    model.NFZUnits = _context.GetNFZUnits();
                    //model.ErrorMessage = "Nie udało się dodać pacjenta!";
                    model.UserName = _loggedUser.Person.FullName;
                    if (string.IsNullOrWhiteSpace(model.Message))
                    {
                        model.Message = "Pacent nie został dodany. Wypełnij poprawnie wszystkie dane!";
                    }
                    model.AlertMessageType = AlertMessageType.WarningMessage;
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
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                PatientAddEditViewModel model = new PatientAddEditViewModel();
                model.MedicalPackages = _context.GetMedicalPackages();
                model.NFZUnits = _context.GetNFZUnits();
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public IActionResult PatientItemEdit(string id)
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                if (long.TryParse(id, out long lid))
                {
                    Patient patient = _context.GetPatientById(lid);
                    PatientDetailsViewModel model = new PatientDetailsViewModel();
                    model.CurrentPatient = patient;
                    model.CurrentPatientId = patient.Id;
                    model.MedicalPackages = _context.GetMedicalPackages();
                    model.NFZUnits = _context.GetNFZUnits();
                    model.UserName = _loggedUser.Person.FullName;

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

        [HttpPost]
        public IActionResult PatientItemEdit(PatientDetailsViewModel model)
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

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
                        if (string.IsNullOrWhiteSpace(model.CurrentPatient.User.PasswordHash))
                        {
                            if (ModelState.ContainsKey("CurrentPatient.User.PasswordHash"))
                            {
                                ModelState.Remove("CurrentPatient.User.PasswordHash");
                            }
                        }
                        UpdatePatientData(model, patient).Wait();

                        model.CurrentPatient = _context.GetPatientById(patient.Id);
                    }
                    else if (model.ViewMode == ViewMode.Remove)
                    {
                    }
                    model.UserName = _loggedUser.Person.FullName;

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

        private async Task<bool> UpdatePatientData(PatientDetailsViewModel model, Patient patient)
        {
            if (string.IsNullOrWhiteSpace(model.CurrentPatient.User.PasswordHash))
            {

                //model.CurrentPatient.User.Password = patient.User.Password;
            }
            else
            {
                var passwordValidator = new PasswordValidator<User>();
                IdentityResult result = await passwordValidator.ValidateAsync(_userManager, null, model.CurrentPatient.User.PasswordHash);
                if (result.Succeeded)
                {
                    PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
                    patient.User.PasswordHash = passwordHasher.HashPassword(patient.User, model.CurrentPatient.User.PasswordHash);
                }
                else
                {
                    model.AlertMessageType = AlertMessageType.ErrorMessage;
                    model.Message = "Nie udało się zaktualizować danych pacjenta! Hasło nie spełnia wymogów!";
                    return false;
                }
                //User user = userManager.Users.FirstOrDefault(x => x.Id == patient.UserId);
                
                //Task<IdentityResult> result= await userManager.ChangePasswordAsync(user, model.CurrentPatient.User.PasswordHash);
                //patient.User.PasswordHash = model.CurrentPatient.User.PasswordHash;
            }
            if (string.IsNullOrWhiteSpace(model.CurrentPatient.User.Email))
            {
            }
            else
            {
                patient.User.Email = model.CurrentPatient.User.Email;
            }
            model.CurrentPatient.User = patient.User;

            //if (model.CurrentPatient.Person.ImageFile != null)
            //{
            //    _context.UpdatePersonImage(model.CurrentPatient.Person.ImageFile, model.CurrentPatient.Person, _hostEnvironment.WebRootPath);

            //    //string imagePath = SaveImage(model.CurrentPatient.Person.ImageFile, ImageFolderType.Persons, _hostEnvironment.WebRootPath);
            //    //model.CurrentPatient.Person.ImageFilePath = imagePath;
            //}
            //else
            //{
            //    model.CurrentPatient.Person.ImageFilePath = patient.Person.ImageFilePath;
            //}
            //model.CurrentPatient.User.Person = model.CurrentPatient.Person;

            if (model.IsValid)
            {
                model.CurrentPatient.PersonId = patient.PersonId;
                model.CurrentPatient.Person.Id = model.CurrentPatient.PersonId.Value;

                model.CurrentPatient.Person.ImageFilePath = patient.Person.ImageFilePath;
                _context.UpdatePatient(model.CurrentPatient, _hostEnvironment.WebRootPath);

                model.Message = "Dane pacjenta zostały zaktualizowane!";
                model.AlertMessageType = AlertMessageType.SuccessMessage;
            }
            else
            {
                model.Message = "Nie udało się zaktualizować danych pacjenta!";
                model.AlertMessageType = AlertMessageType.ErrorMessage;
            }
            //model.CurrentPatient = patient;
            model.NFZUnits = _context.GetNFZUnits();
            model.MedicalPackages = _context.GetMedicalPackages();

            return true;
        }
        public IActionResult PatientItemDetails(string id)
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                if (long.TryParse(id, out long lid))
                {
                    Patient patient = _context.GetPatientById(lid);
                    PatientDetailsViewModel model = new PatientDetailsViewModel();
                    model.CurrentPatient = patient;
                    model.CurrentPatientId = patient.Id;
                    model.UserName = _loggedUser.Person.FullName;

                    //model.MedicalPackages = _context.GetMedicalPackages();
                    //model.NFZUnits = _context.GetNFZUnits();
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
        [HttpPost]
        public IActionResult PatientItemRemove(PatientDetailsViewModel model)
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                if (model.CurrentPatientId > 0)
                {
                    Patient patient = _context.GetPatientById(model.CurrentPatientId);
                    if (patient != null)
                    {
                        if (_context.HasPatientVisits(patient.Id))
                        {
                            model.Message = "Pacjent posiada przypisane wizyty!";
                            model.AlertMessageType = AlertMessageType.ErrorMessage;
                        }
                        else
                        {
                            _context.RemovePatientById(model.CurrentPatientId);
                            model.Message = "Pacjent został pomyślnie usunięty!";
                            model.AlertMessageType = AlertMessageType.SuccessMessage;
                        }
                        model.UserName = _loggedUser.Person.FullName;

                        return View(model);
                    }
                    model.Message = "Podczas próby usunięcia pacjenta nastąpił nieoczekiwany błąd!";
                    model.AlertMessageType = AlertMessageType.ErrorMessage;

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
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {

                model.NFZUnits = _context.GetNFZUnits();
                model.MedicalPackages = _context.GetMedicalPackages();
                model.AllPatients = _context.GetAllPatients();
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult PatientItemsManage()
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                PatientsManageViewModel model = new PatientsManageViewModel();
                model.NFZUnits = _context.GetNFZUnits();
                model.MedicalPackages = _context.GetMedicalPackages();
                model.AllPatients = _context.GetAllPatients();
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult MedicalWorkerItemsManage(MedicalWorkersManageViewModel model)
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                model.AllMedicalWorkers = _context.GetMedicalWorkers();
                model.PrimaryServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService == true).ToList();
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult MedicalWorkerItemsManage()
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                MedicalWorkersManageViewModel model = new MedicalWorkersManageViewModel();
                model.AllMedicalWorkers = _context.GetMedicalWorkers();
                model.PrimaryServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService == true).ToList();
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult Temp(long id)
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                LocationsManageViewModel model = new LocationsManageViewModel();
                model.SelectedLocationId = id;
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult LocationItemEdit(long id)
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                LocationsManageViewModel model = new LocationsManageViewModel();
                Location loc = _context.GetLocationById(id);
                model.SelectedLocation = loc;// _context.GetLocationById(model.SelectedLocationId);
                model.GetRooms(_context, model.SelectedLocation);
                //model.UnasignedRooms = _context.GetUnasignedRooms();
                model.PrimaryServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService == true).ToList();
                model.UserName = _loggedUser.Person.FullName;

                return View(model);

            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult UserProfile()
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                UserViewModel model = new UserViewModel();
                model.User = _loggedUser;
                model.UserName = _loggedUser.Person.FullName;

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
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (_loggedUser != null)
            {
                model.PrimaryServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService == true).ToList();

                //model.GetServices(_context,model.SelectedLocation);
                if (model.ViewMode == ViewMode.Read)
                {
                    if (model.SelectedLocationId > 0)
                    {
                        //model.SelectedLocation = _context.GetLocationById(model.SelectedLocationId);
                        Location loc = _context.GetLocationById(model.SelectedLocationId);

                        model.SelectedLocation = loc;// _context.GetLocationById(model.SelectedLocationId);
                                                     // model.UnasignedRooms = _context.GetUnasignedRooms();
                        model.UserName = _loggedUser.Person.FullName;

                        return View(model);
                    }
                }
                else if (model.ViewMode == ViewMode.Edit)
                {
                    // model.SelectedLocation.MedicalRooms = loc.MedicalRooms;
                    // model.SelectedLocation.Id = loc.Id;
                    //model.UnasignedRooms = _context.GetUnasignedRooms();
                    model.UpdateSelectionOfRooms();
                    model.UpdateSelectionOfServices();
                    Location loc = _context.GetLocationById(model.SelectedLocationId);
                    model.SelectedLocation.ImagePath = loc.ImagePath;
                    if (model.SelectedLocation.IsValid)
                    {
                        //if (model.SelectedLocation.ImageFile != null)
                        //{
                        //    _context.UpdateLocationImage(model.SelectedLocation.ImageFile, model.SelectedLocation, _hostEnvironment.WebRootPath);
                        //    //_context.UpdatePersonImage()
                        //}
                        //else
                        //{
                        //    model.SelectedLocation.ImageFile = loc.ImageFile;
                        //    model.SelectedLocation.ImagePath = loc.ImagePath;
                        //}
                        //_context.AddLocation(model.SelectedLocation);
                        _context.UpdateLocation(model.SelectedLocation, _hostEnvironment.WebRootPath);
                        // model.UnasignedRooms = _context.GetUnasignedRooms();
                        //model.SuccessMessage = "Dane placówki zostały zaktualizowane";
                        TempData["successMessage"] = "Dane placówki zostały zaktualizowane";
                        return RedirectToAction("Temp", new { id = model.SelectedLocationId });

                    }
                    else
                    {
                        model.UserName = _loggedUser.Person.FullName;

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
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                if (model.SelectedLocationId > 0)
                {

                    Location location = _context.GetLocationById(model.SelectedLocationId);
                    model.SuccessMessage = "Placówka została pomyślnie usunięta!";
                    TempData["successMessage"] = "Placówka została pomyślnie usunięta!";
                    _context.RemoveLocationById(model.SelectedLocationId);
                    model.UserName = _loggedUser.Person.FullName;
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
        public IActionResult LocationItemAdd(LocationsManageViewModel model)
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                // model.GetRooms(_context, model.SelectedLocation);
                model.SelectedLocation.MedicalRooms = new List<MedicalRoom>();

                if (model.SelectedLocation.IsValid)
                {
                    model.GetServices(_context, model.SelectedLocation);


                    //if (model.SelectedLocation.ImageFile != null)
                    //{
                    //    _context.UpdateLocationImage(model.SelectedLocation.ImageFile, model.SelectedLocation, _hostEnvironment.WebRootPath);
                    //}
                    TempData["successMessage"] = "Placówka została dodana";
                    _context.AddLocation(model.SelectedLocation, model.SelectedLocation.ImageFile, _hostEnvironment.WebRootPath);
                    return RedirectToAction("LocationItemAdd");
                }
                else
                {
                    if (model.SelectedLocation.ImageFile == null)
                    {
                        TempData["errorMessage"] = "Wybierz zdjęcie placówki!";
                    }
                    else
                    {
                        TempData["errorMessage"] = "Placówka nie została dodana. Uzupełnij wszystkie pola!";
                    }

                }
                // model.UnasignedRooms = _context.GetUnasignedRooms();
                model.PrimaryServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService == true).ToList();
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult LocationItemAdd()
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                LocationsManageViewModel model = new LocationsManageViewModel();
                model.SelectedLocation = new Location();
                //model.UnasignedRooms = _context.GetUnasignedRooms();
                model.PrimaryServices = _context.GetMedicalServices().Where(c => c.IsPrimaryService == true).ToList();
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult LocationItemsManage()
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                LocationsManageViewModel model = new LocationsManageViewModel();
                model.AllLocations = _context.GetAllLocations();
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult RoomItemRemove(RoomItemsManageViewModel model)
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                if (model.SelectedRoomId > 0)
                {
                    MedicalRoom room = _context.GetRoomById(model.SelectedRoomId);
                    if (room != null)
                    {
                        _context.RemoveMedicalRoomById(model.SelectedRoomId);
                        TempData[MESSAGE] = new ViewMessage()
                        {
                            Message = "Gabinet został pomyślnie usunięty!",
                            MessageType = AlertMessageType.InfoMessage
                        };
                    }
                    else
                    {
                        TempData[MESSAGE] = new ViewMessage()
                        {
                            Message = "Gabinet nie został usunięty! Wystąpiły błędy!",
                            MessageType = AlertMessageType.InfoMessage
                        };

                        //TempData[ERROR_MESSAGE] = "Gabinet nie został usunięty! Wystąpiły błędy";
                    }
                    return RedirectToAction("RoomItemsManage");
                }
                else
                {
                    TempData[MESSAGE] = new ViewMessage()
                    {
                        Message = "Gabinet nie został usunięty! Wystąpiły błędy!",
                        MessageType = AlertMessageType.InfoMessage
                    };

                    //TempData[ERROR_MESSAGE] = "Gabinet nie został usunięty! Wystąpiły błędy";
                    return RedirectToAction("RoomItemsManage");
                }
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult RoomItemsManage(RoomItemsManageViewModel model)
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                switch (model.ViewMode)
                {
                    case ViewMode.Read:
                        if (model.SelectedLocationId > 0 || model.SelectedLocationId == -2)
                        {
                            model.SelectedLocation = _context.GetLocationById(model.SelectedLocationId);
                        }

                        break;

                    case ViewMode.Edit:
                        break;
                    case ViewMode.Remove:
                        //if (model.SelectedRoomId>0)
                        //{
                        //    MedicalRoom room = _context.GetRoomById(model.SelectedRoomId);
                        //    if (room!=null)
                        //    {
                        //        _context.RemoveMedicalRoomById(room.Id);
                        //        TempData[SUCCESS_MESSAGE] = "Pokój został usunięty!";
                        //        //model.AllRooms = _context.GetAllRooms();
                        //        //model.Locations = _context.GetAllLocations();

                        //        //return View(model);
                        //    }
                        //    else
                        //    {
                        //        return NotFound();
                        //    }
                        //}

                        break;
                    case ViewMode.Add:
                        if (model.NewRoom.IsValid)
                        {
                            _context.AddMedicalRoom(model.NewRoom);
                            TempData[MESSAGE] = new ViewMessage()
                            {
                                Message = "Pokój został pomyślnie dodany!",
                                MessageType = AlertMessageType.InfoMessage
                            };

                            //TempData[SUCCESS_MESSAGE] = "Pokój został pomyślnie dodany!";

                        }
                        break;
                    default:
                        break;
                }
                model.AllRooms = _context.GetAllRooms();
                model.Locations = _context.GetAllLocations();
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult RoomItemAdd(RoomItemsManageViewModel model)
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                //RoomItemsManageViewModel model = new RoomItemsManageViewModel();
                //model.AllRooms = _context.GetAllRooms();
                if (model.NewRoom.IsValid)
                {
                    _context.AddMedicalRoom(model.NewRoom);
                    TempData[MESSAGE] = new ViewMessage()
                    {
                        Message = "Gabinet medyczny został pomyślnie dodany!",
                        MessageType = AlertMessageType.InfoMessage
                    };

                    //TempData[SUCCESS_MESSAGE] = "Gabinet medyczny został pomyślnie dodany!";
                }
                else
                {
                    TempData[MESSAGE] = new ViewMessage()
                    {
                        Message = "Gabinet medyczny nie został dodany! Wpełnij wszystkie pola!",
                        MessageType = AlertMessageType.WarningMessage
                    };

                    //TempData[ERROR_MESSAGE] = "Gabinet medyczny nie został dodany! Wpełnij wszystkie pola!";
                }
                //else
                //{
                //    return View(model);
                //}
                model.Locations = _context.GetAllLocations();
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult RoomItemAdd()
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                RoomItemsManageViewModel model = new RoomItemsManageViewModel();
                //model.AllRooms = _context.GetAllRooms();
                model.Locations = _context.GetAllLocations();
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult RoomItemEdit(long id)
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                if (id > 0)
                {
                    RoomItemsManageViewModel model = new RoomItemsManageViewModel();
                    MedicalRoom room = _context.GetRoomById(id);
                    if (room != null)
                    {
                        model.Locations = _context.GetAllLocations();
                        model.UserName = _loggedUser.Person.FullName;
                        model.SelectedRoom = room;
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
        public IActionResult RoomItemEdit(RoomItemsManageViewModel model)
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                if (model.ViewMode == ViewMode.Read)
                {
                    if (model.SelectedRoomId > 0)
                    {
                        model.SelectedRoom = _context.GetRoomById(model.SelectedRoomId);
                        model.Locations = _context.GetAllLocations();
                        if (model.SelectedRoom.Location != null)
                        {
                            model.SelectedRoom.LocationId = model.SelectedRoom.Location.Id;
                        }
                        model.UserName = _loggedUser.Person.FullName;

                        return View(model);
                    }
                }
                else if (model.ViewMode == ViewMode.Edit)
                {
                    if (model.SelectedRoom.IsValid)
                    {
                        if (model.SelectedRoom.LocationId > 0)
                        {
                            model.SelectedRoom.Location = _context.GetLocationById(model.SelectedRoom.LocationId.Value);
                        }
                        _context.UpdateRoom(model.SelectedRoom);
                        model.Locations = _context.GetAllLocations();
                        TempData[MESSAGE] = new ViewMessage()
                        {
                            Message = "Dane gabinetu zostały pomyślnie zaktualizowane!",
                            MessageType = AlertMessageType.InfoMessage
                        };

                        //TempData[SUCCESS_MESSAGE] = "Dane gabinetu zostały pomyślnie zaktualizowane!";
                        model.UserName = _loggedUser.Person.FullName;

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

        public IActionResult RoomItemsManage()
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                RoomItemsManageViewModel model = new RoomItemsManageViewModel();
                model.AllRooms = _context.GetAllRooms();
                model.Locations = _context.GetAllLocations();
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult MedicalPackageItemAdd()
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                PackageItemsManageViewModel model = new PackageItemsManageViewModel();
                model.MedicalServices = _context.GetMedicalServices();
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult MedicalPackageItemAdd(PackageItemsManageViewModel model)
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                model.MedicalServices = _context.GetMedicalServices();
                model.SelectedPackage.ServiceDiscounts = model.UpdateDiscountsWithInputValues();
                if (model.SelectedPackage.IsValid)
                {
                    _context.AddMedicalPackage(model.SelectedPackage);
                    TempData[MESSAGE] = new ViewMessage()
                    {
                        Message = "Pakiet został pomyślnie dodany!",
                        MessageType = AlertMessageType.InfoMessage
                    };

                    //TempData[SUCCESS_MESSAGE] = "Pakiet został pomyślnie dodany!";
                    return RedirectToAction("MedicalPackageItemsManage");
                }
                else
                {
                    TempData[MESSAGE] = new ViewMessage()
                    {
                        Message = "Pakiet nie został dodany! Popraw dane!",
                        MessageType = AlertMessageType.WarningMessage
                    };

//                    TempData[ERROR_MESSAGE] = "Pakiet nie został dodany! Popraw dane!";
                }
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult MedicalPackageItemEdit(PackageItemsManageViewModel model)
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                if (model.SelectedPackage != null)
                {
                    model.SelectedPackageId = model.SelectedPackage.Id;
                }
                if (model.ViewMode == ViewMode.Read)
                {
                    if (model.SelectedPackageId > 0)
                    {
                        MedicalPackage package = _context.GetMedicalPackageById(model.SelectedPackageId);
                        model.MedicalServices = _context.GetMedicalServices();
                        model.SelectedPackage = package;
                        ModelState.Clear();
                    }
                }
                else if (model.ViewMode == ViewMode.Edit)
                {
                    MedicalPackage package = _context.GetMedicalPackageById(model.SelectedPackage.Id);
                    model.MedicalServices = _context.GetMedicalServices();

                    model.SelectedPackage.ServiceDiscounts = package.ServiceDiscounts;
                    for (int i = 0; i < model.SelectedPackage.ServiceDiscounts.Count; i++)
                    {
                        MedicalServiceDiscount item = model.SelectedPackage.ServiceDiscounts[i];
                        item.MedicalServiceId = item.MedicalService.Id;
                        item.Discount = model.Vals[i];
                    }
                    foreach (MedicalServiceDiscount item in model.SelectedPackage.ServiceDiscounts)
                    {
                    }
                    //model.SelectedPackage.ServiceDiscounts = model.UpdateDiscountsWithInputValues();

                    if (model.SelectedPackage.IsValid)
                    {
                        _context.UpdateMedicalPackage(model.SelectedPackage);
                        TempData[MESSAGE] = new ViewMessage()
                        {
                            Message = "Pakiet został pomyślnie zaktualizowany!",
                            MessageType = AlertMessageType.InfoMessage
                        };

                        //TempData[SUCCESS_MESSAGE] = "Pakiet został pomyślnie zaktualizowany!";
                    }
                    else
                    {
                        TempData[MESSAGE] = new ViewMessage()
                        {
                            Message = "Wprowadzone dane są błędne/niepełne. Pakiet nie został dodany!",
                            MessageType = AlertMessageType.WarningMessage
                        };

                        //TempData[ERROR_MESSAGE] = "Wprowadzone dane są błędne/niepełne. Pakiet nie został dodany!";
                    }

                }
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult MedicalPackageItemRemove(PackageItemsManageViewModel model)
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                if (model.SelectedPackage.Id > 0)
                {

                    MedicalPackage package = _context.GetMedicalPackageById(model.SelectedPackage.Id);
                    try
                    {
                        _context.RemoveMedicalPackageById(model.SelectedPackage.Id);
                        TempData[MESSAGE] = new ViewMessage()
                        {
                            Message = "Pakiet medyczny został pomyślnie usunięty!",
                            MessageType = AlertMessageType.InfoMessage
                        };

                        //TempData[SUCCESS_MESSAGE] = "Pakiet medyczny został pomyślnie usunięty!";
                    }
                    catch (Microsoft.EntityFrameworkCore.DbUpdateException exc)
                    {
                        TempData[MESSAGE] = new ViewMessage()
                        {
                            Message = "Nie można usunąć pakietu, który jest przypisany do jakiegokolwiek pacjenta!",
                            MessageType = AlertMessageType.WarningMessage
                        };

                        //TempData[ERROR_MESSAGE] = "Nie można usunąć pakietu, który jest przypisany do jakiegokolwiek pacjenta!";
                    }


                    model.UserName = _loggedUser.Person.FullName;

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
        public IActionResult MedicalPackageItemsManage(PackageItemsManageViewModel model)
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                //if (model.ViewMode==ViewMode.)
                //{

                //}
                //PackageItemsManageViewModel model = new PackageItemsManageViewModel();
                model.MedicalPackages = _context.GetMedicalPackages();
                model.MedicalServices = _context.GetMedicalServices();
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult MedicalPackageItemsManage()
        {
            UserId = HttpContext.User.GetUserId().Value;
            _loggedUser = _context.GetUserById(UserId);

            if (_loggedUser != null)
            {
                PackageItemsManageViewModel model = new PackageItemsManageViewModel();
                model.MedicalPackages = _context.GetMedicalPackages();
                model.MedicalServices = _context.GetMedicalServices();
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }
        //public IActionResult MedicalServiceItemsAdd()
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
        //public IActionResult MedicalServiceItemsManage()
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
