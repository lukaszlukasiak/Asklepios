using Asklepios.Core.MockModels;
using Asklepios.Core.Models;
using Asklepios.Web.Validators;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.MedicalWorkerArea.Models
{
    public class CurrentVisitViewModel : IBaseViewModel
    {
        private long _VisitId;

        public long VisitId
        {
            get
            {
                if (_VisitId <= 0)
                {
                    if (Visit != null)
                    {
                        return Visit.Id;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return _VisitId;
                }
            }
            set
            {
                _VisitId = value;
            }
        }
        public Visit Visit { get; set; }
        //public Visit TempVisit { get; set; }
        public Patient Patient { get; set; }
        public MedicalWorker MedicalWorker { get; }

        public long MedicalWorkerId { get; set; }
        public long PatientId { get; set; }
        public long ServiceToAddId { get; set; }
        //public long ServiceToRemoveId { get; set; }
        public int ReferralToRemoveIndex { get; set; }

        public int RecommendationToRemoveIndex { get; set; }
        public int ServiceToRemoveIndex { get; set; }

        public VisitSummaryModel VisitSummary {get;set;}
        //public List<Recommendation> Recommendations { get; set; }
        //public Recommendation RecommendationToAdd { get; set; }
        //public Recommendation RecommendationToRemove { get; set; }
        //public Prescription PrescriptionToAdd { get; set; }
        //public IssuedMedicine IssuedMedicineToAdd { get; set; }
        //public IssuedMedicine IssuedMedicineToRemove { get; set; }
        //public List<MedicalService> MinorMedicalServices { get; set; }

        //public hist
        //public List<IssuedMedicine>  
        [Required(ErrorMessage = "Proszę wybrać typ badań")]
        [Range(1, long.MaxValue, ErrorMessage ="Proszę wybrać usługę")]
        [Display(Name = "Typ badań")]

        public long MedicalTestServiceId { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić opis/zakres badań")]
        [Display(Name = "Opis/zakres badań")]

        public string MedicalTestDescription { get; set; }

        [Display(Name = "Wyniki badań")]
        [Required(ErrorMessage = "Proszę wybrać plik z wynikami")]

        public IFormFile MedicalTestFile { get; set; }
        //public long MedicalTestFileIdToRemove { get; set; }

        public long PrescriptionIdToRemove { get; set;  }
        [Display(Name = "Okres ważności recepty (w dniach)")]
        [Required(ErrorMessage = "Proszę wprowadzić okres ważności recepty (w dniach")]
        [DataType(DataType.Text)]
        [Range(7, 365)]
        public int PrescriptionDaysToExpire { get; set; }

        public long MedicineIndexToRemove { get; set; }

        //[Display(Name = "Typ skierowania")]
        ////[Required(ErrorMessage = "Proszę wybrać typ usługi")]
        //[RequiredGreaterThanZero("Proszę wybrać typ usługi")]

        //public long ServiceReferralToAddId { get; set; }
        //[Display(Name = "Komentarz")]
        //[DataType(DataType.MultilineText)]

        //public string ServiceReferralToAddComment { get; set; }

        ////[Display(Name = "Typ skierowania")]
        ////[Required(ErrorMessage = "Proszę wybrać typ ")]
        
        ////public MedicalReferral MedicalReferralToAdd { get; set; }
        //[Display(Name = "Okres ważności skierowania (w dniach)")]
        //[Required(ErrorMessage = "Proszę wprowadzić okres ważności skierowania w dniach")]
        //[DataType(DataType.Text)]
        //[Range(7,365, ErrorMessage ="Ważność skierowania musi wynosić między 7 oraz 365 dni")]
        //public int ServiceReferralDaysToExpire { get; set; }
        //public bool IsMedicalReferralAddingOK
        //{
        //    get
        //    {
        //        if (ServiceReferralToAddId>0)
        //        {
        //            if (ServiceReferralDaysToExpire>=7 && ServiceReferralDaysToExpire<=365)
        //            {
        //                return true;
        //            }
        //        }
        //        return false;
        //    }
        //}
        //public string MedicalReferralComment { get; set; }
        public long ReferralIndexToRemove { get; set; }
        public List<MedicalService> MedicalServicesForReferrals { get; set; }
        public SubmitMode SubmitMode { get; set; }
        public string UserName { get; set; }


        public CurrentVisitViewModel(Visit visit)
        {
            Visit = visit;
        }
        public CurrentVisitViewModel()
        {
        }


        public CurrentVisitViewModel(MedicalWorker medicalWorker)
        {
            MedicalWorker = medicalWorker;
        }
        public List<MedicalService> AvailableMinorServices
        {
            get
            {
                if (Visit==null)
                {
                    return null;
                }
                if (Visit.PrimaryService?.SubServices?.Count()>0)
                {
                    if (VisitSummary.MinorMedicalServices!=null)
                    {
                        var excludedIDs = new List<long>(VisitSummary.MinorMedicalServices.Select(p => p.Id));
                        List<MedicalService> availableServices = Visit.PrimaryService.SubServices.Where(p => !excludedIDs.Contains(p.Id)).ToList();

                      //  List<MedicalService> availableServices = Visit.PrimaryService.SubServices.Except(Visit.MinorMedicalServices).ToList();
                        return availableServices;
                    }
                    else
                    {
                        return Visit.PrimaryService.SubServices.ToList();
                    }
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
