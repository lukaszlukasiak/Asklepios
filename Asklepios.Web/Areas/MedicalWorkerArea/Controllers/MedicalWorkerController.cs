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

        private static User _loggedUser { get; set; }
        private static Person _person { get; set; }
        private static MedicalWorker _medicalWorker { get; set; }
        private static long CurrentVisitId { get; set; }

        internal static void LogOut()
        {
            _loggedUser = null;
            _person = null;
            _medicalWorker = null;

        }
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
                    _loggedUser.Person = _context.GetPersonById(_loggedUser.PersonId);
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
                _medicalWorker.FutureVisits = _context.GetFutureVisitsByMedicalWorkerId(_medicalWorker.Id);

                DashboardViewModel model = new DashboardViewModel(_medicalWorker);
                model.UserName = _loggedUser.Person.FullName;
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
                    Visit visit = _context.GetBookedVisitById(CurrentVisitId);
                    if (visit == null)
                    {
                        return NotFound();
                    }
                    //if (visit.VisitStatus != Core.Enums.VisitStatus.AvailableNotBooked)
                    //{
                    CurrentVisitViewModel model = new CurrentVisitViewModel(visit);
                    List<MedicalService> servicesForReferrals = _context.GetMedicalServices().Where(c => c.RequireRefferal == true).ToList();
                    model.MedicalServicesForReferrals = servicesForReferrals;
                    model.UserName = _loggedUser.Person.FullName;

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
                MedicalWorkerViewModel model = new MedicalWorkerViewModel();
                model.MedicalWorker = _medicalWorker;
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

        private void AddMedicineToPrescription(CurrentVisitViewModel model)
        {
            List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
            Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
            if (visit != null)
            {
                _context.AddMedicine(model.IssuedMedicineToAdd);
                visit.Prescription.IssuedMedicines.Add(model.IssuedMedicineToAdd);

                List<Prescription> prescriptions = _context.GetPrescriptions();
                //visit.Prescription = model.PrescriptionToAdd;
                //_context.UpdatePrescription(visit.Prescription);
                //model.IssuedMedicineToAdd = null;
                model.IssuedMedicineToAdd = new IssuedMedicine();
                ModelState.Clear();
            }

        }
        [HttpPost]
        public IActionResult AddTestResult(CurrentVisitViewModel model)
        {
            if (_loggedUser != null)
            {
                List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();

                if (visit != null)
                {
                    MedicalService medicalService = _context.GetMedicalServiceById(model.MedicalTestServiceId);
                    if (model.MedicalTestFile != null)
                    {
                        //_context.UpdateTestResultFile(model.MedicalTestFile, visit, _hostEnvironment.WebRootPath );

                        MedicalTestResult medicalTestResult = new MedicalTestResult
                        {
                            Visit = visit,
                            ExamDate = DateTimeOffset.Now,
                            MedicalWorker = visit.MedicalWorker,
                            Patient = visit.Patient,
                            MedicalService = medicalService,

                            //PdfDocument=model.MedicalTestFile.,

                        };
                        medicalTestResult.Document = _context.GetDocument(medicalTestResult.DocumentPath, _hostEnvironment.WebRootPath);
                        _context.AddMedicalTestResult(medicalTestResult, model.MedicalTestFile, _hostEnvironment.WebRootPath);
                        visit.MedicalResult = medicalTestResult;

                        //_context.UpdatePersonImage(model.Person.ImageFile, model.Person, _hostEnvironment.WebRootPath);
                    }

                    //prescription.IssuedMedicines.Add(model.IssuedMedicineToAdd);
                    //_context.AddMedicine(model.IssuedMedicineToAdd);
                    ////prescription.IssuedMedicines.Add(model.IssuedMedicineToAdd);

                    //_context.AddPrescription(prescription);

                    //visit.Prescription = prescription;// model.PrescriptionToAdd;
                    //model.IssuedMedicineToAdd = new IssuedMedicine();
                    ModelState.Clear();
                }

                return RedirectToAction("CurrentVisit");

            }
            return NotFound();
        }

        private void AddPrescription(CurrentVisitViewModel model)
        {
            if (_loggedUser != null)
            {
                List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                if (visit != null)
                {
                    Prescription prescription = new Prescription
                    {
                        AccessCode = model.PrescriptionToAdd.AccessCode,
                        ExpirationDate = DateTimeOffset.Now.AddDays(model.PrescriptionDaysToExpire),// model.PrescriptionToAdd.ExpirationDate;
                        IdentificationCode = model.PrescriptionToAdd.IdentificationCode,
                        IssueDate = model.PrescriptionToAdd.IssueDate,
                        Visit = visit,
                        IssuedBy = visit.MedicalWorker,
                        IssuedTo = visit.Patient
                    };
                    prescription.IssuedMedicines.Add(model.IssuedMedicineToAdd);
                    _context.AddMedicine(model.IssuedMedicineToAdd);
                    //prescription.IssuedMedicines.Add(model.IssuedMedicineToAdd);

                    _context.AddPrescription(prescription);

                    visit.Prescription = prescription;// model.PrescriptionToAdd;
                    model.IssuedMedicineToAdd = new IssuedMedicine();
                    ModelState.Clear();
                }




            }
        }

        [HttpPost]
        public IActionResult Schedule(ScheduleViewModel model)
        {
            if (_loggedUser != null)
            {
                List<Visit> visits = _context.GetFutureVisitsByMedicalWorkerId(_medicalWorker.Id);
                model.MedicalWorker = _medicalWorker;

                //ScheduleViewModel model = new ScheduleViewModel(_medicalWorker);
                if (model.SelectedDate != null)
                {
                    model.MedicalWorker.FutureVisits = visits.Where(c => c.DateTimeSince.Date == model.SelectedDate.Value.Date).ToList();
                }
                model.UserName = _loggedUser.Person.FullName;

                //model.SelectedDate=
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
                _medicalWorker.FutureVisits = _context.GetFutureVisitsByMedicalWorkerId(_medicalWorker.Id);
                ScheduleViewModel model = new ScheduleViewModel(_medicalWorker);
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
                    _medicalWorker.PastVisits = _context.GetHistoricalVisitsByMedicalWorkerId(_medicalWorker.Id);
                    MedicalTestResult testResult = _medicalWorker.AllVisits.Where(c => c.MedicalResult != null && c.MedicalResult.Id == idL).FirstOrDefault().MedicalResult;

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
                _medicalWorker.PastVisits = _context.GetHistoricalVisitsByMedicalWorkerId(_medicalWorker.Id);
                MedicalTestResult testResult = _medicalWorker.AllVisits.Where(c => c.MedicalResult != null && c.MedicalResult.Id == id).FirstOrDefault().MedicalResult;

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
                List<Visit> historicalVisits = _context.GetHistoricalVisitsByMedicalWorkerId(_medicalWorker.Id);
                if (model.SelectedDate != null)
                {
                    historicalVisits = historicalVisits.Where(c => c.DateTimeSince.Date == model.SelectedDate.Value.Date).ToList();
                }
                _medicalWorker.PastVisits = historicalVisits;
                //HistoricalVisitsViewModel model = new HistoricalVisitsViewModel(_medicalWorker);
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
                List<Visit> historicalVisits = _context.GetHistoricalVisitsByMedicalWorkerId(_medicalWorker.Id);
                _medicalWorker.PastVisits = historicalVisits;
                HistoricalVisitsViewModel model = new HistoricalVisitsViewModel(_medicalWorker);
                model.UserName = _loggedUser.Person.FullName;

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
                    if (visit.MedicalResult!=null)
                    {
                        _context.AddNotification(visit.MedicalResult.Id,NotificationType.TestResult,visit.Patient.Id, DateTimeOffset.Now, visit.Id);
                    }
                    if (visit.Prescription!=null)
                    {
                        _context.AddNotification(visit.Prescription.Id, NotificationType.Prescription, visit.Patient.Id, DateTimeOffset.Now, visit.Id);
                    }
                    if (visit.ExaminationReferrals!=null)
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
                List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
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
                List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
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
        public IActionResult SaveMedicalHistory(CurrentVisitViewModel model)
        {
            if (_loggedUser != null)
            {
                List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                if (visit != null)
                {
                    visit.MedicalHistory = model.Visit.MedicalHistory;
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
                List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                if (visit != null)
                {
                    visit.Recommendations.Add(model.RecommendationToAdd);
                    _context.AddRecommendation(model.RecommendationToAdd);
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
        public IActionResult DeleteRecommendation(CurrentVisitViewModel model)
        {
            if (_loggedUser != null)
            {
                List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                if (visit != null)
                {
                    _context.DeleteRecommendation(model.RecommendationToRemove.Id);
                    //Recommendation recommendation = visit.Recommendations.Where(c => c.Id == model.RecommendationToRemove.Id).FirstOrDefault();
                    int index = visit.Recommendations.FindIndex(c => c.Id == model.RecommendationToRemove.Id);

                    if (index > -1)
                    {
                        visit.Recommendations.RemoveAt(index);
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


        //[HttpPost]
        //public IActionResult AddExaminationReferral(CurrentVisitViewModel model)
        private void AddExaminationReferral(CurrentVisitViewModel model)
        {
            //if (_loggedUser != null)
            //{
            //    if (!ModelState.IsValid)
            //    {
            //        return RedirectToAction("CurrentVisit");
            //    }
            if (model.MedicalServiceToAddId > 0)
            {
                List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                if (visit != null)
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
                        medicalReferral.IssuedTo = model.Visit.Patient;
                        _context.AddMedicalReferral(medicalReferral);
                        visit.ExaminationReferrals.Add(medicalReferral);
                        model.Visit = visit;
                        //model.Visit.ExaminationReferrals.Add(medicalReferral);
                    }
                    else
                    {
                    }
                    //RedirectToAction();
                }

            }
        }
        [HttpPost]
        public IActionResult RemoveExaminationReferral(CurrentVisitViewModel model)
        {
            if (_loggedUser != null)
            {
                if (model.MedicalReferralIdToRemove > 0)
                {
                    List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                    Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                    if (visit != null)
                    {
                        _context.RemoveMedicalReferralById(model.MedicalReferralIdToRemove);
                        int index = visit.ExaminationReferrals.FindIndex(c => c.Id == model.MedicalReferralIdToRemove);
                        visit.ExaminationReferrals.RemoveAt(index);
                        model.Visit = visit;
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
                List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                if (visit != null)
                {
                    if (model.ServiceToAdd > 0)
                    {
                        MedicalService medicalService = _context.GetMedicalServiceById(model.ServiceToAdd);
                        if (medicalService != null)
                        {
                            visit.MinorMedicalServices.Add(medicalService);
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
        public IActionResult RemoveMedicineFromPrescription(CurrentVisitViewModel model)
        {
            if (_loggedUser != null)
            {
                List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                if (visit != null)
                {
                    if (model.MedicineIdToRemove > 0)
                    {
                        IssuedMedicine medicine = visit.Prescription.IssuedMedicines.First(c => c.Id == model.MedicineIdToRemove);

                        visit.Prescription.IssuedMedicines.Remove(medicine);

                        _context.RemoveIssuedMedicineById(model.MedicineIdToRemove);
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
                List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                if (visit != null)
                {
                    if (model.ServiceToAdd > 0)
                    {
                        MedicalService medicalService = _context.GetMedicalServiceById(model.ServiceToAdd);
                        if (medicalService != null)
                        {
                            visit.MinorMedicalServices.Remove(medicalService);
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
                List<Visit> todayVisits = _medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
                Visit visit = todayVisits.Where(c => c.Id == model.VisitId).FirstOrDefault();
                if (visit != null)
                {
                    //if (model.PrescriptionIdToRemove > 0)
                    //{
                    //    Prescription prescription = _context.GetPrescriptionById(model.PrescriptionIdToRemove);
                    //    if (prescription != null)
                    //    {
                    long id = visit.Prescription.Id;
                    visit.Prescription = null;
                    _context.RemovePrescriptionById(model.PrescriptionIdToRemove);
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
                Visit visit = _context.GetHistoricalVisitById(id);
                if (visit == null)
                {
                    visit = _context.GetFutureVisitsByMedicalWorkerId(_medicalWorker.Id).Where(c => c.Id == id).FirstOrDefault();
                }
                CurrentVisitViewModel model = new CurrentVisitViewModel();
                model.Visit = visit;
                model.UserName = _loggedUser.Person.FullName;

                //Models.LocationsViewModel model = new Models.LocationsViewModel(locations);
                return View(model);
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
                MedicalWorkerArea.Models.PatientViewModel model = new MedicalWorkerArea.Models.PatientViewModel(patient);
                model.UserName = _loggedUser.Person.FullName;

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
                model.UserName = _loggedUser.Person.FullName;

                return View(medicalWorker);
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
