using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.PatientArea.Models
{
    public class PatientViewModel
    {
        public PatientViewModel(Patient patient)
        {
            Patient = patient;
        }

        public Patient Patient { get; set; }
        public List<Visit> DashboardItems 
        { 
            get
            {
                List<Visit> items = new List<Visit>();
                items.AddRange(GetCommingVisits(10));
                items.AddRange(GetHistoricalVisits());

                return items;
            }
            
        }
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
        public List<Visit> GetHistoricalVisits(int numberOfVisits = 3)
        {
            List<Visit> pastVisits = new List<Visit>();

            List<Visit> visits = Patient.HistoricalVisits;
            if (visits.Count > 0)
            {
                if (numberOfVisits > visits.Count)
                {
                    numberOfVisits = visits.Count;
                }

                for (int i = 0; i < numberOfVisits; i++)
                {
                    pastVisits.Add(Patient.BookedVisits.ElementAt(i));
                }
                return pastVisits;
            }
            else
            {
                return pastVisits;
            }
        }

    }
}
