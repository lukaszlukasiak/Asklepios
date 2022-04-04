using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.MedicalWorkerArea.Models
{
    public class ScheduleViewModel
    {
        public MedicalWorker MedicalWorker { get; set; }
        //public List<Visit> AllForthcomingVisits { get; set; }
        public  List<Visit> ForthcomingVisits(int number)
        {
            if (MedicalWorker.FutureVisits.Count<number)
            {
                return MedicalWorker.FutureVisits.OrderBy(c => c.DateTimeSince).ToList();
            }
            else
            {
                return MedicalWorker.FutureVisits.OrderBy(c => c.DateTimeSince).Take(number).ToList();
            }
        }
        public      ScheduleViewModel(MedicalWorker medicalWorker)
        {
            MedicalWorker = medicalWorker;
        }
        

    }
}
