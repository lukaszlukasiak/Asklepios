using Asklepios.Core.Enums;
using Asklepios.Core.MockModels;
using Asklepios.Core.Models;
using Asklepios.Data.Interfaces;
//using Asklepios.Web.Areas.CustomerServiceArea.Models;
using Asklepios.Web.Areas.MedicalWorkerArea.Models;
using Asklepios.Web.Extensions;
using Asklepios.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.MedicalWorkerArea.Controllers
{
    [Area("MedicalWorkerArea")]
    [Authorize(Roles = "MedicalWorker")]
    public class MedicalWorkerController : Controller
    {
        //private readonly ILogger<MedicalWorkerController> _logger;
        IWebHostEnvironment _hostEnvironment { get; set; }
        SignInManager<User> _signManager { get; set; }
        long UserId { get; set; }

        private VisitSummaryModel VisitSummary { get; set; }

        private User _loggedUser { get; set; }
        //private static Person _person { get; set; }
        private long? _medicalWorkerId { get; set; }
        public long? _currentVisitId { get; private set; }

        //session
        const string CURRENT_VISIT_ID = "CURRENT_VISIT_ID";
        const string USER_ID = "USER_ID";
        const string MEDICAL_WORKER_ID = "MEDICAL_WORKER_ID";

        //tempdata
        const string TEMP_VISIT_SUMMARY = "TEMP_VISIT_SUMMARY";


        IMedicalWorkerModuleRepository _context;
        public MedicalWorkerController(IMedicalWorkerModuleRepository context, IWebHostEnvironment hostEnvironment, SignInManager<User> signManager)
        {
            _hostEnvironment = hostEnvironment;
            _signManager = signManager;
            _context = context;
        }

        private long? GetCurrentVisitId()
        {
            if (!string.IsNullOrWhiteSpace(HttpContext.Session.GetString(CURRENT_VISIT_ID)))
            {
                if (long.TryParse(HttpContext.Session.GetString(CURRENT_VISIT_ID), out long id))
                {
                    return id;
                }
            }
            return null;
        }
        private long? GetCurrentMedicalWorkerId()
        {
            if (!string.IsNullOrWhiteSpace(HttpContext.Session.GetString(MEDICAL_WORKER_ID)))
            {
                if (long.TryParse(HttpContext.Session.GetString(MEDICAL_WORKER_ID), out long id))
                {
                    return id;
                }
            }
            return null;
        }
        private long? GetCurrentUserId()
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
        //private Visit GetTempVisit()
        //{
        //    if (TempData.ContainsKey(TEMP_VISIT))
        //    {
        //        var visit=TempData[TEMP_VISIT];
        //        return visit as Visit;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public async Task<IActionResult> LogOutAsync()
        {
            //_loggedUser = null;
            //_person = null;
            //_medicalWorker = null;
            await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "HomeArea" });
        }

        public IActionResult Index()
        {
            
            if (TempData.ContainsKey("User") == true)
            {
                User user = JsonConvert.DeserializeObject<User>((string)TempData["User"]);
                _loggedUser = user;
                _loggedUser.Person = _context.GetPersonById(_loggedUser.PersonId.Value);
                //_loggedUser.Person = _context.GetPersonById(_loggedUser.PersonId.Value);
                MedicalWorker _medicalWorker = _context.GetMedicalWorkerByUserId(_loggedUser.Id);

                HttpContext.Session.SetString(USER_ID, _loggedUser.Id.ToString());
                HttpContext.Session.SetString(MEDICAL_WORKER_ID, _medicalWorker.Id.ToString());


                ViewData["UserName"] = _loggedUser.Person.FullName;
            }

            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
            }
            else
            {
                return NotFound();
            }
            return RedirectToAction("Dashboard");
        }
        [HttpPost]
        public IActionResult SetAsCurrent(int id)
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                _currentVisitId = id;
                HttpContext.Session.SetString(CURRENT_VISIT_ID, id.ToString());
                TempData.Put<VisitSummaryModel>(TEMP_VISIT_SUMMARY, null);
                return RedirectToAction("CurrentVisit");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);
            MedicalWorker _medicalWorker = _context.GetMedicalWorkerByUserId(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {

                //_medicalWorker.AllVisits.AddRange( _context.GetFutureVisitsByMedicalWorkerId(_medicalWorker.Id));
                //List<Visit> visits= _context.GetVisitsByMedicalWorkerId(_loggedUser.Id);
                //_medicalWorker.AllVisits=(visits);

                DashboardViewModel model = new DashboardViewModel(_medicalWorker)
                {
                    UserName = _loggedUser.Person.FullName,
                    TodayVisits = _context.GetVisitsByMedicalWorkerId(_medicalWorker.Id).Where(c => c.DateTimeSince.Date == DateTime.Now.Date).AsQueryable(),
                };
                return View(model);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public IActionResult CurrentVisit()
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                if (ModelState.IsValid)
                {
                }
                _currentVisitId = GetCurrentVisitId();

                if (_currentVisitId > 0)
                {
                    //if (TempData.ContainsKey(TEMP_VISIT))
                    //{
                    VisitSummary = TempData.Get<VisitSummaryModel>(TEMP_VISIT_SUMMARY);//TempData[TEMP_VISIT] as Visit;
                    //}
                    if (VisitSummary == null)
                    {
                        //VisitSummary = visit;
                        VisitSummary = new VisitSummaryModel();
                    }

                    //if (visit.VisitStatus != Core.Enums.VisitStatus.AvailableNotBooked)
                    //{

                    Visit visit = _context.GetBookedVisitById(_currentVisitId.Value);

                    if (visit == null)
                    {
                        return NotFound();
                    }

                    PrepareVisit(visit);

                    CurrentVisitViewModel model = new CurrentVisitViewModel();
                    List<MedicalService> servicesForReferrals = _context.GetMedicalServices().Where(c => c.RequireRefferal == true).ToList();
                    model.MedicalServicesForReferrals = servicesForReferrals;
                    model.UserName = _loggedUser.Person.FullName;
                    model.VisitSummary = VisitSummary;
                    model.Visit = visit;
                    //model.TempVisit = TempVisit;
                    TempData.Put<VisitSummaryModel>(TEMP_VISIT_SUMMARY, VisitSummary);
                    return View(model);
                    //}
                    //else
                    //{
                    //    return RedirectToAction("Dashboard");
                    //}                  
                }
                else
                {
                    return RedirectToAction("Dashboard");
                }
            }
            else
            {
                return NotFound();
            }

        }

        private void PrepareVisit(Visit visit)
        {
            visit.Patient = _context.GetPatientById(visit.PatientId.Value);
            visit.Patient.MedicalPackage = _context.GetMedicalPackageById(visit.Patient.MedicalPackageId.Value);

            visit.MedicalWorker = _context.GetMedicalWorkerById(GetCurrentMedicalWorkerId().Value);
            visit.Location = _context.GetLocationById(visit.LocationId.Value);
            visit.MedicalRoom = _context.GetMedicalRoomById(visit.MedicalRoomId.Value);
            visit.VisitCategory = _context.GetVisitCategoryById(visit.VisitCategoryId.Value);
        }

        public IActionResult UserProfile()
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                //MedicalWorker medicalWorker = _context.GetMedicalWorkerByUserId(User.);
                MedicalWorkerViewModel model = new MedicalWorkerViewModel
                {
                    MedicalWorker = _context.GetMedicalWorkerById(HttpContext.User.GetUserId().Value)
                };
                model.MedicalWorker.User = _loggedUser;//_context.GetUserById(_loggedUser.Id);
                model.UserName = _loggedUser.Person.FullName;
                return View(model);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult CurrentVisit(CurrentVisitViewModel model)
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                //switch (model.SubmitMode)
                //{
                //    case SubmitMode.AddReferral:
                //        //AddExaminationReferral(model);
                //        for (int i = ModelState.Keys.Count() - 1; i >= 0; i--)
                //        {
                //            string item = ModelState.Keys.ElementAt(i);
                //            if (item.Contains("Referral") || item.Contains("MedicalServiceToAddId"))
                //            {

                //            }
                //            else
                //            {
                //                //ModelState[item].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
                //                ModelState.Remove(item);
                //            }
                //        }

                //        break;
                //    case SubmitMode.AddPrescription:
                //        //string[] keys = ModelState.Keys;
                //        for (int i = ModelState.Keys.Count() - 1; i >= 0; i--)
                //        {
                //            string item = ModelState.Keys.ElementAt(i);
                //            if (item.Contains("Prescription") || item.Contains("Medicine"))
                //            {

                //            }
                //            else
                //            {
                //                //ModelState[item].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
                //                ModelState.Remove(item);
                //            }
                //        }
                //        //foreach (string item in ModelState.Keys)
                //        //{
                //        //    if (item.Contains("Prescription") || item.Contains("Medicine"))
                //        //    {

                //        //    }
                //        //    else
                //        //    {
                //        //        ModelState[item].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
                //        //        ModelState.Remove[item];
                //        //    }
                //        //}
                //        break;
                //    case SubmitMode.AddTestRestult:
                //        for (int i = ModelState.Keys.Count() - 1; i >= 0; i--)
                //        {
                //            string item = ModelState.Keys.ElementAt(i);
                //            if (item.Contains("MedicalTest") || item.Contains("Visit.Id"))
                //            {

                //            }
                //            else
                //            {
                //                //ModelState[item].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
                //                ModelState.Remove(item);
                //            }
                //        }

                //        break;
                //    case SubmitMode.AddMedicineToPrescription:
                //        for (int i = ModelState.Keys.Count() - 1; i >= 0; i--)
                //        {
                //            string item = ModelState.Keys.ElementAt(i);
                //            if (item.Contains("Medicine"))
                //            {

                //            }
                //            else
                //            {
                //                ModelState.Remove(item);
                //            }
                //        }
                //        break;
                //    case SubmitMode.Other:
                //        break;
                //    default:
                //        break;
                //}
                //switch (model.SubmitMode)
                //{
                //    case SubmitMode.AddReferral:
                //        if (ModelState.IsValid)
                //        {

                //            AddExaminationReferral(model);
                //        }
                //        break;
                //    case SubmitMode.AddPrescription:
                //        if (ModelState.IsValid)
                //        {
                //            AddPrescription(model);
                //        }

                //        break;
                //    case SubmitMode.AddTestRestult:
                //        if (ModelState.IsValid)
                //        {
                //            AddTestResult(model);
                //        }

                //        break;
                //    case SubmitMode.AddMedicineToPrescription:
                //        if (ModelState.IsValid)
                //        {
                //            AddMedicineToPrescription(model);
                //        }

                //        break;
                //    case SubmitMode.Other:
                //        break;
                //    default:
                //        break;
                //}


                Visit visit = _context.GetBookedVisitById(GetCurrentVisitId().Value);
                if (visit == null)
                {
                    return NotFound();
                }
                model.Visit = visit;
                List<MedicalService> servicesForReferrals = _context.GetMedicalServices().Where(c => c.RequireRefferal == true).ToList();
                model.MedicalServicesForReferrals = servicesForReferrals;
                model.UserName = _loggedUser.Person.FullName;
                TempData.Put<VisitSummaryModel>(TEMP_VISIT_SUMMARY, VisitSummary);

                return View(model);

            }
            else
            {
                return NotFound();
            }
            //return View();


        }

        public IActionResult AddMedicineToPrescription(CurrentVisitViewModel model)
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);
            if (_loggedUser != null)
            {
                //if (TempData.ContainsKey(TEMP_VISIT_SUMMARY))
                //{
                //    VisitSummary = TempData[TEMP_VISIT_SUMMARY] as Visit;
                //}
                VisitSummary = TempData.Get<VisitSummaryModel>(TEMP_VISIT_SUMMARY);

                if (VisitSummary != null)
                {
                    //if (VisitSummary.PrescriptionToAdd == null)
                    //{
                    //    VisitSummary.PrescriptionToAdd = new Prescription
                    //    {
                    //        IssuedById = model.MedicalWorkerId,
                    //        IssuedToId = model.PatientId,
                    //        IssueDate = DateTime.Now,
                    //        ExpirationDate = DateTime.Now.AddDays(model.PrescriptionDaysToExpire)
                    //    };
                    //}
                    VisitSummary.Medicines.Add(model.VisitSummary.MedicineToAdd);
                    //TempData[TEMP_VISIT] = TempVisit;
                    TempData.Put<VisitSummaryModel>(TEMP_VISIT_SUMMARY, VisitSummary);
                    TempData.Put<ViewMessage>(ViewMessage.MESSAGE_KEY, new ViewMessage() { Message = "Lek został dodany!", MessageType = Enums.AlertMessageType.InfoMessage });


                    return RedirectToAction("CurrentVisit");
                }
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult AddTestResult(CurrentVisitViewModel model)
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                //if (TempData.ContainsKey(TEMP_VISIT_SUMMARY))
                //{
                //    VisitSummary = TempData[TEMP_VISIT_SUMMARY] as Visit;
                //}
                VisitSummary = TempData.Get<VisitSummaryModel>(TEMP_VISIT_SUMMARY);

                if (VisitSummary != null)
                {
                    MedicalService medicalService = _context.GetMedicalServiceById(model.VisitSummary.TestResult.MedicalServiceId.Value);
                    if (model.MedicalTestFile != null)
                    {

                        //MedicalTestResult medicalTestResult = new MedicalTestResult
                        //{
                        //    VisitId = VisitSummary.Id,
                        //    ExamDate = VisitSummary.DateTimeSince,
                        //    MedicalWorkerId = VisitSummary.MedicalWorkerId,
                        //    PatientId = VisitSummary.PatientId,
                        //    MedicalService = medicalService,
                        //    UploadDate=DateTime.Now,
                        //    //PdfDocument=model.MedicalTestFile.,

                        //};
                        //MemoryStream stream = new MemoryStream();

                        //model.MedicalTestFile.CopyTo(stream);

                        //model.VisitSummary.TestResult.Document = stream.ToArray();
                        model.VisitSummary.TestResult.FilePath = _context.SaveFile(model.MedicalTestFile, StorageFolderType.TestResult, _hostEnvironment.WebRootPath);
                        model.VisitSummary.TestResult.MedicalServiceId = medicalService.Id;
                        model.VisitSummary.TestResult.MedicalServiceName = medicalService.Name;
                        model.VisitSummary.TestResult.ExamDate = DateTime.Now;

                        if (model.VisitSummary.TestResult.IsModelValid)
                        {
                            VisitSummary.TestResult = model.VisitSummary.TestResult;
                            TempData.Put<ViewMessage>(ViewMessage.MESSAGE_KEY, new ViewMessage() { Message = "Wyniki zostały dodane!", MessageType = Enums.AlertMessageType.InfoMessage });
                        }
                        else
                        {
                            TempData.Put<ViewMessage>(ViewMessage.MESSAGE_KEY, new ViewMessage() { Message = "Wypełnij poprawnie wszystkie pola!", MessageType = Enums.AlertMessageType.WarningMessage });
                        }
                        //VisitSummary.MedicalTestResult = medicalTestResult;
                        //TempData[TEMP_VISIT] = TempVisit;
                    }
                    else
                    {
                        TempData.Put<ViewMessage>(ViewMessage.MESSAGE_KEY, new ViewMessage() { Message = "Wybierz plik z wynikami!", MessageType = Enums.AlertMessageType.WarningMessage });
                    }
                    TempData.Put<VisitSummaryModel>(TEMP_VISIT_SUMMARY, VisitSummary);

                    //ModelState.Clear();
                    return RedirectToAction("CurrentVisit");

                }


            }
            return NotFound();
        }

        public IActionResult AddPrescription(CurrentVisitViewModel model)
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                //if (TempData.ContainsKey(TEMP_VISIT_SUMMARY))
                //{
                //    VisitSummary = TempData[TEMP_VISIT_SUMMARY] as Visit;
                //}
                VisitSummary = TempData.Get<VisitSummaryModel>(TEMP_VISIT_SUMMARY);

                if (VisitSummary != null)
                {
                    //PrescriptionMock prescription = new PrescriptionMock
                    //{
                    //    AccessCode = model.PrescriptionToAdd.AccessCode,
                    //    ExpirationDate = DateTimeOffset.Now.AddDays(model.PrescriptionDaysToExpire),// model.PrescriptionToAdd.ExpirationDate;
                    //    IdentificationCode = model.PrescriptionToAdd.IdentificationCode,
                    //    IssueDate = DateTime.Now,
                    //    Visit = VisitSummary,
                    //    IssuedBy = VisitSummary.MedicalWorker,
                    //    IssuedTo = VisitSummary.Patient
                    //};
                    if (VisitSummary.Medicines == null)
                    {
                        VisitSummary.Medicines = new List<MedicineMock>();
                    }
                    if (model.VisitSummary.PrescriptionToAdd.IsModelValid)
                    {
                        if (model.VisitSummary.MedicineToAdd.IsModelValid)
                        {
                            VisitSummary.PrescriptionToAdd = model.VisitSummary.PrescriptionToAdd;

                            VisitSummary.PrescriptionToAdd.IssueDate = DateTime.Now;
                            VisitSummary.PrescriptionToAdd.ExpirationDate = DateTime.Now.AddDays(model.VisitSummary.PrescriptionToAdd.DaysToExpire);

                            VisitSummary.Medicines.Add(model.VisitSummary.MedicineToAdd);
                            TempData.Put<ViewMessage>(ViewMessage.MESSAGE_KEY, new ViewMessage() { Message = "Recepta wraz z lekiem zostały dodane!", MessageType = Enums.AlertMessageType.InfoMessage });
                        }
                        else
                        {
                            TempData.Put<ViewMessage>(ViewMessage.MESSAGE_KEY, new ViewMessage() { Message = "Wypełnij poprawnie wszystkie pola!", MessageType = Enums.AlertMessageType.WarningMessage });
                        }
                    }
                    else
                    {
                        TempData.Put<ViewMessage>(ViewMessage.MESSAGE_KEY, new ViewMessage() { Message = "Wypełnij poprawnie wszystkie pola!", MessageType = Enums.AlertMessageType.WarningMessage });
                    }
                    ModelState.Clear();
                    //TempData[TEMP_VISIT] = TempVisit;
                    TempData.Put<VisitSummaryModel>(TEMP_VISIT_SUMMARY, VisitSummary);

                    return RedirectToAction("CurrentVisit");

                }
            }
            return NotFound();
        }
        //public IActionResult DownloadTempTestResults()
        //{
        //    byte[] bytes = TempVisit.MedicalTestResult.Document;

        //    return File(bytes, "application/octet-stream", System.IO.Path.GetFileName("wynik"));

        //}

        [HttpPost]
        public IActionResult Schedule(ScheduleViewModel model)
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                model.MedicalWorker = _context.GetMedicalWorkerById(GetCurrentMedicalWorkerId().Value);

                //ScheduleViewModel model = new ScheduleViewModel(_medicalWorker);
                if (model.SelectedDate != null)
                {
                    //model.MedicalWorker.AllVisits.AddRange(visits);//.Where(c => c.DateTimeSince.Date == model.SelectedDate.Value.Date).ToList();
                }
                model.UserName = _loggedUser.Person.FullName;
                model.AllForthcomingVisits = _context.GetFutureVisitsByMedicalWorkerId(model.MedicalWorker.Id);
                return View(model);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Schedule()
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                MedicalWorker _medicalWorker = _context.GetMedicalWorkerById(GetCurrentMedicalWorkerId().Value);
                //_medicalWorker.AllVisits.AddRange(_context.GetFutureVisitsByMedicalWorkerId(_medicalWorker.Id));
                ScheduleViewModel model = new ScheduleViewModel(_medicalWorker);
                model.AllForthcomingVisits = _context.GetFutureVisitsByMedicalWorkerId(_medicalWorker.Id);
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult DownloadTempResults()
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                VisitSummary = TempData.Get<VisitSummaryModel>(TEMP_VISIT_SUMMARY);

                byte[] bytes = _context.GetDocument(VisitSummary.TestResult.FilePath, _hostEnvironment.WebRootPath);
                TempData.Put<VisitSummaryModel>(TEMP_VISIT_SUMMARY,VisitSummary);

                return File(bytes, "application/octet-stream", System.IO.Path.GetFileName(VisitSummary.TestResult.FilePath));
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult DownloadTestResults(string id)
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {

                //Build the File Path.
                if (long.TryParse(id, out long idL))
                {

                    //Visit visit = _context.GetBookedVisitById(_currentVisitId.Value);
                    //_medicalWorker.AllVisits = _context.GetHistoricalVisitsByMedicalWorkerId(_medicalWorker.Id);
                    MedicalTestResult testResult = _context.GetMedicalTestResultById(idL);//_medicalWorker.AllVisits.Where(c => c.MedicalTestResult != null && c.MedicalTestResult.Id == idL).FirstOrDefault().MedicalTestResult;

                    if (testResult != null)
                    {
                        testResult.Document = _context.GetDocument(testResult.DocumentPath, _hostEnvironment.WebRootPath);
                        byte[] bytes = testResult.Document;
                        return File(bytes, "application/octet-stream", System.IO.Path.GetFileName(testResult.DocumentPath));
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
        //do przystosowania
        [HttpPost]
        public IActionResult History(HistoricalVisitsViewModel model)
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                model.HistoricalVisits = _context.GetHistoricalVisitsByMedicalWorkerId(GetCurrentMedicalWorkerId().Value);
                model.MedicalWorker = _context.GetMedicalWorkerById(GetCurrentMedicalWorkerId().Value);//_medicalWorker;
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult History()
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                MedicalWorker medicalWorker = _context.GetMedicalWorkerById(GetCurrentMedicalWorkerId().Value);
                HistoricalVisitsViewModel model = new HistoricalVisitsViewModel(medicalWorker)
                {
                    UserName = _loggedUser.Person.FullName
                };
                model.HistoricalVisits = _context.GetHistoricalVisitsByMedicalWorkerId(medicalWorker.Id);
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
                MedicalWorker medicalWorker = _context.GetMedicalWorkerById(GetCurrentMedicalWorkerId().Value);
                medicalWorker.User = _context.GetUserById(GetCurrentMedicalWorkerId().Value);
                ContactMessageViewModel model = new ContactMessageViewModel(medicalWorker);
                model.UserName = _loggedUser.Person.FullName;

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
                MedicalWorker medicalWorker = _context.GetMedicalWorkerById(GetCurrentMedicalWorkerId().Value);

                ContactMessageViewModel modelP = new ContactMessageViewModel(medicalWorker);
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
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult SetAsCompleted(CurrentVisitViewModel model)
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                //List<Visit> todayVisits = _context.GetFutureVisitsByMedicalWorkerId(GetCurrentMedicalWorkerId().Value)?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                // Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                //Visit visit = GetTempVisit();
                VisitSummary = TempData.Get<VisitSummaryModel>(TEMP_VISIT_SUMMARY);//TempData[TEMP_VISIT] as Visit;
                Visit visit=_context.GetVisitById(GetCurrentVisitId().Value);

                if (VisitSummary != null && visit!=null)
                {
                    visit.VisitStatus = Core.Enums.VisitStatus.Finished;
                    visit = VisitSummary.UpdateVisit(visit);

                    for (int i = 0; i < visit.MinorMedicalServices.Count; i++)
                    {
                        visit.MinorMedicalServices[i] = _context.GetMedicalServiceById(visit.MinorMedicalServices[i].Id);
                    }
                    //foreach (var item in visit.MinorMedicalServices)
                    //{
                    //}
                    // visit.MinorMedicalServices=visit.MinorMedicalServices.ForEach(c => c = _context.GetMedicalServiceById(c.Id));
                    VisitSummary = null;
                    _context.UpdateVisit(visit);

                    if (visit.MedicalTestResult != null)
                    {
                        _context.AddNotification(visit.MedicalTestResult.Id, NotificationType.TestResult, visit.Patient.Id, DateTimeOffset.Now, visit.Id);
                    }
                    if (visit.Prescription != null)
                    {
                        _context.AddNotification(visit.Prescription.Id, NotificationType.Prescription, visit.Patient.Id, DateTimeOffset.Now, visit.Id);
                    }
                    if (visit.ExaminationReferrals != null)
                    {
                        foreach (MedicalReferral item in visit.ExaminationReferrals)
                        {
                            _context.AddNotification(item.Id, NotificationType.MedicalReferral, visit.Patient.Id, DateTimeOffset.Now, visit.Id);
                        }
                    }
                    HttpContext.Session.SetString(CURRENT_VISIT_ID, "");//  = -1;
                    TempData.Put<VisitSummaryModel>(TEMP_VISIT_SUMMARY, null);


                    return RedirectToAction("Dashboard");
                }
                return NotFound();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult SetAsCancelledNoPatient(CurrentVisitViewModel model)
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                //List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                //Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                //Visit visit = GetTempVisit();//_context.GetBookedVisitById(model.VisitId);
                //Visit visit = TempData.Get<Visit>(TEMP_VISIT);//TempData[TEMP_VISIT] as Visit;
                Visit visit = _context.GetVisitById(model.VisitId);

                if (visit != null)
                {
                    visit.VisitStatus = Core.Enums.VisitStatus.NotHeldAbsentPatient;
                    _context.UpdateVisit(visit);
                    //_currentVisitId = -1;
                    HttpContext.Session.SetString(CURRENT_VISIT_ID, "");//  = -1;
                    TempData.Put<VisitSummaryModel>(TEMP_VISIT_SUMMARY, null);

                    return RedirectToAction("Dashboard");
                }
                return NotFound();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult SetAsCancelledOther(CurrentVisitViewModel model)
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                //List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                //Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                Visit visit = _context.GetVisitById(model.VisitId);


                if (visit != null)
                {
                    visit.VisitStatus = Core.Enums.VisitStatus.NotHeldOther;
                    //_currentVisitId = -1;
                    _context.UpdateVisit(visit);
                    HttpContext.Session.SetString(CURRENT_VISIT_ID, "");//  = -1;
                    TempData.Put<VisitSummaryModel>(TEMP_VISIT_SUMMARY, null);

                    return RedirectToAction("Dashboard");
                }
                return NotFound();

            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult AddMedicalHistory(CurrentVisitViewModel model)
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                //List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                //Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                //Visit visit = _context.GetBookedVisitById(model.VisitId);
                //if (TempData.ContainsKey(TEMP_VISIT))
                //{
                VisitSummary = TempData.Get<VisitSummaryModel>(TEMP_VISIT_SUMMARY); //TempData[TEMP_VISIT_SUMMARY] as Visit;
                                                                                    //}

                if (VisitSummary != null)
                {
                    if (!string.IsNullOrWhiteSpace(model.VisitSummary.MedicalHistory))
                    {
                        VisitSummary.MedicalHistory = model.VisitSummary.MedicalHistory;
                        TempData.Put<ViewMessage>(ViewMessage.MESSAGE_KEY, new ViewMessage() { Message = "Wywiad medyczny został dodany!", MessageType = Enums.AlertMessageType.InfoMessage });
                    }
                    else
                    {
                        Asklepios.Web.Enums.AlertMessageType type = Enums.AlertMessageType.WarningMessage;
                        TempData.Put<ViewMessage>(ViewMessage.MESSAGE_KEY, new ViewMessage() { Message = "Uzupełnij wywiad medyczny!", MessageType = type });
                    }
                    //TempData[TEMP_VISIT] = TempVisit;
                    TempData.Put<VisitSummaryModel>(TEMP_VISIT_SUMMARY, VisitSummary);

                    return RedirectToAction("CurrentVisit");
                }
                return NotFound();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult AddRecommendation(CurrentVisitViewModel model)
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                //List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                //Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                //Visit visit = _context.GetBookedVisitById(model.VisitId);
                VisitSummary = TempData.Get<VisitSummaryModel>(TEMP_VISIT_SUMMARY);

                if (VisitSummary != null)
                {
                    //visitSummary.RecommendationToAdd.VisitId = VisitSummary.Id;
                    if (VisitSummary.Recommendations == null)
                    {
                        VisitSummary.Recommendations = new List<RecommendationMock>();
                    }
                    if (model.VisitSummary.RecommendationToAdd.IsModelValid)
                    {
                        VisitSummary.Recommendations.Add(model.VisitSummary.RecommendationToAdd);
                        TempData.Put<ViewMessage>(ViewMessage.MESSAGE_KEY, new ViewMessage() { Message = "Rekomendacje dodane!", MessageType = Enums.AlertMessageType.InfoMessage });
                    }
                    else
                    {
                        TempData.Put<ViewMessage>(ViewMessage.MESSAGE_KEY, new ViewMessage() { Message = "Uzupełnij poprawnie wszystkie pola!", MessageType = Enums.AlertMessageType.WarningMessage });
                    }
                    //TempData[TEMP_VISIT] = TempVisit;
                    TempData.Put<VisitSummaryModel>(TEMP_VISIT_SUMMARY, VisitSummary);
                    //_context.AddRecommendation(model.RecommendationToAdd);
                    return RedirectToAction("CurrentVisit");
                }
                return NotFound();

            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult RemoveRecommendation(CurrentVisitViewModel model)
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                //List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                //Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                //Visit visit = _context.GetBookedVisitById(model.VisitId);
                VisitSummary = TempData.Get<VisitSummaryModel>(TEMP_VISIT_SUMMARY);

                if (VisitSummary != null)
                {
                    //_context.DeleteRecommendation(model.RecommendationToRemove.Id);
                    //Recommendation recommendation = visit.Recommendations.Where(c => c.Id == model.RecommendationToRemove.Id).FirstOrDefault();
                    int index = model.RecommendationToRemoveIndex;//visit.Recommendations.FindIndex(c => c.Id == model.RecommendationToRemove.Id);

                    if (index > -1)
                    {
                        VisitSummary.Recommendations.RemoveAt((int)index);
                        TempData.Put<VisitSummaryModel>(TEMP_VISIT_SUMMARY, VisitSummary);
                        TempData.Put<ViewMessage>(ViewMessage.MESSAGE_KEY, new ViewMessage() { Message = "Zalecenia usunięte!", MessageType = Enums.AlertMessageType.InfoMessage });
                        return RedirectToAction("CurrentVisit");
                    }
                }
                return NotFound();
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult RemoveTestResult(CurrentVisitViewModel model)
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                //long id = model.MedicalTestFileIdToRemove;
                ////Build the File Path.
                //long mwId = GetCurrentMedicalWorkerId().Value;
                VisitSummary = TempData.Get<VisitSummaryModel>(TEMP_VISIT_SUMMARY);

                //Visit visit = _context.GetBookedVisitById(_currentVisitId.Value);
                // MedicalWorker medicalWorker = _context.GetHistoricalVisitsByMedicalWorkerId(mwId);//.AllVisits.AddRange(_context.GetHistoricalVisitsByMedicalWorkerId());
                //MedicalTestResult testResult = _context.GetMedicalTestResultById(id);//_medicalWorker.AllVisits.Where(c => c.MedicalTestResult != null && c.MedicalTestResult.Id == id).FirstOrDefault().MedicalTestResult;

                if (VisitSummary != null)
                {
                    //_context.RemoveTestResult(testResult.Id, testResult.VisitId.Value, _hostEnvironment.WebRootPath);
                    if (!string.IsNullOrWhiteSpace(VisitSummary.TestResult.FilePath))
                    {
                        _context.RemoveFile(VisitSummary.TestResult.FilePath, _hostEnvironment.WebRootPath);
                    }
                    VisitSummary.TestResult = null;
                    TempData.Put<VisitSummaryModel>(TEMP_VISIT_SUMMARY, VisitSummary);
                    TempData.Put<ViewMessage>(ViewMessage.MESSAGE_KEY, new ViewMessage() { Message = "Wyniki zostały usunięte!", MessageType = Enums.AlertMessageType.InfoMessage });

                    return RedirectToAction("CurrentVisit");
                }
                else
                {
                    TempData.Put<ViewMessage>(ViewMessage.MESSAGE_KEY, new ViewMessage() { Message = "Coś posżło nie tak!", MessageType = Enums.AlertMessageType.WarningMessage });
                    return NotFound();
                }

                //byte[] bytes =  //_context.GetTestResultById.Where(c => c.Id == idL).FirstOrDefault()?.PdfDocument;//pdf.  System.IO.File.ReadAllBytes(pdf);

                //Send the File to Download.

            }
            else
            {
                return NotFound();
            }
            //string path = Path.Combine(this.Environment.WebRootPath, "Files/") + fileName;
        }

        [HttpPost]
        //public IActionResult AddExaminationReferral(CurrentVisitViewModel model)
        public IActionResult AddExaminationReferral(CurrentVisitViewModel model)
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            //if (_loggedUser != null)
            //{
            //    if (!ModelState.IsValid)
            //    {
            //        return RedirectToAction("CurrentVisit");
            //    }
            //if (model.IsMedicalReferralAddingOK)
            //{
            //List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
            //Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
            //Visit visit = _context.GetBookedVisitById(model.VisitId);

            //if (TempData.ContainsKey(TEMP_VISIT))
            //{
            //TempVisit = TempData[TEMP_VISIT] as Visit;
            VisitSummary = TempData.Get<VisitSummaryModel>(TEMP_VISIT_SUMMARY);//TempData[TEMP_VISIT] as Visit;

            //}

            //if (VisitSummary != null)
            //{
            if (model.VisitSummary.ReferralMockToAdd.IsModelvalid)
            {

                //MedicalWorker medicalWorker=_context.GetMedicalWorkerById(GetCurrentMedicalWorkerId().Value);
                MedicalService medicalService = _context.GetMedicalServiceById(model.VisitSummary.ReferralMockToAdd.PrimaryMedicalServiceId.Value);
                ReferralMock medicalReferral = model.VisitSummary.ReferralMockToAdd;
                if (medicalService.IsPrimaryService)
                {
                    medicalReferral.PrimaryMedicalServiceId = medicalService.Id;
                    medicalReferral.PrimaryMedicalServiceName = medicalService.Name;
                }
                else
                {
                    medicalReferral.MinorMedicalServiceId = medicalService.Id;
                    medicalReferral.MinorMedicalServiceName = medicalService.Name;
                    MedicalService primaryService = _context.GetMedicalServices().Where(c => c.SubServices != null && c.SubServices.Contains(medicalService)).First();
                    medicalReferral.PrimaryMedicalServiceId = primaryService.Id;
                    medicalReferral.PrimaryMedicalServiceName = primaryService.Name;

                }
                //medicalReferral.Comment = model.ServiceReferralToAddComment;
                medicalReferral.ExpireDate = DateTime.Now.AddDays(medicalReferral.ServiceReferralDaysToExpire);
                medicalReferral.IssueDate = DateTime.Now;
                //medicalReferral.IssuedBy = medicalWorker;
                //medicalReferral.IssuedTo = VisitSummary.Patient;
                //_context.AddMedicalReferral(medicalReferral);
                //if (medicalReferral.IsModelvalid)
                //{
                if (VisitSummary.ReferralMocks == null)
                {
                    VisitSummary.ReferralMocks = new List<ReferralMock>();
                }
                TempData.Put<ViewMessage>(ViewMessage.MESSAGE_KEY, new ViewMessage() { Message = "Skierowanie zostało dodane!", MessageType = Enums.AlertMessageType.InfoMessage });
                VisitSummary.ReferralMocks.Add(medicalReferral);
                //model.VisitSummary = VisitSummary;
                //TempData[TEMP_VISIT] = TempVisit;
                TempData.Put<VisitSummaryModel>(TEMP_VISIT_SUMMARY, VisitSummary);
                //model.Visit.ExaminationReferrals.Add(medicalReferral);
                return RedirectToAction("CurrentVisit");
                //}
                //else
                //{
                //}
                //RedirectToAction();
                //}
            }
            else
            {
                TempData.Put<ViewMessage>(ViewMessage.MESSAGE_KEY, new ViewMessage() { Message = "Wypełnij poprawnie wszystkie pola!", MessageType = Enums.AlertMessageType.WarningMessage });
            }

            return RedirectToAction("CurrentVisit");

        }
        [HttpPost]
        public IActionResult RemoveExaminationReferral(CurrentVisitViewModel model)
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                if (model.ReferralToRemoveIndex >= 0)
                {
                    //List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                    //Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                    // Visit visit = _context.GetBookedVisitById(model.VisitId);
                    //if (TempData.ContainsKey(TEMP_VISIT))
                    //{
                    VisitSummary = TempData.Get<VisitSummaryModel>(TEMP_VISIT_SUMMARY);//TempData[TEMP_VISIT] as Visit;
                                                                                       //}

                    if (VisitSummary != null)
                    {
                        //_context.RemoveMedicalReferralById(model.MedicalReferralIdToRemove);

                        //int index = TempVisit.ExaminationReferrals   .FindIndex(c => c.Id == model.MedicalReferralIdToRemove);
                        VisitSummary.ReferralMocks.RemoveAt((int)(model.ReferralIndexToRemove));
                        TempData.Put<ViewMessage>(ViewMessage.MESSAGE_KEY, new ViewMessage() { Message = "Skierowanie zostało usunięte!", MessageType = Enums.AlertMessageType.WarningMessage });

                        //model.Visit = _context.GetBookedVisitById(model.VisitId);//VisitSummary;
                        TempData.Put<VisitSummaryModel>(TEMP_VISIT_SUMMARY, VisitSummary);
                        return RedirectToAction("CurrentVisit");
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
        public IActionResult AddService(CurrentVisitViewModel model)
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {

                //List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                //Visit visit = _context.GetVisitById(model.VisitId);//  todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                //if (TempData.ContainsKey(TEMP_VISIT))
                //{
                VisitSummary = TempData.Get<VisitSummaryModel>(TEMP_VISIT_SUMMARY);//TempData[TEMP_VISIT] as Visit;
                                                                                   //}

                if (VisitSummary != null)
                {
                    if (model.ServiceToAddId > 0)
                    {
                        MedicalService medicalService = _context.GetMedicalServiceById(model.ServiceToAddId);
                        if (medicalService != null)
                        {
                            if (VisitSummary.MinorMedicalServices == null)
                            {
                                VisitSummary.MinorMedicalServices = new List<ServiceMock>();
                            }
                            ServiceMock serviceMock = new ServiceMock(medicalService);
                            //{
                            //    Id = medicalService.Id,
                            //    Description = medicalService.Description,
                            //    IsPrimaryService = medicalService.IsPrimaryService,
                            //    Name = medicalService.Name,
                            //    PrimaryServiceId = medicalService.PrimaryServiceId,
                            //    RequireRefferal = medicalService.RequireRefferal,
                            //    StandardPrice = medicalService.StandardPrice,
                            //    VisitCategoryId = medicalService.VisitCategoryId,
                            //};
                            VisitSummary.MinorMedicalServices.Add(serviceMock);
                            //TempData[TEMP_VISIT] = TempVisit;
                            TempData.Put<ViewMessage>(ViewMessage.MESSAGE_KEY, new ViewMessage() { Message = "Usługa została dodana!", MessageType = Enums.AlertMessageType.InfoMessage });
                            TempData.Put<VisitSummaryModel>(TEMP_VISIT_SUMMARY, VisitSummary);

                            //model.AvailableMinorServices.Remove(medicalService);
                            // _context.UpdateVisit(visit);
                            return RedirectToAction("CurrentVisit");
                        }
                        else
                        {
                            TempData.Put<ViewMessage>(ViewMessage.MESSAGE_KEY, new ViewMessage() { Message = "Coś poszło nie tak!", MessageType = Enums.AlertMessageType.WarningMessage });
                        }
                    }
                    TempData.Put<VisitSummaryModel>(TEMP_VISIT_SUMMARY, VisitSummary);

                }

                return NotFound();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult ClearVisitData()
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                VisitSummary = TempData.Get<VisitSummaryModel>(TEMP_VISIT_SUMMARY);


                if (!string.IsNullOrWhiteSpace( VisitSummary.TestResult?.FilePath))
                {
                    _context.RemoveFile(VisitSummary.TestResult.FilePath, _hostEnvironment.WebRootPath);
                }
                //if (TempData.ContainsKey(TEMP_VISIT_SUMMARY))
                //{
                //    VisitSummary = TempData.Get<VisitSummaryModel>(TEMP_VISIT_SUMMARY);//TempData[TEMP_VISIT] as Visit;
                //}

                //Visit visit = _context.GetVisitById(visitId);//  todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                //if (visit != null)
                //{
                //    visit.ExaminatinoReferralsIds = null;
                //    visit.ExaminationReferrals = null;
                //    visit.MedicalHistory = null;
                //    visit.MedicalTestResult = null;
                //    visit.MedicalTestResultId = null;
                //    visit.MinorMedicalServices = null;
                //    visit.MinorMedicalServicesIds = null;
                //    visit.MinorServicesToVisits = null;
                //    visit.Prescription=null;
                //    visit.PrescriptionId = null;
                //    visit.RecommendationIds = null;
                //    visit.Recommendations = null;
                //    CurrentVisitViewModel model = new CurrentVisitViewModel(visit);
                //    return RedirectToAction("CurrentVisit");

                TempData.Put<ViewMessage>(ViewMessage.MESSAGE_KEY, new ViewMessage() { Message = "Dane zostały wyczyszczone!", MessageType = Enums.AlertMessageType.InfoMessage });
                TempData.Put<VisitSummaryModel>(TEMP_VISIT_SUMMARY, new VisitSummaryModel());
                return RedirectToAction("CurrentVisit");

            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult RemoveMedicineFromPrescription(CurrentVisitViewModel model)
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                //List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                //Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                //Visit visit = _context.GetBookedVisitById(model.VisitId);
                //if (TempData.ContainsKey(TEMP_VISIT))
                //{
                VisitSummary = TempData.Get<VisitSummaryModel>(TEMP_VISIT_SUMMARY);//TempData[TEMP_VISIT] as Visit;
                                                                                   //}


                if (VisitSummary != null)
                {
                    if (model.MedicineIndexToRemove >= 0)
                    {
                        //IssuedMedicine medicine = visit.Prescription.IssuedMedicines.First(c => c.Id == model.MedicineIndexToRemove);

                        VisitSummary.Medicines.RemoveAt((int)model.MedicineIndexToRemove);
                        TempData.Put<VisitSummaryModel>(TEMP_VISIT_SUMMARY, VisitSummary);
                        TempData.Put<ViewMessage>(ViewMessage.MESSAGE_KEY, new ViewMessage() { Message = "Lek został usunięty!", MessageType = Enums.AlertMessageType.InfoMessage });

                        //_context.RemoveIssuedMedicineById(model.MedicineIndexToRemove);
                        return RedirectToAction("CurrentVisit");
                    }
                }
                TempData.Put<ViewMessage>(ViewMessage.MESSAGE_KEY, new ViewMessage() { Message = "Coś poszło nie tak!", MessageType = Enums.AlertMessageType.WarningMessage });

                return NotFound();

            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult RemoveService(CurrentVisitViewModel model)
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                //List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                //Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                //Visit visit = _context.GetBookedVisitById(model.VisitId);
                VisitSummary = TempData.Get<VisitSummaryModel>(TEMP_VISIT_SUMMARY);//TempData[TEMP_VISIT] as Visit;

                if (VisitSummary != null)
                {
                    if (model.ServiceToRemoveIndex >= 0)
                    {
                        //ServiceMock medicalService = //VisitSummary.MinorMedicalServices.Where(c => c.Id == model.ServiceToRemoveIndex).FirstOrDefault();
                        //MedicalService medicalService = _context.GetMedicalServiceById(model.ServiceToAdd);
                        //if (medicalService != null)
                        //{
                            VisitSummary.MinorMedicalServices.RemoveAt(model.ServiceToRemoveIndex);
                            TempData.Put<VisitSummaryModel>(TEMP_VISIT_SUMMARY, VisitSummary);
                            TempData.Put<ViewMessage>(ViewMessage.MESSAGE_KEY, new ViewMessage() { Message = "Usługa została usunięta!", MessageType = Enums.AlertMessageType.InfoMessage });

                            return RedirectToAction("CurrentVisit");
                        //}
                    }
                }
                TempData.Put<VisitSummaryModel>(TEMP_VISIT_SUMMARY, VisitSummary);
                TempData.Put<ViewMessage>(ViewMessage.MESSAGE_KEY, new ViewMessage() { Message = "Coś poszło nie tak!", MessageType = Enums.AlertMessageType.WarningMessage });

                return NotFound();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult RemovePrescription(CurrentVisitViewModel model)
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                //List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                //Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                //Visit visit = _context.GetBookedVisitById(model.VisitId);
                VisitSummary = TempData.Get<VisitSummaryModel>(TEMP_VISIT_SUMMARY);//TempData[TEMP_VISIT] as Visit;

                if (VisitSummary != null)
                {
                    //if (model.PrescriptionIdToRemove > 0)
                    //{
                    //    Prescription prescription = _context.GetPrescriptionById(model.PrescriptionIdToRemove);
                    //    if (prescription != null)
                    //    {
                    //long id = visit.Prescription.Id;
                    VisitSummary.PrescriptionToAdd = null;
                    TempData.Put<VisitSummaryModel>(TEMP_VISIT_SUMMARY, VisitSummary);
                    TempData.Put<ViewMessage>(ViewMessage.MESSAGE_KEY, new ViewMessage() { Message = "Recepta została usunięta!", MessageType = Enums.AlertMessageType.InfoMessage });

                    //_context.RemovePrescriptionById(model.PrescriptionIdToRemove);
                    return RedirectToAction("CurrentVisit");
                    //    }
                    //}
                }
                TempData.Put<ViewMessage>(ViewMessage.MESSAGE_KEY, new ViewMessage() { Message = "Coś poszło nie tak!", MessageType = Enums.AlertMessageType.WarningMessage });

                return NotFound();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public IActionResult Reviews()
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                MedicalWorker medicalWorker = _context.GetMedicalWorkerById(HttpContext.User.GetUserId().Value);
                List<VisitReview> visitReviews = _context.GetReviewsByMedicalWorkerId(medicalWorker.Id);
                medicalWorker.VisitReviews = visitReviews;
                ReviewsViewModel model = new ReviewsViewModel(medicalWorker);
                model.UserName = _loggedUser.Person.FullName;
                model.VisitReviews.ForEach(c => c.Reviewer.Person = _context.GetPersonById(c.Reviewer.PersonId.Value));
                return View(model);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpGet]
        public IActionResult Locations()
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                List<Location> locations = _context.GetLocations();
                Models.LocationsViewModel model = new Models.LocationsViewModel(locations);
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpGet]
        public IActionResult VisitDetails(int id)
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);
            MedicalWorker medicalWorker = _context.GetMedicalWorkerByUserId(GetCurrentUserId().Value); ;

            if (_loggedUser != null)
            {
                Visit visit = _context.GetVisitById(id); // GetFutureVisitsByMedicalWorkerId(_medicalWorker.Id).Where(c => c.Id == id).FirstOrDefault();
                                                         //if (visit.MedicalWorkerId == medicalWorker.Id)
                                                         //{
                                                         //}
                if (visit.Patient != null)
                {
                    visit.Patient.MedicalPackage = _context.GetMedicalPackageById(visit.Patient.MedicalPackageId.Value);
                }
                CurrentVisitViewModel model = new CurrentVisitViewModel();
                model.Visit = visit;
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
                //}
                //else
                //{
                //    return NotFound();
                //}
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public IActionResult PatientDetails(long id)
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                Patient patient = _context.GetPatientById(id);
                patient.MedicalReferrals = _context.GetMedicalReferralsByPatientId(id);
                patient.TestsResults = _context.GetMedicalTestResultsByPatientId(id);
                patient.Prescriptions = _context.GetPrescriptionsByPatientId(id);
                patient.AllVisits = _context.GetHistoricalVisitsByPatientId(id);

                MedicalWorkerArea.Models.PatientViewModel model = new MedicalWorkerArea.Models.PatientViewModel(patient)
                {
                    UserName = _loggedUser.Person.FullName
                };

                return View(model);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public IActionResult MedicalWorkerDetails(int id)
        {
            _loggedUser = _context.GetUserById(HttpContext.User.GetUserId().Value);

            if (_loggedUser != null)
            {
                MedicalWorker medicalWorker = _context.GetMedicalWorkerById(id);
                //Models.LocationsViewModel model = new Models.LocationsViewModel(locations);
                Models.MedicalWorkerViewModel model = new Models.MedicalWorkerViewModel();
                model.MedicalWorker = medicalWorker;
                model.MedicalWorker.Person = _context.GetPersonById(medicalWorker.PersonId.Value);
                model.MedicalWorker.VisitReviews = _context.GetReviewsByMedicalWorkerId(model.MedicalWorker.Id);
                //model.MedicalWorker.MedicalServices = model.MedicalWorker.MedicalServiceIds.Select(c => _context.GetMedicalServiceById(c)).ToList();
                model.UserName = _loggedUser.Person.FullName;

                return View(model);
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
