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
        public IQueryable<Visit> AllVisits { get; set; }
        //public Patient Patient { get; set; }

        public List<Visit> GetCommingVisits(int numberOfVisits=0)
        {

            if (AllVisits.Count()>0)
            {
                if (numberOfVisits==0)
                {
                    return AllVisits.OrderBy(c => c.DateTimeSince).ToList();
                }
                else
                {
                    return AllVisits.OrderBy(c => c.DateTimeSince).Take(numberOfVisits).ToList();

                }
            }
            else
            {
                return null;
            }
        }
        public List<Visit> GetHistoricalVisits(int numberOfVisits = 0)
        {
            if (AllVisits.Count() > 0)
            {
                if (numberOfVisits == 0)
                {
                    return AllVisits.OrderByDescending(c => c.DateTimeSince).ToList();
                }
                else
                {
                    return AllVisits.OrderByDescending(c => c.DateTimeSince).Take(numberOfVisits).ToList();

                }
            }
            else
            {
                return null;
            }
        }
        public List<MedicalReferral> MedicalReferrals { get; set; }
        public List<Prescription> Prescriptions { get; set; }
        public List<MedicalTestResult> TestResults { get; set; }
        //public List<MedicalReferral> GetValidReferrals()
        //{
        //    List<MedicalReferral> referrals = null;// new List<MedicalReferral>();
        //    referrals = AllVisits
        //        .Where(c => c.ExaminationReferrals != null)
        //        .SelectMany(d => d.ExaminationReferrals)
        //        .ToList()
        //        .Where(e => e.IsActive)
        //        .ToList();//SelectedPatient.MedicalReferrals.Where(c => c.IsActive == true).ToList();
        //    return referrals;
        //}
        //public List<Prescription> GetPrescriptions()
        //{
        //    List<Prescription> prescriptions = null;// new List<MedicalReferral>();
        //    prescriptions = AllVisits
        //        .Where(c => c.Prescription!= null)
        //        .Select(d => d.Prescription)
        //        .ToList();
        //    return prescriptions;
        //}
        //public List<MedicalTestResult> GetTestResults()
        //{
        //    List<MedicalTestResult> results = null;// new List<MedicalReferral>();
        //    results = AllVisits
        //        .Where(c => c.MedicalTestResult != null)
        //        .Select(d => d.MedicalTestResult)
        //        .ToList();//
        //    return results;
        //}

    }
}
