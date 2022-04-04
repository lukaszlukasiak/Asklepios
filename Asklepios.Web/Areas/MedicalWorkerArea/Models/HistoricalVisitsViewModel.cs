using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.MedicalWorkerArea.Models
{
    public class HistoricalVisitsViewModel
    {
        public MedicalWorker MedicalWorker { get; set; }


        public HistoricalVisitsViewModel(MedicalWorker medicalWorker)
        {
            MedicalWorker = medicalWorker;
        }

        public List<Visit> HistoricalVisits 
        {
            get
            {
                return MedicalWorker.PastVisits;
            }
            
        }
        

    }
}
