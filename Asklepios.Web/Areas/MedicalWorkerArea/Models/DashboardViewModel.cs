using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.MedicalWorkerArea.Models
{
    public class DashboardViewModel
    {
        public MedicalWorker MedicalWorker { get; set; }
        public List<Visit> TodayVisits { get; set; }

        public DashboardViewModel(MedicalWorker medicalWorker)
        {
            MedicalWorker = medicalWorker;
            TodayVisits = medicalWorker.FutureVisits?.Where(c => c.DateTimeSince == DateTimeOffset.Now.Date).ToList();
        }
    }
}
