using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.CustomerServiceArea.Models
{
    public class PatientViewModel:IBaseViewModel
    {
        public PatientViewModel(Patient patient)
        {
            SelectedPatient = patient;
        }

        public Patient SelectedPatient { get; set; }
        public string UserName { get; set; }

        //public Patient Patient { get; set; }

        public List<Visit> GetCommingVisits(int numberOfVisits=3)
        {
            List<Visit> commingVisits = new List<Visit>();
            List<Visit> visits = SelectedPatient.BookedVisits.OrderBy(c=>c.DateTimeSince).ToList();

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
        public List<Visit> GetHistoricalVisits(int numberOfVisits = 3)
        {
            List<Visit> pastVisits = new List<Visit>();

            List<Visit> visits = SelectedPatient.HistoricalVisits;
            if (visits.Count > 0)
            {
                if (numberOfVisits > visits.Count)
                {
                    numberOfVisits = visits.Count;
                }

                for (int i = 0; i < numberOfVisits; i++)
                {
                    pastVisits.Add(SelectedPatient.HistoricalVisits.ElementAt(i));
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
            referrals = SelectedPatient.MedicalReferrals.Where(c => c.IsActive == true).ToList();
            return referrals;
        }
    }
}
