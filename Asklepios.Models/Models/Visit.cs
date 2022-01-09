using Asklepios.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asklepios.Core.Models
{
    public class Visit
    {
        public long Id { get; set; }
        public VisitCategory VisitCategory { get; set; }

        public Patient Patient { get; set; }
        public MedicalWorker MedicalWorker { get; set; }
        public DateTimeOffset DateTimeSince { get; set; }
        public DateTimeOffset DateTimeTill { get; set; }
        public List<MedicalService> MinorMedicalServices { get; set; }
        public MedicalService PrimaryService { get; set; }
        public Location Location { get; set; }
        public MedicalRoom MedicalRoom { get; set; }
        public string MedicalHistory { get; set; }
        public MedicalTestResult MedicalResult { get; set; }
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
                if (VisitCategory.Type == VisitCategoryType.MedicalImaging || VisitCategory.Type == VisitCategoryType.MedicalImaging)
                {
                    if (this.MedicalResult == null)
                    {
                        return true;
                    }

                    //if (this.VisitSummary != null)
                    //{
                    //    if (this.VisitSummary.MedicalResult == null)
                    //    {
                    //        return true;
                    //    }
                    //}
                }
                return false;
            }
        }
        //public List<decimal> GetPrices()
        //{
        //    List<decimal> subPrices = new List<decimal>();
        //    MedicalPackage package = Patient.MedicalPackage;

        //    for (int i = 0; i < BookedMinorMedicalServices.Count; i++)
        //    {
        //        MedicalService service = BookedMinorMedicalServices[i];
        //        if (package.ServicesDiscounts.ContainsKey(service))
        //        {
        //            decimal price = service.StandardPrice * package.ServicesDiscounts[service];
        //            subPrices.Add(price);
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    return subPrices;
        //}
        public decimal GetPrice(MedicalService service)
        {
            MedicalPackage package = Patient.MedicalPackage;
            decimal price = decimal.MinusOne;
            if (package.ServicesDiscounts.ContainsKey(service))
            {
                price = service.StandardPrice * package.ServicesDiscounts[service];
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

            if (package.ServicesDiscounts.ContainsKey(PrimaryService))
            {
                decimal price = PrimaryService.StandardPrice * package.ServicesDiscounts[PrimaryService];
                totalPrice += price;
            }

            for (int i = 0; i < MinorMedicalServices.Count; i++)
            {
                MedicalService service = MinorMedicalServices[i];
                if (package.ServicesDiscounts.ContainsKey(service))
                {
                    decimal price = service.StandardPrice * package.ServicesDiscounts[service];
                    totalPrice+= price;
                }
            }
            return totalPrice;
        }

    }

}

