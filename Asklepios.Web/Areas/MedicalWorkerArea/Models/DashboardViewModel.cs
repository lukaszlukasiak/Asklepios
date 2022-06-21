using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.MedicalWorkerArea.Models
{
    public class DashboardViewModel: IBaseViewModel
    {
        public MedicalWorker MedicalWorker { get; set; }
        public List<Visit> TodayVisits { get; set; }
        public long CurrentVisitId { get; set; }
        public Visit CurrentVisit
        {
            get
            {
                if (CurrentVisitId>0)
                {
                    Visit visit = TodayVisits.First(c => c.Id == CurrentVisitId);
                    return visit;
                }
                else
                {
                    return null;
                }
            }
        }
        public List<Visit> FinishedVisits
        {
            get
            {
                List<Visit> visits = TodayVisits.Where(c => c.VisitStatus == Core.Enums.VisitStatus.Finished).ToList();
                return visits;
            }
        }
        public List<Visit> NotHeldNoPatient
        {
            get
            {
                List<Visit> visits = TodayVisits.Where(c => c.VisitStatus == Core.Enums.VisitStatus.NotHeldAbsentPatient).ToList();
                return visits;
            }
        }
        public List<Visit> NotHeldOtherReason
        {
            get
            {
                List<Visit> visits = TodayVisits.Where(c => c.VisitStatus == Core.Enums.VisitStatus.NotHeldOther).ToList();
                return visits;
            }
        }

        public List<Visit> RestOfTodaysVisits
        {
            get
            {
                List<Visit> rest = new List<Visit>(TodayVisits);
                foreach (Visit item in FinishedVisits)
                {
                    if (!(item.VisitStatus == Core.Enums.VisitStatus.AvailableNotBooked || item.VisitStatus==Core.Enums.VisitStatus.Booked))
                    {
                        rest.Remove(item);
                    }
                }
                rest.Remove(CurrentVisit);
                return rest;
            }
        }

        public string UserName { get; set; }

        public DashboardViewModel(MedicalWorker medicalWorker)
        {
            MedicalWorker = medicalWorker;
            TodayVisits = medicalWorker.FutureVisits?.Where(c => c.DateTimeSince.Date == DateTimeOffset.Now.Date).ToList();
        }
    }
}
