using Asklepios.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asklepios.Core.Models
{
    public class Patient 
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public Person Person { get; set; }
        public MedicalPackage MedicalPackage { get; set; }
        public string EmployerName { get; set; }
        public string EmployerNIP { get; set; }
        public NFZUnit NFZUnit { get; set; }
        public List<MedicalTestResult> TestsResults 
        { 
            get
            {
                List<MedicalTestResult> results = HistoricalVisits.Where(c => c.MedicalResult != null).Select(c => c.MedicalResult).ToList();
                return results;
            }
        }
        public List<MedicalReferral> MedicalReferrals 
        {
            get
            {
                List<MedicalReferral> referrals = HistoricalVisits.Where(c => c.ExaminationReferrals != null).SelectMany(c => c.ExaminationReferrals).ToList();
                //List<ExaminationReferral> referrals = HistoricalVisits.Where(c => c.VisitSummary?.ExaminationReferrals != null).SelectMany(c => c.VisitSummary.ExaminationReferrals).ToList();

                return referrals;
            }
        }
        public List<Prescription> Prescriptions 
        {
            get
            {
                List<Prescription> prescs = HistoricalVisits.Where(c => c.Prescription!=null).Select(c=>c.Prescription).ToList();
                //List<Prescription> prescs = HistoricalVisits.Where(c => c.VisitSummary?.Prescription != null).Select(c => c.VisitSummary.Prescription).ToList();
                return prescs;
            }          
        }
        //public List<IssuedMedicine> IssuedMedicines { get; set; }
        public List<Visit> HistoricalVisits { get; set; }
        public List<Visit> BookedVisits { get; set; }

        public void BookVisit(Visit visit)
        {
            visit.Patient = this;
            BookedVisits.Add(visit);
        }

        //public List<NotificationFilter> Notifications {get;set;}
        //public Patient(string name, string surName, long id, string pesel, bool hasPolishCitizenship, string passportNumber, string passportCode, string email, Aglomeration aglomeration) : base(name, surName, id, pesel, hasPolishCitizenship, passportNumber, passportCode, email, aglomeration)
        //{
        //}
        public Patient (Person person)
        {
            Person = person;
            PersonId = person.Id;
        }
    }
}
