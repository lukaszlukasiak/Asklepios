using Asklepios.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asklepios.Core.Models
{
    public class Visit
    {
        public long Id { get; set; }
        public VisitCategory VisitCategory { get; set; }
        public long VisitCategoryId { get; set; }
        public Patient Patient { get; set; }
        public long PatientId { get; set; }
        public MedicalWorker MedicalWorker { get; set; }
        public long MedicalWorkerId { get; set; }
        public DateTimeOffset DateTimeSince { get; set; }
        public DateTimeOffset DateTimeTill { get; set; }
        public List<MedicalService> MinorMedicalServices { get; set; }
        public MedicalService PrimaryService { get; set; }
        public long PrimaryServiceId { get; set; }
        public Location Location { get; set; }
        public MedicalRoom MedicalRoom { get; set; }
        public string MedicalHistory { get; set; }
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
                _medicalTestResult.ExamDate = DateTimeOffset.Now;
            }
        }
        public List<Recommendation> Recommendations { get; set; }
        public Prescription Prescription { get; set; }
        public List<MedicalReferral> ExaminationReferrals { get; set; }
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
                _visitReview.Reviewee = MedicalWorker;
                _visitReview.Reviewer = Patient;
                _visitReview.Visit = this;               
            }
        }
        public bool IsBooked
        {
            get
            {
                if (Patient!=null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
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
            MedicalPackage package = Patient.MedicalPackage;
            decimal price = decimal.MinusOne;
            MedicalServiceDiscount discount = package.ServiceDiscounts.First(c => c.MedicalService == service);
            if (discount!=null)
            {
                price = service.StandardPrice * discount.Discount;//package.ServicesDiscounts[service];
                return price;
            }
            else
            {
                return price;
            }
        }
        public decimal GetTotalPrice()
        {
            MedicalPackage package = Patient.MedicalPackage;
            decimal totalPrice = decimal.MinusOne;

            MedicalServiceDiscount discount = package.ServiceDiscounts.First(c => c.MedicalService == PrimaryService);

            //if (package.ServicesDiscounts.ContainsKey(PrimaryService))
            if (discount != null)

            {
                decimal price = PrimaryService.StandardPrice * discount.Discount; //package.ServicesDiscounts[PrimaryService];
                totalPrice += price;
            }

            for (int i = 0; i < MinorMedicalServices?.Count; i++)
            {
                MedicalService service = MinorMedicalServices[i];
                MedicalServiceDiscount discount2 = package.ServiceDiscounts.First(c => c.MedicalService == service);

                //if (package.ServicesDiscounts.ContainsKey(PrimaryService))
                if (discount2 != null)
                {
                    decimal price = service.StandardPrice * discount2.Discount;//package.ServicesDiscounts[service];
                    totalPrice += price;
                }
            }
            return totalPrice;
        }
        public Visit (Location location,MedicalRoom medicalRoom,MedicalWorker medicalWorker,VisitCategory visitCategory,MedicalService medicalService,DateTimeOffset start, DateTimeOffset end)
        {
            Location = location;
            MedicalRoom = medicalRoom;
            MedicalWorker = medicalWorker;
            VisitCategory = visitCategory;
            PrimaryService = medicalService;
            DateTimeSince = start;
            DateTimeTill = end;
        }
        public Visit()
        {

        }
    }

}

