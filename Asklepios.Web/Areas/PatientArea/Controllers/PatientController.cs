using Asklepios.Core.Models;
using Asklepios.Data.Interfaces;
using Asklepios.Web.Areas.HomeArea.Models;
using Asklepios.Web.Areas.PatientArea.Models;
using Asklepios.Web.Models;
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
        public PatientController(IPatientModuleRepository context)
        {
            _context = context;

        }

        internal static void LogOut()
        {
            _loggedUser = null;
            _person = null;
            _selectedPatient = null;
        }

        public IActionResult Index()
        {
            if (_loggedUser==null)
            {
                if (TempData.ContainsKey("User") == true)
                {
                    User user = JsonConvert.DeserializeObject<User>((string)TempData["User"]);
                    _loggedUser = user;
                    _selectedPatient = _context.GetPatientByUserId(_loggedUser.PersonId);

                    // User user = TempData["User"] as User;
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
                PatientArea.Models.PatientViewModel viewModel = new Models.PatientViewModel(_selectedPatient);
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

        public IActionResult Locations()
        {
            
            if (_loggedUser != null)
            {

                LocationsViewModel model = new LocationsViewModel();
                model.Locations = _context.GetAllLocations().ToList();
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
                    Visit visit = _context.GetAvailableVisitById(lid);
                    return View(visit);
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
                    Patient patient = _context.CurrentPatient;

                    Visit visit = _context.GetAvailableVisitById(lid);

                    if (visit.PrimaryService.RequireRefferal)
                    {
                        if (patient.MedicalReferrals.Count > 0)
                        {
                            MedicalReferral referral = patient.MedicalReferrals.Where(c => c.PrimaryMedicalService == visit.PrimaryService && c.IsActive).FirstOrDefault();
                            if (referral != null)
                            {
                                referral.HasBeenUsed = true;
                                referral.Visit = visit;
                                patient.BookVisit(visit);

                                _context.UpdateReferral(referral);
                                _context.UpdateVisit(visit);
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
                        patient.BookVisit(visit);
                        _context.UpdateVisit(visit);
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
                BookVisitViewModel model2 = new BookVisitViewModel() { AllVisitsList = _context.GetAvailableVisits().ToList() }; //(_context.GetAvailableVisits().ToList(),new VisitSearchOptions());
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
                model.AllVisitsList = _context.GetAvailableVisits().ToList();
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
                BookVisitViewModel model = new BookVisitViewModel() { AllVisitsList = _context.GetAvailableVisits().ToList() }; //(_context.GetAvailableVisits().ToList(),new VisitSearchOptions());
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

                ContactMessageViewModel model = new ContactMessageViewModel(_context.CurrentPatient);
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

                ContactMessageViewModel modelP = new ContactMessageViewModel(_context.CurrentPatient);
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
            else
            {
                return NotFound();
            }

        }
        //public IActionResult MedicalAdvice()
        //{
        //    return View();
        //}
        public IActionResult MedicalWorkersList()
        {
            if (_loggedUser != null)
            {

                List<MedicalWorker> medicalWorkers = _context.GetMedicalWorkers().ToList();
                return View(medicalWorkers);
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
                model.MedicalWorker = _context.GetHistoricalVisitById(model.VisitId).MedicalWorker;
                if (model.IsDataProper)
                {
                    visit.VisitReview = model.GetVisitReview();
                    return RedirectToAction("VisitDetails", "Patient", new { area = "PatientArea", id = visit.Id });
                }
                else
                {
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
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index", "Patient", new { area = "PatientArea", id = _context.CurrentPatient.Id });
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

                Patient patient = _context.CurrentPatient;
                PatientArea.Models.PatientViewModel viewModel = new Models.PatientViewModel(patient);
                return View(viewModel);
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

                Patient patient = _context.CurrentPatient;
                PatientArea.Models.PatientViewModel viewModel = new Models.PatientViewModel(patient);
                return View(viewModel);
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
                    Visit plannedVisit = _selectedPatient.BookedVisits.Where(c => c.Id == lid).FirstOrDefault();
                    if (plannedVisit != null)
                    {
                        if (plannedVisit.DateTimeSince > DateTimeOffset.Now)
                        {
                            _context.ResignFromVisit(plannedVisit, _selectedPatient);
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
                    Visit plannedVisit = _selectedPatient.BookedVisits.Where(c => c.Id == lid).FirstOrDefault();
                    if (plannedVisit != null)
                    {
                        if (plannedVisit.DateTimeSince > DateTimeOffset.Now)
                        {
                            RescheduleVisitViewModel model = new RescheduleVisitViewModel();
                            model.SelectedPrimaryServiceId = plannedVisit.PrimaryService.Id.ToString();
                            model.RescheduledVisitId = lid;
                            model.MedicalServices = _context.GetMedicalServices();
                            model.AllVisitsList = _context.GetAvailableVisits().ToList();
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
            if (_loggedUser != null)
            {

                Visit plannedVisit = _selectedPatient.BookedVisits.Where(c => c.Id == model.RescheduledVisitId).FirstOrDefault();
                if (plannedVisit != null)
                {
                    if (plannedVisit.DateTimeSince > DateTimeOffset.Now)
                    {
                        //RescheduleVisitViewModel model = new RescheduleVisitViewModel();
                        //model.SelectedPrimaryServiceId = plannedVisit.PrimaryService.Id.ToString();
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
                Visit visitToReschedule = _selectedPatient.BookedVisits.Where(c => c.Id == model.RescheduledVisitId).FirstOrDefault();
                if (visitToReschedule!=null)
                {
                    Visit newVisit = _context.GetAvailableVisitById( model.SelectedNewVisitId);
                    if (newVisit!=null)
                    {

                        newVisit.Patient = _selectedPatient;
                        _context.BookVisit(_selectedPatient, newVisit);
                        _context.ResignFromVisit(visitToReschedule, _selectedPatient);
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
                Patient patient = _context.CurrentPatient;
                PatientArea.Models.PatientViewModel viewModel = new Models.PatientViewModel(patient);
                return View(viewModel);
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

                Patient patient = _context.CurrentPatient;
                PatientArea.Models.PatientViewModel viewModel = new Models.PatientViewModel(patient);
                return View(viewModel);
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

                Patient patient = _context.CurrentPatient;
                PatientArea.Models.PatientViewModel viewModel = new Models.PatientViewModel(patient);
                return View(viewModel);
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
                    //PdfSharpCore.Pdf.PdfDocument pdf = _context.CurrentPatient.TestsResults.Where(c => c.Id == idL).FirstOrDefault().PdfDocument;

                    //Read the File data into Byte Array.
                    byte[] bytes = _context.CurrentPatient.TestsResults.Where(c => c.Id == idL).FirstOrDefault()?.PdfDocument;//pdf.  System.IO.File.ReadAllBytes(pdf);

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
