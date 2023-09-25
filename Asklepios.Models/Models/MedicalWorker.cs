using Asklepios.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace Asklepios.Core.Models
{
    public abstract class MedicalWorker //: Person
    {
        public MedicalWorker()
        {
            IsActive = true;
        }
        public MedicalWorker(Person person)
        {
            Person = person;
            IsActive = true;
        }
        public MedicalWorker(long personId, string professionalNumber)
        {
            PersonId = personId;
            ProfessionalNumber = professionalNumber;
            IsActive = true;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public long Id { get; set; }
        public long? PersonId { get; set; }
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
        public long? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [Required(ErrorMessage = "Wprowadź numer zawodowy!")]
        [Display(Name = "Numer zawodowy")]

        public string ProfessionalNumber {get;set;}
        public DateTime HiredSince { get; set; }
        
        [Display(Name = "Czy jest zatrudniony obecnie")]

        public bool IsActive { get; set; }
        [Display(Name = "Tytuł zawodowy")]

        public abstract string ProfessionalTitle { get; }
        public abstract bool CanIssuePrescription { get; }
        public abstract bool CanIssueExamReferral{ get; }
        public abstract bool CanAddMedicalTestResults { get; }

        //public abstract string FullProffesionalName { get; }
        public string FullProffesionalName => ProfessionalTitle + " " + Person.Name + " " + Person.Surname;

        public virtual List<Visit> AllVisits
        {
            get;
            set;
        }
        public virtual List<VisitReview> VisitReviews { get; set; }
        [Required(ErrorMessage = "Wprowadź typ pracownika medycznego!")]
        
        [Display(Name = "Typ pracownika medycznego")]

        public MedicalWorkerType? MedicalWorkerType { get; set; }
        [Required(ErrorMessage = "Wprowadź streszczenie odnośnie zdobytego wykształcenia, przebytych kursów itp.!")]
        [Display(Name = "Edukacja/wykształcenie")]

        public string Education { get; set; }
        [Required(ErrorMessage = "Wprowadź streszczenie odnośnie zdobytego doświadczenia!")]
        [Display(Name = "Doświadczenie")]

        public string Experience { get; set; }

        [Required(ErrorMessage = "Specjalizacja/świadczone usługi")]
        [Display(Name = "Usługi")]
        [NotMapped]
        public List<long> MedicalServiceIds { get; set; }
        [Required(ErrorMessage = "Specjalizacja/świadczone usługi")]
        [Display(Name = "Usługi")]

        public virtual List<MedicalService> MedicalServices { get; set; }
        [NotMapped]
        public List<Visit> FutureVisits
        {
            get
            {
                return AllVisits.Where(c => c.VisitStatus == VisitStatus.AvailableNotBooked || c.VisitStatus == VisitStatus.Booked).ToList();
            }
        }
        [NotMapped]
        public List<Visit> PastVisits
        {
            get
            {
                return AllVisits.Where(c => c.VisitStatus == VisitStatus.Finished || c.VisitStatus == VisitStatus.NotHeldOther || c.VisitStatus == VisitStatus.NotHeldAbsentPatient).ToList();
            }
        }

        [NotMapped]
        public float AverageRating
        {
            get
            {
                if (VisitReviews != null)
                {
                    if (VisitReviews.Count > 0)
                    {

                        return VisitReviews.Average(c => c.GeneralRate);
                    }
                }
                return -1;
            }
        }

        public void UpdateWith(MedicalWorker worker)
        {
            if (!string.IsNullOrWhiteSpace( worker.Education))
            {
                this.Education = worker.Education;
            }
            if (!string.IsNullOrWhiteSpace(worker.Experience))
            {
                this.Experience = worker.Experience;
            }
            //if (worker.IsActive.HasValue)
            //{
                this.IsActive = worker.IsActive;
            //}
            if (worker.MedicalServiceIds!=null)
            {
                this.MedicalServiceIds = worker.MedicalServiceIds;
            }
            //if (worker.MedicalServicesToMedicalWorkers!=null)
            //{
            //    this.MedicalServicesToMedicalWorkers = worker.MedicalServicesToMedicalWorkers;
            //}
            if (worker.MedicalWorkerType.HasValue)
            {
                this.MedicalWorkerType = worker.MedicalWorkerType;
            }
            if (worker.Person!=null)
            {
                this.Person = worker.Person;
            }
            if (!string.IsNullOrWhiteSpace( worker.ProfessionalNumber))
            {
                this.ProfessionalNumber = worker.ProfessionalNumber;
            }
            if (worker.User!=null)
            {
                this.User = worker.User;
            }
        }
        [NotMapped]
        [JsonIgnore]
        public string RatingDescription
        {
            get
            {
                
                if (VisitReviews?.Count > 0)
                {
                    string ratingV = null;
                    if (VisitReviews.Count==1)
                    {
                        ratingV = "ocena";
                    }
                    else if (VisitReviews.Count==2)
                    {
                        ratingV = "oceny";
                    }
                    else
                    {
                        ratingV = "ocen";
                    }
                    return AverageRating.ToString("N2") + " (" + VisitReviews.Count + " " + ratingV +")";
                }
                else
                {
                    return "";
                }
            }
        }
        [NotMapped]
        [JsonIgnore]
        public bool IsValid
        {
            get
            {
                if (MedicalWorkerType.HasValue)
                {
                    if (MedicalServices != null)
                    {
                        if (!string.IsNullOrWhiteSpace(Experience))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }
    }
}