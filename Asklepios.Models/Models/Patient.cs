#nullable enable
using Asklepios.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Asklepios.Core.Models
{
    public class Patient 
    {
        
        public long Id { get; set; }
        [Required]
        public long PersonId { get; set; }
        public Person Person { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }

        private long _medicalPackageId;
        [Required(ErrorMessage = "Wybierz pakiet medyczny")]
        [Display(Name = "Pakiet medyczny")]

        public long MedicalPackageId
        {
            get
            {
                if (_medicalPackageId <= 0)
                {
                    if (MedicalPackage != null)
                    {
                        return MedicalPackage.Id;
                    }
                }
                return _medicalPackageId;
            }
            set
            {
                _medicalPackageId = value;
            }
        }

        [Required(ErrorMessage = "Wybierz pakiet medyczny")]
        [Display(Name = "Pakiet medyczny")]

        public MedicalPackage MedicalPackage { get; set; }

        [Display(Name = "Oddział NFZ")]
        public NFZUnit? NFZUnit { get; set; }
        [Display(Name = "Oddział NFZ")]
        private long _NFZUnitId;
        public long NFZUnitId
        {
            get
            {
                if (_NFZUnitId <= 0)
                {
                    if (NFZUnit != null)
                    {
                        if (NFZUnit!=null)
                        {
                            return NFZUnit.Id;
                        }
                    }
                }
                return _NFZUnitId;
            }
            set
            {
                _NFZUnitId = value;
            }
        }


        [Display(Name = "Nazwa pracodawcy")]
        public string? EmployerName { get; set; }
        [Display(Name = "NIP pracodawcy")]
        public string? EmployerNIP { get; set; }


        public List<MedicalTestResult> TestsResults 
        { 
            get
            {
                if (HistoricalVisits != null)
                {
                    List<MedicalTestResult> results = HistoricalVisits.Where(c => c.MedicalResult != null).Select(c => c.MedicalResult).ToList();
                    return results;
                }
                else
                {
                    return null;
                }
            }
        }
        public List<MedicalReferral> MedicalReferrals 
        {
            get
            {
                List<MedicalReferral> referrals = HistoricalVisits?.Where(c => c.ExaminationReferrals != null).SelectMany(c => c.ExaminationReferrals).ToList();
                //List<ExaminationReferral> referrals = HistoricalVisits.Where(c => c.VisitSummary?.ExaminationReferrals != null).SelectMany(c => c.VisitSummary.ExaminationReferrals).ToList();

                return referrals;
            }
        }
        public List<Prescription> Prescriptions 
        {
            get
            {
                List<Prescription>? prescs = HistoricalVisits?.Where(c => c.Prescription!=null).Select(c=>c.Prescription).ToList();
                //List<Prescription> prescs = HistoricalVisits.Where(c => c.VisitSummary?.Prescription != null).Select(c => c.VisitSummary.Prescription).ToList();
                return prescs;
            }          
        }
        //public List<IssuedMedicine> IssuedMedicines { get; set; }
        public List<Visit> HistoricalVisits { get; set; }
        public List<Visit> BookedVisits { get; set; }
        public bool IsValid 
        {
            get
            {
                if (Person!=null || PersonId>0)
                {
                    if (NFZUnit!=null || NFZUnitId>0)
                    {
                        if (MedicalPackage!=null || MedicalPackageId>0)
                        {
                            if (User!=null || UserId>0)
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
        }
        public List<Notification> Notifications 
        { get; set; }
        public void BookVisit(Visit visit)
        {
            visit.Patient = this;
            visit.PatientId = this.Id;
            if (BookedVisits==null)
            {
                BookedVisits = new List<Visit>();
            }
            BookedVisits.Add(visit);
        }

        //public List<NotificationFilter> Notifications {get;set;}
        //public Patient(string name, string surName, long id, string pesel, bool hasPolishCitizenship, string passportNumber, string passportCode, string email, Aglomeration aglomeration) : base(name, surName, id, pesel, hasPolishCitizenship, passportNumber, passportCode, email, aglomeration)
        //{
        //}
        public Patient(Person person)
        {
            Person = person;
            PersonId = person.Id;
        }
        public Patient()
        {

        }
        
    }
}
