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


        //public List<Visit> DashboardItems 
        //{ 
        //    get
        //    {
        //        List<Visit> items = new List<Visit>();
        //        items.AddRange(GetCommingVisits(10));
        //        items.AddRange(GetHistoricalVisits());

        //        return items;
        //    }

        //}
        //private IEnumerable<object> GetMedicalResults(int numberOfResults = 3)
        //{
        //    List<Visit> commingVisits = new List<Visit>();
        //    List<Visit> visits = Patient.BookedVisits.OrderByDescending(c=>c.DateTimeSince).ToList();

        //    if (visits.Count > 0)
        //    {
        //        if (numberOfResults > visits.Count)
        //        {
        //            numberOfResults = visits.Count;
        //        }

        //        for (int i = 0; i < numberOfResults; i++)
        //        {
        //            commingVisits.Add(visits.ElementAt(i));
        //        }
        //        return commingVisits;
        //    }
        //    else
        //    {
        //        return commingVisits;
        //    }
        //}

        //dla odbytych - klasa nowa czy lista obiektów?
        public List<Visit> GetCommingVisits(int numberOfVisits=3)
        {
            //List<Visit> commingVisits = new List<Visit>();
            //if (Patient.BookedVisits==null)
            //{
            //    return null;
            //}
            List<Visit> visits = Visits
                .Where(c=>c.DateTimeSince.Date>=DateTime.Now.Date)
                .OrderBy(c=>c.DateTimeSince).ToList();

            if (visits.Count>0)
            {
                if (numberOfVisits> visits.Count)
                {
                    numberOfVisits = visits.Count;
                }

                //for (int i = 0; i < numberOfVisits; i++)
                //{
                //    commingVisits.Add(visits.ElementAt(i));
                //}
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
            //List<Visit> visits = Visits.Where(c => c.VisitStatus == Core.Enums.VisitStatus.Finished && c.ExaminationReferrals!=null).OrderByDescending(c => c.DateTimeSince).ToList();  //Patient.HistoricalVisits.OrderByDescending(c => c.DateTimeSince).ToList();

            ////referrals = Patient.MedicalReferrals.OrderBy(c => c.IssueDate).ToList();

            //if (visits.Count > 0)
            //{
            //    if (numberOfVisits > visits.Count)
            //    {
            //        numberOfVisits = visits.Count;
            //    }

            //    for (int i = 0; i < numberOfVisits; i++)
            //    {
            //        if (visits.ElementAt(i).ExaminationReferrals!=null)
            //        {
            if (MedicalReferrals==null)
            {
                return null;
            }
            if (MedicalReferrals.Count()>0)
            {
                //for (int j = 0; j < MedicalReferrals.Count(); j++)
                //{
                //    if (MedicalReferrals.ElementAt(j).IsActive)
                //    {
                //        validReferrals.Add(MedicalReferrals.ElementAt(j));
                //    }
                //}
                //    }
                //}
                validReferrals = MedicalReferrals
                    .OrderBy(c => c.IssueDate)
                    .ToList();
                return validReferrals;

            }
            //            List<MedicalReferral> referrals = visits.ElementAt(i).ExaminationReferrals;
            //}
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
            List<Visit> visits = Visits
                //Where(c => c.VisitStatus == Core.Enums.VisitStatus.Finished)
                .OrderByDescending(c => c.DateTimeSince)
                .ToList(); //Patient.HistoricalVisits.OrderByDescending(c => c.DateTimeSince).ToList();
            if (visits.Count > 0)
            {
                if (numberOfVisits > visits.Count)
                {
                    numberOfVisits = visits.Count;
                }
                return visits.Take(numberOfVisits).ToList();
                //for (int i = 0; i < numberOfVisits; i++)
                //{
                //    pastVisits.Add(visits.ElementAt(i));
                //}
                //return pastVisits;
            }
            else
            {
                return null;
            }
        }
        public List<MedicalReferral> GetValidReferrals()
        {
           // List<MedicalReferral> referrals = null;// new List<MedicalReferral>();
            //referrals = Visits.Where(c => c.ExaminationReferrals != null).OrderByDescending(c => c.DateTimeSince).SelectMany(d => d.ExaminationReferrals).ToList(); //Patient.MedicalReferrals.Where(c => c.IsActive == true).ToList();
            //return referrals;
            return GetReferrals().Where(c=>c.IsActive).ToList();
        }
    }
}
