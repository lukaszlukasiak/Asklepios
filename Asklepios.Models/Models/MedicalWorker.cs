using Asklepios.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asklepios.Core.Models
{
    public abstract class MedicalWorker : Person
    {
        public MedicalWorker(string name, string surName, long id, string pesel, bool hasPolishCitizenship, string passportNumber, string passportCode, string email, Aglomeration aglomeration)
            : base(name, surName, id, pesel, hasPolishCitizenship, passportNumber, passportCode, email, aglomeration)
        {
        }
        //public MedicalWorker(Person person,DateTime hiredSince, string professionlTitle, MedicalWorkerType medicalWorkerType,List<string> education ,string experience, string imaagePath)
        //{

        //}
        public DateTime HiredSince { get; set; }
        //public DateTime HiredUntil { get; set; }
        public bool IsCurrentlyHired { get; set; }
        public abstract string ProfessionalTitle { get; }
        //public abstract string FullProffesionalName { get; }
        public string FullProffesionalName => ProfessionalTitle + " " + Name + " " + Surname;
        public List<Visit> FutureVisits { get; set; }
        public List<Visit> PastVisits { get; set; }
        public List<VisitReview> VisitReviews { get; set; }
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
        public MedicalWorkerType MedicalWorkerType { get; set; }
        public List<string> Education { get; set; }
        public string Experience { get; set; }
        public string ImagePath { get; set; }
        public List<MedicalService> MedicalServices { get; set; }
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
    }
}