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
        public long? PersonId { get; set; }
        [ForeignKey("PersonId")]
        public Person Person { get; set; }
        public long? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required(ErrorMessage = "Wybierz pakiet medyczny")]
        [Display(Name = "Pakiet medyczny")]
        [Range(1,long.MaxValue, ErrorMessage ="Wybierz pakiet medyczny")]
        
        public long? MedicalPackageId
        {
            get;set;
        }

        [Required(ErrorMessage = "Wybierz pakiet medyczny")]
        [Display(Name = "Pakiet medyczny")]
        [ForeignKey("MedicalPackageId")]
        public MedicalPackage MedicalPackage { get; set; }

        [Display(Name = "Oddział NFZ")]
        [Range(1, long.MaxValue, ErrorMessage = "Wybierz oddział NFZ")]

        public long?     NFZUnitId
        {
            get;set;
        }
        [Display(Name = "Oddział NFZ")]
        [ForeignKey("NFZUnitId")]
        public NFZUnit? NFZUnit { get; set; }


        [Display(Name = "Nazwa pracodawcy")]
        public string? EmployerName { get; set; }
        [Display(Name = "NIP pracodawcy")]
        public string? EmployerNIP { get; set; }
        public bool IsActive { get; set; }

        [NotMapped]
        public  List<MedicalTestResult> TestsResults { get; set; }

        [NotMapped]
        public List<MedicalReferral> MedicalReferrals { get; set; }

        [NotMapped]
        public List<Prescription> Prescriptions { get; set; }
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
                    return AllVisits.Where(c => c.VisitStatus == VisitStatus.Booked || c.VisitStatus == VisitStatus.Active).ToList();
                }
                else
                {
                    return null;
                }               
            }
        }
        [NotMapped]
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
        public virtual List<Notification> Notifications         { get; set; }
        public void BookVisit(Visit visit)
        {
            visit.PatientId = this.Id;
            visit.VisitStatus = VisitStatus.Booked;
        }

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
