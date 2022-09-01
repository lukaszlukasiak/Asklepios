using Asklepios.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Asklepios.Core.Models
{
    public abstract class MedicalWorker //: Person
    {
        //public MedicalWorker(string name, string surName, long id, string pesel, bool hasPolishCitizenship, string passportNumber, string passportCode, string email, Aglomeration aglomeration)
        //    : base(name, surName, id, pesel, hasPolishCitizenship, passportNumber, passportCode, email, aglomeration)
        //{
        //}
        public MedicalWorker(Person person)
        {
            Person = person;
        }
        public MedicalWorker(long personId)
        {
            PersonId = personId;
        }

        //public MedicalWorker(Person person,DateTime hiredSince, string professionlTitle, MedicalWorkerType medicalWorkerType,List<string> education ,string experience, string imaagePath)
        //{

        //}
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]

        public long Id { get; set; }
        public long PersonId { get; set; }
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [Required(ErrorMessage = "Wprowadź numer zawodowy!")]
        [Display(Name = "Numer zawodowy")]

        public string ProfessionalNumber {get;set;}
        public DateTime HiredSince { get; set; }
        //public DateTime HiredUntil { get; set; }
        
        [Display(Name = "Czy zatrudniony obecnie")]

        public bool IsCurrentlyHired { get; set; }
        [Display(Name = "Tytuł zawodowy")]

        public abstract string ProfessionalTitle { get; }
        //public abstract string FullProffesionalName { get; }
        public string FullProffesionalName => ProfessionalTitle + " " + Person.Name + " " + Person.Surname;
        [NotMapped]
        public  List<Visit> FutureVisits 
        {
            get
            {
                return AllVisits.Where(c => c.VisitStatus == VisitStatus.AvailableNotBooked || c.VisitStatus == VisitStatus.Booked).ToList();
            }
        }
        [NotMapped]
        public  List<Visit> PastVisits 
        {
            get
            {
                return AllVisits.Where(c => c.VisitStatus == VisitStatus.Finished || c.VisitStatus == VisitStatus.NotHeldOther || c.VisitStatus == VisitStatus.NotHeldAbsentPatient).ToList();
            }
        }

        public virtual List<Visit> AllVisits
        {
            get;
            set;
            //get
            //{
            //    return FutureVisits.Union(PastVisits).ToList();
            //}
        }
        public virtual List<VisitReview> VisitReviews { get; set; }
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
        [Required(ErrorMessage = "Wprowadź typ pracownika medycznego!")]
        [Display(Name = "Typ pracownika medycznego")]

        public MedicalWorkerType? MedicalWorkerType { get; set; }
        //public List<string> Education { get; set; }
        [Required(ErrorMessage = "Wprowadź streszczenie odnośnie zdobytego wykształcenia, przebytych kursów itp.!")]
        [Display(Name = "Edukacja/wykształcenie")]

        public string Education { get; set; }
        [Required(ErrorMessage = "Wprowadź streszczenie odnośnie zdobytego doświadczenia!")]
        [Display(Name = "Doświadczenie")]

        public string Experience { get; set; }
        //public string ImagePath { get; set; }

        [Required(ErrorMessage = "Specjalizacja/świadczone usługi")]
        [Display(Name = "Usługi")]
        [NotMapped]
        public List<long> MedicalServiceIds { get; set; }
        [Required(ErrorMessage = "Specjalizacja/świadczone usługi")]
        [Display(Name = "Usługi")]

        public virtual List<MedicalService> MedicalServices { get; set; }
        public virtual List<MedicalServiceMedicalWorker> MedicalServiceMedicalWorker { get; set; }


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
            //if (worker.IsCurrentlyHired.HasValue)
            //{
                this.IsCurrentlyHired = worker.IsCurrentlyHired;
            //}
            if (worker.MedicalServiceIds!=null)
            {
                this.MedicalServiceIds = worker.MedicalServiceIds;
            }
            if (worker.MedicalServices!=null)
            {
                this.MedicalServices = worker.MedicalServices;
            }
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

        public string RatingDescription
        {
            get
            {
                if (VisitReviews.Count > 0)
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
                    return AverageRating.ToString() + " (" + VisitReviews.Count + " " + ratingV +")";
                }
                else
                {
                    return "";
                }
            }
        }
        public bool IsValid
        {
            get
            {
                if (MedicalWorkerType.HasValue)
                {
                    if (MedicalServices!=null)
                    {
                        if (!string.IsNullOrWhiteSpace( Experience))
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