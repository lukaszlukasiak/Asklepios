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
    public class CurrentVisitViewModel: IBaseViewModel
    {
        private long _VisitId;
        
        public long VisitId 
        { 
            get
            {
                if (_VisitId<=0)
                {
                    if (Visit!=null)
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

        public MedicalWorker MedicalWorker { get; }
        public long MedicalWorkerId { get; set; }
        public long PatientId { get; set; }
        public Patient Patient { get; set; }
        public long ServiceToAdd { get; set; }
        public long ServiceToRemove { get; set; }
        public Recommendation RecommendationToAdd { get; set; }
        public Recommendation RecommendationToRemove { get; set; }
        public Prescription PrescriptionToAdd { get; set; }
        public IssuedMedicine IssuedMedicineToAdd { get; set; }
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
        public long MedicalTestFileIdToRemove { get; set; }

        public long PrescriptionIdToRemove { get; set;  }
        [Display(Name = "Okres ważności recepty (w dniach)")]
        [Required(ErrorMessage = "Proszę wprowadzić okres ważności recepty (w dniach")]
        [DataType(DataType.Text)]
        [Range(7, 365)]
        public int PrescriptionDaysToExpire { get; set; }

        public long MedicineIdToRemove { get; set; }
        public SubmitMode SubmitMode { get; set; }

        [Display(Name = "Typ skierowania")]
        //[Required(ErrorMessage = "Proszę wybrać typ usługi")]
        [RequiredGreaterThanZero("Proszę wybrać typ usługi")]

        public long MedicalServiceToAddId { get; set; }
        [Display(Name = "Komentarz")]
        [DataType(DataType.MultilineText)]

        public string MedicalReferralToAddComment { get; set; }

        //[Display(Name = "Typ skierowania")]
        //[Required(ErrorMessage = "Proszę wybrać typ ")]
        
        //public MedicalReferral MedicalReferralToAdd { get; set; }
        [Display(Name = "Okres ważności skierowania (w dniach)")]
        [Required(ErrorMessage = "Proszę wprowadzić okres ważności skierowania w dniach")]
        [DataType(DataType.Text)]
        [Range(7,365, ErrorMessage ="Ważność skierowania musi wynosić między 7 oraz 365 dni")]
        public int MedicalReferralDaysToExpire { get; set; }
        public bool IsMedicalReferralAddingOK
        {
            get
            {
                if (MedicalServiceToAddId>0)
                {
                    if (MedicalReferralDaysToExpire>=7 && MedicalReferralDaysToExpire<=365)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        //public string MedicalReferralComment { get; set; }
        public long MedicalReferralIdToRemove { get; set; }
        public List<MedicalService> MedicalServicesForReferrals { get; set; }
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
                    if (Visit.MinorMedicalServices!=null)
                    {
                        List<MedicalService> availableServices = Visit.PrimaryService.SubServices.Except(Visit.MinorMedicalServices).ToList();
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

        public string UserName { get; set; }
    }
}
