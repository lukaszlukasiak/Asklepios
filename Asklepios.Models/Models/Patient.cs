#nullable enable
using Asklepios.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Asklepios.Core.Models
{
    public class Patient 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]

        public long Id { get; set; }
        [Required]
        public long PersonId { get; set; }
        [ForeignKey("PersonId")]
        public Person Person { get; set; }
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        //private long _medicalPackageId;
        [Required(ErrorMessage = "Wybierz pakiet medyczny")]
        [Display(Name = "Pakiet medyczny")]
        [Range(1,long.MaxValue, ErrorMessage ="Wybierz pakiet medyczny")]
        
        public long MedicalPackageId
        {
            get;set;
            //get
            //{
            //    if (_medicalPackageId <= 0)
            //    {
            //        if (MedicalPackage != null)
            //        {
            //            return MedicalPackage.Id;
            //        }
            //    }
            //    return _medicalPackageId;
            //}
            //set
            //{
            //    _medicalPackageId = value;
            //}
        }

        [Required(ErrorMessage = "Wybierz pakiet medyczny")]
        [Display(Name = "Pakiet medyczny")]
        [ForeignKey("MedicalPackageId")]
        public MedicalPackage MedicalPackage { get; set; }
        //[Display(Name = "Oddział NFZ")]
        //private long _NFZUnitId;

        [Display(Name = "Oddział NFZ")]
        [Range(1, long.MaxValue, ErrorMessage = "Wybierz oddział NFZ")]

        public long NFZUnitId
        {
            get;set;
            //get
            //{
            //    if (_NFZUnitId <= 0)
            //    {
            //        if (NFZUnit != null)
            //        {
            //            if (NFZUnit != null)
            //            {
            //                return NFZUnit.Id;
            //            }
            //        }
            //    }
            //    return _NFZUnitId;
            //}
            //set
            //{
            //    _NFZUnitId = value;
            //}
        }
        [Display(Name = "Oddział NFZ")]
        [ForeignKey("NFZUnitId")]
        public NFZUnit? NFZUnit { get; set; }


        [Display(Name = "Nazwa pracodawcy")]
        public string? EmployerName { get; set; }
        [Display(Name = "NIP pracodawcy")]
        public string? EmployerNIP { get; set; }


        public virtual List<MedicalTestResult> TestsResults 
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
                    return new List<MedicalTestResult>() ;
                }
            }
        }
        public List<MedicalReferral> MedicalReferrals 
        {
            get
            {
                if (HistoricalVisits!=null)
                {
                    List<MedicalReferral> referrals = HistoricalVisits.Where(c => c.ExaminationReferrals != null).SelectMany(c => c.ExaminationReferrals).ToList();
                    //List<ExaminationReferral> referrals = HistoricalVisits.Where(c => c.VisitSummary?.ExaminationReferrals != null).SelectMany(c => c.VisitSummary.ExaminationReferrals).ToList();

                    return referrals;

                }
                else
                {
                    return new List<MedicalReferral>();
                }
            }
        }
        public List<Prescription> Prescriptions 
        {
            get
            {
                if (HistoricalVisits!=null)
                {
                    List<Prescription> prescs = HistoricalVisits.Where(c => c.Prescription != null).Select(c => c.Prescription).ToList();
                    //List<Prescription> prescs = HistoricalVisits.Where(c => c.VisitSummary?.Prescription != null).Select(c => c.VisitSummary.Prescription).ToList();
                    return prescs;

                }
                else
                {
                    return new List<Prescription>();
                }
            }          
        }
        //public List<IssuedMedicine> IssuedMedicines { get; set; }
        [NotMapped]
        public List<Visit>? HistoricalVisits 
        {
            get
            {
                if (AllVisits!=null)
                {
                    return AllVisits.Where(c => c.VisitStatus == VisitStatus.Finished).ToList();
                }
                else
                {
                    return null;
                }
            }
        }
        [NotMapped]
        public List<Visit>? BookedVisits 
        {
            get
            {
                if (AllVisits!=null)
                {
                    return AllVisits.Where(c => c.VisitStatus == VisitStatus.Booked || c.VisitStatus == VisitStatus.Started).ToList();
                }
                else
                {
                    return null;
                }
                
            }
        }
        public List<Visit> AllVisits { get; set; } = new List<Visit>();
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
        public virtual List<Notification> Notifications 
        { get; set; }
        public void BookVisit(Visit visit)
        {
            visit.Patient = this;
            visit.PatientId = this.Id;
            visit.VisitStatus = VisitStatus.Booked;
            //if (BookedVisits==null)
            //{
            //    BookedVisits = new List<Visit>();
            //}
            AllVisits.Add(visit);
        }

        //public List<NotificationFilter> Notifications {get;set;}
        //public Patient(string name, string surName, long id, string pesel, bool hasPolishCitizenship, string passportNumber, string passportCode, string email, Aglomeration aglomeration) : base(name, surName, id, pesel, hasPolishCitizenship, passportNumber, passportCode, email, aglomeration)
        //{
        //}
        public Patient(Person person)
        {
            Person = person;
            PersonId = person.Id;
            User = new User();
        }
        public Patient()
        {
            Person = new Person();
            User = new User();
        }
        
    }
}
