using Asklepios.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asklepios.Core.Models
{
    public class Patient : Person
    {
        public MedicalPackage MedicalPackage { get; set; }
        public string EmployerName { get; set; }
        public string EmployerNIP { get; set; }
        public NFZUnit NFZUnit { get; set; }
        public List<MedicalTestResult> TestsResults 
        { 
            get
            {
                List<MedicalTestResult> results = HistoricalVisits.Where(c => c.VisitSummary?.MedicalResult != null).Select(c => c.VisitSummary.MedicalResult).ToList();
                return results;
            }
        }
        public List<ExaminationReferral> MedicalReferrals 
        {
            get
            {
                List<ExaminationReferral> referrals = HistoricalVisits.Where(c => c.VisitSummary?.ExaminationReferrals != null).SelectMany(c => c.VisitSummary.ExaminationReferrals).ToList();
                return referrals;
            }
        }
        public List<Prescription> Prescriptions 
        {
            get
            {
                List<Prescription> prescs = HistoricalVisits.Where(c => c.VisitSummary?.Prescription!=null).Select(c=>c.VisitSummary.Prescription).ToList();
                return prescs;
            }          
        }
        //public List<IssuedMedicine> IssuedMedicines { get; set; }
        public List<Visit> HistoricalVisits { get; set; }
        public List<Visit> BookedVisits { get; set; }
        //public List<NotificationFilter> Notifications {get;set;}
        public Patient(string name, string surName, long id, string pesel, bool hasPolishCitizenship, string passportNumber, string passportCode, string email, Aglomeration aglomeration) : base(name, surName, id, pesel, hasPolishCitizenship, passportNumber, passportCode, email, aglomeration)
        {
        }
    }
}
