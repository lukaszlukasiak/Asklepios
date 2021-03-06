using Asklepios.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Asklepios.Core.Models
{
    public class Visit
    {
        public long Id { get; set; }
        public VisitCategory VisitCategory { get; set; }
        public long VisitCategoryId { get; set; }
        public VisitStatus VisitStatus { get; set; }
        private Patient _Patient;
        public Patient Patient 
        {
            get
            {
                return _Patient;
            }
            set
            {
                if (value!=null)
                {
                    _Patient = value;
                    VisitStatus = VisitStatus.Booked;
                }
            }
        }
        public long PatientId { get; set; }
        public MedicalWorker MedicalWorker { get; set; }
        public long MedicalWorkerId { get; set; }
        public DateTimeOffset DateTimeSince { get; set; }
        public DateTimeOffset DateTimeTill { get; set; }
        public List<long> MinorMedicalServicesIds { get; set; }
        public List<MedicalService> MinorMedicalServices { get; set; }
        public MedicalService PrimaryService { get; set; }
        public long PrimaryServiceId { get; set; }
        public Location Location { get; set; }
        public long LocationId { get; set; }
        public MedicalRoom MedicalRoom { get; set; }
        public long MedicalRoomId { get; set; }
        public string MedicalHistory { get; set; }
        public long MedicalResultId { get; set; }
        private MedicalTestResult _medicalTestResult;

        public MedicalTestResult MedicalResult 
        { 
            get
            {
                return _medicalTestResult;
            }
            set
            {
                _medicalTestResult = value;
                if (_medicalTestResult!=null)
                {
                    _medicalTestResult.ExamDate = DateTimeOffset.Now;
                    MedicalResultId = _medicalTestResult.Id;
                }
            }
        }
        public IFormFile ImageFile { get; set; }
        public string ImageFilePath { get; set; }
        public string ImageSource
        {
            get
            {
                if (ImageFile != null)
                {
                    if (ImageFile.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            ImageFile.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            string s = Convert.ToBase64String(fileBytes);


                            return string.Format("data:image/jpg;base64,{0}", s);
                            // act on the Base64 data
                        }
                    }

                }
                return ImageFilePath;
            }
        }
        private List<Recommendation> _recommendations;
        public List<Recommendation> Recommendations 
        {
            get
            {
                return _recommendations;
            }
            set
            {
                _recommendations = value;
                if (_recommendations!=null)
                {
                    foreach (var item in _recommendations)
                    {
                        item.Visit = this;
                    }
                    RecommendationIds = Recommendations.Select(c => c.Id).ToList();
                }              
            }
        }
        public List<long> RecommendationIds { get; set; }

        private Prescription _prescription;
        public Prescription Prescription 
        { 
            get
            {
                return _prescription;
            }
            set
            {
                _prescription = value;
                if (_prescription!=null)
                {
                    _prescription.Visit = this;
                    PrescriptionId = _prescription.Id;
                }
                
            }
        }
        public long PrescriptionId { get; set; }
        private MedicalReferral _usedExaminationReferral;
        public MedicalReferral UsedExaminationReferral
        {
            get
            {
                return _usedExaminationReferral;
            }
            set
            {
                _usedExaminationReferral = value;
                if (_usedExaminationReferral != null)
                {
                    //foreach (var item in _examinationReferrals)
                    //{
                    _usedExaminationReferral.VisitWhenIssued = this;
                    //item.VisitWhenIssued = this;
                    //item.IssuedBy = this.MedicalWorker;
                    //item.IssuedTo = this.Patient;
                    //}
                    UsedExaminationReferralId = _usedExaminationReferral.Id;
                }
            }
        }
        public long UsedExaminationReferralId { get; set; }

        private List<MedicalReferral> _examinationReferrals;
        public List<MedicalReferral> ExaminationReferrals 
        { 
            get
            {
                return _examinationReferrals;
            }
            set
            {
                _examinationReferrals = value;
                if (_examinationReferrals!=null)
                {
                    foreach (var item in _examinationReferrals)
                    {
                        item.VisitWhenIssued = this;
                        //item.IssuedBy = this.MedicalWorker;
                        //item.IssuedTo = this.Patient;
                    }
                    ExaminatinoReferralsIds = _examinationReferrals.Select(c => c.Id).ToList();
                }              
            }
        }
        public List<long> ExaminatinoReferralsIds { get; set; }
        public VisitReview _visitReview;
        public VisitReview VisitReview 
        {
            get
            {
                return _visitReview;
            }
            set
            {
                _visitReview = value;
                if (value!=null)
                {
                    _visitReview.Reviewee = MedicalWorker;
                    _visitReview.Reviewer = Patient;
                    _visitReview.Visit = this;
                }
                if (_visitReview!=null)
                {
                    VisitReviewId = _visitReview.Id;
                }
            }
        }
        public long VisitReviewId { get; set; }
        //public bool IsBooked
        //{
        //    get
        //    {
        //        if (Patient!=null)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        //public VisitSummary VisitSummary { get; set; }
        public string GetVisitDateDescription()
        {
            string dateDescription = DateTimeSince.ToString("dd-MM-yyyy") + " " + DateTimeSince.ToString("HH:mm");// + "-" + DateTimeTill.ToString("HH:mm");
            return dateDescription;
        }
        public string GetVisitDateFullDescription()
        {
            string dateDescription = DateTimeSince.ToString("dd-MM-yyyy") + " " + DateTimeSince.ToString("HH:mm") + "-" + DateTimeTill.ToString("HH:mm");
            return dateDescription;
        }

        public string VisitDescription
        {
            get
            {
                if (PrimaryService==null )
                {
                    return null;
                }
                string description = PrimaryService.Name;
                switch (VisitCategory.Type)
                {
                    case VisitCategoryType.Consultations:

                        break;
                    case VisitCategoryType.EConsultations:
                        description = "E-" + description;
                        break;
                    case VisitCategoryType.Stomatology:

                        break;
                    case VisitCategoryType.MedicalImaging:
                        description = "Diasgnostyka obrazowa - " + description;
                        break;
                    case VisitCategoryType.Physiotherapy:
                        description = "Fizjoterpaia - " + description;
                        break;
                    case VisitCategoryType.TreatmentRoom:
                        description = "Gabinet zabiegowy - " + description;
                        break;
                    default:
                        break;
                }
                return description;
            }
        }


        public bool IsNotCompletedMedicalTest
        {
            get
            {
                if (VisitCategory==null)
                {
                    return false;
                }
                if (VisitCategory.Type == VisitCategoryType.MedicalImaging || VisitCategory.Type == VisitCategoryType.MedicalImaging)
                {
                    if (this.MedicalResult == null)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public decimal GetPrice(MedicalService service)
        {
            MedicalServiceDiscount discount = null;
            decimal price = decimal.Zero;// service.StandardPrice;

            if (VisitStatus==VisitStatus.Booked)
            {
                MedicalPackage package = Patient.MedicalPackage;
                //price = decimal.MinusOne;
                discount = package.ServiceDiscounts.First(c => c.MedicalService == service);
                price = service.StandardPrice * (1 - discount.Discount);//package.ServicesDiscounts[service];
                return price;

            }
            else
            {
                return service.StandardPrice;
            }
        }
        public decimal GetTotalPrice()
        {
            decimal totalPrice = decimal.Zero;

            if (VisitStatus==VisitStatus.Booked)
            {
                MedicalPackage package = Patient.MedicalPackage;

                MedicalServiceDiscount discount = package.ServiceDiscounts.First(c => c.MedicalService.Id == PrimaryService.Id);
                if (discount != null)
                {
                    decimal priceP = PrimaryService.StandardPrice * (1 - discount.Discount); //package.ServicesDiscounts[PrimaryService];
                    totalPrice += priceP;
                }
                else
                {
                    decimal priceP = PrimaryService.StandardPrice; //package.ServicesDiscounts[PrimaryService];
                    totalPrice += priceP;
                }
                for (int i = 0; i < MinorMedicalServices?.Count; i++)
                {
                    MedicalService service = MinorMedicalServices[i];
                    if (service==null)
                    {
                        continue;
                    }
                    MedicalServiceDiscount discount2 = package.ServiceDiscounts.First(c => c.MedicalService.Id == service.Id);

                    //if (package.ServicesDiscounts.ContainsKey(PrimaryService))
                    if (discount2 != null)
                    {
                        decimal price = service.StandardPrice * (1 - discount.Discount);//package.ServicesDiscounts[service];
                        totalPrice += price;
                    }
                    else
                    {
                        totalPrice += service.StandardPrice;
                    }
                }
            }
            else
            {
                totalPrice += PrimaryService.StandardPrice;

                for (int i = 0; i < MinorMedicalServices?.Count; i++)
                {
                    MedicalService service = MinorMedicalServices[i];
                    totalPrice += service.StandardPrice;
                }

            }

            return totalPrice;
        }
        public Visit (Location location,MedicalRoom medicalRoom,MedicalWorker medicalWorker,VisitCategory visitCategory,MedicalService medicalService,DateTimeOffset start, DateTimeOffset end)
        {
            Location = location;
            LocationId = location.Id;
            MedicalRoom = medicalRoom;
            MedicalRoomId = medicalRoom.Id;
            MedicalWorker = medicalWorker;
            MedicalWorkerId = medicalWorker.Id;
            VisitCategory = visitCategory;
            VisitCategoryId = visitCategory.Id;
            PrimaryService = medicalService;
            PrimaryServiceId = medicalService.Id;
            DateTimeSince = start;
            DateTimeTill = end;
            MinorMedicalServices = new List<MedicalService>();
            Recommendations = new List<Recommendation>();
        }
        public Visit()
        {
            MinorMedicalServices = new List<MedicalService>();
            Recommendations = new List<Recommendation>();
            ExaminationReferrals = new List<MedicalReferral>();
        }
    }

}

