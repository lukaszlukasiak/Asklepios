using Asklepios.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asklepios.Web.Areas.MedicalWorkerArea.Models
{
    public class CurrentVisitViewModel
    {
        private Visit Visit { get; set; }
        public MedicalWorker MedicalWorker { get; }

        public CurrentVisitViewModel(Visit visit)
        {
            Visit = visit;
        }

        public CurrentVisitViewModel(MedicalWorker medicalWorker)
        {
            MedicalWorker = medicalWorker;
        }
    }
}
