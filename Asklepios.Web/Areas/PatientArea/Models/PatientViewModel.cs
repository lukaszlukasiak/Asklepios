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
            List<Visit> commingVisits = new List<Visit>();
            if (Patient.BookedVisits==null)
            {
                return null;
            }
            List<Visit> visits = Patient.BookedVisits.OrderBy(c=>c.DateTimeSince).ToList();

            if (visits.Count>0)
            {
                if (numberOfVisits> visits.Count)
                {
                    numberOfVisits = visits.Count;
                }

                for (int i = 0; i < numberOfVisits; i++)
                {
                    commingVisits.Add(visits.ElementAt(i));
                }
                return commingVisits;
            }
            else
            {
                return commingVisits;
            }
        }
        public List<MedicalReferral> GetReferrals(int numberOfVisits = 10)
        {
            List<MedicalReferral> validReferrals= new List<MedicalReferral>();
            List<Visit> visits = Patient.HistoricalVisits.OrderByDescending(c => c.DateTimeSince).ToList();

            //referrals = Patient.MedicalReferrals.OrderBy(c => c.IssueDate).ToList();

            if (visits.Count > 0)
            {
                if (numberOfVisits > visits.Count)
                {
                    numberOfVisits = visits.Count;
                }

                for (int i = 0; i < numberOfVisits; i++)
                {
                    if (visits.ElementAt(i).ExaminationReferrals!=null)
                    {
                        List<MedicalReferral> referrals = visits.ElementAt(i).ExaminationReferrals;
                        for (int j = 0; j < referrals.Count; j++)
                        {
                            if (referrals.ElementAt(j).IsActive)
                            {
                                validReferrals.Add(referrals.ElementAt(j));
                            }
                        }
                    }
                }
                return validReferrals;
            }
            else
            {
                return validReferrals;
            }
        }

        public List<Visit> GetHistoricalVisits(int numberOfVisits = 3)
        {
            List<Visit> pastVisits = new List<Visit>();

            if (Patient.HistoricalVisits==null)
            {
                return null;
            }
            List<Visit> visits = Patient.HistoricalVisits.OrderByDescending(c => c.DateTimeSince).ToList();
            if (visits.Count > 0)
            {
                if (numberOfVisits > visits.Count)
                {
                    numberOfVisits = visits.Count;
                }

                for (int i = 0; i < numberOfVisits; i++)
                {
                    pastVisits.Add(visits.ElementAt(i));
                }
                return pastVisits;
            }
            else
            {
                return pastVisits;
            }
        }
        public List<MedicalReferral> GetValidReferrals()
        {
            List<MedicalReferral> referrals = null;// new List<MedicalReferral>();
            referrals = Patient.MedicalReferrals.Where(c => c.IsActive == true).ToList();
            return referrals;
        }
    }
}
