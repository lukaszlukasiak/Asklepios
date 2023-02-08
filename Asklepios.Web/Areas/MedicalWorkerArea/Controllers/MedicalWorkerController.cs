using Asklepios.Core.Enums;
using Asklepios.Core.Models;
using Asklepios.Data.Interfaces;
//using Asklepios.Web.Areas.CustomerServiceArea.Models;
using Asklepios.Web.Areas.MedicalWorkerArea.Models;
using Asklepios.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Asklepios.Web.Areas.MedicalWorkerArea.Controllers
{
    [Area("MedicalWorkerArea")]

    public class MedicalWorkerController : Controller
    {
        //private readonly ILogger<MedicalWorkerController> _logger;
        IWebHostEnvironment _hostEnvironment { get; set; }

        //public MedicalWorkerController(ILogger<MedicalWorkerController> logger)
        //{
        //    _logger = logger;
        //}
        IMedicalWorkerModuleRepository _context;
        public MedicalWorkerController(IMedicalWorkerModuleRepository context, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;

            _context = context;
        }
        private  static User _loggedUser { get; set; }
        private  static Person _person { get; set; }
        private  static MedicalWorker _medicalWorker { get; set; }
        private  static long _currentVisitId;
        private  static long CurrentVisitId
        {
            get
            {
                return _currentVisitId;
            }
            set
            {
                _currentVisitId = value;
                if (_currentVisitId == -1)
                {
                    TempVisit = null;
                }
            }
        }
        private static Visit TempVisit { get; set; }

        internal void LogOut()
        {
            _loggedUser = null;
            _person = null;
            _medicalWorker = null;

        }

        //private static User _loggedUser { get; set; }
        //private static Person _person { get; set; }
        //private static MedicalWorker _medicalWorker { get; set; }
        //private static long _currentVisitId;
        //private static long CurrentVisitId 
        //{
        //    get
        //    {
        //        return _currentVisitId;
        //    }
        //    set
        //    {
        //        _currentVisitId = value;
        //        if (_currentVisitId==-1)
        //        {
        //            TempVisit = null;
        //        }
        //    }
        //}
        //public IActionResult LogOut()
        //{
        //    return RedirectToAction("Index", "Home", new { area = "HomeArea" });
        //}
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public IActionResult Index()
        {
            if (_loggedUser == null)
            {
                if (TempData.ContainsKey("User") == true)
                {
                    User user = JsonConvert.DeserializeObject<User>((string)TempData["User"]);
                    _loggedUser = user;
                    _loggedUser.Person = _context.GetPersonById(_loggedUser.PersonId.Value);
                    _medicalWorker = _context.GetMedicalWorkerByPersonId(_loggedUser.Id);
                    ViewData["UserName"] = _loggedUser.Person.FullName;
                }
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
            if (_loggedUser != null)
            {
                CurrentVisitId = id;
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
            if (_loggedUser != null)
            {
                if (ModelState.IsValid)
                {
                }
                if (CurrentVisitId > 0)
                {
                    if (TempVisit==null)
                    {
                        Visit visit = _context.GetBookedVisitById(CurrentVisitId);
                        
                        if (visit == null)
                        {
                            return NotFound();
                        }

                        visit.Patient = _context.GetPatientById(visit.PatientId.Value);
                        visit.Patient.MedicalPackage = _context.GetMedicalPackageById(visit.Patient.MedicalPackageId.Value);

                        visit.MedicalWorker = _medicalWorker;
                        visit.Location = _context.GetLocationById(visit.LocationId.Value);
                        visit.MedicalRoom = _context.GetMedicalRoomById(visit.MedicalRoomId.Value);
                        visit.VisitCategory = _context.GetVisitCategoryById(visit.VisitCategoryId.Value);
                        TempVisit = visit;

                    }

                    //if (visit.VisitStatus != Core.Enums.VisitStatus.AvailableNotBooked)
                    //{
                    CurrentVisitViewModel model = new CurrentVisitViewModel(TempVisit);
                    List<MedicalService> servicesForReferrals = _context.GetMedicalServices().Where(c => c.RequireRefferal == true).ToList();
                    model.MedicalServicesForReferrals = servicesForReferrals;
                    model.UserName = _loggedUser.Person.FullName;
                    //model.TempVisit = TempVisit;
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
        public IActionResult UserProfile()
        {
            if (_loggedUser != null)
            {
                //MedicalWorker medicalWorker = _context.GetMedicalWorkerByUserId(User.);
                MedicalWorkerViewModel model = new MedicalWorkerViewModel
                {
                    MedicalWorker = _medicalWorker
                };
                model.MedicalWorker.User = _context.GetUserById(_medicalWorker.UserId.Value);
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
            if (_loggedUser != null)
            {
                switch (model.SubmitMode)
                {
                    case SubmitMode.AddReferral:
                        //AddExaminationReferral(model);
                        for (int i = ModelState.Keys.Count() - 1; i >= 0; i--)
                        {
                            string item = ModelState.Keys.ElementAt(i);
                            if (item.Contains("Referral") || item.Contains("MedicalServiceToAddId"))
                            {

                            }
                            else
                            {
                                //ModelState[item].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
                                ModelState.Remove(item);
                            }
                        }

                        break;
                    case SubmitMode.AddPrescription:
                        //string[] keys = ModelState.Keys;
                        for (int i = ModelState.Keys.Count() - 1; i >= 0; i--)
                        {
                            string item = ModelState.Keys.ElementAt(i);
                            if (item.Contains("Prescription") || item.Contains("Medicine"))
                            {

                            }
                            else
                            {
                                //ModelState[item].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
                                ModelState.Remove(item);
                            }
                        }
                        //foreach (string item in ModelState.Keys)
                        //{
                        //    if (item.Contains("Prescription") || item.Contains("Medicine"))
                        //    {

                        //    }
                        //    else
                        //    {
                        //        ModelState[item].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
                        //        ModelState.Remove[item];
                        //    }
                        //}
                        break;
                    case SubmitMode.AddTestRestult:
                        for (int i = ModelState.Keys.Count() - 1; i >= 0; i--)
                        {
                            string item = ModelState.Keys.ElementAt(i);
                            if (item.Contains("MedicalTest") || item.Contains("Visit.Id"))
                            {

                            }
                            else
                            {
                                //ModelState[item].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
                                ModelState.Remove(item);
                            }
                        }

                        break;
                    case SubmitMode.AddMedicineToPrescription:
                        for (int i = ModelState.Keys.Count() - 1; i >= 0; i--)
                        {
                            string item = ModelState.Keys.ElementAt(i);
                            if (item.Contains("Medicine"))
                            {

                            }
                            else
                            {
                                ModelState.Remove(item);
                            }
                        }
                        break;
                    case SubmitMode.Other:
                        break;
                    default:
                        break;
                }
                switch (model.SubmitMode)
                {
                    case SubmitMode.AddReferral:
                        if (ModelState.IsValid)
                        {

                            AddExaminationReferral(model);
                        }
                        break;
                    case SubmitMode.AddPrescription:
                        if (ModelState.IsValid)
                        {
                            AddPrescription(model);
                        }

                        break;
                    case SubmitMode.AddTestRestult:
                        if (ModelState.IsValid)
                        {
                            AddTestResult(model);
                        }

                        break;
                    case SubmitMode.AddMedicineToPrescription:
                        if (ModelState.IsValid)
                        {
                            AddMedicineToPrescription(model);
                        }

                        break;
                    case SubmitMode.Other:
                        break;
                    default:
                        break;
                }


                Visit visit = _context.GetBookedVisitById(CurrentVisitId);
                if (visit == null)
                {
                    return NotFound();
                }
                model.Visit = visit;
                List<MedicalService> servicesForReferrals = _context.GetMedicalServices().Where(c => c.RequireRefferal == true).ToList();
                model.MedicalServicesForReferrals = servicesForReferrals;
                model.UserName = _loggedUser.Person.FullName;

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
            //List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
            //Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
            //Visit visit = _context.GetBookedVisitById(model.VisitId);

            if (TempVisit != null)
            {
                //_context.AddMedicine(model.IssuedMedicineToAdd);
                if (TempVisit.Prescription==null)
                {
                    TempVisit.Prescription = new Prescription();
                    TempVisit.Prescription.IssuedById = model.MedicalWorkerId;
                    TempVisit.Prescription.IssuedToId = model.PatientId;
                    TempVisit.Prescription.IssueDate = DateTime.Now;
                    TempVisit.Prescription.ExpirationDate = DateTime.Now.AddDays(model.PrescriptionDaysToExpire);
                }
                TempVisit.Prescription.IssuedMedicines.Add(model.IssuedMedicineToAdd);

                //List<Prescription> prescriptions = _context.GetPrescriptions();
                //visit.Prescription = model.PrescriptionToAdd;
                //_context.UpdatePrescription(visit.Prescription);
                //model.IssuedMedicineToAdd = null;
               // model.IssuedMedicineToAdd = new IssuedMedicine();
                //ModelState.Clear();
                return RedirectToAction("CurrentVisit");

            }
            return NotFound();

        }
        [HttpPost]
        public IActionResult AddTestResult(CurrentVisitViewModel model)
        {
            if (_loggedUser != null)
            {
                //List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                //Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                //Visit visit = _context.GetBookedVisitById(model.VisitId);

                if (TempVisit != null)
                {
                    MedicalService medicalService = _context.GetMedicalServiceById(model.MedicalTestServiceId);
                    if (model.MedicalTestFile != null)
                    {
                        //_context.UpdateTestResultFile(model.MedicalTestFile, visit, _hostEnvironment.WebRootPath );

                        MedicalTestResult medicalTestResult = new MedicalTestResult
                        {
                            VisitId = TempVisit.Id,
                            ExamDate = TempVisit.DateTimeSince,
                            MedicalWorkerId = TempVisit.MedicalWorkerId,
                            PatientId = TempVisit.PatientId,
                            MedicalService = medicalService,
                            UploadDate=DateTime.Now,
                            //PdfDocument=model.MedicalTestFile.,

                        };
                        MemoryStream stream = new MemoryStream();

                        model.MedicalTestFile.CopyTo(stream);
                        medicalTestResult.Document= stream.ToArray();

                        // medicalTestResult.Document = _context.GetDocument(medicalTestResult.DocumentPath, _hostEnvironment.WebRootPath);
                        //_context.AddMedicalTestResult(medicalTestResult, model.MedicalTestFile, _hostEnvironment.WebRootPath);
                        TempVisit.MedicalTestResult = medicalTestResult;

                        //_context.UpdatePersonImage(model.Person.ImageFile, model.Person, _hostEnvironment.WebRootPath);
                    }

                    ModelState.Clear();
                }

                return RedirectToAction("CurrentVisit");

            }
            return NotFound();
        }

        public IActionResult AddPrescription(CurrentVisitViewModel model)
        {
            if (_loggedUser != null)
            {
                //List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                //Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                //Visit visit = _context.GetBookedVisitById(model.VisitId);

                if (TempVisit != null)
                {
                    Prescription prescription = new Prescription
                    {
                        AccessCode = model.PrescriptionToAdd.AccessCode,
                        ExpirationDate = DateTimeOffset.Now.AddDays(model.PrescriptionDaysToExpire),// model.PrescriptionToAdd.ExpirationDate;
                        IdentificationCode = model.PrescriptionToAdd.IdentificationCode,
                        IssueDate = DateTime.Now,
                        Visit = TempVisit,
                        IssuedBy = TempVisit.MedicalWorker,
                        IssuedTo = TempVisit.Patient
                    };
                    prescription.IssuedMedicines.Add(model.IssuedMedicineToAdd);
                   // _context.AddMedicine(model.IssuedMedicineToAdd);
                    //prescription.IssuedMedicines.Add(model.IssuedMedicineToAdd);

                    //_context.AddPrescription(prescription);

                    TempVisit.Prescription = prescription;// model.PrescriptionToAdd;
                    model.IssuedMedicineToAdd = new IssuedMedicine();
                    ModelState.Clear();
                    return RedirectToAction("CurrentVisit");

                }
            }
            return NotFound();
        }
        public IActionResult DownloadTempTestResults()
        {
            byte[] bytes = TempVisit.MedicalTestResult.Document;

            return File(bytes, "application/octet-stream", System.IO.Path.GetFileName("wynik"));

        }

        [HttpPost]
        public IActionResult Schedule(ScheduleViewModel model)
        {
            if (_loggedUser != null)
            {
                model.MedicalWorker = _medicalWorker;

                //ScheduleViewModel model = new ScheduleViewModel(_medicalWorker);
                if (model.SelectedDate != null)
                {
                    //model.MedicalWorker.AllVisits.AddRange(visits);//.Where(c => c.DateTimeSince.Date == model.SelectedDate.Value.Date).ToList();
                }
                model.UserName = _loggedUser.Person.FullName;
                model.AllForthcomingVisits = _context.GetFutureVisitsByMedicalWorkerId(_medicalWorker.Id);
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
            if (_loggedUser != null)
            {
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
        public IActionResult DownloadTestResults(string id)
        {
            if (_loggedUser != null)
            {

                //Build the File Path.
                if (long.TryParse(id, out long idL))
                {

                    Visit visit = _context.GetBookedVisitById(CurrentVisitId);
                    //_medicalWorker.AllVisits = _context.GetHistoricalVisitsByMedicalWorkerId(_medicalWorker.Id);
                    MedicalTestResult testResult = _medicalWorker.AllVisits.Where(c => c.MedicalTestResult != null && c.MedicalTestResult.Id == idL).FirstOrDefault().MedicalTestResult;

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

                    //byte[] bytes =  //_context.GetTestResultById.Where(c => c.Id == idL).FirstOrDefault()?.PdfDocument;//pdf.  System.IO.File.ReadAllBytes(pdf);

                    //Send the File to Download.

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
        [HttpPost]
        public IActionResult RemoveTestResult(CurrentVisitViewModel model)
        {
            if (_loggedUser != null)
            {
                long id = model.MedicalTestFileIdToRemove;
                //Build the File Path.

                Visit visit = _context.GetBookedVisitById(CurrentVisitId);
                _medicalWorker.AllVisits.AddRange(_context.GetHistoricalVisitsByMedicalWorkerId(_medicalWorker.Id));
                MedicalTestResult testResult = _medicalWorker.AllVisits.Where(c => c.MedicalTestResult != null && c.MedicalTestResult.Id == id).FirstOrDefault().MedicalTestResult;

                if (testResult != null)
                {
                    _context.RemoveTestResult(testResult.Id, visit.Id, _hostEnvironment.WebRootPath);
                    return RedirectToAction("CurrentVisit");
                }
                else
                {
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
        public IActionResult History(HistoricalVisitsViewModel model)
        {
            if (_loggedUser != null)
            {
                model.HistoricalVisits = _context.GetHistoricalVisitsByMedicalWorkerId(_medicalWorker.Id);
                model.MedicalWorker = _medicalWorker;
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
            if (_loggedUser != null)
            {
                HistoricalVisitsViewModel model = new HistoricalVisitsViewModel(_medicalWorker)
                {
                    UserName = _loggedUser.Person.FullName
                };
                model.HistoricalVisits = _context.GetHistoricalVisitsByMedicalWorkerId(_medicalWorker.Id);
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
                _medicalWorker.User = _context.GetUserById(_medicalWorker.Id);
                ContactMessageViewModel model = new ContactMessageViewModel(_medicalWorker);
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
            if (_loggedUser != null)
            {

                ContactMessageViewModel modelP = new ContactMessageViewModel(_medicalWorker);
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
            if (_loggedUser != null)
            {
                List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                if (visit != null)
                {
                    visit.VisitStatus = Core.Enums.VisitStatus.Finished;
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
                    CurrentVisitId = -1;
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
            if (_loggedUser != null)
            {
                //List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                //Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                Visit visit = _context.GetBookedVisitById(model.VisitId);

                if (visit != null)
                {
                    visit.VisitStatus = Core.Enums.VisitStatus.NotHeldAbsentPatient;
                    CurrentVisitId = -1;
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
            if (_loggedUser != null)
            {
                //List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                //Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                Visit visit = _context.GetBookedVisitById(model.VisitId);

                if (visit != null)
                {
                    visit.VisitStatus = Core.Enums.VisitStatus.NotHeldOther;
                    CurrentVisitId = -1;
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

            if (_loggedUser != null)
            {
                //List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                //Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                //Visit visit = _context.GetBookedVisitById(model.VisitId);

                if (TempVisit != null)
                {
                    TempVisit.MedicalHistory = model.Visit.MedicalHistory;
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
            if (_loggedUser != null)
            {
                //List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                //Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                //Visit visit = _context.GetBookedVisitById(model.VisitId);

                if (TempVisit != null)
                {
                    model.RecommendationToAdd.VisitId = TempVisit.Id;
                    TempVisit.Recommendations.Add(model.RecommendationToAdd);
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
            if (_loggedUser != null)
            {
                //List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                //Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                //Visit visit = _context.GetBookedVisitById(model.VisitId);

                if (TempVisit != null)
                {
                    //_context.DeleteRecommendation(model.RecommendationToRemove.Id);
                    //Recommendation recommendation = visit.Recommendations.Where(c => c.Id == model.RecommendationToRemove.Id).FirstOrDefault();
                    long index = model.RecommendationToRemove.Id;//visit.Recommendations.FindIndex(c => c.Id == model.RecommendationToRemove.Id);

                    if (index > -1)
                    {
                        TempVisit.Recommendations.RemoveAt((int)index);
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
        //public IActionResult AddExaminationReferral(CurrentVisitViewModel model)
        public IActionResult AddExaminationReferral(CurrentVisitViewModel model)
        {
            //if (_loggedUser != null)
            //{
            //    if (!ModelState.IsValid)
            //    {
            //        return RedirectToAction("CurrentVisit");
            //    }
            if (model.MedicalServiceToAddId > 0)
            {
                //List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                //Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                //Visit visit = _context.GetBookedVisitById(model.VisitId);
                

                if (TempVisit != null)
                {
                    if (model.IsMedicalReferralAddingOK)
                    {
                        MedicalService medicalService = _context.GetMedicalServiceById(model.MedicalServiceToAddId);
                        MedicalReferral medicalReferral = new MedicalReferral();
                        if (medicalService.IsPrimaryService)
                        {

                            medicalReferral.PrimaryMedicalService = medicalService;
                        }
                        else
                        {
                            medicalReferral.MinorMedicalService = medicalService;
                            medicalReferral.PrimaryMedicalService = _context.GetMedicalServices().Where(c => c.SubServices != null && c.SubServices.Contains(medicalService)).First();

                        }
                        medicalReferral.Comment = model.MedicalReferralToAddComment;
                        medicalReferral.ExpireDate = DateTimeOffset.Now.AddDays(model.MedicalReferralDaysToExpire);
                        medicalReferral.IssueDate = DateTimeOffset.Now;
                        medicalReferral.IssuedBy = _medicalWorker;
                        medicalReferral.IssuedTo = TempVisit.Patient;
                        //_context.AddMedicalReferral(medicalReferral);
                        TempVisit.ExaminationReferrals.Add(medicalReferral);
                        model.Visit = TempVisit;
                        //model.Visit.ExaminationReferrals.Add(medicalReferral);
                        return RedirectToAction("CurrentVisit");

                    }
                    else
                    {
                    }
                    //RedirectToAction();
                }
            }
            return NotFound();

        }
        [HttpPost]
        public IActionResult RemoveExaminationReferral(CurrentVisitViewModel model)
        {
            if (_loggedUser != null)
            {
                if (model.MedicalReferralIdToRemove >= 0)
                {
                    //List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                    //Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                   // Visit visit = _context.GetBookedVisitById(model.VisitId);

                    if (TempVisit != null)
                    {
                        //_context.RemoveMedicalReferralById(model.MedicalReferralIdToRemove);
                        
                        //int index = TempVisit.ExaminationReferrals   .FindIndex(c => c.Id == model.MedicalReferralIdToRemove);
                        TempVisit.ExaminationReferrals.RemoveAt((int)(model.MedicalReferralIdToRemove));
                        model.Visit = TempVisit;
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
            if (_loggedUser != null)
            {
                //List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                //Visit visit = _context.GetVisitById(model.VisitId);//  todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                if (TempVisit != null)
                {
                    if (model.ServiceToAdd > 0)
                    {
                        MedicalService medicalService = _context.GetMedicalServiceById(model.ServiceToAdd);
                        if (medicalService != null)
                        {
                            if (TempVisit.MinorMedicalServices == null)
                            {
                                TempVisit.MinorMedicalServices = new List<MedicalService>();
                            }
                            TempVisit.MinorMedicalServices.Add(medicalService);
                            //model.AvailableMinorServices.Remove(medicalService);
                           // _context.UpdateVisit(visit);
                            return RedirectToAction("CurrentVisit");
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
        public IActionResult ClearVisitData(long visitId)
        {
            if (_loggedUser != null)
            {
                Visit visit = _context.GetVisitById(visitId);//  todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                if (visit != null)
                {
                    visit.ExaminatinoReferralsIds = null;
                    visit.ExaminationReferrals = null;
                    visit.MedicalHistory = null;
                    visit.MedicalTestResult = null;
                    visit.MedicalTestResultId = null;
                    visit.MinorMedicalServices = null;
                    visit.MinorMedicalServicesIds = null;
                    visit.MinorServicesToVisits = null;
                    visit.Prescription=null;
                    visit.PrescriptionId = null;
                    visit.RecommendationIds = null;
                    visit.Recommendations = null;
                    CurrentVisitViewModel model = new CurrentVisitViewModel(visit);
                    return RedirectToAction("CurrentVisit");

                }
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult RemoveMedicineFromPrescription(CurrentVisitViewModel model)
        {
            if (_loggedUser != null)
            {
                //List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                //Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                //Visit visit = _context.GetBookedVisitById(model.VisitId);


                if (TempVisit != null)
                {
                    if (model.MedicineIndexToRemove >= 0)
                    {
                        //IssuedMedicine medicine = visit.Prescription.IssuedMedicines.First(c => c.Id == model.MedicineIndexToRemove);

                        TempVisit.Prescription.IssuedMedicines.RemoveAt((int)model.MedicineIndexToRemove);

                        //_context.RemoveIssuedMedicineById(model.MedicineIndexToRemove);
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
        public IActionResult RemoveService(CurrentVisitViewModel model)
        {
            if (_loggedUser != null)
            {
                //List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                //Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                //Visit visit = _context.GetBookedVisitById(model.VisitId);

                if (TempVisit != null)
                {
                    if (model.ServiceToRemove > 0)
                    {
                        MedicalService medicalService = TempVisit.MinorMedicalServices.Where(c => c.Id == model.ServiceToRemove).FirstOrDefault();                                    
                            //MedicalService medicalService = _context.GetMedicalServiceById(model.ServiceToAdd);
                        if (medicalService != null)
                        {
                            TempVisit.MinorMedicalServices.Remove(medicalService);
                            return RedirectToAction("CurrentVisit");
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
        public IActionResult RemovePrescription(CurrentVisitViewModel model)
        {
            if (_loggedUser != null)
            {
                //List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                //Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                //Visit visit = _context.GetBookedVisitById(model.VisitId);

                if (TempVisit != null)
                {
                    //if (model.PrescriptionIdToRemove > 0)
                    //{
                    //    Prescription prescription = _context.GetPrescriptionById(model.PrescriptionIdToRemove);
                    //    if (prescription != null)
                    //    {
                    //long id = visit.Prescription.Id;
                    TempVisit.Prescription = null;
                    //_context.RemovePrescriptionById(model.PrescriptionIdToRemove);
                    return RedirectToAction("CurrentVisit");
                    //    }
                    //}
                }
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
            if (_loggedUser != null)
            {
                List<VisitReview> visitReviews = _context.GetReviewsByMedicalWorkerId(_medicalWorker.Id);
                _medicalWorker.VisitReviews = visitReviews;
                ReviewsViewModel model = new ReviewsViewModel(_medicalWorker);
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
            if (_loggedUser != null)
            {
                //Visit visit = _context.GetHistoricalVisitById(id);
                //if (visit == null)
                //{
                Visit visit = _context.GetVisitById(id); // GetFutureVisitsByMedicalWorkerId(_medicalWorker.Id).Where(c => c.Id == id).FirstOrDefault();
                if (visit.MedicalWorkerId == _medicalWorker.Id)
                {
                    //}
                    if (visit.Patient != null)
                    {
                        visit.Patient.MedicalPackage = _context.GetMedicalPackageById(visit.Patient.MedicalPackageId.Value);
                    }
                    CurrentVisitViewModel model = new CurrentVisitViewModel();
                    model.Visit = visit;
                    model.UserName = _loggedUser.Person.FullName;
                    //model.Visit.MedicalRoom = _context.GetMedicalRoomById(model.Visit.MedicalRoomId.Value);
                    //model.Visit.VisitCategory = _context.GetVisitCategoryById(model.Visit.VisitCategoryId.Value);
                    //model.Visit.PrimaryService = _context.GetMedicalServiceById(model.Visit.PrimaryServiceId.Value);
                    // model.Visit.Patient.MedicalPackage = _context.GetMedicalPackageById(model.Visit.Patient.MedicalPackageId);
                    //Models.LocationsViewModel model = new Models.LocationsViewModel(locations);
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
        [HttpGet]
        public IActionResult PatientDetails(int id)
        {
            if (_loggedUser != null)
            {
                Patient patient = _context.GetPatientById(id);
                //patient.NFZUnit = _context.GetNFZUnitById(patient.NFZUnitId.Value);
                //patient.MedicalPackage = _context.GetMedicalPackageById(patient.MedicalPackageId.Value);

                MedicalWorkerArea.Models.PatientViewModel model = new MedicalWorkerArea.Models.PatientViewModel(patient)
                {
                    UserName = _loggedUser.Person.FullName
                };

                //patient.HistoricalVisits
                //Models.LocationsViewModel model = new Models.LocationsViewModel(locations);
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
