using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.PatientArea.Models
{
    public class PatientViewModel:IBaseViewModel
    {
        public PatientViewModel(Patient patient)
        {
            Patient = patient;
        }

        public Patient Patient { get; set; }
        public string UserName { get; set; }
        public List<Notification> Notifications { get; set; }
        public IQueryable<Visit> Visits { get; set; }
        public IQueryable<MedicalReferral> MedicalReferrals { get; set; }
        public IQueryable<Prescription> Prescriptions { get; set; }
        public IQueryable<MedicalTestResult>MedicalTestResults { get; set; }

        public List<Visit> GetCommingVisits(int numberOfVisits=3)
        {
            List<Visit> visits = Visits
                .Where(c=>c.DateTimeSince.Date>=DateTime.Now.Date)
                .OrderBy(c=>c.DateTimeSince).ToList();

            if (visits.Count>0)
            {
                if (numberOfVisits> visits.Count)
                {
                    numberOfVisits = visits.Count;
                }

                List<Visit> list= visits.Take(numberOfVisits).ToList();
                return list;
            }
            else
            {
                return null;
            }
        }

        public List<MedicalTestResult> GetMedicalTestResults()
        {
            if (MedicalTestResults==null)
            {
                return null;
            }
            List<MedicalTestResult> medicalTestResults = MedicalTestResults
                .OrderBy(c => c.ExamDate)
                .ToList();
            return medicalTestResults;
        }

        public List<MedicalReferral> GetReferrals()
        {
            List<MedicalReferral> validReferrals = null;// new List<MedicalReferral>();
    
            if (MedicalReferrals==null)
            {
                return null;
            }
            if (MedicalReferrals.Count()>0)
            {
                validReferrals = MedicalReferrals
                    .OrderBy(c => c.IssueDate)
                    .ToList();
                return validReferrals;

            }
            else
            {
                return null;
            }
        }

        public List<Visit> GetHistoricalVisits(int numberOfVisits = 3)
        {
            if (Visits==null)
            {
                return null;
            }
            IQueryable<Visit> visits = Visits
                .Where(c => c.VisitStatus == Core.Enums.VisitStatus.Finished)
                .OrderByDescending(c => c.DateTimeSince)
                .AsQueryable(); //Patient.HistoricalVisits.OrderByDescending(c => c.DateTimeSince).ToList();
            if (visits.Count() > 0)
            {
                if (numberOfVisits > visits.Count())
                {
                    numberOfVisits = visits.Count();
                }
                return visits.Take(numberOfVisits).ToList();
            }
            else
            {
                return null;
            }
        }
        public List<MedicalReferral> GetValidReferrals()
        {
            return GetReferrals().Where(c=>c.IsActive).ToList();
        }
    }
}
