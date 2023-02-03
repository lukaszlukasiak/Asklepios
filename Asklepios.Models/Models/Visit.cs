using Asklepios.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;

namespace Asklepios.Core.Models
{
    public class Visit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]

        public long Id { get; set; }
        public long? VisitCategoryId { get; set; }
        [ForeignKey("VisitCategoryId")]
        public virtual VisitCategory VisitCategory { get; set; }
        public VisitStatus VisitStatus { get; set; }
        public long? PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient Patient 
        {
            get;set;
        }
        public long? MedicalWorkerId { get; set; }
        [ForeignKey("MedicalWorkerId")]
        public virtual MedicalWorker MedicalWorker { get; set; }
        public DateTimeOffset DateTimeSince { get; set; }
        public DateTimeOffset DateTimeTill { get; set; }
        [NotMapped]
        public List<long> MinorMedicalServicesIds { get; set; }
        //[NotMapped]
        public virtual List<MedicalService> MinorMedicalServices { get; set; }
        public virtual List<MinorServiceToVisit> MinorServicesToVisits { get; set; }

        public long? PrimaryServiceId { get; set; }
        [ForeignKey("PrimaryServiceId")]
        public virtual MedicalService PrimaryService { get; set; }
        public long? LocationId { get; set; }
        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }
        public long? MedicalRoomId { get; set; }
        [ForeignKey("MedicalRoomId")]
        public MedicalRoom MedicalRoom { get; set; }
        
        public string MedicalHistory { get; set; }
        public long? MedicalTestResultId { get; set; }
        
        //private MedicalTestResult _medicalTestResult;
        [ForeignKey("MedicalTestResultId")]
        public virtual MedicalTestResult MedicalTestResult 
        {
            get;set;

        }

      
        private List<Recommendation> _recommendations;
        public virtual List<Recommendation> Recommendations 
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
        [NotMapped]
        public List<long> RecommendationIds { get; set; }

        public long? PrescriptionId { get; set; }

        private Prescription _prescription;
        [ForeignKey("PrescriptionId")]
        public virtual Prescription Prescription 
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
        public long? UsedExaminationReferralId { get; set; }

        [ForeignKey("UsedExaminationReferralId")]
        public virtual MedicalReferral UsedExaminationReferral
        {
            get;set;
        }

        public virtual List<MedicalReferral> ExaminationReferrals 
        {
            get;set;
        }
        [NotMapped]
        public List<long> ExaminatinoReferralsIds { get; set; }
        public long? VisitReviewId { get; set; }

        public VisitReview _visitReview;
        [ForeignKey("VisitReviewId")]
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
                if (VisitCategory.Type == VisitCategoryType.MedicalImaging && VisitStatus==VisitStatus.Started)
                {
                    if (this.MedicalTestResult == null)
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
                MedicalPackage package =  Patient.MedicalPackage;
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
                MedicalServiceDiscount discount = package.ServiceDiscounts.First(c => c.MedicalServiceId == PrimaryService.Id);
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
            //MinorMedicalServices = new List<MedicalService>();
            Recommendations = new List<Recommendation>();
            ExaminationReferrals = new List<MedicalReferral>();
        }
    }

}

